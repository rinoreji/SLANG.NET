
Public Class Lexer

    Dim _expression As String
    Dim _currIndex As Integer
    Dim _length As Integer
    Dim _number As Double

    Public Sub New(expression As String)
        _expression = expression
        _currIndex = 0
        _length = expression.Length
    End Sub

    Function GetNextToken() As Token
        MoveToValidIndex()
        If (_expression.Length = _currIndex) Then
            Return Token.EOE
        End If
        Dim charAtIndex = _expression(_currIndex)
        Select Case charAtIndex
            Case "+"c
                _currIndex += 1
                Return Token.Plus
            Case "-"c
                _currIndex += 1
                Return Token.Minus
            Case "/"c
                _currIndex += 1
                Return Token.Div
            Case "*"c
                _currIndex += 1
                Return Token.Mul
            Case "("c
                _currIndex += 1
                Return Token.OpeningParenthesis
            Case ")"c
                _currIndex += 1
                Return Token.ClosingParenthesis
            Case "."c, "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                Dim str As String = ""
                Dim tempChar As Char = charAtIndex
                While (_length > _currIndex AndAlso (charAtIndex = "."c Or Char.IsDigit(charAtIndex)))
                    str += _expression(_currIndex)
                    _currIndex += 1
                    If _length > _currIndex Then
                        charAtIndex = _expression(_currIndex)
                    End If
                End While
                _number = Convert.ToDouble(str)
                Return Token.Number
        End Select
        _currIndex += 1
        Throw New Exception("Error while parsing")
    End Function

    Private Sub MoveToValidIndex()
        While _length > 0 AndAlso
            _length > _currIndex AndAlso
            (_expression(_currIndex) = " "c Or _expression(_currIndex) = ControlChars.Tab)
            _currIndex += 1
        End While
    End Sub

    Public Function GetNumber() As Double
        Return _number
    End Function

End Class
