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
	var sql = "select * from Employees";
	this.Connection.GenerateInsertCommand(sql, "Employees").Dump();
}

// Define other methods, classes and namespaces here
public static class LINQPadExtensions
{
	public static string GenerateInsertCommand(this IDbConnection connection, string sql, string tableName = "TableName")
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
            if (reader.FieldCount <= 1)
            {
                continue;
            }

            builder.AppendFormat("INSERT INTO [dbo].[{0}]{1}", tableName, Environment.NewLine);
            builder.AppendLine("(");

            var schema = reader.GetSchemaTable();
            var columnNames = new List<string>();

            foreach (DataRow row in schema.Rows)
            {
                var columnName = (string)row["ColumnName"];
                columnNames.Add(columnName);
            }

            foreach (var columnName in columnNames)
            {
                builder.AppendFormat("    [{0}]{1}{2}",
                    columnName,
                    columnNames.IndexOf(columnName).Equals(columnNames.Count - 1) ? "" : ",",
                    Environment.NewLine);
            }

            builder.AppendLine(")");
            builder.AppendLine("VALUES");
            builder.AppendLine("(");

            foreach (var columnName in columnNames)
            {
                builder.AppendFormat("    @{0}{1}{2}",
                    columnName,
                    columnNames.IndexOf(columnName).Equals(columnNames.Count - 1) ? "" : ",",
                    Environment.NewLine);
            }

            builder.AppendLine(");");
            builder.AppendLine();
        }

        while (reader.NextResult());

        return builder.ToString();
    }
}