Imports SLANG

<TestClass> Public Class SymbolTests
    <TestMethod>
    Sub PropertyTest_StringValue()
        Dim target = New Symbol()
        Dim expextedName = Guid.NewGuid().ToString()
        Dim expextedString = Guid.NewGuid().ToString()
        Dim expectedBool = New Random().Next() Mod 2 = 0
        Dim expectedNumeric = New Random().NextDouble()

        target.Name = expextedName
        target.NumericValue = expectedNumeric
        target.StringValue = expextedString
        target.BoolValue = expectedBool
        target.Type = Types.BooleanType

        Assert.AreEqual(expextedName, target.Name)
        Assert.AreEqual(expectedBool, target.BoolValue)
        Assert.AreEqual(expextedString, target.StringValue)
        Assert.AreEqual(expectedNumeric, target.NumericValue)
        Assert.AreEqual(Types.BooleanType, target.Type)
    End Sub

    <TestMethod>
    Sub ConstructorTest_StringValue()
        Dim expextedName = Guid.NewGuid().ToString()
        Dim expextedString = Guid.NewGuid().ToString()

        Dim target = New Symbol(expextedName, expextedString)

        Assert.AreEqual(expextedName, target.Name)
        Assert.AreEqual(Types.StringType, target.Type)
        Assert.AreEqual(expextedString, target.StringValue)
    End Sub

    <TestMethod>
    Sub ConstructorTest_BooleanValue()
        Dim expextedName = Guid.NewGuid().ToString()
        Dim expectedBool = New Random().Next() Mod 2 = 0

        Dim target = New Symbol(expextedName, expectedBool)

        Assert.AreEqual(expextedName, target.Name)
        Assert.AreEqual(Types.BooleanType, target.Type)
        Assert.AreEqual(expectedBool, target.BoolValue)
    End Sub

    <TestMethod>
    Sub ConstructorTest_NumericValue()
        Dim expextedName = Guid.NewGuid().ToString()
        Dim expectedNumeric = New Random().NextDouble()

        Dim target = New Symbol(expextedName, expectedNumeric)

        Assert.AreEqual(expextedName, target.Name)
        Assert.AreEqual(Types.NumericType, target.Type)
        Assert.AreEqual(expectedNumeric, target.NumericValue)
    End Sub
End Class
