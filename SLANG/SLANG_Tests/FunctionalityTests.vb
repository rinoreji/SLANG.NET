Imports SLANG

<TestClass> Public Class FunctionalityTests

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

    <TestMethod>
    Public Sub RDParserTest_ParsesAndEvauateProperly()
        Dim target = New RDParser("-((1+2)+(2*3)-3)")
        Dim expression = target.GetExpression()
        Dim result = expression.Evaluate(Nothing)
        Assert.AreEqual(-6.0, result)
        Assert.AreEqual("Minus 1 Plus 2 Plus 2 Mul 3 Minus 3", expression.ToString())
    End Sub

    <TestMethod>
    Sub RDParsetTest_ParseStatementsAndExecutesProperly()
        Dim rand = New Random()
        Dim value0 = rand.Next(100)
        Dim value1 = rand.Next(100)

        Dim text = String.Format("printline {0}+{1}; printline {0}+({0}*{1})-{1};", value0, value1)
        Dim result As String = ""
        Dim target = New RDParser(text)
        Dim expressions = target.Parse()

        For Each expresion In expressions
            CType(expresion, PrintStatement).Write = Sub(value As String)
                                                         result += value
                                                     End Sub
            expresion.Execute(Nothing)
        Next

        Dim expected As String = (value0 + value1).ToString() + (value0 + (value0 * value1) - value1).ToString()
        Assert.AreEqual(expected, result)
    End Sub

End Class
