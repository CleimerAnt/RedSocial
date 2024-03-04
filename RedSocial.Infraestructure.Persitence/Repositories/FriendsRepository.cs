using RedSocial.Core.Application.Interfaces.Repository;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Application.Viewmodel.PublicationViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Persitence.Context;

namespace RedSocial.Infraestructure.Persitence.Repositories
{
    public class FriendsRepository : GenericRepository<Friends>, IRepositoryFriends
    {
        private readonly ApplicationContext _Context;
        public FriendsRepository(ApplicationContext context) : base(context)
        {
            _Context = context;
        }

    }
}
