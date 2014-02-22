jQuery(document).ready(function () {

    var templateid = GetDefaultTemplate();

    $('.dataeditor').on('click', '.custvieweditaddress', function () {
        $('.address').find('*[contenteditable]').attr('contenteditable', 'true');
        $(this).text('Update');
        $(this).removeClass('custvieweditaddress').addClass('custviewupdateaddress');
    });

    $('.dataeditor').on('click', '.custvieweditcontact', function () {
        $('.contact').find('*[contenteditable]').attr('contenteditable', 'true');
        $(this).text('Update');
        $(this).removeClass('custvieweditcontact').addClass('custviewupdatecontact');
    });

    $('.dataeditor').on('click', '.custviewupdateaddress', function () {

        var address = {};
        address.id = $('.address').attr('data-addressid');
        address.line1 = $('.address .line1').text();
        address.line2 = $('.address .line2').text();
        address.city = $('.address .city').text();
        address.state = $('.address .state').text();
        address.zip = $('.address .zip').text();
        address.country = $('.address .country').text();

        var companyid = $('.address').attr('data-companyid');
        var customerid = $('.address').attr('data-customerid');

        UpdateAddress(companyid, customerid, address);

        $('.address').find('*[contenteditable]').attr('contenteditable', 'false');

        $(this).text('Edit');
        $(this).removeClass('custviewupdateaddress').addClass('custvieweditaddress');

    });

    $('.dataeditor').on('click', '.custviewupdatecontact ', function () {

       
        var contact = {};
        contact.id = $('.contact').attr('data-contactid');

        contact.title = $('.contact .title').text();
        contact.FirstName = $('.contact .fname').text();
        contact.LastName = $('.contact .lname').text();
        contact.email = $('.contact .email').text();
        contact.mobile = $('.contact .mobile').text();


        var companyid1 = $('.contact').attr('data-companyid');
        var customerid1 = $('.contact').attr('data-customerid');

        UpdateContact(companyid1, customerid1, contact);
        $('.contact').find('*[contenteditable]').attr('contenteditable', 'false');
        $(this).text('Edit');
        $(this).removeClass('custviewupdatecontact').addClass('custvieweditcontact');

    });


});


