jQuery(document).ready(function () {



    $('#UserLoginForm').submit(function (e) {

       
        if ($('#email').val() == "") {
            $('#LVE002').removeClass('hide');
            e.preventDefault();
        }
        else 
        {
            if (ValidateEmail($('#email').val())) {
                $('#LVE001').addClass('hide');
            }
            else {
                $('#LVE001').removeClass('hide');
                e.preventDefault();
            }

            $('#LVE002').addClass('hide');
            if ($('#password').val() == "") {
                $('#LVE003').removeClass('hide');
                e.preventDefault();
            }
            else
            {
                $('#LVE003').addClass('hide');
            }
        }

    });
});