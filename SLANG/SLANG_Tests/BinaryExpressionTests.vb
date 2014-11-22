Imports SLANG

<TestClass> Public Class BinaryExpressionTests
    <TestMethod> Public Sub ExtendsFromExpression()
        Dim target = New BinaryExpression(Nothing, Nothing, Operators.Illegal)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod> Public Sub EvaluateReturn_NanValue_InvalidOperator()
        Dim target = New BinaryExpression(Nothing, Nothing, Operators.Illegal)

        Assert.IsTrue(Double.IsNaN(target.Evaluate(Nothing)))
    End Sub

    <TestMethod, ExpectedException(GetType(NullReferenceException))>
    Public Sub ThrowsExceptionIfExpressionsNull()
        Dim target = New BinaryExpression(Nothing, Nothing, Operators.Plus)

        Dim result = target.Evaluate(Nothing)
    End Sub

    <TestMethod>
    Public Sub ReturnSumOfExpression_OperatorPlus()
        Dim param1 = New Random().NextDouble()
        Dim param2 = New Random().NextDouble()

        Dim target = New BinaryExpression(New NumericConstant(param1), New NumericConstant(param2), Operators.Plus)

        Assert.AreEqual(param1 + param2, target.Evaluate(Nothing))
    End Sub

    <TestMethod>
    Public Sub ReturnSumOfExpression_OperatorMinus()
        Dim param1 = New Random().NextDouble()
        Dim param2 = New Random().NextDouble()

        Dim target = New BinaryExpression(New NumericConstant(param1), New NumericConstant(param2), Operators.Minus)

        Assert.AreEqual(param1 - param2, target.Evaluate(Nothing))
    End Sub

    <TestMethod>
    Public Sub ReturnSumOfExpression_OperatorDiv()
        Dim param1 = New Random().NextDouble()
        Dim param2 = New Random().NextDouble()

        Dim target = New BinaryExpression(New NumericConstant(param1), New NumericConstant(param2), Operators.Div)

        Assert.AreEqual(param1 / param2, target.Evaluate(Nothing))
    End Sub

    <TestMethod>
    Public Sub ReturnSumOfExpression_OperatorMul()
        Dim param1 = New Random().NextDouble()
        Dim param2 = New Random().NextDouble()

        Dim target = New BinaryExpression(New NumericConstant(param1), New NumericConstant(param2), Operators.Mul)

        Assert.AreEqual(param1 * param2, target.Evaluate(Nothing))
    End Sub

End Class
