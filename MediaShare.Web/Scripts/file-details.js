
$('#comment-container').hide();
function showCommentContainer() {
    $('#comment-container').show('slow');
    $('#show-pannel-btn').hide('slow');
}
function hideCommentContainer(e) {
    $('#comment-container').hide('slow');
    $('#show-pannel-btn').show('slow');
}
function pesho() {
    $('#comment-message').show();
    setInterval(function () {
        $('#comment-message').fadeOut(2000);
    }, 2000)
}

function hideAddFavBtn() {
    $('#add-fav-btn').hide();
    $('#rem-fav-btn').show();
}
function hideRemFavBtn() {
    $('#add-fav-btn').show();
    $('#rem-fav-btn').hide();
}
