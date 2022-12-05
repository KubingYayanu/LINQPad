<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Text.Unicode</Namespace>
</Query>

void Main()
{
	var apiKey = "UTK";
	var baseRequest = GetAuthenticationRequest(apiKey);
	var requestJson = baseRequest.ToJson(escapeAllSymbol: true);
    var csKey = HMACSHA1HashHelper.HashBase64(apiKey, requestJson);
    Console.WriteLine(requestJson);
	Console.WriteLine();
	Console.WriteLine(csKey);
}

// You can define other methods, fields, classes and namespaces here
private static BaseRequest GetAuthenticationRequest(string apiKey)
{
    var now = DateTime.Now;
    var transNo = Guid.NewGuid().ToString("N").ToUpper();
    var sysKey = HMACSHA1HashHelper.HashBase64(apiKey, transNo);
    var request = new BaseRequest
    {
        TransNo = transNo,
        SysKey = sysKey,
        SysDate = now.ToFormatted(TimeConstants.yyyyMMdd),
        SysTime = now.ToFormatted(TimeConstants.HHmmss)
    };
    return request;
}

public class BaseRequest
{
    /// <summary>
    /// 交易序號, 安全碼加密字串; 由串接廠商提供
    /// </summary>
    [JsonPropertyName("trans_no")]
    public string TransNo { get; set; }

    /// <summary>
    /// 安全碼
    /// </summary>
    [JsonPropertyName("sys_key")]
    public string SysKey { get; set; }

    /// <summary>
    /// 系統日期 yyyyMMdd(西元年); ex:20160101
    /// </summary>
    [JsonPropertyName("sys_date")]
    public string SysDate { get; set; }

    /// <summary>
    /// 系統時間 hhmmss(時分秒); ex:141025
    /// </summary>
    [JsonPropertyName("sys_time")]
    public string SysTime { get; set; }
}

public class HMACSHA1HashHelper
{
    public static string GenerateKey()
    {
        var hmac = new HMACSHA1();
        return Convert.ToBase64String(hmac.Key);
    }

    public static byte[] Hash(string key, string source)
    {
        var sourceByte = Encoding.UTF8.GetBytes(source);
        var keyByte = Encoding.UTF8.GetBytes(key);
        var hmac = new HMACSHA1(keyByte);
        return hmac.ComputeHash(sourceByte);
    }

    public static string HashBase64(string key, string source)
    {
        var hashResult = Hash(key, source);
        return Convert.ToBase64String(hashResult);
    }

    public static string HashHex(string key, string source)
    {
        var hashResult = Hash(key, source);
        return BitConverter.ToString(hashResult).Replace("-", "").ToLower();
    }
}

public static class DateTimeHelper
{
    private static readonly string[] iso8601Formats = {
        // Basic formats
        "yyyyMMddTHHmmsszzz",
        "yyyyMMddTHHmmsszz",
        "yyyyMMddTHHmmssZ",
        // Extended formats
        "yyyy-MM-ddTHH:mm:sszzz",
        "yyyy-MM-ddTHH:mm:sszz",
        "yyyy-MM-ddTHH:mm:ssZ",
        // All of the above with reduced accuracy
        "yyyyMMddTHHmmzzz",
        "yyyyMMddTHHmmzz",
        "yyyyMMddTHHmmZ",
        "yyyy-MM-ddTHH:mmzzz",
        "yyyy-MM-ddTHH:mmzz",
        "yyyy-MM-ddTHH:mmZ",
        // Accuracy reduced to hours
        "yyyyMMddTHHzzz",
        "yyyyMMddTHHzz",
        "yyyyMMddTHHZ",
        "yyyy-MM-ddTHHzzz",
        "yyyy-MM-ddTHHzz",
        "yyyy-MM-ddTHHZ",
        // Only date
        "yyyyMMdd",
        "yyyy-MM-dd",
        "yyyy-MM",
        "yyyyMM"
    };

    public static bool IsIso8601(this string source)
    {
        var isIso8601 = DateTime.TryParseExact(source, iso8601Formats,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime datetime);
        return isIso8601;
    }

