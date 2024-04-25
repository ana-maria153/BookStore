function likeBook(bookId, event) {
    event.preventDefault();
    $.post('/home/likebook', { pubId: bookId }, function () {
        updateLikeCount(bookId);
    });
}

function updateLikeCount(bookId) {
    $.get('/home/booklikes', { pubId: bookId }, function (likes) {
        var likeCountElement = $('#like-count-' + bookId);
        likeCountElement.text(likes);
    });
}