﻿using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using Data.Entities;
using Utility;

namespace ReChargeBackend.Data
{
    public class TestSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<AppDbContext>();
            context.Database.OpenConnection();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    AccessHash = Hasher.Encrypt("123"),
                    BirthDate = DateTime.Now,
                    Email = "mihsasandomirskiy@gmail.com",
                    Gender = "male",
                    Id = 0,
                    Name = "Mikhail",
                    PhoneNumber = "+79251851096",
                    Surname = "Sandomirskii",
                    City = "Москва",
                    ImageUrl = "https://images.ctfassets.net/h6goo9gw1hh6/2sNZtFAWOdP1lmQ33VwRN3/24e953b920a9cd0ff2e1d587742a2472/1-intro-photo-final.jpg?w=1200&h=992&fl=progressive&q=70&fm=jpg"
                });
            }

            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.location ON");
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                new Location
                {
                    Id = 2,
                    AddressBuildingNumber = "1с5",
                    AddressCity = "Москва",
                    AddressLatitude = 55.824104,
                    AddressLongitude = 37.499872,
                    AddressNearestMetro = "Войковская",
                    AddressStreet = "Старопетровский проезд",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Спортивный зал Fitness Planet на Войковской это новейший спортивный зал с чем-то там еще новым," +
                    "типа новыми технологиями и добрыми тренерами по выгодной цене",
                    LocationName = "Fitness Planet"
                },
                new Location
                {
                    Id = 1,
                    AddressBuildingNumber = "39с53",
                    AddressCity = "Москва",
                    AddressLatitude = 55.837442,
                    AddressLongitude = 37.479109,
                    AddressNearestMetro = "Водный стадион",
                    AddressStreet = "Ленинградское шоссе",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Бассейн Динамо предлагает вам прийти к ним покупаться и узнать, что такое понастоящему чистая вода." +
                    " Настолько гениально продуманой раздевалки не видал народ никогда и вы просто офигеете, когда узнаете, что кладут в пирожки" +
                    " в местной столовке",
                    LocationName = "Бассейн Динамо"
                },
                new Location
                {
                    Id = 3,
                    AddressBuildingNumber = "16с60",
                    AddressCity = "Москва",
                    AddressLatitude = 55.805673,
                    AddressLongitude = 37.645428,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "3-я Мытищинская улица",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Новый зал единоборств клуба Ударник Алексеевская / ВДНХ это один из самых просторных залов нашего клуба." +
                    " Проводятся тренировки по Боксу, ММА, Детскому САМБО Кикбоксингу, Тайскому боксу, ФитБоксу, Карате. В" +
                    " зале установлен профессиональный боксерский ринг",
                    LocationName = "Боксерский клуб Ударник Алексеевская"
                },
                new Location
                {
                    Id = 4,
                    AddressBuildingNumber = "25Ас2",
                    AddressCity = "Москва",
                    AddressLatitude = 55.827385,
                    AddressLongitude = 37.479324,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "Ленинградское шоссе",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Супер тренировки, супер корты, самое новое оборудование и добрые тренера (или тренеры)." +
                    " Нет в мире лучше теннисного клуба, чем Tennis Capital",
                    LocationName = "Tennis Capital"
                }
                );
            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.location OFF");

            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.category ON");
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Id = 4, Name = "Плаванье", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTFsFjfGYNytLnLRpVMHt9LBAue_yc8XqceVG4Z9-vIfA&s", CategoryCategoryId = 0 },
                    new Category { Id = 3, Name = "Спорт залы", Image = "https://t4.ftcdn.net/jpg/03/29/67/97/360_F_329679742_4vrHnqpRSsqiTrLWEsmpLwvwHc3aNc4I.jpg", CategoryCategoryId = 0 },
                    new Category { Id = 1, Name = "Бокс", Image = "https://static.vecteezy.com/system/resources/thumbnails/000/421/048/small/Sports__28112_29.jpg", CategoryCategoryId = 0},
                    new Category { Id = 2, Name = "Теннис", Image = "https://icons.iconarchive.com/icons/iconsmind/outline/512/Tennis-icon.png", CategoryCategoryId = 0 },
                    new Category { Id = 5, Name = "Ноготички", Image = "https://i.pinimg.com/236x/ee/07/83/ee078346b43a46705c4da433b6c7e392.jpg", CategoryCategoryId = 1 },
                    new Category { Id = 6, Name = "Массаж", Image = "https://actual-cosmetology.ru/wp-content/uploads/2022/08/7-1.jpg", CategoryCategoryId = 1 }
                );
            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.category OFF");


            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.activity ON");
            if (!context.Activities.Any())
            {
                context.Activities.AddRange(
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Свободное плавание в бассейне Чайка это 5 дорожек длинной " +
                    "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо",
                        ActivityName = "Свободное плавание 45 минут",
                        CategoryId = 4,
                        Id = 1,
                        LocationId = 1,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется справка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Проход в Fitness Planet даёт вам безлимитный доступ ко всем тренажёрам, " +
                        "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров",
                        ActivityName = "Проход в Fitness Planet",
                        CategoryId = 3,
                        Id = 2,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        WarningText = "",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Тренировка с профессиональным тренером это намного эффективнее и круче! " +
                        "А если вам кажется, что вы и сами все знаете, попробуйте тренировку с тренером в Fitness Planet и познайте, что такое" +
                        "по настоящему интенсивная тренировка ",
                        ActivityName = "Часовая тренировка с личным тренером",
                        CategoryId = 3,
                        Id = 3,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        WarningText = "",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Час с тренером в клубе Ударник, это самый ценный час в вашей жизни. " +
                        "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов.",
                        ActivityName = "Часовая тренировка с тренером",
                        CategoryId = 1,
                        Id = 4,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        WarningText = "Вас будут бить",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "3 часа в группе в клубе Ударник, это самые ценные 3 часа в вашей жизни. " +
                        "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов, и практикуйтесь вместе с группой.",
                        ActivityName = "3-ех часовое занятие в группе",
                        CategoryId = 1,
                        Id = 5,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        WarningText = "Вас будут бить",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Самые лучшие корты в Москве. Регулярно убираются и чистятся. Наши уборщики вообще не спят.",
                        ActivityName = "Аренда корта на час",
                        CategoryId = 2,
                        Id = 6,
                        LocationId = 4,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Тренер вам покажет как это делается. Вы ничего не умеете, а он умеет всё. Вы тупые, он гений.",
                        ActivityName = "2-ух часовое занятие с тренером",
                        CategoryId = 2,
                        Id = 7,
                        LocationId = 4,
                        ShouldDisplayWarning = false,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    }
                );

            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.activity OFF");
            
            if (!context.Slots.Any())
            {
                context.Slots.AddRange(
                      new Slot()
                      {
                          ActivityId = 1,
                          Id = 1,
                          FreePlaces = 5,
                          Price = 1500,
                          SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0)
                      },
                new Slot()
                {
                    ActivityId = 1,
                    Id = 2,
                    FreePlaces = 5,
                    Price = 2000,
                    SlotDateTime = DateTime.Now.AddHours(1),
                    LengthMinutes = 45,
                },
                new Slot()
                {
                    ActivityId = 1,
                    Id = 3,
                    FreePlaces = 5,
                    Price = 1250,
                    SlotDateTime = DateTime.Now.AddHours(2),
                    LengthMinutes = 45,
                },

                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 4,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                    LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 5,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                    LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 6,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                    LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 7,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 8,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 9,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 10,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 11,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 12,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 13,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 14,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 15,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 16,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 17,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 18,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 19,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = DateTime.Now.AddHours(1),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 20,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = DateTime.Now.AddHours(2),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 21,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = DateTime.Now.AddHours(1).AddDays(1),
                            LengthMinutes = 45,
                        }
                    );
            }
            context.SaveChanges();

            if (!context.Reservations.Any())
            {
                context.Reservations.AddRange(
                    new Reservation { Id = 1, IsOver = false, SlotId = 2, UserId = 1, Count = 1, Email = "mihsasandomirskiy@gmail.com", Name = "Misha", PhoneNumber = "+79251851096" },
                    new Reservation { Id = 2, IsOver = true, SlotId = 1, UserId = 1, Count = 3, Email = "mihsasandomirskiy@gmail.com", Name = "Misha", PhoneNumber = "+79251851096" },
                    new Reservation { Id = 3, IsOver = false, SlotId = 4, UserId = 1, Count = 1, Email = "gled@gmail.com", Name = "Zhora", PhoneNumber = "+79251851096" }
                );
            }
            context.SaveChanges();

            context.Database.CloseConnection();
        }
    }
}
