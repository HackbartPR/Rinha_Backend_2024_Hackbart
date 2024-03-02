using ClienteEntity = Rinha_Backend_2024.Domain.Entities.Cliente.Cliente;
using Rinha_Backend_2024.Domain.Entities.Cliente;
using Rinha_Backend_2024.Domain.Config.Constants;

namespace Rinha_Backend_2024.Domain.Services.Cliente
{
    public static class ClienteService
    {
        public static void Operacionar(ClienteEntity cliente, ClienteTransacao transacao)
        {
            if (transacao.Transacao.Tipo.Equals(Constants.Debito))
                cliente.Saldo.Debitar(transacao.Transacao.Valor);
            else
                cliente.Saldo.Creditar(transacao.Transacao.Valor);
        }
    }
}
