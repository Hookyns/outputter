using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace RJDev.Outputter
{
	// class Awaiter<TResult> : INotifyCompletion
	// {
	// 	private readonly IAwaitableOutput<TResult> awaitable;
	//
	// 	public bool IsCompleted => this.awaitable.IsCompleted;
	//
	// 	public Awaiter(IAwaitableOutput<TResult> awaitable)
	// 	{
	// 		this.awaitable = awaitable;
	// 	}
	//    
	// 	public void OnCompleted(Action continuation)
	// 	{
	// 		if (IsCompleted)
	// 		{
	// 			continuation();
	// 			return;
	// 		}
	// 		var capturedContext = SynchronizationContext.Current;
	// 		awaitable.OnCompleted += () =>
	// 		{
	// 			if (capturedContext != null)
	// 			{
	// 				capturedContext.Post(_ => continuation(), null);
	// 			}
	// 			else
	// 			{
	// 				continuation();
	// 			}
	// 		};
	// 	}
	//
	// 	/// <summary>
	// 	/// Returns result
	// 	/// </summary>
	// 	/// <returns></returns>
	// 	public TResult GetResult()
	// 	{
	// 		if (!this.IsCompleted)
	// 		{
	// 			SpinWait wait = new();
	// 			
	// 			while (!this.IsCompleted)
	// 			{
	// 				wait.SpinOnce();
	// 			}
	// 		}
	// 		
	// 		return this.awaitable.Result;
	// 	}
	// }

	class ExecutorOutput// : IAsyncEnumerable<string>
	{
        /// <summary>
        /// Message buffer.
        /// </summary>
        private readonly BufferBlock<OutputEntry> bufferblock = new();
        
		public bool IsCompleted { get; private set; }

		internal void Complete()
		{
			
		}
		
		public TaskAwaiter<string> GetAwaiter()
		{
			var tcs = new TaskCompletionSource<string>();
			
			return tcs.Task.GetAwaiter();
		}

		public IAsyncEnumerator<string> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
		{
			throw new NotImplementedException();
		}
	}

	// public class AsyncExecutorOutput : IAwaitableOutput<string>
	// {
	// 	public bool IsCompleted { get; }
	// }
}