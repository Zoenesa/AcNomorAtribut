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
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AcNomorAtribut
{
    public class KoleksiBlok
    {
        private List<FileInfo> ListFileDwgs;

        private Database acDbs;

        private Document acDoc;

        /// <summary>
        /// Enum Type Pilihan Drawing
        /// </summary>
        public enum PilihanDrawing
        {
            CurrentDrawing = 0,
            PickFromDrawing = 1,
            ExternalDrawing = 2
        }

        public KoleksiBlok()
        {

        }

        public KoleksiBlok(PilihanDrawing mode)
        {
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    acDoc = AcAp.DocumentManager.MdiActiveDocument;
                    acDbs = acDoc.Database;
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    break;
                default:
                    break;
            }
        }

        public KoleksiBlok(PilihanDrawing mode, string[] FileNames)
        {
            if (mode == PilihanDrawing.ExternalDrawing)
            {
                ListFileDwgs = new List<FileInfo>();
                dbFiles = new List<Database>();
                for (int i = 0; i < FileNames.Length; i++)
                {
                    ListFileDwgs.Add(new FileInfo(FileNames[i]));
                    dbFiles.Add(new Database(false, true));
                    //try
                    //{
                    //    dbFiles[i].ReadDwgFile(ListFileDwgs[i].FullName, FileShare.Read, false, "");
                    //}
                    //catch (Autodesk.AutoCAD.Runtime.Exception acExep)
                    //{
                    //    throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.FilerError, acExep.Message);
                    //}
                }
            }
        }

        private List<Database> dbFiles;

        private List<Database> DBFiles
        {
            get
            { return dbFiles; }
        }

        /// <summary>
        /// Get Koleksi Blok Table ID
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiBlokTableId = new Dictionary<string, ObjectId>();

        /// <summary>
        /// Get Koleksi Block Has Reference Ids
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiBlockReferenceIds = new Dictionary<string, ObjectId>();

        /// <summary>
        /// Get Koleksi All Blok Attribut From Block Reference Ids
        /// </summary>
        public Dictionary<string, ObjectId> KoleksiAtributBlokRefs = new Dictionary<string, ObjectId>();

        /// <summary>
        /// File Drawings
        /// </summary>
        public List<FileInfo> FileDrawings
        {
            get
            {
                return ListFileDwgs;
            }
        }

        /// <summary>
        /// Get Block Attributes From Drawing
        /// </summary>
        /// <param name="mode">Select All From Current Drawing, Pick From Current Drawing, Select All From External Drawing</param>
        public void GetBlokByDrawing(PilihanDrawing mode)
        {
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    acDoc = AcAp.DocumentManager.MdiActiveDocument;
                    acDbs = acDoc.Database;
                    break;
                case PilihanDrawing.PickFromDrawing:
                  
                    break;
                case PilihanDrawing.ExternalDrawing:
                    for (int i = 0; i < (FileDrawings.Count); i++)
                    {
                        acDbs = new Database(false, true);
                        string file = FileDrawings[i].FullName;
                        try
                        {
                            acDbs.ReadDwgFile(file, FileShare.Read, false, "");
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception AcEx)
                        {
                            throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.CantOpenFile, "Error Loading File" + "\n" + AcEx.Message);
                        }
                        using (Transaction tr = acDbs.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)acDbs.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                            foreach (ObjectId tbId in bt)
                            {
                                BlockTableRecord btr = (BlockTableRecord)tbId.GetObject(OpenMode.ForRead);
                                if (btr.IsAnonymous)
                                {
                                    continue;
                                }
                                if (btr.GetBlockReferenceIds(true, false).Count > 0 )
                                {
                                    if (btr.HasAttributeDefinitions)
                                    {
                                        KoleksiBlokTableId.Add(btr.Name, tbId);
                                        ObjectIdCollection ObjRefIds = btr.GetBlockReferenceIds(true, false);
                                        foreach (ObjectId ObjId in ObjRefIds)
                                        {
                                            BlockReference bRef = (BlockReference)tr.GetObject(ObjId, OpenMode.ForRead);
                                            KoleksiBlockReferenceIds.Add(bRef.Name + bRef.Id, ObjId);
                                            foreach (ObjectId AtrId in bRef.AttributeCollection)
                                            {
                                                AttributeReference AttRef = (AttributeReference)tr.GetObject(AtrId, OpenMode.ForRead);
                                                try
                                                {
                                                    if (KoleksiAtributBlokRefs.Keys.Contains(AttRef.Tag))
                                                    {
                                                        continue;
                                                    }
                                                    KoleksiAtributBlokRefs.Add(AttRef.Tag, AttRef.Id);
                                                }
                                                catch
                                                {

                                                }                                                
                                            }
                                        }                                        
                                    }
                                }
                            }
                            
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        
        public Dictionary<string, ObjectId> GetDataBlokAttributes(PilihanDrawing mode)
        {
            Dictionary<string, ObjectId> newList = new Dictionary<string, ObjectId>();
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    break;
                default:
                    break;
            }

            return newList;
        }

        /// <summary>
        /// Interact With Progress Meter CAD Aplication
        /// </summary>
        /// <param name="display">Display String On Progress Meter</param>
        /// <param name="limit">Set Limit / Max. Value</param>
        public void AcProgress(string display, int limit)
        {
            ProgressMeter pm = new ProgressMeter();
            pm.Start(display);
            pm.SetLimit(limit);
            for (int i = 0; i < limit; i++)
            {
                pm.MeterProgress();
                System.Threading.Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
            }
            pm.Stop();
        }

        /// <summary>
        /// Interact With Progress Meter CAD Application
        /// </summary>
        /// <param name="Display">Display String On Progress Meter</param>
        /// <param name="limit">Set Limit / Max. Value</param>
        /// <param name="usedelay">usedelay if true with delay</param>
        /// <param name="delay">delay each loop per milisecond</param>
        public void AcProgress(string Display, int limit, bool usedelay, int delay)
        {
            ProgressMeter pm = new ProgressMeter();
            pm.Start(Display);
            pm.SetLimit(limit);
            for (int i = 0; i < limit; i++)
            {
                if (usedelay)
                {
                    System.Threading.Thread.Sleep(delay);
                    pm.MeterProgress();
                }
                System.Windows.Forms.Application.DoEvents();
                pm.MeterProgress();
            }
            pm.Stop();
        }

        private Dictionary<string, ObjectId> getbtId;

        public Dictionary<string, ObjectId> GetBT_IDs
        {
            get
            { return getbtId; }
        }

        public Dictionary<string, ObjectId> GetBlokTableIdFromDrawing(PilihanDrawing mode)
        {
            Dictionary<string, ObjectId> newList = new Dictionary<string, ObjectId>();
            getbtId = new Dictionary<string, ObjectId>();
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    acDoc = AcAp.DocumentManager.MdiActiveDocument;
                    acDbs = acDoc.Database;

                    using (Transaction tr = acDbs.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = (BlockTable)acDbs.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                        foreach (ObjectId btId in bt)
                        {
                            BlockTableRecord btr = (BlockTableRecord)tr.GetObject(btId, OpenMode.ForRead);
                            if (btr.HasAttributeDefinitions)
                            {
                                    if (newList.Keys.Contains(btr.Name))
                                    {
                                        continue;
                                    }
                                    newList.Add(btr.Name, btId);
                                    getbtId.Add(btr.Name, btId);                                
                            }
                        }
                    }

                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:                     
                    for (int i = 0; i < DBFiles.Count; i++)
                    {
                        acDbs = new Database(false, true);
                        try
                        {
                            acDbs.ReadDwgFile(ListFileDwgs[i].FullName, FileShare.Read, false, "");
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception acExep)
                        {
                            throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.FilerError, acExep.Message);
                        }
                        using (Transaction tr = acDbs.TransactionManager.StartTransaction())
                        {
                            BlockTable bt = (BlockTable)acDbs.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                            foreach (ObjectId btId in bt)
                            {
                                BlockTableRecord btr = (BlockTableRecord)btId.GetObject(OpenMode.ForRead);
                                if (btr.HasAttributeDefinitions)
                                {
                                    if (newList.Keys.Contains(btr.Name))
                                    {
                                        continue;
                                    }
                                    newList.Add(btr.Name, btId);
                                    getbtId.Add(btr.Name, btId);
                                }
                            }
                            //tr.Commit();
                        }
                    }
                    break;
                default:
                    break;
            }
            return newList;
        }

        public Dictionary<string, ObjectId> GetBlockReferenceFromBtIds(PilihanDrawing mode)
        {
            Dictionary<string, ObjectId> newList = new Dictionary<string, ObjectId>();
            ObjectIdCollection objIds = new ObjectIdCollection();
            switch (mode)
            {
                case PilihanDrawing.CurrentDrawing:
                    break;
                case PilihanDrawing.PickFromDrawing:
                    break;
                case PilihanDrawing.ExternalDrawing:
                    foreach (KeyValuePair<string, ObjectId> btName in GetBT_IDs)
                    {
                        for (int i1 = 0; i1 < DBFiles.Count; i1++)
                        {
                            acDbs = new Database(false, true);
                            try
                            {
                                acDbs.ReadDwgFile(ListFileDwgs[i1].FullName, FileShare.Read, false, "");
                            }
                            catch (Autodesk.AutoCAD.Runtime.Exception acExep)
                            {
                                throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.FilerError, acExep.Message);
                            }
                            using (Transaction tr = acDbs.TransactionManager.StartTransaction())
                            {
                                BlockTable bt = acDbs.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[btName.Key], OpenMode.ForRead);
                                objIds = btr.GetBlockReferenceIds(true, false);
                                foreach (ObjectId id in objIds)
                                {
                                    BlockReference bRef = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                                    newList.Add(btr.Name + id, id);
                                }
                            }
                        }
                    }                   
                    break;
                default:
                    break;
            }
            return newList;
        }

        private System.Data.DataTable dtTabel;

        public System.Data.DataTable BuildDatatabel()
        {
            dtTabel = new System.Data.DataTable();
            dtTabel.Columns.Add(new System.Data.DataColumn("Column1", typeof(string), "Drawing File"));
            dtTabel.Columns.Add(new System.Data.DataColumn("Column1", typeof(string), "Block Reference Name"));
            dtTabel.Columns.Add(new System.Data.DataColumn("Column1", typeof(int), "Count"));
            dtTabel.Columns.Add(new System.Data.DataColumn("Column1", typeof(string), "Atribut"));

            System.Data.DataRow drow = dtTabel.NewRow();
            drow.ItemArray = new object[] { (string)"Nama File", (string)"Blok Reference", Convert.ToInt32("1"), ""};
            return dtTabel;
        }


    }
}
