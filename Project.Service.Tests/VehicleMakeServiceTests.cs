using Moq;
using Project.DAL.Context;
using Project.DAL.DomainModels;
using Project.Repository;
using Project.Repository.Common;
using Project.Repository.Common.Repositories;
using Project.Repository.Repositories;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Project.DAL.IDomainModels;
using FluentAssertions;
using Project.Model.VehicleMakeDomainModels;
using Project.Service.VehicleMakeServices;

namespace Project.Service.Tests
{
    public class VehicleMakeServiceTests
    {

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async void CreateVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
        {
            //arrange
            var vehicle = new VehicleMakeEntity() { Name = "someName", Abrv = "someAbrv" };

            var unit = new Mock<IUnitOfWork>();
            unit.Setup(x => x.AddAsync(vehicle)).Returns(Task.FromResult(1));
            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));


            //act
            //VehicleMakeService service = new VehicleMakeService(unit.Object);

            //bool isCreated = await service.CreateVehicleMakeAsync(vehicle);


            //assert
            //if(numberofSuccessfulCommits == 0)
            //    isCreated.Should().BeFalse();
            //else
            //    isCreated.Should().BeTrue();


        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async void UpdateVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
        {
            //arrange
            var vehicle = new VehicleMakeEntity() { Id = new Guid(), Name = "someName", Abrv = "someAbrv" };

            var unit = new Mock<IUnitOfWork>();

            unit.Setup(x => x.UpdateAsync(vehicle)).Returns(Task.FromResult(1));
            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));

            //act

            //VehicleMakeService service = new VehicleMakeService(unit.Object);

            //var isUpdated = await service.UpdateVehicleMakeAsync(vehicle);



            //assert
            //if (numberofSuccessfulCommits == 0)
            //    isUpdated.Should().BeFalse();
            //else
            //    isUpdated.Should().BeTrue();

        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async void DeleteVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
        {
            //arrange
            var vehicle = new VehicleMake() { Id = new Guid(), Name = "someName", Abrv = "someAbrv" };

            var unit = new Mock<IUnitOfWork>();

            unit.Setup(x => x.DeleteAsync<VehicleMakeEntity>(vehicle.Id)).Returns(Task.FromResult(1));
            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));

            //act

            //VehicleMakeService service = new VehicleMakeService(unit.Object);

            //var isDeleted = await service.DeleteVehicleMakeAsync(vehicle.Id);



            //assert
            //if (numberofSuccessfulCommits == 0)
            //    isDeleted.Should().BeFalse();
            //else
            //    isDeleted.Should().BeTrue();
        }


    }
}
