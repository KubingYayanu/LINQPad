<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var task = NotFullyAsynchronousDemo.TestNotFullyAsync();
	task.Wait();
}

// You can define other methods, fields, classes and namespaces here
public static class NotFullyAsynchronousDemo
{
  	// This method synchronously blocks a thread.
	public static async Task TestNotFullyAsync()
  	{
    	await Task.Yield();
    	Thread.Sleep(1500);
  	}
}