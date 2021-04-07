using Data_;
using Data_.Entities;

namespace Data_.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Achievement> AchievementRepository { get; }
        GenericRepository<User> UserRepository { get; }

        void SaveChanges();
    }
}
