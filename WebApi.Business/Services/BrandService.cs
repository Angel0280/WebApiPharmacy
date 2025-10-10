using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApi.Business.Interfaces;
using WebApi.Core.Common;
using WebApi.Core.Entities;
using WebAPi.DataAccess.Intarfaces;

namespace WebAPi.DataAccess.Repositories
{
    public class BrandService : IBrandService
    {
        // Implementacion del metodo GetAllAsync para obtener todas las marcas
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<ServiceResponse<IEnumerable<Brands>>> GetAllAsync()
        {
            var result = await _brandRepository.GetAllAsync();

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<IEnumerable<Brands>>()
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCode = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };


            }
            switch (result.OperationStatusCode)
            {
                case 50008:
                    return new ServiceResponse<IEnumerable<Brands>>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCode = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };


                default:
                    return new ServiceResponse<IEnumerable<Brands>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCode = MessageCodes.NoData,
                        Message = "Ocurrio un error inesperado"
                    };

            }

        }

        public async Task<ServiceResponse<int>> AddAsync(Brands brand)
        {
            var result = await _brandRepository.AddAsync(brand);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<int>
                {
                    Data = result.Data, // Aquí debería ser el ID generado por el SP
                    IsSuccess = true,
                    MessageCode = MessageCodes.Success,
                    Message = "Marca registrada correctamente"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50005: // Ejemplo: código para duplicado
                    return new ServiceResponse<int>
                    {
                        Data = -1,
                        IsSuccess = false,
                        MessageCode = MessageCodes.DuplicateData,
                        Message = "La marca ya existe"
                    };

                default:
                    return new ServiceResponse<int>
                    {
                        Data = -1,
                        IsSuccess = false,
                        MessageCode = MessageCodes.ErrorDataBase,
                        Message = "Ocurrió un error inesperado al registrar la marca"
                    };
            }
        }

        public async Task<ServiceResponse<int>> DeactiveAsync(int id)
        {
            var result = await _brandRepository.DeactiveAsync(id);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<int>
                {
                    Data = result.Data, // Aquí debería ser el ID generado por el SP
                    IsSuccess = true,
                    MessageCode = MessageCodes.Success,
                    Message = "Marca desactivada correctamente"
                };
            }
            switch (result.OperationStatusCode)
            {
                case 50006: // Ejemplo: código para no encontrado
                    return new ServiceResponse<int>
                    {
                        Data = -1,
                        IsSuccess = false,
                        MessageCode = MessageCodes.NoData,
                        Message = "La marca no existe"
                    };
                default:
                    return new ServiceResponse<int>
                    {
                        Data = -1,
                        IsSuccess = false,
                        MessageCode = MessageCodes.ErrorDataBase,
                        Message = "Ocurrió un error inesperado al desactivar la marca"
                    };
            }
        }

        public async Task<ServiceResponse<Brands>> GetByIdAsync(int id)
        {
            var result = await _brandRepository.GetByIdAsync(id);
            try
            {
                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Brands>
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
                        return new ServiceResponse<Brands>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCode = MessageCodes.NotFound,
                            Message = "La marca no existe"
                        };
                    default:
                        return new ServiceResponse<Brands>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCode = MessageCodes.ErrorDataBase,
                            Message = "Ocurrió un error inesperado al obtener la marca"
                        };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ServiceResponse<Brands>> UpdateAsync(int id, Brands brands)
        {
            var result = await _brandRepository.GetByIdAsync(id);
            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<Brands>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCode = MessageCodes.Success,
                    Message = "Marca actualizada correctamente"
                };
            }
            switch (result.OperationStatusCode)
            {
                case 50009: // Ejemplo: código para no encontrado
                    return new ServiceResponse<Brands>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCode = MessageCodes.NotFound,
                        Message = "La marca no existe"
                    };
                case 50005: // Ejemplo: código para duplicado
                    return new ServiceResponse<Brands>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCode = MessageCodes.DuplicateData,
                        Message = "La marca ya existe"
                    };
                default:
                    return new ServiceResponse<Brands>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCode = MessageCodes.ErrorDataBase,
                        Message = "Ocurrió un error inesperado al actualizar la marca"
                    };


            }
            
        }
    }
}