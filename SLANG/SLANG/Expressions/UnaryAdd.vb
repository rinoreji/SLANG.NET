Public Class UnaryAdd
    Inherits Expression
    Private _expression As Expression
    Sub New(expression As Expression)
        _expression = expression
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        Dim symbol = _expression.Evaluate(context)
        If (symbol.Type = Types.NumericType) Then
            Return New Symbol("", symbol.NumericValue)
        Else
            Throw New Exception("Type mismatch/invalid")
        End If
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        Dim type = _expression.TypeCheck(context)
        If (type = Types.NumericType) Then
            _type = type
            Return _type
        Else
            Throw New Exception("Type mismatch/invalid")
        End If
    End Function
End Class
