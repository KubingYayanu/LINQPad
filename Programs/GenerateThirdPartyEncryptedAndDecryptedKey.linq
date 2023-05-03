<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var label = ThirdPartySettingKey.Lit_OtpApiKey.ToLabel();
    var thirdParty = new ThirdParty
    {
        VendorId = label.vendor,
        Key = label.key,
        Value = "ddwdfwf"
    };

    var value = thirdParty.GetEncryptedValue();
	value.Dump();
}

public const string AesKey = "FR#Dp9H^2X*gP35z";

// You can define other methods, fields, classes and namespaces here
public class ThirdParty
{
    /// <summary>
    /// PK
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 品牌 Id, FK
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// 廠商 Id
    /// </summary>
    public string VendorId { get; set; }

    /// <summary>
    /// 子品牌 Id
    /// </summary>
    public int SubBrandId { get; set; }

    /// <summary>
    /// 參數 Key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 參數 Value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 建立者
    /// </summary>
    public int Creator { get; set; }
}

public enum ThirdPartySettingVendor
{
    Maac,
    Thinker,
    Shopline,
    Utk,
    RedComet,
    Flaps,
    Lit,
    BotBonnie
}

public enum ThirdPartySettingKey
{
    Maac_LiffUrl,
    Maac_LiffId,
    Maac_LineOa,
    Maac_LineChannelName,
    Maac_ApiUrl,

    [EncryptedThirdPartySetting]
    Maac_ApiKey,

    Maac_BindTagId,
    Maac_BindTemplateId,
    Maac_CustomerIdField,
    Thinker_SiteDomain,
    Thinker_DepartmentId,

    [EncryptedThirdPartySetting]
    Thinker_GiftListApiKey,

    [EncryptedThirdPartySetting]
    Thinker_GiftSmsApiKey,

    Thinker_GiftSmsListId,

    [EncryptedThirdPartySetting]
    Shopline_OpenApiKey,

    [EncryptedThirdPartySetting]
    Utk_ApiKey,

    [EncryptedThirdPartySetting]
    RedComet_AuthIdKey,

    [EncryptedThirdPartySetting]
    RedComet_Aid,

    Flaps_InstructionSet,

    [EncryptedThirdPartySetting]
    Flaps_ApiToken,

    Flaps_ApName,
    Lit_BindField,

    [EncryptedThirdPartySetting]
    Lit_OtpApiKey,
	
	BotBonnie_LineOa,
    BotBonnie_LineBot,
    BotBonnie_BindTagId,
    BotBonnie_BindModuleId,

    [EncryptedThirdPartySetting]
    BotBonnie_ApiToken
}

