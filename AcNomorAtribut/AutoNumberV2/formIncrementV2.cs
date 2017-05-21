using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AcBlockAtributeIncrement
{
    public partial class formIncrementV2 : Form
    {

        private Database _db;

        private Dictionary<int, CheckBox> _chkBoxes;

        private Dictionary<int, CheckBox> _chkEntTypes;

        private bool _isFrench;

        private double _textHeight;

        private double _blockScale;

        private double _blockRotation;

        private double _textRotation;

        private float _ScaleX, _ScaleY;

        internal string AutoEntTypeFlag
        {
            get;
            set;
        }

        public double BlockRotation
        {
            get { return this._blockRotation; }
            set { this._blockRotation = value;
                this.txtBlkRot.Text = this._blockRotation.ToString(); }
        }

        public double TextRotation
        {
            get
            {
                return this._textRotation;
            }
            set
            {
                this._textRotation = value;
                this.txtRot.Text = Converter.AngleToString(this._textRotation);
            }
        }

        public double BlockScale
        {
            get { return this._blockScale; }
            set { this._blockScale = value;
                this.txtBlkScale.Text = this._blockScale.ToString();}
        }

        internal int EntityTypeFlag
        {
            get; private set;
        }

        internal int IncrValue
        {
            get; set;
        }

        internal string Separator
        {
            get; set;
        }

        internal string StartValue
        {
            get; set;
        }

        internal int StringPositionFlag
        {
            get; set;
        }

        internal int Tab { get; private set; }

        internal int TypeFlag
        {
            get;
            private set;
        }

        public double TextHeight
        {
            get { return this._textHeight; }
            set
            {
                this._textHeight = value;
                this.txtTextHeight.Text = Converter.DistanceToString(this._textHeight);
            }

        }

        private string[] GetStylesNames()
        {
            return (
                from ts in this._db.TextStyleTableId.GetObject<TextStyleTable>().GetObjects<TextStyleTableRecord>()
                select ts.Name into n
                where n.Trim() != ""
                orderby n
                select n).ToArray<string>();
        }

        public formIncrementV2()
        {
            InitializeComponent();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            Graphics graphic = Graphics.FromHwnd(IntPtr.Zero);
            this._ScaleX = graphic.DpiX / 96f;
            this._ScaleY = graphic.DpiY / 96f;
            cbxAttrib.DisplayMember = "Tag";
            cbxSelBlk.DisplayMember = "Name";
            cbxSelTag.DisplayMember = "Tag";
            cbxBlock.DisplayMember = "Name";
            this._db = HostApplicationServices.WorkingDatabase;
            this.TextHeight = this._db.Textsize;
            this._chkBoxes = new Dictionary<int, CheckBox>()
            {
                { 1, chkAngka },
                { 2, chkKecil },
                { 3, chkKapital },
                { 4, chkRomawi }
            };

            this._chkEntTypes = new Dictionary<int, CheckBox>()
            {
                { 1, this.chkSelText }, { 2, this.chkMtext }, { 4, this.chkSelBlock }
            };

            using (Transaction tr = this._db.TransactionManager.StartTransaction())
            {
                BlockTableRecord[] blockwithAttribute = this._db.GetBlocksWithAttribute();
                this.cbxBlock.DataSource = blockwithAttribute;
                this.BlockScale = 1;
                this.BlockRotation = 0;
                this.BlockRotation = (objRot.Checked ? 1 : this.BlockRotation);
                this.cbxSelBlk.DataSource = blockwithAttribute;
                if (blockwithAttribute != null && blockwithAttribute.Length != 0)
                {
                    this.cbxSelBlk.SelectedIndex = 0;
                }

                cbxStyles.Items.AddRange(this.GetStylesNames());
                cbxStyles.SelectedItem = this._db.Textstyle.GetObject<TextStyleTableRecord>().Name;
                cbxAlignment.SelectedIndex = 0;
                this.StringPositionFlag = 2;
                tr.Commit();
            }
        }

        private void cbxBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxAttrib.DataSource = (
                from att in ((BlockTableRecord)this.cbxBlock.SelectedItem).GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToArray<AttributeDefinition>();
        }

        private void chkAngka_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAngka.Checked)
            {
                chkRomawi.Checked = false;
                chkHex.Checked = false;
                txtSeparator.Enabled = true;
            }
        }

        private void chkKecil_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKecil.Checked)
            {
                chkHex.Checked = false;
                chkRomawi.Checked = false;
                txtSeparator.Enabled = true;
            }
        }

        private void chkKapital_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKapital.Checked)
            {
                chkHex.Checked = false;
                chkRomawi.Checked = false;
                txtSeparator.Enabled = true;
            }
        }

        private void chkRomawi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRomawi.Checked)
            {
                chkAngka.Checked = false;
                chkKecil.Checked = false;
                chkKapital.Checked = false;
                txtSeparator.Enabled = false;
            }
        }

        private void chkHex_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHex.Checked)
            {
                chkAngka.Checked = false;
                chkKapital.Checked = false;
                chkKecil.Checked = false;
                chkRomawi.Checked = false;
                txtSeparator.Enabled = false;
            }
        }

        private void cbxBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.cbxBlock_Validating(sender, new CancelEventArgs());
            }
        }

        private void cbxSelBlk_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxSelTag.DataSource = (
                from att in ((BlockTableRecord)this.cbxSelBlk.SelectedItem).GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToArray<AttributeDefinition>();
        }

        private void cbxBlock_Validating(object sender, CancelEventArgs e)
        {
            string text = this.cbxBlock.Text;
            if (text == "")
            {
                return;
            }
            BlockTable obj = this._db.BlockTableId.GetObject<BlockTable>();
            if (obj.Has(text))
            {
                BlockTableRecord blockTableRecords = obj[text].GetObject<BlockTableRecord>();
                AttributeDefinition[] arrayAttDef = (
                    from att in blockTableRecords.GetObjects<AttributeDefinition>()
                    where !att.Constant
                    select att).ToArray<AttributeDefinition>();
                if (arrayAttDef != null && arrayAttDef.Length != 0)
                {
                    this.cbxBlock.SelectedItem = blockTableRecords;
                    return;
                }
                AcAp.ShowAlertDialog(("Block terpilih tidak terdapat Attribute Reference."));
                this.cbxBlock.SelectAll();
                return;
            }
            ObjectId block = obj.GetBlock(text);
            if (block == ObjectId.Null)
            {
                AcAp.ShowAlertDialog("Block '" + text + "'Tidak ada.");
                this.cbxBlock.SelectAll();
                return;
            }
            BlockTableRecord obj1 = block.GetObject<BlockTableRecord>();
            AttributeDefinition[] arrayDef = (
                from att in obj1.GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToArray<AttributeDefinition>();
            if (arrayDef == null || arrayDef.Length == 0)
            {
                AcAp.ShowAlertDialog("Tidak Ada Attribute pada Block yang dipilih.");
                this.cbxBlock.SelectAll();
                return;
            }
            if (!this.cbxBlock.Items.Contains(obj1))
            {
                this.cbxBlock.DataSource = this._db.GetBlocksWithAttribute();
            }
            this.cbxBlock.SelectedItem = obj1;
        }

        private bool IsAlpabetik(string str, string val)
        {
            if (str == null || str.Trim() == "")
            {
                return false;
            }
            if (string.IsNullOrEmpty(val))
            {
                return !str.Any<char>((char c) => !char.IsLetter(c));
            }
            char chr = val.First<char>();
            string[] strArrays = str.Split(new char[] { chr });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                if (strArrays[i].Any<char>((char c) => !char.IsLetter(c)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsAlpaNumerik(string str, string val)
        {
            if (str == null || str.Trim() == "")
            {
                return false;
            }
            if (string.IsNullOrEmpty(val))
            {
                return !str.Any<char>((char c) => !char.IsLetterOrDigit(c));
            }
            char chr = val.First<char>();
            string[] strArrays = str.Split(new char[] { chr });
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                if (strArrays[i].Any<char>((char c) => !char.IsLetterOrDigit(c)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsLower(string str)
        {
            return str == str.ToLower();
        }

        private bool IsUpper(string str)
        {
            return str == str.ToUpper();
        }

        private bool IsNumeric(string str, string val)
        {
            if (str == null || str.Trim() == "")
            {
                return false;
            }
            if (string.IsNullOrEmpty(val))
            {
                return !str.Any<char>((char c) => !char.IsDigit(c));
            }
            char chr = val.First<char>();
            string[] strArr = str.Split(new char[] { chr });
            for (int i = 0; i < (int)strArr.Length; i++)
            {
                if (strArr[i].Any<char>((char c) => !char.IsDigit(c)))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".dwg";
            ofd.Title = "Pilih File Block";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ObjectId block = this._db.BlockTableId.GetObject<BlockTable>().GetBlock(ofd.FileName);
            if (block == ObjectId.Null)
            {
                AcAp.ShowAlertDialog("Invalid File");
                return;
            }
            BlockTableRecord obj = block.GetObject<BlockTableRecord>();
            AttributeDefinition[] array = (
                from att in obj.GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToArray<AttributeDefinition>();
            if (array == null || array.Length == 0)
            {
                AcAp.ShowAlertDialog("");
                return;
            }
            if (!this.cbxBlock.Items.Contains(obj))
            {
                this.cbxBlock.DataSource = this._db.GetBlocksWithAttribute();
            }
            this.cbxBlock.SelectedItem = obj;
        }

        private void btnSelBlk_Click(object sender, EventArgs e)
        {
            Editor editor = AcAp.DocumentManager.MdiActiveDocument.Editor;
            PromptEntityOptions pEnt = new PromptEntityOptions("\nSelect Block: ");
            pEnt.AllowNone = true;
            pEnt.SetRejectMessage("Selected Object is not a Block!.");
            pEnt.AddAllowedClass(typeof(BlockReference), true);
            PromptEntityResult pEntRes = editor.GetEntity(pEnt);
            if (pEntRes.Status != PromptStatus.OK)
            {
                return;
            }
            BlockReference bRef = pEntRes.ObjectId.GetObject<BlockReference>();
            Dictionary<string, string> dicTag = new Dictionary<string, string>();
            for (int i = 0; i < bRef.AttributeCollection.Count; i++)
            {
                string strTag = bRef.AttributeCollection[i].GetObject<AttributeReference>(OpenMode.ForRead).Tag;
                string strTextString = bRef.AttributeCollection[i].GetObject<AttributeReference>(OpenMode.ForRead).TextString;
                dicTag.Add(strTag, strTextString);
                switch (i)
                {
                    case 0:
                        {
                            lblTag1.Text = strTag;
                            textString1.Text = strTextString;
                        }
                        break;
                    case 1:
                        {
                            lblTag2.Text = strTag;
                            textString2.Text = strTextString;
                        }
                        break;
                    case 2:
                        {
                            lblTag3.Text = strTag;
                            textString3.Text = strTextString;
                        }
                        break;
                    case 3:
                        {
                            lblTag4.Text = strTag;
                            textString4.Text = strTextString;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (objRot.Checked)
            {
                this.BlockRotation = bRef.Rotation;
                txtBlkRot.Text = Converter.AngleToString(bRef.Rotation);
            }
            else 
            {
                this.BlockRotation = Converter.StringToAngle(txtRot.Text);
            }
            txtRot.Text = Converter.AngleToString(bRef.Rotation);
            Autodesk.AutoCAD.DatabaseServices.AttributeCollection attRefColl = bRef.AttributeCollection;
            if (attRefColl == null || attRefColl.Count == 0)
            {
                AcAp.ShowAlertDialog("Selected Block do not have Attributes.");
                return;
            }
            this.cbxBlock.SelectedItem = (bRef.IsDynamicBlock ? bRef.DynamicBlockTableRecord.GetObject<BlockTableRecord>() : bRef.BlockTableRecord.GetObject<BlockTableRecord>());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int num;
            string str;
            this.TypeFlag = (
                from kvp in this._chkBoxes
                where kvp.Value.Checked
                select kvp.Key).Sum();
            this.EntityTypeFlag = (
                from kvp in this._chkEntTypes
                where kvp.Value.Checked
                select kvp.Key).Sum();
            this.StartValue = this.txtValue.Text;
            this.Separator = this.txtSeparator.Text;
            if (this.TypeFlag == 0)
            {
                AcAp.ShowAlertDialog("Tidak ada Block.");
                return;
            }
            if (!this.IsValidString(this.StartValue ,  this.Separator))
            {
                AcAp.ShowAlertDialog("Invalid Nilai");
                this.txtValue.Select();
                return;
            }
            if (this.TypeFlag == 16 && int.TryParse(this.StartValue, out num))
            {
                this.StartValue = ((Romawi)num).ToString();
            }
            base.DialogResult = DialogResult.OK;
            //base.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void chkSelText_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.chkSelBlock.Checked = false;
            }
        }

        private void chkSelBlock_CheckedChanged(object sender, EventArgs e)
        {
            bool flag;
            if (!chkSelBlock.Checked)
            {
                ComboBox combobox = this.cbxSelBlk;
                int num = 0;
                flag = Convert.ToBoolean(num);
                this.cbxSelTag.Enabled = Convert.ToBoolean(num);
                combobox.Enabled = flag;
                return;
            }
            if (this.cbxSelBlk.Items == null || this.cbxSelBlk.Items.Count <= 0)
            {
                AcAp.ShowAlertDialog("None block with attributes in this drawing.");
                this.chkSelBlock.Checked = false;
                return;
            }
            ComboBox combobox1 = this.cbxSelBlk;
            int num1 = 1;
            flag = Convert.ToBoolean(num1);
            this.cbxSelTag.Enabled = Convert.ToBoolean(num1);
            CheckBox checkbox = this.chkMtext;
            int num2 = 0;
            flag = Convert.ToBoolean(num2);
            this.chkSelText.Checked = Convert.ToBoolean(num2);
            checkbox.Checked = flag;

        }

        private void chkMtext_CheckedChanged(object sender, EventArgs e)
        {

        }

        private double StringToScale(string s)
        {
            double num;
            double num1;
            double num2;
            if (double.TryParse(s, out num))
            {
                if (num <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return num;
            }
            string[] strArrays = s.Split(new char[] { '/', ':' });
            if ((int)strArrays.Length != 2 || !double.TryParse(strArrays[0], out num2) || !double.TryParse(strArrays[1], out num1))
            {
                throw new ArgumentException();
            }
            return num2 / num1;
        }

        private void cbxStyles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool IsValidString(string str, string val)
        {
            int num;
            Romawi romawi;
            switch (this.TypeFlag)
            {
                case 1:
                    {
                        return this.IsNumeric(str, val);
                    }
                case 2:
                    {
                        if (!this.IsAlpabetik(str, val))
                        {
                            return false;
                        }
                        return this.IsUpper(str);
                    }
                case 3:
                    {
                        if (!this.IsAlpaNumerik(str, val))
                        {
                            return false;
                        }
                        return this.IsUpper(str);
                    }
                case 4:
                    if (!this.IsAlpabetik(str, val))
                    {
                        return false;
                    }
                    return this.IsLower(str);
                case 5:
                    if (!this.IsAlpaNumerik(str, val))
                    {
                        return false;
                    }
                    return this.IsLower(str);
                case 6:
                    {
                        return IsAlpabetik(str, val);
                    }
                case 7:
                    {
                        return IsAlpaNumerik(str, val);
                    }
                case 8:
                    {
                        return int.TryParse(str, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out num);
                    }
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    {
                        return false;
                    }
                case 16:
                    {
                        return Romawi.TryParse(str, out romawi);
                    }
                default:
                    return false;
            }
        }
       
    }
}
