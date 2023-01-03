using Horizon.DataOperation.Tests.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Horizon.DataOperation.Tests
{
    [TestClass]
    public class RepositoryMethodTests
    {
        [TestMethod]
        public void InsertAgenda()
        {
            var agenda = new Agenda();
            agenda.StartTime = DateTime.Now;
            agenda.Shared = false;
            agenda.UserId = 12;
            agenda.Date = DateTime.Today;
            agenda.EndTime = DateTime.Now.AddHours(2);
            agenda.LocationId = 14;
            agenda.Title = "Agenda Item";
            agenda.Notes = "It is empty here.";

            using (var uow = new SampleUnitOfWork())
            {
                uow.AgendaRepository.Insert(agenda);
                uow.SaveChanges();
            }
        }

        [TestMethod]
        public void AgendaRecordCheck()
        {
            using (var uow = new SampleUnitOfWork())
            {
                bool exists = uow.AgendaRepository.Any();
                Assert.IsFalse(exists);
            }
        }
    }
}
