using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;



namespace BlockTreeView
{
    public partial class TreeViewForm : Form
    {
        private static Document doc;
        private static Database db;
        private static Editor ed;

        public TreeViewForm()
        {
            InitializeComponent();
            FillParentNodes(treeViewBlocks);
            FillChildNodes();
            FillTreeView();
        }

        private void FillBlockNames()
        {
            doc = AcadApp.DocumentManager.MdiActiveDocument;
            db = doc.Database;
            ed = doc.Editor;

            using (Transaction trx = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                foreach (ObjectId objId in bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)objId.GetObject(OpenMode.ForRead);

                    if (btr.HasAttributeDefinitions)
                    {
                        //TreeNode node = treeViewBlocks.Nodes.Add(btr.Name);
                        //if (btr.GetBlockReferenceIds(true, false).Count > 0)
                        //{
                        //node.Nodes.Add("Filler");
                        //}  
                        //continue;
                    }                     
                }
                trx.Commit();
            }
        }

        private Dictionary<string,List<string>> KoleksiBlok(string blkRefName)
        {
            Dictionary<string, List<string>> newList = new Dictionary<string, List<string>>();
            using (Transaction trx = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = (BlockTableRecord)trx.GetObject(bt[blkRefName], OpenMode.ForRead);
                ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);

                foreach (ObjectId id in objIds)
                {
                    BlockReference bRef = (BlockReference)trx.GetObject(id, OpenMode.ForRead);
                    List<string> AttList = new List<string>();
                    foreach (ObjectId idRef in bRef.AttributeCollection)
                    {
                        AttributeReference AttRef = (AttributeReference)trx.GetObject(idRef, OpenMode.ForRead);
                        AttList.Add(String.Format("[{0}:{1}]", AttRef.Tag, AttRef.TextString));
                    }
                    newList.Add(bRef.Name, AttList);
                }
                trx.Commit();
            }

            return newList;
        }

        private List<string> datablok;
        private List<string> DataBlok
        {
            get
            {
                return datablok;
            }
        }

        private List<string> AttributBlok;

        private List<string> DataAtribut
        {
            get
            {
                return AttributBlok;
            }
        }

        private ObjectIdCollection koleksi;
        private ObjectIdCollection DataKoleksiBlok
        {
            get
            { return koleksi; }
        }

        public void FillParentNodes(TreeView tv)
        {
            doc = AcadApp.DocumentManager.MdiActiveDocument;
            db = doc.Database;
            ed = doc.Editor;

            using (Transaction trx = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                foreach (ObjectId objId in bt)
                {
                    BlockTableRecord btr = (BlockTableRecord)objId.GetObject(OpenMode.ForRead);

                    if (btr.HasAttributeDefinitions)
                    {
                        tv.Nodes.Add(btr.Name);
                    }
                }
            }
        }

        private void FillChildNodes()
        {
            foreach (TreeNode nd in treeViewBlocks.Nodes)
            {
                using (Transaction trx = db.TransactionManager.StartTransaction())
                {
                    BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr = (BlockTableRecord)trx.GetObject(bt[nd.Text], OpenMode.ForRead);
                    ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);
                    foreach (ObjectId id in objIds)
                    {
                        BlockReference bRef = (BlockReference)trx.GetObject(id, OpenMode.ForRead);
                        List<string> AttList = new List<string>();
                        TreeNode node = nd.Nodes.Add(String.Format("BlokRef:{0}|Id:{1}", btr.Name, id));
                        foreach (ObjectId idRef in bRef.AttributeCollection)
                        {
                            AttributeReference AttRef = (AttributeReference)trx.GetObject(idRef, OpenMode.ForRead);
                            AttList.Add(String.Format("[{0}:{1}]", AttRef.Tag, AttRef.TextString));
                            TreeNode ndChilds = node.Nodes.Add(String.Format("[{0}:{1}]", AttRef.Tag, AttRef.TextString));
                        }
                    }
                }
            }
        }

        private void FillChildToChildNodes(TreeNode TvNodeChilds)
        {

        }

        private void FillTreeView()
        {
            using (Transaction trx = db.TransactionManager.StartTransaction())
            {
                datablok = new List<string>();
                BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;
                foreach (ObjectId BtId in bt)
                {
                    BlockTableRecord BlokTblRec = (BlockTableRecord)BtId.GetObject(OpenMode.ForRead);
                    if (BlokTblRec.HasAttributeDefinitions)
                    {
                        datablok.Add(string.Format("{0}|{1}",BlokTblRec.Name, BtId));
                        listBox1.Items.Add(string.Format("{0}|{1}", BlokTblRec.Name, BtId));
                    }
                }
                            
                foreach (string item in datablok)
                {
                    string[] SplitItem = item.Split('|');
                    BlockTableRecord btr = (BlockTableRecord)trx.GetObject(bt[SplitItem[0]], OpenMode.ForRead);
                    ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);
                    AttributBlok = new List<string>();
                    koleksi = new ObjectIdCollection();
                    foreach (ObjectId id in objIds)
                    {
                        AttributBlok.Add(string.Format("{0}|{1}", btr.Name, id));
                        koleksi.Add(id);
                        listBox2.Items.Add(string.Format("{0}|{1}", btr.Name, id));                                            
                    }
 
                    foreach (ObjectId itemid in koleksi)
                    {
                        BlockReference bRef = (BlockReference)trx.GetObject(
                            itemid, OpenMode.ForRead);
                        foreach (ObjectId idRef in bRef.AttributeCollection)
                        {
                            AttributeReference AttRef = (AttributeReference)trx.GetObject(
                                idRef, OpenMode.ForRead);
                            AttributBlok.Add(String.Format("[{0}:{1}]", 
                                AttRef.Tag, 
                                AttRef.TextString));
                        }
                    }
                }
            }
        }
                
        private void treeViewBlocks_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //TreeNode currentNode = e.Node;
            //currentNode.Nodes.Clear();
            //using (Transaction trx = db.TransactionManager.StartTransaction())
            //{
            //    BlockTable bt = db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable;

            //    BlockTableRecord btr = (BlockTableRecord)trx.GetObject(bt[currentNode.Text], OpenMode.ForRead);
            //    ObjectIdCollection objIds = btr.GetBlockReferenceIds(true, false);

            //    foreach (ObjectId id in objIds)
            //    {
            //        BlockReference bRef = (BlockReference)trx.GetObject(id, OpenMode.ForRead);
            //        List<string> AttList = new List<string>();
            //        TreeNode node = currentNode.Nodes.Add(String.Format("{0}|{1}", btr.Name, id));
            //            foreach (ObjectId idRef in bRef.AttributeCollection)
            //            {
            //                AttributeReference AttRef = (AttributeReference)trx.GetObject(idRef, OpenMode.ForRead);
            //                AttList.Add(String.Format("[{0}:{1}]", AttRef.Tag, AttRef.TextString));
            //            }                    
            //    }
            //    trx.Commit();
            //}
        }

        private void treeViewBlocks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }
    }
}
