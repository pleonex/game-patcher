namespace PleOps.GamePatcher.Poc.Mvvm;

using System;

public class Interaction<TInput, TOutput>
{
    private Func<TInput, TOutput>? handler;

    public TOutput Handle(TInput input)
    {
        if (handler is null)
        {
            throw new InvalidOperationException("Missing handler");
        }

        return handler(input);
    }

    public void RegisterHandler(Func<TInput, TOutput> newHandler)
    {
        this.handler = newHandler;
    }
}

public class Interaction<TOutput>
{
    private Func<TOutput>? handler;

    public TOutput Handle()
    {
        if (handler is null)
        {
            throw new InvalidOperationException("Missing handler");
        }

        return handler();
    }

    public void RegisterHandler(Func<TOutput> newHandler)
    {
        this.handler = newHandler;
    }
}
