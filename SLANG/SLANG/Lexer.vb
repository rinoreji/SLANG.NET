
Public Class Lexer

    Dim _expression As String
    Dim _currIndex As Integer
    Dim _length As Integer
    Dim _number As Double
    Dim _valueTable() As ValueTable

    Public Sub New(expression As String)
        _expression = expression
        _currIndex = 0
        _length = expression.Length
        InitValueTable()

    End Sub

    Private Sub InitValueTable()
        _valueTable = New ValueTable() {New ValueTable(Tokens.Print, "PRINT"),
                                         New ValueTable(Tokens.PrintLine, "PRINTLINE")}
    End Sub

    Function GetNextToken() As Tokens
        MoveToValidIndex()
        If (_expression.Length = _currIndex) Then
            Return Tokens.EOE
        End If
        Dim charAtIndex = _expression(_currIndex)
        Select Case charAtIndex
            Case "+"c
                _currIndex += 1
                Return Tokens.Plus
            Case "-"c
                _currIndex += 1
                Return Tokens.Minus
            Case "/"c
                _currIndex += 1
                Return Tokens.Div
            Case "*"c
                _currIndex += 1
                Return Tokens.Mul
            Case "("c
                _currIndex += 1
                Return Tokens.OpeningParenthesis
            Case ")"c
                _currIndex += 1
                Return Tokens.ClosingParenthesis
            Case ";"c
                _currIndex += 1
                Return Tokens.SemiColon
            Case "."c, "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                Dim str As String = ""
                While (_length > _currIndex AndAlso (charAtIndex = "."c Or Char.IsDigit(charAtIndex)))
                    str += charAtIndex
                    _currIndex += 1
                    If _length > _currIndex Then
                        charAtIndex = _expression(_currIndex)
                    End If
                End While
                _number = Convert.ToDouble(str)
                Return Tokens.Number
            Case Else
                If (Char.IsLetter(charAtIndex)) Then
                    Dim str As String = ""
                    While (_length > _currIndex AndAlso (charAtIndex = "_"c Or Char.IsLetterOrDigit(charAtIndex)))
                        str += charAtIndex
                        _currIndex += 1
                        If _length > _currIndex Then
                            charAtIndex = _expression(_currIndex)
                        End If
                    End While

                    str = str.ToUpper()
                    For index = 0 To _valueTable.Length - 1
                        If (_valueTable(index).Value = str) Then
                            Return _valueTable(index).Token
                        End If
                    Next
                End If
        End Select
        _currIndex += 1
        Throw New Exception("Error while parsing")
    End Function

    Private Sub MoveToValidIndex()
        While _length > 0 AndAlso
            _length > _currIndex AndAlso
            (_expression(_currIndex) = " "c Or
             _expression(_currIndex) = ControlChars.Tab Or
             _expression(_currIndex) = ControlChars.Cr Or
             _expression(_currIndex) = ControlChars.Lf)
            _currIndex += 1
        End While
    End Sub

    Public Function GetNumber() As Double
        Return _number
    End Function

End Class
