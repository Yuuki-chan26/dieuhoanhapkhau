$(document).ready(function () {
    $('.productDetail-buy .btn-up').click(function () {
        var num_order = parseInt($(this).parents('.quantity').find('.input-text').val());
        num_order += 1;
        $(this).parent().find('.input-text').val(num_order);
    });
    $('.productDetail-buy .btn-down').click(function () {
        var num_order = parseInt($(this).parents('.quantity').find('.input-text').val());
        if (num_order <= 1) {
            num_order = 1
        } else {
            num_order -= 1;
        }
        $(this).parent().find('.input-text').val(num_order);
    });
});
$(window).scroll(function () {
    if ($(this).scrollTop() > 200) $('#goTop').stop().animate({ bottom: '50px' }, 500);
    else $('#goTop').stop().animate({ bottom: '-60px' }, 500);
});
$(document).ready(function () {
    $('#goTop').click(function (event) {
        event.preventDefault();
        $('html, body').animate({ scrollTop: 0 }, 500);
    });
});