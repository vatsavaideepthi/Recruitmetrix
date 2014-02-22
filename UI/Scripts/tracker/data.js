// This File contains all data communication methods related and 
// was organized into the module specific operations.
// Detailed Information is available at each function header..
// Payment Tracker Beta::Simplify your Expenses


//#region Templates

function GetInvoiceTemplate(templateid) {
    var templatedata;
    $.ajax({
        url: "/Invoice/GetInvoiceTemplate",
        type: "GET",
        dataType: "HTML",
        data: { tempid: templateid },
        async: false,
        success: function (templdata) {
            if (templdata != null) {
                templatedata = templdata;

            }
        },
        error: function (data) {
        }
    });

    return templatedata;
}


function GetDefaultTemplate() {
    var templateid;
    $.ajax({
        url: "/AjaxData/GetDefaultTemplate",
        type: "POST",
        dataType: "text",
        async: false,
        success: function (data) {
            templateid = data;
        },
        error: function (data) {

        }
    });
    return templateid;
}
//#endregion

//#region Address

function IsCustomerHasAddress(compid, custid) {
    var status;

    $.ajax({
        url: "/AjaxData/IsCustomerHasAddress",
        type: "POST",
        dataType: "text",
        data: { companyid: compid, customerid: custid },
        async: false,
        success: function (data) {
            status = data;
        },
        error: function (data) {

        }
    });

    return status;
}

function GetCustomerAdress(custid) {
    var address;
    $.ajax({
        url: "/AjaxData/GetAddress",
        type: "POST",
        dataType: "JSON",
        data: { objectid: custid },
        async: false,
        success: function (data) {
            address = data;
        },
        error: function (data) {
        }
    });

    return address;
}

function UpdateAddress(companyid, custid, address) {
    var status;
    $.ajax({
        url: "/AjaxData/UpdateAddress",
        type: "POST",
        dataType: "text",
        data: {
            addressid: $.trim(address.id),
            companyid: $.trim(companyid),
            objectid: $.trim(custid),
            line1: $.trim(address.line1),
            line2: $.trim(address.line2),
            city: $.trim(address.city),
            state: $.trim(address.state),
            zip: $.trim(address.zip),
            country: $.trim(address.country)
        },
        async: false,
        success: function (data) {
            if (data != null || data == "")
                status = data;
        },
        error: function (data) {


        }
    });
    return status;
}

function DeleteAddress(companyid, custid) {
    var status;
    $.ajax({
        url: "/AjaxData/DeleteAddress",
        type: "POST",
        dataType: "text",
        data: {
            companyid: companyid,
            objectid: custid
        },
        async: false,
        success: function (data) {
            if (data != null || data == "")
                status = data;
        },
        error: function (data) {


        }
    });
    return status;
}

//#endregion

//#region Contact

function AddContact(objid, email, mob) {
    var contactid;
    $.ajax({
        url: "/AjaxData/AddContact",
        type: "POST",
        dataType: "text",
        data: { objectid: objid, email: email, mobile: mob },
        async: false,
        success: function (data) {
            if (data != null || data == "")
                contactid = data;
        },
        error: function (data) {

            e.preventDefault();
        }
    });
    return contactid;
}

function IsCustomerHasContact(compid, custid) {
    var status;

    $.ajax({
        url: "/AjaxData/IsCustomerHasContact",
        type: "POST",
        dataType: "text",
        data: { companyid: compid, customerid: custid },
        async: false,
        success: function (data) {
            status = data;
        },
        error: function (data) {

        }
    });

    return status;
}

function GetCustomerContact(custid) {
    var contact;
    $.ajax({
        url: "/AjaxData/GetContact",
        type: "POST",
        dataType: "JSON",
        data: { objectid: custid },
        async: false,
        success: function (data) {
            contact = data;
        },
        error: function (data) {

        }
    });

    return contact;
}

function UpdateContact(companyid, custid, contact) {
    var status;
    $.ajax({
        url: "/AjaxData/UpdateContact",
        type: "POST",
        dataType: "text",
        data: {
            contactid: $.trim(contact.id),
            companyid: $.trim(companyid),
            objectid: $.trim(custid),
            title: $.trim(contact.title),
            fname: $.trim(contact.FirstName),
            lname: $.trim(contact.LastName),
            email: $.trim(contact.email),
            mobile: $.trim(contact.mobile)
        },
        async: false,
        success: function (data) {
            if (data != null || data == "")
                status = data;
        },
        error: function (data) {


        }
    });
    return status;
}

function DeleteContact(companyid, custid) {
    var status;
    $.ajax({
        url: "/AjaxData/DeleteContact",
        type: "POST",
        dataType: "text",
        data: {
            companyid: companyid,
            objectid: custid
        },
        async: false,
        success: function (data) {
            if (data != null || data == "")
                status = data;
        },
        error: function (data) {


        }
    });
    return status;
}

//#endregion

//#region Invoice

function CreateInvoice(invoiceObject)
    {
 $.ajax({
    url: "/AjaxData/CreateInvoice",
    type: "POST",
    dataType: "text",
    data: {
        invoiceobj: JSON.stringify(invoiceObject)
    },
    success: function (data) {
        var res = data;
        return res;
    },
    error: function (data) {
        return null;
    }
});
}

function AddExternalInvoice(invoiceObject) {
    $.ajax({
        url: "/invoice/CreateInvoice",
        type: "POST",
        dataType: "text",
        data: {
            invoiceobj: JSON.stringify(invoiceObject)
        },
        async: true,
        success: function (data) {
            var res = data;
            return res;
        },
        error: function (data) {
            return null;
        }
    });
}


