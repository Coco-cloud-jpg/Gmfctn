using AutoMapper;
using Data_;
using Data_.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Tests
{
    [TestClass()]
    public class AchievementServiceTests
    {
        private readonly Mock<IGenericRepository<Achievement>> AchievementRepoMock = new Mock<IGenericRepository<Achievement>>();
        private readonly Mock<IMapper> MapperMock = new Mock<IMapper>();
        private readonly Mock<IUnitOfWork> UnitOfWorkMock = new Mock<IUnitOfWork>();
        [TestMethod()]
        public async Task GetAchievementById_ShouldReturnAchievement_WhenAchievementExists()
        {
            // Arange
            var Id = Guid.NewGuid();
            var CancellationToken = new CancellationToken();
            var AchievementResult = new Achievement()
            {
                Id = Id,
            };

            AchievementRepoMock.Setup( x =>  x.GetById(Id, CancellationToken))
                .ReturnsAsync(AchievementResult).Verifiable();
            UnitOfWorkMock.Setup(m => m.AchievementRepository).Returns(AchievementRepoMock.Object);

            AchievementService _AchievementService = new AchievementService(UnitOfWorkMock.Object, MapperMock.Object);

            // Act

            var Achievement = await _AchievementService.GetAchievementById(Id, CancellationToken);

            // Assert

            Assert.IsNotNull(Achievement);
            AchievementRepoMock.Verify();
            Assert.AreEqual(AchievementResult.Id, Achievement.Id);
        }

        [TestMethod()]
        public async Task GetAllAchievements_ShouldReturnCollectionOfAchievement_WhenAchievementExists()
        {
            // Arange
            var CancellationToken = new CancellationToken();
            IEnumerable<Achievement> AchievementsResult = new List<Achievement> { new Achievement(), new Achievement(), new Achievement() };
            AchievementRepoMock.Setup(x => x.GetAll(CancellationToken))
                .ReturnsAsync(AchievementsResult).Verifiable();
            UnitOfWorkMock.Setup(m => m.AchievementRepository).Returns(AchievementRepoMock.Object);
            
            AchievementService _AchievementService = new AchievementService(UnitOfWorkMock.Object, MapperMock.Object);

            // Act

            var Achievements = await _AchievementService.GetAllAchievements(CancellationToken);

            // Assert

            Assert.IsNotNull(Achievements);
            AchievementRepoMock.Verify();
            Assert.AreEqual(AchievementsResult, Achievements);
        }

        [TestMethod()]
        public async Task DeleteAchievement_ShouldReturnVoid_WhenAchievementExists()
        {
            // Arange
            var Id = Guid.NewGuid();
            var CancellationToken = new CancellationToken();

            AchievementRepoMock
                .Setup(x => x.Delete(It.IsAny<Guid>(), CancellationToken))
                .Returns(()=> throw  new ArgumentNullException()).Verifiable();
            UnitOfWorkMock.Setup(m => m.AchievementRepository).Returns(AchievementRepoMock.Object);

            AchievementService _AchievementService = new AchievementService(UnitOfWorkMock.Object, MapperMock.Object);

            // Act

            // Assert

            AchievementRepoMock.Verify();
            
            Assert.ThrowsException<ArgumentNullException>(async() => await _AchievementService.DeleteAchievement(It.IsAny<Guid>(), CancellationToken));
        }
    }
}