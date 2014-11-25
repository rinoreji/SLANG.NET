Imports SLANG

<TestClass()> Public Class BooleanConstantTests
    <TestMethod()>
    Sub ExtendsFromExpression()
        Dim target = New BooleanConstant(False)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod>
    Sub ConstructorTest()
        Dim expectedValue = New Random().Next() Mod 2 = 0
        Dim target = New BooleanConstant(expectedValue)

        Dim result = target.Evaluate(Nothing)

        Assert.AreEqual(expectedValue, result.BoolValue)
        Assert.IsNull(result.Name)
        Assert.AreEqual(Types.BooleanType, result.Type)
    End Sub

    <TestMethod>
    Sub Get_TypeTest_ReturnsBoolean()
        Dim target = New BooleanConstant(False)
        Assert.AreEqual(Types.BooleanType, target.Get_Type())
    End Sub

    <TestMethod>
    Sub TypeCheckTest_ReturnsBoolean()
        Dim target = New BooleanConstant(13.0)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(Nothing))
    End Sub

End Class
