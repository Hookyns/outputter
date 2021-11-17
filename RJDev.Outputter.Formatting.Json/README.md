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

Output of each formatted entry is JSON object like `{ type: number, tokens: Array<{ type: number, value: string }> }`.