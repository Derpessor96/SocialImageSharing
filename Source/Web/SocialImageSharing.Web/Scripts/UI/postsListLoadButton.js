function beginAjax() {
	$("#load-posts-button").hide();
}

function completeAjax() {
	$("#load-posts-button").show();
}

(function () {
	var nextPage = 1;

	$("#load-posts-button").on("click", function () {
		var currentLink = $(this).attr("href");
		var prefix = currentLink.substring(0, currentLink.indexOf("page=") + "page=".length);
		var sufix = currentLink.substring(currentLink.indexOf("&orderBy="), currentLink.length);

		$(this).attr("href", prefix + nextPage + sufix);

		++nextPage;
	});
}())