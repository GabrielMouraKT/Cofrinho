using Cofrinho.Models;

namespace Cofrinho.Repositories
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        void Delete(Transaction transaction);
        List<Transaction> GettAll();
        void Update(Transaction transaction);
    }
}