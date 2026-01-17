//const validationGroupName = "EmployeeForm";

//function initEmployeeForm() {
//    setTimeout(function() {
//        const $employeeId = $("#employeeId");
//        const $txtName = $("#txtName");
//        const $txtEmail = $("#txtEmail");
//        const $txtPhone = $("#txtPhone");
//        const $txtAddress = $("#txtAddress");
//        const $btnSave = $("#btnSave");
//        const $validationSummary = $("#Validationsummary");

//        if (!$employeeId.length || !$txtName.length) {
//            setTimeout(initEmployeeForm, 100);
//            return;
//        }

//        $validationSummary.dxValidationSummary({
//            validationGroup: validationGroupName
//        });

//        const employeeId = parseInt($employeeId.val()) || 0;

//        let nameValue = $txtName.attr("data-value");
//        let emailValue = $txtEmail.attr("data-value");
//        let phoneValue = $txtPhone.attr("data-value");
//        let addressValue = $txtAddress.attr("data-value");

//        if (!nameValue) nameValue = "";
//        if (!emailValue) emailValue = "";
//        if (!phoneValue) phoneValue = "";
//        if (!addressValue) addressValue = "";

//        $txtName.dxTextBox({ value: nameValue }).dxValidator({
//            validationGroup: validationGroupName,
//            validationRules: [{ type: "required", message: "Name is required" }]
//        });

//        $txtEmail.dxTextBox({ value: emailValue }).dxValidator({
//            validationGroup: validationGroupName,
//            validationRules: [
//                { type: "required", message: "Email is required" },
//                { type: "email", message: "Email is invalid" }
//            ]
//        });

//        let cleanPhone = phoneValue ? phoneValue.replace(/\D/g, '') : '';

//        $txtPhone.dxTextBox({
//            value: cleanPhone
//        }).dxValidator({
//            validationGroup: validationGroupName,
//            validationRules: [
//                { type: "required", message: "Phone is required" },
//                {
//                    type: 'pattern',
//                    pattern: /^\d{10}$/,
//                    message: 'Phone must be exactly 10 digits'
//                }
//            ]
//        });

//        $txtAddress.dxTextBox({ value: addressValue }).dxValidator({
//            validationGroup: validationGroupName,
//            validationRules: [{ type: "required", message: "Address is required" }]
//        });

//        $btnSave.off("click").on("click", function () {
//            const result = DevExpress.validationEngine.validateGroup(validationGroupName);
//            if (!result.isValid) return;

//            const data = {
//                Id: employeeId,
//                Name: $txtName.dxTextBox("instance").option("value"),
//                Email: $txtEmail.dxTextBox("instance").option("value"),
//                Phone: $txtPhone.dxTextBox("instance").option("value"),
//                Address: $txtAddress.dxTextBox("instance").option("value"),
//                ServiceIds: null
//            };

//            $.post("/Employee/Save", data).done(() => {
//                $("#popupEmployee").dxPopup("instance").hide();
//                $("#gridEmployees").dxDataGrid("instance").refresh();
//            }).fail(() => alert("Save failed"));
//        });
//    }, 300);
//}


//$(function () {
//    const grid = $("#gridEmployees");
//    if (!grid.length) return;

//    grid.dxDataGrid({
//        dataSource: new DevExpress.data.CustomStore({
//            key: "id",
//            load: () => $.getJSON("/Employee/GetEmployees")
//        }),
//        showBorders: true,
//        paging: { pageSize: 10 },
//        columns: [
//            { dataField: "name", caption: "Name" },
//            { dataField: "email", caption: "Email" },
//            { dataField: "phone", caption: "Phone" },
//            { dataField: "address", caption: "Address" },
//            {
//                caption: "Action",
//                width: 150,
//                cellTemplate: function (container, options) {
//                    $("<button>")
//                        .addClass("btn btn-primary btn-sm me-1")
//                        .text("Edit")
//                        .appendTo(container)
//                        .on("click", () => openEmployeePopup(options.data.id));

//                    $("<button>")
//                        .addClass("btn btn-danger btn-sm")
//                        .text("Delete")
//                        .appendTo(container)
//                        .on("click", function () {
//                            if (confirm("Delete this employee?")) {
//                                $.post("/Employee/Delete", { id: options.data.id })
//                                    .done(function() {
//                                        grid.dxDataGrid("instance").refresh();
//                                    })
//                                    .fail(function(xhr, status, error) {
//                                        alert("Delete failed: " + error);
//                                    });
//                            }
//                        });
//                }
//            }
//        ]
//    });

//    $("#btnAdd").on("click", () => openEmployeePopup());
//});

//function openEmployeePopup(id) {
//    const popup = $("#popupEmployee");

//    try {
//        const popupInstance = popup.dxPopup("instance");
//        if (popupInstance) {
//            popup.dxPopup("dispose");
//        }
//    } catch (e) {}

//    popup.empty();

//    $.get("/EmployeeModal/AddOrEdit", { id: id || 0 })
//        .done(function (html) {
//            popup.html(html);

