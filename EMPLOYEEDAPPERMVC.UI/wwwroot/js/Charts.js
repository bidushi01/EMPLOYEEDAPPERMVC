$(function () {
    $.getJSON("/ServiceBooking/GetDashboardCharts")
        .done(function (data) {
            $("#differentcharts").empty();

            var customerData = data.customers || [];
            var serviceData = data.services || [];
            var employeeData = data.employees || [];
            var statusData = data.status || [];
            var monthlyData = data.monthly || [];

            $("<div>").addClass("mb-4").attr("id", "customerChart").appendTo("#differentcharts");
            $("<div>").addClass("mb-4").attr("id", "serviceChart").appendTo("#differentcharts");
            $("<div>").addClass("mb-4").attr("id", "employeeChart").appendTo("#differentcharts");
            $("<div>").addClass("mb-4").attr("id", "statusChart").appendTo("#differentcharts");
            $("<div>").addClass("mb-4").attr("id", "monthlyChart").appendTo("#differentcharts");

            $("#customerChart").dxChart({
                dataSource: customerData,
                series: {
                    argumentField: "label",
                    valueField: "value",
                    type: "bar",
                    color: "#28a745"
                },
                title: "Customer Bookings",
                export: {
                    enabled: true
                }
            });

            $("#serviceChart").dxPieChart({
                dataSource: serviceData,
                size: { width: 500 },
                palette: "bright",
                series: {
                    argumentField: "label",
                    valueField: "value",
                    label: {
                        visible: true,
                        connector: {
                            visible: true,
                            width: 1
                        }
                    }
                },
                title: "Service Bookings",
                
                export: {
                    enabled: true
                }
                
            });

            $("#employeeChart").dxChart({
                dataSource: employeeData,
                series: {
                    argumentField: "label",
                    valueField: "value",
                    type: "bar",
                    color: "#007bff"
                },
                title: "Employee Bookings",
                export: {
                    enabled: true
                }
                
            });

            $("#statusChart").dxPieChart({
                dataSource: statusData,
                size: { width: 500 },
                palette: "bright",
                series: {
                    argumentField: "label",
                    valueField: "value",
                    label: {
                        visible: true,
                        connector: {
                            visible: true,
                            width: 1
                        }
                    }
                },
                title: "Booking Status",
                export: {
                    enabled: true
                }
            });

            $("#monthlyChart").dxChart({
                dataSource: monthlyData,
                series: {
                    argumentField: "label",
                    valueField: "value",
                    type: "line",
                    color: "#ffc107"
                },
                title: "Monthly Bookings",
                
                export: {
                    enabled: true
                }
               
            });
        })
        .fail(function () {
            alert("Failed to load dashboard charts");
        });
});

