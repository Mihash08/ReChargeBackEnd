﻿using Azure.Core;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using ReChargeBackend.Utility;
using Utility;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationsController : ControllerBase
    {
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ISlotRepository slotRepository;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger,
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            ISlotRepository slotRepository)
        {
            _logger = logger;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.slotRepository = slotRepository;
        }

        [HttpPost(Name = "MakeReservation")]
        public IActionResult MakeReservation(int slotId, [FromBody] MakeReservationRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользоватен не найден (невалидный токен доступа)");
            }
            var slot = slotRepository.GetById(slotId);
            if (slot is null)
            {
                return BadRequest($"Слот с id {slotId} не найден");
            }
            if (reservationRepository.GetReservationsByUser(user.Id).Any(x => x.SlotId == slot.Id 
            && x.Status != Status.New 
            && x.Status != Status.Confirmed))
            {
                return StatusCode(452, "Вы уже записаны на это занятие");
            }
            if (slot.FreePlaces >= request.ReserveCount)
            {
                reservationRepository.Add(new Reservation
                {
                    Slot = slot,
                    SlotId = slotId,
                    User = user,
                    UserId = user.Id,
                    Count = request.ReserveCount,
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.Phone,
                    AccessCode = Temp.GenerateAccessCode(),
                    Status = Status.New
                });
                slot.FreePlaces -= request.ReserveCount;
                slotRepository.Update(slot);
                return Ok();
            }
            return BadRequest($"Не достаточно свободных мест на слоте. Достуных мест: {slot.FreePlaces}");

        }
        [HttpGet(Name = "GetNextReservation")]
        public IActionResult GetNextReservation()
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = reservationRepository.GetNextReservation(user.Id);
            if (res is null)
            {
                return Ok();
            }

            var response = new GetNextReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
                Name = res.Slot.Activity.ActivityName,
                ImageUrl = res.Slot.Activity.ImageUrl,
                DateTime = res.Slot.SlotDateTime,
                AddressString = $"{res.Slot.Activity.Location?.AddressCity ?? ""} {res.Slot.Activity.Location?.AddressStreet ?? ""} {res.Slot.Activity.Location?.AddressBuildingNumber ?? ""}",
                Coordinates = new Coordinates
                {
                    Latitude = res.Slot.Activity.Location.AddressLatitude,
                    Longitude = res.Slot.Activity.Location.AddressLongitude
                },
                LocationName = res.Slot.Activity.Location.LocationName,
                ReservationId = res.Id,
                Status = res.Status

            };
            return Ok(response);

        }
        [HttpGet(Name = "GetReservation")]
        public IActionResult GetReservation(int reservationId)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res is null)
            {
                return BadRequest($"Брони с id {reservationId} не существует");
            }
            if (res.UserId != user.Id)
            {
                return BadRequest("Данная бронь принадлежит другому пользователю");
            }

            var response = new GetReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
                ReservationId = reservationId,
                AccessCode = res.AccessCode,
                DateTime = res.Slot.SlotDateTime,
                SlotId = res.SlotId,
                Count = res.Count,
                Status = res.Status,

            };
            return Ok(response);
        }
        [HttpGet(Name = "GetReservations")]
        public IActionResult GetReservations(DateTime startDate, DateTime endDate)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var reservations = reservationRepository.GetReservationsByUser(user.Id)
                .Where(x => x.Slot.SlotDateTime >= startDate && x.Slot.SlotDateTime <= endDate);
            if (reservations is null)
            {
                return Ok("Нет броней");
            }
            var reservationsResponse = reservations.Select(x => new GetSingleReservation
            {
                Address = $"{x.Slot.Activity.Location.AddressCity} {x.Slot.Activity.Location.AddressStreet} {x.Slot.Activity.Location.AddressBuildingNumber}",
                DateTime = x.Slot.SlotDateTime,
                ImageUrl = x.Slot.Activity.ImageUrl,
                LocationName = x.Slot.Activity.Location.LocationName,
                ReservationId = x.Id,
                ActivityId = x.Slot.ActivityId,
                ActivityName = x.Slot.Activity.ActivityName,
                Coordinates = new Coordinates
                {
                    Latitude = x.Slot.Activity.Location.AddressLatitude,
                    Longitude = x.Slot.Activity.Location.AddressLongitude
                },
                Status = x.Status

            }).ToList();
            var response = new GetReservationsResponse
            {
                SelectedDateTimeStart = startDate,
                SelectedDateTimeEnd = endDate,
                Reservations = reservationsResponse

            };
            return Ok(response);
        }

        [HttpPost(Name = "SetReservationCanceledByUser")]
        public IActionResult SetReservationCanceledByUser(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res is null)
            {
                return BadRequest("Брони не найдены");
            }
            if (res.UserId != user.Id)
            {
                return BadRequest("Данная бронь принадлежит другому пользователю");
            }
            if (res.Status != Status.New && res.Status != Status.Confirmed)
            {
                return BadRequest("Бронь должна быть \"New\" или \"Confirmed\"");
            }
            if (res.Slot.SlotDateTime - DateTime.Now < new TimeSpan(12, 0, 0))
            {
                return BadRequest("Нельзя отменить бронь менее, чем за 12 часов");
            }
            res.Status = Status.CanceledByUser;
            reservationRepository.Update(res);
            return Ok();
        }
    }
}