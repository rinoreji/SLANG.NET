Imports SLANG

<TestClass>
Public Class TokenTests

    <TestMethod>
    Sub TokenTest_IllegalHasSpecificValue()
        Assert.AreEqual(-1, Convert.ToInt32(Token.Illegal))
    End Sub

    <TestMethod>
    Sub TokenTest_PlusSpecificValue()
        Assert.AreEqual(1, Convert.ToInt32(Token.Plus))
    End Sub

End Class
