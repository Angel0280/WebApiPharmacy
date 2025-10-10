using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.DTOs;
using WebApi.Core.Common;
using WebApi.Core.Entities;

namespace WebApi.Business.Interfaces
{
    public interface IPresentationService
    {
        Task<ServiceResponse<IEnumerable<Presentations>>> GetAllAsync();
        //Task<ServiceResponse<Presentations>> AddAsync(CreatePresentationDto newpresentations);

        Task<ServiceResponse<Presentations>> GetByUnitMeasureAsync(string UnitMeasure);


    }
}
