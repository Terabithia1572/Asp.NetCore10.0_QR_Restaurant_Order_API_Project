"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/dashboardHub")
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

    // İstersen burada ApexCharts / grafikleri de update edersin
});

connection.start().catch(err => console.error(err.toString()));
