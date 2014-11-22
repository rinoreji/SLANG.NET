Public Class UnaryExpression
    Inherits Expression

    Dim _expression As Expression
    Dim _operator As Operators

    Sub New(expression As Expression, optr As Operators)
        _expression = expression
        _operator = optr
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Double
        Select Case _operator
            Case Operators.Plus
                Return _expression.Evaluate(context)
            Case Operators.Minus
                Return -_expression.Evaluate(context)
        End Select
        Return Double.NaN
    End Function

End Class
