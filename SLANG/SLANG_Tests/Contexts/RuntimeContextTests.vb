Imports SLANG
<TestClass>
Public Class RuntimeContextTests

    <TestMethod>
    Sub SymbolTable_PropertyTest()
        Dim target = New RuntimeContext()
        Dim expectedTable = New SymbolTable()

        Assert.IsNotNull(target.SymbolTable)

        target.SymbolTable = expectedTable

        Assert.AreSame(expectedTable, target.SymbolTable)
    End Sub

End Class
