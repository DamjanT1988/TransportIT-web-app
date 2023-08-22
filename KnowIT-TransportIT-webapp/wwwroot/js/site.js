//dark mode function
$(document).ready(function () {
    //save preference in local storage
    if (localStorage.getItem("theme") === "dark-mode") {
        $("body").addClass("dark-mode");
        $("#darkModeToggle").text("LIGHT MODE");
    }

    //toggle modes
    $("#darkModeToggle").click(function () {
        $("body").toggleClass("dark-mode");

        //switch modes
        if ($("body").hasClass("dark-mode")) {
            $("#darkModeToggle").text("LIGHT MODE");
            localStorage.setItem("theme", "dark-mode");
        } else {
            $("#darkModeToggle").text("DIMM MODE");
            localStorage.removeItem("theme");
        }
    });
});

//function for managing tables - DataTable
$(document).ready(function () {
    $('#sortableTable').DataTable({
        "pageLength": 11, //specify defaultlenght of table
        "lengthMenu": [[10, 20, 30, 50, 100, -1], [10, 20, 30, 50, 100, "All"]], //specify the various table options, in menu
        "stateSave": true // save state - paging, ordering, and search settings
    });
});
