Public Enum Tokens
    Illegal = -1 'Not a valid token
    Plus = 1 '+
    Mul '*
    Div '/
    Minus ' -
    OpeningParenthesis '(
    ClosingParenthesis ')
    Number 'Data, Number
    EOE 'End of expression
    Print 'Print statement
    PrintLine  'PrintLine statement
    SemiColon ';

    UnquotedString 'Unquoted section in statement line, used for variable name and function names
    Var_Number ' Number datatype
    Var_String 'String datatype
    Var_Bool 'Bool datatype
    Comment
    TrueValue
    FalseValue
    Assignment
    StringValue
End Enum