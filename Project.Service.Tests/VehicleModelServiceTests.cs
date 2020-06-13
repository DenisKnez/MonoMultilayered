//using AutoMapper;
//using FluentAssertions;
//using Moq;
//using Project.DAL.DomainModels;
//using Project.Model.VehicleModelDomainModels.CRUD;
//using Project.Repository.Common;
//using Project.Service.VehicleModelServices;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Project.Service.Tests
//{
//    public class VehicleModelServiceTests
//    {
//        [Theory]
//        [InlineData(0)]
//        [InlineData(1)]
//        [InlineData(2)]
//        public async void CreateVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
//        {
//            //arrange
//            var vehicleEntity = new VehicleModelEntity() { Name = "someName", Abrv = "someAbrv", VehicleMakeEntityId = new Guid() };
//            var vehicle = new CreateVehicleModel() { Id = Guid.Empty, Name = "someName", Abrv = "someAbrv", VehicleMakeId = new Guid() };

//            var unit = new Mock<IUnitOfWork>();
//            unit.Setup(x => x.AddAsync(vehicleEntity)).Returns(Task.FromResult(1));
//            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));

//            var mapper = new Mock<IMapper>();
//            mapper.Setup(x => x.Map<VehicleMakeEntity>(It.IsAny<CreateVehicleModel>())).Returns(new VehicleMakeEntity());


//            //act
//            VehicleModelService service = new VehicleModelService(unit.Object, mapper.Object);

//            bool isCreated = await service.CreateVehicleModelAsync(vehicle);


//            //assert
//            if (numberofSuccessfulCommits == 0)
//                isCreated.Should().BeFalse();
//            else
//                isCreated.Should().BeTrue();

//        }


//        [Theory]
//        [InlineData(0)]
//        [InlineData(1)]
//        [InlineData(2)]
//        public async void UpdateVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
//        {
//            //arrange
//            var vehicleEntity = new VehicleModelEntity() { Id = new Guid(), Name = "someName", Abrv = "someAbrv", VehicleMakeEntityId = new Guid() };
//            var vehicle = new UpdateVehicleModel() { Id = new Guid(), Name = "someName", Abrv = "someAbrv", VehicleMakeId = new Guid() };


//            var unit = new Mock<IUnitOfWork>();

//            unit.Setup(x => x.UpdateAsync(vehicleEntity)).Returns(Task.FromResult(1));
//            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));

//            var mapper = new Mock<IMapper>();
//            mapper.Setup(x => x.Map<VehicleMakeEntity>(It.IsAny<UpdateVehicleModel>())).Returns(new VehicleMakeEntity());

//            //act

//            VehicleModelService service = new VehicleModelService(unit.Object, mapper.Object);

//            var isUpdated = await service.UpdateVehicleModelAsync(vehicle);



//            //assert
//            if (numberofSuccessfulCommits == 0)
//                isUpdated.Should().BeFalse();
//            else
//                isUpdated.Should().BeTrue();

//        }

//        [Theory]
//        [InlineData(0)]
//        [InlineData(1)]
//        [InlineData(2)]
//        public async void DeleteVehicleMakeAsync_ReturnsCorrect(int numberofSuccessfulCommits)
//        {
//            //arrange
//            var vehicle = new VehicleModelEntity() { Id = new Guid(), Name = "someName", Abrv = "someAbrv" };
//            var unit = new Mock<IUnitOfWork>();

//            unit.Setup(x => x.DeleteAsync<VehicleModelEntity>(vehicle.Id)).Returns(Task.FromResult(1));
//            unit.Setup(x => x.CommitAsync()).Returns(Task.FromResult(numberofSuccessfulCommits));

//            var mapper = new Mock<IMapper>();


//            //act
//            VehicleModelService service = new VehicleModelService(unit.Object, mapper.Object);

//            var isDeleted = await service.DeleteVehicleModelAsync(vehicle.Id);



//            //assert
//            if (numberofSuccessfulCommits == 0)
//                isDeleted.Should().BeFalse();
//            else
//                isDeleted.Should().BeTrue();
//        }


//    }
//}

