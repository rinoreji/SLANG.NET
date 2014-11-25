Public Class Assignment
    Inherits Statement
    Private _expression As Expression
    Private _variable As Variable

    Sub New(variable As Variable, expression As Expression)
        _variable = variable
        _expression = expression
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Symbol
        Dim symbol = _expression.Evaluate(context)
        context.SymbolTable.Assign(_variable.Name, symbol)
        Return Nothing
    End Function
End Class
