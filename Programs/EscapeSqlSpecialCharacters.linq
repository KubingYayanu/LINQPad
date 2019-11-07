<Query Kind="Program" />

void Main()
{
	var escape = "奇怪標題開始<M>[N]#%$\"'_(*)_結束";
	EscapeSqlSpecialCharacters(escape, '=').Dump();
}

// Define other methods, classes and namespaces here
public static string EscapeSqlSpecialCharacters(string input, char escapeCharacter = '\\')
{
    input = input.Replace(escapeCharacter.ToString(), $@"{escapeCharacter.ToString()}{escapeCharacter.ToString()}");
    input = input.Replace(@"%", $@"{escapeCharacter.ToString()}%");
    input = input.Replace(@"[", $@"{escapeCharacter.ToString()}[");
    input = input.Replace(@"]", $@"{escapeCharacter.ToString()}]");
    input = input.Replace(@"_", $@"{escapeCharacter.ToString()}_");
    return input;
}