Public MustInherit Class BinaryExpression
    Inherits Expression

    Friend _leftExpression As Expression
    Friend _rightExpression As Expression
    Friend _leftSymbol As Symbol
    Friend _rightSymbol As Symbol
    Friend _leftType As Types
    Friend _rightType As Types

    Sub New(leftExp As Expression, rightExp As Expression)
        _leftExpression = leftExp
        _rightExpression = rightExp
    End Sub

    Friend Sub PopulateSymbolsFromContext(context As RuntimeContext)
        _leftSymbol = _leftExpression.Evaluate(context)
        _rightSymbol = _rightExpression.Evaluate(context)
    End Sub

    Friend Sub PopulateTypesFromContext(context As CompilationContext)
        _leftType = _leftExpression.TypeCheck(context)
        _rightType = _rightExpression.TypeCheck(context)
    End Sub

    Private Function IsSymbolsAreOfSameTypes()
        If (_leftSymbol.Type = _rightSymbol.Type) Then
            Return True
        End If
        Return False
    End Function

    Friend Function IsStringLiterals()
        If (IsSymbolsAreOfSameTypes() AndAlso _leftSymbol.Type = Types.StringType) Then
            Return True
        End If
        Return False
    End Function

    Friend Function IsNumericConstants()
        If (IsSymbolsAreOfSameTypes() AndAlso _leftSymbol.Type = Types.NumericType) Then
            Return True
        End If
        Return False
    End Function
End Class
