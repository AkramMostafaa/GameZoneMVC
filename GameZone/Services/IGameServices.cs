using GameZone.Models;
using GameZone.ViewModel;

namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Games> GetAll();
        Games? GetById(int id);

        Task Create(CreateGameFormViewModel model) ;

        Task<Games?> Update(EditGameFormViewModel model) ;

        bool Delete(int id);


    }
}
