<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var result = EnumExtensions.GetAllFlagEnumCombinationsV1<ColumnType>();
	var count = result.Count();
	
	count.Dump();
	result.Dump();
}

// You can define other methods, fields, classes and namespaces here
[Flags]
public enum ColumnType
{
    None = 0,

    /// <summary>
    /// 基礎值 2^0
    /// </summary>
    A = 1,

    /// <summary>
    /// 基礎值 2^1
    /// </summary>
    B = 2,

    /// <summary>
    /// 基礎值 2^2
    /// </summary>
    C = 4,

    /// <summary>
    /// 基礎值 2^3
    /// </summary>
    D = 8,

	/// <summary>
    /// 基礎值 2^4
    /// </summary>
    E = 16
}

public static class EnumExtensions
{
	/// <summary>
    /// 取得 FlagsEnum 所有組合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllFlagEnumCombinationsV1<T>()
        where T : struct, IConvertible
    {
        if (typeof(T).BaseType != typeof(Enum))
            throw new ArgumentException("T must be an Enum type");

        // The return type of Enum.GetValues is Array but it is effectively int[] per docs
        // This bit converts to int[]
        var values = Enum.GetValues(typeof(T)).Cast<int>().ToArray();

        if (!typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Any())
        {
            // We don't have flags so just return the result of GetValues
            return values.Cast<T>().ToList();
        }

        var valuesInverted = values.Select(v => ~v).ToArray();
        int max = 0;
        for (int i = 0; i < values.Length; i++)
        {
            max |= values[i];
        }

        var result = new List<T>();
        for (int i = 0; i <= max; i++)
        {
            int unaccountedBits = i;
            for (int j = 0; j < valuesInverted.Length; j++)
            {
                // This step removes each flag that is set in one of the Enums
                // thus ensuring that an Enum with missing bits won't be passed an int that has those bits set
                unaccountedBits &= valuesInverted[j];
                if (unaccountedBits == 0)
                {
                    result.Add((T)(object)i);
                    break;
                }
            }
        }

        //Check for zero
        try
        {
            if (string.IsNullOrEmpty(Enum.GetName(typeof(T), (T)(object)0)))
            {
                result.Remove((T)(object)0);
            }
        }
        catch
        {
            result.Remove((T)(object)0);
        }

        return result;
    }

    /// <summary>
    /// 取得 FlagsEnum 所有組合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllFlagEnumCombinationsV2<T>()
    {
        var values = Enum.GetValues(typeof(T)).Cast<int>().ToArray();
        if (!typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Any())
        {
            // We don't have flags so just return the result of GetValues
            return values.Cast<T>().ToList();
        }

        var highestEnum = Enum.GetValues(typeof(T)).Cast<int>().Max();
        var upperBound = highestEnum * 2;
        var result = new List<T>();
        for (int i = 0; i < upperBound; i++)
        {
            result.Add((T)(object)i);
        }

        return result;
    }
}