
Public Class IfStatement
    Inherits Statement

    Private _condition As Expression
    Private _ifStatements As ArrayList
    Private _elseStatements As ArrayList

    Sub New(conditon As Expression, ifStatements As ArrayList, elseStatements As ArrayList)
        _condition = conditon
        _ifStatements = ifStatements
        _elseStatements = elseStatements
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Symbol
        Dim symbol = _condition.Evaluate(context)
        If (IsNothing(symbol) Or symbol.Type <> Types.BooleanType) Then
            Return Nothing
        End If
        If (symbol.BoolValue) Then
            For Each stmt As Statement In _ifStatements
                stmt.Execute(context)
            Next
        Else
            For Each stmt As Statement In _elseStatements
                stmt.Execute(context)
            Next
        End If
        Return Nothing
    End Function


End Class