public static class ThirdPartySettingVendorExtensions
{
    public static ThirdPartySettingKey GetKeyEnum(this ThirdPartySettingVendor vendor, string key)
    {
        return (vendor, key) switch
        {
            // Maac
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.LiffUrl) =>
                ThirdPartySettingKey.Maac_LiffUrl,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.LiffId) =>
                ThirdPartySettingKey.Maac_LiffId,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.LineOa) =>
                ThirdPartySettingKey.Maac_LineOa,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.LineChannelName) =>
                ThirdPartySettingKey.Maac_LineChannelName,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.MaacApiUrl) =>
                ThirdPartySettingKey.Maac_ApiUrl,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.MaacApiKey) =>
                ThirdPartySettingKey.Maac_ApiKey,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.MaacBindTagId) =>
                ThirdPartySettingKey.Maac_BindTagId,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.MaacBindTemplateId) =>
                ThirdPartySettingKey.Maac_BindTemplateId,
            (ThirdPartySettingVendor.Maac, ThirdPartySettingKeyExtensions.MaacCustomerIdField) =>
                ThirdPartySettingKey.Maac_CustomerIdField,
            // Thinker
            (ThirdPartySettingVendor.Thinker, ThirdPartySettingKeyExtensions.ThinkerSiteDomain) =>
                ThirdPartySettingKey.Thinker_SiteDomain,
            (ThirdPartySettingVendor.Thinker, ThirdPartySettingKeyExtensions.ThinkerDepartmentId) =>
                ThirdPartySettingKey.Thinker_DepartmentId,
            (ThirdPartySettingVendor.Thinker, ThirdPartySettingKeyExtensions.ThinkerGiftListApiKey) =>
                ThirdPartySettingKey.Thinker_GiftListApiKey,
            (ThirdPartySettingVendor.Thinker, ThirdPartySettingKeyExtensions.ThinkerGiftSmsApiKey) =>
                ThirdPartySettingKey.Thinker_GiftSmsApiKey,
            (ThirdPartySettingVendor.Thinker, ThirdPartySettingKeyExtensions.ThinkerGiftSmsListId) =>
                ThirdPartySettingKey.Thinker_GiftSmsListId,
            // Shopline
            (ThirdPartySettingVendor.Shopline, ThirdPartySettingKeyExtensions.ShoplineOpenApiKey) =>
                ThirdPartySettingKey.Shopline_OpenApiKey,
            // Utk
            (ThirdPartySettingVendor.Utk, ThirdPartySettingKeyExtensions.UtkApiKey) =>
                ThirdPartySettingKey.Utk_ApiKey,
            // RedComet
            (ThirdPartySettingVendor.RedComet, ThirdPartySettingKeyExtensions.RedCometAid) =>
                ThirdPartySettingKey.RedComet_Aid,
            (ThirdPartySettingVendor.RedComet, ThirdPartySettingKeyExtensions.RedCometAuthIdKey) =>
                ThirdPartySettingKey.RedComet_AuthIdKey,
            // Flaps
            (ThirdPartySettingVendor.Flaps, ThirdPartySettingKeyExtensions.FlapsInstructionSet) =>
                ThirdPartySettingKey.Flaps_InstructionSet,
            (ThirdPartySettingVendor.Flaps, ThirdPartySettingKeyExtensions.FlapsApiToken) =>
                ThirdPartySettingKey.Flaps_ApiToken,
            (ThirdPartySettingVendor.Flaps, ThirdPartySettingKeyExtensions.FlapsApName) =>
                ThirdPartySettingKey.Flaps_ApName,
            // Lit
            (ThirdPartySettingVendor.Lit, ThirdPartySettingKeyExtensions.LitBindField) =>
                ThirdPartySettingKey.Lit_BindField,
            (ThirdPartySettingVendor.Lit, ThirdPartySettingKeyExtensions.LitOtpApiKey) =>
                ThirdPartySettingKey.Lit_OtpApiKey,
			// BotBonnie
            (ThirdPartySettingVendor.BotBonnie, ThirdPartySettingKeyExtensions.LineOa) =>
                ThirdPartySettingKey.BotBonnie_LineOa,
            (ThirdPartySettingVendor.BotBonnie, ThirdPartySettingKeyExtensions.LineBot) =>
                ThirdPartySettingKey.BotBonnie_LineBot,
            (ThirdPartySettingVendor.BotBonnie, ThirdPartySettingKeyExtensions.BotBonnieBindTagId) =>
                ThirdPartySettingKey.BotBonnie_BindTagId,
            (ThirdPartySettingVendor.BotBonnie, ThirdPartySettingKeyExtensions.BotBonnieBindModuleId) =>
                ThirdPartySettingKey.BotBonnie_BindModuleId,
            (ThirdPartySettingVendor.BotBonnie, ThirdPartySettingKeyExtensions.BotBonnieApiToken) =>
                ThirdPartySettingKey.BotBonnie_ApiToken,
            _ => throw new NotImplementedException(),
        };
    }

    public static string ToLabel(this ThirdPartySettingVendor vendor)
    {
        return vendor switch
        {
            ThirdPartySettingVendor.Maac => "maac",
            ThirdPartySettingVendor.Thinker => "thinker",
            ThirdPartySettingVendor.Shopline => "shopline",
            ThirdPartySettingVendor.Utk => "utk",
            ThirdPartySettingVendor.RedComet => "redcomet",
            ThirdPartySettingVendor.Flaps => "flaps",
            ThirdPartySettingVendor.Lit => "lit",
            ThirdPartySettingVendor.BotBonnie => "botbonnie",
            _ => null
        };
    }
}
public static class ThirdPartySettingKeyExtensions
{
    public const string LiffUrl = "liff_url";
    public const string LiffId = "liff_id";
    public const string LineOa = "line_oa";
	public const string LineBot = "line_bot";
    public const string LineChannelName = "line_channel_name";
    public const string MaacApiUrl = "maac_api_url";
    public const string MaacApiKey = "maac_api_key";
    public const string MaacBindTagId = "maac_bind_tag_id";
    public const string MaacBindTemplateId = "maac_bind_template_id";
    public const string MaacCustomerIdField = "maac_customer_id_field";
    public const string ThinkerSiteDomain = "thinker_site_domain";
    public const string ThinkerDepartmentId = "thinker_department_id";
    public const string ThinkerGiftListApiKey = "thinker_gift_list_api_key";
    public const string ThinkerGiftSmsApiKey = "thinker_gfit_sms_api_key";
    public const string ThinkerGiftSmsListId = "thinker_gift_sms_list_id";
    public const string ShoplineOpenApiKey = "shopline_openapi_key";
    public const string UtkApiKey = "utk_api_key";
    public const string RedCometAid = "redcomet_aid";
    public const string RedCometAuthIdKey = "redcomet_auth_id_key";
    public const string FlapsInstructionSet = "flaps_instruction_set";
    public const string FlapsApiToken = "flaps_api_token";
    public const string FlapsApName = "flaps_ap_name";
    public const string LitBindField = "lit_bind_field";
    public const string LitOtpApiKey = "lit_otp_api_key";
	public const string BotBonnieBindTagId = "botbonnie_bind_tag_id";
    public const string BotBonnieBindModuleId = "botbonnie_bind_module_id";
    public const string BotBonnieApiToken = "botbonnie_api_token";

