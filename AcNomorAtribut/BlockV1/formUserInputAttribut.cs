using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcNomorAtribut
{
    public partial class formUserInputAttribut : Form
    {
        public formUserInputAttribut()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(IWin32Window parentform, List<string> listTag)
        {
            DialogResult dlg;
            for (int i = 0; i < listTag.Count; i++)
            {
                Label lb = new Label();
                lb.Font = this.Font;
                lb.ForeColor = this.ForeColor;
                lb.TextAlign = ContentAlignment.MiddleLeft;
                lb.Text = listTag[i];
                tableLayoutPanel1.Controls.Add(lb, 0, i);
                tableLayoutPanel1.SetColumnSpan(lb, 1);
                lb.Dock = DockStyle.Fill;
            }
            base.StartPosition = FormStartPosition.CenterParent;
            dlg = base.ShowDialog();
            return dlg;
        }
         
        public DialogResult ShowDialog(List<string> listTag)
        {
            DialogResult dlg;
            for (int i = 0; i < listTag.Count; i++)
            {
                Label lb = new Label();
                lb.Font = this.Font;
                lb.ForeColor = this.ForeColor;
                lb.TextAlign = ContentAlignment.MiddleLeft;
                lb.Text = listTag[i];
                tableLayoutPanel1.Controls.Add(lb, 0, i);
                tableLayoutPanel1.SetColumnSpan(lb, 1);
                lb.Dock = DockStyle.Fill;
            }
            base.StartPosition = FormStartPosition.CenterScreen;
            this.textBox3.Text = TraceArea.ValueAreaBoundaries;
            dlg = base.ShowDialog();
            return dlg;
        }

        private List<string> userInput;
        public List<string> InputString
        {
            get
            { return userInput; }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            userInput = new List<string>();
            userInput.Add(textBox1.Text.Trim()); //No
            userInput.Add(textBox3.Text.Trim()); //Area
            userInput.Add(textBox2.Text.Trim()); //blok
            userInput.Add(textBox4.Text.Trim()); //Tipe
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string area = TraceArea.GetAreaBound();
            if (area != null)
            {
                this.Show();
                textBox3.Text = area;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool flagRot;
        public bool UseRotation
        {
            get
            {
                return flagRot;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flagRot = checkBox1.Checked;
        }

    }
}
