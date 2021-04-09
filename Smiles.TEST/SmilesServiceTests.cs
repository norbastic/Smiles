using Microsoft.EntityFrameworkCore;
using Smiles.BL;
using Smiles.Core.Models;
using Smiles.DAL;
using Xunit;

namespace Smiles.TEST
{
    public class SmilesServiceTests
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly SmilesService _smilesService;

        public SmilesServiceTests()
        {
            var options = new DbContextOptionsBuilder<SmilesDbContext>()
                .UseNpgsql("Host=localhost;Database=smilesdb;Username=postgres;Password=Password1")
                .Options;
            var dataContext = new SmilesDbContext(options);

            _unitOfWork = new UnitOfWork(dataContext);
            _smilesService = new SmilesService(_unitOfWork);
        }

        [Theory]
        [InlineData("C[Co@](F)(Cl)(Br)(I)S")]
        [InlineData("F[Co@@](S)(I)(C)(Cl)Br")]
        [InlineData("S[Co@OH5](F)(I)(Cl)(C)Br")]
        public async void AddSmilesTest(string Data)
        {
            var result = await _smilesService.CreateSmilesEntity(new SmilesEntity() { Data = Data });
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }

        // TODO: Test all methods...
    }
}
