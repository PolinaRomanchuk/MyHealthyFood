$(document).ready(function () {

    $('#Name').on("keyup", function () {
        const productName = $('#Name').val();
        $('.preview span.name').text(productName);
    });

    $('#Price').on("keyup", function () {
        const price = $('#Price').val();
        $('.preview span.price').text(price);
    });

    $('#ImgUrl').on("keyup", function () {
        const imgurl = $('#ImgUrl').val();
        $('.preview img').attr('src', imgurl);
    }); 
});
