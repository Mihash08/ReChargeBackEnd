﻿using BackendReCharge.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SlotController : ControllerBase
    {
        List<Slot> slots = new List<Slot>()
            {
                new Slot()
                {
                    ActivityId = 1, Id = 1, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 1, Id = 2, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 1, Id = 3, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 2, Id = 4, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 2, Id = 5, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 2, Id = 6, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 3, Id = 7, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 3, Id = 8, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 3, Id = 9, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 4, Id = 10, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 4, Id = 11, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 4, Id = 12, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 5, Id = 13, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 5, Id = 14, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 5, Id = 15, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 6, Id = 16, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 6, Id = 17, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 6, Id = 18, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },

                new Slot()
                {
                    ActivityId = 7, Id = 19, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
                },
                new Slot()
                {
                    ActivityId = 7, Id = 20, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
                },
                new Slot()
                {
                    ActivityId = 7, Id = 21, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
                },
            };
        private readonly ILogger<SlotController> _logger;

        public SlotController(ILogger<SlotController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSlots")]
        public IEnumerable<Slot> GetSlotsByActivityIdAndTime(int activityId, DateTime dateTime)
        {
            return slots.Where(x => x.ActivityId == activityId && x.SlotDateTime > dateTime && x.SlotDateTime.Date  ==  dateTime.Date);
        }
        [HttpGet(Name = "GetSlot")]
        public Slot GetSlot(int id)
        {
            return slots.Where(x => x.Id == id).First();
        }
    }
}