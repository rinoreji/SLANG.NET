Imports SLANG
<TestClass>
Public Class VariableTests

    <TestMethod>
    Sub ConstructorTest_SetNameOfPassedSymbol()
        Dim symbol = New Symbol(Guid.NewGuid().ToString(), True)
        Dim target = New Variable(symbol)

        Assert.AreEqual(symbol.Name, target.Name)
    End Sub

    <TestMethod>
    Sub ConstructorTest_AddSymbolToSymbolTable_String()
        Dim context = New CompilationContext()
        Dim expectedName = Guid.NewGuid().ToString()
        Dim expectedString = Guid.NewGuid().ToString()

        Dim target = New Variable(context, expectedName, expectedString)

        Assert.AreEqual(expectedName, target.Name)
        Assert.AreEqual(Types.StringType, target.TypeCheck(context))
        Assert.AreEqual(Types.StringType, target.Get_Type())

        Dim symbol = context.SymbolTable.GetSymbol(expectedName)
        Assert.AreEqual(expectedString, symbol.StringValue)
    End Sub

    <TestMethod>
    Sub ConstructorTest_AddSymbolToSymbolTable_Double()
        Dim context = New CompilationContext()
        Dim expectedName = Guid.NewGuid().ToString()
        Dim expected = New Random().NextDouble()

        Dim target = New Variable(context, expectedName, expected)

        Assert.AreEqual(expectedName, target.Name)
        Assert.AreEqual(Types.NumericType, target.TypeCheck(context))
        Assert.AreEqual(Types.NumericType, target.Get_Type())

        Dim symbol = context.SymbolTable.GetSymbol(expectedName)
        Assert.AreEqual(expected, symbol.NumericValue)
    End Sub

    <TestMethod>
    Sub ConstructorTest_AddSymbolToSymbolTable_Bool()
        Dim context = New CompilationContext()
        Dim expectedName = Guid.NewGuid().ToString()
        Dim expected = New Random().Next() Mod 2 = 0

        Dim target = New Variable(context, expectedName, expected)

        Assert.AreEqual(expectedName, target.Name)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(context))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        Dim symbol = context.SymbolTable.GetSymbol(expectedName)
        Assert.AreEqual(expected, symbol.BoolValue)
    End Sub

    <TestMethod>
    Sub TypeCheckTest_ReturnsIllegal_ContextDoesntHaveSymbol()
        Dim target = New Variable(New CompilationContext(), "name", "value")

        Assert.AreEqual(Types.Illegal, target.TypeCheck(New CompilationContext()))
    End Sub

    <TestMethod>
    Sub EvaluateTest_ReturnSymbolFromRunTimeContext()
        Dim rContext = New RuntimeContext()
        Dim context = New CompilationContext()
        Dim expectedName = Guid.NewGuid().ToString()
        Dim expectedString = Guid.NewGuid().ToString()

        Dim target = New Variable(context, expectedName, expectedString)
        rContext.SymbolTable = context.SymbolTable

        Dim result = target.Evaluate(rContext)

        Assert.AreEqual(expectedName, result.Name)
        Assert.AreEqual(expectedString, result.StringValue)
    End Sub
End Class
