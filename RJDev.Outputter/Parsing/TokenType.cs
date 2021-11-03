using System;

namespace RJDev.Outputter.Parsing
{
    [Flags]
    public enum TokenType
    {
        Text = 1,
        ArgumentGeneric = 1 << 1,
        ArgumentStruct = 1 << 2,
        ArgumentDateTime = 1 << 3,
        ArgumentObject = 1 << 4,

        Argument = ArgumentGeneric | ArgumentStruct | ArgumentDateTime | ArgumentObject
    }
}