using ClienteEntity = Rinha_Backend_2024.Domain.Entities.Cliente.Cliente;
using ExtratoEntity = Rinha_Backend_2024.Domain.Entities.Extrato.Extrato;

namespace Rinha_Backend_2024.Domain.Repositories.Extrato
{
    public interface IExtratoRepository 
    {
        Task<ExtratoEntity> ConsultarAsync(ClienteEntity cliente);
    }
}