    public static (string vendor, string key) ToLabel(this ThirdPartySettingKey key)
    {
        return key switch
        {
            // Maac
            ThirdPartySettingKey.Maac_LiffUrl =>
                (ThirdPartySettingVendor.Maac.ToLabel(), LiffUrl),
            ThirdPartySettingKey.Maac_LiffId =>
                (ThirdPartySettingVendor.Maac.ToLabel(), LiffId),
            ThirdPartySettingKey.Maac_LineOa =>
                (ThirdPartySettingVendor.Maac.ToLabel(), LineOa),
            ThirdPartySettingKey.Maac_LineChannelName =>
                (ThirdPartySettingVendor.Maac.ToLabel(), LineChannelName),
            ThirdPartySettingKey.Maac_ApiUrl =>
                (ThirdPartySettingVendor.Maac.ToLabel(), MaacApiUrl),
            ThirdPartySettingKey.Maac_ApiKey =>
                (ThirdPartySettingVendor.Maac.ToLabel(), MaacApiKey),
            ThirdPartySettingKey.Maac_BindTagId =>
                (ThirdPartySettingVendor.Maac.ToLabel(), MaacBindTagId),
            ThirdPartySettingKey.Maac_BindTemplateId =>
                (ThirdPartySettingVendor.Maac.ToLabel(), MaacBindTemplateId),
            ThirdPartySettingKey.Maac_CustomerIdField =>
                (ThirdPartySettingVendor.Maac.ToLabel(), MaacCustomerIdField),
            // Thinker
            ThirdPartySettingKey.Thinker_SiteDomain =>
                (ThirdPartySettingVendor.Thinker.ToLabel(), ThinkerSiteDomain),
            ThirdPartySettingKey.Thinker_DepartmentId =>
                (ThirdPartySettingVendor.Thinker.ToLabel(), ThinkerDepartmentId),
            ThirdPartySettingKey.Thinker_GiftListApiKey =>
                (ThirdPartySettingVendor.Thinker.ToLabel(), ThinkerGiftListApiKey),
            ThirdPartySettingKey.Thinker_GiftSmsApiKey =>
                (ThirdPartySettingVendor.Thinker.ToLabel(), ThinkerGiftSmsApiKey),
            ThirdPartySettingKey.Thinker_GiftSmsListId =>
                (ThirdPartySettingVendor.Thinker.ToLabel(), ThinkerGiftSmsListId),
            // Shopline
            ThirdPartySettingKey.Shopline_OpenApiKey =>
                (ThirdPartySettingVendor.Shopline.ToLabel(), ShoplineOpenApiKey),
            // Utk
            ThirdPartySettingKey.Utk_ApiKey =>
                (ThirdPartySettingVendor.Utk.ToLabel(), UtkApiKey),
            // RedComet
            ThirdPartySettingKey.RedComet_Aid =>
                (ThirdPartySettingVendor.RedComet.ToLabel(), RedCometAid),
            ThirdPartySettingKey.RedComet_AuthIdKey =>
                (ThirdPartySettingVendor.RedComet.ToLabel(), RedCometAuthIdKey),
            // Flaps
            ThirdPartySettingKey.Flaps_InstructionSet =>
                (ThirdPartySettingVendor.Flaps.ToLabel(), FlapsInstructionSet),
            ThirdPartySettingKey.Flaps_ApiToken =>
                (ThirdPartySettingVendor.Flaps.ToLabel(), FlapsApiToken),
            ThirdPartySettingKey.Flaps_ApName =>
                (ThirdPartySettingVendor.Flaps.ToLabel(), FlapsApName),
            // Lit
            ThirdPartySettingKey.Lit_BindField =>
                (ThirdPartySettingVendor.Lit.ToLabel(), LitBindField),
            ThirdPartySettingKey.Lit_OtpApiKey =>
                (ThirdPartySettingVendor.Lit.ToLabel(), LitOtpApiKey),
			// BotBonnie
            ThirdPartySettingKey.BotBonnie_LineOa =>
                (ThirdPartySettingVendor.BotBonnie.ToLabel(), LineOa),
            ThirdPartySettingKey.BotBonnie_LineBot =>
                (ThirdPartySettingVendor.BotBonnie.ToLabel(), LineBot),
            ThirdPartySettingKey.BotBonnie_BindTagId =>
                (ThirdPartySettingVendor.BotBonnie.ToLabel(), BotBonnieBindTagId),
            ThirdPartySettingKey.BotBonnie_BindModuleId =>
                (ThirdPartySettingVendor.BotBonnie.ToLabel(), BotBonnieBindModuleId),
            ThirdPartySettingKey.BotBonnie_ApiToken =>
                (ThirdPartySettingVendor.BotBonnie.ToLabel(), BotBonnieApiToken),
            _ => (null, null)
        };
    }
}

