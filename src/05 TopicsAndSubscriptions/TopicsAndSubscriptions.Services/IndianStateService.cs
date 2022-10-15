using TopicsAndSubscriptions.Abstraction;
using TopicsAndSubscriptions.Model;

namespace TopicsAndSubscriptions.Services
{
    public class IndianStateService : IIndianStateProvider
    {
        public List<IndianState> GetAllIndianStates()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "files", "states.json");
            List<IndianState> states = new List<IndianState>();
            if (File.Exists(filePath))
            {
                var text = File.ReadAllText(filePath);
                states = System.Text.Json.JsonSerializer.Deserialize<List<IndianState>>(text);
            }
            return states;
        }
    }
}