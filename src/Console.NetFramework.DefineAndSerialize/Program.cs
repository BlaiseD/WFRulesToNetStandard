using Console.NetFramework.DefineAndSerialize.Properties;
using System.CodeDom;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;

namespace Console.NetFramework.DefineAndSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateAndSerializeRuleSet();
        }

        private static Rule Rule_with_equals_condition_and_setter_action()
        {
            // define first predicate: this.State == "CT"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("CT")
            };

            //discount action this.Discount = 100
            CodeAssignStatement discountAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(100)
                );

            Rule rule1 = new Rule("Rule1")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule1.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule1;
        }

        private static Rule Rule_with_array_indexer_condition_and_setter_action()
        {
            //this.StringList[1, 1]
            CodeArrayIndexerExpression indexerExpression = new CodeArrayIndexerExpression
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "StringList"),
                    new CodeExpression[] { new CodePrimitiveExpression(1), new CodePrimitiveExpression(1) }
                );

            //this.StringList[1, 1] == "A"
            CodeBinaryOperatorExpression stringIndexTest = new CodeBinaryOperatorExpression
            {
                Left = indexerExpression,
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("A")
            };

            //discount action this.Discount = 200
            CodeAssignStatement discountAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(200)
                );

            Rule rule2 = new Rule("Rule2")
            {
                Condition = new RuleExpressionCondition(stringIndexTest)
            };
            rule2.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule2;
        }


        private static Rule Rule_with_method_condition()
        {
            //this.boolMethod()
            CodeMethodInvokeExpression boolMethodInvoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "BoolMethod", new CodeExpression[] { });

            //discount action this.discount = 300
            CodeAssignStatement discountAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(300)
                );

            Rule rule3 = new Rule("Rule3")
            {
                Condition = new RuleExpressionCondition(boolMethodInvoke)
            };
            rule3.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule3;
        }


        private static Rule Rule_with_multiple_conditions()
        {
            // define first predicate: this.State == "MD"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("MD")
            };

            //this.boolMethod()
            CodeMethodInvokeExpression boolMethodInvoke = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "BoolMethod", new CodeExpression[] { });

            //combine both expressions this.state == "MD" && this.boolMethod()
            CodeBinaryOperatorExpression codeBothExpression = new CodeBinaryOperatorExpression
            {
                Left = ruleStateTest,
                Operator = CodeBinaryOperatorType.BooleanAnd,
                Right = boolMethodInvoke
            };

            //discount action this.Discount = 400
            CodeAssignStatement discountAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "Discount"),
                    new CodePrimitiveExpression(400)
                );

            Rule rule4 = new Rule("Rule4")
            {
                Condition = new RuleExpressionCondition(codeBothExpression)
            };
            rule4.ThenActions.Add(new RuleStatementAction(discountAction));

            return rule4;
        }


        private static Rule Rule_with_static_method_action()
        {
            // define first predicate: this.State == "MA"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("MA")
            };

            //action SampleFlow.FlowEntity.SetDefaultState("NH")
            CodeMethodInvokeExpression methodInvoke = new CodeMethodInvokeExpression
                (
                    new CodeTypeReferenceExpression("SampleFlow.FlowEntity"),
                    "SetDefaultState",
                    new CodeExpression[] { new CodePrimitiveExpression("NH") }
                );

            Rule rule5 = new Rule("Rule5")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule5.ThenActions.Add(new RuleStatementAction(methodInvoke));

            return rule5;
        }


        private static Rule Rule_with_reference_method_action()
        {
            // define first predicate: this.State == "PA"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("PA")
            };


            //action this.TheType = this.GetType
            CodeAssignStatement setTypeAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TheType"),
                    new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetType", new CodeExpression[] { })
                );

            Rule rule6 = new Rule("Rule6")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule6.ThenActions.Add(new RuleStatementAction(setTypeAction));

            return rule6;
        }


        private static Rule Rule_with_Cast_object_expression_in_then_action()
        {
            // define first predicate: this.State == "VA"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("VA")
            };

            //(SampleFlow.ChildEntity)this.DClass
            CodeCastExpression castExpression = new CodeCastExpression
                (
                "SampleFlow.ChildEntity",
                 //this.DClass
                 new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "DClass")
                 );

            //((SampleFlow.ChildEntity)this.DClass).Description = "This Description"
            CodeAssignStatement assignmentAction = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression(castExpression, "Description"),
                    new CodePrimitiveExpression("This Description")
                );

            Rule rule7 = new Rule("Rule7")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule7.ThenActions.Add(new RuleStatementAction(assignmentAction));

            return rule7;
        }


        private static Rule Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action()
        {
            // define first predicate: this.State == "NC"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("NC")
            };

            //this.DoNothing(new SampleFlow.OtherEntity("AAA", "BBB"))
            CodeMethodInvokeExpression methodInvokeDoNothing = new CodeMethodInvokeExpression
                (
                    new CodeThisReferenceExpression(),
                    "SetFirstValue",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference("SampleFlow.OtherEntity"),
                            new CodeExpression[]
                            {
                                new CodePrimitiveExpression("AAA"),
                                new CodePrimitiveExpression("BBB")
                            }
                        )
                );

            Rule rule8 = new Rule("Rule8")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule8.ThenActions.Add(new RuleStatementAction(methodInvokeDoNothing));

            return rule8;
        }

        private static Rule Rule_with_list_initialization()
        {
            // define first predicate: this.State == "SC"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("SC")
            };

            //this.SetCollection(new System.Collections.ObjectModel.Collection<object>(new object[] { "AValue", "BValue"}))
            CodeMethodInvokeExpression methodInvokeSetCollection = new CodeMethodInvokeExpression
                (
                    new CodeThisReferenceExpression(),
                    "SetCollection",
                    new CodeObjectCreateExpression
                        (
                            new CodeTypeReference
                                (
                                    "System.Collections.ObjectModel.Collection",
                                    new CodeTypeReference[] { new CodeTypeReference("System.Object") }
                                ),
                            new CodeArrayCreateExpression
                            (
                                "System.Object",
                                new CodeExpression[] { new CodePrimitiveExpression("AValue"), new CodePrimitiveExpression("BValue") }
                            )
                        )
                );

            Rule rule9 = new Rule("Rule9")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule9.ThenActions.Add(new RuleStatementAction(methodInvokeSetCollection));

            return rule9;
        }

        private static Rule Rule_with_child_and_granchild_reference()
        {
            // define first predicate: this.State == "GA"
            CodeBinaryOperatorExpression ruleStateTest = new CodeBinaryOperatorExpression
            {
                Left = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "State"),
                Operator = CodeBinaryOperatorType.ValueEquality,
                Right = new CodePrimitiveExpression("GA")
            };

            //this.FirstClass.SecondClass.Property1 = "This Value"
            CodeAssignStatement setProperty1Action = new CodeAssignStatement
                (
                    new CodePropertyReferenceExpression
                    (
                        new CodePropertyReferenceExpression
                        (
                            new CodePropertyReferenceExpression
                            (
                                new CodeThisReferenceExpression(),
                                "FirstClass"
                            ),
                            "SecondClass"
                        ),
                        "Property1"
                    ),
                    new CodePrimitiveExpression("This Value")
                );

            Rule rule10 = new Rule("Rule10")
            {
                Condition = new RuleExpressionCondition(ruleStateTest)
            };
            rule10.ThenActions.Add(new RuleStatementAction(setProperty1Action));

            return rule10;
        }

        private static void CreateAndSerializeRuleSet()
        {
            RuleSet ruleSet = new RuleSet
            {
                Name = "MyRuleSet",
                ChainingBehavior = RuleChainingBehavior.Full
            };

            ruleSet.Rules.Add(Rule_with_equals_condition_and_setter_action());
            ruleSet.Rules.Add(Rule_with_array_indexer_condition_and_setter_action());
            ruleSet.Rules.Add(Rule_with_method_condition());
            ruleSet.Rules.Add(Rule_with_multiple_conditions());
            ruleSet.Rules.Add(Rule_with_static_method_action());
            ruleSet.Rules.Add(Rule_with_reference_method_action());
            ruleSet.Rules.Add(Rule_with_Cast_object_expression_in_then_action());
            ruleSet.Rules.Add(Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action());
            ruleSet.Rules.Add(Rule_with_list_initialization());
            ruleSet.Rules.Add(Rule_with_child_and_granchild_reference());

            string rulesSetString = SerializeRules(ruleSet);
            using (System.IO.StreamWriter sr = new System.IO.StreamWriter(Settings.Default.ruleSetFile, false, System.Text.Encoding.Unicode))
            {
                sr.Write(rulesSetString);
            }
        }

        private static string SerializeRules(object drs)
        {
            System.Text.StringBuilder ruleDefinition = new System.Text.StringBuilder();
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            using (System.IO.StringWriter stringWriter = new System.IO.StringWriter(ruleDefinition, System.Globalization.CultureInfo.InvariantCulture))
            {
                using (System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stringWriter))
                {
                    serializer.Serialize(writer, drs);
                    writer.Flush();
                }
                stringWriter.Flush();
            }

            return ruleDefinition.ToString();
        }
    }
}
