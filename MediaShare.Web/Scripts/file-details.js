﻿
$('#comment-container').hide();
function showCommentContainer() {
    $('#comment-container').show();
    $('#show-pannel-btn').hide();
}
function hideCommentContainer(e) {
    $('#comment-container').hide();
    $('#show-pannel-btn').show();
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