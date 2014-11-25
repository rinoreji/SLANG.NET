Imports SLANG
<TestClass> Public Class TypesTest
    <TestMethod>
    Sub IllegalHaveSpecificValue()
        Assert.AreEqual(-1, Convert.ToInt32(Types.Illegal))
    End Sub
End Class
