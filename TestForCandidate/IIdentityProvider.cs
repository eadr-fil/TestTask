namespace TestForCandidate
{
    public interface IIdentityProvider
    {
        bool IsAuthenticated();

        TestIdentity GetIdentity();
    }
}