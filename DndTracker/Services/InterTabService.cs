using DndTracker.Data;

namespace DndTracker.Services
{
    public class InterTabService
    {
        public event Action? OnEncounterRecieved;

        public void NotifyEncounterReceived()
        {
            OnEncounterRecieved?.Invoke();
        }
    }
}
