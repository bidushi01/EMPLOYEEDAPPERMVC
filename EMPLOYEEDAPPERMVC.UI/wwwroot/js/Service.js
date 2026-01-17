const validationGroupNameService = "ServiceForm";

function initServiceForm() {
    $("#ValidationsummaryService").dxValidationSummary({
        validationGroup: validationGroupNameService
    });

    const serviceId = parseInt($("#serviceId").val()) || 0;

    const nameValue = $("#txtServiceName").data("value") || "";
    const durationValue = parseInt($("#txtDurationMinute").data("value")) || 0;
    const chargeValue = parseFloat($("#txtCharge").data("value")) || 0;

    $("#txtServiceName").dxTextBox({ value: nameValue }).dxValidator({
        validationGroup: validationGroupNameService,
        validationRules: [{ type: "required", message: "Service name is required" }]
    });

    $("#txtDurationMinute").dxNumberBox({
        value: durationValue,
        min: 1,
        showSpinButtons: true
    }).dxValidator({
        validationGroup: validationGroupNameService,
        validationRules: [
            { type: "required", message: "Duration is required" },
            { type: "range", min: 1, message: "Duration must be at least 1 minute" }
        ]
    });

    $("#txtCharge").dxNumberBox({
        value: chargeValue,
        format: "#,##0.00",
        min: 0,
        showSpinButtons: true
    }).dxValidator({
        validationGroup: validationGroupNameService,
        validationRules: [
            { type: "required", message: "Charge is required" },
            { type: "range", min: 0, message: "Charge must be 0 or greater" }
        ]
    });

    $("#btnSaveService").off().on("click", function () {
        const result = DevExpress.validationEngine.validateGroup(validationGroupNameService);
        if (!result.isValid) return;

        const data = {
            Id: serviceId,
            Name: $("#txtServiceName").dxTextBox("instance").option("value"),
            DurationMinute: $("#txtDurationMinute").dxNumberBox("instance").option("value"),
            Charge: $("#txtCharge").dxNumberBox("instance").option("value")
        };

        $.post("/Service/Save", data).done(function() {
            $("#popupService").dxPopup("instance").hide();
            $("#gridServices").dxDataGrid("instance").refresh();
        }).fail(function() {
            alert("Save failed");
        });
    });
}

$(function () {
    const grid = $("#gridServices");
    if (!grid.length) return;

    grid.dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function() {
                return $.getJSON("/Service/GetServices");
            }
        }),
        showBorders: true,
        paging: { pageSize: 10 },
        columns: [
            { dataField: "name", caption: "Service Name" },
            { dataField: "durationMinute", caption: "Duration (min)", dataType: "number" },
            { dataField: "charge", caption: "Charge", dataType: "number", format: "#,##0.00" },
            {
                caption: "Action",
                width: 150,
                cellTemplate: function (container, options) {
                    $("<button>")
                        .addClass("btn btn-primary btn-sm me-1")
                        .text("Edit")
                        .appendTo(container)
                        .on("click", function() {
                            openServicePopup(options.data.id);
                        });

                    $("<button>")
                        .addClass("btn btn-danger btn-sm")
                        .text("Delete")
                        .appendTo(container)
                        .on("click", function () {
                            if (confirm("Delete this service?")) {
                                $.post("/Service/Delete", { id: options.data.id })
                                    .done(function() {
                                        grid.dxDataGrid("instance").refresh();
                                    })
                                    .fail(function() {
                                        grid.dxDataGrid("instance").refresh();
                                    });
                            }
                        });
                }
            }
        ]
    });

    $("#btnAddService").on("click", function() {
        openServicePopup();
    });
});

function openServicePopup(id) {
    $.get("/ServiceModal/AddOrEdit", { id })
        .done(function (html) {
            $("#popupService").dxPopup({
                width: 500,
                height: "auto",
                visible: true,
                showTitle: true,
                title: "Add / Edit Service",
                contentTemplate: function() {
                    return $("<div>").html(html);
                },
                onShown: function() {
                    initServiceForm();
                }
            });
        });
}