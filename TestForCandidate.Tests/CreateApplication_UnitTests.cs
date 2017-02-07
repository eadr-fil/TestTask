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

        [TestMethod]
        public void Is_Amount_Or_Period_Out_Of_Range_Return_Forbidden()
        {
            TestIdentity testIdentity = new TestIdentity();
            IIdentityProvider identityProvider = Mock.Of<IIdentityProvider>(i => i.IsAuthenticated() == true && i.GetIdentity() == testIdentity);
            IApplicationRepository applicationRepository = Mock.Of<IApplicationRepository>();

            ApplicationService appService = new ApplicationService(identityProvider, applicationRepository);
            CreationResult creationResult1 = appService.CreateApplication(1001, 1);
            CreationResult creationResult2 = appService.CreateApplication(1, 31);            

            Assert.AreEqual(creationResult1, CreationResult.Forbidden);
            Assert.AreEqual(creationResult2, CreationResult.Forbidden);           
        }

        [TestMethod]
        public void Is_Correct_Arguments_Return_Success()
        {
            TestIdentity testIdentity = new TestIdentity { Id = 100, Name = "myIdentity" };
            IIdentityProvider identityProvider = Mock.Of<IIdentityProvider>(i => i.IsAuthenticated() == true && i.GetIdentity() == testIdentity);
            IApplicationRepository applicationRepository = Mock.Of<IApplicationRepository>();

            ApplicationService appService = new ApplicationService(identityProvider, applicationRepository);
            CreationResult creationResult = appService.CreateApplication(1, 1);

            Assert.AreEqual(creationResult, CreationResult.Success);
        }
    }
}
