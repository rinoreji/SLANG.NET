Imports SLANG
<TestClass>
Public Class CompilationContextTests

    <TestMethod>
    Sub SymbolTable_PropertyTest()
        Dim target = New CompilationContext()
        Dim expectedTable = New SymbolTable()

        Assert.IsNotNull(target.SymbolTable)

        target.SymbolTable = expectedTable

        Assert.AreSame(expectedTable, target.SymbolTable)
    End Sub

End Class
