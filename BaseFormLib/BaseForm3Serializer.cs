using BaseFormLib;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeDomSerializerSample
{
    internal class BaseForm3Serializer : CodeDomSerializer
    {
        public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
        {
            // This is how we associate the component with the serializer.
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
            GetSerializer(typeof(BaseForm3).BaseType, typeof(CodeDomSerializer));

            /* This is the simplest case, in which the class just calls the base class
                to do the work. */
            return baseClassSerializer.Deserialize(manager, codeObject);
        }

        private object BaseSerialize(IDesignerSerializationManager manager, object value)
        {
            object obj2 = null;
            if ((manager == null) || (value == null))
            {
                throw new ArgumentNullException((manager == null) ? "manager" : "value");
            }
            //using (CodeDomSerializerBase.TraceScope("CodeDomSerializer::Serialize"))
            //{
            bool flag2;
            bool flag3;
            if (value is Type)
            {
                return new CodeTypeOfExpression((Type)value);
            }
            bool flag = false;
            CodeExpression expression = base.SerializeCreationExpression(manager, value, out flag2);
            if (!(value is IComponent))
            {
                flag = flag2;
            }
            ExpressionContext context = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
            if ((context != null) && object.ReferenceEquals(context.PresetValue, value))
            {
                flag3 = true;
            }
            else
            {
                flag3 = false;
            }
            if (expression == null)
            {
                return obj2;
            }
            if (flag)
            {
                return expression;
            }
            CodeStatementCollection statements = new CodeStatementCollection();
            if (flag3)
            {
                base.SetExpression(manager, value, expression, true);
            }
            else
            {
                string uniqueName = base.GetUniqueName(manager, value);
                CodeVariableDeclarationStatement statement = new CodeVariableDeclarationStatement(TypeDescriptor.GetClassName(value), uniqueName)
                {
                    InitExpression = expression
                };
                statements.Add(statement);
                CodeExpression expression2 = new CodeVariableReferenceExpression(uniqueName);
                base.SetExpression(manager, value, expression2);
            }
            //base.SerializePropertiesToResources(manager, statements, value, _designTimeFilter);
            base.SerializeProperties(manager, statements, value, _designTimeFilter);

            base.SerializeProperties(manager, statements, value, _runTimeFilter);
            base.SerializeEvents(manager, statements, value, _runTimeFilter);
            return statements;
            //}
        }

        private void BaseSerializePropertiesToResources(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
        {
            //using (TraceScope("ComponentCodeDomSerializerBase::SerializePropertiesToResources"))
            //{
                PropertyDescriptorCollection descriptors = GetPropertiesHelper(manager, value, filter);
                manager.Context.Push(statements);
                try
                {
                    CodeExpression targetObject = this.SerializeToExpression(manager, value);
                    if (targetObject != null)
                    {
                        CodePropertyReferenceExpression expression = new CodePropertyReferenceExpression(targetObject, string.Empty);
                        foreach (PropertyDescriptor descriptor in descriptors)
                        {
                            ExpressionContext context = new ExpressionContext(expression, descriptor.PropertyType, value);
                            manager.Context.Push(context);
                            try
                            {
                                if (descriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Visible))
                                {
                                    string name;
                                    expression.PropertyName = descriptor.Name;
                                    if (targetObject is CodeThisReferenceExpression)
                                    {
                                        name = "$this";
                                    }
                                    else
                                    {
                                        name = manager.GetName(value);
                                    }
                                    name = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[] { name, descriptor.Name });
                                    ResourceCodeDomSerializer.Default.SerializeMetadata(manager, name, descriptor.GetValue(value), descriptor.ShouldSerializeValue(value));
                                }
                            }
                            finally
                            {
                                manager.Context.Pop();
                            }
                        }
                    }
                }
                finally
                {
                    manager.Context.Pop();
                }
            //}

        }

        private static readonly Attribute[] _designTimeFilter = new Attribute[] { DesignOnlyAttribute.Yes };
        private static readonly Attribute[] _runTimeFilter = new Attribute[] { DesignOnlyAttribute.No };

        public override object Serialize(IDesignerSerializationManager manager, object value)
        {
            WalkContextStack(manager.Context);



            /* Associate the component with the serializer in the same manner as with
                Deserialize */
            CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
                GetSerializer(typeof(BaseForm3).BaseType, typeof(CodeDomSerializer));

            object codeObject = BaseSerialize(manager, value);

            /* Anything could be in the codeObject.  This sample operates on a
                CodeStatementCollection. */
            if (codeObject is CodeStatementCollection)
            {
                CodeStatementCollection statements = (CodeStatementCollection)codeObject;

                // The code statement collection is valid, so add a comment.
                string commentText = "This comment was added to this object by a custom serializer23.";
                CodeCommentStatement comment = new CodeCommentStatement(commentText);
                statements.Insert(0, comment);
            }
            return codeObject;
        }

        public static void WalkContextStack(ContextStack contextStack)
        {
            StringBuilder sb = new();

            int i = 0;
            while (true)
            {
                if (contextStack[i] != null)
                {
                    var context = contextStack[i];
                    AnalyzeContext(context, i, sb);
                    i++;
                }
                else
                {
                    break;
                }
            }

            File.WriteAllText(@"c:\temp\Contexts.txt", sb.ToString());
        }

        public static void AnalyzeContext(object context, int idx, StringBuilder sb)
        {
            if (ToStringFuncs.TryGetValue(context.GetType(), out var func) || ToStringFuncsByTypeName.TryGetValue(context.GetType().Name, out func))
            {
                sb.AppendLine(new string(' ', idx) + func(context));
            }
            else
            {
                sb.AppendLine(new string(' ', idx) + $"{context.GetType().Name} {context.GetType().FullName}");
            }


        }

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

        private static Dictionary<string, Func<object, string>> ToStringFuncsByTypeName = new()
        {
            ["LegacyExpressionTable"] = ctx =>
            {
                var tab = (Hashtable)ctx;
                string res = $"LegacyExpressionTable : ({tab.Count}){Environment.NewLine}";
                foreach (DictionaryEntry item in tab)
                {
                    if (item.Key.Equals(item.Value))
                    {
                        res += $"{item.Key.GetType().Name} : {item.Key}{Environment.NewLine}";

                    }
                    else
                    {
                        res += $"[{item.Key.GetType().Name} : {item.Key}] = {item.Value.GetType().Name} : {item.Value}{Environment.NewLine}";
                    }
                }
                return res;
            },
            ["ExpressionTable"] = ctx =>
            {
                Type typ = ctx.GetType();
                var prop = typ.GetProperty("Expressions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                Hashtable exprs = prop.GetValue(ctx) as Hashtable;
                string items = "";
                if (exprs != null)
                {
                    items = $"({exprs.Count}{Environment.NewLine})";
                    foreach (DictionaryEntry item in exprs)
                    {
                        if (item.Key.Equals(item.Value))
                        {
                            items += $"{item.Key.GetType().Name} : {item.Key}{Environment.NewLine}";

                        }
                        else
                        {
                            items += $"[{item.Key.GetType().Name} : {item.Key}] = {item.Value.GetType().Name} : {item.Value}{Environment.NewLine}";
                        }
                    }
                }
                return $"{ctx.GetType().Name} {items}";
            },
        };
    }
}