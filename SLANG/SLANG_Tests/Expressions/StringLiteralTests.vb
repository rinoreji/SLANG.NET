Imports SLANG

<TestClass()> Public Class StringLiteralTests
    <TestMethod()>
    Sub ExtendsFromExpression()
        Dim target = New StringLiteral("")

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod>
    Sub ConstructorTest()
        Dim expectedValue = Guid.NewGuid().ToString()
        Dim target = New StringLiteral(expectedValue)

        Dim result = target.Evaluate(Nothing)

        Assert.AreEqual(expectedValue, result.StringValue)
        Assert.IsNull(result.Name)
        Assert.AreEqual(Types.StringType, result.Type)
    End Sub

    <TestMethod>
    Sub Get_TypeTest_ReturnsString()
        Dim target = New StringLiteral("")
        Assert.AreEqual(Types.StringType, target.Get_Type())
    End Sub
End Class
