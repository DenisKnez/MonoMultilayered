using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Common;
using Project.Model;
using Project.Model.Common;
using Project.Model.Common.IVehicleModelDomainModels;
using Project.Model.VehicleModelDomainModels;
using Project.Service.Common.IVehicleModelServices;
using Project.WebAPI.IModels.IVehicleModelRestModels;
using Project.WebAPI.Models.VehicleModelRestModels;
using Project.WebAPI.Models.VehicleModelRestModels.CRUD;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelController : ControllerBase
    {
        public IVehicleModelService VehicleModelService { get; set; }
        public IMapper Mapper { get; set; }

        public VehicleModelController(IVehicleModelService vehicleModelService, IMapper mapper)
        {
            VehicleModelService = vehicleModelService;
            Mapper = mapper;
        }

        // POST api/vehiclemake/paginated
        [HttpPost("paginated")]
        public async Task<ActionResult> GetPaginatedVehicleModelsAsync([FromBody]PageSettings pageSettings)
        {
            IPage<IVehicleModel> vehicles = await VehicleModelService.GetPaginatedFilteredListAsync(pageSettings);

            VehicleModelPageRestModel model = Mapper.Map<VehicleModelPageRestModel>((Page<IVehicleModel>)vehicles);

            return Ok(model);
        }



        // GET api/vehiclemake/get/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IVehicleModelRestModel>> GetVehicleByIdAsync(Guid id)
        {

            var vehicle = await VehicleModelService.GetVehicleModelByIdAsync(id);

            if (vehicle == null)
            {
                return BadRequest();
            }
            else
            {
                var vehicleModel = Mapper.Map<VehicleModelRestModel>(vehicle);

                return Ok(vehicleModel);
            }

        }


        // POST api/vehiclemake/createVehicle
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleModelAsync([FromBody] CreateVehicleModelRestModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<VehicleModel>(createModel);


            var isCreated = await VehicleModelService.CreateVehicleModelAsync(vehicle);

            if (isCreated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // PUT api/vehiclemake/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicleModelAsync([FromBody] UpdateVehicleModelRestModel updateModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<VehicleModel>(updateModel);

            var isUpdated = await VehicleModelService.UpdateVehicleModelAsync(vehicle);


            if (isUpdated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // DELETE api/vehiclemake/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleModelAsync(Guid id)
        {
            var isDeleted = await VehicleModelService.DeleteVehicleModelAsync(id);


            if (isDeleted == true)
                return Ok();
            else
                return BadRequest();

        }

    }




}