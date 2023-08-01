$(document).ready(function () {

    $('.get-products-count').click(function () {
        const userTag = $('.user-tag').val();
        const url = `/api/store/ProductCount?userTag=${userTag}`;

        $.get(url)
            .then(function (dataObj) {
                console.log(dataObj);
                const namesofproducts = dataObj.productNames.join(', ');
                const answer = `${dataObj.totalProductCount} (${namesofproducts})`;
                $('.product-count').text(answer);
                $('.have-to-block').removeAttr('disabled');
                $('.loader').hide();
            });

        $('.have-to-block').attr('disabled', 'disabled');
        $('.loader').show();
    });
});