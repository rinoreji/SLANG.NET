Public Class LogicalExpression
    Inherits Expression

    Private _expression1 As Expression
    Private _expression2 As Expression
    Private _operator As Tokens


    Sub New(expression1 As Expression, expression2 As Expression, optr As Tokens)
        _expression1 = expression1
        _expression2 = expression2
        _operator = optr
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        Dim symbol = New Symbol() With {.Name = "", .Type = Types.BooleanType}

        Dim left = _expression1.Evaluate(context)
        If (left.Type = Types.BooleanType AndAlso _operator = Tokens.LogicalNot) Then
            symbol.BoolValue = Not left.BoolValue
            Return symbol
        End If

        Dim right = _expression2.Evaluate(context)

        If (left.Type = right.Type AndAlso left.Type = Types.BooleanType) Then
            Select Case _operator
                Case Tokens.LogicalAnd
                    symbol.BoolValue = left.BoolValue And right.BoolValue
                Case Tokens.LogicalOr
                    symbol.BoolValue = left.BoolValue Or right.BoolValue
                Case Else
                    Return Nothing
            End Select
            Return symbol
        End If
        Return Nothing
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        Dim left = _expression1.TypeCheck(context)
        If (_operator = Tokens.LogicalNot AndAlso left = Types.BooleanType) Then
            _type = Types.BooleanType
            Return _type
        End If
        Dim right = _expression2.TypeCheck(context)

        If (left = right AndAlso left = Types.BooleanType) Then
            _type = Types.BooleanType
            Return _type
        Else
            Throw New Exception("Invalid type")
        End If
    End Function
End Class
