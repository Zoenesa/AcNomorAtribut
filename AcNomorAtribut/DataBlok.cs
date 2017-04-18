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
    public class DataBlok
    {

        private Document Doc;

        private Database acDb;

        public DataBlok()
        {
            Doc = AcAp.DocumentManager.MdiActiveDocument;
            acDb = Doc.Database;
            HostApplicationServices hs = HostApplicationServices.Current;
            string path = hs.FindFile(Doc.Name, Doc.Database, FindFileHint.Default);
            infoFile = new FileInfo(path);
            ListBlokTabel = new Dictionary<string, ObjectId>();
            ListBlokReference = new Dictionary<string, ObjectId>();
            ListAtributReference = new Dictionary<string, ObjectId>();
            dt = new System.Data.DataTable("DataAtribut");             
            GetBlockTable();
        }

        public FileInfo infoFile;

        public Dictionary<string, ObjectId> ListBlokTabel;

        public Dictionary<string, ObjectId> ListBlokReference;

        public Dictionary<string, ObjectId> ListAtributReference;

        private void GetBlockTable()
        {
            ListBlokTabel = new Dictionary<string, ObjectId>();
            using (Transaction tr = acDb.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)acDb.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                foreach (ObjectId btid in bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(btid, OpenMode.ForRead);
                    if (btr.IsAnonymous)
                    {
                        continue;
                    }
                    if (btr.GetBlockReferenceIds(true, false).Count > 0)
                    {
                        if (btr.HasAttributeDefinitions)
                        {
                            if (ListBlokTabel.Keys.Contains(btr.Name))
                            {
                                continue;
                            }
                            ListBlokTabel.Add(btr.Name, btid);
                            //BlockReference bref = (BlockReference)tr.GetObject(
                            //    btid, OpenMode.ForRead);
                            //foreach (ObjectId refId in bref.AttributeCollection)
                            //{
                            //    AttributeReference atref = (AttributeReference)tr.GetObject(
                            //        refId, OpenMode.ForRead);
                            //    ListAtributReference.Add(atref.Tag, refId);
                            //}
                        }
                    }
                }
            }
        }

        private System.Data.DataTable dt;
        public System.Data.DataTable DataTabel
        {
            get { return dt; }
        }

        public void GetBlokRefs()
        {
            Doc.LockDocument();
            using (Transaction tr = acDb.TransactionManager.StartTransaction())
            {
 
                List<string> arrListTable = new List<string>();
                List<string> arrListRef = new List<string>();
                foreach (KeyValuePair<string, ObjectId> keyListBt in ListBlokTabel)
                { 
                    BlockTable bt = acDb.BlockTableId.GetObject(
                        OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(
                        bt[keyListBt.Key], OpenMode.ForRead);
                    ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);
                    foreach (ObjectId id in objIds)
                    {
                        ListBlokReference.Add(btr.Name, id);
                    }
                }              
            }
        }

        public void GetAttribuName()
        {

        }


    }

   
}
