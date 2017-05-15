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

namespace AcBlockAtributeIncrement
{
    public class cmdAutoIncrement
    {
        private formIncrementV2 dlg;

        public cmdAutoIncrement()
        {
            dlg = new formIncrementV2();
        }

        [CommandMethod("AutoIncr")]
        public void frmIncrementV2()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTableRecord[] blockWithAttribute = db.GetBlocksWithAttribute();
                BlockTableRecord selectedItem = (BlockTableRecord)this.dlg.cbxBlock.SelectedItem;
                int selectedindex = this.dlg.cbxAttrib.SelectedIndex;
                this.dlg.cbxBlock.DataSource = blockWithAttribute;
                if (!blockWithAttribute.Contains<BlockTableRecord>(selectedItem))
                {
                    this.dlg.cbxBlock.Text = "";
                    this.dlg.cbxAttrib.Items.Clear();
                }
                else
                {
                    this.dlg.cbxBlock.SelectedItem = selectedItem;
                    this.dlg.cbxAttrib.SelectedIndex = selectedindex;
                }
                selectedItem = (BlockTableRecord)this.dlg.cbxSelBlk.SelectedItem;
                selectedindex = this.dlg.cbxSelTag.SelectedIndex;
                this.dlg.cbxSelBlk.DataSource = blockWithAttribute;
                if (blockWithAttribute == null || blockWithAttribute.Length == 0)
                {
                    this.dlg.cbxSelTag.Items.Clear();
                }
                else if (!blockWithAttribute.Contains<BlockTableRecord>(selectedItem))
                {
                    this.dlg.cbxSelBlk.SelectedIndex = 0;
                }
                else
                {
                    this.dlg.cbxSelBlk.SelectedItem = selectedItem;
                    this.dlg.cbxAttrib.SelectedIndex = selectedindex;
                }
                if (AcAp.ShowModalDialog(this.dlg) == System.Windows.Forms.DialogResult.OK)
                {

                }
            }
        }
    }
}
