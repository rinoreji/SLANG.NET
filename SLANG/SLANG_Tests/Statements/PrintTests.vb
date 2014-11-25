Imports SLANG

<TestClass>
Public Class PrintTests
    <TestMethod>
    Sub InheritedFromStatement()
        Dim target = New Print(Nothing)

        Assert.IsInstanceOfType(target, GetType(Statement))
    End Sub

    <TestMethod>
    Sub ExecuteTest_CallsWriteWithExecuteResult()
        Dim expectedValue = New Random().NextDouble()
        Dim expression = New NumericConstant(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New Print(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsNull(target.Execute(Nothing))
        Assert.AreEqual(expectedValue.ToString(), actualResult)
    End Sub

    <TestMethod>
    Sub ExecuteTest_CallsWriteWithExecuteResult_StringLiteral()
        Dim expectedValue = Guid.NewGuid().ToString()
        Dim expression = New StringLiteral(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New Print(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsNull(target.Execute(Nothing))
        Assert.AreEqual(expectedValue, actualResult)
    End Sub

    <TestMethod>
    Sub ExecuteTest_CallsWriteWithExecuteResult_Boolean()
        Dim expectedValue = New Random().Next() Mod 2 = 0
        Dim expression = New BooleanConstant(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New Print(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsNull(target.Execute(Nothing))
        Assert.AreEqual(expectedValue.ToString().ToUpperInvariant(),
                        actualResult)
    End Sub

    <TestMethod>
    Sub PrintLine_ExecuteTest_CallsWriteWithExecuteResult()
        Dim expectedValue = New Random().NextDouble()
        Dim expression = New NumericConstant(expectedValue)
        Dim actualResult As String = String.Empty

        Dim target = New PrintLine(expression)
        target.Write = Sub(value As String)
                           actualResult = value
                       End Sub

        Assert.IsNull(target.Execute(Nothing))
        Assert.AreEqual(expectedValue.ToString(), actualResult)
    End Sub
End Class
