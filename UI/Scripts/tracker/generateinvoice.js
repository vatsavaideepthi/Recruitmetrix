

jQuery(document).ready(function () {

    var d = new Date();
    $('.invoice-meta .duedate').text(GetMonthName(d.getMonth()) + ' ' + d.getDate() + ',' + d.getFullYear());

    function UpdateClientAddress(useraddress) {

        $('.client .address .line1').text(useraddress.line1);
        $('.client .address .line2').text(useraddress.line2);
        $('.client .address .city').text(useraddress.city);
        $('.client .address .city').attr('data-id');
        $('.client .address .state').text(useraddress.state);
        $('.client .address .state').attr('data-id');
        $('.client .address .country').text(useraddress.country);
        $('.client .address .country').attr('data-id');
        $('.client .address .pincode').text(useraddress.pincode);
    }

    function UpdateClientContact(usercontact) {
        $('.client .info .name').text(usercontact.customername);
        $('.client .info .cattnname').text(usercontact.FirstName);
        $('.client .info .email').text('(' + usercontact.email + ')');
        $('.client .info .compname').text(usercontact.compname);
    }

    function UpdateCustomerInfo(userinfo) {
        $('.client .info .name').text(userinfo.name);
        $('.client .info .compname').text(userinfo.name);
    }


    $('#actionsetchangecustomer').click(function () {
        $('#choosecustomermodal').modal('show');
    });

    if ($('#viewid').val() == "invoice-create") {
        if ($('#currentcustomer').val() == "") {

            $('#choosecustomermodal').modal('show');
        }
        else {
            var customerid = $('#currentcustomer').val();
            var companyid = $('#currentcompanyid').val();
            var customrinfoobject = GetCustomerInfo(companyid, customerid);
            UpdateCustomerInfo(customrinfoobject);
            if (IsCustomerHasAddress(companyid, customerid).toLowerCase() == 'true') {
                var custaddress = GetCustomerAdress(customerid);
                UpdateClientAddress(custaddress);

            }
            else {
                $('#addressmodal').modal('show');
            }

            if (IsCustomerHasContact(companyid, customerid).toLowerCase() == 'true') {
                var contact = GetCustomerContact(customerid);
                UpdateClientContact(contact);
            }
            else {
                $('#contactmodal').modal('show');
            }

            $('#generateinvoiceform #templateid').val($('#currenttemplate').val());
        }
    }

    $('#choosecustomermodal').on('hidden', function () {

        $('.ui-autocomplete').hide();
        $('#tags').val('');
    });

    $('body').on("click", '#crncust,#mancrsutomer', function () {
        $('#choosecustomermodal').modal('hide');
        $('#createCustomermodal').modal('show');
    });

    $('#chscustomerselected').click(function () {
        var customerid = $('#currentcustomer').val();
        var companyid = $('#currentcompanyid').val();

        if (IsCustomerHasAddress(companyid, customerid).toLowerCase() == 'true') {
            var customerinfo = GetCustomerInfo(companyid, customerid);
            UpdateCustomerInfo(customerinfo);
            var address = GetCustomerAdress(customerid);
            UpdateClientAddress(address);

        }
        else {
            $('#addressmodal').modal('show');
        }

        if (IsCustomerHasContact(companyid, customerid).toLowerCase() == 'true') {
            var contact = GetCustomerContact(customerid);
            UpdateClientContact(contact);
        }
        else {
            $('#contactmodal').modal('show');
        }

        $('#chscustomerselected').addClass('hide');
        $('#choosecustomermodal').modal('hide');
    })

    $('#chssupplierselected').click(function () {
        var customerid = $('#currentcustomer').val();
        var companyid = $('#currentcompanyid').val();
        $('#choosecustomermodal').modal('hide');
        $('#addinvoicemodal').modal('show');
    })

    
    $('#next1').click(function (e) {
        e.preventDefault();
        var customername = $('#newcustname').val();
        var customeremail = $('#newcustemail').val()
        $.ajax({
            url: "/AjaxData/create",
            type: "POST",
            dataType: "text",
            data: { name: customername, compname: customeremail },
            async: false,
            success: function (data) {
                $('#crncustomeraddress #customerid').val(data);
                $('#crncustomeraddress #customeremail').val(customeremail);
                $('#crncustomer').hide();
                $('#modaltitle').text('Add Address for ' + customername);

                $('#crncustomeraddress').show();
            },
            error: function (data) {

                e.preventDefault();
            }
        });
    });

    $('#next2').click(function (e) {
        e.preventDefault();
        var objid = $('#crncustomeraddress #customerid').val();
        var ln1 = $('#addline1').val();
        var ln2 = $('#addline2').val();
        var pin = $('#addzipcode').val();
        $.ajax({
            url: "/AjaxData/AddAddress",
            type: "POST",
            dataType: "text",
            data: { objectid: objid, line1: ln1, line2: ln2, pincode: pin },
            async: false,
            success: function (data) {
                $('#crncustomeraddress').hide();
                $('#modaltitle').text('Add Contact for ' + $('#crncustomeraddress #customeremail').val());
                $('#crncustomercontact').show();
                $('#addemail').val($('#crncustomeraddress #customeremail').val());
            },
            error: function (data) {

                e.preventDefault();
            }
        });
    });

    $('#next3').click(function (e) {
        e.preventDefault();
        var objid = $('#crncustomeraddress #customerid').val();
        var ctitle = $('#addtitle').val();
        var cfname = $('#addfname').val();
        var clname = $('#addlname').val();
        var email = $('#addemail').val();
        var mob = $('#addmob').val();
        $.ajax({
            url: "/AjaxData/AddContact",
            type: "POST",
            dataType: "text",
            data: { objectid: objid, email: email, mobile: mob },
            async: false,
            success: function (data) {

                $('#crncustomercontact').hide();
                $('#crncustomerfinish').show();
            },
            error: function (data) {

                e.preventDefault();
            }
        });
    });

    $('#next4').click(function (e) {

        $('#createCustomermodal').modal('hide');
        //if ($('#applyselectedcustomer').is(':checked'))
        //{
        //    var customerid = $('#currentcustomer').val();
        //    var companyid = $('#currentcompanyid').val();
        //    if (IsCustomerHasAddress(companyid, customerid).toLowerCase() == 'true') {
        //        var address = GetCustomerAdress(customerid);
        //        UpdateClientAddress(address);

        //    }
        //    else {
        //        $('#addressmodal').modal('show');
        //    }

        //    if (IsCustomerHasContact(companyid, customerid).toLowerCase() == 'true') {
        //        var contact = GetCustomerContact(customerid);
        //        UpdateClientContact(contact);
        //    }
        //    else {
        //        $('#contactmodal').modal('show');
        //    }
        //    $('#generateinvoiceform #templateid').val($('#currenttemplate').val());
        //}
    });


    $('#generateinvoiceform').on('click', '.addlineitem', function () {
        var clonetr = $(this).parents('table').find('#clonelineitem').clone();
        $(clonetr).removeClass('hide').addClass('lineitem');
        $(clonetr).removeAttr('id');
        $(clonetr).find('.rowunitprice,.rowquantity,.rowtax,.rowtotal').val('0');
        $(this).parents('tr').before(clonetr);
        $('.rowtotal,.discount,.shipping,.totalamount,.rowunitprice,.rowquantity,.rowtax').numeric();

    });
    $('#generateinvoiceform').on('click', '.deleterow', function () {
        var clonetr = $(this).parents('tr').remove();
        evaluateItemValues();
        evaluatetax();
    });

    $('#generateinvoiceform').on('keyup', '.rowquantity,.rowunitprice', function () {
        var quantity = parseInt($(this).parents('tr').find('.rowquantity').val());
        var unitprice = parseInt($(this).parents('tr').find('.rowunitprice').val());
        $(this).parents('tr').find('.rowtotal').val(isNaN(quantity * unitprice) ? 0 : quantity * unitprice);
        var subtotalamount = parseInt(0, 10);
        $('#generateinvoiceform').find('.rowtotal').each(function () {
            if (isNaN(subtotalamount)) {
                subtotalamount = 0;
                subtotalamount = isNaN(parseInt($(this).val(), 10) + parseInt(subtotalamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

                $('#generateinvoiceform').find('.discount').val(subtotalamount);
            }
            else {
                subtotalamount = parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

                $('#generateinvoiceform').find('.discount').val(subtotalamount);
            }
        });

        var subtotalamount = parseInt($('#generateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#generateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);

    });

    $('#generateinvoiceform').on('keyup', '.rowtax', function () {
        var totaltaxamount = isNaN(parseInt($('#generateinvoiceform').find('.shipping').val(), 10)) ? 0 : parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        $('#generateinvoiceform').find('.rowtax').each(function () {
            if (isNaN(totaltaxamount)) {
                totaltaxamount = 0;
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#generateinvoiceform').find('.shipping').val(totaltaxamount);
            }
            else {
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#generateinvoiceform').find('.shipping').val(totaltaxamount);
            }
        });

        var subtotalamount = parseInt($('#generateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#generateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);

    });

    function evaluateItemValues() {
        var quantity = parseInt($(this).parents('tr').find('.rowquantity').val());
        var unitprice = parseInt($(this).parents('tr').find('.rowunitprice').val());
        $(this).parents('tr').find('.rowtotal').val(isNaN(quantity * unitprice) ? 0 : quantity * unitprice);
        var subtotalamount = parseInt(0, 10);
        $('#generateinvoiceform').find('.rowtotal').each(function () {
            if (isNaN(subtotalamount)) {
                subtotalamount = 0;
                subtotalamount = isNaN(parseInt($(this).val(), 10) + parseInt(subtotalamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

                $('#generateinvoiceform').find('.discount').val(subtotalamount);
            }
            else {
                subtotalamount = parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

                $('#generateinvoiceform').find('.discount').val(subtotalamount);
            }
        });

        var subtotalamount = parseInt($('#generateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#generateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);
    }

    function evaluatetax() {
        var totaltaxamount = isNaN(parseInt($('#generateinvoiceform').find('.shipping').val(), 10)) ? 0 : parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        $('#generateinvoiceform').find('.rowtax').each(function () {
            if (isNaN(totaltaxamount)) {
                totaltaxamount = 0;
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#generateinvoiceform').find('.shipping').val(totaltaxamount);
            }
            else {
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#generateinvoiceform').find('.shipping').val(totaltaxamount);
            }
        });

        var subtotalamount = parseInt($('#generateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#generateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#generateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);
    }

    $('.save-inv').click(function (e) {
        //GenerateInvoice


        var clientinfo = new info($('#generateinvoiceform .client .info .name').text(),
                                $('#generateinvoiceform .client .info .email').text().replace('(', '').replace(')', ''),
                                $('#generateinvoiceform .client .info .compname').text());

        var clientaddress = new address($('#generateinvoiceform .client .address .line1').text(),
                                $('#generateinvoiceform .client .address .line2').text(),
                                $('#generateinvoiceform .client .address .city').text(),
                                $('#generateinvoiceform .client .address .city').attr('data-id'),
                                $('#generateinvoiceform .client .address .state').text(),
                                $('#generateinvoiceform .client .address .state').attr('data-id'),
                                $('#generateinvoiceform .client .address .country').text(),
                                $('#generateinvoiceform .client .address .country').attr('data-id'),
                                $('#generateinvoiceform .client .address .compname').text());

        var clientobj = new client(clientinfo, clientaddress);


        var selfinfo = new info($('#generateinvoiceform .self .info .name').text(),
                                $('#generateinvoiceform .self .info .email').text(),
                                $('#generateinvoiceform .self .info .compname').text());

        var selfaddress = new address($('#generateinvoiceform .self .address .line1').text(),
                                $('#generateinvoiceform .self .address .line2').text(),
                                $('#generateinvoiceform .self .address .city').text(),
                                $('#generateinvoiceform .self .address .city').attr('data-id'),
                                $('#generateinvoiceform .self .address .state').text(),
                                $('#generateinvoiceform .self .address .state').attr('data-id'),
                                $('#generateinvoiceform .self .address .country').text(),
                                $('#generateinvoiceform .self .address .country').attr('data-id'),
                                $('#generateinvoiceform .self .address .compname').text());

        var selfobj = new client(clientinfo, clientaddress);

        var lineitemscollection = [];

        var itemarr = $('.lineitem');
        jQuery.each(itemarr, function () {
            var item = new invoiceline($(this).find('.rowquantity').val(),
                                   $(this).find('.rowdescription').val(),
                                    $(this).find('.rowunitprice').val(),
                                    $(this).find('.rowtotal').val(),
                                    $(this).find('.rowtax').val());
            lineitemscollection.push(item);
        });

        var lineitemsobj = new lineitems(lineitemscollection);

        var invmeta = new invoicemeta($('#generateinvoiceform .self .invoice-meta .invnumber').text(),
                                        $('#generateinvoiceform .self .invoice-meta .invdesc').text(),
                                        $('#currenttemplate').val(),
                                        $('#generateinvoiceform .self .invoice-meta .duedate').text(), '', $(' .invoice-box .discount ').val(), $(' .invoice-box .shipping ').val(),
                                        $('#generateinvoiceform .invoice-box .totalamount ').val(),
                                        '', '');
        var invoiceobj = new invoice(clientobj, selfobj, invmeta, lineitemsobj);

        var result = CreateInvoice(invoiceobj);

        if (result != null && result.length > 0) {
            $('#createinvoiceresult .success').removeClass('hide');
            $('#createinvoiceresult .success').find('a').attr('href', '/invoice/view?invid=' + res + '&tid=' + $('#currenttemplate').val());
            $('#generateinvoiceform').hide();
            $('.save-inv,.edit-inv').addClass('hide');
        }
        else {
            $('#createinvoiceresult .failure').removeClass('hide');
        }

    });


    $('#addinboundinvoiceform').on('click','#btinvoicedone', function (e)
    {
        e.preventDefault();

        var clientinfo = new info("","","");

        var clientaddress = new address("","","","","","","","","");

        var clientobj = new client(clientinfo, clientaddress);


        var selfinfo = new info("", "", "");

        var selfaddress = new address("", "", "", "", "", "", "", "", "");

        var selfobj = new client(clientinfo, clientaddress);

        var lineitemscollection = [];

        var itemarr = [];
        jQuery.each(itemarr, function () {
            var item = new invoiceline("", "", "", "", "");
            lineitemscollection.push(item);
        });

        var lineitemsobj = new lineitems(lineitemscollection);

        var invmeta = new invoicemeta("","","","", '', "","","",'', '');
        var invoiceobj = new invoice(clientobj, selfobj, invmeta, lineitemsobj);

        var result;
        $.ajax({
            url: "/AjaxData/AddExternalInvoice",
            type: "POST",
            dataType: "text",
            data: {
                invoiceobj: JSON.stringify(invoiceobj)
            },
            success: function (data) {
               result = data;
               if (result != null && result.length > 0) {
                   $('#genextinvoice').val(result);
                   $('#addinboundinvoiceform').submit();
               }
               else {
                   $('#createinvoiceresult .failure').removeClass('hide');
               }
            },
            error: function (data) {
                return null;
            }
        });
       

    });

    if ($('#viewid').val() == "invoice-inbound") {
        if ($('#currentcustomer').val() == "") {

            $('#choosecustomermodal').modal('show');
        }
        else {
            var customerid = $('#currentcustomer').val();
            var companyid = $('#currentcompanyid').val();
            var customrinfoobject = GetCustomerInfo(companyid, customerid);
            UpdateCustomerInfo(customrinfoobject);
            if (IsCustomerHasAddress(companyid, customerid).toLowerCase() == 'true') {
                var custaddress = GetCustomerAdress(customerid);
                UpdateClientAddress(custaddress);

            }
            else {
                $('#addressmodal').modal('show');
            }

            if (IsCustomerHasContact(companyid, customerid).toLowerCase() == 'true') {
                var contact = GetCustomerContact(customerid);
                UpdateClientContact(contact);
            }
            else {
                $('#contactmodal').modal('show');
            }

            $('#generateinvoiceform #templateid').val($('#currenttemplate').val());
        }
    }


});

