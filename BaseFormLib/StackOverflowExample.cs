using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using System.ComponentModel.Design;
using System.Globalization;

namespace LocalizationTest
{
    [Designer(typeof(LocalizerDesigner))]
    [DesignerSerializer(typeof(LocalizerSerializer), typeof(CodeDomSerializer))]
    [ToolboxItem(true)]
    public class Localizer : Component
    {
        public static void GetResourceManager(Type type, ref ComponentResourceManager resourceManager)
        {
            var newResourceMan = new MyResourceManager(type, resourceManager);
            resourceManager = newResourceMan;
        }
    }

    public class MyResourceManager : ComponentResourceManager
    {
        public MyResourceManager(Type type, ComponentResourceManager manager) : base(type)
        {
            InnerManager = manager;
        }

        private ComponentResourceManager InnerManager { get; set; }

        public override void ApplyResources(object value, string objectName, CultureInfo culture)
        {
            base.ApplyResources(value, objectName, culture);
        }

        
    }


    public class LocalizerSerializer : CodeDomSerializer
    {
        public override object Deserialize(IDesignerSerializationManager manager, object codeDomObject)
        {
            CodeDomSerializer baseSerializer = (CodeDomSerializer)
                manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
            return baseSerializer.Deserialize(manager, codeDomObject);
        }

        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            CodeDomSerializer baseSerializer =
                (CodeDomSerializer)manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));

            object codeObject = baseSerializer.Serialize(manager, value);

            if (codeObject is CodeStatementCollection)
            {
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;
                CodeTypeDeclaration classTypeDeclaration =
                    (CodeTypeDeclaration)manager.GetService(typeof(CodeTypeDeclaration));
                CodeExpression typeofExpression = new CodeTypeOfExpression(classTypeDeclaration.Name);
                CodeDirectionExpression refResourceExpression = new CodeDirectionExpression(
                    FieldDirection.Ref, new CodeVariableReferenceExpression("resources"));
                CodeExpression rightCodeExpression =
                    new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("LocalizationTest.Localizer"), "GetResourceManager",
                    new CodeExpression[] { typeofExpression, refResourceExpression });

                statements.Insert(0, new CodeExpressionStatement(rightCodeExpression));
            }
            return codeObject;
        }
    }

    public class LocalizerDesigner : ComponentDesigner
    {
        public override void Initialize(IComponent c)
        {
            base.Initialize(c);
            var dh = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (dh == null)
                return;

            var innerListProperty = dh.Container.Components.GetType().GetProperty("InnerList", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.FlattenHierarchy);
            var innerList = innerListProperty.GetValue(dh.Container.Components, null) as System.Collections.ArrayList;
            if (innerList == null)
                return;
            if (innerList.IndexOf(c) <= 1)
                return;
            innerList.Remove(c);
            innerList.Insert(0, c);

        }
    }


}