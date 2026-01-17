$(function () {
    var today = new Date();
    var lastMonth = new Date();
    lastMonth.setDate(today.getDate() - 30);

    $("#startDateFilter").dxDateBox({
        type: "date",
        value: lastMonth,
        displayFormat: "MM/dd/yyyy"
    });

    $("#endDateFilter").dxDateBox({
        type: "date",
        value: today,
        displayFormat: "MM/dd/yyyy"
    });

    $("#serviceFilter").dxSelectBox({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function () {
                return $.getJSON("/ServiceBooking/GetAvailableServices");
            }
        }),
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select a Service",
        showClearButton: true,
        searchEnabled: true
    });

    var grid = $("#bookingSummaryGrid").dxDataGrid({
        dataSource: [],
        keyExpr: "bookingID",
        showBorders: true,
        columnAutoWidth: true,
        noDataText: "No service available",
        columns: [
            { dataField: "employeeName", caption: "Employee Name" },
            { dataField: "customerName", caption: "Customer Name" },
            { dataField: "serviceName", caption: "Service Name" },
            { dataField: "bookingDate", caption: "Booking Date", dataType: "date", format: "MM/dd/yyyy" }
        ]
    }).dxDataGrid("instance");

    function loadSummary() {
        var startDateValue = $("#startDateFilter").dxDateBox("instance").option("value");
        var endDateValue = $("#endDateFilter").dxDateBox("instance").option("value");
        var serviceIdValue = $("#serviceFilter").dxSelectBox("instance").option("value");

        var query = {
            startDate: startDateValue ? startDateValue.toISOString().split("T")[0] : null,
            endDate: endDateValue ? endDateValue.toISOString().split("T")[0] : null,
            serviceId: serviceIdValue || null
        };

        $.getJSON("/ServiceBooking/GetServiceBookingSummary", query)
            .done(function (data) {
                grid.option("dataSource", data);
            })
            .fail(function () {
                alert("Failed to load booking summary");
            });
    }

    $("#btnSearchSummary").on("click", function () {
        loadSummary();
    });

    loadSummary();
});








