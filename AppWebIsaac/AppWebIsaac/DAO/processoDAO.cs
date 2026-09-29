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
        try
        {
            using var con = _conexao.GetConnection();
            con.Open();

            string sql = @"INSERT INTO processos
                (numero_pro, data_pro, interessado_pro, assunto_pro, descricao_pro, situacao_pro)
                VALUES
                (@numero, @data, @interessado, @assunto, @descricao, @situacao)";

            using var comando = con.CreateCommand();
            comando.CommandText = sql;
            comando.Parameters.AddWithValue("@numero", processo.Numero);
            comando.Parameters.AddWithValue("@data", processo.Data!.Value.ToDateTime(TimeOnly.MinValue));
            comando.Parameters.AddWithValue("@interessado", processo.Interessado);
            comando.Parameters.AddWithValue("@assunto", processo.Assunto);
            comando.Parameters.AddWithValue("@descricao", processo.Descricao);
            comando.Parameters.AddWithValue("@situacao", processo.Situacao);

            comando.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
    }

    public List<Processo> Listar()
    {
        var lista = new List<Processo>();

        using var con = _conexao.GetConnection();
        con.Open();

        using var comando = con.CreateCommand();
        comando.CommandText = "SELECT * FROM processos ORDER BY 1 DESC";

        using var leitor = comando.ExecuteReader();

        var colunas = Enumerable.Range(0, leitor.FieldCount).Select(leitor.GetName).ToList();
        int idxId = colunas.IndexOf("id_pro");
        if (idxId < 0) idxId = colunas.IndexOf("id");
        if (idxId < 0) idxId = 0;

        string Texto(string coluna)
        {
            int i = leitor.GetOrdinal(coluna);
            return leitor.IsDBNull(i) ? string.Empty : leitor.GetString(i);
        }

        while (leitor.Read())
        {
            int iData = leitor.GetOrdinal("data_pro");

            lista.Add(new Processo
            {
                Id = Convert.ToInt32(leitor.GetValue(idxId)),
                Numero = Texto("numero_pro"),
                Data = leitor.IsDBNull(iData) ? null : DateOnly.FromDateTime(leitor.GetDateTime(iData)),
                Interessado = Texto("interessado_pro"),
                Assunto = Texto("assunto_pro"),
                Descricao = Texto("descricao_pro"),
                Situacao = Texto("situacao_pro")
            });
        }

        return lista;
    }
}
