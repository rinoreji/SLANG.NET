﻿
'http://en.wikipedia.org/wiki/Syntax_diagram
'http://en.wikipedia.org/wiki/Recursive_descent_parser

Public Class RDParser
    Inherits Lexer

    Dim _CurrToken As Tokens

    Sub New(expression As String)
        MyBase.New(expression)
    End Sub

    Public Function GetExpression(context As CompilationContext) As Expression
        _CurrToken = GetNextToken()
        Return Expression(context)
    End Function

    Private Function Expression(context As CompilationContext) As Expression
        Dim lastToken As Tokens
        Dim _expression = Term(context)

        While _CurrToken = Tokens.Plus Or _CurrToken = Tokens.Minus
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Expression(context)
            _expression = If(lastToken = Tokens.Plus,
                             New Add(_expression, nextExpression),
                             New Subtract(_expression, nextExpression))
        End While

        Return _expression
    End Function

    Private Function Term(context As CompilationContext) As Expression
        Dim lastToken As Tokens
        Dim _expression = Factor(context)

        While _CurrToken = Tokens.Div Or _CurrToken = Tokens.Mul
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            Dim nextExpression As Expression = Term(context)
            _expression = If(lastToken = Tokens.Div,
                             New Divide(_expression, nextExpression),
                             New Multiply(_expression, nextExpression))
        End While

        Return _expression
    End Function

    Private Function Factor(context As CompilationContext) As Expression
        Dim lastToken As Tokens
        Dim _expression As Expression

        If (_CurrToken = Tokens.Number) Then
            _expression = New NumericConstant(GetNumber())
            _CurrToken = GetNextToken()
        ElseIf _CurrToken = Tokens.StringValue Then
            _expression = New StringLiteral(GetString())
            _CurrToken = GetNextToken()
        ElseIf _CurrToken = Tokens.TrueValue Or _CurrToken = Tokens.FalseValue Then
            _expression = New BooleanConstant(If(_CurrToken = Tokens.TrueValue, True, False))
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Tokens.OpeningParenthesis) Then
            _CurrToken = GetNextToken()
            _expression = Expression(context)
            If (_CurrToken <> Tokens.ClosingParenthesis) Then
                Throw New Exception("Missing closing parenthesis")
            End If
            _CurrToken = GetNextToken()
        ElseIf (_CurrToken = Tokens.Minus Or _CurrToken = Tokens.Plus) Then
            lastToken = _CurrToken
            _CurrToken = GetNextToken()
            _expression = Factor(context)
            _expression = If(lastToken = Tokens.Plus,
                             New UnaryAdd(_expression),
                             New UnarySubtract(_expression))
        ElseIf _CurrToken = Tokens.UnquotedString Then
            Dim symbol = context.SymbolTable.GetSymbol(GetString())
            If (IsNothing(symbol)) Then
                Throw New Exception("Undefined symbol")
            End If
            _CurrToken = GetNextToken()
            _expression = New Variable(symbol)
        Else
            Throw New Exception("invalid token")
        End If
        Return _expression
    End Function

    Public Function Parse(context As CompilationContext) As Statement()
        _CurrToken = GetNextToken()
        Dim statements As ArrayList = New ArrayList()

        While (_CurrToken <> Tokens.EOE)
            statements.Add(ExtractStatement(context))
            _CurrToken = GetNextToken()
        End While

        Return statements.ToArray(GetType(Statement))
    End Function

    Private Function ExtractStatement(context As CompilationContext) As Statement
        Select Case _CurrToken
            Case Tokens.Var_Bool, Tokens.Var_Number, Tokens.Var_String
                Return GetVariableDeclarationStatement(context)
            Case Tokens.UnquotedString
                Return GetAssignmentStatement(context)
            Case Tokens.Print
                Return New Print(GetExpressionToPrint(context))
            Case Tokens.PrintLine
                Return New PrintLine(GetExpressionToPrint(context))
            Case Else
                Throw New Exception("Invalid statement")
        End Select
    End Function

    Private Function GetVariableDeclarationStatement(context As CompilationContext) As Statement
        Dim lastToken = _CurrToken
        _CurrToken = GetNextToken()
        If (_CurrToken = Tokens.UnquotedString) Then
            Dim symbol = New Symbol()
            symbol.Name = GetString()
            symbol.Type = If(lastToken = Tokens.Var_Bool, Types.BooleanType,
                             If(lastToken = Tokens.Var_Number,
                                Types.NumericType, Types.StringType))
            _CurrToken = GetNextToken()
            'semi col chk.
            'TODO: make it ok to allow string c="rrc"
            If (_CurrToken = Tokens.SemiColon) Then
                context.SymbolTable.Add(symbol) 'for type analysis
                Return New VariableDeclaration(symbol)
            Else
                Throw New Exception("missing ;")
            End If
        Else
            Throw New Exception("syntax error in variable declaration")
        End If
    End Function

    Private Function GetAssignmentStatement(context As CompilationContext) As Statement
        Dim value = GetString()
        Dim symbol = context.SymbolTable.GetSymbol(value)

        If (IsNothing(symbol)) Then
            Throw New Exception("Variable not found")
        End If
        _CurrToken = GetNextToken()
        If (_CurrToken <> Tokens.Assignment) Then
            Throw New Exception("Invalid statement, = expected")
        End If
        Dim expression = GetExpression(context)
        If (expression.TypeCheck(context) <> symbol.Type) Then
            Throw New Exception("Type mismatch in assignment")
        End If
        If (_CurrToken <> Tokens.SemiColon) Then
            Throw New Exception("; expected")
        End If
        '_CurrToken = GetNextToken()
        Return New Assignment(New Variable(symbol), expression)

    End Function

    Private Function GetExpressionToPrint(context As CompilationContext) As Expression
        Dim expression = GetExpression(context)
        If (_CurrToken <> Tokens.SemiColon) Then
            Throw New Exception("Invalid statement, missing ';'")
        End If
        Return expression
    End Function

End Class
