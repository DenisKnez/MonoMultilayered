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
using Project.Model.VehicleMakeModels.CRUD;
using Project.Model.Common;
using Microsoft.AspNetCore.Cors;
using Project.Common;
using AutoMapper;
using Project.Common.Interfaces;

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

            List<VehicleMakeModel_Model> makeModels = Mapper.Map<List<VehicleMakeModel_Model>>(vehicles.Items);

            var model = new VehicleMakePage_Model()
            {
                Items = makeModels.Cast<IVehicleMakeModel_Model>().ToList(),
                PageIndex = vehicles.PageIndex,
                TotalPages = vehicles.TotalPages,
                SearchString = pageSettings.SearchString,
                SortOrder = pageSettings.SortOrder
            };


            return Ok(model);
        }



        // GET api/vehiclemake/get/{id}
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IVehicleMakeModel_Model>> GetVehicleByIdAsync(Guid id)
        {

            var vehicle = await VehicleMakeService.GetVehicleMakeByIdAsync(id);


            if (vehicle == null)
            {
                return BadRequest();
            }
            else
            {
                var vehicleModel = Mapper.Map<VehicleMakeModel_Model>(vehicle);

                return Ok(vehicleModel);
            }

        }


        // POST api/vehiclemake/createVehicle
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicleMakeAsync([FromBody] CreateVehicleMake_Model createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // TODO: automapper
            IVehicleMake vehicle = new VehicleMake()
            {
                Name = createModel.Name,
                Abrv = createModel.Abrv
            };

            var isCreated = await VehicleMakeService.CreateVehicleMakeAsync(vehicle);

            if (isCreated == true)
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);


        }

        // PUT api/vehiclemake/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateVehicleMakeAsync([FromBody] UpdateVehicleMake_Model updateModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            IVehicleMake vehicle = new VehicleMake()
            {
                Id = updateModel.Id,
                Name = updateModel.Name,
                Abrv = updateModel.Abrv
            };

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