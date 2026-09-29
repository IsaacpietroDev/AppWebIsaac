
using MySql.Data.MySqlClient;

namespace AppWebIsaac.DAO;

public class Conexao
{
    private readonly IConfiguration _configuration;

    public Conexao(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MySqlConnection GetConnection()
    {
        var connectionString = _configuration.GetConnectionString("MySqlConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("A string de conexão MySqlConnection não foi configurada.");

        return new MySqlConnection(connectionString);
    }
}
