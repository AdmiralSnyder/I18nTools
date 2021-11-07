using I18nTest1;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseFormLib
{
    public class FirstDefaultSerializationProvider : IDesignerSerializationProvider
    {
        public FirstDefaultSerializationProvider() { }

        public object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
        {
            MessageBox.Show("firstdef");
            if (typeof(BaseForm).IsAssignableFrom(objectType))
            {
                return new BaseFormSerializer();
            }
            else
            {
                return currentSerializer;
            }
        }
    }
}
