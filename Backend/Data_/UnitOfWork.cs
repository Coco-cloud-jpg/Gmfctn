using Data_.Entities;
using Data_.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GmfctnContext context;
        private GenericRepository<Achievement> achievementRepository;
        private GenericRepository<User> userRepository;

        public UnitOfWork(GmfctnContext context)
        {
            this.context = context;
        }

        public GenericRepository<Achievement> AchievementRepository
        {
            get
            {
                if (this.achievementRepository == null)
                {
                    this.achievementRepository = new GenericRepository<Achievement>(context);
                }
                return achievementRepository;
            }
        }
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
