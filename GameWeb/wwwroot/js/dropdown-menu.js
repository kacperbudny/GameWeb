$('.dropdown-container').on("click", function () {

    $(this).find(".dropdown-content").toggle();
    $(this).siblings().find(".dropdown-content").hide();

    if ($(".dropdown-content:visible").length === 0) {
        $("#menu-overlay").hide();
    } else {
        $("#menu-overlay").show();
    }
});

$("#menu-overlay").on("click", function () {
    $(".dropdown-content").hide();
    $(this).hide();
});