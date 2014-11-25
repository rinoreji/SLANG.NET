Imports SLANG

<TestClass>
Public Class VariableDeclarationTests
    <TestMethod>
    Sub InheritedFromStatement()
        Dim target = New VariableDeclaration(Nothing)

        Assert.IsInstanceOfType(target, GetType(VariableDeclaration))
    End Sub

    <TestMethod>
    Sub ExecuteTest_AddsSymbolToSymbolTable()
        Dim context = New RuntimeContext()
        Dim symbol = New Symbol("key", True)

        Dim target = New VariableDeclaration(symbol)

        Assert.IsNull(target.Execute(context))
        Assert.AreSame(symbol, context.SymbolTable.GetSymbol(symbol.Name))
    End Sub

End Class
