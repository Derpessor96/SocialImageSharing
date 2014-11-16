function like(data) {
	$("#like-button").hide();
	$("#dislike-button").show();
	updateLikes(data);
}

function dislike(data) {
	$("#like-button").show();
	$("#dislike-button").hide();
	updateLikes(data);
}

function updateLikes(data) {
	$("#like-value").text(data);
}

function favorite() {
	$("#favorite-button").hide();
	$("#unfavorite-button").show();
}

function unfavorite() {
	$("#favorite-button").show();
	$("#unfavorite-button").hide();
}
