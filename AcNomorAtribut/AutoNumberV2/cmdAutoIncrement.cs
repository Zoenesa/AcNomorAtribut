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
        private Document doc;
        private Database db;
        private Editor ed;
        private Dictionary<string, AttachmentPoint> justify;
        
        public cmdAutoIncrement()
        {
            this.justify = new Dictionary<string, AttachmentPoint>();
            this.justify.Add("Left", AttachmentPoint.BaseLeft);
            this.justify.Add("Center", AttachmentPoint.BaseCenter);
            this.justify.Add("Right", AttachmentPoint.BaseRight);
            this.justify.Add("Top Left", AttachmentPoint.TopLeft);
            this.justify.Add("Top Center", AttachmentPoint.TopCenter);
            this.justify.Add("Top Right", AttachmentPoint.TopRight);
            this.justify.Add("Middle Left", AttachmentPoint.MiddleLeft);
            this.justify.Add("Middle Center", AttachmentPoint.MiddleCenter);
            this.justify.Add("Middle Right", AttachmentPoint.MiddleRight);
            this.justify.Add("Bottom Left", AttachmentPoint.BottomLeft);
            this.justify.Add("Bottom Center", AttachmentPoint.BottomCenter);
            this.justify.Add("Bottom Right", AttachmentPoint.BottomRight);
            dlg = new formIncrementV2();
        }

        [CommandMethod("AutoIncr")]
        public void frmIncrementV2()
        {
            doc = Application.DocumentManager.MdiActiveDocument;
            db = doc.Database;
            ed = doc.Editor;

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
                    string prefix = this.dlg.txtPrefix.Text;
                    string suffix = this.dlg.txtSuffix.Text;
                    switch (this.dlg.Tab)
                    {
                        case 0:
                            {
                                this.IncrementAttribute((BlockTableRecord)this.dlg.cbxBlock.SelectedItem, this.dlg.cbxAttrib.SelectedIndex, prefix, suffix);
                            }
                            break;
                        case 1:
                            {
                                this.IncrementText(this.dlg.cbxStyles.Text, this.dlg.cbxAlignment.Text, prefix, suffix);
                            }
                            break;
                        case 2:
                            {

                            }
                            break;
                        default:
                            {
                                
                            }
                            break;
                    }
                }
                tr.Commit();
            }
        }

        private string IncrementAbjad(string value)
        {
            int num;
            int typeFlag = this.dlg.TypeFlag;
            int incrValue = this.dlg.IncrValue;
            string separator = this.dlg.Separator;
            List<char> list = value.Reverse<char>().ToList<char>();
            char chr = (string.IsNullOrEmpty(separator) ? '\0' : separator.First<char>());
            for (int i = 0; i < list.Count; i++)
            {
                char item = list[i];
                if (item != chr)
                {
                    int num1 = (char.IsDigit(item) ? 10 : 26);
                    if (char.IsDigit(item))
                    {
                        num = 48;
                    }
                    else
                    {
                        num = (char.IsUpper(item) ? 65 : 97);
                    }
                    int num2 = num;
                    int num3 = item - (char)num2;
                    int num4 = (num3 + incrValue) / num1;
                    list[i] = (char)(num2 + (num3 + incrValue) % num1);
                    if (num4 == 0)
                    {
                        break;
                    }
                    incrValue = num4;
                    if (i == list.Count - 1)
                    {
                        list.Add((char)num2);
                        if (num1 != 10)
                        {
                            incrValue--;
                        }
                    }
                }
            }
            list.Reverse();
            return new string(list.ToArray());
        }

        private string Increment(string value)
        {
            string str;
            int typeFlag = this.dlg.TypeFlag;
            if (typeFlag != 8)
            {
                if (typeFlag != 16)
                {
                    return this.IncrementAbjad(value);
                }
                return ((Romawi)value + this.dlg.IncrValue).ToString();
            }
            typeFlag = Convert.ToInt32(value, 16) + this.dlg.IncrValue;
            str = (value == value.ToUpper() ? "X" : "x");
            int length = value.Length;
            return typeFlag.ToString(string.Concat(str, length.ToString()));
        }

        private void IncrementText(string styleName, string Justifi, string prefix, string suffix)
        {
            string startvalue = this.dlg.StartValue;
            int num = 0;
            Stack<ObjectId> objectIds = new Stack<ObjectId>();
            Stack<string> strs = new Stack<string>();
            ObjectId item = this.db.TextStyleTableId.GetObject<TextStyleTable>()[styleName];
            bool flag = Justifi == "Left";
            while (true)
            {
                using (DBText dbText = new DBText())
                {

                }
            }
        }

        public void IncrementAttribute(BlockTableRecord TableRecord, int index, string prefix, string suffix)
        {
            string startValue = this.dlg.StartValue;
            int num = 0;
            Stack<ObjectId> objectIds = new Stack<ObjectId>();
            Stack<string> strs = new Stack<string>();
            while (true)
            {
                using (BlockReference blockRef = new BlockReference(Point3d.Origin, TableRecord.ObjectId))
                {
                    blockRef.Rotation = this.dlg.BlockRotation;
                    blockRef.ScaleFactors = new Scale3d(this.dlg.BlockScale);
                    blockRef.TransformBy(this.ed.CurrentUserCoordinateSystem);
                    if (blockRef.Annotative == AnnotativeStates.True)
                    {
                        ObjectContextCollection contextCollection = this.db.ObjectContextManager.GetContextCollection("ACDB_ANNOTATIONSCALES");
                        Autodesk.AutoCAD.Internal.ObjectContexts.AddContext(blockRef, contextCollection.CurrentContext);
                    }
                    ObjectId objectId = this.db.GetCurrentSpace(OpenMode.ForWrite).Add(blockRef);
                    blockRef.AddAttributeReferences(index, string.Concat(prefix, startValue, suffix));
                    BlockJig blockJig = new BlockJig(blockRef, num, TableRecord.GetAttributesTextInfos());
                    PromptResult promptResult = this.ed.Drag(blockJig);
                    if (promptResult.Status == PromptStatus.Keyword)
                    {
                        blockRef.Erase();
                        if (num != 0)
                        {
                            objectIds.Pop().GetObject<BlockReference>(OpenMode.ForWrite).Erase();
                            this.db.TransactionManager.QueueForGraphicsFlush();
                            startValue = strs.Pop();
                            num--;
                        }
                        else
                        {
                            this.ed.WriteMessage("\nNothing to undo !");
                        }
                    }
                    else if (promptResult.Status == PromptStatus.OK)
                    {
                        this.db.TransactionManager.QueueForGraphicsFlush();
                        strs.Push(startValue);
                        objectIds.Push(objectId);
                        startValue = this.Increment(startValue);
                        num++;
                    }
                    else
                    {
                        blockRef.Erase();
                        break;
                    }
                }
            }
            this.dlg.txtValue.Text = startValue;
        }

        private bool IsValidEntity(ObjectId id)
        {
            RXClass @class = RXObject.GetClass(typeof(DBText));
            RXClass rXClass = RXObject.GetClass(typeof(MText));
            int entityTypeFlag = this.dlg.EntityTypeFlag;
            if (entityTypeFlag == 1)
            {
                return id.ObjectClass.IsDerivedFrom(@class);
            }
            if (entityTypeFlag == 2)
            {
                return id.ObjectClass == rXClass;
            }
            if (id.ObjectClass == rXClass)
            {
                return true;
            }
            return id.ObjectClass.IsDerivedFrom(@class);
        }

        private void OnSelectionAdded(object sender, SelectionAddedEventArgs e)
        {
            string name = ((BlockTableRecord)this.dlg.cbxAutoBlock.SelectedItem).Name;
            SelectionSet addedObjects = e.AddedObjects;
            for (int i = 0; i < e.AddedObjects.Count; i++)
            {
                if (addedObjects[i].ObjectId.GetObject<BlockReference>().GetEffectiveName() != name)
                {
                    e.Remove(i);
                }
            }
        }


    }
}
