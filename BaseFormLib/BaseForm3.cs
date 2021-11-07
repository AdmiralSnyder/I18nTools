using CodeDomSerializerSample;
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

namespace BaseFormLib
{
    [DesignerSerializer(typeof(BaseForm3Serializer), typeof(CodeDomSerializer))]

    public partial class BaseForm3 : Form
    {
        public BaseForm3()
        {
            InitializeComponent();
        }
        public string Naffel { get; set; } = "Keks";
    }
}
