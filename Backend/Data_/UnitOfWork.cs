using Data_.Entities;
using Data_.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data_
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GmfctnContext _Context;
        private IGenericRepository<Achievement> _AchievementRepository;
        private IGenericRepository<User> _UserRepository;
        private IGenericRepository<Role> _RoleRepository;
        private IGenericRepository<Thank> _ThankRepository; 
        private IGenericRepository<Event> _EventRepository;
        private IGenericRepository<RequestAchievement> _RequestAchievementRepository;
        private IGenericRepository<PasswordResetRequest> _PasswordResetRequestRepository;
        private IGenericRepository<File> _FileRepository; 
        public UnitOfWork(GmfctnContext Context)
        {
            this._Context = Context;
        }
        public IGenericRepository<PasswordResetRequest> PasswordResetRequestRepository
        {
            get
            {
                if (this._PasswordResetRequestRepository == null)
                {
                    this._PasswordResetRequestRepository = new GenericRepository<PasswordResetRequest>(_Context);
                }
                return _PasswordResetRequestRepository;
            }
        }
        public IGenericRepository<File> FileRepository
        {
            get
            {
                if (this._FileRepository == null)
                {
                    this._FileRepository = new GenericRepository<File>(_Context);
                }
                return _FileRepository;
            }
        }
        public IGenericRepository<Achievement> AchievementRepository
        {
            get
            {
                if (this._AchievementRepository == null)
                {
                    this._AchievementRepository = new GenericRepository<Achievement>(_Context);
                }
                return _AchievementRepository;
            }
        }
        public IGenericRepository<Event> EventRepository
        {
            get
            {
                if (this._EventRepository == null)
                {
                    this._EventRepository = new GenericRepository<Event>(_Context);
                }
                return _EventRepository;
            }
        }
        public IGenericRepository<RequestAchievement> RequestAchievementRepository
        {
            get
            {
                if (this._RequestAchievementRepository == null)
                {
                    this._RequestAchievementRepository = new GenericRepository<RequestAchievement>(_Context);
                }
                return _RequestAchievementRepository;
            }
        }
        public IGenericRepository<Thank> ThankRepository
        {
            get
            {
                if (this._ThankRepository == null)
                {
                    this._ThankRepository = new GenericRepository<Thank>(_Context);
                }
                return _ThankRepository;
            }
        }
        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (this._UserRepository == null)
                {
                    this._UserRepository = new GenericRepository<User>(_Context);
                }
                return _UserRepository;
            }
        }
        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                if (this._RoleRepository == null)
                {
                    this._RoleRepository = new GenericRepository<Role>(_Context);
                }
                return _RoleRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken Cancel)
        {
            await _Context.SaveChangesAsync(Cancel);
        }
    }
}
