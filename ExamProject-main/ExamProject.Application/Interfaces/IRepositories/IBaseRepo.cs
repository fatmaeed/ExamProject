namespace ExamProject.Application.Interfaces.IRepositories {

    public interface IBaseRepo<T> {

        Task<T> AddAsync(T entity);

        Task Update(T entity);

        Task<T> Delete(int id);

        IQueryable<T> GetAllAsync();

        Task<T?> GetByIdAsync(int id);
    }
}