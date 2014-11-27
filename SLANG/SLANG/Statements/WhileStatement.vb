
Public Class WhileStatement
    Inherits Statement

    Private _condition As Expression
    Private _statements As ArrayList

    Sub New(conditon As Expression, statements As ArrayList)
        _condition = conditon
        _statements = statements
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Symbol
        Return ExecuteWhile(context)
    End Function

    Private Function ExecuteWhile(context As RuntimeContext) As Symbol
        Dim symbol = _condition.Evaluate(context)
        If (IsNothing(symbol) Or symbol.Type <> Types.BooleanType) Then
            Return Nothing
        End If
        If (symbol.BoolValue) Then
            For Each stmt As Statement In _statements
                Dim returnVal = stmt.Execute(context)
                If (Not IsNothing(returnVal)) Then
                    Return returnVal
                End If
            Next
            Return ExecuteWhile(context)
        End If
        Return Nothing
    End Function

End Class