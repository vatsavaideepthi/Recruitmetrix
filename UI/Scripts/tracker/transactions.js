$('.updatetransaction').on('click', '.addaccount', function () {
    var usrid = $(this).parent().parent().attr('data-userid');
    var toaccid = $(this).parent().parent().attr('data-accid');

    $.ajax({
        url: "/Payments/updateTransaction",
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
});