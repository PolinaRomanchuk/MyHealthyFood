$(document).ready(function () {

    $('.get-games-count').click(function () {
        const userBudget = $('.user-budget').val();
        const url = '/api/game/GamesCount?budget=' + userBudget;
        $.get(url)
            .then(function (dataObj) {
                console.log(dataObj);
                const gameNames = dataObj.randomGamesNames.join(', ');
                const answer = `${dataObj.totalGamesCount} (${gameNames})`;
                $('.game-count').text(answer);
                $('.have-to-block').removeAttr('disabled');
                $('.loader').hide();
            });

        $('.have-to-block').attr('disabled', 'disabled');
        $('.loader').show();
    });

    $('.games-set h1').click(function () {
        $(this)
            .closest('.games-set')
            .find('.games-block')
            .toggle(1000);
    });

});

