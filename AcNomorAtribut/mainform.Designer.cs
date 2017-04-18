namespace AcNomorAtribut
{
    partial class mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelObjCount = new System.Windows.Forms.Label();
            this.radioDrawingExisting = new System.Windows.Forms.RadioButton();
            this.radioEksternalDrawing = new System.Windows.Forms.RadioButton();
            this.radioPilihObjek = new System.Windows.Forms.RadioButton();
            this.buttonPick = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgDrawings = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonLoadList = new System.Windows.Forms.Button();
            this.buttonHapus = new System.Windows.Forms.Button();
            this.buttonTambah = new System.Windows.Forms.Button();
            this.buttonSaveList = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgListBlocks = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonDaftarAtribut = new System.Windows.Forms.Button();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgTabel = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.buttonKembali = new System.Windows.Forms.Button();
            this.buttonLanjut = new System.Windows.Forms.Button();
            this.buttonTutup = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDrawings)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListBlocks)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTabel)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.groupBox2, 4);
            this.groupBox2.Controls.Add(this.labelObjCount);
            this.groupBox2.Controls.Add(this.radioDrawingExisting);
            this.groupBox2.Controls.Add(this.radioEksternalDrawing);
            this.groupBox2.Controls.Add(this.radioPilihObjek);
            this.groupBox2.Controls.Add(this.buttonPick);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 122);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Objek";
            // 
            // labelObjCount
            // 
            this.labelObjCount.AutoSize = true;
            this.labelObjCount.Location = new System.Drawing.Point(248, 59);
            this.labelObjCount.Name = "labelObjCount";
            this.labelObjCount.Size = new System.Drawing.Size(21, 15);
            this.labelObjCount.TabIndex = 8;
            this.labelObjCount.Text = "{0}";
            // 
            // radioDrawingExisting
            // 
            this.radioDrawingExisting.AutoSize = true;
            this.radioDrawingExisting.Checked = true;
            this.radioDrawingExisting.Location = new System.Drawing.Point(12, 22);
            this.radioDrawingExisting.Name = "radioDrawingExisting";
            this.radioDrawingExisting.Size = new System.Drawing.Size(112, 19);
            this.radioDrawingExisting.TabIndex = 5;
            this.radioDrawingExisting.TabStop = true;
            this.radioDrawingExisting.Text = "Drawing &Existing";
            this.radioDrawingExisting.UseVisualStyleBackColor = true;
            // 
            // radioEksternalDrawing
            // 
            this.radioEksternalDrawing.AutoSize = true;
            this.radioEksternalDrawing.Location = new System.Drawing.Point(12, 91);
            this.radioEksternalDrawing.Name = "radioEksternalDrawing";
            this.radioEksternalDrawing.Size = new System.Drawing.Size(124, 19);
            this.radioEksternalDrawing.TabIndex = 7;
            this.radioEksternalDrawing.Text = "Eksternal &Drawings";
            this.radioEksternalDrawing.UseVisualStyleBackColor = true;
            // 
            // radioPilihObjek
            // 
            this.radioPilihObjek.AutoSize = true;
            this.radioPilihObjek.Location = new System.Drawing.Point(12, 57);
            this.radioPilihObjek.Name = "radioPilihObjek";
            this.radioPilihObjek.Size = new System.Drawing.Size(158, 19);
            this.radioPilihObjek.TabIndex = 6;
            this.radioPilihObjek.Text = "Pilih &Objek pada Drawing";
            this.radioPilihObjek.UseVisualStyleBackColor = true;
            // 
            // buttonPick
            // 
            this.buttonPick.Location = new System.Drawing.Point(207, 52);
            this.buttonPick.Name = "buttonPick";
            this.buttonPick.Size = new System.Drawing.Size(28, 28);
            this.buttonPick.TabIndex = 4;
            this.buttonPick.Text = "Pick";
            this.buttonPick.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.tabControl1, 6);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Enabled = false;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.ItemSize = new System.Drawing.Size(79, 20);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(8, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(543, 464);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(535, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Source";
            this.tabPage1.ToolTipText = "Pilihan Data Source untuk mendapatkan Block Reference";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Controls.Add(this.dgDrawings, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 5, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(529, 430);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // dgDrawings
            // 
            this.dgDrawings.AllowUserToAddRows = false;
            this.dgDrawings.AllowUserToDeleteRows = false;
            this.dgDrawings.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrawings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDrawings.ColumnHeadersHeight = 32;
            this.dgDrawings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgDrawings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.tableLayoutPanel2.SetColumnSpan(this.dgDrawings, 5);
            this.dgDrawings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDrawings.EnableHeadersVisualStyles = false;
            this.dgDrawings.Location = new System.Drawing.Point(3, 131);
            this.dgDrawings.MultiSelect = false;
            this.dgDrawings.Name = "dgDrawings";
            this.dgDrawings.ReadOnly = true;
            this.dgDrawings.RowHeadersVisible = false;
            this.dgDrawings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDrawings.Size = new System.Drawing.Size(434, 258);
            this.dgDrawings.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 200F;
            this.Column1.HeaderText = "Drawing";
            this.Column1.MinimumWidth = 85;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 300F;
            this.Column2.HeaderText = "Folder";
            this.Column2.MinimumWidth = 100;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 140F;
            this.Column3.HeaderText = "";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 140;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.buttonLoadList, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.buttonHapus, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonTambah, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonSaveList, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(443, 131);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(83, 258);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // buttonLoadList
            // 
            this.buttonLoadList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLoadList.Location = new System.Drawing.Point(3, 197);
            this.buttonLoadList.Name = "buttonLoadList";
            this.buttonLoadList.Size = new System.Drawing.Size(77, 26);
            this.buttonLoadList.TabIndex = 1;
            this.buttonLoadList.Text = "&Load List";
            this.buttonLoadList.UseVisualStyleBackColor = true;
            // 
            // buttonHapus
            // 
            this.buttonHapus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonHapus.Location = new System.Drawing.Point(3, 35);
            this.buttonHapus.Name = "buttonHapus";
            this.buttonHapus.Size = new System.Drawing.Size(77, 26);
            this.buttonHapus.TabIndex = 1;
            this.buttonHapus.Text = "&Hapus";
            this.buttonHapus.UseVisualStyleBackColor = true;
            // 
            // buttonTambah
            // 
            this.buttonTambah.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTambah.Location = new System.Drawing.Point(3, 3);
            this.buttonTambah.Name = "buttonTambah";
            this.buttonTambah.Size = new System.Drawing.Size(77, 26);
            this.buttonTambah.TabIndex = 1;
            this.buttonTambah.Text = "&Tambah";
            this.buttonTambah.UseVisualStyleBackColor = true;
            // 
            // buttonSaveList
            // 
            this.buttonSaveList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSaveList.Location = new System.Drawing.Point(3, 229);
            this.buttonSaveList.Name = "buttonSaveList";
            this.buttonSaveList.Size = new System.Drawing.Size(77, 26);
            this.buttonSaveList.TabIndex = 1;
            this.buttonSaveList.Text = "&Save List";
            this.buttonSaveList.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(535, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data Attribut";
            this.tabPage2.ToolTipText = "Data Attribut yang terdapat pada Block Reference";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 241F));
            this.tableLayoutPanel1.Controls.Add(this.dgListBlocks, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClearAll, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSelAll, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.75078F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.24922F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(529, 430);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // dgListBlocks
            // 
            this.dgListBlocks.AllowUserToAddRows = false;
            this.dgListBlocks.AllowUserToDeleteRows = false;
            this.dgListBlocks.AllowUserToResizeRows = false;
            this.dgListBlocks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgListBlocks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListBlocks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgListBlocks.ColumnHeadersHeight = 32;
            this.dgListBlocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgListBlocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.tableLayoutPanel1.SetColumnSpan(this.dgListBlocks, 2);
            this.dgListBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgListBlocks.EnableHeadersVisualStyles = false;
            this.dgListBlocks.Location = new System.Drawing.Point(3, 3);
            this.dgListBlocks.MultiSelect = false;
            this.dgListBlocks.Name = "dgListBlocks";
            this.dgListBlocks.ReadOnly = true;
            this.dgListBlocks.RowHeadersVisible = false;
            this.tableLayoutPanel1.SetRowSpan(this.dgListBlocks, 2);
            this.dgListBlocks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgListBlocks.Size = new System.Drawing.Size(282, 350);
            this.dgListBlocks.TabIndex = 3;
            this.dgListBlocks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListBlocks_CellContentClick);
            // 
            // Column4
            // 
            this.Column4.FillWeight = 65F;
            this.Column4.HeaderText = "";
            this.Column4.MinimumWidth = 45;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.FillWeight = 152.3477F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Nama Block";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.ToolTipText = "Nama Block dari Drawing";
            this.dataGridViewTextBoxColumn1.Width = 132;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.FillWeight = 200F;
            this.dataGridViewTextBoxColumn3.HeaderText = "";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 45;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 154;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonDaftarAtribut);
            this.groupBox3.Controls.Add(this.checkBox5);
            this.groupBox3.Controls.Add(this.checkBox4);
            this.groupBox3.Controls.Add(this.checkBox3);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(291, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 207);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kategori Attribut";
            // 
            // buttonDaftarAtribut
            // 
            this.buttonDaftarAtribut.Location = new System.Drawing.Point(103, 158);
            this.buttonDaftarAtribut.Name = "buttonDaftarAtribut";
            this.buttonDaftarAtribut.Size = new System.Drawing.Size(36, 24);
            this.buttonDaftarAtribut.TabIndex = 5;
            this.buttonDaftarAtribut.Text = ". . .";
            this.buttonDaftarAtribut.UseVisualStyleBackColor = true;
            this.buttonDaftarAtribut.Click += new System.EventHandler(this.PilihAtribut_Click);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(17, 161);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(67, 19);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "Attribut";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(17, 128);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(54, 19);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Layer";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(17, 95);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 19);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "Posisi Z";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(17, 62);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(66, 19);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Posisi Y";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 19);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Posisi X";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearAll.Location = new System.Drawing.Point(147, 359);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(138, 22);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnSelAll
            // 
            this.btnSelAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelAll.Location = new System.Drawing.Point(3, 359);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(138, 22);
            this.btnSelAll.TabIndex = 1;
            this.btnSelAll.Text = "Select All";
            this.btnSelAll.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.dgTabel);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(535, 436);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data Tabel";
            this.tabPage3.ToolTipText = "Koleksi dari Attribut pada Drawing";
            // 
            // dgTabel
            // 
            this.dgTabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTabel.Location = new System.Drawing.Point(7, 7);
            this.dgTabel.Name = "dgTabel";
            this.dgTabel.Size = new System.Drawing.Size(522, 374);
            this.dgTabel.TabIndex = 3;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(535, 436);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Hitung Analisa";
            this.tabPage4.ToolTipText = "Hitung Analisa Persentasi";
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(3, 473);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(83, 23);
            this.buttonSettings.TabIndex = 1;
            this.buttonSettings.Text = "&Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(94, 473);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(83, 23);
            this.button12.TabIndex = 1;
            this.button12.Text = "Te&mplate";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // buttonKembali
            // 
            this.buttonKembali.Location = new System.Drawing.Point(276, 473);
            this.buttonKembali.Name = "buttonKembali";
            this.buttonKembali.Size = new System.Drawing.Size(83, 23);
            this.buttonKembali.TabIndex = 1;
            this.buttonKembali.Text = "&Kembali";
            this.buttonKembali.UseVisualStyleBackColor = true;
            // 
            // buttonLanjut
            // 
            this.buttonLanjut.Location = new System.Drawing.Point(367, 473);
            this.buttonLanjut.Name = "buttonLanjut";
            this.buttonLanjut.Size = new System.Drawing.Size(83, 23);
            this.buttonLanjut.TabIndex = 1;
            this.buttonLanjut.Text = "&Lanjut";
            this.buttonLanjut.UseVisualStyleBackColor = true;
            // 
            // buttonTutup
            // 
            this.buttonTutup.Location = new System.Drawing.Point(458, 473);
            this.buttonTutup.Name = "buttonTutup";
            this.buttonTutup.Size = new System.Drawing.Size(84, 23);
            this.buttonTutup.TabIndex = 1;
            this.buttonTutup.Text = "&Batal";
            this.buttonTutup.UseVisualStyleBackColor = true;
            this.buttonTutup.Click += new System.EventHandler(this.buttonTutup_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel4.Controls.Add(this.buttonSettings, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button12, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonTutup, 5, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonLanjut, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.buttonKembali, 3, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(549, 502);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 502);
            this.Controls.Add(this.tableLayoutPanel4);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Attribut Kolektor";
            this.Load += new System.EventHandler(this.mainform_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDrawings)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgListBlocks)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTabel)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioDrawingExisting;
        private System.Windows.Forms.RadioButton radioEksternalDrawing;
        private System.Windows.Forms.RadioButton radioPilihObjek;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelObjCount;
        private System.Windows.Forms.Button buttonPick;
        private System.Windows.Forms.DataGridView dgDrawings;
        private System.Windows.Forms.Button buttonSaveList;
        private System.Windows.Forms.Button buttonLoadList;
        private System.Windows.Forms.Button buttonHapus;
        private System.Windows.Forms.Button buttonTambah;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgListBlocks;
        private System.Windows.Forms.Button buttonDaftarAtribut;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgTabel;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnSelAll;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button buttonKembali;
        private System.Windows.Forms.Button buttonLanjut;
        private System.Windows.Forms.Button buttonTutup;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}