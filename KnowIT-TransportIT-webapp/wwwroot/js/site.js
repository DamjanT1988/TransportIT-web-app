$(document).ready(function () {
    // Check local storage for theme preference
    if (localStorage.getItem("theme") === "dark-mode") {
        $("body").addClass("dark-mode");
        $("#darkModeToggle").text("LIGHT MODE");
    }

    $("#darkModeToggle").click(function () {
        $("body").toggleClass("dark-mode");

        // Update the button text based on mode
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
    $('#sortableTable').DataTable();
});
