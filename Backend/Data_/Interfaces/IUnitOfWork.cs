using Data_;
using Data_.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Data_.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Achievement> AchievementRepository { get; }
        GenericRepository<User> UserRepository { get; }

        Task SaveChangesAsync(CancellationToken cancel);
    }
}
