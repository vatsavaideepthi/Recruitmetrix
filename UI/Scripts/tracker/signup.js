jQuery(document).ready(function () {

    $('#reguser').click(function () {

        $('#usersignupform').removeClass('hide');
        $('#usersignupform').find("input[type=text], textarea,input[type=password]").val("");
        $('#orgsignupform').addClass('hide');
    });

    $('#regorg').click(function () {
        $('#usersignupform').addClass('hide');
        $('#orgsignupform').find("input[type=text], textarea,input[type=password]").val("");
        $('#orgsignupform').removeClass('hide');
    });

    $('#usersignupform').submit(function (e)
    {
        if(ValidateEmail($('#signupemail').val()))
        {
            $('#SGE001').addClass('hide');
        }
        else
        {
            $('#SGE001').removeClass('hide');
            e.preventDefault();
        }


        if ($('#signuppassword').val() == "" || $('#cpassword').val() == "") {
            $('#SGE013').removeClass('hide');
            e.preventDefault();
        }
        else {
            $('#SGE013').addClass('hide');

            if ($('#signuppassword').val() == $('#cpassword').val() )
            {
                $('#SGE012').addClass('hide');
            }
            else
            {
                $('#SGE012').removeClass('hide');
                e.preventDefault();
            }
        }
    });

    $('#orgsignupform').submit(function (e) {
        if (ValidateEmail($('#orgemail').val())) {
            $('#SGE001').addClass('hide');
        }
        else {
            $('#SGE001').removeClass('hide');
            e.preventDefault();
        }

        if ($('#orgpassword').val() == "" || $('#orgcpassword').val() == "") {
            $('#SGE013').removeClass('hide');
            e.preventDefault();
        }
        else {
            $('#SGE013').addClass('hide');

            if ($('#orgpassword').val() == $('#orgcpassword').val()) {
                $('#SGE012').addClass('hide');
            }
            else {
                $('#SGE012').removeClass('hide');
                e.preventDefault();
            }
        }
        

    });

});