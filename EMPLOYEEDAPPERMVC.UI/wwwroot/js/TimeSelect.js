const validationGroupNameTimeSelect = "TimeSelectForm";

function parseTime(timeString) {
    if (!timeString) return null;
    const parts = timeString.split(':');
    const date = new Date();
    date.setHours(parseInt(parts[0]), parseInt(parts[1]), 0, 0);
    return date;
}

function formatTime(date) {
    if (!date) return null;
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    return hours + ':' + minutes + ':00';
}

function initTimeSelectForm() {
    $("#ValidationSummaryTimeSlot").dxValidationSummary({
        validationGroup: validationGroupNameTimeSelect
    });

    const timeSlotId = parseInt($("#timeSlotId").val()) || 0;
    const employeeId = parseInt($("#employeeId").val()) || 0;
    const startTimeValue = parseTime($("#startTimeValue").val());
    const endTimeValue = parseTime($("#endTimeValue").val());

    $.getJSON("/TimeSelect/GetEmployees").done(function (employees) {
        $("#selectEmployee").dxSelectBox({
            dataSource: employees,
            valueExpr: "id",
            displayExpr: "name",
            value: employeeId,
            placeholder: "Choose Employee",
            searchEnabled: true
        }).dxValidator({
            validationGroup: validationGroupNameTimeSelect,
            validationRules: [{ type: "required", message: "Employee is required" }]
        });
    });

    $("#startTimeBox").dxDateBox({
        type: "time",
        value: startTimeValue,
        displayFormat: "HH:mm",
        placeholder: "Select start time"
    }).dxValidator({
        validationGroup: validationGroupNameTimeSelect,
        validationRules: [{ type: "required", message: "Start time is required" }]
    });

    $("#endTimeBox").dxDateBox({
        type: "time",
        value: endTimeValue,
        displayFormat: "HH:mm",
        placeholder: "Select end time"
    }).dxValidator({
        validationGroup: validationGroupNameTimeSelect,
        validationRules: [{ type: "required", message: "End time is required" }]
    });

    $("#btnSaveTimeSlot").off("click").on("click", function () {
        const result = DevExpress.validationEngine.validateGroup(validationGroupNameTimeSelect);
        if (!result.isValid) return;

        const startTimeDate = $("#startTimeBox").dxDateBox("instance").option("value");
        const endTimeDate = $("#endTimeBox").dxDateBox("instance").option("value");

        const data = {
            Id: timeSlotId,
            EmployeeId: $("#selectEmployee").dxSelectBox("instance").option("value"),
            StartTime: formatTime(startTimeDate),
            EndTime: formatTime(endTimeDate)
        };

        $.post("/TimeSelect/Save", data).done(function (response) {
            if (response.success) {
                $("#popupTimeSlot").dxPopup("instance").hide();
                $("#gridTimeSlots").dxDataGrid("instance").refresh();
            } else {
                alert(response.message || "Save failed");
            }
        }).fail(function () {
            alert("Save failed");
        });
    });
}

$(function () {
    const grid = $("#gridTimeSlots");
    if (!grid.length) return;

    grid.dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function () {
                return $.getJSON("/TimeSelect/GetTimeSlots");
            }
        }),
        showBorders: true,
        paging: { pageSize: 10 },
        columns: [
            { dataField: "employeeName", caption: "Employee" },
            {
                dataField: "startTime",
                caption: "Start Time",
                customizeText: function (cellInfo) {
                    if (!cellInfo.value) return "";
                    return cellInfo.value.substring(0, 5);
                }
            },
            {
                dataField: "endTime",
                caption: "End Time",
                customizeText: function (cellInfo) {
                    if (!cellInfo.value) return "";
                    return cellInfo.value.substring(0, 5);
                }
            },
            {
                caption: "Action",
                width: 150,
                cellTemplate: function (container, options) {
                    $("<button>")
                        .addClass("btn btn-primary btn-sm me-1")
                        .text("Edit")
                        .appendTo(container)
                        .on("click", function () {
                            openTimeSlotPopup(options.data.id);
                        });

                    $("<button>")
                        .addClass("btn btn-danger btn-sm")
                        .text("Delete")
                        .appendTo(container)
                        .on("click", function () {
                            if (confirm("Delete this time slot?")) {
                                $.post("/TimeSelect/Delete", { id: options.data.id })
                                    .done(function () {
                                        grid.dxDataGrid("instance").refresh();
                                    });
                            }
                        });
                }
            }
        ]
    });

    $("#btnAddTimeSlot").on("click", function () {
        openTimeSlotPopup();
    });
});

function openTimeSlotPopup(id) {
    $.get("/TimeSelectModal/AddOrEdit", { id: id || 0 })
        .done(function (html) {
            $("#popupTimeSlot").dxPopup({
                width: 500,
                height: "auto",
                visible: true,
                showTitle: true,
                title: "Add / Edit Time Slot",
                contentTemplate: function () {
                    return $("<div>").html(html);
                },
                onShown: function () {
                    initTimeSelectForm();
                }
            });
        });
}