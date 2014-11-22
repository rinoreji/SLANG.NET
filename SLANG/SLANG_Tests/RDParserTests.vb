Imports SLANG

<TestClass>
Public Class RDParserTests

    <TestMethod>
    Public Sub ConstructorTest_IsLexer()
        Dim target = New RDParser("")

        Assert.IsInstanceOfType(target, GetType(Lexer))
    End Sub

    <TestMethod>
    Public Sub ConstructorTest_PassExpressionToBase()
        Dim expectedNumber = (New Random().NextDouble() * New Random().Next()).ToString()

        Dim target = New RDParser(expectedNumber)

        Assert.AreEqual(Token.Number, target.GetNextToken())
        Assert.AreEqual(Convert.ToDouble(expectedNumber), target.GetNumber())
        Assert.AreEqual(Token.EOE, target.GetNextToken())
    End Sub

End Class
