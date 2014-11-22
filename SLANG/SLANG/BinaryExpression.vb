Public Class BinaryExpression
    Inherits Expression

    Dim _expression1 As Expression
    Dim _expression2 As Expression
    Dim _operator As Operators

    Public Sub New(exp1 As Expression, exp2 As Expression, optr As Operators)
        _expression1 = exp1
        _expression2 = exp2
        _operator = optr
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Double

        Select Case _operator
            Case Operators.Plus
                Return _expression1.Evaluate(context) + _expression2.Evaluate(context)
            Case Operators.Minus
                Return _expression1.Evaluate(context) - _expression2.Evaluate(context)
            Case Operators.Div
                Return _expression1.Evaluate(context) / _expression2.Evaluate(context)
            Case Operators.Mul
                Return _expression1.Evaluate(context) * _expression2.Evaluate(context)
        End Select

        Return Double.NaN
    End Function

    Public Overrides Function ToString() As String
        Return String.Format("{0} {1} {2}", _expression1, _operator, _expression2)
    End Function
End Class
