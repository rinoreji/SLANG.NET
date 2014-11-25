Imports SLANG

<TestClass()> Public Class NumericConstantTests
    <TestMethod()>
    Sub ExtendsFromExpression()
        Dim target = New NumericConstant(13.0)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod>
    Sub ConstructorTest()
        Dim expectedValue = New Random().NextDouble()
        Dim target = New NumericConstant(expectedValue)

        Dim result = target.Evaluate(Nothing)

        Assert.AreEqual(expectedValue, result.NumericValue)
        Assert.IsNull(result.Name)
        Assert.AreEqual(Types.NumericType, result.Type)
    End Sub

    <TestMethod>
    Sub Get_TypeTest_ReturnsNumeric()
        Dim target = New NumericConstant(13.0)
        Assert.AreEqual(Types.NumericType, target.Get_Type())
    End Sub

    <TestMethod>
    Sub TypeCheckTest_ReturnsNumeric()
        Dim target = New NumericConstant(13.0)
        Assert.AreEqual(Types.NumericType, target.TypeCheck(Nothing))
    End Sub

End Class
