namespace ProductApi;

public class ProductDb(string connectionString)
{
    public Npgsql.NpgsqlConnection Open() => new(connectionString);
}
