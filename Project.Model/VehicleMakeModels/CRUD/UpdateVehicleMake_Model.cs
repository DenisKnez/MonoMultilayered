using Project.Model.Common.IVehicleMakeModels.CRUD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Model.VehicleMakeModels.CRUD
{
    public class UpdateVehicleMake_Model : IUpdateVehicleMake_Model
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
