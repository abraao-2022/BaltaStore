using BaltaStore.Domain.StoreContext.Entites;
using BaltaStore.Domain.StoreContext.ValueObjects;

namespace BaltaStore.Test
{
    [TestClass]
    public class DocumentTests
    {
        private Document validDocument;
        private Document InvalidDocument;

        public DocumentTests()
        {
            validDocument = new Document("28659170377");
            InvalidDocument = new Document("12345678910");
        }

        [TestMethod]
        public void shouldReturnNotificationWhenDocumentIsNotValid()
        {
            Assert.AreEqual(false, InvalidDocument.IsValid);
            Assert.AreEqual(1, InvalidDocument.Notifications.Count);
        }

        [TestMethod]
        public void shouldReturnNotNotificationWhenDocumentIsValid()
        {
            Assert.AreEqual(true, validDocument.IsValid);
            Assert.AreEqual(0, validDocument.Notifications.Count);
        }
    }
}