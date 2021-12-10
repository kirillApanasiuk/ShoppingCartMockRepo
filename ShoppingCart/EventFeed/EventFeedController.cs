using Microsoft.AspNetCore.Mvc;
using ShoppingCart.ShoppingCart.interfaces;
using System.Linq;

namespace ShoppingCart.ShoppingCart.EventFeed
{
    [Route("/events")]
    public class EventFeedController : Controller
    {
        private readonly IEventStore _eventStore;

        public EventFeedController(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [HttpGet("")]
        public Event[] Get([FromQuery] long start, [FromQuery] long end = long.MaxValue)
        {

            return _eventStore.GetEvents(start, end).ToArray();
        }
    }
}
