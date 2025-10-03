using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common;
using WebApi.Core.Entities;

namespace WebAPi.DataAccess.Intarfaces
{
    public interface IBrandRepository
    {
        // Firma para botener el metodo de todas las marcas 
        Task<RepositoryResponse<IEnumerable<Brands>>> GetAllAsync();

        // Firma para agregar una nueva marca
        Task<RepositoryResponse<int>>AddAsync(Brands brand);
        //Firma para desactivar una marca
        Task<RepositoryResponse<int>> DeactiveAsync(int id);

        //Firma para obtener una marca por id
        Task<RepositoryResponse<Brands>> GetByIdAsync(int id);

    }
}
