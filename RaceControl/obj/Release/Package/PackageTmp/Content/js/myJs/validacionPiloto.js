bootstrap_alert = function () { }

bootstrap_alert.warning = function (message) {
    $('#alertModal').html('<div class="alert alert-warning"><a class="close" data-dismiss="alert">×</a><span>' + message + '</span></div>')
}


$('#inputNombre').blur(function () {

    if (!$(this).val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el nombre del piloto');
    }
    else {
        $('#alertModal').html('')
    }

});

$('#inputApellido').blur(function () {

    if (!$(this).val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el apellido del piloto');
    }
    else {
        $('#alertModal').html('')
    }

});

$('#inputDNI').blur(function () {

    if (!$(this).val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el DNI del piloto');
    }
    else {
        $('#alertModal').html('')
    }

});

$('#inputEmail').blur(function () {

    if (!$(this).val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el Email del piloto');
    }
    else {
        $('#alertModal').html('')
    }

});

$("#formPiloto").on("submit", function () {

 

    if (!$('#inputNombre').val()) {
        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el nommbre del piloto');
        return false;
    }

    if (!$('#inputApellido').val()) {
        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el apellido del piloto');
        return false;
    }

    if (!$('#inputDNI').val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el DNI del piloto');
        return false;
    }
  
    if (!$('#inputEmail').val()) {

        bootstrap_alert.warning('<strong>¡Campo obligatorio!</strong> Por favor ingrese el Email del piloto');
        return false;
    }


});

