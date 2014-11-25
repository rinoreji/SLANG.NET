Imports SLANG

<TestClass>
Public Class RDParserTests

    <TestMethod>
    Public Sub ConstructorTest_IsLexer()
        Dim target = New RDParser("")

        Assert.IsInstanceOfType(target, GetType(Lexer))
    End Sub

    <TestMethod>
    Public Sub ConstructorTest_PassExpressionToBase()
        Dim expectedNumber = (New Random().NextDouble() * New Random().Next()).ToString()

        Dim target = New RDParser(expectedNumber)

        Assert.AreEqual(Tokens.Number, target.GetNextToken())
        Assert.AreEqual(Convert.ToDouble(expectedNumber), target.GetNumber())
        Assert.AreEqual(Tokens.EOE, target.GetNextToken())
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_InvalidToken()
        Dim context = New CompilationContext()
        Dim source = "NUMBER num; INVALIDTYPE invalid"
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("Variable not found", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_SemiColMissing()
        Dim context = New CompilationContext()
        Dim source = "NUMBER num"
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("missing ;", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_SyntaxError()
        Dim context = New CompilationContext()
        Dim source = "NUMBER 1invalid"
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("syntax error in variable declaration", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_TypeMismatch()
        Dim context = New CompilationContext()
        Dim source = "NUMBER num;num=""Test"""
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("Type mismatch in assignment", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_SemcolAfterAssignment()
        Dim context = New CompilationContext()
        Dim source = "NUMBER num;num=1"
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("; expected", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_ThrowsException_AssignmetMissing()
        Dim context = New CompilationContext()
        Dim source = "NUMBER num;num ;"
        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()
        Try
            target.Parse(context)
        Catch ex As Exception
            Assert.AreEqual("Invalid statement, = expected", ex.Message)
        End Try
    End Sub

    <TestMethod>
    Sub ParseTest_Declate_Assign_Print()
        Dim context = New CompilationContext()
        Dim str = Guid.NewGuid().ToString()
        Dim num = New Random().NextDouble()
        Dim bVal = New Random().Next() Mod 2 = 0
        Dim actualResult As String = ""
        Dim source = String.Format("STRING str;NUMBER num;BOOL bVal;str=""{0}"";num={1};bVal={2};num=num+13;Printline str;Print num;Print bVal;", str, num, bVal)

        Dim target = New RDParser(source)
        Dim runtime = New RuntimeContext()

        Dim statements = target.Parse(context)
        For Each state As Statement In statements
            If (state.GetType() = GetType(Print) Or state.GetType() = GetType(PrintLine)) Then
                CType(state, Print).Write = Sub(data As String)
                                                actualResult += data
                                            End Sub
            End If
            state.Execute(runtime)
        Next
        Assert.AreEqual(String.Format("{0}{1}{2}", str, num + 13, bVal.ToString().ToUpper()), actualResult)
    End Sub

    <TestMethod>
    Sub ParseTest_()
        Dim target = New RDParser("print 1+2 ; Printline 2;")

        Dim result = target.Parse(Nothing)

        Assert.IsInstanceOfType(result(0), GetType(Print))
        Assert.IsInstanceOfType(result(1), GetType(PrintLine))
    End Sub



End Class
