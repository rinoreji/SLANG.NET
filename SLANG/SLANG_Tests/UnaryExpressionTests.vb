Imports SLANG

<TestClass> Public Class UnaryExpressionTests
    <TestMethod()> Public Sub ExtendsFromExpression()
        Dim target = New UnaryExpression(Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod> Public Sub RetunsNan_UnSupportedOperator()
        Dim target = New UnaryExpression(Nothing, Operators.Div)
        Assert.IsTrue(Double.IsNaN(target.Evaluate(Nothing)))

        target = New UnaryExpression(Nothing, Operators.Mul)
        Assert.IsTrue(Double.IsNaN(target.Evaluate(Nothing)))
    End Sub

    <TestMethod> Public Sub RetunsNumber_OperatorPlus()
        Dim value = New Random().NextDouble()

        Dim target = New UnaryExpression(New NumericConstant(value), Operators.Plus)

        Assert.AreEqual(value, target.Evaluate(Nothing))
    End Sub

    <TestMethod> Public Sub RetunsNegetiveNumber_OperatorMin()
        Dim value = New Random().NextDouble()

        Dim target = New UnaryExpression(New NumericConstant(value), Operators.Minus)

        Assert.AreEqual(value * -1, target.Evaluate(Nothing))
    End Sub
End Class