//            popup.dxPopup({
//                width: 600,
//                height: "auto",
//                visible: true,
//                showTitle: true,
//                title: "Add / Edit Employee",
//                dragEnabled: true,
//                closeOnOutsideClick: true,
//                onShown: function() {
//                    setTimeout(function() {
//                        initEmployeeForm();
//                    }, 100);
//                },
//                onHiding: function() {
//                    try {
//                        const popupContent = $("#popupEmployee");
//                        popupContent.find("#txtName, #txtEmail, #txtPhone, #txtAddress").each(function() {
//                            const $el = $(this);
//                            try {
//                                if ($el.dxTextBox) {
//                                    const instance = $el.dxTextBox("instance");
//                                    if (instance) $el.dxTextBox("dispose");
//                                }
//                            } catch (e) {}
//                        });
//                    } catch (e) {}
//                }
//            });
//        })
//        .fail(function(xhr, status, error) {
//            alert("Failed to load employee form: " + error);
//        });
//}
const validationGroupName = "EmployeeForm";

function initEmployeeForm() {

    const employeeId = parseInt($("#employeeId").val()) || 0;

    const nameValue = $("#txtName").data("value") || "";
    const emailValue = $("#txtEmail").data("value") || "";
    const phoneValue = $("#txtPhone").data("value") || "";
    const addressValue = $("#txtAddress").data("value") || "";

    $("#Validationsummary").dxValidationSummary({
        validationGroup: validationGroupName
    });

    $("#txtName").dxTextBox({ value: nameValue }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [{ type: "required", message: "Name is required" }]
    });

    $("#txtEmail").dxTextBox({ value: emailValue }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [
            { type: "required", message: "Email is required" },
            { type: "email", message: "Invalid email" }
        ]
    });

    $("#txtPhone").dxTextBox({ value: phoneValue }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [
            { type: "required", message: "Phone is required" },
            {
                type: "pattern",
                pattern: /^\d{10}$/,
                message: "Phone must be 10 digits"
            }
        ]
    });

    $("#txtAddress").dxTextBox({ value: addressValue }).dxValidator({
        validationGroup: validationGroupName,
        validationRules: [{ type: "required", message: "Address is required" }]
    });

    $("#btnSave").off("click").on("click", function () {

        const result = DevExpress.validationEngine.validateGroup(validationGroupName);
        if (!result.isValid) return;

        const data = {
            Id: employeeId,
            Name: $("#txtName").dxTextBox("instance").option("value"),
            Email: $("#txtEmail").dxTextBox("instance").option("value"),
            Phone: $("#txtPhone").dxTextBox("instance").option("value"),
            Address: $("#txtAddress").dxTextBox("instance").option("value"),
            ServiceIds: null // preserve existing services
        };

        $.post("/Employee/Save", data)
            .done(function () {
                $("#popupEmployee").dxPopup("instance").hide();
                $("#gridEmployees").dxDataGrid("instance").refresh();
            })
            .fail(function () {
                alert("Failed to save employee");
            });
    });
}


$(function () {

    if (!$("#gridEmployees").length) return;

    $("#gridEmployees").dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: () => $.getJSON("/Employee/GetEmployees")
        }),
        showBorders: true,
        paging: { pageSize: 10 },
        columns: [
            { dataField: "name", caption: "Name" },
            { dataField: "email", caption: "Email" },
            { dataField: "phone", caption: "Phone" },
            { dataField: "address", caption: "Address" },
            {
                caption: "Action",
                width: 160,
                cellTemplate: function (container, options) {

                    $("<button>")
                        .addClass("btn btn-primary btn-sm me-1")
                        .text("Edit")
                        .appendTo(container)
                        .on("click", function () {
                            openEmployeePopup(options.data.id);
                        });

                    $("<button>")
                        .addClass("btn btn-danger btn-sm")
                        .text("Delete")
                        .appendTo(container)
                        .on("click", function () {
                            if (confirm("Delete this employee?")) {
                                $.post("/Employee/Delete", { id: options.data.id })
                                    .done(function () {
                                        $("#gridEmployees").dxDataGrid("instance").refresh();
                                    })
                                    .fail(function () {
                                        $("#gridEmployees").dxDataGrid("instance").refresh();
                                    });
                            }
                        });
                }
            }
        ]
    });

    $("#btnAdd").on("click", function () {
        openEmployeePopup(0);
    });
});


function openEmployeePopup(id) {

    $.get("/EmployeeModal/AddOrEdit", { id: id })
        .done(function (html) {

            $("#popupEmployee").dxPopup({
                width: 600,
                height: "auto",
                visible: true,
                showTitle: true,
                title: "Add / Edit Employee",
                closeOnOutsideClick: true,
                contentTemplate: function () {
                    return $("<div>").html(html);
                },
                onShown: function () {
                    initEmployeeForm();
                }
            });
        })
        .fail(function () {
            alert("Failed to load employee form");
        });
}
