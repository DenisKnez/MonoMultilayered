using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model;
using Project.Service.Common;
using Microsoft.AspNetCore.Http;
using Project.Model.Common;
using Microsoft.AspNetCore.Cors;
using Project.Common;
using AutoMapper;
using Project.Common.Interfaces;
using Project.WebAPI.Models.IVehicleMakeRestModels;
using Project.WebAPI.Models.VehicleMakeRestModels.CRUD;
using Project.Model.VehicleMakeDomainModels;
using Project.WebAPI.Models.VehicleMakeRestModels;
using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Service.Common.IVehicleMakeServices;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        public IVehicleMakeService VehicleMakeService { get; set; }
        public IMapper Mapper { get; set; }

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            VehicleMakeService = vehicleMakeService;
            Mapper = mapper;
        }

        // POST api/vehiclemake/paginated
        [HttpPost("paginated")]
        public async Task<ActionResult> GetPaginatedVehicleMakesAsync([FromBody]PageSettings pageSettings)
        {
            IPage<IVehicleMake> vehicles =  await VehicleMakeService.GetPaginatedFilteredListAsync(pageSettings);

            VehicleMakePageRestModel model = Mapper.Map<VehicleMakePageRestModel>((Page<IVehicleMake>)vehicles);

            return Ok(model);
        }



        // GET api/vehiclemake/get/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IVehicleMakeRestModel>> GetVehicleByIdAsync(Guid id)
        {

            var vehicle = await VehicleMakeService.GetVehicleMakeByIdAsync(id);

            if (vehicle == null)
            {
                return BadRequest();
            }
            else
            {
                var vehicleModel = Mapper.Map<VehicleMakeRestModel>(vehicle);

                return Ok(vehicleModel);
            }

        }


        // POST api/vehiclemake/createVehicle
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleMakeAsync([FromBody] CreateVehicleMakeRestModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<VehicleMake>(createModel);


            var isCreated = await VehicleMakeService.CreateVehicleMakeAsync(vehicle);

            if (isCreated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // PUT api/vehiclemake/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicleMakeAsync([FromBody] UpdateVehicleMakeRestModel updateModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var vehicle = Mapper.Map<VehicleMake>(updateModel);

            var isUpdated = await VehicleMakeService.UpdateVehicleMakeAsync(vehicle);


            if (isUpdated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // DELETE api/vehiclemake/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleMakeAsync(Guid id)
        {
            var isDeleted = await VehicleMakeService.DeleteVehicleMakeAsync(id);


            if (isDeleted == true)
                return Ok();
            else
                return BadRequest();

        }

    }
}