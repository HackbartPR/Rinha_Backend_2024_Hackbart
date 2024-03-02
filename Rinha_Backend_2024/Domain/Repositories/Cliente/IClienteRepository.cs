using Rinha_Backend_2024.Domain.Entities.Cliente;
using ClienteEntity = Rinha_Backend_2024.Domain.Entities.Cliente.Cliente;

namespace Rinha_Backend_2024.Domain.Repositories.Cliente
{
    public interface IClienteRepository
    {
        Task<ClienteEntity> ConsultarAsync(int id);
        Task TransacionarAsync(ClienteEntity cliente, ClienteTransacao transacao);
    }
}
