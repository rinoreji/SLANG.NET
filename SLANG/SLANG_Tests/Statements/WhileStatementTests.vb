Imports SLANG
<TestClass>
Public Class WhileStatementTests
    <TestMethod>
    Sub InheritedFromStatement()
        Dim target = New WhileStatement(Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Statement))
    End Sub

    'Wrong test, stack will overflow. As of now no idea how to test
    '<TestMethod>
    'Sub ExecuteTest_ConditionTrue_ExecuteStatements()
    '    Dim result = 0

    '    Dim printstmt = New Print(New StringLiteral(vbTrue.ToString()))
    '    printstmt.Write = Sub(data As String)
    '                          result += 1
    '                      End Sub
    '    Dim arrTrueStmts = New ArrayList()
    '    arrTrueStmts.Add(printstmt)
    '    arrTrueStmts.Add(printstmt)

    '    Dim target = New WhileStatement(New BooleanConstant(True), arrTrueStmts)
    '    target.Execute(New RuntimeContext())
    '    Assert.AreEqual(2, result)

    '    result = 0
    '    target = New WhileStatement(New BooleanConstant(False), arrTrueStmts)
    '    target.Execute(New RuntimeContext())
    '    Assert.AreEqual(0, result)
    'End Sub

End Class
