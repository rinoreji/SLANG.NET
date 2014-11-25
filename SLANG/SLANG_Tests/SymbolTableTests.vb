Imports SLANG
<TestClass>
Public Class SymbolTableTests
    <TestMethod>
    Sub AndAndGetTest()
        Dim target = New SymbolTable()
        Dim expectedSymbol1 = New Symbol()
        expectedSymbol1.Name = Guid.NewGuid().ToString()
        Dim expectedSymbol2 = New Symbol()
        expectedSymbol2.Name = Guid.NewGuid().ToString()

        target.Add(expectedSymbol1)
        target.Add(expectedSymbol2)

        Assert.AreSame(expectedSymbol2, target.GetSymbol(expectedSymbol2.Name))
        Assert.AreSame(expectedSymbol1, target.GetSymbol(expectedSymbol1.Name))
    End Sub

    <TestMethod>
    Sub AssignTest_ReplacesSymbol()
        Dim target = New SymbolTable()
        Dim expectedSymbol1 = New Symbol()
        expectedSymbol1.Name = Guid.NewGuid().ToString()
        Dim expectedSymbol2 = New Symbol()
        expectedSymbol2.Name = Guid.NewGuid().ToString()

        target.Add(expectedSymbol1)

        Assert.AreSame(expectedSymbol1, target.GetSymbol(expectedSymbol1.Name))

        target.Assign(expectedSymbol1.Name, expectedSymbol2)

        Assert.AreSame(expectedSymbol2, target.GetSymbol(expectedSymbol1.Name))
    End Sub


End Class
