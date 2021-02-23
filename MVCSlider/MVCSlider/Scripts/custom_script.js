
$(document).ready(function () {

    $(document).on('click', '.mob-menu', function () {
        if ($('.nav-pills').hasClass('active') == true) {
            $('.nav-pills').removeClass('active');
        } else {
            $('.nav-pills').addClass('active');
        }

});

/*
    $('.mob-menu').click(function () {
        //$('html, body').animate({
        //    scrollTop: $(".form").offset().top
        //}, 1000);

        alert('ss');


    });
*/
    $('.login-btn').click(function () {
        //$('html, body').animate({
        //    scrollTop: $(".form").offset().top
        //}, 1000);

        $("#frmLogin").modal('show');


    });
});

