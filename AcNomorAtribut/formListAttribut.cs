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
    public partial class formListAttribut : Form
    {
        public formListAttribut()
        {
            InitializeComponent();
        }

        private void formListAttribut_Load(object sender, EventArgs e)
        {
            if (mainform.atributName.Count > 0)
            {
                foreach (string item in mainform.atributName)
                {
                    chListAtr.Items.Add(item.ToString(), true);
                }
            }
        }
    }
}
