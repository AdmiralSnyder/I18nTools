using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I18nTest1
{
    public class CustomCodeDomSerializer : CodeDomSerializer
    {
        public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
        {
            //return base.Deserialize(manager, codeObject);


            // This is how we associate the component with the serializer.
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
            GetSerializer(codeObject.GetType().BaseType, typeof(CodeDomSerializer));

            /* This is the simplest case, in which the class just calls the base class
                to do the work. */
            return baseClassSerializer.Deserialize(manager, codeObject);
        }


        public override object Serialize(IDesignerSerializationManager manager, object value) =>
            //base.Serialize(manager, value);
            SerializeObj(manager, value);

        private static Dictionary<Type, Func<object, string>> ToStringFuncs = new()
        {
            [typeof(CodeStatementCollection)] = ctx => $"{nameof(CodeStatementCollection)} : ({((CodeStatementCollection)ctx).Count})",
            [typeof(ExpressionContext)] = ctx => $"{ nameof(ExpressionContext)}: {((ExpressionContext)ctx).Expression}",
            //[typeof(CodeDomLocalizationModel)] = ctx => $"{ nameof(CodeDomLocalizationModel)}: ???",
            [typeof(StatementContext)] = ctx => $"{ nameof(StatementContext)}: {((StatementContext)ctx).StatementCollection.OfType<CodeStatement>().Count()}",
            [typeof(RootContext)] = ctx => $"{ nameof(RootContext)}: {((RootContext)ctx).Expression}",
            [typeof(CodeTypeDeclaration)] = ctx => $"{ nameof(CodeTypeDeclaration)}: {((CodeTypeDeclaration)ctx).Name}",

            

            //[typeof(InheritedPropertyDescriptor)] = ctx => $"{ nameof(InheritedPropertyDescriptor)}: {((ExpressionContext)ctx).Expression}",
        };

        private object SerializeObj(IDesignerSerializationManager manager, object value)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{DateTime.Now.Ticks} { value.GetType().Name}");
            var obj = base.Serialize(manager, value);

            if (obj is CodeStatementCollection objCol)
            {
                sb.AppendLine($"obj: { obj.GetType()}, ({(obj as CodeStatementCollection)?.Count?? -1})");
                foreach (CodeStatement stmt in objCol)
                {
                    sb.AppendLine($"stmt: { stmt.GetType()}: {stmt}");
                }
            }


            for (var i = 0; manager.Context[i] != null; i++)
            {
                var context = manager.Context[i];
                if (ToStringFuncs.TryGetValue(context.GetType(), out var func))
                {
                    sb.AppendLine(func(context));
                }
                else
                {
                    sb.AppendLine($"{context.GetType().Name} {context}");
                }
                var collection = manager.Context[i] as CodeStatementCollection;
                if (collection != null)
                {
                    sb.AppendLine($"IN {manager.Context[i].GetType().Name} ({collection.Count})");
                    CodeVariableDeclarationStatement st = null;
                    foreach (var statement in collection)
                    {
                        sb.AppendLine($"{statement} // {statement.GetType().Name}");
                        st = statement as CodeVariableDeclarationStatement;
                        if (st?.Type.BaseType == typeof(ComponentResourceManager).FullName)
                        {
                            var ctr = new CodeTypeReference(typeof(CustomComponentResourceManager));
                            st.Type = ctr;
                            st.InitExpression = new CodeObjectCreateExpression(ctr, new CodeTypeOfExpression(manager.GetName(value)));

                        }
                    }
                    if (st is not null)
                    {
                        var idx = collection.IndexOf(st);
                        collection.Insert(idx, new CodeCommentStatement("Hier ist ein Kommentar"));
                    }

                }
            }
            File.WriteAllText(@"c:\temp\statements.txt", 
                //Environment.NewLine + Environment.NewLine + 
                sb.ToString());
            var baseClassSerializer = (CodeDomSerializer)manager.GetSerializer(value.GetType().BaseType, typeof(CodeDomSerializer));
            var codeObject = baseClassSerializer.Serialize(manager, value);
            return codeObject;
        }
    }
}
