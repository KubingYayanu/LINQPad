<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var pattern = @"^[a-zA-Z0-9+._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z0-9_-]+$";
	var regex = new Regex(pattern);
	
	PrintValidList(regex);
	PrintInvalidList(regex);
}

// You can define other methods, fields, classes and namespaces here
private void PrintValidList(Regex regex)
{
	var validList = GetValidList();
	
	Console.WriteLine("Valid List:");
	foreach (var item in validList)
	{
		var isMatch = regex.IsMatch(item);
		Console.WriteLine($"{item}: {isMatch}");
	}
}

private void PrintInvalidList(Regex regex)
{
	var invalidList = GetInvalidList();
	
	Console.WriteLine("");
	Console.WriteLine("Invalid List:");
	foreach (var item in invalidList)
	{
		var isMatch = regex.IsMatch(item);
		Console.WriteLine($"{item}: {isMatch}");
	}
}


private List<string> GetValidList()
{
	return new List<string>
	{
		"zztest07@tester.com",
		"zztest.02@tester.com",
		"zztest+03@tester.com",
		"zzTest_03@tester.com",
		"test..Don@yahoo.com.tw",
		"607@lddl-tester.com",
		"2f.chefwen@wd-tester.com",
		"fwef2_cfsssn@tester.com",
		"al2fff.x.ksse@tester.com",
		"Raan.Hvwv@tw.tester.com",
		"afg0136@tester.com.tw",
		"noreply-mailli0qTGdSPWySLXL@memek.qinzijiaoyu.org",
		"auth-replyYxqVt8hw7cx4YAj@lingosol.com",
		"noreplymail-NR7kQOYUA@ay.lorenzos-paintersinc.com"
	};
}

private List<string> GetInvalidList()
{
	return new List<string>
	{
		"zztest0 7@tester.com",
		"zztest07 @tester.com",
		" zztest07@tester.com",
		"中文+數字@tester.com",
		"我我我是測試01@tester.com"
	};
}