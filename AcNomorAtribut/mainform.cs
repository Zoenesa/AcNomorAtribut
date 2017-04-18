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
            this.dgDrawings.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dwgList_Remove);
            //this.tabControl1.DrawItem += new DrawItemEventHandler(this.tabcontrol_drawItem);
            numexpor = 0;
            dgDrawings.Enabled = false;
            buttonPick.Enabled = false;
            buttonTambah.Enabled = false;
            buttonHapus.Enabled = false;
            colblock = 0; labelObjCount.Text = colblock.ToString();
            tabControl1.Enabled = true;
        }

        private string[] headerName = { "Attribute Kolektor", "-","Data Source"};

        private string[] HeaderName
        { get
            { return headerName; }
        set
            { this.Text = string.Concat(value); }
        }
        
        private bool flagtabeldrawing
        {
            get
            {
                return tabCancel;
            }
        }

        private int colblock;

        private int ObjCollCount
        {
            get
            { return colblock; }
        }

        private List<FileInfo> listfile = null;
        private void mainform_Load(object sender, EventArgs e)
        {
            tabCancel = true;
            ((Control)this.tabPage1).Enabled = true;
            ((Control)this.tabPage2).Enabled = false;
            ((Control)this.tabPage3).Enabled = false;
            ((Control)this.tabPage4).Enabled = false;
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
                        dgDrawings.Rows.Add(listfile[i].Name, listfile[i].DirectoryName);
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
            if (dgDrawings.Rows.Count >= 0)
            {
                dgDrawings.Rows.RemoveAt(dgDrawings.CurrentRow.Index);
            }
        }

        private int numexpor;
        private int methodselected
        {
            get { return numexpor; }
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn != null && btn.Checked)
            {
                switch (btn.Name)
                {
                    case "radioDrawingExisting":
                        dgDrawings.Enabled = false;
                        buttonPick.Enabled = false;
                        buttonTambah.Enabled = false;
                        buttonHapus.Enabled = false;
                        numexpor = 0;
                        break;
                    case "radioPilihObjek":
                        dgDrawings.Enabled = false;
                        buttonPick.Enabled = true;
                        buttonTambah.Enabled = false;
                        buttonHapus.Enabled = false;
                        numexpor = 1;
                        break;
                    case "radioEksternalDrawing":
                        dgDrawings.Enabled = true;
                        buttonPick.Enabled = false;
                        buttonTambah.Enabled = true;
                        buttonHapus.Enabled = true;
                        numexpor = 2;
                        break;
                    default:
                        dgDrawings.Enabled = false;
                        break;
                }
            }
        }

        bool tabCancel = true;
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

        private static void EnabledTab(TabPage page, bool enable)
        {
            EnableControls(page.Controls, enable);
        }

        private static void EnableControls(Control.ControlCollection ctls, bool enable)
        {
            foreach (Control ctl in ctls)
            {
                ctl.Enabled = enable;
                EnableControls(ctl.Controls, enable);
            }
        }

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

            int numf = dgDrawings.Rows.Count;
            string[] files = new string[numf];
            foreach (DataGridViewRow drow in dgDrawings.Rows)
            {
                int num1 = drow.Index;
                files[num1] = Path.Combine(Path.Combine(drow.Cells[1].Value.ToString(), drow.Cells[0].Value.ToString()));
            }            
            KoleksiBlok kb = new KoleksiBlok(KoleksiBlok.PilihanDrawing.ExternalDrawing, files);
            kb.GetBlokByDrawing(KoleksiBlok.PilihanDrawing.ExternalDrawing);
            foreach (KeyValuePair<string, Autodesk.AutoCAD.DatabaseServices.ObjectId> item in kb.KoleksiBlokTableId )
            {
                newList.Add(item.Key.ToString());
            }
            foreach (string item in newList)
            {
                string[] value = new string[2];
                value[0] = Convert.ToString("true");
                value[1] = Convert.ToString(item);
                dgListBlocks.Rows.Add(value);
            }
        }
  
        private void dwgList_Remove(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgDrawings.Rows.Count > 0)
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
            if (dgDrawings.Rows.Count > 0)
            {
                buttonHapus.Enabled = true;
            }
            else
            {
                buttonHapus.Enabled = false;
            }
        }

        List<Autodesk.AutoCAD.DatabaseServices.BlockReference> ListAcDbBRef = new List<Autodesk.AutoCAD.DatabaseServices.BlockReference>();
        private void buttonSettings_Click(object sender, EventArgs e)
        {

        }

        private void PilihAtribut_Click(object sender, EventArgs e)
        {
            formListAttribut formlistAtr = new formListAttribut();
            formlistAtr.ShowDialog(this);
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

    }
}
