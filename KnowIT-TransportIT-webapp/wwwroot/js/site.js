$(document).ready(function () {

    if (localStorage.getItem("theme") === "dark-mode") {
        $("body").addClass("dark-mode");
        $("#darkModeToggle").text("LIGHT MODE");
    }

    $("#darkModeToggle").click(function () {
        $("body").toggleClass("dark-mode");


        if ($("body").hasClass("dark-mode")) {
            $("#darkModeToggle").text("LIGHT MODE");
            localStorage.setItem("theme", "dark-mode");
        } else {
            $("#darkModeToggle").text("DARK MODE");
            localStorage.removeItem("theme");
        }
    });
});


$(document).ready(function () {
    $('#sortableTable').DataTable({
        "pageLength": 13,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
    });
});
