<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var vendorId = "maac";
	var hashToken = GenerateHashToken(vendorId);
	var key = GenerateEncryptedKey(hashToken);
	
	hashToken.Dump();
	key.Dump();
}

// You can define other methods, fields, classes and namespaces here
/// <summary>
/// 使用 SHA256 產生一組 Token 交給第三方廠商
/// </summary>
/// <param name="vendorId"></param>
/// <returns></returns>
public static string GenerateHashToken(string vendorId)
{
    // without hyphen
    var guid = Guid.NewGuid().ToString("N").ToUpper();
    var guidChuncks = Split(guid, 8).ToList();
    var str = $"{guidChuncks[2]}.{vendorId}.{guidChuncks[3]}.{guidChuncks[1]}.{guidChuncks[0]}";
    var hash = HMACSHA256HashHelper.HashBase64(str)
        .Replace('+', '-').Replace('/', '_');

    return hash;
}

private static IEnumerable<string> Split(string str, int chunkSize)
{
    return Enumerable.Range(0, str.Length / chunkSize)
        .Select(i => str.Substring(i * chunkSize, chunkSize));
}

public static string GenerateEncryptedKey(string token)
{
    const string iv = "sSU5oiFFi4Z3o9S9ZyFJxQ==";
    var aesKey = "FR#Dp9H^2X*gP35z";
    var key = EncryptHelper.EncryptAES(token, aesKey, iv);

    return key;
}

public static class HMACSHA256HashHelper
{
    private const string _key = "3zLAB6xb/jIuQZ1SKglVU99/DL3tADe9RCKcLMCMc6V9fmL80wWGR9u/NVia9vovprhHUH53Gg/iSwbBHy30EA==";

    private static HMACSHA256 _provider;

    private static HMACSHA256 provider
    {
        get
        {
            if (_provider == null)
            {
                var key = Encoding.UTF8.GetBytes(_key);
                _provider = new HMACSHA256(key);
            }
            return _provider;
        }
    }

    public static string GenerateKey()
    {
        var hmac = new HMACSHA256();
        return Convert.ToBase64String(hmac.Key);
    }

    public static byte[] Hash(string source)
    {
        byte[] sourceByte = Encoding.UTF8.GetBytes(source);
        return provider.ComputeHash(sourceByte);
    }

    public static string HashBase64(string source)
    {
        var hashResult = Hash(source);
        return Convert.ToBase64String(hashResult);
    }

    public static string HashHex(string source)
    {
        var hashResult = Hash(source);
        return BitConverter.ToString(hashResult).Replace("-", string.Empty);
    }
}

public static class EncryptHelper
{
    public static string EncryptAES(string text, string key, string iv)
    {
        var sourceBytes = Encoding.UTF8.GetBytes(text);
        var aes = Aes.Create();
        
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);

        using (var md5 = MD5.Create())
        {
            aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(iv));
        }

        var transform = aes.CreateEncryptor();
        return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
    }

    public static string DecryptAES(string text, string key, string iv)
    {
        var encryptBytes = Convert.FromBase64String(text);
        var aes = Aes.Create();

        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(key);

        using (var md5 = MD5.Create())
        {
            aes.IV = md5.ComputeHash(Encoding.Unicode.GetBytes(iv));
        }

        var transform = aes.CreateDecryptor();
        return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
    }
}