using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseFormLib
{
    [DefaultSerializationProvider(typeof(FirstDefaultSerializationProvider))]

    public class BaseFormSerializer : CodeDomSerializer
    {
        public BaseFormSerializer()
        {
            MessageBox.Show("BaseFormSerializer ctor");
        }
    }
}
