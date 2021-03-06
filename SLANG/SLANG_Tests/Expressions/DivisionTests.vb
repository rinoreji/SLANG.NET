﻿Imports SLANG

<TestClass>
Public Class DivisionTests
    <TestMethod>
    Sub DivisionTest_ExtendsFromExpression()
        Dim target = New Divide(Nothing, Nothing)

        Assert.IsInstanceOfType(target, GetType(Expression))
        Assert.IsInstanceOfType(target, GetType(BinaryExpression))
    End Sub

    <TestMethod>
    Sub DivisionTest_Evaluate_Numeric()
        Dim rand = New Random()
        Dim num1 = rand.NextDouble()
        Dim num2 = rand.NextDouble()

        Dim target = New Divide(New NumericConstant(num1), New NumericConstant(num2))

        Dim result = target.Evaluate(Nothing)

        Assert.AreEqual(num1 / num2, result.NumericValue)
        Assert.AreEqual(Types.NumericType, result.Type)
    End Sub

    <TestMethod>
    Sub DivisionTest_Evaluate_InvalidType_ThrowsException()
        Dim target = New Divide(New BooleanConstant(True), New BooleanConstant(True))
        Try
            target.Evaluate(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub DivisionTest_Evaluate_DifferentType_ThrowsException()
        Dim target = New Divide(New NumericConstant(13.0), New StringLiteral("RRc"))
        Try
            target.Evaluate(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub DivisionTest_TypeCheck_Numeric()
        Dim target = New Divide(New NumericConstant(13.3), New NumericConstant(13.3))

        Dim result = target.TypeCheck(Nothing)

        Assert.AreEqual(Types.NumericType, result)
    End Sub

    <TestMethod>
    Sub DivisionTest_TypeCheck_InvalidType_ThrowsException()
        Dim target = New Divide(New BooleanConstant(True), New BooleanConstant(True))
        Try
            target.TypeCheck(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub DivisionTest_TypeCheck_DifferentType_ThrowsException()
        Dim target = New Divide(New NumericConstant(13.0), New StringLiteral("RRc"))
        Try
            target.TypeCheck(Nothing)
            Assert.Fail()
        Catch ex As Exception
            Assert.AreEqual("Type mismatch/invalid", ex.Message)
        End Try
    End Sub
End Class