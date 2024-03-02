using Dapper;
using Npgsql;
using Rinha_Backend_2024.Domain.Repositories.Extrato;
using Rinha_Backend_2024.Infrastructure.Models.Cliente;
using SaldoEntity = Rinha_Backend_2024.Domain.Entities.Shared.Saldo;
using ExtratoSaldoEntity = Rinha_Backend_2024.Domain.Entities.Extrato.ExtratoSaldo;
using ClienteEntity = Rinha_Backend_2024.Domain.Entities.Cliente.Cliente;
using ExtratoEntity = Rinha_Backend_2024.Domain.Entities.Extrato.Extrato;
using Rinha_Backend_2024.Domain.Entities.Shared;
using System.Collections.Generic;

namespace Rinha_Backend_2024.Infrastructure.Repositories.Extrato
{
    public class ExtratoRepository : IExtratoRepository
    {
        private const string TABLE_CLIENTE_TRANSACTION_NAME = "\"Transacoes\"";
        private NpgsqlConnection _connection;

        public ExtratoRepository(IConfigurationRoot configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
            _connection.Open();
        }

        public async Task<ExtratoEntity> ConsultarAsync(ClienteEntity cliente)
        {
            string query = $"SELECT \"Valor\", \"Tipo\", \"Descricao\", \"Realizada_Em\" " +
                $"FROM {TABLE_CLIENTE_TRANSACTION_NAME} WHERE \"ClienteId\" = @clienteId " +
                $"ORDER BY \"Realizada_Em\" DESC LIMIT 10 ;";

            var queryArguments = new { ClienteId = cliente.Id };
            IEnumerable<ClienteTransacaoModel> clienteTransacaoModel = await _connection.QueryAsync<ClienteTransacaoModel>(query, queryArguments);

            return new ExtratoEntity(
                    new ExtratoSaldoEntity(DateTime.UtcNow, cliente.Saldo.Limite, cliente.Saldo.Valor),
                    clienteTransacaoModel.Select(t => new Transacao(t.Valor, t.Tipo, t.Descricao, t.Realizada_Em)).ToList()); 
        }
    }
}
