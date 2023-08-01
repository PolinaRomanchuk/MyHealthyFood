$(document).ready(function () {

    $('#Name').on("keyup", function () {
        const gameName = $('#Name').val();
        $('.preview span.name').text(gameName);
    });

    $('#Price').on("keyup", function () {
        const price = $('#Price').val();
        $('.preview span.price').text(price);
    });

    //$('#Price').on("keydown", function (e) {
    //    if (e.which == 8) {
    //        return;
    //    }

    //    e.preventDefault();
    //    e.stopPropagation();
    //});

    $('#CoverUrl').on("keyup", function () {
        const coverUrl = $('#CoverUrl').val();
        $('.preview .preview-container img').attr('src', coverUrl);
    });

});

