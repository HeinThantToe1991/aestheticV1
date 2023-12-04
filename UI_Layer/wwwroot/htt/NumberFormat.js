

var _qtyDecimalPlace = 0;
var priceDecimalPlace = 2;
var amountDecimalPlace = 2;
var catlogamountDecimalPlace = 0;
var rateDecimalPlace = 2;

new AutoNumeric.multiple('.quantityFormat', {
    minimumValue: 0,
    maximumValue: '9999999999',
    decimalPlaces: _qtyDecimalPlace,
    //allowDecimalPadding: "floats",
    decimalCharacter: ".",
    //digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true //!!!
});
new AutoNumeric.multiple('.priceFormat', {
    minimumValue: 0,
    maximumValue: '9999999999.99',
    decimalPlaces: priceDecimalPlace,
    //allowDecimalPadding: "floats",
    decimalCharacter: ".",
    //digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true //!!!
});
new AutoNumeric.multiple('.amountFormat', {
    minimumValue: 0,
    maximumValue: '9999999999.99',
    decimalPlaces: amountDecimalPlace,
    //allowDecimalPadding: "floats",
    decimalCharacter: ".",
    //digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true, //!!!
    noformatOnSubmit: true

});
new AutoNumeric.multiple('.rateFormat', {
    minimumValue: 0,
    maximumValue: '9999999999.99',
    decimalPlaces: rateDecimalPlace,
    //allowDecimalPadding: "floats",
    //decimalCharacter: ".",
    //digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true //!!!
});

//const RoundDecimal = (num, decimals) => num.toLocaleString('en-US', {
//    minimumFractionDigits: 2,
//    maximumFractionDigits: 2,
//});

new AutoNumeric.multiple('.sellingpriceFormat', {
    minimumValue: 0,
    maximumValue: '9999999999',
    decimalPlaces: 0,
    //allowDecimalPadding: "floats",
    decimalCharacter: ".",
    digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true, //!!!
    noformatOnSubmit: true

});

new AutoNumeric.multiple('.CatalogamountFormat', {
    minimumValue: 0,
    maximumValue: '9999999999',
    decimalPlaces: catlogamountDecimalPlace,
    //allowDecimalPadding: "floats",
    decimalCharacter: ".",
    //digitGroupSeparator: ",",
    //emptyInputBehavior: "zero",
    watchExternalChanges: true, //!!!
    noformatOnSubmit: true

});
function QuantityTextBox_Keydown(e,textboxId) {
    var keynum;
    var quantity = $('#'+textboxId).val();
    if (window.event) { // IE                  
        keynum = e.keyCode;
    } else if (e.which) { // Netscape/Firefox/Opera                 
        keynum = e.which;
    }
    var unicode = e.keyCode ? e.keyCode : e.charCode;
    if (unicode != 8 && unicode != 46 && unicode != 37 && unicode != 39) {
        if (isNaN(parseFloat(e.key))) {
            return false;
        }
    }
  
    if (unicode == 8 || unicode == 46) {
        if (quantity.length == 1) {
            return false;
        }   
    }
    return true;
}
function QuantityTextBox_Keyup(e, textboxId) {
    var keynum;
    var quantity = $('#' + textboxId).val();
    if (window.event) { // IE                  
        keynum = e.keyCode;
    } else if (e.which) { // Netscape/Firefox/Opera                 
        keynum = e.which;
    }
    var unicode = e.keyCode ? e.keyCode : e.charCode;
    var quantity = $('#' + textboxId).val();
    if (quantity.length == 1 && quantity == 0) {
        quantity = 1;
    }
    return quantity;
}
