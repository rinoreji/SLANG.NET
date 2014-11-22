Imports SLANG

<TestClass>
Public Class LexerTests
    <TestMethod>
    Public Sub GetTokenTest_ReturnNullTokenForEmptyInput()
        Dim target = New Lexer("")

        Assert.AreEqual(Token.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Public Sub GetTokenTest_ReturnAsExpected()
        Dim expected = New List(Of Token) From {Token.Minus, Token.OpeningParenthesis, Token.Number,
                                                Token.Mul, Token.Number, Token.ClosingParenthesis,
                                                Token.Plus, Token.Number, Token.EOE}
        Dim target = New Lexer("- (2* 2 ) + 10 ")

        For Each item As Token In expected
            Assert.AreEqual(item, target.GetNextToken())
        Next
    End Sub

    <TestMethod>
    Public Sub GetTokenTest_ThrowsExceptionOnInvalidToken()
        Dim target = New Lexer("1+2&3")
        For index = 1 To 6
            If (index = 4) Then
                Try
                    target.GetNextToken()
                Catch ex As Exception
                    Assert.AreEqual("Error while parsing", ex.Message)
                End Try
            Else
                target.GetNextToken()
            End If
        Next
    End Sub

    <TestMethod>
    Public Sub GetNumberTest_GetNumberOnNumberToken()
        Dim expectedNumber = (New Random().NextDouble() * New Random().Next()).ToString()

        Dim target = New Lexer(expectedNumber)

        Assert.AreEqual(Token.Number, target.GetNextToken())
        Assert.AreEqual(Convert.ToDouble(expectedNumber), target.GetNumber())
        Assert.AreEqual(Token.EOE, target.GetNextToken())
    End Sub
End Class
