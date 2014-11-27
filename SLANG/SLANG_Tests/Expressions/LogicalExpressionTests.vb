Imports SLANG
<TestClass>
Public Class LogicalExpressionTests
    <TestMethod()>
    Sub ExtendsFromExpression()
        Dim target = New LogicalExpression(Nothing, Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnNull_IfNotBooleanType()
        Dim target = New LogicalExpression(New NumericConstant(1), New NumericConstant(2), Tokens.LogicalAnd)

        Assert.IsNull(target.Evaluate(New RuntimeContext()))
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnTrue_InputTrueAndTrue()
        Dim target = New LogicalExpression(New BooleanConstant(True), New BooleanConstant(True), Tokens.LogicalAnd)

        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnFalse_InputTrueAndFalse()
        Dim target = New LogicalExpression(New BooleanConstant(True), New BooleanConstant(False), Tokens.LogicalAnd)

        Dim result = target.Evaluate(New RuntimeContext())

        Assert.AreEqual("", result.Name)
        Assert.AreEqual(Types.BooleanType, result.Type)
        Assert.IsFalse(result.BoolValue)
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnFalse_InputTrueOrFalse()
        Dim target = New LogicalExpression(New BooleanConstant(True), New BooleanConstant(False), Tokens.LogicalOr)

        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub


    <TestMethod>
    Sub EvaluateTest_ReturnNull_InvalidOperation()
        Dim target = New LogicalExpression(New BooleanConstant(True), New BooleanConstant(True), Tokens.Minus)

        Assert.IsNull(target.Evaluate(New RuntimeContext()))
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnNotValue_OperatorNot()
        Dim target = New LogicalExpression(New BooleanConstant(False), Nothing, Tokens.LogicalNot)

        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub

    <TestMethod>
    Sub TypeCheck_ThrowsException_InvalidType()
        Dim target = New LogicalExpression(New NumericConstant(10), New NumericConstant(23), Tokens.Comment)

        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Invalid type", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub TypeCheck_ReturnsBoolean_BooleanType()
        Dim target = New LogicalExpression(New BooleanConstant(True), New BooleanConstant(True), Tokens.LogicalNot)

        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
    End Sub

    <TestMethod>
    Sub TypeCheck_LogicalNot()
        Dim target = New LogicalExpression(New BooleanConstant(True), Nothing, Tokens.LogicalNot)

        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
    End Sub
End Class
