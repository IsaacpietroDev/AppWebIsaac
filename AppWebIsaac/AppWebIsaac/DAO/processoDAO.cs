using AppWebIsaac.Model;
using MySql.Data.MySqlClient;

namespace AppWebIsaac.DAO;

public class ProcessoDAO
{
    private readonly Conexao _conexao;

    public ProcessoDAO(Conexao conexao)
    {
        _conexao = conexao;
    }

    public void Inserir(Processo processo)
    {
        using var con = _conexao.GetConnection();
        con.Open();

        using var cmd = con.CreateCommand();
        cmd.CommandText = """
            INSERT INTO processos
                (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro)
            VALUES
                (@numero, @data, @interessado, @assunto, @descricao, @situacao)
            """;

        cmd.Parameters.AddWithValue("@numero", processo.Numero);
        cmd.Parameters.AddWithValue("@data", processo.Data!.Value.ToDateTime(TimeOnly.MinValue));
        cmd.Parameters.AddWithValue("@interessado", processo.Interessado);
        cmd.Parameters.AddWithValue("@assunto", processo.Assunto);
        cmd.Parameters.AddWithValue("@descricao", processo.Descricao);
        cmd.Parameters.AddWithValue("@situacao", processo.Situacao);

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
    }
}
