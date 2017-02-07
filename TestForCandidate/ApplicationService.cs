namespace TestForCandidate
{
    public class ApplicationService
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IIdentityProvider identityProvider, IApplicationRepository applicationRepository)
        {
            _identityProvider = identityProvider;
            _applicationRepository = applicationRepository;
        }

        public CreationResult CreateApplication(int amount, short period)
        {
            if (!_identityProvider.IsAuthenticated())
            {
                return CreationResult.Forbidden;
            }
            
            var identity = _identityProvider.GetIdentity();
            var application = _applicationRepository.GetActiveApplication(identity.Id);

            if (application != null)
            {
                return CreationResult.ThereIsActiveApplication;
            }

            if (amount > 1000 || period > 30)
            {
                return CreationResult.Forbidden;
            }

            _applicationRepository.AddNewApplication(new Application
                                                            {
                                                                Amount = amount,
                                                                Period = period,
                                                                ClientId = identity.Id
                                                            });

            return CreationResult.Success;
        }
    }
}