    public static DateTime? ToExactFormatDateTime(string dateString)
    {
        return ToExactFormatDateTime(dateString, iso8601Formats);
    }

    public static DateTime? ToExactFormatDateTime(string dateString, string format)
    {
        return ToExactFormatDateTime(dateString, new string[] { format });
    }

    public static DateTime? ToExactFormatDateTime(string dateString, string[] formats)
    {
        DateTime? datetime = null;
        if (!dateString.IsNullOrWhiteSpace())
        {
            var isValid = DateTime.TryParseExact(
                dateString,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime output);
            if (isValid)
            {
                datetime = output;
            }
        }

        return datetime;
    }

    public static DateTime? ToDateTime(string dateString)
    {
        DateTime? datetime = null;
        if (!dateString.IsNullOrWhiteSpace())
        {
            var isValid = DateTime.TryParse(
                dateString,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime output);
            if (isValid)
            {
                datetime = output;
            }
        }

        return datetime;
    }

    /// <summary>
    /// 預設轉為 yyyy/MM/dd HH:mm:ss 格式字串
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string ToFormatted(this DateTime time, string format = "yyyy/MM/dd HH:mm:ss")
    {
        return time.ToString(format);
    }

    public static string ToFormattedHyphen(this DateTime time)
    {
        return time.ToFormatted(TimeConstants.yyyyMMdd_hyphen_HHmmss);
    }

    public static string ToFormattedYmdHyphen(this DateTime time)
    {
        return time.ToFormatted(TimeConstants.yyyyMMdd_hyphen);
    }
}

public static class TimeConstants
{
    /// <summary>
    /// ISO 8601; 2022-02-17T00:00:00+08:00
    /// </summary>
    public const string IsoDateTime = "yyyy-MM-ddTHH:mm:sszzz";

    /// <summary>
    /// 20190407
    /// </summary>
    public const string yyyyMMdd = "yyyyMMdd";

    /// <summary>
    /// 20190407192235
    /// </summary>
    public const string yyyyMMddHHmmss = "yyyyMMddHHmmss";

    /// <summary>
    /// 201904
    /// </summary>
    public const string yyyyMM = "yyyyMM";

    #region Hyphen

    /// <summary>
    /// 2019-04-07
    /// </summary>
    public const string yyyyMMdd_hyphen = "yyyy-MM-dd";

    /// <summary>
    /// 2019-4-7
    /// </summary>
    public const string yyyyMd_hyphen = "yyyy-M-d";

    /// <summary>
    /// 2019-04-07 19:22:35
    /// </summary>
    public const string yyyyMMdd_hyphen_HHmmss = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// 2019-04-07 19:22
    /// </summary>
    public const string yyyyMMdd_hyphen_HHmm = "yyyy-MM-dd HH:mm";

    #endregion Hyphen

    #region Slash

    /// <summary>
    /// sample: 2019/04/07 19:22:59
    /// </summary>
    public const string yyyyMMdd_slash_HHmmss = "yyyy/MM/dd HH:mm:ss";

    /// <summary>
    /// 2019/04/07 19:22
    /// </summary>
    public const string yyyyMMdd_slash_HHmm = "yyyy/MM/dd HH:mm";

    /// <summary>
    /// 2019/04/07
    /// </summary>
    public const string yyyyMMdd_slash = "yyyy/MM/dd";

    #endregion Slash

    /// <summary>
    /// 1559599876
    /// </summary>
    public const string HHmmssffff = "HHmmssffff";

    /// <summary>
    /// 155959
    /// </summary>
    public const string HHmmss = "HHmmss";
}

public static class StringHelper
{
	public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
	
	public static string ToJson<T>(
        this T source,
        bool isCamelCase = false,
        bool escapeAllSymbol = false,
        bool ignoreDefault = false)
        where T : class, new()
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            Encoder = escapeAllSymbol ? JavaScriptEncoder.UnsafeRelaxedJsonEscaping : null,
            PropertyNamingPolicy = isCamelCase ? System.Text.Json.JsonNamingPolicy.CamelCase : null
        };

        return System.Text.Json.JsonSerializer.Serialize(source, options);
    }
}