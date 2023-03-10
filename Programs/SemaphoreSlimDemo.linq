<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

private static readonly SemaphoreSlim Locker = new SemaphoreSlim(1, 1);
private static int sum;

void Main()
{
	sum = 0;
	Task.WaitAll(Enumerable.Range(0, 10000).Select(x => DoAdding()).ToArray());
	
	Console.WriteLine(sum);
}

// You can define other methods, fields, classes and namespaces here
private static async Task DoAdding()
{
	await Locker.WaitAsync();
	await Task.Run(() => { sum++; });
	Locker.Release();
}