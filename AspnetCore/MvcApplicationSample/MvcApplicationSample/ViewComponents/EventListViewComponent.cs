using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcApplicationSample.Models;

namespace MvcApplicationSample
{
    public class EventListViewComponent:ViewComponent
    {
        private readonly EventsAndMenusContext context;

        public EventListViewComponent(EventsAndMenusContext context)
        {
            this.context = context;
        }

        public Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to) =>
            Task.FromResult<IViewComponentResult>(View(EventsByDateRange(from,to)));

        private IEnumerable<Event> EventsByDateRange(DateTime from, DateTime to) =>
            context.Events.Where(e => e.Day >= from && e.Day <= to);
    }
}
