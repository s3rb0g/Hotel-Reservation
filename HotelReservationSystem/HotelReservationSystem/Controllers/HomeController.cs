using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HotelReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            var imagePaths = Directory.GetFiles(imageDirectory);

            var imageInfoList = imagePaths.Select(imagePath => new ImageInfo
            {
                RelativePath = "/images/" + Path.GetFileName(imagePath),
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(imagePath)
            }).ToList();

            return View(imageInfoList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Appointment()
        {
            return View();
        }

        public IActionResult Records()
        {
            var records = _context.Reservations.ToList();
            return View(records);
        }
        
        



        private readonly HotelReservationContext _context;

        public IActionResult SaveReservation(ReservationModel model)
        {
            if (ModelState.IsValid)
            {
                return View("SaveReservation", model);
            }
            return View("Appointment", model);
        }
        /*
                public IActionResult DeleteRecord(int id)
                {
                    var record = _context.Reservations.Find(id);
                    if (record != null)
                    {
                        _context.Reservations.Remove(record);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Records");
                }

               */

        [HttpPost]
        public IActionResult DeleteRecord(string name, DateTime checkInDate, DateTime checkOutDate)
        {
            var record = _context.Reservations.FirstOrDefault(r => r.Name == name && r.CheckIn == checkInDate && r.CheckOut == checkOutDate);
            if (record != null)
            {
                _context.Reservations.Remove(record);
                _context.SaveChanges();
            }
            return RedirectToAction("Records");
        }

        public IActionResult UpdateRecord(string name, DateTime checkInDate, DateTime checkOutDate)
        {
            var record = _context.Reservations.FirstOrDefault(r => r.Name == name && r.CheckIn == checkInDate && r.CheckOut == checkOutDate);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        [HttpPost]
        public IActionResult UpdateRecord(Reservation updatedReservation, string originalName, DateTime originalCheckInDate, DateTime originalCheckOutDate)
        {
            if (ModelState.IsValid)
            {
                var record = _context.Reservations.FirstOrDefault(r => r.Name == originalName && r.CheckIn == originalCheckInDate && r.CheckOut == originalCheckOutDate);
                if (record != null)
                {
                    record.Name = updatedReservation.Name;
                    record.CheckIn = updatedReservation.CheckIn;
                    record.CheckOut = updatedReservation.CheckOut;
                    _context.SaveChanges();
                }
                return RedirectToAction("Records");
            }
            return View(updatedReservation);
        }

        public IActionResult UpdateRecord()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
