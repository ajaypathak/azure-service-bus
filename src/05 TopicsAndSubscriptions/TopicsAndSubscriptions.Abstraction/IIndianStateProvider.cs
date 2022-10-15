using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Abstraction
{
    public interface IIndianStateProvider
    {
        List<IndianState> GetAllIndianStates();
    }
}