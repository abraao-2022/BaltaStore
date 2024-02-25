using BaltaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;

namespace BaltaStore.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTests
    {
        [TestMethod]
        public void ShouldValidateWhenCommandIsValid()
        {
            var command = new CreateCustomerCommand();
            command.FirstName = "Andre";
            command.LastName = "Baltieri";
            command.Document = "28659170377";
            command.Email = "andrebaltieri@gmail.com";
            command.Phone = "11999999997";

            Assert.AreEqual(true, command.Valid());
        }
    }
}