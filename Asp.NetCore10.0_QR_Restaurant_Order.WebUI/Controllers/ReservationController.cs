using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class ReservationController : Controller
    {
        // Şimdilik dummy veriler – ileride WebAPI + DB ile değiştiririz
        private static readonly List<ReservationViewModel> _reservations = new()
        {
            new ReservationViewModel
            {
                Id = 1,
                CustomerName = "Ahmet Yılmaz",
                Phone = "+90 532 000 00 01",
                Email = "ahmet.yilmaz@example.com",
                GuestCount = 4,
                ReservationDate = DateTime.Today.AddHours(20), // bugün 20:00
                TableId = 5,
                Status = ReservationStatus.Confirmed,
                Source = ReservationSource.Phone,
                IsVip = true,
                Notes = "Doğum günü kutlaması, pasta servisi rica edildi."
            },
            new ReservationViewModel
            {
                Id = 2,
                CustomerName = "Zeynep Demir",
                Phone = "+90 532 000 00 02",
                Email = "zeynep.demir@example.com",
                GuestCount = 2,
                ReservationDate = DateTime.Today.AddDays(1).AddHours(19), // yarın 19:00
                TableId = null,
                Status = ReservationStatus.Pending,
                Source = ReservationSource.Web,
                IsVip = false,
                Notes = "Pencere kenarı masa talep ediyor."
            },
            new ReservationViewModel
            {
                Id = 3,
                CustomerName = "Mehmet Kaya",
                Phone = "+90 532 000 00 03",
                Email = "mehmet.kaya@example.com",
                GuestCount = 6,
                ReservationDate = DateTime.Today.AddDays(-1).AddHours(21), // dün 21:00
                TableId = 3,
                Status = ReservationStatus.Completed,
                Source = ReservationSource.QR,
                IsVip = true,
                Notes = "Kurumsal misafirler ile geldi."
            },
            new ReservationViewModel
            {
                Id = 4,
                CustomerName = "Elif Karaca",
                Phone = "+90 532 000 00 04",
                Email = "elif.karaca@example.com",
                GuestCount = 3,
                ReservationDate = DateTime.Today.AddHours(18).AddMinutes(30).AddSeconds(0), // bugün 18:30
                TableId = 2,
                Status = ReservationStatus.Seated,
                Source = ReservationSource.WalkIn,
                IsVip = false,
                Notes = "Glutensiz ekmek talebi var."
            }
        };

        // GENEL LİSTE – /Reservation/Index
        public IActionResult Index()
        {
            var model = _reservations
                .OrderBy(r => r.ReservationDate)
                .ToList();

            return View(model);
        }

        // SADECE BEKLEYENLER – /Reservation/Pending
        public IActionResult Pending()
        {
            var model = _reservations
                .Where(r => r.Status == ReservationStatus.Pending)
                .OrderBy(r => r.ReservationDate)
                .ToList();

            ViewData["Title"] = "Bekleyen Rezervasyonlar";
            return View("Index", model);
        }

        // BUGÜN – /Reservation/Today
        public IActionResult Today()
        {
            var today = DateTime.Today;
            var model = _reservations
                .Where(r => r.ReservationDate.Date == today)
                .OrderBy(r => r.ReservationDate)
                .ToList();

            ViewData["Title"] = "Bugünkü Rezervasyonlar";
            return View("Index", model);
        }

        // DETAY – /Reservation/Detail/1
        public IActionResult Detail(int id)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
                return NotFound();

            return View(reservation);
        }
    }

    // R2 MODEL – Müşteri + Masa + Kaynak + Durum + VIP + Not
  

    public enum ReservationStatus
    {
        Pending = 0,
        Confirmed = 1,
        Seated = 2,
        Completed = 3,
        Cancelled = 4,
        NoShow = 5
    }

    public enum ReservationSource
    {
        Phone = 0,
        Web = 1,
        QR = 2,
        WalkIn = 3
    }

    // Küçük helper – istersen ayrı sınıfa taşıyabilirsin
    public static class ReservationExtensions
    {
        public static string ToStatusText(this ReservationStatus status)
        {
            return status switch
            {
                ReservationStatus.Pending => "Beklemede",
                ReservationStatus.Confirmed => "Onaylandı",
                ReservationStatus.Seated => "Masada",
                ReservationStatus.Completed => "Tamamlandı",
                ReservationStatus.Cancelled => "İptal",
                ReservationStatus.NoShow => "Gelmedi (No-Show)",
                _ => "Bilinmiyor"
            };
        }

        public static string ToSourceText(this ReservationSource source)
        {
            return source switch
            {
                ReservationSource.Phone => "Telefon",
                ReservationSource.Web => "Web",
                ReservationSource.QR => "QR",
                ReservationSource.WalkIn => "Walk-in",
                _ => "Diğer"
            };
        }
    }
}
