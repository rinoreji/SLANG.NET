Public Class StringLiteral
    Inherits Expression
    Private _symbol As Symbol

    Public Sub New(value As String)
        _symbol = New Symbol(Nothing, value)
        _type = _symbol.Type
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        Return _symbol
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        Return _type
    End Function
End Class
