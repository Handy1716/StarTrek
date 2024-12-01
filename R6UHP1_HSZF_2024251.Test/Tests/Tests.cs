using NUnit.Framework;
using R6UHP1_HSZF_2024251.Persistence.MsSql.Infrastructure.Entities;
using R6UHP1_HSZF_2024251.Application.Services;

namespace R6UHP1_HSZF_2024251.Test.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        private SpaceShipService _spaceShipService;
        private PlanetService _planetService;
        private CrewMemberService _crewMemberService;
        private MissionService _missionService;

        [SetUp]
        public void Setup()
        {
            _spaceShipService = new SpaceShipService();
            _planetService = new PlanetService();
            _crewMemberService = new CrewMemberService();
            _missionService = new MissionService();
        }

        // 1. GetSpaceShipById - Null értéket kell visszaadnia, ha az ID nem létezik
        [Test]
        public void GetSpaceShipById_ShouldReturnNull_WhenNotFound()
        {
            var result = _spaceShipService.GetSpaceShipById(999);
            Assert.That(result, Is.Null);
        }

        // 2. DeleteSpaceShip - False-t kell visszaadnia, ha az ID nem létezik
        [Test]
        public void DeleteSpaceShip_ShouldReturnFalse_WhenNotFound()
        {
            var result = _spaceShipService.DeleteSpaceShip(999);
            Assert.That(result, Is.False);
        }

        // 3. GetPlanetById - Null értéket kell visszaadnia, ha az ID nem létezik
        [Test]
        public void GetPlanetById_ShouldReturnNull_WhenNotFound()
        {
            var result = _planetService.GetPlanetById(999);
            Assert.That(result, Is.Null);
        }

        // 4. DeletePlanet - False-t kell visszaadnia, ha az ID nem létezik
        [Test]
        public void DeletePlanet_ShouldReturnFalse_WhenNotFound()
        {
            var result = _planetService.DeletePlanet(999);
            Assert.That(result, Is.False);
        }

        // 5. GetCrewMemberById - Null értéket kell visszaadnia, ha az ID nem létezik
        [Test]
        public void GetCrewMemberById_ShouldReturnNull_WhenNotFound()
        {
            var result = _crewMemberService.GetCrewMemberById(999);
            Assert.That(result, Is.Null);
        }

        // 6. DeleteCrewMember - False-t kell visszaadnia, ha az ID nem létezik
        [Test]
        public void DeleteCrewMember_ShouldReturnFalse_WhenNotFound()
        {
            var result = _crewMemberService.DeleteCrewMember(999);
            Assert.That(result, Is.False);
        }

        // 7. GetMissionById - Null értéket kell visszaadnia, ha az ID nem létezik
        [Test]
        public void GetMissionById_ShouldReturnNull_WhenNotFound()
        {
            var result = _missionService.GetMissionById(999);
            Assert.That(result, Is.Null);
        }

        // 8. DeleteMission - False-t kell visszaadnia, ha az ID nem létezik
        [Test]
        public void DeleteMission_ShouldReturnFalse_WhenNotFound()
        {
            var result = _missionService.DeleteMission(999);
            Assert.That(result, Is.False);
        }

        // 9. GetPagedSpaceShips - Üres listát kell visszaadnia, ha a lapozás nem talál adatot
        [Test]
        public void GetPagedSpaceShips_ShouldReturnEmptyList_WhenNoDataOnPage()
        {
            var result = _spaceShipService.GetPagedSpaceShips(100, 5);
            Assert.That(result, Is.Empty);
        }

        // 10. GetPagedPlanets - Üres listát kell visszaadnia, ha a lapozás nem talál adatot
        [Test]
        public void GetPagedPlanets_ShouldReturnEmptyList_WhenNoDataOnPage()
        {
            var result = _planetService.GetPagedPlanets(100, 5);
            Assert.That(result, Is.Empty);
        }
    }
}
