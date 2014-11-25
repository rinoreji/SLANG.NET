Public Class VariableDeclaration
    Inherits Statement
    Private _symbol As Symbol
    Sub New(symbol As Symbol)
        _symbol = symbol
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Symbol
        context.SymbolTable.Add(_symbol)
        Return Nothing
    End Function
End Class
