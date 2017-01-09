$(window).on("load", function () {

    // VARS

    var $table = $(".table-bootstrap");

    // FUNCTIONS

    // EVENTS

    $("#btn-loading").click(function () {
        $(this).button("loading");
    });

    // INIT

    $table.bootstrapTable();

    $("[data-toggle='tooltip']").tooltip();

    $table.bootstrapTable("togglePagination");
});