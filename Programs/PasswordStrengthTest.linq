<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	// 字串不可包含任一空白，並且以下 1 ~ 3 的規則取其中 2 個符合就可以
	// 1. 至少 10 個字元
	// 2. 包含英文字母(大小寫都要有)
	// 3. 數字和符號 (僅限 ASCII 標準字元)
	
	var pattern = @"(^(?![\s\S]*\s)(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{10,})|(^(?![\s\S]*\s)(?=.*[a-z])(?=.*[A-Z])(?=.*[\p{S}\p{P}]).{10,})|(^(?![\s\S]*\s)(?=.*\d)(?=.*[\p{S}\p{P}]).{10,})$";
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
		"Ab#1234567",
		"Abc123$Def",
		"P@ssw0rdS1te",
		"7HelloWorld%",
		"Qwerty4567!",
		"Xyz789#%Abcd",
		"12P@ssword34",
		"Sym8bols&Num",
		"A1b2C3d4E5!",
		"SecurePass#19",
		"3xclamationW0rd$",
		"Abc123456789",
		"Abc!@^^#($9583",
		"Abc!@#$%%^^^#($",
		"!@#$%%^-^^#32",
		"Qwerty12345",
		"我123ABc#$464🐻",
		"1234567890#",
		"1234567890#!",
		"1234567890!",
		"1234567890@",
		"1234567890%",
		"1234567890&",
		"1234567890*",
		"1234567890(",
		"1234567890)",
		"1234567890:",
		"1234567890?",
		"1234567890/",
		"1234567890\\",
		"1234567890.",
		"1234567890$",
		"1234567890$!",
		"1234567890-",
		"P@sswordP@ssword",
		"P@ssword++",
		"Password++",
		"Passwordee!",
		"Passwordee@",
		"Passwordee#",
		"Passwordee%",
		"Passwordee&",
		"Passwordee*",
		"Passwordee(",
		"Passwordee)",
		"Passwordee:",
		"Passwordee?",
		"Passwordee/",
		"Passwordee\\",
		"Passwordee."
	};
}

private List<string> GetInvalidList()
{
	return new List<string>
	{
		"-- Valid$$",
		"-- Invalid3",
		"Ab#123 4567",
		"P@ssw0rd S1te",
		"我123ABc#$ 464🐻",
		"SimplePass",
		"1234567890",
		"abcdEFGH",
		"!@#$%^&*",
		"*(&@$&(@$&($@",
		"shortPwd!",
		"NoNumbersOrSymbols",
		"Lowerandupper",
		"6789_ABCD",
		"P@ssw0rd",
		"          In$",
		"     C3d              "
	};
}