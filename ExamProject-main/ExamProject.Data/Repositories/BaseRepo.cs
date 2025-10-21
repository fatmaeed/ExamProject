using ExamProject.Application.Interfaces.IEntity;
using ExamProject.Application.Interfaces.IRepositories;
using ExamProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Infrastructure.Repositories {

    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseEntity, new() {
        protected readonly ExamDbContext examDb;

        public BaseRepo(ExamDbContext examDb) {
            this.examDb = examDb;
        }

        public async Task<T> AddAsync(T entity) {
            await examDb.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T?> Delete(int id) {
            var entity = await GetByIdAsync(id);
            if (entity == null) {
                return null;
            }
            entity!.IsDeleted = true;
            await Update(entity);
            return entity;
        }

        public IQueryable<T> GetAllAsync() {
            return examDb.Set<T>().Where(entity => !entity.IsDeleted);
        }

        public async Task<T?> GetByIdAsync(int id) {
            return await examDb.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted);
        }

        public async Task Update(T entity) {
            examDb.Entry(entity).State = EntityState.Modified;
            entity.UpdatedDate = DateTime.Now;
            entity.isUpdated = true;
        }
    }
}