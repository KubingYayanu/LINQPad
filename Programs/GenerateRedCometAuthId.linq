<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var serverTime = 1655293067;
	var secondKey = serverTime.ToString().Substring(0, 8);
	Console.WriteLine($"Second Key: {secondKey}");
	var authIdKey = "eLidot28520811";
	var authId = Md5CryptoHelper.EncryptMD5($"{secondKey}{authIdKey}");
	Console.WriteLine($"Auth Id: {authId}");
}

// You can define other methods, fields, classes and namespaces here
public class Md5CryptoHelper
{
    public static string EncryptMD5(string source)
    {
        using var cryptoMD5 = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(source);
        var hash = cryptoMD5.ComputeHash(bytes);
        var md5 = BitConverter.ToString(hash)
            .Replace("-", string.Empty)
            .ToLower();

        return md5;
    }
}