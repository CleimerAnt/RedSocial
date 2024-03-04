namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel, postViewModel, Entity>
        where ViewModel : class
        where postViewModel : class
        where Entity : class
    {
        Task<postViewModel> AddAsync(postViewModel vm);
        Task Editar(postViewModel vm, int ID);
        Task Eliminar(int Id);
        Task<List<ViewModel>> GetAllAsync();
        Task<postViewModel> GetById(int Id);
    }
}