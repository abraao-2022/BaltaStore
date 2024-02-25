using BaltaStore.Domain.StoreContext.ValueObjects;

namespace BaltaStore.Tests.ValueObjects
{
    [TestClass]
    public class NameTest
    {
        [TestMethod]
        public void shouldReturnNotificationWhenNameIsNotValid()
        {
            var name = new Name("", "Baltieri");
            Assert.AreEqual(false, name.IsValid);
            Assert.AreEqual(1, name.Notifications.Count);
        }
    }
}