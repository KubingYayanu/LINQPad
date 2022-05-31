<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Text.Encodings.Web</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
  <Namespace>System.Text.Unicode</Namespace>
</Query>

void Main()
{	
	var apiKey = "UTK";
	var now = DateTime.Now;
	var sysDate = now.ToString("yyyyMMdd");
	var sysTime = now.ToString("HHmmss");
	var transNo = Guid.NewGuid().ToString("N").ToUpper();
	var sysKey = HMACSHA1HashHelper.HashBase64(apiKey, transNo);
	var request = GetCreateRequest();
	request.TransNo = transNo;
    request.SysKey = sysKey;
    request.SysDate = sysDate;
    request.SysTime = sysTime;
	
	var requestJson = request.ToJson(escapeAllSymbol: true);
	var csKey = HMACSHA1HashHelper.HashBase64(apiKey, requestJson);
	requestJson.Dump();
	csKey.Dump();
}

// You can define other methods, fields, classes and namespaces here
private CreateMemberRequest GetCreateRequest()
{
	return new CreateMemberRequest(
		mobile: "0944544654",
		name: "KuYu",
		email: "kuyu@gmail.com",
		channel: "培芝_高雄漢神巨蛋",
		birthday: "19970214");
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

public class CreateMemberRequest : BaseRequest
{
    public CreateMemberRequest()
    {
    }

    public CreateMemberRequest(
        string mobile,
        string name,
        string email,
        string channel,
        string birthday)
    {
        Mobile = mobile;
        Name = name;
        Email = email;
        Channel = channel;
        Birthday = birthday;
    }

    /// <summary>
    /// 交易類型; 查詢: Q, 新增會員: M
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "M";

    /// <summary>
    /// 手機
    /// </summary>
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// 電子郵件(非必填)
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }

    /// <summary>
    /// 通路
    /// </summary>
    [JsonPropertyName("path")]
    public string Channel { get; set; }

    /// <summary>
    /// 生日; yyyyMMdd(西元年)
    /// </summary>
    [JsonPropertyName("birthday")]
    public string Birthday { get; set; }
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
        return BitConverter.ToString(hashResult).Replace("-", string.Empty);
    }
}

public static class StringHelper
{
	public static string ToJson<T>(
        this T source,
        bool isCamelCase = false,
        bool escapeAllSymbol = false)
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