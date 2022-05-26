<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var hash = AddThirdPartyInvoiceVM.GenerateKey(30, ThirdPartyInvoiceVendor.Maac);
	hash.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class AddThirdPartyInvoiceVM
{
    private const string HashSalt = "75E5BFADEF1B4D7CA9568394C6FC2158";

    public int BrandId { get; set; }

    /// <summary>
    /// Sha256
    /// </summary>
    public string Key { get; set; }

    public ThirdPartyInvoiceVendor? Vendor { get; set; }

    public bool IsKeyValid()
    {
        var password = $"{BrandId}.{Vendor}.{HashSalt}";
        var hash = StringHelper.ToSha256ForPassword(password);

        return hash.Equals(Key);
    }

    public static string GenerateKey(int brandId, ThirdPartyInvoiceVendor vendor)
    {
        var password = $"{brandId}.{vendor}.{HashSalt}";
        var hash = StringHelper.ToSha256ForPassword(password);

        return hash;
    }
}

public enum ThirdPartyInvoiceVendor
{
    /// <summary>
    /// 漸強
    /// </summary>
    [Description("漸強")]
    Maac = 1
}

public static class StringHelper
{
	public static string ToSha256ForPassword(string password)
    {
        string result = "";

        using (SHA256 sha256 = SHA256.Create())
        {
            string firstResult = "", swappedResult = "", replacedResult = "", secondResult = "";
            byte[] hashValue;
            StringBuilder stringBuilder;

            // 使用 SHA256 計算
            hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            stringBuilder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                stringBuilder.Append(hashValue[i].ToString("x2"));
            }
            firstResult = stringBuilder.ToString();

            if (firstResult.Length == 64)
            {
                // 切半前後交換
                swappedResult = $"{firstResult.Substring(32)}{firstResult.Substring(0, 32)}";

                // 前3碼取代成 "Lit"，中間32~36取代成 "loyal"，後面61~64取代成 "Migo"
                replacedResult = $"Lit{swappedResult.Substring(3, 28)}loyal{swappedResult.Substring(36, 24)}Migo";
            }

            // 再使用 SHA256 計算
            hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(replacedResult));
            stringBuilder = new StringBuilder();
            for (int i = 0; i < hashValue.Length; i++)
            {
                stringBuilder.Append(hashValue[i].ToString("x2"));
            }
            secondResult = stringBuilder.ToString();

            result = secondResult;
        }

        return result;
    }
}