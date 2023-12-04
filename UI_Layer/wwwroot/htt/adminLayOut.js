function AlertMessage(_status, message) {
    toastr.clear();
    switch (_status) {
        case 0:
            toastr.info(message);
            break;
        case 1:
            toastr.error(message);
            break;
        case 2:
            toastr.success(message);
            break;
        case 3:
            toastr.warning(message);
            break;
    }
}
function getValidationMessage(maxlength) {
    validationMessage = '@language.Getkey("MV00084")';
    validationMessage = validationMessage.replace("*$*", maxlength);
    return validationMessage;
}