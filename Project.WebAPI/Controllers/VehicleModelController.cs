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
using Project.Model.VehicleModelDomainModels.CRUD;
using Project.Service.Common.IVehicleMakeServices;
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
        public IVehicleMakeService VehicleMakeService { get; set; }
        public IMapper Mapper { get; set; }

        public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            VehicleModelService = vehicleModelService;
            VehicleMakeService = vehicleMakeService;
            Mapper = mapper;
        }

        // POST api/vehiclemodel/paginated
        [HttpPost("paginated")]
        public async Task<ActionResult> GetPaginatedVehicleModelsAsync([FromBody]PageSettings pageSettings)
        {
            IPage<IVehicleModel> vehicles = await VehicleModelService.GetPaginatedFilteredListAsync(pageSettings);

            VehicleModelPageRestModel model = Mapper.Map<VehicleModelPageRestModel>((Page<IVehicleModel>)vehicles);

            return Ok(model);
        }


        // GET api/vehiclemodel/get/{id}
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


        // POST api/vehiclemodel/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleModelAsync([FromBody] CreateVehicleModelRestModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<CreateVehicleModel>(createModel);


            var isCreated = await VehicleModelService.CreateVehicleModelAsync(vehicle);

            if (isCreated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // PUT api/vehiclemodel/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicleModelAsync([FromBody] UpdateVehicleModelRestModel updateModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<UpdateVehicleModel>(updateModel);

            var isUpdated = await VehicleModelService.UpdateVehicleModelAsync(vehicle);


            if (isUpdated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // DELETE api/vehiclemodel/delete/{id}
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



