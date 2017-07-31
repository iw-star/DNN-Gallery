$(function () {

    resizeIWGallery();

    $(window).resize(function () {
        resizeIWGallery();
    });

    $(".iw-star-gallery.carousel.lazy").on("slide.bs.carousel", function (e) {
        var lazy;
        lazy = $(e.relatedTarget).find("img[data-src]");
        lazy.attr("src", lazy.data('src'));
        lazy.removeAttr("data-src");
    });

    $('.iw-star-gallery .thumbnails-carousel').parent().on('slide.bs.carousel', function (e) {
        $(this).find('.thumbnails-carousel li').removeClass('active-thumbnail');
        $(this).find('.thumbnails-carousel li').eq($(e.relatedTarget).index()).addClass('active-thumbnail');
    });

    $('.iw-star-gallery .thumbnails-carousel li').click(function () {
        $(this).parents('.carousel').carousel($(this).index());
    });

    $('.iw-star-gallery .iw-popup img').click(function () {
        var title = $(this).data("title");
        var desc = $(this).data("desc");
        var comStr = "";
        if (title != null && title.length != 0) {
            comStr = title + "<br>";
        };
        if (desc != null && desc.length != 0) {
            comStr += desc;
        };

        $.fancybox({
            'padding': 0,
            'href': $(this).attr("src"),
            'title': comStr,
            'titlePosition': "over",
            'transitionIn': 'elastic',
            'transitionOut': 'elastic'
        });
    });

});

function resizeIWGallery() {
    $(".iw-star-gallery").each(function () {
        var width = $(this).find(".carousel-inner").data("width");
        var height = $(this).find(".carousel-inner").data("height");
        var currentWidth = $(this).width();
        $(this).find(".item").css("height", currentWidth * height / width);
    });
}

function confirmDeleteItem() {
    return confirm("Are you sure you want to delete?");
}