Public Class Multiply
    Inherits BinaryExpression

    Sub New(leftExp As Expression, rightExp As Expression)
        MyBase.New(leftExp, rightExp)
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Symbol
        PopulateSymbolsFromContext(context)

        If (IsNumericConstants()) Then
            Return New Symbol("", _leftSymbol.NumericValue * _rightSymbol.NumericValue)
        Else
            Throw New Exception("Type mismatch/invalid")
        End If
    End Function

    Public Overrides Function TypeCheck(context As CompilationContext) As Types
        PopulateTypesFromContext(context)

        If (_leftType = _rightType AndAlso _leftType = Types.NumericType) Then
            _type = _leftType
            Return _type
        Else
            Throw New Exception("Type mismatch/invalid")
        End If
    End Function
End Class