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

        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(Convert.ToDouble(expectedNumber), target.GetNumber())
        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_InvalidToken()
        Dim target = New RDParser("print invalid")

        Try
            target.Parse()
        Catch ex As Exception
            Assert.AreEqual("Error while parsing", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_()
        Dim target = New RDParser("print 1+2 ; Printline 2;")

        Dim result = target.Parse()

        Assert.IsInstanceOfType(result(0), GetType(PrintStatement))
        Assert.IsInstanceOfType(result(1), GetType(PrintLineStatement))
    End Sub



End Class
