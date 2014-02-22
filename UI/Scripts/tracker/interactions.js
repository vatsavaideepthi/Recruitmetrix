jQuery(document).ready(function () {


    //Handles Toprmenu Inerations here

    $('.topbarmenu .levelone li').click(function () {

        //Identifying the Menu Item got clicked

        $('.topbarmenu .levelone li').each(function () {
            $(this).removeClass('active');
        })
        $(this).addClass('active');


        var levelitem = $(this);
        var levelitemdata = levelitem.attr('data-childmenuset');

        //Displaying Respective childmenu Item and hiding Others

        $('.topbarmenu ul.leveltwo').each(function () {

            if ($(this).attr('data-menuset') == levelitemdata) {
                $(this).show();
            }
            else { $(this).hide(); }
        });
    });







});