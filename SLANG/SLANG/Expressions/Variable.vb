Public Class Variable
    Inherits Expression

    Private _name As String
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Sub New(symbol As Symbol)
        _name = symbol.Name
    End Sub

    Public Sub New(context As CompilationContext, name As String, value As String)
        Dim _symbol = New Symbol(name, value)
        context.SymbolTable.Add(_symbol)
        _name = name
    End Sub

    Public Sub New(context As CompilationContext, name As String, value As Double)
        Dim _symbol = New Symbol(name, value)
        context.SymbolTable.Add(_symbol)
        _name = name
    End Sub

    Public Sub New(context As CompilationContext, name As String, value As Boolean)
        Dim _symbol = New Symbol(name, value)
        context.SymbolTable.Add(_symbol)
        _name = name
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        Return context.SymbolTable.GetSymbol(_name)
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        Dim symbol = context.SymbolTable.GetSymbol(_name)
        If Not IsNothing(symbol) Then
            _type = symbol.Type
            Return _type
        End If
        Return Types.Illegal
    End Function
End Class
