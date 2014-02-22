jQuery(document).ready(function () {


    $('.addlineitem').on('click', function () {
        var clonetr = $(this).parents('table').find('#clonelineitem').clone();
        $(clonetr).removeClass('hide').addClass('lineitem');
        $(clonetr).removeAttr('id');
        $(this).parents('tr').before(clonetr);
    });

    $('#updateinvoiceform').on('click', '.deleterow', function () {
        var clonetr = $(this).parents('tr').remove();
        evaluateItemValues();
        evaluatetax();
    });
    $('#updateinvoiceform').on('keyup', '.rowquantity,.rowunitprice', function () {
        var quantity = parseInt($(this).parents('tr').find('.rowquantity').val());
        var unitprice = parseInt($(this).parents('tr').find('.rowunitprice').val());
        $(this).parents('tr').find('.rowtotal').val(isNaN(quantity * unitprice) ? 0 : quantity * unitprice);
        var subtotalamount = parseInt(0, 10);
        $('#updateinvoiceform').find('.rowtotal').each(function () {
            if (isNaN(subtotalamount)) {
                subtotalamount = 0;
                subtotalamount = isNaN(parseInt($(this).val(), 10) + parseInt(subtotalamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);
               
                $('#updateinvoiceform').find('.discount').val(subtotalamount);
            }
            else {
                subtotalamount = parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);
               
                $('#updateinvoiceform').find('.discount').val(subtotalamount);
            }
        });

        var subtotalamount = parseInt($('#updateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#updateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);

    });

    $('#updateinvoiceform').on('keyup', '.rowtax', function () {
        var totaltaxamount = isNaN(parseInt($('#updateinvoiceform').find('.shipping').val(), 10)) ? 0 : parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
        $('#updateinvoiceform').find('.rowtax').each(function () {
            if (isNaN(totaltaxamount)) {
                totaltaxamount = 0;
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#updateinvoiceform').find('.shipping').val(totaltaxamount);
            }
            else {
                totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
                $('#updateinvoiceform').find('.shipping').val(totaltaxamount);
            }
        });

        var subtotalamount = parseInt($('#updateinvoiceform').find('.discount').val(), 10);
        var totaltaxamount = parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
        var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
        $('#updateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);

    });

    $('.edit-inv').click(function () {
        $('.addlineitem').show();
        $('#invdispholder').find('*[contenteditable]').attr('contenteditable', 'true');
        $('#invdispholder').find("input[type='text']").removeAttr('disabled');
        $('.sendinv,.downinv').addClass('hide');
        $('.cancel-edit,.update-inv').removeClass('hide');
        $('.deleterow').removeClass('deleterowforcehide');
        $(this).hide();
    });
    $('.update-inv,.cancel-edit').click(function () {
        $('.addlineitem').hide();
        $('#invdispholder').find('*[contenteditable]').attr('contenteditable', 'false');
        $('#invdispholder').find("input[type='text']").prop('disabled', 'true');
        $('.sendinv,.downinv').removeClass('hide');
        $('.cancel-edit,.update-inv').addClass('hide');
        $('.edit-inv').show();
        $('.deleterow').addClass('deleterowforcehide');
    });

});

function evaluateItemValues()
{
    var quantity = parseInt($(this).parents('tr').find('.rowquantity').val());
    var unitprice = parseInt($(this).parents('tr').find('.rowunitprice').val());
    $(this).parents('tr').find('.rowtotal').val(isNaN(quantity * unitprice) ? 0 : quantity * unitprice);
    var subtotalamount = parseInt(0, 10);
    $('#updateinvoiceform').find('.rowtotal').each(function () {
        if (isNaN(subtotalamount)) {
            subtotalamount = 0;
            subtotalamount = isNaN(parseInt($(this).val(), 10) + parseInt(subtotalamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

            $('#updateinvoiceform').find('.discount').val(subtotalamount);
        }
        else {
            subtotalamount = parseInt($(this).val(), 10) + parseInt(subtotalamount, 10);

            $('#updateinvoiceform').find('.discount').val(subtotalamount);
        }
    });

    var subtotalamount = parseInt($('#updateinvoiceform').find('.discount').val(), 10);
    var totaltaxamount = parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
    var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
    $('#updateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);

}


function evaluatetax()
{
    var totaltaxamount = isNaN(parseInt($('#updateinvoiceform').find('.shipping').val(), 10)) ? 0 : parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
    $('#updateinvoiceform').find('.rowtax').each(function () {
        if (isNaN(totaltaxamount)) {
            totaltaxamount = 0;
            totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
            $('#updateinvoiceform').find('.shipping').val(totaltaxamount);
        }
        else {
            totaltaxamount = isNaN(parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10)) ? 0 : parseInt($(this).val(), 10) + parseInt(totaltaxamount, 10);
            $('#updateinvoiceform').find('.shipping').val(totaltaxamount);
        }
    });

    var subtotalamount = parseInt($('#updateinvoiceform').find('.discount').val(), 10);
    var totaltaxamount = parseInt($('#updateinvoiceform').find('.shipping').val(), 10);
    var totalamount = parseInt((isNaN(subtotalamount) ? 0 : subtotalamount) + (isNaN(totaltaxamount) ? 0 : totaltaxamount));
    $('#updateinvoiceform').find('.totalamount').val(isNaN(totalamount) ? 0 : totalamount);
}

function client(info, address) {
    this.info = info;
    this.address = address;
}

function info(name, email, compname) {
    this.name = name;
    this.email = email;
    this.compname = compname;
}

function address(line1, line2, city, cityid, state, stateid, country, countryid, pincode) {
    this.line1 = line1;
    this.line2 = line2;
    this.city = city;
    this.cityid = cityid;
    this.state = state;
    this.stateid = stateid;
    this.country = country;
    this.countryid = countryid
    this.pincode = pincode;
}

function invoiceline(quantity, description, price, total,tax) {
    this.quantity = quantity;
    this.description = description;
    this.price = price;
    this.total = total;
    this.tax = tax;
}

function invoicemeta(id,invdesc, templateid, duedate, dueamount, discount, shipping, totalamount, paidamount, balance) {
    this.id = id;
    this.desc = invdesc;
    this.templateid = templateid;
    this.duedate = duedate;
    this.amountdue = dueamount;
    this.discount = discount;
    this.shipping = shipping;
    this.totalamount = totalamount;
    this.paidamount = paidamount;
    this.balance = balance;
}

function invoice(clientobj, selfobj, invoicemeta, lineitemsobj) {
    this.client = clientobj;
    this.self = selfobj;
    this.metadata = invoicemeta;
    this.items = lineitemsobj;
}

function lineitems(invoicelines) {
    this.item = invoicelines
}
(function ($) {
    $.QueryString = (function (a) {
        if (a == "") return {};
        var b = {};
        for (var i = 0; i < a.length; ++i) {
            var p = a[i].split('=');
            if (p.length != 2) continue;
            b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
        }
        return b;
    })(window.location.search.substr(1).split('&'))
})(jQuery);

jQuery(document).ready(function () {

    $('#invdispholder').on('click', '.addlineitem', function () {
        var clonetr = $(this).parents('table').find('#clonelineitem').clone();
        $(clonetr).removeClass('hide').addClass('lineitem');
        $(clonetr).removeAttr('id');
        $(this).parents('tr').before(clonetr);
    });



    var templateid = $.QueryString["tid"];
    var invoiceid = $.QueryString["invid"];
    var ownid = '<%= Session["userid"] %>';
    try{
        if ($('#isinvoiceview').val() == '1') {
            if ($('#viewinvoice').is(':empty')) {
            }
            else {

                $.ajax({
                    url: "/Invoice/GetInvoiceTemplate",
                    type: "GET",
                    dataType: "HTML",
                    data: { tempid: templateid },
                    async: false,
                    success: function (data) {
                        $('#currenttemplate').val(templateid);
                        $('#invdispholder').addClass('hide');
                        $('#invdispholder').html(data);
                    },
                    error: function (data) {
                    }
                });


                var dat;
                $.ajax({
                    url: "/Invoice/GetInvoice",
                    type: "GET",
                    dataType: "XML",
                    data: { invid: invoiceid },
                    async: false,
                    success: function (data) {
                        if (data == null) {
                            $('#invdispholder').removeClass('hide');
                        }
                        else
                        {
                            $('#invoiceid').val(invoiceid);
                            populatedata(data);
                        }
                       

                    },
                    error: function (data) {
                    }
                });

            }
        }
    }
    catch (e)
    {

    }
    function populatedata(data) {
        var client = $(data).find('client');
        var self = $(data).find('self');
        var metadata = $(data).find('metadata');
        var items = $(data).find('items');
        $('.deleterow').addClass('deleterowforcehide');
        $('.client .info .name').text($(client).find('info').children('name')[0].textContent);
        $('.client .info .email').text($(client).find('info').children('email')[0].textContent);
        $('.client .info .compname').text($(client).find('info').children('compname')[0].textContent);

        $('.client .address .line1').text($(client).find('address').children('line1')[0].textContent);
        $('.client .address .line2').text($(client).find('address').children('line2')[0].textContent);
        $('.client .address .city').text($(client).find('address').children('city')[0].textContent);
        $('.client .address .city').attr('data-id');
        $('.client .address .state').text($(client).find('address').children('state')[0].textContent);
        $('.client .address .state').attr('data-id');
        $('.client .address .country').text($(client).find('address').children('country')[0].textContent);
        $('.client .address .country').attr('data-id');
        $('.client .address .pincode').text($(client).find('address').children('pincode')[0].textContent);

        $('.self .info .name').text($(self).find('info').children('name')[0].textContent);
        $('.self .info .email').text($(self).find('info').children('email')[0].textContent);
        $('.self .info .compname').text($(self).find('info').children('compname')[0].textContent);

        $('.self .address .line1').text($(self).find('address').children('line1')[0].textContent);
        $('.self .address .line2').text($(self).find('address').children('line2')[0].textContent);
        $('.self .address .city').text($(self).find('address').children('city')[0].textContent);
        $('.self .address .city').attr('data-id');
        $('.self .address .state').text($(self).find('address').children('state')[0].textContent);
        $('.self .address .state').attr('data-id');
        $('.self .address .country').text($(self).find('address').children('country')[0].textContent);
        $('.self .address .country').attr('data-id');
        $('.self .address .pincode').text($(self).find('address').children('pincode')[0].textContent);

        var itemcollection = $(items).find('item');
        for (var j = 1; j < itemcollection.length; j++) {
            var clonetr = $('#clonelineitem').clone();
            $(clonetr).removeClass('hide').addClass('lineitem');
            $(clonetr).removeAttr('id');
            $(clonetr).attr('data-id', j);
            $('.invoice-box .lineitem').last().after(clonetr);
        }
        var itemarr;
        $('#invdispholder').on('click', '.lineitem', function () {
            itemarr = $('#invdispholder').find('.lineitem');
        });
        $('.lineitem').trigger('click');
        var i = 0;
        jQuery.each(itemarr, function () {
            try {
                $(this).find('.rowquantity').val($(items).find('item').find('quantity')[i].textContent);
                $(this).find('.rowdescription').val($(items).find('item').find('description')[i].textContent);
                $(this).find('.rowunitprice').val($(items).find('item').find('price')[i].textContent);
                $(this).find('.rowtotal').val($(items).find('item').find('total')[i].textContent);
                try{
                    $(this).find('.rowtax').val($(items).find('item').find('tax')[i].textContent);
                }
                catch(e){$(this).find('.rowtax').val(0);}
            }
            catch (e) {
            }
            i++;
        });

        $('.invoice-meta .invnumber').text($(metadata).find('id')[0].textContent);

        $('#templateid').val($(metadata).find('templateid')[0].textContent);
        $('#currenttemplate').val($(metadata).find('templateid')[0].textContent);
       
        $('.invoice-meta .duedate').text($(metadata).find('duedate')[0].textContent);
        $('.invoice-meta .invdesc').text($(metadata).find('desc')[0].textContent);
        try{
            $('.invoice-box .shipping ').val($(metadata).find('shipping')[0].textContent);
        }
        catch (e) { $('.invoice-box .shipping ').val(0); }
        try{
            $('.invoice-box .discount ').val($(metadata).find('discount')[0].textContent);
        }
        catch (e) { $('.invoice-box .discount ').val(0); }
        $('.invoice-box .totalamount ').val($(metadata).find('totalamount')[0].textContent);

        $('.addlineitem').hide();
        $('#invdispholder').find('*[contenteditable]').attr('contenteditable', 'false');
        $('#invdispholder').find("input[type='text']").prop('disabled', 'true').css('cursor', 'default');
        $('#invdispholder').removeClass('hide');


    }



    $('.update-inv').click( function (e) {
       

        var clientinfo = new info($(' .client .info .name').text(),
                                $(' .client .info .email').text(),
                                $(' .client .info .compname').text());

        var clientaddress = new address($(' .client .address .line1').text(),
                                $(' .client .address .line2').text(),
                                $(' .client .address .city').text(),
                                $(' .client .address .city').attr('data-id'),
                                $(' .client .address .state').text(),
                                $(' .client .address .state').attr('data-id'),
                                $(' .client .address .country').text(),
                                $(' .client .address .country').attr('data-id'),
                                $(' .client .address .compname').text());

        var clientobj = new client(clientinfo, clientaddress);


        var selfinfo = new info($(' .self .info .name').text(),
                                $(' .self .info .email').text(),
                                $(' .self .info .compname').text());

        var selfaddress = new address($(' .self .address .line1').text(),
                                $(' .self .address .line2').text(),
                                $(' .self .address .city').text(),
                                $(' .self .address .city').attr('data-id'),
                                $(' .self .address .state').text(),
                                $(' .self .address .state').attr('data-id'),
                                $(' .self .address .country').text(),
                                $(' .self .address .country').attr('data-id'),
                                $(' .self .address .compname').text());

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

        var invmeta = new invoicemeta($(' .self .invoice-meta .invnumber').text(),
                                            $('.self .invoice-meta .invdesc').text(),
                                            $('#currenttemplate').val(),
                                        $(' .self .invoice-meta .duedate').text(), '', $(' .invoice-box .discount ').val(), $(' .invoice-box .shipping ').val(),
                                        $(' .invoice-box .totalamount ').val(),
                                        '', '');

        var invoiceobj = new invoice(clientobj, selfobj, invmeta, lineitemsobj);


        $.ajax({
            url: "/Invoice/UpdateInvoice",
            type: "POST",
            dataType: "JSON",
            data: {
                invoiceid: $('#invoiceid').val(),
                invoiceobj: JSON.stringify(invoiceobj)
            },
            async: false,
            success: function (data) { },
            error: function (data) {
            }
        });

    });



    $('.prepinvemail').click(function () {

        $('#emailsenderslist').val($('.client .info .email').text());
        $('#invemailmodal').modal('show');
    });
});