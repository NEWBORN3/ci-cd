using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagmentApi.Services
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options)
            : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }


        public async Task<Event> Add(Event newEvent)
        {
            Events.Add(newEvent);
            await SaveChangesAsync();
            return newEvent;
        }

        public async void Delete(int id)
        {
            var theEvent = GetById(id);
            if (theEvent != null)
            {
                throw new ArgumentException("No event exsist with the given id", nameof(id));
            }
            Events.Remove(theEvent);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetAll() => await Events.ToArrayAsync();

        public Event GetById(int id) => Events.Single(ev => ev.Id == id);
    }
}
 