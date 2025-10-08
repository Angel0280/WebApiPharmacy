using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.Interfaces;
using WebApi.Core.Common;
using WebApiPharmacy.DTOs;

namespace WebApiPharmacy.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandsService;

        //Constructor del controlador
        public BrandController(IBrandService brand)
        {
            _brandsService = brand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResponse = await _brandsService.GetAllAsync();

            if (serviceResponse.IsSuccess)
            {
                //mapeo de los datos recibidos a la estructura del DTO a enviar
                //En este caso mapear la estructura brand a brandDTO usando LINQ
                var brandsDtoCollection = serviceResponse.Data.Select(c => new GetAllBrandDto
                {
                    Id = c.Id,
                    Name = c.BrandName,
                    BrandDescription = c.Description,
                    RegisteredDate = c.RegisteredDate,
                    IsActive = c.IsActive
                });

                //preparamos la respuesta ApiResponse
                var apiResponse = new ApiResponse<IEnumerable<GetAllBrandDto>>
                {
                    Data = brandsDtoCollection,
                    Meta = new
                    {
                        TotalAmount = brandsDtoCollection.Count(), message = serviceResponse.Message

                    }
                };
                return Ok(apiResponse);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCode)
            {
                case MessageCodes.NoData:
                    unsuccessfulResponse.Code = "200";
                    unsuccessfulResponse.Message = "No se encontraron registros";
                    unsuccessfulResponse.Details = new { info = "Temporalmente no hay registros en la BD" };

                    return Ok(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unsuccessfulResponse.Details = new { info = "Error interno en la aplicacion" };

                    return StatusCode(500, unsuccessfulResponse);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (id <= 0 || id == null)
            {
                var response = new UnsuccessfulResponseDto()
                {
                    Code = "400",
                    Message = "Id proporcionado debe de ser mayor a 0",
                    Details = new { info = "Error en el formato de valor enviado" }
                };
                return BadRequest(response);
            }
            var serviceResponse = await _brandsService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var brandDto = new GetAllBrandDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data.BrandName,
                    BrandDescription = serviceResponse.Data.Description!,
                    RegisteredDate = serviceResponse.Data.RegisteredDate,
                    IsActive = serviceResponse.Data.IsActive
                };

                return Ok(brandDto);
            }

            switch (serviceResponse.MessageCode)
            {
                case MessageCodes.NotFound:
                    var unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "400",
                        Message = serviceResponse.Message ?? "No se encontro la marca",
                        Details = new { info = serviceResponse.Message ?? "No se encontro el recurso solicitado" }
                    };
                    return BadRequest(unsuccessfulResponse);

                default:
                    unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "500",
                        Message = "Ocurrio un error",
                        Details = new { info = serviceResponse.Message ?? "Error interno" }
                    };
                    return StatusCode(500, unsuccessfulResponse);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddBrandsDto brandDto)
        {
            var brand = new WebApi.Core.Entities.Brands
            {
                BrandName = brandDto.BrandName,
                Description = brandDto.BrandDescription,
                IsActive = true,
                RegisteredDate = DateTime.Now
            };
            var serviceResponse = await _brandsService.AddAsync(brand);
            if (serviceResponse.IsSuccess)
            {
                var apiResponse = new ApiResponse<int>
                {
                    Data = serviceResponse.Data,
                    Meta = new
                    {
                        message = serviceResponse.Message
                    }
                };
                return Ok(apiResponse);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();
            switch (serviceResponse.MessageCode)
            {
                case MessageCodes.DuplicateData:
                    unsuccessfulResponse.Code = "200";
                    unsuccessfulResponse.Message = "El registro ya existe";
                    unsuccessfulResponse.Details = new { info = "Ya existe un registro con el mismo nombre" };

                    return Ok(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unsuccessfulResponse.Details = new { info = "Error interno en la aplicacion" };

                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> DeactiveAsync(int id)
        {
            var serviceResponse = await _brandsService.DeactiveAsync(id);
            if (serviceResponse.IsSuccess)
            {
                var apiResponse = new ApiResponse<int>
                {
                    Data = serviceResponse.Data,
                    Meta = new
                    {
                        message = serviceResponse.Message
                    }
                };
                return Ok(apiResponse);
            }
            var unsuccessfulResponse = new UnsuccessfulResponseDto();
            switch (serviceResponse.MessageCode)
            {
                case MessageCodes.NoData:
                    unsuccessfulResponse.Code = "200";
                    unsuccessfulResponse.Message = "No se encontraron registros";
                    unsuccessfulResponse.Details = new { info = "El id proporcionado no existe" };
                    return Ok(unsuccessfulResponse);
                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unsuccessfulResponse.Details = new { info = "Error interno en la aplicacion" };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

       
        
    }


}

