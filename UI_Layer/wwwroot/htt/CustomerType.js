
var addForm, editForm;
$(document).ready(function () {
    BindListingTable();
    JqueryValidate();
    bsCustomFileInput.init();
})
function BindListingTable() {
    $.ajax({
        type: "GET",
        url: "/BackendSystem/CustomerType/GetData",
        dataType: "json",
        success: function (response) {
            debugger;
            $("#listing").DataTable().destroy();
            $("#listing").DataTable({
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": false,
                "responsive": true,
                "buttons": [
                    { extend: "copy", exportOptions: { columns: ':visible' } },
                    { extend: "csv", exportOptions: { columns: ':visible' }, charset: 'utf-8', bom: true },
                    { extend: "excel", exportOptions: { columns: ':visible' } },
                    {
                        extend: "pdf", exportOptions: { columns: ':visible' },
                        customize: function (doc) {    
                            processDoc(doc);
                            pdfFunction(doc, "listing");                                  
                            pdfFunctionWithCompanyLogo(doc, "listing");
                        }
                    },
                    {
                        extend: "print", exportOptions: { columns: ':visible' },
                        header: true,
                        messageTop: '<h4><p align="right">Print Date : ' + formatDate(new Date()) + '</p></h4>',
                    },
                    { extend: "colvis", text: 'Show/Hide', exportOptions: { columns: ':visible' } }
                ],
                data: response.dataList,
                columns: [
                    { 'data': 'code' },
                    { 'data': 'description' },
                    {
                        data: null,
                        'sorting': false,
                        render: function (data, type, row) {
                            return "<a id='btnEdit' value='" + MB00005 + "' class='btn  btn-outline-warning btn-sm' data-id='" + data.id + "'  onclick=EditPopup('" + data.id + "')><i class='fas fa-pencil-alt'></i><span>" + MB00005 + "</span></a> <a  id='btnDelete' value='" + MB00004 + "' class='btn btn-outline-danger btn-sm'  onclick=DeletePopUp('" + data.id + "')><i class='fas fa-trash'></i><span>" + MB00004 + "</span></a>";
                        }
                    }
                ]
            }).buttons().container().appendTo('#listing_wrapper .col-md-6:eq(0)');
        },
        failure: function (r) {
            alert(r.responseText);
        },
        error: function (r) {
            alert(r.responseText);
        }
    });
}
function JqueryValidate() {
    addForm = $('#quickForm').validate({
        rules: {
            Code: {
                required: true,
                checkSpace: true,
                maxlength: 5
            },
            Description: {
                required: true,
                checkSpace: true,
                maxlength: 50
            }
        },
        messages: {
            Code: {
                required: MV00018,
                maxlength: MV00073
            },
            Description: {
                required: MV00017,
                maxlength: MV00074
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
    editForm = $('#editForm').validate({
        rules: {
            Code: {
                required: true,
                checkSpace: true,
                maxlength: 5
            },
            Description: {
                required: true,
                checkSpace: true,
                maxlength: 100
            }
        },
        messages: {
            Code: {
                required: MV00018,
                maxlength: MV00073
            },
            Description: {
                required: MV00017,
                maxlength: MV00074
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
}

//#region Create
function fxAddPopup() {
    clearValidationMessage(addForm);
    $('#addModal').modal("show");
}

function Add(form) {

    $.validator.unobtrusive.parse(form);
    var formData = new FormData(form);
    if (!$(form).valid()) {
        return;
    }
    $('.submit').attr('disabled', 'disabled');
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
                $('#addModal').modal('hide');
                BindListingTable();
                clearAddForm();
                debugger;
            }
            AlertMessage(data.messageStatus, data.message);
            $('.submit').removeAttr('disabled');
        }
    });

    return false;
}

function clearAddForm() {
    $('#Code').val('');
    $('#Description').val('');
}
//#endregion

//#region Update
function EditPopup(id) {
    clearValidationMessage(editForm);
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: "/BackendSystem/CustomerType/Edit?id=" + id,
        contentType: "application/json;charset=UTF-8",
        success: function (result) {
            $('#EditCode').val(result.code);
            $("#EditID").val(result.id);
            $('#EditID').hide();
            $('#EditDescription').val(result.description);
            $('#updateModal').modal("show");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Update(form) {

    $.validator.unobtrusive.parse(form);
    var formData = new FormData(form);
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
            if (data.success) {
                $('#updateModal').modal('hide');
                //$('#listing').DataTable().ajax.url(ReloadLink);
                //$('#listing').DataTable().ajax.reload();
                BindListingTable();
                $('#EditCode').val('');
                $('#EditDescription').val('');
                clearEditForm();
            }
            AlertMessage(data.messageStatus, data.message);
            $('.submit').removeAttr('disabled');
        }
    });
    return false;
}

function clearEditForm() {
    $('#EditCode').val('');
    $('#EditDescription').val('');
}
//#endregion

//#region Delete
function DeletePopUp(id) {
    $("#txtDeleteCodeId").val(id);
    $('#DeleteModal').modal("show");
}

function Delete(form) {
    $('.submit').attr('disabled', 'disabled');
    $.validator.unobtrusive.parse(form);
    var formData = new FormData(form);
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
                $('#DeleteModal').modal('hide');
                //$('#listing').DataTable().ajax.url(ReloadLink);
                //$('#listing').DataTable().ajax.reload();

            }
            AlertMessage(data.messageStatus, data.message);
            $('.submit').removeAttr('disabled');
        }
    });
    return false;
}
    //#endregion