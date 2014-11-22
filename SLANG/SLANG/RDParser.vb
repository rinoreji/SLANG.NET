
'http://en.wikipedia.org/wiki/Syntax_diagram
'http://en.wikipedia.org/wiki/Recursive_descent_parser

Public Class RDParser
    Inherits Lexer

    Dim _CurrToken As Token

    Sub New(expression As String)
        MyBase.New(expression)
    End Sub

    Public Function GetExpression() As Expression
        _CurrToken = GetNextToken()
        Return Expression()
    End Function


    Function Expression() As Expression
        Dim lastToken As Token
        Dim _expression = Term()

        While _CurrToken = Token.Plus Or _CurrToken = Token.Minus
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Expression()
            _expression = New BinaryExpression(_expression, nextExpression, If(lastToken = Token.Plus, Operators.Plus, Operators.Minus))
        End While

        Return _expression
    End Function

    Function Term() As Expression
        Dim lastToken As Token
        Dim _expression = Factor()

        While _CurrToken = Token.Div Or _CurrToken = Token.Mul
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Term()
            _expression = New BinaryExpression(_expression, nextExpression, If(lastToken = Token.Div, Operators.Div, Operators.Mul))
        End While

        Return _expression
    End Function

    Function Factor() As Expression
        Dim lastToken As Token
        Dim _expression As Expression

        If (_CurrToken = Token.Number) Then
            _expression = New NumericConstant(GetNumber())
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Token.OpeningParenthesis) Then
            _CurrToken = GetNextToken()
            _expression = Expression()
            If (_CurrToken <> Token.ClosingParenthesis) Then
                Throw New Exception("Missing closing parenthesis")
            End If
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Token.Minus Or _CurrToken = Token.Plus) Then
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            _expression = Factor()
            _expression = New UnaryExpression(_expression, If(lastToken = Token.Plus, Operators.Plus, Operators.Minus))
        Else
            Throw New Exception("invalid token")
        End If
        Return _expression
    End Function
End Class
