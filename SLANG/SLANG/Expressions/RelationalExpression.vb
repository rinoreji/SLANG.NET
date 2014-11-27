
Public Class RelationalExpression
    Inherits Expression

    Dim _expression1 As Expression
    Dim _expression2 As Expression
    Dim _operator As Operators

    Sub New(expression1 As Expression, expression2 As Expression, optr As Operators)
        _expression1 = expression1
        _expression2 = expression2
        _operator = optr
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        Dim left = _expression1.Evaluate(context)
        Dim right = _expression2.Evaluate(context)

        Dim symbol As Symbol = New Symbol()
        symbol.Type = Types.BooleanType
        symbol.Name = ""
        If (left.Type = right.Type) Then
            If (left.Type = Types.NumericType) Then
                Select Case _operator
                    Case Operators.Equals
                        symbol.BoolValue = left.NumericValue = right.NumericValue
                    Case Operators.NotEquals
                        symbol.BoolValue = left.NumericValue <> right.NumericValue
                    Case Operators.LessThan
                        symbol.BoolValue = left.NumericValue < right.NumericValue
                    Case Operators.LessThanEqual
                        symbol.BoolValue = left.NumericValue <= right.NumericValue
                    Case Operators.GreaterThan
                        symbol.BoolValue = left.NumericValue > right.NumericValue
                    Case Operators.GreaterThanEqual
                        symbol.BoolValue = left.NumericValue >= right.NumericValue
                    Case Else
                        symbol.BoolValue = False
                End Select
                Return symbol
            ElseIf (left.Type = Types.StringType) Then
                Select Case _operator
                    Case Operators.Equals
                        symbol.BoolValue = If(String.Compare(left.StringValue, right.StringValue) = 0, True, False)
                    Case Operators.NotEquals
                        symbol.BoolValue = If(String.Compare(left.StringValue, right.StringValue) <> 0, True, False)
                    Case Else
                        symbol.BoolValue = False
                End Select
                Return symbol
            ElseIf (left.Type = Types.BooleanType) Then
                Select Case _operator
                    Case Operators.Equals
                        symbol.BoolValue = left.BoolValue = right.BoolValue
                    Case Operators.NotEquals
                        symbol.BoolValue = left.BoolValue <> right.BoolValue
                    Case Else
                        symbol.BoolValue = False
                End Select
                Return symbol
            End If
        Else
            Return Nothing
        End If
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        Dim left = _expression1.TypeCheck(context)
        Dim right = _expression2.TypeCheck(context)

        If (left <> right) Then
            Throw New Exception("Type mismatch")
        End If
        If (left = Types.BooleanType Or left = Types.StringType) Then
            If (_operator <> Operators.Equals AndAlso _operator <> Operators.NotEquals) Then
                Throw New Exception("Only == and != operations are valid on this type")
            End If
        End If
        _type = Types.BooleanType
        Return _type
    End Function
End Class