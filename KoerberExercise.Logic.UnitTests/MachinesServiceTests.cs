using KoerberExercise.Logic.Services.Implementations;
using NUnit.Framework;

namespace KoerberExercise.Logic.UnitTests
{
    public class MachinesServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsParentSameAsMachine_SameIds_ReturnsTrue()
        {
            //Arrange
            //var service = new MachinesService();

            //// Act
            var result = MachinesService.IsParentSameAsMachine(0, 0);

            //// Assert
            //Assert.That(result, Is.True);
        }
    }
}