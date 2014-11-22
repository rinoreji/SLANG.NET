Imports SLANG

<TestClass()> Public Class NumericConstantTests

    <TestMethod()> Public Sub ExtendsFromExpression()
        Dim target = New NumericConstant(13.0)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod> Public Sub ConstructorTest()
        Dim expectedValue = New Random().NextDouble()
        Dim target = New NumericConstant(expectedValue)

        Assert.AreEqual(expectedValue, target.Evaluate(Nothing))
    End Sub

End Class
