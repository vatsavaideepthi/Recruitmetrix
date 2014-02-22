jQuery(document).ready(function () {

    $('.alertclose').click(function ()
    {
        $(this).closest('.alert').addClass('hide');
    });

  
    $('#addcontactproxy').click(function () {

        var validcode = '11';
        var testcode = '';

        //Job title Validation Starts
        if ($('#contactfirstname').val().trim() == "") {
            $('#contactfirstname').addClass('alertred');
            testcode += '0';
        }
        else {
            //$('#jobtitleempty.alert').addClass('hide');
            $('#contactfirstname').removeClass('alertred');
            testcode += '1';
        }

        if ($('#contactemailid').val().trim() == "") {
            $('#contactemailid').addClass('alertred');

            testcode += '0';
        }
        else {
            if (ValidateEmail($('#contactemailid').val())) {
                $('#contactemailid').removeClass('alertred');
                //alert('success valid Email');
                testcode += '1';
            }
            else {
                $('#contactemailid').addClass('alertred');
                //alert('In Valid Email');
                testcode += '0';
            }

        }

        if (testcode == validcode) {

            $('#addcontactproxy').hide();
            $('#addcontacta').removeClass('hide');
            $('#addcontactform').submit();  
      
        }
        else {
          //  alert('User Attention Required');
        }
    });

    $('#contactfirstname').on('input', function () {

        if ($('#contactfirstname').val().length > 0) {
            $('#contactfirstname').removeClass('alertred');
        }
    });

    $('#contactemailid').on('input propertychange', function () {
        if ($('#contactemailid').val().trim() == "") {
            $('#contactemailid').removeClass('alertred');
        }
        else {
            $('#contactemailid').addClass('alertred');
            if (ValidateEmail($('#contactemailid').val())) {
                $('#contactemailid').removeClass('alertred');
            }
            else {
                $('#contactemailid').addClass('alertred');
                //alert('In Valid Email');
            }

        }
    });

});