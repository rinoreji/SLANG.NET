Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports SLANG

<TestClass()> Public Class MiscTests

    <TestMethod()> Public Sub OperatorsTest_ValueOfIllegal()

        Assert.AreEqual(-1, Convert.ToInt32(Operators.Illegal))

    End Sub

End Class