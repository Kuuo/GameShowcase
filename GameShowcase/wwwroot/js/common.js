$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

function removeElementById(id) {
    $('#' + id).remove();
}