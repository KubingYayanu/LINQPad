<Query Kind="Program">
  <Connection>
    <ID>ca48f61b-acfd-4aec-864f-47ef9f8d5cd2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>dev</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAOUqWty4W/U+oYWVmb3ygRwAAAAACAAAAAAADZgAAwAAAABAAAAAhVdXXfWO4NK52XliS0IhKAAAAAASAAACgAAAAEAAAAMsyDQvmHSTped2iS4IyKxwIAAAAJd25LphDbBcUAAAAkNNfa3Qulr2VPrwAV6waDci+m9g=</Password>
    <Database>Northwind</Database>
  </Connection>
</Query>

void Main()
{
	var sql = "select * from Territories";
	this.Connection.DumpClass(sql, "Territories").Dump();
}

// Define other methods, classes and namespaces here
public static class LINQPadExtensions
{
	private static readonly Dictionary<Type, string> typeAliases =
        new Dictionary<Type, string>
        {
            { typeof(int), "int" },
            { typeof(short), "short" },
            { typeof(byte), "byte" },
            { typeof(byte[]), "byte[]" },
            { typeof(long), "long" },
            { typeof(double), "double" },
            { typeof(decimal), "decimal" },
            { typeof(float), "float" },
            { typeof(bool), "bool" },
            { typeof(string), "string" }
        };

    private static readonly HashSet<Type> nullableTypes =
        new HashSet<Type>
        {
            typeof(int),
            typeof(short),
            typeof(long),
            typeof(double),
            typeof(decimal),
            typeof(float),
            typeof(bool),
            typeof(DateTime)
        };

    /// <summary>
    /// 從 SQL 查詢結果動態產生 C# POCO
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="sql"></param>
    /// <param name="className">POCO 名稱</param>
    /// <returns></returns>
    public static string DumpClass(this IDbConnection connection, string sql, string className = "Info")
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        var reader = cmd.ExecuteReader();

        var builder = new StringBuilder();
        do
        {
            if (reader.FieldCount <= 1) continue;

            builder.AppendFormat("public class {0}{1}", className, Environment.NewLine);
            builder.AppendLine("{");
            var schema = reader.GetSchemaTable();

            foreach (DataRow row in schema.Rows)
            {
                var type = (Type)row["DataType"];
                var name = typeAliases.ContainsKey(type) ? typeAliases[type] : type.Name;
                var isNullable = (bool)row["AllowDBNull"] && nullableTypes.Contains(type);
                var collumnName = (string)row["ColumnName"];

                builder.AppendLine(string.Format("\tpublic {0}{1} {2} {{ get; set; }}", name, isNullable ? "?" : string.Empty, collumnName));
                builder.AppendLine();
            }

            builder.AppendLine("}");
            builder.AppendLine();
        } while (reader.NextResult());

        return builder.ToString();
    }
}