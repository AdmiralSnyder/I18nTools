using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I18nTest1
{
    //[DesignerSerializer(typeof(CustomCodeDomSerializer), typeof(CodeDomSerializer))]
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }
    }
}
