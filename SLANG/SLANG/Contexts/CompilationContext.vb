Public Class CompilationContext
    Sub New()
        _symbolTable = New SymbolTable()
    End Sub

    Private _symbolTable As SymbolTable
    Public Property SymbolTable() As SymbolTable
        Get
            Return _symbolTable
        End Get
        Set(ByVal value As SymbolTable)
            _symbolTable = value
        End Set
    End Property

End Class
