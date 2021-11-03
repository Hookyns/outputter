# Json Formatting Plugin for Outputter

## Examples
Tokenizing JSON:
```c#
using RJDev.Outputter;
using RJDev.Outputter.Sinks;

JsonTokenizingFormatter jsonTokenFormatter = new();

await outputter.OutputReader
    .Pipe(new SimpleLambdaSink(entry =>
    {
        Console.WriteLine(jsonTokenFormatter.Format(entry));
    }))
    ;
```

Output of each formatted entry is JSON array with token objects `{ type: number, value: string }`.