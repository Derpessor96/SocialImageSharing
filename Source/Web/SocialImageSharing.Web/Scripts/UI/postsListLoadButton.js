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
		
		var pageIndex = currentLink.indexOf("page=");

		var prefix = currentLink.substring(0, pageIndex + "page=".length);
		var sufix = currentLink.substring(currentLink.indexOf("&", pageIndex + 1), currentLink.length);

		$(this).attr("href", prefix + nextPage + sufix);

		++nextPage;
	});
}())