Imports SLANG

<TestClass> Public Class SLANGTests
    <TestMethod> Sub SyntaxTreeTest_BinaryMultiplication()
        '// Abstract Syntax Tree (AST) for x*y
        Dim value1 = New Random().NextDouble()
        Dim value2 = New Random().NextDouble()

        Dim expression = New BinaryExpression(New NumericConstant(value1),
                                              New NumericConstant(value2),
                                              Operators.Mul)

        Assert.AreEqual(value1 * value2, expression.Evaluate(Nothing))
    End Sub

    <TestMethod>
    Sub SyntaxTreeTest_ComplexOperation1()
        'AST for  (x + (y + z ) )
        Dim random = New Random()
        Dim xVal = random.NextDouble()
        Dim yVal = random.NextDouble()
        Dim zVal = random.NextDouble()

        Dim innerExpression = New BinaryExpression(
                              New NumericConstant(yVal),
                              New NumericConstant(zVal),
                              Operators.Plus)

        Dim outerExpression = New BinaryExpression(
                              New NumericConstant(xVal),
                              innerExpression,
                              Operators.Plus)

        Assert.AreEqual((xVal + (yVal + zVal)), outerExpression.Evaluate(Nothing))
    End Sub

End Class
