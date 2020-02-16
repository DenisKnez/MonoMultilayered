using Project.WebAPI.Models.IVehicleMakeRestModels.CRUD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.WebAPI.Models.VehicleMakeRestModels.CRUD
{
    public class UpdateVehicleMakeRestModel : IUpdateVehicleMakeRestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 1)]
        public string Abrv { get; set; }

    }
}
