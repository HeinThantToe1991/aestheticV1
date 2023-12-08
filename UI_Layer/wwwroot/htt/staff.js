﻿var max_img_size = 102400;
$(document).ready(function () {
    $('#txtFromDate').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#txtToDate').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $("#ddFilter").val('');
    $("#ddFilter1").val('');
    $("#ddFilter").select2({
        placeholder: $("#hide_chooseone").val(),
        allowClear: true
    }).on('change', function (e) {
        if (e.target.value === "") {
            // Code to handle clear event
            $('#txtFromDate input').val('');
            $('#txtToDate input').val('');
        }
    });
    $("#ddFilter1").select2({
        placeholder: $("#hide_chooseone").val(),
        allowClear: true
    }).on('change', function (e) {
        if (e.target.value === "") {
            // Code to handle clear event
            $('#txtFilter1').val('');
        }
    });;
    $("#ddReportType").select2({
        placeholder: $("#ML00209").val(),
        allowClear: true
    });
    $("#Gender").select2({
        placeholder: $("#ML00051").val(),
        dropdownParent: $("#addModal"),
        allowClear: true,
        ajax: {
            minimumResultsForSearch: 20,
            url: "/BackendSystem/Customer/GetGender",
            dataType: 'json',
            type: "GET",
            data: function(params) {
                var query = {
                    search: params.term,
                    type: "",
                }
                return query;
            },
            processResults: function(data, params) {
                return {
                    results: data
                };
            }
        }
    });
    $.validator.addMethod("customDate", function (value, element) {
        var day = $("input[name='Day']").val();
        var month = $("input[name='Month']").val();
        var year = $("input[name='Year']").val();

        // Convert the day, month, and year to integers
        var dayInt = parseInt(day, 10);
        var monthInt = parseInt(month, 10);
        var yearInt = parseInt(year, 10);
        var currentYear = new Date().getFullYear();
        // Check if the year is valid (you can adjust the range as needed)
        if (yearInt < 1900 || yearInt > currentYear) {
            return false;
        }

        // Check if the month is valid (1-12)
        if (monthInt < 1 || monthInt > 12) {
            return false;
        }

        // Check for invalid days based on the month
        if (dayInt < 1 || dayInt > new Date(yearInt, monthInt, 0).getDate()) {
            return false;
        }

        return true;
    }, "Please enter a valid date");
    $('#addForm').validate({
        rules: {
            LoginName: {
                required: true,
                maxlength: 100
            },
            FullName: {
                required: true,
                maxlength: 100
            },
            Gender: {
                required: true,
            },
            Day: {
                required: true,
                maxlength: 2,
                customDate: true
            },
            Month: {
                required: true,
                maxlength: 2,
                customDate: true
            },
            Year: {
                required: true,
                maxlength: 4,
                customDate: true
            },
            Email: {
                required: true,
                maxlength: 254,
                customEmail: true
            },
            PhoneNo: {
                required: true,
                maxlength: 50
            },
            StateDivision: {
                required: true,
            },
            District: {
                required: true,
            },
            Township: {
                required: true,
            },
            Address: {
                required: true,
                maxlength: 254
            },
            Remark: {
                required: false,
                maxlength: 200
            }
        },
        messages: {
            LoginName: {
                required: "Please enter Name",
                maxlength: getValidationMessage(100)
            },
            FullName: {
                required: "Please enter Full Name",
                maxlength: getValidationMessage(25)
            },
            Gender: {
                required: "Please choose Gender"
            },
            Day: {
                required: "Please enter birth Date",
                maxlength: getValidationMessage(2),
                customDate: "Please enter valid date"
            },
            Month: {
                required: "Please enter birth Month",
                maxlength: getValidationMessage(2),
                customDate: "Please enter valid month"
            },
            Year: {
                required: "Please enter birth Year",
                maxlength: getValidationMessage(2),
                customDate: "Please enter valid year"
            },
            PhoneNumber: {
                required: MV00082,
                maxlength: getValidationMessage(15),
            },
            Email: {
                required: MV00083,
                maxlength: getValidationMessage(254),
            },
            StateDivision: {
                required: "Please enter Division",
            },
            District: {
                required: "Please enter District",
            },
            Township: {
                required: "Please enter Township",
            },
            CompanyAddress: {
                required: MV00080,
                maxlength: getValidationMessage(254),
            },
            WebsiteLink: {
                //required: "Please enter Facebook Link",
                maxlength: getValidationMessage(200),
            },
            FacebookLink: {
                //required: "Please enter Facebook Link",
                maxlength: getValidationMessage(200),
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    $('#ReportForm').validate({
        rules: {
            txtFromDate: {
                CheckRequiredFilter: true,
                isGreaterThanToDate: true
            },
            txtToDate: {
                CheckRequiredFilter: true,
                isGreaterThanToday: true
            },
            txtFilter1: {
                CheckRequiredFilter1: true
            }
        },
        ignore: ':hidden:not([class~=selectized]),:hidden > .selectized, .selectize-control .selectize-input input',
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});

$("input[data-bootstrap-switch]").each(function () {
    //$(this).bootstrapSwitch('state', $(this).prop('checked'));
    $(this).bootstrapSwitch('state', false);
    // Add the "checked" and "disabled" attributes
   
});
$("input[data-bootstrap-switch]").on('switchChange.bootstrapSwitch', function (event, state) {
    var $radioEqual = $("input.custom-control-input#rdoEqual");
    var $radioContain = $("input.custom-control-input#rdoContain");
    if (state) {
        $radioEqual.prop('checked', true);
        $radioEqual.prop('disabled', true);
        $radioContain.prop('disabled', true);
    } else {
        $radioEqual.prop('checked', true);
        $radioEqual.prop('disabled', false);
        $radioContain.prop('disabled', false);
    } 
});
var loadFile = function (event) {
    console.log("File input changed:", event.target.files);
    if (CheckSpecialCharacters(event.target.files[0].name)) {
        toastr.clear();
        toastr.error(MV00086);
        $("#imgfile")[0].value = "";
        clearImage("output");
        return false;
    }
    else if (CheckImageFileName(event.target.files[0].name)) {
        toastr.clear();
        toastr.error(MV00087);
        $("#imgfile")[0].value = "";
        clearImage("output");
        return false;
    }
    else if (checkImageFileSize(event.target.files[0])) {
        toastr.clear();
        toastr.error(MV00088 + (max_img_size / 1024) + "KB");
        $("#imgfile")[0].value = "";
        clearImage("output");
        return false;
    } else {
        var image = document.getElementById('output');
        image.src = URL.createObjectURL(event.target.files[0]);
    }
    var filePathDiv = document.getElementById('customfile');
    filePathDiv.innerText = event.target.files[0].name;
};
function Add() {
    var form = $('#addForm')[0];
    $('.submit').attr('disabled', 'disabled');
    var formData = new FormData(form);
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            url: form.action,
            data: formData,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 800000,
            success: function (data, status, xhr) {
                if (data.success) {
                    clear();
                }
                AlertMessage(data.messageStatus, data.message);
            }
        });
    }
    $('.submit').removeAttr('disabled');
    return false;
};
function clear() {
    $('#LoginName').val('');
    $('#FullName').val('');
    $('#Day').val('');
    $('#Month').val('');
    $('#Year').val('');
    $('#PhoneNo').val('');
    $('#DivisionId').val('');
    $('#DistrictId').val('');
    $('#TownshipId').val('');
    $('#Email').val('');
    $('#Address').val('');
    $('#Remark').val('');
    $("#Gender").val('').trigger('change');
}
function cancelClick() {
    $("#ddFilter").val('').trigger('change'); 
    $("#ddFilter1").val('').trigger('change');
    $('#txtFromDate input').val('');
    $('#txtToDate input').val('');
    $('#txtFilter1').val('');
}
function StaffInformation(form) {
    var formData = new FormData(form);
    formData.append("FilterKey", $("#ddFilter").val());
    formData.append("FilterFromValue", $("#txtFromDate input").val());
    formData.append("FilterToValue", $("#txtToDate input").val());
    formData.append("Filter1Key", $("#ddFilter1").val());
    formData.append("Filter1Value1", $("#txtFilter1").val());
    if ($(form).valid()) {
        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: form.action,
            data: formData,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 800000,
            async: false,
            success: function (data, status, xhr) {
                if (data.dataList.length > 0) {
                    // Create a new form element
                    var newForm = document.createElement('form');
                    newForm.method = 'POST';
                    newForm.action = '/BackendSystem/Staff/StaffInfoFilterReport';
                    newForm.target = '_blank';

                    // Create a hidden input field for the viewModel data
                    var input = document.createElement('input');
                    input.type = 'hidden';
                    input.name = 'viewModel';
                    input.value = JSON.stringify(data.dataList);
                    newForm.appendChild(input);

                    // Append the form to the body and submit it
                    document.body.appendChild(newForm);
                    newForm.submit();
                    document.body.removeChild(newForm);
                } else {
                    // Display an alert if there is no data for the report
                    AlertMessage(data.messageStatus, data.message);
                }

            }
        });
    }
    return false;
}