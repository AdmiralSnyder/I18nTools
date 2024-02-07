using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignerInterceptor;

public class I18nTypeDescriptorFilterService : ITypeDescriptorFilterService
{
    private Attribute[] nonLoc = new[] { LocalizableAttribute.No };
    private HashSet<string> PropertyNamesToLocalize = new() { "Caption", "Text", "Hint", "ToolTip" };

    public I18nTypeDescriptorFilterService(ITypeDescriptorFilterService baseService)
    {
        BaseService = baseService;
    }

    private ITypeDescriptorFilterService BaseService;

    public bool FilterAttributes(IComponent component, IDictionary attributes)
    {
        return BaseService?.FilterAttributes(component, attributes) ?? true;
    }

    public bool FilterEvents(IComponent component, IDictionary events)
    {
        return BaseService?.FilterEvents(component, events) ?? true;
    }

    public bool FilterProperties(IComponent component, IDictionary properties)
    {
        var res = BaseService?.FilterProperties(component, properties) ?? true;


        string[] propNames = new string[properties.Keys.Count];
        properties.Keys.CopyTo(propNames, 0);
        foreach (string propName in propNames)
        {
            if (PropertyNamesToLocalize.Contains(propName))
            {
                continue;
            }

            if (properties[propName] is PropertyDescriptor prop && prop.Attributes.Contains(LocalizableAttribute.Yes))
            {
                prop = TypeDescriptor.CreateProperty(
                    prop.ComponentType,
                    prop,
                    nonLoc);
                properties[propName] = prop;
                // TODO auf die Tag-Property gucken, und die raushauen, wenn sie ein string ist (und keine GUID)
            }
        }
        return res;
    }
}
