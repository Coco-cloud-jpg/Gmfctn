using Data_;
using Data_.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Data_.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Achievement> AchievementRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Thank> ThankRepository { get; }
        IGenericRepository<Event> EventRepository { get; }
        IGenericRepository<File> FileRepository { get; }
        IGenericRepository<PasswordResetRequest> PasswordResetRequestRepository { get; }
        IGenericRepository<RequestAchievement> RequestAchievementRepository { get; }
        Task SaveChangesAsync(CancellationToken cancel);
    }
}
