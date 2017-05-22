using System.Linq;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AcNomorAtribut
{
    public class Commands
    {
        static int _index = 1;
       
        private System.Data.DataTable blockdt; 

        public System.Data.DataTable blokRefTabel
        {
            get { return blockdt; }
        }

        public enum FileExportOption
        {Xls = 0, CSV = 1, Data = 2};

        [CommandMethod("TBH")]
        public void TraceBoundaryAndHatch()
        {
            Document doc =
              Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            // Select a seed point for our boundary

            PromptPointResult ppr =
              ed.GetPoint("\nSelect internal point: ");

            if (ppr.Status != PromptStatus.OK)
                return;

            // Get the objects making up our boundary

            DBObjectCollection objs =
              ed.TraceBoundary(ppr.Value, false);

            if (objs.Count > 0)
            {
                Transaction tr =
                  doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    // We'll add the objects to the model space

                    BlockTable bt =
                      (BlockTable)tr.GetObject(
                        doc.Database.BlockTableId,
                        OpenMode.ForRead
                      );

                    BlockTableRecord btr =
                      (BlockTableRecord)tr.GetObject(
                        bt[BlockTableRecord.ModelSpace],
                        OpenMode.ForWrite
                      );

                    // Add our boundary objects to the drawing and
                    // collect their ObjectIds for later use

                    ObjectIdCollection ids = new ObjectIdCollection();
                    foreach (DBObject obj in objs)
                    {
                        Entity ent = obj as Entity;
                        if (ent != null)
                        {
                            // Set our boundary objects to be of
                            // our auto-incremented colour index

                            ent.ColorIndex = _index;

                            // Set our transparency to 50% (=127)
                            // Alpha value is Truncate(255 * (100-n)/100)

                            ent.Transparency = new Transparency(127);

                            // Add each boundary object to the modelspace
                            // and add its ID to a collection

                            ids.Add(btr.AppendEntity(ent));
                            tr.AddNewlyCreatedDBObject(ent, true);
                        }
                    }

                    // Create our hatch

                    Hatch hat = new Hatch();

                    // Solid fill of our auto-incremented colour index

                    hat.SetHatchPattern(
                      HatchPatternType.PreDefined,
                      "SOLID"
                    );
                    hat.ColorIndex = _index++;

                    // Set our transparency to 50% (=127)
                    // Alpha value is Truncate(255 * (100-n)/100)

                    hat.Transparency = new Transparency(127);

                    // Add the hatch to the modelspace & transaction

                    ObjectId hatId = btr.AppendEntity(hat);
                    tr.AddNewlyCreatedDBObject(hat, true);

                    // Add the hatch loops and complete the hatch

                    hat.Associative = true;
                    hat.AppendLoop(
                      HatchLoopTypes.Default,
                      ids
                    );

                    hat.EvaluateHatch(true);

                    // Commit the transaction

                    tr.Commit();
                }
            }
        }

        [CommandMethod("ExportAttribute", "expatt", CommandFlags.Session | CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void ExportBref(FileExportOption exportmode)
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            TypedValue[] filter = { new TypedValue(0, "INSERT") };
            PromptSelectionResult psr = ed.GetSelection(new SelectionFilter(filter));
            if (psr.Status != PromptStatus.OK) return;
            using (DocumentLock acdocLock = doc.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        switch (exportmode)
                        {
                            case FileExportOption.Xls:
                                PromptPointResult ppr = ed.GetPoint("\nInsertion point: ");
                                if (ppr.Status != PromptStatus.OK) return;
                                System.Data.DataTable dataTable = psr.Value.GetObjectIds()
                                    .Select(id => new BlockAttribute(id.GetObject<BlockReference>()))
                                    .ToDataTable("Extraction");
                                Table tbl = dataTable.ToAcadTable(9.0, 40.0);
                                tbl.Position = ppr.Value.TransformBy(ed.CurrentUserCoordinateSystem);
                                BlockTableRecord btr = db.CurrentSpaceId.GetObject<BlockTableRecord>(OpenMode.ForWrite);
                                btr.AppendEntity(tbl);
                                tr.AddNewlyCreatedDBObject(tbl, true);
                                try
                                {
                                    string filename = (string)AcAp.GetSystemVariable("dwgprefix") + "Extraction.xls";
                                    string sheet = "DataUnit"; 
                                    dataTable.WriteXls(filename, sheet , true);
                                    //blkdt = dataTable;
                                }
                                catch
                                {
                                    AcAp.ShowAlertDialog("Failed to open Excel");
                                }
                                break;                                
                            case FileExportOption.CSV:
                                break;
                            case FileExportOption.Data:
                                blockdt = psr.Value.GetObjectIds()
                                    .Select(id => new BlockAttribute(id.GetObject<BlockReference>()))
                                    .ToDataTable("Data Unit");
                                break;
                        }
                    }
                    catch (System.Exception exp)
                    {
                        AcAp.ShowAlertDialog(exp.Message);
                    }
                    tr.Commit();
                }                               
            }
        }
        
        [CommandMethod("AppendAttributeToBlock", "appatt", CommandFlags.Session | CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public static void AppendAttributeTest()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;

            Editor ed = doc.Editor;

            Database db = doc.Database;

            try
            {
                using (doc.LockDocument())
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                        BlockTableRecord currSp = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;


                        PromptNestedEntityOptions pno =

                         new PromptNestedEntityOptions("\nSelect source attribute to append new attribute below this one >>");

                        PromptNestedEntityResult nres =

                                                ed.GetNestedEntity(pno);

                        if (nres.Status != PromptStatus.OK)

                            return;

                        ObjectId id = nres.ObjectId;

                        Entity ent = (Entity)tr.GetObject(id, OpenMode.ForRead);

                        Point3d pnt = nres.PickedPoint;

                        ObjectId owId = ent.OwnerId;

                        AttributeReference attref = null;

                        if (id.ObjectClass.IsDerivedFrom(RXClass.GetClass(typeof(AttributeReference))))
                        {
                            attref = tr.GetObject(id, OpenMode.ForWrite) as AttributeReference;
                        }


                        BlockTableRecord btr = null;

                        BlockReference bref = null;

                        if (owId.ObjectClass.IsDerivedFrom(RXClass.GetClass(typeof(BlockReference))))
                        {
                            bref = tr.GetObject(owId, OpenMode.ForWrite) as BlockReference;

                            if (bref.IsDynamicBlock)
                            {
                                btr = tr.GetObject(bref.DynamicBlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                            }
                            else
                            {
                                btr = tr.GetObject(bref.BlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                            }
                        }

                        Point3d insPt = attref.Position.TransformBy(bref.BlockTransform);

                        btr.UpgradeOpen();

                        ObjectIdCollection bids = new ObjectIdCollection();

                        AttributeDefinition def = null;

                        foreach (ObjectId defid in btr)
                        {
                            if (defid.ObjectClass.IsDerivedFrom(RXClass.GetClass(typeof(AttributeDefinition))))
                            {
                                def = tr.GetObject(defid, OpenMode.ForRead) as AttributeDefinition;

                                if (def.Tag == attref.Tag)
                                {
                                    def.UpgradeOpen();

                                    bids.Add(defid);

                                    break;
                                }
                            }
                        }



                        IdMapping map = new IdMapping();

                        db.DeepCloneObjects(bids, btr.ObjectId, map, true);

                        ObjectIdCollection coll = new ObjectIdCollection();

                        AttributeDefinition attDef = null;

                        foreach (IdPair pair in map)
                        {
                            if (pair.IsPrimary)
                            {
                                Entity oent = (Entity)tr.GetObject(pair.Value, OpenMode.ForWrite);

                                if (oent != null)
                                {

                                    if (pair.Value.ObjectClass.IsDerivedFrom(RXClass.GetClass(typeof(AttributeDefinition))))
                                    {
                                        attDef = oent as AttributeDefinition;

                                        attDef.UpgradeOpen();

                                        attDef.SetPropertiesFrom(def as Entity);
                                        // add other properties from source attribute definition to suit here:

                                        attDef.Justify = def.Justify;

                                        attDef.Position = btr.Origin.Add(

                                            new Vector3d(attDef.Position.X, attDef.Position.Y - attDef.Height * 1.25, attDef.Position.Z)).TransformBy(Matrix3d.Identity

                                            );

                                        attDef.Tag = "NEW_TAG";

                                        attDef.TextString = "New Prompt";

                                        attDef.TextString = "New Textstring";

                                        coll.Add(oent.ObjectId);


                                    }
                                }
                            }
                        }
                        btr.AssumeOwnershipOf(coll);

                        btr.DowngradeOpen();

                        attDef.Dispose();//optional

                        bref.RecordGraphicsModified(true);

                        tr.TransactionManager.QueueForGraphicsFlush();

                        doc.TransactionManager.FlushGraphics();//optional

                        ed.UpdateScreen();

                        tr.Commit();
                    }

                }
            }

            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                AcAp.ShowAlertDialog("Call command \"ATTSYNC\" manually");

            }

        }

        [CommandMethod("DATA ATTRIBUT", "DTATR", CommandFlags.Session | CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
        public void ListBlockRef()
        {            
            mainform frmBrefColl = new mainform();
            AcAp.ShowModalDialog(frmBrefColl);        
        }

        List<string> ListblockRefName;

        public ObjectIdCollection GetModelSpaceBlockReferenceOIDCByBlockName(string blockReferenceName)
        {
            ObjectIdCollection returnOIDs = new ObjectIdCollection();
            
            using (Database db = AcAp.DocumentManager.MdiActiveDocument.Database)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btrModel = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    foreach (ObjectId thisOid in btrModel)
                    {
                        Entity blk = (Entity)trans.GetObject(thisOid, OpenMode.ForRead);
                        if (blk.GetType().ToString() == "Autodesk.AutoCAD.DatabaseServices.BlockReference")
                        {
                            BlockReference thisBlockRef = (BlockReference)trans.GetObject(thisOid, OpenMode.ForRead);
                            ListblockRefName = new List<string>();
                            if (string.Compare(thisBlockRef.Name, blockReferenceName, true) == 0)
                            {
                                returnOIDs.Add(thisOid);
                            }
                            ListblockRefName.Add(thisBlockRef.Name);
                        }
                    }
                    return returnOIDs;
                }
            }
        }

        public List<string> GetModelSpaceBlockReferenceOIDCByBlockName()
        {
            ObjectIdCollection returnOIDs = new ObjectIdCollection();
            List<string> RefList = new List<string>();
            using (Database db = AcAp.DocumentManager.MdiActiveDocument.Database)
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btrModel = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                    foreach (ObjectId thisOid in btrModel)
                    {
                        Entity blk = (Entity)trans.GetObject(thisOid, OpenMode.ForRead);
                        if (blk.GetType().ToString() == "Autodesk.AutoCAD.DatabaseServices.BlockReference")
                        {
                            BlockReference thisBlockRef = (BlockReference)trans.GetObject(thisOid, OpenMode.ForRead);
                            ListblockRefName = new List<string>();
                            RefList.Add(thisBlockRef.Name);
                        }
                    }
                    return RefList;
                }
            }
        }
        
        [CommandMethod("shbt")]
        public void showBlockTree()
        {
            BlockTreeView.TreeViewForm tfm = new BlockTreeView.TreeViewForm();
            AcAp.ShowModalDialog(tfm);
        }

        [CommandMethod("dtblk")]
        public void datablk()
        {
            formTest ftest = new formTest();
            AcAp.ShowModalDialog(ftest);
        }

        [CommandMethod("atnum")]
        public void showFormAutoNumber()
        {
            formIncrementBlock frmauto = new formIncrementBlock();
            if (frmauto.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
            //AcAp.ShowModalDialog(frmauto);
        }



        
    }
}
