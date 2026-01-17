//$(function () {
//    const grid = $("#gridEmployees");
//    if (!grid.length) return;

//    grid.dxDataGrid({
//        dataSource: new DevExpress.data.CustomStore({
//            key: "id",
//            load: function() {
//                return $.getJSON("/Service/GetEmployeesWithServices");
//            }
//        }),
//        showBorders: true,
//        paging: { pageSize: 10 },
//        columns: [
//            { dataField: "name", caption: "Name" },

//            { dataField: "serviceName", caption: "Service" },
//            {
//                caption: "Action",
//                width: 150,
//                cellTemplate: function (container, options) {
//                    $("<button>")
//                        .addClass("btn btn-primary btn-sm")
//                        .text("Edit Service")
//                        .appendTo(container)
//                        .on("click", function() {
//                            openServicePopup(options.data.id, options.data.serviceName);
//                        });
//                }
//            }
//        ]
//    });
//});

//function openServicePopup(employeeId, currentServiceName) {
//    // Load services and current employee service
//    $.when(
//        $.getJSON("/Service/GetServices"),
//        $.getJSON("/Employee/GetEmployeeServiceIds", { employeeId })
//    ).done(function(servicesResponse, serviceIdsResponse) {
//        const services = servicesResponse[0];
//        const selectedServiceIds = serviceIdsResponse[0];

//        const popupContent = $("<div>").css({ padding: "20px" });

//        // Create label for services
//        const serviceLabel = $("<label>")
//            .addClass("form-label mb-2")
//            .text("Services");
//        popupContent.append(serviceLabel);

//        // Create TagBox for services
//        const serviceTagBox = $("<div>")
//            .attr("id", "serviceTagBoxPopup")
//            .css({ marginBottom: "20px" });
//        popupContent.append(serviceTagBox);

//        const saveButton = $("<button>")
//            .addClass("btn btn-success mt-3")
//            .text("Save")
//            .css({ width: "50%" });
//        popupContent.append(saveButton);

//        $("#popupService").dxPopup({
//            width: 600,
//            height: "auto",
//            visible: true,
//            showTitle: true,
//            title: "Edit Service",
//            contentTemplate: function() {
//                return popupContent;
//            },
//            onShown: function() {
//                // Create TagBox with services
//                createServiceTagBox(services, selectedServiceIds || []);

//                // Save button click handler
//                saveButton.off("click").on("click", function() {
//                    const tagBoxInstance = $("#serviceTagBoxPopup").dxTagBox("instance");
//                    const newSelectedServiceIds = tagBoxInstance.option("value") || [];

//                    // Delete all existing service assignments for this employee
//                    const deletePromises = [];
//                    if (selectedServiceIds && selectedServiceIds.length > 0) {
//                        selectedServiceIds.forEach(function(serviceId) {
//                            if (newSelectedServiceIds.indexOf(serviceId) === -1) {
//                                deletePromises.push($.post("/Service/DeleteEmployeeService", { 
//                                    employeeId: employeeId, 
//                                    serviceId: serviceId 
//                                }));
//                            }
//                        });
//                    }


//                    const savePromises = [];
//                    if (newSelectedServiceIds && newSelectedServiceIds.length > 0) {
//                        newSelectedServiceIds.forEach(function(serviceId) {
//                            if (!selectedServiceIds || selectedServiceIds.indexOf(serviceId) === -1) {
//                                savePromises.push($.post("/Service/SaveEmployeeService", { 
//                                    employeeId: employeeId, 
//                                    serviceId: serviceId 
//                                }));
//                            }
//                        });
//                    }

//                    const allPromises = deletePromises.concat(savePromises);
//                    if (allPromises.length > 0) {
//                        $.when.apply($, allPromises).done(function() {
//                            $("#popupService").dxPopup("instance").hide();
//                            $("#gridEmployees").dxDataGrid("instance").refresh();
//                        });
//                    } else {
//                        $("#popupService").dxPopup("instance").hide();
//                        $("#gridEmployees").dxDataGrid("instance").refresh();
//                    }
//                });
//            }
//        });
//    });
//}

//function createServiceSelectBox(data, values)
//$("#serviceplaceholderPopup").dxSelectBox({
//    items: simpleProducts,
//    inputAttr: { 'aria-label': 'Product With Placeholder' },
//    placeholder: 'Select services.....',
//    showClearButton: true,
//});