function UpdateInvoice(invoiceid,invoiceobject)
{
    $.ajax({
        url: "/AjaxData/UpdateInvoice",
        type: "POST",
        dataType: "JSON",
        data: {
            invoiceid: invoiceid,
            invoiceobj: JSON.stringify(invoiceobject)
        },
        async: false,
        success: function (data) { },
        error: function (data) {
        }
    });
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

function invoiceline(quantity, description, price, total, tax) {
    this.quantity = quantity;
    this.description = description;
    this.price = price;
    this.total = total;
    this.tax = tax;
}

function invoicemeta(id, invdesc, templateid, duedate, dueamount, discount, shipping, totalamount, paidamount, balance) {
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

//#endregion

//#region Customers

function GetCustomers()
{
  
    $.ajax({
        url: "/AjaxData/GetAllCustomers",
        type: "GET",
        dataType: "JSON",
        data: {
            tag: ''
        },
        success: function (data) {
            alert(data);
        },
        error: function (data) {
        }
    })
    
}


function GetCustomerInfo(companyid,customerid) {

    var customerinfo;
    $.ajax({
        url: "/AjaxData/GetCustomerInfo",
        type: "POST",
        dataType: "JSON",
        data: {
            companyid: companyid,
            customerid: customerid
        },
        async: false,
        success: function (data) {
            customerinfo = data;
        },
        error: function (data) {
        }
    })
    return customerinfo;
}

//#endregion 

//#region Transactions

function CreateTransaction() {
    $.ajax({
        url: "/AjaxData/CreateTransaction",
        type: "POST",
        dataType: "JSON",
        data: {
            tid: $(this).parent().parent().find('.accname').val(),
            uid: usrid,
            toaccid: toaccid,
            paymode: "",
            subcatid: $(this).parent().parent().attr('data-subcatid'),
            catid: $(this).parent().parent().attr('data-catid'),
            desc: $(this).parent().parent().find('.desc').val(),
            paidamount: $(this).parent().parent().find('.dueamount').val(),
            budget: $(this).parent().parent().find('.budget').attr('data-budgetamount'),
            duemonth: $(this).parent().parent().find('.paydue').attr('data-month'),
            duedate: $(this).parent().parent().find('.paydue').attr('data-date'),
            paiddate: $(this).parent().parent().find('.duemonth').val(),
            paidmonth: $(this).parent().parent().find('.duemonth').val(),
            paidyear: $(this).parent().parent().find('.duemonth').val(),
            recurrence: $(this).parent().parent().find('.recurrence option:selected').val()

        },
        async: true,
        success: function (data) { },
        error: function (data) {
        }
    });

}




//Update Transaction
function UpdateTransaction(tid, uid, toaccid, paymode, subcatid, catid, desc, paidamount, budget, duemonth, duedate, paiddate, paidmonth, paidyear, recurrence) {
    $.ajax({
        url: "/AjaxData/updateTransaction",
        type: "POST",
        dataType: "JSON",
        data: {
            tid: tid,
            uid: uid,
            toaccid: toaccid,
            paymode: paymode,
            subcatid: subcatid,
            catid: catid,
            desc: desc,
            paidamount:paidamount,
            budget: budget,
            duemonth: duemonth,
            duedate: duedate,
            paiddate: paiddate,
            paidmonth: paidmonth,
            paidyear: paidyear,
            recurrence: recurrence

        },
        async: true,
        success: function (data) { },
        error: function (data) {
        }
    });

}


//#endregion

//#region Accounts

//Update Account
function UpdateAccount() {
    $.ajax({
        url: "/AjaxData/UpdateAccount",
        type: "POST",
        dataType: "JSON",
        data: {
            accountid: accid,
            accountname: $(this).parent().parent().find('.accname').val(),
            userid: usrid,
            accounttype: "",
            subcategoryid: $(this).parent().parent().attr('data-subcatid'),
            categoryid: $(this).parent().parent().attr('data-catid'),
            categoryname: $(this).parent().parent().attr('data-catname'),
            description: $(this).parent().parent().find('.desc').val(),
            providerid: $(this).parent().parent().find('.prname').attr('data-prid'),
            providername: $(this).parent().parent().find('.prname').val(),
            amount: $(this).parent().parent().find('.budget').val(),
            duemonth: $(this).parent().parent().find('.duemonth').val(),
            duedate: $(this).parent().parent().find('.duedate').val(),
            recurrence: $(this).parent().parent().find('.recurrence option:selected').val(),
        },
        async: true,
        success: function (data) { },
        error: function (data) {
        }
    });
}

//Delete Account
function DeleteAccount() {
    $.ajax({
        url: "/AjaxData/DeleteAccount",
        type: "POST",
        dataType: "JSON",
        data: {
            accountid: accid,
            userid: usrid
        },
        async: true,
        success: function (data) { },
        error: function (data) {
        }
    });
}

//#endregion

//#region TimeSheets




//#endregion

//#region Persons

function GetEmployeeInfo(employeeid, companyid,objtype)
{
    var employeeinfo;
    $.ajax({
        url: "/AjaxData/GetEmployeeInfo",
        type: "POST",
        dataType: "JSON",
        data: {
            employeeid: employeeid,
            companyid: companyid,
            objectype:objtype
        },
        async: false,
        success: function (data) {
            employeeinfo = data;
        },
        error: function (data) {
        }
    })
    return employeeinfo;
}


//#endregion 