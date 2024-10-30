$(document).ready(function () {
    $(document).on("click", ".page-link", function (e) {
        e.preventDefault();
        const url = $(this).attr("href");

        $.get(url, function (data) {
            $("#telescope-list-container").html(data);
        });
    });
});