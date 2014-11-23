
'http://en.wikipedia.org/wiki/Syntax_diagram
'http://en.wikipedia.org/wiki/Recursive_descent_parser

Public Class RDParser
    Inherits Lexer

    Dim _CurrToken As Tokens

    Sub New(expression As String)
        MyBase.New(expression)
    End Sub

    Public Function GetExpression() As Expression
        _CurrToken = GetNextToken()
        Return Expression()
    End Function

    Private Function Expression() As Expression
        Dim lastToken As Tokens
        Dim _expression = Term()

        While _CurrToken = Tokens.Plus Or _CurrToken = Tokens.Minus
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Expression()
            _expression = New BinaryExpression(_expression, nextExpression, If(lastToken = Tokens.Plus, Operators.Plus, Operators.Minus))
        End While

        Return _expression
    End Function

    Private Function Term() As Expression
        Dim lastToken As Tokens
        Dim _expression = Factor()

        While _CurrToken = Tokens.Div Or _CurrToken = Tokens.Mul
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Term()
            _expression = New BinaryExpression(_expression, nextExpression, If(lastToken = Tokens.Div, Operators.Div, Operators.Mul))
        End While

        Return _expression
    End Function

    Private Function Factor() As Expression
        Dim lastToken As Tokens
        Dim _expression As Expression

        If (_CurrToken = Tokens.Number) Then
            _expression = New NumericConstant(GetNumber())
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Tokens.OpeningParenthesis) Then
            _CurrToken = GetNextToken()
            _expression = Expression()
            If (_CurrToken <> Tokens.ClosingParenthesis) Then
                Throw New Exception("Missing closing parenthesis")
            End If
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Tokens.Minus Or _CurrToken = Tokens.Plus) Then
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            _expression = Factor()
            _expression = New UnaryExpression(_expression, If(lastToken = Tokens.Plus, Operators.Plus, Operators.Minus))
        Else
            Throw New Exception("invalid token")
        End If
        Return _expression
    End Function

    Public Function Parse() As Statement()
        _CurrToken = GetNextToken()
        Dim statements As ArrayList = New ArrayList()

        While (_CurrToken <> Tokens.EOE)
            statements.Add(ExtractStatement())
            _CurrToken = GetNextToken()
        End While

        Return statements.ToArray(GetType(Statement))
    End Function

    Private Function ExtractStatement() As Statement
        Select Case _CurrToken
            Case Tokens.Print
                Return New PrintStatement(GetExpressionToPrint())
            Case Tokens.PrintLine
                Return New PrintLineStatement(GetExpressionToPrint())
            Case Else
                Throw New Exception("Invalid statement")
        End Select
    End Function

    Private Function GetExpressionToPrint() As Expression
        Dim expression = GetExpression()
        If (_CurrToken <> Tokens.SemiColon) Then
            Throw New Exception("Invalid statement, missing ';'")
        End If
        Return expression
    End Function

End Class
