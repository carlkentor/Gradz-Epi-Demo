$(function () {
    var $newsItems = $(document).find(".news-item, .page-partial");
    $newsItems.each(function (i, current) {
        $(this).delay(i * 300).queue(function (next) {
            $(current).addClass("fade-from-darkness");
            next();
        });
    });


    $(document).on("scroll", function (e) {
        console.log("scrolling");
    });
});
