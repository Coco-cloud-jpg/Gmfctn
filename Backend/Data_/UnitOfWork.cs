using Data_.Entities;
using Data_.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data_
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GmfctnContext _Context;
        private GenericRepository<Achievement> _AchievementRepository;
        private GenericRepository<User> _UserRepository;

        public UnitOfWork(GmfctnContext Context)
        {
            this._Context = Context;
        }

        public GenericRepository<Achievement> AchievementRepository
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
        public GenericRepository<User> UserRepository
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

        public async Task SaveChangesAsync(CancellationToken Cancel)
        {
            await _Context.SaveChangesAsync(Cancel);
        }
    }
}
