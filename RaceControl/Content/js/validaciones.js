

$('#inputNombre').blur(function () {
    //alert('ingreso');
    if (!$(this).val()) {
        $(this).parents('p').addClass('warning');
        
    }
});