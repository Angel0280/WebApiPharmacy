using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common;
using WebApi.Core.Entities;

namespace WebAPi.DataAccess.Intarfaces
{
    public interface IPresentationRepository
    {
        Task<RepositoryResponse<IEnumerable<Presentations>>> GetAllAsync();


        Task<RepositoryResponse<Presentations>> GetByUnitMeasureAsync(string UnitMeasure);

        Task<RepositoryResponse<Presentations>> AddAsync(Presentations presentations);
            
    }
}
