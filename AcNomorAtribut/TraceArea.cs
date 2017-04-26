using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace AcNomorAtribut
{
    public class TraceArea
    {
        public static string GetAreaBound()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            PromptKeywordOptions pkOp = new PromptKeywordOptions("");
            pkOp.Message = "\nUse Island Detection ";
            pkOp.Keywords.Add("Yes");
            pkOp.Keywords.Add("No");
            pkOp.AllowNone = false;
            PromptResult pr = ed.GetKeywords(pkOp);
            if (pr.Status == PromptStatus.Cancel)
            {
                return null;
            }
            string pkostr = pr.StringResult;
            bool flagIsland = false;
            if (pkostr == "Yes") { flagIsland = true; } else { flagIsland = false; }
            PromptPointResult ppr = ed.GetPoint("\nSelect Internal Point on Closed Area: ");
            if (ppr.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n" + ppr.StringResult);
            }
            string value = null;
            DBObjectCollection dbObjColl = ed.TraceBoundary(ppr.Value, flagIsland);
            Double area = 0;
            try
            {
                if (dbObjColl.Count > 0)
                {
                    Transaction tr = doc.TransactionManager.StartTransaction();
                    using (tr)
                    {
                        BlockTable bt = (BlockTable)
                           tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead);
                        BlockTableRecord btr = (BlockTableRecord)
                            tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                        ObjectIdCollection objIds = new ObjectIdCollection();
                        foreach (DBObject Obj in dbObjColl)
                        {
                            Entity ent = Obj as Entity;
                            if (ent != null)
                            {
                                // ent.GetField("Area");
                                if (ent is Polyline)
                                {
                                    Polyline p = (Polyline)ent;
                                    if (p.Closed)
                                    {
                                        area = p.Area;
                                        string presisi = "#";
                                        switch (db.Auprec)
                                        {
                                            case 0:
                                                presisi = "#";
                                                break;
                                            case 1:
                                                presisi = "#0.0";
                                                break;
                                            case 2:
                                                presisi = "#0.00";
                                                break;
                                            case 3:
                                                presisi = "#0.000";
                                                break;
                                            case 4:
                                                presisi = "#0.0000";
                                                break;
                                            case 5:
                                                presisi = "#0.00000";
                                                break;
                                            default:
                                                presisi = "#0.00";
                                                break;
                                        }
                                        value = area.ToString(presisi);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch 
            {

                throw;
            }
            return value;
        }
        
        [CommandMethod("TestCopi")]
        public void CopiBlok()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptEntityOptions pEntOpt = new PromptEntityOptions("\nSelect Block Attribut:");
            pEntOpt.SetRejectMessage("\nInvalid Selection!");
            pEntOpt.AddAllowedClass(typeof(BlockReference), true);
            PromptEntityResult pEntRes = ed.GetEntity(pEntOpt);
            if (pEntRes.Status == PromptStatus.OK)
            {

            }
        }

        [CommandMethod("GetAttInfo")]
        public static void FindAttributeInformation()
        {
            Document doc = AcAp.DocumentManager.MdiActiveDocument;
            if (doc == null) return;
            Editor ed = doc.Editor;
            try
            {
                PromptEntityOptions pEntOpt = new PromptEntityOptions("\nSelect Block Attribut:");
                pEntOpt.SetRejectMessage("\nInvalid Selection!");
                pEntOpt.AddAllowedClass(typeof(BlockReference), true);
                PromptEntityResult pEntRes = ed.GetEntity(pEntOpt);
                if (pEntRes.Status == PromptStatus.OK)
                {
                    GetBlokAttributInfo(pEntRes.ObjectId, ed);
                }
                else { ed.WriteMessage("\n*Cancel*"); }

            }
            catch (System.Exception ex)
            {
                ed.WriteMessage("\nError: {0}\n{1}", ex.Message, ex.StackTrace);
            }
            finally { Autodesk.AutoCAD.Internal.Utils.PostCommandPrompt(); }
        }

        /// <summary>
        /// Get Block Attribute Info In BlockReference
        /// </summary>
        /// <param name="bRefId"></param>
        /// <param name="ed"></param>
        private static void GetBlokAttributInfo(ObjectId bRefId, Editor ed)
        {
            using (Transaction tr = bRefId.Database.TransactionManager.StartTransaction())
            {
                BlockReference bRef = (BlockReference)
                    tr.GetObject(bRefId, OpenMode.ForRead);
                Dictionary<string, string> dic = GetAttributDef(bRef.BlockTableRecord, tr);
                if (bRef.AttributeCollection.Count > 0 && dic.Count > 0)
                {
                    foreach (ObjectId id in bRef.AttributeCollection)
                    {
                        AttributeReference atRef = (AttributeReference)tr.GetObject(id, OpenMode.ForRead);
                        if (dic.ContainsKey(atRef.Tag.ToUpper()))
                        {
                            string[] info = dic[atRef.Tag.ToUpper()].Split('|');
                            ed.WriteMessage(
                                "\nAttribute \"{0}\" -> Prompt: {1}; Default Value: {2}", atRef.Tag, info[0], info[2]);
                        }
                        else
                        {
                            ed.WriteMessage(
                                "\nAttribute \"{0}\" -> Prompt and default value are unknown.", atRef.Tag);
                        }
                    }
                }
                else
                {
                    ed.WriteMessage(
                        "\nEither the selected blockreference does not have attribute\n" +
                        "or the block's definition does not have attribute defined.");
                }
                tr.Commit();
            }
        }

        private static Dictionary<string, string> GetAttributDef(ObjectId bRefId, Transaction tr)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bRefId, OpenMode.ForRead);
            foreach (ObjectId item in btr)
            {
                AttributeDefinition atdef = tr.GetObject(item, OpenMode.ForRead) as AttributeDefinition;
                if (atdef != null)
                {
                    if (!dic.ContainsKey(atdef.Tag.ToUpper()))
                    {
                        dic.Add(atdef.Tag.ToUpper(), atdef.Prompt + "|" + atdef.TextString);
                    }
                }
            }
            return dic;
        }

        private static void ApplyAttibutes(Database db, Transaction tr, BlockReference bref, List<string> listTags, List<string> listValues)
        {
            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bref.BlockTableRecord, OpenMode.ForRead);

            foreach (ObjectId attId in btr)
            {
                Entity ent = (Entity)tr.GetObject(attId, OpenMode.ForRead);
                if (ent is AttributeDefinition)
                {
                    AttributeDefinition attDef = (AttributeDefinition)ent;
                    AttributeReference attRef = new AttributeReference();

                    attRef.SetAttributeFromBlock(attDef, bref.BlockTransform);
                    bref.AttributeCollection.AppendAttribute(attRef);
                    tr.AddNewlyCreatedDBObject(attRef, true);
                    if (listTags.Contains(attDef.Tag.ToUpper()))
                    {
                        ListStringComparer lsc = new ListStringComparer();
                        int found = listTags.BinarySearch(0, 4, attDef.Tag.ToUpper(), lsc);
                        if (found >= 0)
                        {
                            attRef.TextString = listValues[found];
                            attRef.AdjustAlignment(db);
                        }
                    }
                }
            }
        }

        [CommandMethod("iab")]
        public static void testAttributedBlockInsert()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            // change block name to your suit

            PromptEntityOptions pEnOpt = new PromptEntityOptions("\nPick Block Reference:");
            pEnOpt.SetRejectMessage("\nObject Not Block Reference");
            pEnOpt.AddAllowedClass(typeof(BlockReference), true);

            PromptEntityResult pEnRes = ed.GetEntity(pEnOpt);
            ObjectId EntId = pEnRes.ObjectId;
            //PromptResult pr = ed.GetString("\nType Block Name: ");
            //string blockName = pr.StringResult;
            string blockName = null;
            BlockReference UserBlockref = null;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                UserBlockref = (BlockReference)trans.GetObject(EntId, OpenMode.ForRead);
                blockName = UserBlockref.Name;
            }            
            Matrix3d ucs = ed.CurrentUserCoordinateSystem;
            //get current UCS matrix
            try
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    // to force update drawing screen
                    doc.TransactionManager.EnableGraphicsFlush(true);
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForWrite);

                    // if the block table doesn't already exists, exit
                    if (!bt.Has(blockName))
                    {
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Block " + blockName + " does not exist.");
                        return;
                    }

                    // insert the block in the current space
                    PromptPointResult ppr = ed.GetPoint("\nSpecify insertion point: ");
                    if (ppr.Status != PromptStatus.OK)
                    {
                        return;
                    }

                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
                    ObjectContextCollection occ = db.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                    List<string> ListTag = new List<string>();
                    List<string> ListString = new List<string>();

                    Point3d pt = ppr.Value;
                    BlockReference bref = new BlockReference(pt, bt[blockName]);
                    
                    Dictionary<string, string> dic = GetAttributDef(bref.BlockTableRecord, tr);
                    foreach (KeyValuePair<string, string> item in dic)
                    {
                        ListTag.Add(item.Key.ToUpper());
                        // string[] info = item.Value.Split('|');
                    }
                    formUserInputAttribut frmInput = new formUserInputAttribut();
                    IntPtr handle = AcAp.MainWindow.Handle;
                    if (frmInput.ShowDialog(ListTag) == System.Windows.Forms.DialogResult.OK)
                    {
                        ListString.AddRange(frmInput.InputString);
                    }
                    else { return; }

                    bref.Rotation = frmInput.UseRotation ? UserBlockref.Rotation : 0.0;
                   
                    bref.TransformBy(ucs);
                    bref.AddContext(occ.CurrentContext);
                    //add blockreference to current space
                    btr.AppendEntity(bref);
                    tr.AddNewlyCreatedDBObject(bref, true);
                    // set attributes to desired values


                    ApplyAttibutes(db, tr, bref, ListTag, ListString);
                    //    ApplyAttibutes(db, tr, bref, new List<string>(new string[] {
                    //    "TAG_NO",
                    //    "TAG_NUMBLOK",
                    //    "TAG_AT_AREA",
                    //    "TAG_TIPE"
                    //}), new List<string>(new string[] {
                    //    "Value #1",
                    //    "Value #2",
                    //    "Value #3",
                    //    "Value #4"
                    //}));


                    bref.RecordGraphicsModified(true);
                    // to force updating a block reference
                    tr.TransactionManager.QueueForGraphicsFlush();
                    tr.Commit();
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.Message);
            }
            finally
            {
                // Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Pokey")
            }
        }

        
    }


    public class ListStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater.
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Length.CompareTo(y.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.CompareTo(y);
                    }
                }
            }
        }
    }
}
