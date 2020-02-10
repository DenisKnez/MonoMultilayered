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


        [HttpPost("paginated")]
        public async Task<ActionResult> GetPaginatedVehicleMakesAsync([FromBody]PageSettings pageSettings)
        {
            PageList<IVehicleMake> vehicles = await VehicleMakeService.GetPaginatedFilteredListAsync(pageSettings);


            IVehicleMakeModel_Model m = Mapper.Map<VehicleMakeModel_Model>((VehicleMake)vehicles.Items[0]);

            string something = m.Name;

            // automapper
            List<IVehicleMakeModel_Model> pageModel = new List<IVehicleMakeModel_Model>();

            VehicleMakePage_Model model = new VehicleMakePage_Model();


            foreach (var item in vehicles.Items)
            {
                model.VehicleMakeModel_Models.Add(Mapper.Map<VehicleMakeModel_Model>((VehicleMake)model));
            }


            return Ok(pageModel);
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
                var vehicleModel = new VehicleMakeModel_Model()
                {
                    Id = vehicle.Id,
                    Name = vehicle.Name,
                    Abrv = vehicle.Abrv

                };


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
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // PUT api/vehiclemake/updateVehicle
        [HttpPut("update")]
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
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        // DELETE api/vehiclemake/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicleMakeAsync(Guid id)
        {
            var isDeleted = await VehicleMakeService.DeleteVehicleMakeAsync(id);


            if (isDeleted == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

    }
}