using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagmentApi.Services
{
    public class EventRepository : IEventRepository
    {
        private List<Event> Events { get; } = new();
        public Event Add(Event newEvent)
        {
            Events.Add(newEvent);
            return newEvent;
        }

        public IEnumerable<Event> GetAll() => Events;

        public Event GetById(int id) => Events.FirstOrDefault(e => e.Id == id);

        public void Delete(int id)
        {
            var eventToDelete = GetById(id);
            if (eventToDelete == null)
            {
                throw new ArgumentException("No event exsist with the given id", nameof(id));
            }
            Events.Remove(eventToDelete);
        }
    }
}
