Imports SLANG

<TestClass>
Public Class RelationalExpressionTests
    <TestMethod()>
    Sub ExtendsFromExpression()
        Dim target = New RelationalExpression(Nothing, Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    Function GetExp(value As Double) As Expression
        Return New NumericConstant(value)
    End Function

    Function GetExp(value As String) As Expression
        Return New StringLiteral(value)
    End Function

    Function GetExp(value As Boolean) As Expression
        Return New BooleanConstant(value)
    End Function

    <TestMethod>
    Sub EvaluateTest_NumericType()
        Dim target = New RelationalExpression(GetExp(13), GetExp(13), Operators.Equals)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.NotEquals)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.GreaterThan)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.GreaterThanEqual)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.LessThan)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.LessThanEqual)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub

    <TestMethod>
    Sub EvaluateTest_String()
        Dim target = New RelationalExpression(GetExp("RRc"), GetExp("RRc"), Operators.Equals)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp("RRc"), GetExp("Rrc"), Operators.NotEquals)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp("this operation is invalid"), GetExp("always false"), Operators.GreaterThan)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)
        target = New RelationalExpression(GetExp("this operation is invalid"), GetExp("always false"), Operators.LessThanEqual)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub

    <TestMethod>
    Sub EvaluateTest_Boolean()
        Dim target = New RelationalExpression(GetExp(True), GetExp(True), Operators.Equals)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(True), GetExp(False), Operators.NotEquals)
        Assert.IsTrue(target.Evaluate(New RuntimeContext()).BoolValue)

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.GreaterThan)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)
        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.LessThanEqual)
        Assert.IsFalse(target.Evaluate(New RuntimeContext()).BoolValue)
    End Sub

    <TestMethod>
    Sub TypeCheckTest_NumericType()
        Dim target = New RelationalExpression(GetExp(13), GetExp(13), Operators.Equals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.NotEquals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.GreaterThan)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.GreaterThanEqual)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.LessThan)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))

        target = New RelationalExpression(GetExp(13), GetExp(13), Operators.LessThanEqual)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
    End Sub

    <TestMethod>
    Sub TypeCheckTest_String()
        Dim target = New RelationalExpression(GetExp(""), GetExp(""), Operators.Equals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(""), GetExp(""), Operators.NotEquals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(""), GetExp(10), Operators.GreaterThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(""), GetExp(""), Operators.GreaterThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(""), GetExp(""), Operators.LessThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(""), GetExp(""), Operators.LessThanEqual)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(""), GetExp(""), Operators.GreaterThanEqual)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub TypeCheckTest_Boolean()
        Dim target = New RelationalExpression(GetExp(True), GetExp(True), Operators.Equals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.NotEquals)
        Assert.AreEqual(Types.BooleanType, target.TypeCheck(New CompilationContext()))
        Assert.AreEqual(Types.BooleanType, target.Get_Type())

        target = New RelationalExpression(GetExp(True), GetExp(10), Operators.GreaterThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.GreaterThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.LessThan)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.LessThanEqual)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try

        target = New RelationalExpression(GetExp(True), GetExp(True), Operators.GreaterThanEqual)
        Try
            target.TypeCheck(New CompilationContext())
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Only == and != operations are valid on this type", ex.Message)
        End Try
    End Sub

End Class
