
Public Class Lexer

    Dim _expression As String
    Dim _currIndex As Integer
    Dim _length As Integer
    Dim _number As Double
    Dim _valueTable() As ValueTable
    Dim _lastStringValue As String = ""

    Public Sub New(expression As String)
        _expression = expression
        _currIndex = 0
        _length = expression.Length
        InitValueTable()

    End Sub

    Private Sub InitValueTable()
        _valueTable = New ValueTable() {New ValueTable(Tokens.Print, "PRINT"),
                                        New ValueTable(Tokens.PrintLine, "PRINTLINE"),
                                        New ValueTable(Tokens.TrueValue, "TRUE"),
                                        New ValueTable(Tokens.FalseValue, "FALSE"),
                                        New ValueTable(Tokens.Var_Number, "NUMBER"),
                                        New ValueTable(Tokens.Var_Bool, "BOOL"),
                                        New ValueTable(Tokens.Var_String, "STRING"),
                                        New ValueTable(Tokens.IfToken, "IF"),
                                        New ValueTable(Tokens.ElseToken, "ELSE"),
                                        New ValueTable(Tokens.EndIfToken, "ENDIF"),
                                        New ValueTable(Tokens.WhileToken, "WHILE"),
                                        New ValueTable(Tokens.EndWhileToken, "WEND"),
                                        New ValueTable(Tokens.ThenToken, "THEN")
                                        }
    End Sub

    Private Sub MoveToEndOfLine()
        While _currIndex < _length AndAlso _expression(_currIndex) <> ControlChars.Cr
            _currIndex += 1
        End While
        If (_currIndex = _length) Then
            Return
        ElseIf (_expression(_currIndex + 1) = ControlChars.Lf) Then
            _currIndex += 2
            Return
        End If
        _currIndex += 1
        Return
    End Sub

    Function GetNextToken() As Tokens
re_start:
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
                If (_expression(_currIndex) = "/"c) Then
                    'from here till line end its commented, 
                    'so move index to next line
                    MoveToEndOfLine()
                    GoTo re_start
                Else
                    Return Tokens.Div
                End If
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
            Case "&"c
                _currIndex += 1
                If (_expression(_currIndex) = "&"c) Then
                    _currIndex += 1
                    Return Tokens.LogicalAnd
                End If
            Case "|"c
                _currIndex += 1
                If (_expression(_currIndex) = "|"c) Then
                    _currIndex += 1
                    Return Tokens.LogicalOr
                End If
            Case "!"c
                _currIndex += 1
                Return Tokens.LogicalNot
            Case "="c
                _currIndex += 1
                If (_expression(_currIndex) = "="c) Then
                    _currIndex += 1
                    Return Tokens.Equals
                End If
                Return Tokens.Assignment
            Case ">"c
                _currIndex += 1
                If (_expression(_currIndex) = "="c) Then
                    _currIndex += 1
                    Return Tokens.GreaterThanEqual
                End If
                Return Tokens.GreaterThan
            Case "<"c
                _currIndex += 1
                If (_expression(_currIndex) = "="c) Then
                    _currIndex += 1
                    Return Tokens.LessThanEqual
                ElseIf (_expression(_currIndex) = ">"c) Then
                    _currIndex += 1
                    Return Tokens.NotEquals
                End If
                Return Tokens.LessThan
            Case """"c
                _currIndex += 1
                Dim str As String = ""
                While _currIndex < _length AndAlso _expression(_currIndex) <> """"c
                    str += _expression(_currIndex)
                    _currIndex += 1
                End While
                If _currIndex = _length Then
                    Return Tokens.Illegal
                Else
                    _currIndex += 1
                    _lastStringValue = str
                    Return Tokens.StringValue
                End If

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

                    For index = 0 To _valueTable.Length - 1
                        If (_valueTable(index).Value = str.ToUpper()) Then
                            Return _valueTable(index).Token
                        End If
                    Next
                    _lastStringValue = str
                    Return Tokens.UnquotedString
                Else
                    Return Tokens.Illegal
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

    Public Function GetString() As String
        Return _lastStringValue
    End Function

End Class
