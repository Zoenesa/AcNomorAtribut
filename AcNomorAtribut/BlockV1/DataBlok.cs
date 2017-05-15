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
 
        }

        public FileInfo infoFile;

        public Dictionary<string, ObjectId> ListBlokTabel;

        public Dictionary<string, ObjectId> ListBlokReference;

        public Dictionary<string, ObjectId> ListAtributReference;

        public List<string> GetParentBlokName()
        {
            List<string> newlist = new List<string>();
            dataParentBlok = new List<string>();
            using (Transaction tr = acDb.TransactionManager.StartTransaction())
            {
                BlockTable bt = acDb.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                foreach (ObjectId objId in bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)objId.GetObject(OpenMode.ForRead);
                    if (btr.IsAnonymous)
                    {
                        continue;
                    }
                    if (btr.GetBlockReferenceIds(true, false).Count > 0)
                    {
                        if (btr.HasAttributeDefinitions)
                        {
                            newlist.Add(btr.Name);
                            dataParentBlok.Add(btr.Name);
                        }
                    }
                }
            }
            return newlist;
        }

        private List<string> dataParentBlok;

        public List<string> AttList;

        public List<string> GetBlokRefs()
        {
            List<string> ParentBlok = new List<string>();
            ParentBlok.AddRange(dataParentBlok);
            List<string> newlist = new List<string>();
            foreach (string item in ParentBlok)
            {
                using (Transaction tr = acDb.TransactionManager.StartTransaction())
                {
                    BlockTable bt = acDb.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[item], OpenMode.ForRead);
                    ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);
                    foreach (ObjectId id in objIds)
                    {
                        BlockReference bRef = (BlockReference)tr.GetObject(id, OpenMode.ForRead);
                        AttList = new List<string>();
                        newlist.Add(string.Format("{0}:{1}",btr.Name, id));
                        foreach (ObjectId idRef in bRef.AttributeCollection)
                        {
                            AttributeReference AttRef = (AttributeReference)tr.GetObject(idRef, OpenMode.ForRead);
                            AttList.Add(String.Format("[{0}:{1}]", AttRef.Tag, AttRef.TextString));
                        }
                    }
                }
            }
            return newlist;
        }
 
    }

   
}
