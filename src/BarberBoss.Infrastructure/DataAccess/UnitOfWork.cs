﻿using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infraestructure.DataAccess
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly BarberBossDbContext _dbContext;

        public UnitOfWork(BarberBossDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
