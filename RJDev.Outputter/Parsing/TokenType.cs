using System;

namespace RJDev.Outputter.Parsing
{
    [Flags]
    public enum TokenType
    {
        Text = 1,
        ArgumentGeneric = 2,
        ArgumentStruct = 4,
        ArgumentDateTime = 8,
        ArgumentObject = 0x10,
        
        Argument = ArgumentGeneric | ArgumentStruct | ArgumentDateTime | ArgumentObject
    }
}