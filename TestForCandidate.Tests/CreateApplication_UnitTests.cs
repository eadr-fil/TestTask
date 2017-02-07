using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestForCandidate.Tests
{
    [TestClass]
    public class CreateApplication_UnitTests
    {
        [TestMethod]
        public void Is_Not_Authentificated_Return_Forbidden()
        {
            IIdentityProvider identityProvider = Mock.Of<IIdentityProvider>(i => i.IsAuthenticated() == false);
            IApplicationRepository applicationRepository = Mock.Of<IApplicationRepository>();

            ApplicationService appService = new ApplicationService(identityProvider, applicationRepository);
            CreationResult creationResult = appService.CreateApplication(0, 0);

            Assert.AreEqual(creationResult, CreationResult.Forbidden); 
        }

        [TestMethod]
        public void Is_Authentificated_Not_Null_App_Return_ThereIsActiveApplication()
        {
            Application app = new Application();
            TestIdentity testIdentity = new TestIdentity();
            IIdentityProvider identityProvider = Mock.Of<IIdentityProvider>(i => i.IsAuthenticated() == true && i.GetIdentity() == testIdentity);
            IApplicationRepository applicationRepository = Mock.Of<IApplicationRepository>(a => a.GetActiveApplication(app.Id) == app);

            ApplicationService appService = new ApplicationService(identityProvider, applicationRepository);
            CreationResult creationResult = appService.CreateApplication(0, 0);

            Assert.AreEqual(creationResult, CreationResult.ThereIsActiveApplication);
        }
        
    }
}
