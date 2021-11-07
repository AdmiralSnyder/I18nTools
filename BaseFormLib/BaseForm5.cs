using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseFormLib
{
    public partial class BaseForm5 : Form
    {
        public BaseForm5()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("x-cdvsl");
            var newForm = new BaseForm5();
            newForm.ShowDialog();
        }
    }
}
