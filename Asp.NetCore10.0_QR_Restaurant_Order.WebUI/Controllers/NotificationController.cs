using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            // Şimdilik dummy veriler – ileride DB & SignalR ile gerçeklenecek
            var notifications = new List<NotificationViewModel>
            {
                new NotificationViewModel
                {
                    Id = 1,
                    Title = "Yeni Sipariş",
                    Message = "Masa 3 için yeni sipariş oluşturuldu.",
                    Type = NotificationType.Order,
                    CreatedAt = DateTime.Now.AddMinutes(-5),
                    IsRead = false
                },
                new NotificationViewModel
                {
                    Id = 2,
                    Title = "Rezervasyon Onayı",
                    Message = "19:30 için 4 kişilik rezervasyon onaylandı.",
                    Type = NotificationType.Info,
                    CreatedAt = DateTime.Now.AddMinutes(-25),
                    IsRead = true
                },
                new NotificationViewModel
                {
                    Id = 3,
                    Title = "Ödeme Uyarısı",
                    Message = "Masa 7 için ödeme henüz tamamlanmadı.",
                    Type = NotificationType.Warning,
                    CreatedAt = DateTime.Now.AddHours(-1),
                    IsRead = false
                },
                new NotificationViewModel
                {
                    Id = 4,
                    Title = "Stok Uyarısı",
                    Message = "Ribeye Steak stoğu kritik seviyede.",
                    Type = NotificationType.Stock,
                    CreatedAt = DateTime.Now.AddHours(-3),
                    IsRead = true
                }
            };

            return View(notifications);
        }
    }


    public enum NotificationType
    {
        Info,
        Order,
        Warning,
        Stock,
        System
    }
}
