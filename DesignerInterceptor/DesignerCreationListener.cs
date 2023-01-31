using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignerInterceptor
{
    class DesignerCreationListener : Microsoft.VisualStudio.Editor.IVsTextViewCreationListener
    {
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            //throw new NotImplementedException();
        }
    }
}
