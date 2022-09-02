using ApiPostgresDapper.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApirPostgresDapper.Infraestructure.Interfaces
{
    public interface IPeticionRepository
    {
        Task<IEnumerable<Peticion>> GetAll();
        Task<bool> Add(Peticion entity);
        Task<Peticion> GetById(int id);
        Task<bool> Update(Peticion entity);
        Task<bool> Delete(int id);
    }
}
