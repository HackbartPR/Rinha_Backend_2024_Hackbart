using Dapper;
using Npgsql;
using Rinha_Backend_2024.Domain.Config.Exceptions;
using Rinha_Backend_2024.Domain.Entities.Cliente;
using Rinha_Backend_2024.Domain.Repositories.Cliente;
using Rinha_Backend_2024.Infrastructure.Models.Cliente;
using ClienteEntity = Rinha_Backend_2024.Domain.Entities.Cliente.Cliente;
using SaldoEntity = Rinha_Backend_2024.Domain.Entities.Shared.Saldo;


namespace Rinha_Backend_2024.Infrastructure.Repositories.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        private const string TABLE_CLIENTE_NAME = "\"Clientes\"";
        private const string TABLE_CLIENTE_TRANSACTION_NAME = "\"Transacoes\"";
        private NpgsqlConnection _connection;

        public ClienteRepository(IConfigurationRoot configuration) 
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
            _connection.Open();
        }

        public async Task<ClienteEntity> ConsultarAsync(int id)
        {
            string query = $"SELECT * FROM {TABLE_CLIENTE_NAME} WHERE \"Id\" = @id;";
            var queryArguments = new { id };

            ClienteModel cliente = await _connection.QueryFirstOrDefaultAsync<ClienteModel>(query, queryArguments)
                ?? throw new NotFoundException();

            return new ClienteEntity(cliente.Id, new SaldoEntity(cliente.Limite, cliente.Saldo));
        }

        public async Task TransacionarAsync(ClienteEntity cliente, ClienteTransacao transacao)
        {
            //ISOLATION LEVEL SERIALIZABLE, READ WRITE; 
            string query = $"BEGIN TRANSACTION; " +
                $"UPDATE {TABLE_CLIENTE_NAME} SET \"Limite\" = @Limite,  \"Saldo\" = @Saldo WHERE \"Id\" = @Id; " +
                $"INSERT INTO {TABLE_CLIENTE_TRANSACTION_NAME} (\"ClienteId\", \"Valor\", \"Tipo\", \"Descricao\", \"Realizada_Em\")" +
                $"VALUES (@clienteId, @valor, @tipo, @descricao, @realizada_em);" +
                $"END TRANSACTION;";

            var queryArguments = new 
            {
                cliente.Id,
                cliente.Saldo.Limite,
                Saldo = cliente.Saldo.Valor,
                transacao.ClienteId,
                transacao.Transacao.Valor,
                transacao.Transacao.Tipo,
                transacao.Transacao.Descricao,
                Realizada_Em = DateTime.UtcNow
            };

            var teste = await _connection.ExecuteAsync(query, queryArguments);
        }
    }
}
