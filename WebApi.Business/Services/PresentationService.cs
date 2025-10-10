using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.DTOs;
using WebApi.Business.Interfaces;
using WebApi.Core.Common;
using WebApi.Core.Entities;
using WebAPi.DataAccess.Intarfaces;
using WebAPi.DataAccess.Repositories;


namespace WebApi.Business.Services
{
    public class PresentationService : IPresentationService
    {
        private readonly IPresentationRepository _presentationRepository;

        public PresentationService(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        public async Task<ServiceResponse<IEnumerable<Presentations>>> GetAllAsync()
        {
            var result = await _presentationRepository.GetAllAsync();

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<IEnumerable<Presentations>>()
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCode = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };


            }
            switch (result.OperationStatusCode)
            {
                case 50010:
                    return new ServiceResponse<IEnumerable<Presentations>>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCode = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };


                default:
                    return new ServiceResponse<IEnumerable<Presentations>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCode = MessageCodes.NoData,
                        Message = "Ocurrio un error inesperado"
                    };

            }
        }

        public async Task<ServiceResponse<Presentations>> GetByUnitMeasureAsync(string UnitMeasure)
        {
            var result = await _presentationRepository.GetByUnitMeasureAsync(UnitMeasure);
            try
            {
                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Presentations>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCode = MessageCodes.Success,
                        Message = result.Message ?? "Operacion exitosa"
                    };
                }
                switch (result.OperationStatusCode)
                {
                    case 50009: // Ejemplo: código para no encontrado
                        return new ServiceResponse<Presentations>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCode = MessageCodes.NotFound,
                            Message = "La unidad de medida no existe"
                        };
                    default:
                        return new ServiceResponse<Presentations>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCode = MessageCodes.ErrorDataBase,
                            Message = "Ocurrió un error inesperado al obtener la unidad de medida"
                        };
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        


    }
    
}
