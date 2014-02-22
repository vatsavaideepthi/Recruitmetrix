//Golbal Validation Functions


function ValidateEmail(email) {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(email);
}

function ValidateUSZip(sZip) {
    return /^\d{5}(-\d{4})?$/.test(sZip);
}

function ValidateOnlyNumbers(sNumber) {
    var pattern = new RegExp(/^[0-9]+$/);
    return pattern.test(sNumber);
}

//Function verifies the input date is in MM/DD/YYYY or MM-DD-YYYY format
function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[1];
    dtDay = dtArray[3];
    dtYear = dtArray[5];

    //Checks for dd/mm/yyyy format.
    //    dtDay = dtArray[1];
    //    dtMonth = dtArray[3];
    //    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}


//Function to get month name

function GetMonthName(monthNumber)
{
    var monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"];

    return monthNames[monthNumber];
}

(function ($) {

    $.fn.numeric = function (options) {

        return this.each(function () {
            var $this = $(this);

            $this.keypress(options, function (e) {
                // allow backspace and delete 
                if (e.which == 8 || e.which == 0)
                    return true;

                //if the letter is not digit 
                if (e.which < 48 || e.which > 57)
                    return false;

                // check max range 
                var dest = e.which - 48;
                var result = this.value + dest.toString();
                if (result > e.data.max) {
                    return false;
                }
            });
        });
    };
})(jQuery);

(function ($) {

    $.fn.decimal = function (options) {

        return this.each(function () {
            var $this = $(this);

            $this.keypress(options, function (e) {
                // allow backspace and delete 
                if (e.which == 8 || e.which == 0)
                    return true;

                //if the letter is not digit 
                if (e.which != 46) {
                    if (e.which < 48 || e.which > 57)
                        return false;
                }
                else
                {
                    return true;
                }
               

                // check max range 
                var dest = e.which - 48;
                var result = this.value + dest.toString();
                //if (result > e.data.max) {
                //    return false;
                //}
            });
        });
    };
})(jQuery);