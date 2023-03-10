<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// This method causes a deadlock when called in a GUI or ASP.NET context.
void Main()
{
	// Start the delay.
	var delayTask = DelayAsync();
    // Wait for the delay to complete.
	delayTask.Wait();
}

// You can define other methods, fields, classes and namespaces here
private static async Task DelayAsync()
{
	await Task.Delay(1000);
}