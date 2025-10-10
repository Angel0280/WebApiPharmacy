using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common;
using WebApi.Core.Entities;

namespace WebApi.Business.Interfaces
{
    public interface IBrandService
    {
        Task<ServiceResponse<IEnumerable<Brands>>> GetAllAsync();
        Task<ServiceResponse<int>> AddAsync(Brands brand);
        Task <ServiceResponse <int>> DeactiveAsync (int id);
        Task<ServiceResponse<Brands>> GetByIdAsync(int id);
        Task<ServiceResponse<Brands>> UpdateAsync(int id, Brands brands);
    }
}
