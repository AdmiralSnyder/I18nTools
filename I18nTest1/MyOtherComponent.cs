using CodeDomSerializerSample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I18nTest1
{
    [ToolboxItem(true)]
    public partial class MyOtherComponent : MyComponent
    {
        public MyOtherComponent()
        {
            InitializeComponent();
        }

        public MyOtherComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        public string OtherDATA { get; set; }
    }
}
