Imports SLANG

<TestClass>
Public Class LexerTests
    <TestMethod>
    Public Sub GetTokenTest_ReturnNullTokenForEmptyInput()
        Dim target = New Lexer("")

        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Public Sub GetTokenTest_ReturnAsExpected()
        Dim expected = New List(Of Tokens) From {Tokens.Minus, Tokens.OpeningParenthesis, Tokens.Number,
                                                Tokens.Mul, Tokens.Number, Tokens.ClosingParenthesis,
                                                Tokens.Plus, Tokens.Number, Tokens.EOE}
        Dim target = New Lexer("- (2* 2 ) + 10 ")

        For Each item As Tokens In expected
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

        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(Convert.ToDouble(expectedNumber), target.GetNumber())
        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Sub GetNextTokenTest_SkipsCrLfInExpression()
        Dim target = New Lexer(String.Format("{0}{1}{2}1{0}{2}2",
                                             ControlChars.Cr,
                                             ControlChars.NewLine,
                                             ControlChars.Tab))

        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(1.0, target.GetNumber())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(2.0, target.GetNumber())
    End Sub

    <TestMethod>
    Sub GetNextTokenTest_ReturnsTokenFromValueTable()
        Dim target = New Lexer("Printline ; 1 Print 2")

        Assert.AreEqual(Tokens.PrintLine, target.GetNextToken())
        Assert.AreEqual(Tokens.SemiColon, target.GetNextToken())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(1.0, target.GetNumber())
        Assert.AreEqual(Tokens.Print, target.GetNextToken())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(2.0, target.GetNumber())
    End Sub

    <TestMethod>
    Sub GetNextTokenTest_ReturnsStringsAndAssignments()
        Dim varName = "variableName_With_No_Digits"
        Dim value = Guid.NewGuid().ToString() + " " + Guid.NewGuid().ToString()

        Dim target = New Lexer(String.Format("Printline ; datatype {0}=""{1}"";Print 1;", varName, value))

        Assert.AreEqual(Tokens.PrintLine, target.GetNextToken())
        Assert.AreEqual(Tokens.SemiColon, target.GetNextToken())
        Assert.AreEqual(Tokens.UnquotedString, target.GetNextToken())
        Assert.AreEqual("datatype", target.GetString())
        Assert.AreEqual(Tokens.UnquotedString, target.GetNextToken())
        Assert.AreEqual(varName, target.GetString())
        Assert.AreEqual(Tokens.Assignment, target.GetNextToken())
        Assert.AreEqual(Tokens.StringValue, target.GetNextToken())
        Assert.AreEqual(value, target.GetString())
        Assert.AreEqual(Tokens.SemiColon, target.GetNextToken())
        Assert.AreEqual(Tokens.Print, target.GetNextToken())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(1.0, target.GetNumber())
        Assert.AreEqual(Tokens.SemiColon, target.GetNextToken())
        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Sub GetNextTokenTest_HandlesComments()
        Dim sourceCode = String.Format("//comment goes here. {0}Print 1;{0}//nextComment{1}{2}Print 2", ControlChars.NewLine, ControlChars.Cr, ControlChars.Lf)

        Dim target = New Lexer(sourceCode)

        Assert.AreEqual(Tokens.Print, target.GetNextToken())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(1.0, target.GetNumber())
        Assert.AreEqual(Tokens.SemiColon, target.GetNextToken())
        Assert.AreEqual(Tokens.Print, target.GetNextToken())
        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(2.0, target.GetNumber())
        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub
End Class
