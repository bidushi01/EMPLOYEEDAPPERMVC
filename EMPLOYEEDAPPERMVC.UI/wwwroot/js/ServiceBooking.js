var validationGroupName = "ServiceBookingForm";

function initBookingForm() {
    var employeeId = parseInt($("#hiddenEmployeeId").val()) || 0;
    var serviceId = parseInt($("#hiddenServiceId").val()) || 0;
    var bookingDate = $("#hiddenBookingDate").val() || "";
    var startTime = $("#hiddenStartTime").val() || "";
    var endTime = $("#hiddenEndTime").val() || "";
    var serviceNameValue = $("#txtServiceName").data("value") || "";
    var bookingDateValue = $("#txtBookingDate").data("value") || "";
    var timeSlotValue = $("#txtTimeSlot").data("value") || "";

    $("#ValidationSummary").dxValidationSummary({
        validationGroup: validationGroupName
    });

    $("#txtServiceName").dxTextBox({
        value: serviceNameValue,
        readOnly: true
    });
   
    $("#txtBookingDate").dxTextBox({
        value: bookingDateValue,
        readOnly: true
    });
 
    $("#txtTimeSlot").dxTextBox({
        value: timeSlotValue,
        readOnly: true
    });
  
    $("#txtCustomerName").dxTextBox({ value: "" }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [{ type: "required", message: "Name is required" }]
    });
  
    $("#txtCustomerPhone").dxTextBox({ value: "" }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [
            { type: "required", message: "Phone is required" },
            { type: "pattern", pattern: /^\d{10}$/, message: "Phone must be 10 digits" }
        ]
    });
 
    $("#txtCustomerEmail").dxTextBox({ value: "" }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [{ type: "email", message: "Invalid email" }]
    });

    $("#btnSaveBooking").off("click").on("click", function () {
        
        var result = DevExpress.validationEngine.validateGroup(validationGroupName);
        
        if (!result.isValid) return;

   
        var data = {
            employeeId: employeeId,
            serviceId: serviceId,
            bookingDate: bookingDate,
            startTime: startTime,
            endTime: endTime,
            customerName: $("#txtCustomerName").dxTextBox("instance").option("value"),
            customerPhone: $("#txtCustomerPhone").dxTextBox("instance").option("value"),
            customerEmail: $("#txtCustomerEmail").dxTextBox("instance").option("value")
        };

       
        $.post("/ServiceBooking/Save", data)
            .done(function (response) {
                if (response.success) {
                    $("#popupBooking").dxPopup("instance").hide();
                    DevExpress.ui.notify(response.message || "Booking successful!", "success", 2000);
                    $("#btnSearchSlots").click();
                } else {
                    alert(response.message || "Failed to save booking");
                }
            })
            .fail(function () {
                alert("Failed to save booking");
            });
    });
}


$(function () {
  
    $("#selectService").dxSelectBox({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
        
            load: function() {
                return $.getJSON("/ServiceBooking/GetAvailableServices");
            }
        }),
       
        displayExpr: "name",
       
        valueExpr: "id",
        placeholder: "Select a Service",
        showClearButton: true,
        searchEnabled: true
    });

   
    var today = new Date();
  
    $("#selectDate").dxDateBox({
        type: "date",
        value: today,
        min: today,
        displayFormat: "MM/dd/yyyy",
        useMaskBehavior: false
    });

    $("#btnSearchSlots").on("click", function () {
      
        var serviceId = $("#selectService").dxSelectBox("instance").option("value");
        
        var selectedDate = $("#selectDate").dxDateBox("instance").option("value");

       
        if (!serviceId) {
            DevExpress.ui.notify("Please select a service", "error", 2000);
            return;
        }

   
        if (!selectedDate) {
            DevExpress.ui.notify("Please select a date", "error", 2000);
            return;
        }

        var dateString = selectedDate.toISOString().split("T")[0];

     
        $.post("/ServiceBooking/GetAvailableSlots", { serviceId: serviceId, bookingDate: dateString })
            .done(function (slots) {
                
                if (slots.length === 0) {
                    DevExpress.ui.notify("No slots available on this date", "warning", 3000);
                    $("#slotsSection").hide();
                } else {
                    showAvailableSlots(slots);
                    $("#slotsSection").show();
                }
            })
            .fail(function () {
               
                alert("Failed to load slots");
            });
    });
});


function showAvailableSlots(slots) {
    
    $("#slotsContainer").empty();
    
   
    var employeeGroups = {};
    slots.forEach(function(slot) {
        var empName = slot.employeeName;
        if (!employeeGroups[empName]) {
            employeeGroups[empName] = [];
        }
        employeeGroups[empName].push(slot);
    });
    
    
    for (var empName in employeeGroups) {
        
        var $empSection = $("<div>").addClass("mb-4");
        
       
        var $empHeader = $("<h5>")
            .addClass("mb-3")
            .css({
                "font-weight": "bold",
                "color": "#333",
                "border-bottom": "2px solid #28a745",
                "padding-bottom": "5px"
            })
            .text(empName);
        
       
        var $empSlots = $("<div>")
            .addClass("d-flex flex-wrap gap-2");
        
 
        employeeGroups[empName].forEach(function(slot) {
        
            var timeStr = formatTimeSlot(slot.startTime, slot.endTime);
       
            var isBooked = slot.booked === 1;
            
        
            var $btn = $("<button>")
                .addClass(isBooked ? "btn btn-danger" : "btn btn-success")
                .text(timeStr)
                .css({
                    "min-width": "120px",
                    "padding": "10px 15px",
                    "margin": "2px"
                });
           
            if (isBooked) {
                $btn.prop("disabled", true);
            } else {
              
                $btn.on("click", function() {
                    openBookingPopup(slot);
                });
            }
            
            $empSlots.append($btn);
        });
       
        $empSection.append($empHeader);
        $empSection.append($empSlots);
      
        $("#slotsContainer").append($empSection);
    }
}


function formatTimeSlot(startTime, endTime) {
    
    var start = startTime.substring(0, 5);
    var end = endTime.substring(0, 5);
   
    return start + "-" + end;
}


function openBookingPopup(slot) {
    var serviceId = $("#selectService").dxSelectBox("instance").option("value");
    var serviceName = $("#selectService").dxSelectBox("instance").option("text");
    var selectedDate = $("#selectDate").dxDateBox("instance").option("value");
    var timeDisplay = formatTimeSlot(slot.startTime, slot.endTime);

    $.get("/ServiceBookingModal/AddOrEdit", {
        employeeId: slot.employeeID,
        serviceId: serviceId,
        serviceName: serviceName,
        bookingDate: selectedDate.toISOString().split("T")[0],
        timeSlot: timeDisplay,
        startTime: slot.startTime,
        endTime: slot.endTime
    })
        .done(function (html) {
           
            $("#popupBooking").dxPopup({
                width: 600,
                height: "auto",
                visible: true,
                showTitle: true,
                title: "Book This Slot",
                closeOnOutsideClick: true,
                contentTemplate: function () {
                    return $("<div>").html(html);
                },
                onShown: function () {
                    
                    initBookingForm();
                }
            });
        });
}
