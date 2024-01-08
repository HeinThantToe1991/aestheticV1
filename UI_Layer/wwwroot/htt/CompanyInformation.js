$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "/BackendSystem/CompanyInformation/GetData",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            if (response.success) {
                var dataEntity = response.dataEntity;
                $("#Id").val(dataEntity.id);
                $("#txtCompanyName").val(dataEntity.companyName);
                $("#txtCompanyShortName").val(dataEntity.companyShortName);
                $("#txtRefCompanyId").val(dataEntity.refCompanyId);
                $("#txtEmail").val(dataEntity.email);
                $("#txtAddPhoneNumber").val(dataEntity.phoneNumber);
                $("#txtContactPerson").val(dataEntity.contactPerson);
                $("#txtStateDivision").val(dataEntity.stateDivision);
                $("#txtDistrict").val(dataEntity.district);
                $("#txtTownship").val(dataEntity.township);
                $("#txtCompanyAddress").val(dataEntity.companyAddress);
                $("#txtWebsiteLink").val(dataEntity.websiteLink);
                $("#txtFacebookLink").val(dataEntity.facebookLink);
                $("#txtStreetNumber_Name").val(dataEntity.streetNumber_Name);
                $("#txtBusinessType").val(dataEntity.businessType);
                $('#customfile').html(dataEntity.companyLogo_str);
                $('#output').attr('src', '/images/' + dataEntity.companyLogo_str);
                $('#imgfile').val($('#output').val());
                $("#btnUpdate").show();
                $("#btnSave").hide();
            } else {
                if (response.message == null) {
                    $("#btnSave").show();
                    $("#btnUpdate").hide();
                } else {
                    AlertMessage(response.messageStatus, response.message);
                    $("#btnUpdate").hide();
                    $("#btnSave").hide();
                }


            }
        },
        failure: function(r) {
            alert(r.responseText);
        },
        error: function(r) {
            alert(r.responseText);
        }
    });

    $('#quickForm').validate({
        rules: {
            CompanyName: {
                required: true,
                maxlength: 100
            },
            CompanyShortName: {
                required: true,
                maxlength: 18
            },
            RefCompanyId: {
                required: true,
                maxlength: 100
            },
            //CompanyLogo: {
            //    required: true,
            //},
            PhoneNumber: {
                required: true,
                maxlength: 15
            },
            Email: {
                required: true,
                maxlength: 254,
                customEmail: true
            },
            ContactPerson: {
                required: true,
                maxlength: 50
            },
            StateDivision: {
                required: true
            },
            District: {
                required: true
            },
            Township: {
                required: true
            },
            CompanyAddress: {
                required: true,
                maxlength: 254
            },
            FacebookLink: {
                required: false,
                maxlength: 200
            },
            WebsiteLink: {
                required: false,
                maxlength: 200
            }
        },
        messages: {
            CompanyName: {
                required: "Please enter CompanyName",
                maxlength: getValidationMessage(100)
            },
            CompanyShortName: {
                required: "Please enter CompanyShortName",
                maxlength: getValidationMessage(18)
            },
            RefCompanyId: {
                required: "Please enter Company Reference Id",
                maxlength: getValidationMessage(100)
            },
            //CompanyLogo: {
            //    required: MV00077,
            //},
            PhoneNumber: {
                required: MV00082,
                maxlength: getValidationMessage(15)
            },
            Email: {
                required: MV00083,
                maxlength: getValidationMessage(254)
            },
            ContactPerson: {
                required: MV00091,
                maxlength: getValidationMessage(50)
            },
            StateDivision: {
                required: "Please enter Division"
            },
            District: {
                required: "Please enter District"
            },
            Township: {
                required: "Please enter Township"
            },
            CompanyAddress: {
                required: MV00080,
                maxlength: getValidationMessage(254)
            },
            WebsiteLink: {
                //required: "Please enter Facebook Link",
                maxlength: getValidationMessage(200)
            },
            FacebookLink: {
                //required: "Please enter Facebook Link",
                maxlength: getValidationMessage(200)
            }
        },
        errorElement: 'span',
        errorPlacement: function(error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function(element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function(element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});

function Add(form, event) {
    $.validator.unobtrusive.parse(form);
    var formData = new FormData(form);
    var file = document.getElementById("imgfile").files[0];
    formData.append("CompanyLogo", file);
    var clickedButtonId = event.submitter.id;
    if (!$(form).valid()) {
        return;
    }
    $('.submit').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: form.action,
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 800000,
        success: function (data, status, xhr) {       
            AlertMessage(data.messageStatus, data.message);
            $('.submit').removeAttr('disabled');
        }
    });
    return false;
}

var loadFile = function (event) {
    console.log("File input changed:", event.target.files);
    if (CheckSpecialCharacters(event.target.files[0].name)) {
        toastr.clear();
        toastr.error(MV10086);
        $("#imgfile")[0].value = "";
        clearImage("output");
        return false;
    }
    else if (CheckImageFileName(event.target.files[0].name)) {
        toastr.clear();
        toastr.error(MV10087);
        $("#imgfile")[0].value = "";
        clearImage("output");
        return false;
    }
    else if (checkImageFileSize(event.target.files[0])) {
        toastr.clear();
        toastr.error(MV10088 + (max_img_size / 1024) + "KB");
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