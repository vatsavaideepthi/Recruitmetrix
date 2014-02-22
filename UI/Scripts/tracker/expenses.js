

//Create Transaction
jQuery(document).ready(function () {

    $('#monthlyaccountsetup').click(function () {
        $('#monthaccsetuptable').find('.accountrecord').each(function () {
            var accid = $(this).attr('data-accountid');
            var usrid = $(this).attr('data-userid');
            var budgetamnt = $(this).find('.budget').val();
            if (parseInt(budgetamnt) > 0) {
                $.ajax({
                    url: "/AjaxData/UpdateAccount",
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        accountid: accid,
                        accountname: $(this).find('.accname').text(),
                        userid: usrid,
                        accounttype: $(this).attr('data-accounttype'),
                        subcategoryid: $(this).attr('data-subcatid'),
                        categoryid: $(this).attr('data-catid'),
                        categoryname: $(this).attr('data-catname'),
                        description: "",
                        providerid: $(this).find('.prname').attr('data-prid'),
                        providername: $(this).find('.prname').val(),
                        amount: $(this).find('.budget').val(),
                        duemonth: '0',
                        duedate: $(this).find('.duedate').val(),
                        recurrence: 'month',
                    },
                    async: false,
                    success: function (data) { },
                    error: function (data) {
                    }
                });
            }
        });
    });


    

    $('#monthaccsetuptable').on('click','.jsSetupAccount',function () {

        var topelement = $(this).parents('tr')
        $.ajax({
            url: "/AjaxData/CreateNewAccount",
            type: "POST",
            dataType: "text",
            data: {
                accountname: $(topelement).find('.accname').text(),
                userid: $(topelement).attr('data-userid'),
                accounttype: $(topelement).attr('data-accounttype'),
                subcategoryid: $(topelement).attr('data-subcatid'),
                categoryid: $(topelement).attr('data-catid'),
                categoryname: $(topelement).attr('data-catname'),
                providerid: $(topelement).find('.prname').attr('data-prid'),
                providername: $(topelement).find('.prname').val(),
                amount: $(topelement).find('.budget').val(),
                duedate: $(topelement).find('.duedate').val()
            },
            async: false,
            success: function (data) {
                $(topelement).attr('data-accountid', data);
                //$(topelement).find('td').remove();
                //$(topelement).append('<span>Account has been Setup Successfully</span>')
            },
            error: function (data) {
            }
        });
    });


        $('.updatetransaction').click(function () {

            var tid= $(this).parent().parent().attr('data-transactionid');
            var uid= $(this).parent().parent().attr('data-userid');
            var toaccid= $(this).parent().parent().attr('data-accid');
            var paymode= $(this).parent().parent().find('.paymode option:selected').val();
            var subcatid= $(this).parent().parent().attr('data-subcatid');
            var catid= $(this).parent().parent().attr('data-catid');
            var desc= $(this).parent().parent().find('.desc').val();
            var paidamount= $(this).parent().parent().find('.dueamount').val();
            var budget= $(this).parent().parent().find('.budget').attr('data-budgetamount');
            var duemonth= $(this).parent().parent().find('.paydue').attr('data-month');
            var duedate= $(this).parent().parent().find('.paydue').attr('data-date');
            var paiddate= 0;
            var paidmonth= 0;
            var paidyear= 0;
            var recurrence = $(this).parent().parent().attr('data-recurrence');

        
            UpdateTransaction(tid,uid,toaccid,paymode,subcatid,catid,desc,paidamount,budget,duemonth,duedate,paiddate,paidmonth,paidyear,recurrence);
        });
    });