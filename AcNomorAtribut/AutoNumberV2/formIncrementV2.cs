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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcBlockAtributeIncrement
{
    public partial class formIncrementV2 : Form
    {
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
                { 1, this.chkAngka}, { 2, chkKecil}, { 3, chkKapital}, { 4, chkRomawi }
            };
            using (Transaction tr = this._db.TransactionManager.StartTransaction())
            {
                BlockTableRecord[] blockwithAttribute = this._db.GetBlocksWithAttribute();
                this.cbxBlock.DataSource = blockwithAttribute;
                this.BlockScale = 1;
                this.BlockRotation = 0;
                this.cbxSelBlk.DataSource = blockwithAttribute;
                if (blockwithAttribute != null && blockwithAttribute.Length != 0)
                {
                    this.cbxSelBlk.SelectedIndex = 0;
                }

                cbxStyles.Items.AddRange(this.GetStylesNames());
                cbxStyles.SelectedItem = this._db.Textstyle.GetObject<TextStyleTableRecord>().Name;
                this.StringPositionFlag = 2;
                tr.Commit();
            }
        }

        private Database _db;

        private Dictionary<int, CheckBox> _chkBoxes;

        private Dictionary<int, CheckBox> _chkEntTypes;

        private bool _isFrench;

        private double _textHeight;

        private double _blockScale;

        private double _blockRotation;

        private double _textRotation;

        private float _ScaleX, _ScaleY;

        internal string AutoEntTypeFlag { get; set; }

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

        private void cbxBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxAttrib.DataSource = (
                from att in (
                (BlockTableRecord)this.cbxBlock
                .SelectedItem).GetObjects<AttributeDefinition>()
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
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(("Block terpilih tidak terdapat Attribute Reference."));
                this.cbxBlock.SelectAll();
                return;
            }
            ObjectId block = obj.GetBlock(text);
            if (block == ObjectId.Null)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog("Block '" + text + "'Tidak ada.");
                this.cbxBlock.SelectAll();
                return;
            }
            BlockTableRecord obj1 = block.GetObject<BlockTableRecord>();
            AttributeDefinition[] arrayDef = (
                from att in obj1.GetObjects<AttributeDefinition>()
                where !att.Constant
                select att).ToArray<AttributeDefinition>();
        }

        public double TextHeight
        {
            get { return this._textHeight; }
            set { this._textHeight = value;
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
       
    }
}
