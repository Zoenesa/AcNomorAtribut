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
using System.Windows.Forms;

namespace AcNomorAtribut
{
    public partial class formIncrementBlock : Form
    {
        public formIncrementBlock()
        {
            InitializeComponent();
            btnInsert.Click += new EventHandler(btnInsert_Click);
        }
         
        private int _increment;
        private int increment
        {
            get { return _increment; }
            set { _increment = value; }
        }
 
        public PromptPointResult PromptForPoint(string promptMessage, bool useDashedLine = false, bool useBasePoint = false, Point3d basePoint = new Point3d(), bool allowNone = true)
        {
            var pointOptions = new PromptPointOptions(promptMessage);
            if (useBasePoint)
            {
                pointOptions.UseBasePoint = true;
                pointOptions.BasePoint = _editor.GetPoint("Insert Base Point: ").Value;
                pointOptions.AllowNone = allowNone;
            }

            if (useDashedLine)
            {
                pointOptions.UseDashedLine = true;
            }
            var pointResult = _editor.GetPoint(pointOptions);
            return pointResult;
        }

        Document doc;
        Database db;
        Editor _editor;

        private void formIncrementBlock_Load(object sender, EventArgs e)
        {
            doc = AcAp.DocumentManager.MdiActiveDocument;
            db = doc.Database;
            _editor = doc.Editor;
            lanjut = true;
            //edUtils = new EditorUtilities.classEditorHelper(doc);
        }

        int numinc = 0;
        bool lanjut;
        private void InsertCommand()
        {
            
            PromptKeywordOptions opS = new PromptKeywordOptions("");
            opS.Message = ("Insert Blok");
            opS.Keywords.Add("Insert", "I", "Insert", true, true);
            opS.Keywords.Add("Replace", "p", "Replace", true, true);
            opS.Keywords.Add("BasePoint", "B", "Base Point", true, true);
            
            PromptResult pRes = _editor.GetKeywords(opS);
            if (pRes.Status == PromptStatus.Cancel)
            {
                lanjut = false;
                return;
            }
            else
            {
                Point3d ptBase = new Point3d();
                if (pRes.StringResult == "BasePoint")
                {
                    ptBase = _editor.GetPoint("Define Base Point").Value;
                }
                PromptPointResult ppRes = PromptForPoint("Pick Internal Point:", true, true, ptBase);
                if (ppRes.Status == PromptStatus.OK)
                {
                    string pointValue = ppRes.Value + numinc.ToString();
                    numinc = numinc + _increment;
                    AcAp.ShowAlertDialog(pointValue);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            _increment = Convert.ToInt32(txtIncrement.Text);

            while (lanjut)
            {
                InsertCommand();
            }
        }


    }
}
