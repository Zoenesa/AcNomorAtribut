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
        }

        private Database _db;

        private Dictionary<int, CheckBox> _chkBoxes;

        private Dictionary<int, CheckBox> _chkEntTypes;

        private bool _isFrench;

        private double _textHeight;

        private double _blockScale;

        private double _blockRotation;

        private float _ScaleX, _ScaleY;

        internal string AutoEntTypeFlag { get; set; }

        public double BlockRotation
        {
            get { return this._blockRotation; }
            set { this._blockRotation = value;
                this.txtBlkRot.Text = this._blockRotation.ToString(); }
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

        internal int Tab { get; set; }

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
       
    }
}
