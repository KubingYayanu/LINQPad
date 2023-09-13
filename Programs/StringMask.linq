<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	foreach (var prefix in GetEmailPrefix())
	{
		var masked = GetMaskedEmailPrefix(prefix);
		masked.Dump();
	}
}

// You can define other methods, fields, classes and namespaces here
public string GetMaskedEmailPrefix(string email)
{
	var length = email.Length;
	if (length == 1)
	{
		//return $"{email}****";
		return $"Origin: {email}, Masked: {email}****";
	}
	
	if (length == 2)
	{
		//return $"{email[0]}****{email[1]}";
		return $"Origin: {email}, Masked: {email[0]}****{email[1]}";
	}
	
	var median = length / 2;
	var leftPointer = 0;
	var rightPointer = length - 1;
	var offset = 2;
	while (median - leftPointer > offset && rightPointer - median > offset)
	{
		rightPointer--;
		leftPointer++;
	}
	
	var count = rightPointer - leftPointer - 1;
	var stars = new string(Enumerable.Repeat('*', count).ToArray());
	var prefix = email.Substring(0, leftPointer + 1);
	var surfix = email.Substring(rightPointer);
	
	//return $"Origin: {email}, Masked: {prefix + "****" + surfix}, Left: {leftPointer}, Median: {median}, Right: {rightPointer}, Count: {count}";
	return $"Origin: {email}, Masked: {prefix + "****" + surfix}";
}

public List<string> GetEmailPrefix()
{
	return new List<string>
	{
		"1",
		"12",
		"123",
		"1234",
		"12345",
		"123456",
		"1234567",
		"12345678",
		"123456789",
		"123456789A",
		"123456789AB",
		"123456789ABC",
		"123456789ABCD",
		"123456789ABCDE"
	};
}