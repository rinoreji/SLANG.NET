Imports SLANG

<TestClass> Public Class ValueTableTests
    <TestMethod> Sub ConstructorTest()
        Dim expectedValue = Guid.NewGuid().ToString()
        Dim target = New ValueTable(Tokens.Plus, expectedValue)

        Assert.AreEqual(Tokens.Plus, target.Token)
        Assert.AreEqual(expectedValue, target.Value)
    End Sub
End Class
