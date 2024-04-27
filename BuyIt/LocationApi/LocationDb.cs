namespace LocationApi;

public class LocationDb(string connectionString)
{
    public Npgsql.NpgsqlConnection Open() => new(connectionString);
}
