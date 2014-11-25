Imports SLANG
<TestClass>
Public Class AssignmentTests
    <TestMethod>
    Sub InheritedFromStatements()
        Dim target = New Assignment(Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Statement))
    End Sub

    <TestMethod>
    Sub ExecuteTest_AddSymbolToSymbolTable()
        Dim expectedValue = New Random().NextDouble()
        Dim variable = New Variable(New Symbol("test", 13.3))
        Dim expression = New NumericConstant(expectedValue)
        Dim context = New RuntimeContext()

        Dim target = New Assignment(variable, expression)

        Assert.IsNull(target.Execute(context))
        Dim result = context.SymbolTable.GetSymbol(variable.Name).NumericValue
        Assert.AreEqual(expectedValue, result)

    End Sub


End Class
