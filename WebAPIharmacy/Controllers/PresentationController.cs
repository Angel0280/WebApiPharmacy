using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business.DTOs;
using WebApi.Business.Interfaces;
using WebApi.Core.Common;
using WebApi.Core.Entities;

namespace WebApiPharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentationController : ControllerBase
    {
        private readonly IPresentationService _presentationsService;

        //Constructor del controlador
        public PresentationController(IPresentationService presentation)
        {
            _presentationsService = presentation;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResponse = await _presentationsService.GetAllAsync();

            if (serviceResponse.IsSuccess)
            {
                //mapeo de los datos recibidos a la estructura del DTO a enviar
                //En este caso mapear la estructura brand a brandDTO usando LINQ
                var presentationDtoCollection = serviceResponse.Data.Select(c => new GetAllPresentationsDto
                {
                    Id = c.Id,
                    Description = c.Description!,
                    Quantity = c.Quantity!,
                    UnitMeasure = c.UnitMeasure!,
                    RegisteredDate = c.RegisteredDate,
                    IsActive = c.IsActive
                });

                //preparamos la respuesta ApiResponse
                var apiResponse = new ApiResponse<IEnumerable<GetAllPresentationsDto>>
                {
                    Data = presentationDtoCollection,
                    Meta = new
                    {
                        TotalAmount = presentationDtoCollection.Count(),
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
                    unsuccessfulResponse.Details = new { info = "Temporalmente no hay registros en la BD" };

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
