using Rinha_Backend_2024.Domain.Config.Constants;
using Rinha_Backend_2024.Domain.Config.Exceptions;
using Rinha_Backend_2024.Domain.Entities.Shared;

namespace Rinha_Backend_2024.Domain.Entities.Cliente
{
    public class ClienteTransacao
    {
        public int? Id { get; private set; }

        public int ClienteId { get; private set; }

        public Transacao Transacao { get; private set; }

        public ClienteTransacao(int? id, int clienteId, Transacao transacao)
        {
            Id = id;
            ClienteId = clienteId;
            Transacao = transacao;

            Validate();
        }

        private void Validate()
        {
            if (Transacao.Valor < 0) throw new UnprocessableEntityException();

            if (Transacao.Descricao.Length > 10 || string.IsNullOrEmpty(Transacao.Descricao)) throw new UnprocessableEntityException();

            if (!Transacao.Tipo.Equals(Constants.Debito) && !Transacao.Tipo.Equals(Constants.Credito)) throw new UnprocessableEntityException();
        }
    }
}
