Imports SLANG
<TestClass>
Public Class IfStatementTests
    <TestMethod>
    Sub InheritedFromStatement()
        Dim target = New IfStatement(Nothing, Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Statement))
    End Sub

    <TestMethod>
    Sub ExecuteTest_ConditionTrue_ExecuteIfStatements()
        Dim result = 0

        Dim printIf = New Print(New StringLiteral(vbTrue.ToString()))
        printIf.Write = Sub(data As String)
                            result += 1
                        End Sub
        Dim arrIf = New ArrayList()
        arrIf.Add(printIf)
        arrIf.Add(printIf)

        Dim printElse = New Print(New StringLiteral(vbFalse.ToString()))
        printElse.Write = Sub(data As String)
                              result += 10
                          End Sub
        Dim arrElse = New ArrayList()
        arrElse.Add(printElse)
        arrElse.Add(printElse)

        Dim target = New IfStatement(New BooleanConstant(True), arrIf, arrElse)
        target.Execute(New RuntimeContext())
        Assert.AreEqual(2, result)

        result = 0
        target = New IfStatement(New BooleanConstant(False), arrIf, arrElse)
        target.Execute(New RuntimeContext())
        Assert.AreEqual(20, result)
    End Sub

End Class
