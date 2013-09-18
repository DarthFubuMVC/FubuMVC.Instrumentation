$(document).ready(function() {
    $('tr.instrumentation-row').dblclick(function(tr) {
        var url = $(tr.currentTarget).data('url');
        window.location = url;
    });
})