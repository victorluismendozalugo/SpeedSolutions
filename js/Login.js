$(document).ready(function () {

    limpiarCampos()

});

$('#btnaceptar').click(function () {
    LogueoUsuario();
});

LogueoUsuario = () => {
    let datos = {
        login: {
            "UsuarioClave": $("#txtlogin").val(),
            "UsuarioContraseña": $("#txtpassword").val(),
        }
    }
    if (validaDatosSesion())
        $(datos).AjaxRequest({
            url: "Login.aspx/Logeo",
            success: function (datos) {
                (datos) ? $(location).attr('href', 'Principal.aspx') : noSesion();
            }, error: function (e) {
                bootbox.alert("Error en el servidor, contactar con el administrador");
                console.log(e.statusText + " " + e.status)
            }
        });
    else
        bootbox.alert("Favor de validar sus credenciales");

    limpiarCampos()
}

noSesion = () => {
    limpiarCampos()
    bootbox.alert("Usuario o contraseña incorrecto")
}

validaDatosSesion = () => {
    return $("#txtlogin").val() == "" || $("#txtpassword").val() == "" ? false : true;
}

limpiarCampos = () => {
    $("#txtlogin").val("")
    $("#txtpassword").val("")
}