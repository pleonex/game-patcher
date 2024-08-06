namespace PleOps.GamePatcher.Poc.Mvvm;

using System;
using System.Threading.Tasks;

public class AsyncInteraction<TInput, TOutput>
{
    private Func<TInput, Task<TOutput>>? handler;

    public Task<TOutput> HandleAsync(TInput input)
    {
        if (handler is null)
        {
            throw new InvalidOperationException("Missing handler");
        }

        return handler(input);
    }

    public void RegisterHandler(Func<TInput, Task<TOutput>> newHandler)
    {
        handler = newHandler;
    }
}

public class AsyncInteraction<TOutput>
{
    private Func<Task<TOutput>>? handler;

    public Task<TOutput> HandleAsync()
    {
        if (handler is null)
        {
            throw new InvalidOperationException("Missing handler");
        }

        return handler();
    }

    public void RegisterHandler(Func<Task<TOutput>> newHandler)
    {
        handler = newHandler;
    }
}

public class AsyncInteraction
{
    private Func<Task>? handler;

    public Task HandleAsync()
    {
        if (handler is null)
        {
            throw new InvalidOperationException("Missing handler");
        }

        return handler();
    }

    public void RegisterHandler(Func<Task> newHandler)
    {
        handler = newHandler;
    }
}
