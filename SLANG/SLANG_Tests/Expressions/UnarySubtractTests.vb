Imports SLANG
<TestClass>
Public Class UnarySubtractTests
    <TestMethod>
    Sub UnarySubtractTest_InheritedFromExpression()
        Dim target = New UnarySubtract(New NumericConstant(13.3))

        Assert.IsInstanceOfType(target, GetType(Expression))
    End Sub

    <TestMethod>
    Sub UnarySubtractTest_Evaluate_Numeric()
        Dim rand = New Random().NextDouble()

        Dim target = New UnarySubtract(New NumericConstant(rand))

        Dim result = target.Evaluate(Nothing)

        Assert.AreEqual(-rand, result.NumericValue)
        Assert.AreEqual(Types.NumericType, result.Type)
    End Sub

    <TestMethod>
    Sub UnarySubtractTest_Evaluate_InvalidType_ThrowsException()
        Dim target = New UnarySubtract(New BooleanConstant(True))
        Try
            target.Evaluate(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub UnarySubtractTest_TypeCheck_Numeric()
        Dim target = New UnarySubtract(New NumericConstant(13.3))

        Dim result = target.TypeCheck(Nothing)

        Assert.AreEqual(Types.NumericType, result)
    End Sub

    <TestMethod>
    Sub UnaryAddTest_TypeCheck_InvalidType_ThrowsException()
        Dim target = New UnaryAdd(New BooleanConstant(True))
        Try
            target.TypeCheck(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub
End Class
