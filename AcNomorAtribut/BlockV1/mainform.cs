using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcNomorAtribut
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
            this.radioDrawingExisting.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            this.radioEksternalDrawing.CheckedChanged += new EventHandler(this.radio_CheckedChanged);
            this.radioPilihObjek.CheckedChanged += new EventHandler(this.radio_CheckedChanged);
            this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
            this.tabControl1.Selecting += new TabControlCancelEventHandler(this.tabcontrol_Selecting);
            this.buttonLanjut.Click += new EventHandler(this.Lanjut_Click);
            this.buttonKembali.Click += new EventHandler(this.Kembali_Click);
            this.buttonTambah.Click += new EventHandler(this.browsedrawing_Click);
            this.buttonHapus.Click += new EventHandler(this.HapusFile_Click);
            this.dgListDrawings.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dwgList_Remove);
            //this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabcontrol_drawItem);
            numexpor = 0;
            dgListDrawings.Enabled = false;
            buttonPick.Enabled = false;
            buttonTambah.Enabled = false;
            buttonHapus.Enabled = false;
            colblock = 0; labelObjCount.Text = colblock.ToString();
            tabControl1.Enabled = true;
        }

        #region Konstruktur

        private string[] headerName = { "Attribute Kolektor", "-","Data Source"};
        private List<FileInfo> listfile = null;
        public static List<string> atributName;
        private KoleksiBlok classKoleksiBlok;
        private int colblock;
        private int numexpor;

        #endregion

        /// <summary>
        /// Property For Header Selected TabPage
        /// </summary>
        private string[] HeaderName
        { get
            { return headerName; }
        set
            { this.Text = string.Concat(value); }
        }

        /// <summary>
        /// Count Block Reference Picks From Current Drawing
        /// </summary>
        private int ObjCollCount
        {
            get
            { return colblock; }
        }
        
        /// <summary>
        /// metode
        /// </summary>
        private int methodselected
        {
            get { return numexpor; }
        }

        private void mainform_Load(object sender, EventArgs e)
        { 
            ((Control)this.tabPage1).Enabled = true;
            ((Control)this.tabPage2).Enabled = false;
            ((Control)this.tabPage3).Enabled = false;
            ((Control)this.tabPage4).Enabled = false;
        }
        
        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn != null && btn.Checked)
            {
                switch (btn.Name)
                {
                    case "radioDrawingExisting":
                        dgListDrawings.Enabled = false;
                        buttonPick.Enabled = false;
                        buttonTambah.Enabled = false;
                        buttonHapus.Enabled = false;
                        numexpor = 0;
                        break;
                    case "radioPilihObjek":
                        dgListDrawings.Enabled = false;
                        buttonPick.Enabled = true;
                        buttonTambah.Enabled = false;
                        buttonHapus.Enabled = false;
                        numexpor = 1;
                        break;
                    case "radioEksternalDrawing":
                        dgListDrawings.Enabled = true;
                        buttonPick.Enabled = false;
                        buttonTambah.Enabled = true;
                        buttonHapus.Enabled = true;
                        numexpor = 2;
                        break;
                    default:
                        dgListDrawings.Enabled = false;
                        break;
                }
            }
        }

        #region tabControl & datagrid files

        private void tabcontrol_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !((Control)e.TabPage).Enabled;
        }
         
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    buttonKembali.Enabled = false;
                    buttonLanjut.Enabled = true;
                    headerName[2] = "[" + tabPage1.Text + "]";
                    break;
                case 1:
                    buttonKembali.Enabled = true;
                    buttonLanjut.Enabled = true;
                    headerName[2] = "[" + tabPage2.Text + "]";
                    break;
                case 2:
                    buttonKembali.Enabled = true;
                    buttonLanjut.Enabled = true;
                    headerName[2] = "[" + tabPage3.Text + "]";
                    break;
                case 3:
                    buttonKembali.Enabled = true;
                    buttonLanjut.Enabled = false;
                    headerName[2] = "[" + tabPage4.Text + "]";
                    break;
                default:
                    break;
            }
            this.Text = string.Concat(HeaderName);
        }

        private void tabcontrol_drawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tbC = sender as TabControl;
            TabPage tabpage = tbC.TabPages[e.Index];
            if (tabpage.Enabled == false)
            {
                using (SolidBrush brush = new SolidBrush(SystemColors.GrayText))
                {
                    e.Graphics.DrawString(tabpage.Text, tabpage.Font, brush,
                        e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(tabpage.ForeColor))
                {
                    e.Graphics.DrawString(tabpage.Text, tabpage.Font, brush,
                        e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
        }
        
        private void browsedrawing_Click(object sender, EventArgs e)
        {
            Autodesk.AutoCAD.ApplicationServices.Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.HostApplicationServices hs = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.Current;
            string path = hs.FindFile(doc.Name, doc.Database, Autodesk.AutoCAD.DatabaseServices.FindFileHint.Default);
            string path2 = doc.Database.Filename;
            string dirpath = Path.GetDirectoryName(path2); 
            try
            {
                OpenFileDialog ofd = new OpenFileDialog() {
                    Filter = "AutoCad Drawing|*.dwg",
                    Title = "Pilih File Drawing",
                    Multiselect = true,
                    RestoreDirectory = false,
                    SupportMultiDottedExtensions = true,
                    InitialDirectory = dirpath};
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    listfile = new List<FileInfo>();
                    foreach (string filedwg in ofd.FileNames)
                    {
                        listfile.Add(new FileInfo(filedwg.ToString()));
                    }
                }
                else
                {
                    return;
                }
                if (listfile.Count > 0)
                {
                    buttonHapus.Enabled = !false;
                    for (int i = 0; i <= (listfile.Count - 1); i++)
                    {
                        dgListDrawings.Rows.Add(listfile[i].Name, listfile[i].DirectoryName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.Message);
            }
        }

        private void HapusFile_Click(object sender, EventArgs e)
        {
            if (dgListDrawings.Rows.Count >= 0)
            {
                dgListDrawings.Rows.RemoveAt(dgListDrawings.CurrentRow.Index);
            }
        }

        private void dwgList_Remove(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgListDrawings.Rows.Count > 0)
            {
                buttonHapus.Enabled = true;
            }
            else
            {
                buttonHapus.Enabled = false;
            }
        }

        private void dwgList_RowAdd(object sender, DataGridViewRowsAddedEventArgs e)
        {
            buttonHapus.Enabled = true;
            if (dgListDrawings.Rows.Count > 0)
            {
                buttonHapus.Enabled = true;
            }
            else
            {
                buttonHapus.Enabled = false;
            }
        }

        #endregion
 
        private void Kembali_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4)
            {
                tabPage4.Enabled = false;
                tabPage3.Enabled = true;
                tabControl1.SelectedTab = tabPage3;
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                tabPage3.Enabled = false;
                tabPage2.Enabled = true;
                tabControl1.SelectedTab = tabPage2;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                tabPage2.Enabled = false;
                tabPage1.Enabled = true;
                tabControl1.SelectedTab = tabPage1;
            }
        }

        private void Lanjut_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                tabPage1.Enabled = false;
                tabPage2.Enabled = true;
                tabControl1.SelectedTab = tabPage2;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                tabPage2.Enabled = false;
                tabPage3.Enabled = true;
                tabControl1.SelectedTab = tabPage3;
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                tabPage3.Enabled = false;
                tabPage4.Enabled = true;
                tabControl1.SelectedTab = tabPage4;
            }

            dgListBlocks.Rows.Clear();
            List<string> newList = new List<string>();
            
            switch (methodselected)
            {
                case 0:
                    classKoleksiBlok = new KoleksiBlok(KoleksiBlok.PilihanDrawing.CurrentDrawing);
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> btIdm1 = classKoleksiBlok.GetBlokTableIdFromDrawing(KoleksiBlok.PilihanDrawing.CurrentDrawing);
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> bRefm1 = classKoleksiBlok.GetBlockReferenceFromBtIds(KoleksiBlok.PilihanDrawing.CurrentDrawing);
                    break;
                case 1:
                    classKoleksiBlok = new KoleksiBlok(KoleksiBlok.PilihanDrawing.PickFromDrawing);

                    break;
                case 2:
                    int numf = dgListDrawings.Rows.Count;
                    string[] files = new string[numf];
                    foreach (DataGridViewRow drow in dgListDrawings.Rows)
                    {
                        int num1 = drow.Index;
                        files[num1] = Path.Combine(Path.Combine(drow.Cells[1].Value.ToString(), drow.Cells[0].Value.ToString()));
                    }
                    classKoleksiBlok = new KoleksiBlok(KoleksiBlok.PilihanDrawing.ExternalDrawing, files);
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> btIdmode2 = classKoleksiBlok.GetBlokTableIdFromDrawing(KoleksiBlok.PilihanDrawing.ExternalDrawing);
                    Dictionary<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> btRef = classKoleksiBlok.GetBlockReferenceFromBtIds(KoleksiBlok.PilihanDrawing.ExternalDrawing);
                    break;
                default:
                    break;
            }
            classKoleksiBlok.GetBlokByDrawing((KoleksiBlok.PilihanDrawing)methodselected);
            string[] arrListBlok = new string[2];
            foreach (KeyValuePair<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> kvp in classKoleksiBlok.KoleksiBlokTableId)
            {
                arrListBlok[0] = Convert.ToString(true);
                arrListBlok[1] = Convert.ToString(kvp.Key);
                dgListBlocks.Rows.Add(arrListBlok);
            }

        }
        
        private void buttonSettings_Click(object sender, EventArgs e)
        {

        }

        private void ShowListAttributes_Click(object sender, EventArgs e)
        {
            int introw = 0;
            atributName = new List<string>();
            foreach (DataGridViewRow drow in dgListBlocks.Rows)
            {
                introw = drow.Index;
                bool flagValue = Convert.ToBoolean(drow.Cells[0].Value);
                if (flagValue == true)
                {
                    foreach (KeyValuePair<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> item in classKoleksiBlok.KoleksiAtributBlokRefs)
                    {
                        atributName.Add(item.Key.ToString());
                    }
                    introw++;
                }
            }
            formListAttribut formlistAtr = new formListAttribut();
            formlistAtr.ShowDialog(this);
            formlistAtr.Dispose();
        }
 
        private void buttonTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgListBlocks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgListBlocks.MultiSelect = false; 
            DataGridViewCheckBoxCell chkcell = (DataGridViewCheckBoxCell)dgListBlocks.CurrentRow.Cells[0];
            dgListBlocks.BeginEdit(true);
            bool flag = Convert.ToBoolean(chkcell.Value);
            dgListBlocks.CurrentRow.Cells[0].Value = !flag; 
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rw in dgListBlocks.Rows)
            {
                rw.Cells[0].Value = true;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rw in dgListBlocks.Rows)
            {
                rw.Cells[0].Value = false;
            }
        }
    }
}