//function createServiceTagBox(data, values) {
//    $("#serviceTagBoxPopup").dxTagBox({
//        dataSource: data,
//        valueExpr: "id",
//        displayExpr: "name",
//        value: values,
//        showSelectionControls: true,
//        searchEnabled: true,
//        width: "100%",
//        placeholder: "Select services..."
//    });
//}



$(function () {
    const grid = $("#gridEmployees");
    if (!grid.length) return;

    grid.dxDataGrid({
        dataSource: new DevExpress.data.CustomStore({
            key: "id",
            load: function () {
                return $.getJSON("/Service/GetEmployeesWithServices");
            }
        }),
        showBorders: true,
        paging: { pageSize: 10 },
        columns: [
            { dataField: "name", caption: "Name" },
            { dataField: "serviceName", caption: "Service" },
            {
                caption: "Action",
                width: 150,
                cellTemplate: function (container, options) {
                    $("<button>")
                        .addClass("btn btn-primary btn-sm")
                        .text("Edit Service")
                        .appendTo(container)
                        .on("click", function () {
                            openServicePopup(options.data.id);
                        });
                }
            }
        ]
    });
});

function openServicePopup(employeeId) {
    $.when(
        $.getJSON("/Service/GetServices"),
        $.getJSON("/Employee/GetEmployeeServiceIds", { employeeId: employeeId })
    ).done(function (servicesResponse, serviceIdsResponse) {
        const services = servicesResponse[0];
        const selectedServiceIds = serviceIdsResponse[0];
        const currentServiceId = selectedServiceIds && selectedServiceIds.length > 0 ? selectedServiceIds[0] : null;

        const popupContent = $("<div>").css({ padding: "20px" });

        const serviceLabel = $("<label>")
            .addClass("form-label mb-2")
            .text("Service");
        popupContent.append(serviceLabel);

        const serviceSelectBox = $("<div>")
            .attr("id", "serviceSelectBoxPopup")
            .css({ marginBottom: "20px" });
        popupContent.append(serviceSelectBox);

        const saveButton = $("<button>")
            .addClass("btn btn-success mt-3")
            .text("Save")
            .css({ width: "100%" });
        popupContent.append(saveButton);

        $("#popupService").dxPopup({
            width: 500,
            height: "auto",
            visible: true,
            showTitle: true,
            title: "Edit Service",
            contentTemplate: function () {
                return popupContent;
            },
            onShown: function () {
                createServiceSelectBox(services, currentServiceId);

                saveButton.off("click").on("click", function () {
                    saveService(employeeId, currentServiceId);
                });
            }
        });
    });
}

function createServiceSelectBox(data, value) {
    $("#serviceSelectBoxPopup").dxSelectBox({
        dataSource: data,
        valueExpr: "id",
        displayExpr: "name",
        value: value,
        placeholder: "Choose Service",
        showClearButton: true,
        searchEnabled: true,
        width: "100%"
    });
}

function saveService(employeeId, oldServiceId) {
    const selectBox = $("#serviceSelectBoxPopup").dxSelectBox("instance");
    const newServiceId = selectBox.option("value");

    if (oldServiceId) {
        $.post("/Service/DeleteEmployeeService", {
            employeeId: employeeId,
            serviceId: oldServiceId
        }).done(function () {
            if (newServiceId) {
                $.post("/Service/SaveEmployeeService", {
                    employeeId: employeeId,
                    serviceId: newServiceId
                }).done(function () {
                    $("#popupService").dxPopup("instance").hide();
                    $("#gridEmployees").dxDataGrid("instance").refresh();
                });
            } else {
                $("#popupService").dxPopup("instance").hide();
                $("#gridEmployees").dxDataGrid("instance").refresh();
            }
        });
    } else {
        if (newServiceId) {
            $.post("/Service/SaveEmployeeService", {
                employeeId: employeeId,
                serviceId: newServiceId
            }).done(function () {
                $("#popupService").dxPopup("instance").hide();
                $("#gridEmployees").dxDataGrid("instance").refresh();
            });
        } else {
            $("#popupService").dxPopup("instance").hide();
            $("#gridEmployees").dxDataGrid("instance").refresh();
        }
    }
}

