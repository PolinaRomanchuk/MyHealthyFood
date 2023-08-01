$(document).ready(function () {

    (function init() {
        const manufacturer = $('#Manufacturer').val();
        $('.manufacturer-preview').text(manufacturer);
    })();


    $('#Name').on("keyup", function () {
        const productName = $('#Name').val();
        $('.name-preview').text(productName);
    });

    $('#Price').on("keyup", function (e) {
        const price = $('#Price').val();
        $('.price-preview').text(price);
        
    });
    $('#Price').on("keydown", function (e) {
        let regex = /^[0-9]+$/
        let key = String.fromCharCode(e.which)
        if (e.which == 8) {
            return;
        }
        if (!key.match(regex)) {
            e.preventDefault();
            e.stopPropagation();
            $('.price-error').text("NO letters!");
        } else {
            $('.price-error').text("");
        }
    });


    $('#Img').on("keyup", function () {
        const coverUrl = $('#Img').val();
        $('.product--image img').attr('src', coverUrl);
    });


    $('#Manufacturer').on("change", function () {
        const manufacturer = $('#Manufacturer').val();
        $('.manufacturer-preview').text(manufacturer);
    });

});

