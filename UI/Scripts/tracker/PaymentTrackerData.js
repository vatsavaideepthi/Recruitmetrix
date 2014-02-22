jQuery(document).ready(function () {
    $('.jsRemove').click(function () {
        var decision = confirm("Are you sure you want to remove this item?");
        if (decision) {
            var accid = $(this).parent().parent().attr('data-accountid');
            var usrid = $(this).parent().parent().attr('data-userid');
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
            $(this).parent().parent().remove();


        }
        else {

        }

    });

    

    $('.jsUpdate').click(function () {

        var accid = $(this).parent().parent().attr('data-accountid');
        var usrid = $(this).parent().parent().attr('data-userid');
        $.ajax({
            url: "/AjaxData/UpdateAccount",
            type: "POST",
            dataType: "JSON",
            data: {
                accountid: accid,
                accountname: $(this).parent().parent().find('.accname').text(),
                userid: usrid,
                accounttype: $(this).parent().parent().attr('data-accounttype'),
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
    });
});
  

    
   

    jQuery(document).ready(function () {

        $('.additem').click(function () {
            var clonetr = $(this).parents('table').find('#clonerow').clone();
            $(clonetr).removeClass('hide');
            $(clonetr).removeAttr('id');
            $(clonetr).attr('data-catid', $(this).attr('data-catid'));
            $(clonetr).attr('data-userid', $(this).attr('data-userid'));
            $(clonetr).attr('data-catname', $(this).attr('data-catname'));
            $(this).parents('tr').after(clonetr);
        });

        $('.invfulltime').tooltip('hide');
        

     

        $('.ptcontent').on('click','.addaccount',function () {
            alert("entered");
            var accid = $(this).parent().parent().attr('data-accountid');
            var usrid = $(this).parent().parent().attr('data-userid');
            $.ajax({
                url: "/Welcome/CreateAccount",
                type: "POST",
                dataType: "JSON",
                data: {
                    accountname: $(this).parent().parent().find('.accname').val(),
                    userid: usrid,
                    accounttype: "",
                    catid: $(this).parent().parent().attr('data-catid'),
                    catname: $(this).parent().parent().attr('data-catname'),
                    desc: $(this).parent().parent().find('.desc').val(),
                    providerid: "",
                    providername: $(this).parent().parent().find('.prname').val(),
                    amount: $(this).parent().parent().find('.budget').val(),
                    duemonth: $(this).parent().parent().find('.duemonth').val(),
                    recurrence: $(this).parent().parent().find('.recurrence option:selected').val(),
                    duedate: $(this).parent().parent().find('.duedate').val()

                },
                async: true,
                success: function (data) { },
                error: function (data) {
                }
            });
        });


     


        $('.ptcontent').on('click', '.jscollapse', function () {
            $(this).children('i').removeClass('icon-chevron-down').addClass('icon-chevron-right');
            $(this).removeClass('jscollapse').addClass('jsExpand');
            $(this).parents().closest('.recordheader').nextUntil('.recordheader').each(function () { $(this).hide().delay(3000); });

        });

        $('.ptcontent').on('click', '.jsExpand', function () {
            var elements = $(this).parents().closest('.recordheader').nextUntil('.recordheader').show();
            $(this).children('i').removeClass('icon-chevron-right').addClass('icon-chevron-down');
            $(this).removeClass('jsExpand').addClass('jscollapse');
        });

       

    });

    