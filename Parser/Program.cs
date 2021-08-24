﻿using System;
using System.Collections.Generic;
using Models.Availability;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfecs;
using Microsoft.Extensions.Logging;

namespace Parser
{
    class Program
    {
        private static IHotelManagerService _hotelManagerService;

        public Program(IHotelManagerService hotelManagerService)
        {
            _hotelManagerService = hotelManagerService;
        }

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            var container = Startup.ConfigureService();
            _hotelManagerService = container.GetRequiredService<IHotelManagerService>();

            Console.ForegroundColor = ConsoleColor.Red;
            //------------------Print Hotel Informtion
            List<string> hotelsInfo = _hotelManagerService.GetHotelName();
            Console.WriteLine($"Hotel Name is :{ hotelsInfo[0]} \n" +
                              $"Hotel Address is : {hotelsInfo[1]}");
            Console.WriteLine("\n----------------------------------------------------------------------------------\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            //------------------Print Hotel Description
            Console.WriteLine(_hotelManagerService.GetHodelDescription());
            Console.WriteLine("\n----------------------------------------------------------------------------------\n");

            Console.ForegroundColor = ConsoleColor.Green;
            //------------------print Availability Description
            RoomTable availability = _hotelManagerService.GetAvailability();
            Console.WriteLine(availability.Title);
            foreach (var item in availability.Header)
            {
                Console.Write(item + "\t\t\t\t\t\t\t");
            }
            Console.WriteLine("");
            foreach (var item in availability.Rooms)
            {
                foreach (var item2 in item.Max)
                {
                    Console.Write(item2 + "\t\t\t");
                }
                Console.WriteLine(item.RoomType);
            }
            Console.WriteLine("\n----------------------------------------------------------------------------------\n");


            Console.ForegroundColor = ConsoleColor.Yellow;
            // ------------------Print Alternative Hotels
            var hotels = _hotelManagerService.AlternativeHotel();
            foreach (var item in hotels)
            {
                Console.WriteLine(item.Title + ' ' + item.RateString + "\n");
                Console.WriteLine(item.Description);
                Console.WriteLine("Image Href :\t" + item.ImageHref);
                Console.WriteLine("Image Tooltip :\t" + item.ImageTooltip);
                Console.WriteLine("People Count :\t" + item.ViewCount);
                Console.WriteLine("Score :\t" + item.Score);
                Console.WriteLine("ReviewCount :\t" + item.ReviewCount);
                Console.WriteLine("ScoreTitle :\t" + item.ScoreTitle);
                Console.WriteLine("BookNowLink :\t" + item.BookNowLink);
                Console.WriteLine("********************************************************************************\n");
            }
            Console.WriteLine("\n----------------------------------------------------------------------------------\n");


            Console.ForegroundColor = ConsoleColor.Cyan;
            // ------------------Print AlternativeLink
            var Links = _hotelManagerService.GetAlternativeLink();
            foreach (var item in Links)
            {
                Console.WriteLine(item.Title +"\n\n\n" + item.URL);

                Console.WriteLine("********************************************************************************\n");
            }

            Console.ReadLine();
        }

    }
}
