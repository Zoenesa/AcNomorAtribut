﻿using Autodesk.AutoCAD.DatabaseServices;
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
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace AcNomorAtribut
{
    public partial class formTest : Form
    {
        public formTest()
        {
            InitializeComponent();
            this.radioDrawingExisting.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            this.radioEksternalDrawing.CheckedChanged += new EventHandler(this.radio_CheckedChanged);
            this.radioPilihObjek.CheckedChanged += new EventHandler(this.radio_CheckedChanged);
            this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected); 
 
            numexpor = 0;
            dgListDrawings.Enabled = false;
            buttonPick.Enabled = false;
            buttonTambah.Enabled = false;
            buttonHapus.Enabled = false;
            colblock = 0; labelObjCount.Text = colblock.ToString();
            tabControl1.Enabled = true;
        }

        private string[] headerName = { "Attribute Kolektor", "-","Data Source"};
        public static List<string> atributName;
        private int colblock;
        private int numexpor;
        private DataBlok dtBlok;

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

        List<string> ListBlokTabledwg;
        List<string> ListBlokRefsdwg;
        List<string> ListAttr;

        private TreeView tvw;
                
        private void formTest_Load(object sender, EventArgs e)
        {
            tvw = new TreeView();

            dtBlok = new DataBlok();
            ListBlokTabledwg = dtBlok.GetParentBlokName();
            ListBlokRefsdwg = dtBlok.GetBlokRefs();
            ListAttr = dtBlok.AttList;
            dgListDrawings.Rows.Add(dtBlok.infoFile.Name, dtBlok.infoFile.DirectoryName);
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

        private void buttonLanjut_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                tabPage1.Enabled = false;
                tabPage2.Enabled = true;
                tabControl1.SelectedTab = tabPage2;
                dgListBlocks.Rows.Clear();
                object[] values = new object[3];
                string[] dataKategori = { "Data Unit", "Data Kavling", "Data Point"};
                foreach (string item in this.ListBlokTabledwg)
                {
                    values[0] = true;
                    values[1] = item;
                    values[2] = dataKategori;
                    dgListBlocks.Rows.Add(true, item, " ");
                }
                for (int i = 0; i < dgListBlocks.Rows.Count; i++)
                {
                    DataGridViewComboBoxCell cbcell = (DataGridViewComboBoxCell)dgListBlocks.Rows[i].Cells[3];
                    cbcell.Value = dataKategori.ToList();
                }
                
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
        }

        private void buttonKembali_Click(object sender, EventArgs e)
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

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
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

        private void buttonTutup_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void buttonDaftarAtribut_Click(object sender, EventArgs e)
        { 
            formListAttribut frmListatt = new formListAttribut();
            Autodesk.AutoCAD.ApplicationServices.Application.ShowModalDialog(frmListatt);
           
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
