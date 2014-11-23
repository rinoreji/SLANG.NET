Imports SLANG

<TestClass>
Public Class PrintStatementTests
    <TestMethod>
    Sub InheritedFromStatement()
        Dim target = New PrintStatement(Nothing)

        Assert.IsInstanceOfType(target, GetType(Statement))
    End Sub

    <TestMethod>
    Sub ExecuteTest_CallsWriteWithExecuteResult()
        Dim expectedValue = New Random().NextDouble()
        Dim expression = New NumericConstant(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New PrintStatement(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsTrue(target.Execute(Nothing))
        Assert.AreEqual(expectedValue.ToString(), actualResult)
    End Sub

    <TestMethod>
    Sub PrintLine_ExecuteTest_CallsWriteWithExecuteResult()
        Dim expectedValue = New Random().NextDouble()
        Dim expression = New NumericConstant(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New PrintLineStatement(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsTrue(target.Execute(Nothing))
        Assert.AreEqual(expectedValue.ToString(), actualResult)
    End Sub
End Class
