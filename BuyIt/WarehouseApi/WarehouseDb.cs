namespace WarehouseApi;

public class WarehouseDb(string connectionString)
{
    public Npgsql.NpgsqlConnection Open() => new(connectionString);
}
