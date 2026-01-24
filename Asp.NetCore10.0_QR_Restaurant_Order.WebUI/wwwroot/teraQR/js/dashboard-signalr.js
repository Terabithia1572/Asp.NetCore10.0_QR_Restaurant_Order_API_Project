"use strict";

// 🔹 API projesinin adresine göre URL (Swagger hangi portta ise onu yaz)
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7074/SignalRHub") // <--- Burası KRİTİK
    .withAutomaticReconnect()
    .build();

connection.on("ReceiveDashboardSummary", function (summary) {
    document.getElementById("todayRevenue").innerText =
        summary.todayRevenue.toLocaleString("tr-TR", { style: "currency", currency: "TRY" });

    document.getElementById("totalOrderCount").innerText = summary.totalOrderCount;
    document.getElementById("todayOrderCount").innerText = summary.todayOrderCount;
    document.getElementById("monthlyRevenue").innerText =
        summary.monthlyRevenue.toLocaleString("tr-TR", { style: "currency", currency: "TRY" });
    document.getElementById("monthlyOrderCount").innerText = summary.monthlyOrderCount;
    document.getElementById("totalGuestCount").innerText = summary.totalGuestCount;
    document.getElementById("activeTableCount").innerText = summary.activeTableCount;
});

// Biraz daha sağlam start fonksiyonu
async function startConnection() {
    try {
        await connection.start();
        console.log("✅ SignalR connected.");
    } catch (err) {
        console.error("❌ SignalR connection error:", err);
        setTimeout(startConnection, 5000);
    }
}

startConnection();
