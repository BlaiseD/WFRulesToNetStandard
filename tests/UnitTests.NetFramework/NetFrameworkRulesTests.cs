using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using Xunit;

namespace UnitTests.NetFramework
{
    public class NetFrameworkRulesTests
    {
        public NetFrameworkRulesTests()
        {
            CreateRuleEngine();
        }

        [Fact]
        public void Test_Rule_with_equals_condition_and_setter_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "CT"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.Discount == 100);
        }

        [Fact]
        public void Test_Rule_with_array_indexer_condition_and_setter_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity();
            entity.StringList[1, 1] = "A";

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.Discount == 200);
        }

        [Fact]
        public void Test_Rule_with_method_condition()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                BoolText = "false"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.Discount == 300);
        }

        [Fact]
        public void Test_Rule_with_multiple_conditions()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                BoolText = "false",
                State = "MD"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.Discount == 400);
        }

        [Fact]
        public void Test_Rule_with_static_method_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "MA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(SampleFlow.FlowEntity.DEFAULTSTATE == "NH");
        }

        [Fact]
        public void Test_Rule_with_reference_method_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "PA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.TheType.FullName == typeof(SampleFlow.FlowEntity).FullName);
        }

        [Fact]
        public void Test_Rule_with_cast_object_expression_in_then_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "VA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(((SampleFlow.ChildEntity)entity.DClass).Description == "This Description");
        }

        [Fact]
        public void Test_Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "NC"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.FirstValue == "AAA");
        }

        [Fact]
        public void Test_Rule_with_list_initialization()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "SC"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.MyCollection.First().ToString() == "AValue");
        }

        [Fact]
        public void Test_Rule_with_child_and_granchild_reference()
        {
            //Arrange
            SampleFlow.FlowEntity entity = new SampleFlow.FlowEntity
            {
                State = "GA"
            };

            //Act
            ruleEngine.Execute(entity);
            //Assert
            Assert.True(entity.FirstClass.SecondClass.Property1 == "This Value");
        }

        [Fact]
        public void TestSerialization()
        {
            //Arrange
            const string existing = "<RuleSet ChainingBehavior=\"Full\" Description=\"{p1:Null}\" Name=\"MyRuleSet\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">\r\n\t<RuleSet.Rules>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule1\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">CT</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">100</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule2\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression.Indices>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression.Indices>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeArrayIndexerExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"StringList\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodeArrayIndexerExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">A</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">200</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule3\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"BoolMethod\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">300</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule4\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"BooleanAnd\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">MD</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"BoolMethod\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Discount\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">400</ns1:Int32>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule5\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">MA</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetDefaultState\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeTypeReferenceExpression Type=\"SampleFlow.FlowEntity\" />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">NH</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t</ns0:CodeExpressionStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule6\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">PA</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"TheType\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"GetType\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule7\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">VA</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Description\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeCastExpression TargetType=\"SampleFlow.ChildEntity\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeCastExpression.Expression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeFieldReferenceExpression FieldName=\"DClass\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeFieldReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeFieldReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeFieldReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeCastExpression.Expression>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeCastExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">This Description</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule8\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">NC</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetFirstValue\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"SampleFlow.OtherEntity\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">AAA</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">BBB</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t</ns0:CodeExpressionStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule9\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">SC</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeExpressionStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression MethodName=\"SetCollection\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeMethodReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Method>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression CreateType=\"System.Collections.ObjectModel.Collection`1[[System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeObjectCreateExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression CreateType=\"System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\" Size=\"0\" SizeExpression=\"{p1:Null}\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeArrayCreateExpression.Initializers>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">AValue</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">BValue</ns1:String>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression.Initializers>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeArrayCreateExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression.Parameters>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodeObjectCreateExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression.Parameters>\r\n\t\t\t\t\t\t\t\t</ns0:CodeMethodInvokeExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeExpressionStatement.Expression>\r\n\t\t\t\t\t\t</ns0:CodeExpressionStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t\t<Rule Active=\"True\" Description=\"{p1:Null}\" Name=\"Rule10\" Priority=\"0\" ReevaluationBehavior=\"Always\">\r\n\t\t\t<Rule.Condition>\r\n\t\t\t\t<RuleExpressionCondition Name=\"{p1:Null}\">\r\n\t\t\t\t\t<RuleExpressionCondition.Expression>\r\n\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"State\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">GA</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression.Right>\r\n\t\t\t\t\t\t</ns0:CodeBinaryOperatorExpression>\r\n\t\t\t\t\t</RuleExpressionCondition.Expression>\r\n\t\t\t\t</RuleExpressionCondition>\r\n\t\t\t</Rule.Condition>\r\n\t\t\t<Rule.ThenActions>\r\n\t\t\t\t<RuleStatementAction>\r\n\t\t\t\t\t<RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t\t\t<ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"Property1\">\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"SecondClass\">\r\n\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression PropertyName=\"FirstClass\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<ns0:CodeThisReferenceExpression />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression.TargetObject>\r\n\t\t\t\t\t\t\t\t</ns0:CodePropertyReferenceExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Left>\r\n\t\t\t\t\t\t\t<ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t\t\t<ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t\t\t<ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">This Value</ns1:String>\r\n\t\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression.Value>\r\n\t\t\t\t\t\t\t\t</ns0:CodePrimitiveExpression>\r\n\t\t\t\t\t\t\t</ns0:CodeAssignStatement.Right>\r\n\t\t\t\t\t\t</ns0:CodeAssignStatement>\r\n\t\t\t\t\t</RuleStatementAction.CodeDomStatement>\r\n\t\t\t\t</RuleStatementAction>\r\n\t\t\t</Rule.ThenActions>\r\n\t\t</Rule>\r\n\t</RuleSet.Rules>\r\n</RuleSet>";
            //Act
            string rulesSetString = SerializeRules(ruleSet);
            //Assert
            Assert.Equal(rulesSetString, existing);
        }

        private Rule Rule_with_equals_condition_and_setter_action()
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

        private Rule Rule_with_array_indexer_condition_and_setter_action()
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


        private Rule Rule_with_method_condition()
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


        private Rule Rule_with_multiple_conditions()
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


        private Rule Rule_with_static_method_action()
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


        private Rule Rule_with_reference_method_action()
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


        private Rule Rule_with_Cast_object_expression_in_then_action()
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


        private Rule Rule_with_simple_CodeObjectCreateExpression_calling_a_constructor_in_then_action()
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

        private Rule Rule_with_list_initialization()
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

        private Rule Rule_with_child_and_granchild_reference()
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

        private void CreateRuleEngine()
        {
            ruleSet = new RuleSet
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

            RuleValidation ruleValidation = GetValidation(ruleSet, typeof(SampleFlow.FlowEntity));
            ruleEngine = new RuleEngine(ruleSet, ruleValidation);
        }

        private RuleSet ruleSet;
        private RuleEngine ruleEngine;

        private RuleValidation GetValidation(RuleSet ruleSet, Type type)
        {
            RuleValidation ruleValidation = null;

            if (ruleSet == null)
                throw new InvalidOperationException(Resources.ruleSetCannotBeNull);

            ruleValidation = new RuleValidation(type);
            if (ruleValidation == null)
                throw new InvalidOperationException(Resources.ruleValidationCannotBeNull);

            if (!ruleSet.Validate(ruleValidation))
            {
                InvalidOperationException ex = new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.invalidRuleSetFormat, ruleSet.Name));
                foreach (ValidationError validationError in ruleValidation.Errors)
                    ex.Data.Add(ex.Data.Count, validationError.ErrorText);

                throw ex;

            }
            return ruleValidation;
        }

        private string SerializeRules(object drs)
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
