<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><FWS><IS>LERICKSON</IS><Method>iDCS/GetMemberData</Method><Type>1</Type><CellPhone>886-0931843321</CellPhone></FWS>";
	var token = "5500aefc-6564-4702-a063-f359e382833c";
	var signature = Md5CryptoHelper.EncryptMD5($"{token}{xml}");
	signature.Dump();
	
	//typeof(string).Dump();
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
            .ToUpper();

        return md5;
    }
}