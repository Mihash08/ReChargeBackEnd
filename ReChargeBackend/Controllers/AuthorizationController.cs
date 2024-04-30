﻿using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using Data.Interfaces;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Utility;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Utility;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        public AuthorizationController(IUserRepository userRepository, IVerificationCodeRepository verificationCodeRepository) 
        {
            this.userRepository = userRepository;
            this.verificationCodeRepository = verificationCodeRepository;
        }

        private readonly ILogger<UserController> _logger;

        [HttpPost(Name = "RequestCode")]
        public IActionResult AuthPhone([FromBody] PhoneAuthRequest info)
        {
            //TODO: IMPLEMENT PROPER NUMBER CHECKING
            if (Temp.IsPhoneNumberValid(info.phoneNumber))
            {
                var sessionId = Temp.GenerateSessionId();
                //var code = Temp.GenerateCode();
                var code = "12345";
                verificationCodeRepository.Add(new VerificationCode()
                {
                    Code = Hasher.Encrypt(code),
                    PhoneNumber = info.phoneNumber,
                    SessionId = sessionId,
                });
                //TODO: send code to phone
                Console.WriteLine(code);
                return Ok(new PhoneAuthResponse()
                {
                    SessionId = sessionId,
                    TitleText = "Введите полученный код",
                    CodeSize = 5,
                    ConditionalInfo = new ConditionalInfoResponse()
                    {
                        Message = "Совершая авторизацию,\nвы соглашаетесь с правилами сервиса",
                        Url = "google.com"
                    }

                });
            }

            return BadRequest("Невалидный номер телефона");
        }
        [HttpGet(Name = "GetConditionalInfo")]
        public IActionResult GetConditionalInfo()
        {
            return Ok(new ConditionalInfoResponse()
            {
                Message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                Url = "google.com"
            });
        }

        [HttpPost(Name = "Auth")]
        public IActionResult Auth([FromBody] AuthRequest info)
        {
            try
            {
                var session = verificationCodeRepository.GetBySession(info.sessionId);
                if (Hasher.Verify(info.code, session.Code))
                {
                    var user = userRepository.GetByNumber(session.PhoneNumber);
                    string accessToken = Temp.GenerateAccessToken();
                    if (user is null)
                    {
                        if (DateTime.Now - session.CreationDateTime < new TimeSpan(0, 5, 0))
                        {
                            return BadRequest("Время действия кода истекло");
                        }
                        userRepository.Add(new User()
                        {
                            PhoneNumber = session.PhoneNumber,
                            AccessHash = Hasher.Encrypt(accessToken),
                            ImageUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png"
                        });
                    } else
                    {
                        user.AccessHash = Hasher.Encrypt(accessToken);
                        userRepository.Update(user);
                    }
                    verificationCodeRepository.Delete(session);
                    return Ok(new AuthResponse { AccessToken = accessToken});
                }
                return BadRequest("Неправильный код");
            } catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return BadRequest("Сессия не найдена");

        }


    }
}