namespace TestForCandidate
{
    public interface IApplicationRepository
    {
        void AddNewApplication(Application application);

        Application GetActiveApplication(long clientId);
    }
}