public static class ThirdPartyExtensions
{
    public static ThirdPartySettingVendor GetVendorEnum(this ThirdParty thirdParty)
    {
        return thirdParty.VendorId.ParseEnum<ThirdPartySettingVendor>(true);
    }

    public static ThirdPartySettingKey GetKeyEnum(this ThirdParty thirdParty)
    {
        return thirdParty.GetVendorEnum().GetKeyEnum(thirdParty.Key);
    }

    public static string GetEncryptedValue(this ThirdParty thirdParty)
    {
        if (thirdParty.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        var aesKey = AesKey;
        if (aesKey == null)
        {
            throw new SystemException("Unable to get the environment variable AesKey.");
        }

        var iv = thirdParty.Key;
        var encrypted = EncryptHelper.EncryptAES(thirdParty.Value, aesKey, iv);

        return encrypted;
    }

    public static string GetDecryptedValue(this ThirdParty thirdParty)
    {
        if (thirdParty.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        var keyEnum = thirdParty.GetKeyEnum();
        var isEncrypted = keyEnum
            .GetType()
            .GetField(keyEnum.ToString())
            .GetCustomAttributes<EncryptedThirdPartySettingAttribute>()
            .FirstOrDefault() != null;
        if (!isEncrypted)
        {
            return thirdParty.Value;
        }

        var aesKey = AesKey;
        if (aesKey == null)
        {
            throw new SystemException("Unable to get the environment variable AesKey.");
        }

        var iv = thirdParty.Key;
        var decrypted = EncryptHelper.DecryptAES(thirdParty.Value, aesKey, iv);

        return decrypted;
    }
}


[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class EncryptedThirdPartySettingAttribute : Attribute
{
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

public static class StringHelper
{
	public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
}

public static class EnumHelper
{
	/// <summary>
    /// 將字串轉換為 enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="ignoreCase"></param>
    /// <returns></returns>
    public static T ParseEnum<T>(this string source, bool ignoreCase = false)
    {
        return (T)Enum.Parse(typeof(T), source, ignoreCase);
    }
}