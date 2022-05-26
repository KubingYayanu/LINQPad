<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var value = "123stet";
    var encrypted = LiffThirdParty.GetEncryptedKey(nameof(LiffThirdParty.MaacApiKey), value);
    var liffThirdParty = new LiffThirdParty
    {
        MaacApiKey = encrypted
    };
	
	var decrypted = liffThirdParty.DecryptedMaacApiKey();
	decrypted.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class LiffThirdParty
{
	private const string AesKey = "FR#Dp9H^2X*gP35z";

    /// <summary>
    /// 已用 AES 加密的 maac_api_key
    /// </summary>
    [Description("maac_api_key")]
    public string MaacApiKey { get; set; }

    public string DecryptedMaacApiKey()
        => GetDecryptedKey(nameof(MaacApiKey), MaacApiKey);

    /// <summary>
    /// 已用 AES 加密的 otp_api_key
    /// </summary>
    [Description("otp_api_key")]
    public string OtpApiKey { get; set; }

    public string DecryptedOtpApiKey()
        => GetDecryptedKey(nameof(OtpApiKey), OtpApiKey);

    /// <summary>
    /// 已用 AES 加密的 shopline_openapi_key
    /// </summary>
    [Description("shopline_openapi_key")]
    public string ShoplineOpenApiKey { get; set; }

    public string DecryptedShoplineOpenApiKey()
        => GetDecryptedKey(nameof(ShoplineOpenApiKey), ShoplineOpenApiKey);

    /// <summary>
    /// 已用 AES 加密的 flaps_api_token
    /// </summary>
    [Description("flaps_api_token")]
    public string FlapsApiToken { get; set; }

    public string DecryptedFlapsApiToken()
        => GetDecryptedKey(nameof(FlapsApiToken), FlapsApiToken);

    /// <summary>
    /// 已用 AES 加密的 utk_api_key
    /// </summary>
    [Description("utk_api_key")]
    public string UtkApiKey { get; set; }

    public string DecryptedUtkApiKey()
        => GetDecryptedKey(nameof(UtkApiKey), UtkApiKey);

    private static string GetBsonElementName(string propertyName)
    {
        var elementName = typeof(LiffThirdParty)
            .GetProperty(propertyName)
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault()?.Description ?? string.Empty;

        return elementName;
    }

    private string GetDecryptedKey(string propertyName, string encrypted)
    {
        if (encrypted.IsNullOrWhiteSpace())
        {
            return null;
        }

        var aesKey = AesKey;
        if (aesKey == null)
        {
            throw new SystemException("Unable to get the environment variable AesKey.");
        }

        var iv = GetBsonElementName(propertyName);
        var decrypted = EncryptHelper.DecryptAES(encrypted, aesKey, iv);

        return decrypted;
    }

    public static string GetEncryptedKey(string propertyName, string value)
    {
        if (value.IsNullOrWhiteSpace())
        {
            return null;
        }

        var aesKey = AesKey;
        if (aesKey == null)
        {
            throw new SystemException("Unable to get the environment variable AesKey.");
        }

        var iv = GetBsonElementName(propertyName);
        var encrypted = EncryptHelper.EncryptAES(value, aesKey, iv);

        return encrypted;
    }
}

public static class StringHelper
{
	public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
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