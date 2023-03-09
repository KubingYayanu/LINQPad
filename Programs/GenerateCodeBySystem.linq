<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	//for (var i = 0; i < 290; i++)
	//{
	//	var giftCode = RandomHelper.RandomString(
	//        codePrefix: "03RG",
	//        generateType: DefaultConstants.SystemGeneratedCode.CodeType,
	//        totalLength: 12);
	//	giftCode.Dump();
	//}
	
	var gotGiftWayGroup = RandomHelper.RandomString(
        "",
        RandomHelper.GenerateTypeEnum.Code128,
        30);
	gotGiftWayGroup.Dump();
}

// You can define other methods, fields, classes and namespaces here

public static class RandomHelper
{
	private const string _strLowerEnglish = "abcdefghijklmnopqrstuvwxyz";
    private const string _strUpperEnglish = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string _strNumber = "0123456789";
    private const string _strCode39_128_Symbol = @"-.$/+%"; //Code 39 和 Code 128 都有的特殊符號
    private const string _strCode128_Symbol = @"!""&\'()*,:;<=>?@[]^_`{|}~"; //Code 128 才有的特殊符號

	/// <summary>
    /// 在指定數字區間內,隨機取得數字
    /// </summary>
    /// <param name="maxValue">最大數字(不包含)</param>
    /// <returns></returns>
    public static int RandomInt(int maxValue)
    {
        //避免 Generator.Next 一直回傳 0, 所以要 lock
        lock (Generator) return Generator.Next(maxValue);
    }

	/// <summary>產生一組亂數字串(數字、英文字母與特殊符號混和)</summary>
    /// <param name="codePrefix">產生的字串的固定前綴(若不要前綴字則傳入空值)</param>
    /// <param name="generateType">產生類型(使用GenerateTypeEnum)</param>
    /// <param name="totalLength">待生成的總長度</param>
    /// <returns>string: 產生的字串</returns>
    public static string RandomString(string codePrefix, GenerateTypeEnum generateType, int totalLength)
    {
        return RandomString(codePrefix, GetAllowWord(generateType), totalLength);
    }
	
	/// <summary>產生一組亂數字串(數字、英文字母與特殊符號混和)</summary>
    /// <param name="codePrefix">產生的字串的固定前綴(若不要前綴字則傳入空值)</param>
    /// <param name="allowWord">允許的字元</param>
    /// <param name="totalLength">待生成的總長度</param>
    /// <returns>string: 產生的字串</returns>
    public static string RandomString(string codePrefix, string allowWord, int totalLength)
    {
        string randomResult = string.Empty;
        //固定前綴防呆: 去除空白的字元
        string generatePrefix = codePrefix == null ? string.Empty : codePrefix.Replace(" ", "");
        //沒有傳入生成位數, 表示有問題就直接回傳空值, 不在往下執行
        if (totalLength == 0)
        {
            return randomResult;
        }
        //沒有允許的字元, 表示有問題就直接回傳空值, 不在往下執行
        if (string.IsNullOrWhiteSpace(allowWord))
        {
            return randomResult;
        }
        //generateLength: 實際要產生亂數的位數, 必須扣掉固定前綴字位數
        int generateLength = totalLength;
        if (!string.IsNullOrWhiteSpace(generatePrefix))
        {
            generateLength = generateLength - generatePrefix.Length;
        }
        //如果 generateLength<=0 表示有問題就直接回傳空值, 不在往下執行
        if (generateLength <= 0)
        {
            return randomResult;
        }

        //允許生成的字元
        string readyStr = allowWord;
        int readyStr_EndIdx = readyStr.Length - 1;
        var buffer = new char[generateLength];
        for (int i = 0; i < generateLength; i++)
        {
            //改用 RandomInt, 避免直接使用 Generator.Next
            buffer[i] = readyStr[RandomInt(readyStr.Length) % readyStr_EndIdx];
        }
        randomResult = new string(buffer);

        return $"{generatePrefix}{randomResult}";
    }
	
	/// <summary>取得產生類型的允許字元</summary>
    /// <param name="generateType">產生類型(使用GenerateTypeEnum)</param>
    public static string GetAllowWord(GenerateTypeEnum generateType)
    {
        string readyStr;
        switch (generateType)
        {
            case GenerateTypeEnum.English_Number:
                readyStr = $"{_strLowerEnglish}{_strUpperEnglish}{_strNumber}";
                break;

            case GenerateTypeEnum.Code39:
                readyStr = $"{_strUpperEnglish}{_strNumber}{_strCode39_128_Symbol}";
                break;

            case GenerateTypeEnum.Code128:
                readyStr = $"{_strLowerEnglish}{_strUpperEnglish}{_strNumber}{_strCode39_128_Symbol}{_strCode128_Symbol}";
                break;

            case GenerateTypeEnum.UpperEnglish_Number:
            default:
                readyStr = $"{_strUpperEnglish}{_strNumber}";
                break;
        }
        return readyStr;
    }
	
	/// <summary>Random generator (注意要 Lock)</summary>
    public static Random Generator { get; } = new Random(SystemRandomInt());
	
	/// <summary>系統隨機產生數字</summary>
    public static int SystemRandomInt()
    {
        return BitConverter.ToInt32(SystemRandomBytes(4), 0);
    }
	
	/// <summary>系統產生隨機的亂數 bytes</summary>
    /// <param name="maxLength">最大位元數</param>
    /// <returns>byte[]</returns>
    public static byte[] SystemRandomBytes(int maxLength)
    {
        byte[] buffer = new byte[maxLength];
        //長度的決定就由 byte 陣列大小來決定，因為一個byte可以生成2個字元的HEX碼，
        //所以byte[16]可以生成32字元HEX碼，byte[32] 可以生成64字元的HEX碼，依此類推。
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(buffer);
        }
        return buffer;
    }
	
	/// <summary>
    /// 序號產生的方式
    /// [Description]目前有再拋出錯誤訊息用, 請注意不要亂更改
    /// </summary>
    public enum GenerateTypeEnum
    {
        [Description("大寫英文和數字")]
        UpperEnglish_Number = 1,

        [Description("英文和數字")]
        English_Number = 2,

        [Description("英文、數字和少數特殊符號")]
        Code39 = 3,

        [Description("英文、數字和ASCII特殊符號")]
        Code128 = 4
    }
}

public class DefaultConstants
{
	public static class SystemGeneratedCode
	{
		// 系統生成的序號方式:大寫英文和數字
		public const GenerateTypeEnum CodeType = GenerateTypeEnum.UpperEnglish_Number;
	}
}



/// <summary>
/// 序號產生的方式
/// [Description]目前有再拋出錯誤訊息用, 請注意不要亂更改
/// </summary>
public enum GenerateTypeEnum
{
    [Description("大寫英文和數字")]
    UpperEnglish_Number = 1,

    [Description("英文和數字")]
    English_Number = 2,

    [Description("英文、數字和少數特殊符號")]
    Code39 = 3,

    [Description("英文、數字和ASCII特殊符號")]
    Code128 = 4
}