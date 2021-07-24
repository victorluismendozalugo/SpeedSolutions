
//var table = $('#dataTableProductos').DataTable();
$(document).ready(function () {

    limpiarCampos()
    ObtieneEstaciones()

});

limpiarCampos = () => {
    $('#txtClaveUsuario').val(0)
    $('#txtLogin').val("")
    $('#txtPrimerNombre').val("")
    $('#txtApellido').val("")
    $('#txtContraseña').val("")
    $('#txtEstatusUsuario').val(1)
}

$('#btnGuardarUsuario').click(function () {
    guardarUsuario()
});

var guardarUsuario = function () {
    var datos = {
        usuario: {
            "UsuarioID": $('#txtClaveUsuario').val(),
            "UsuarioClave": $('#txtLogin').val(),
            "UsuarioNombre": $('#txtPrimerNombre').val(),
            "UsuarioApellido": $('#txtApellido').val(),
            "UsuarioContraseña": $('#txtContraseña').val(),
            "UsuarioEstatus": parseInt($('#txtEstatusUsuario').val())
        }
    }
    $(datos).AjaxRequest({
        validateResponse: true,
        url: "Principal.aspx/UsuarioGuardar",
        success: function (d) {
            bootbox.alert("Datos registrados con exito");
        }, error: function (e) {
            console.log(e)
            bootbox.alert("Error al registrar el usuario");
        }
    });
}

var ObtieneEstaciones = function () {
    var datos = {
        estaciones: {
            "EstacionClave": 0
        }
    };

    $(datos).AjaxRequest({
        url: "Principal.aspx/EstacionesConsultar",
        success: function (datos) {
            if (datos) {
                if (datos) {
                    $('#listadoEstaciones').llenarCombo(datos, 'EstacionClave', 'EstacionNombre', 1);
                }
            }
        }
    });
}

$('#btnGuardarProducto').click(function () {
    guardarProducto()
})

var guardarProducto = function () {
    var datos = {
        producto: {
            "EstacionClave": $('#listadoEstaciones').val(),
            "ProductoClave": $('#txtClaveProducto').val(),
            "ProductoNombre": $('#txtProductoNombre').val(),
            "ProductoPrecio": parseFloat($('#txtProductoPrecio').val())
        }
    }
    $(datos).AjaxRequest({
        validateResponse: true,
        url: "Principal.aspx/ProductosGuardar",
        success: function (d) {
            bootbox.alert("Datos registrados con exito");
            limpiarCamposProductos()
        }, error: function (e) {
            console.log(e)
            bootbox.alert("Error al registrar el producto");
        }
    });
}

var limpiarCamposProductos = function () {
    $("#dataTableProductos").dataTable().fnClearTable();
    $("#dataTableProductos").dataTable().fnDestroy();
    $('#listadoEstaciones').val(1)
    $('#txtClaveProducto').val(0)
    $('#txtProductoNombre').val("")
    $('#txtProductoPrecio').val(0)
    $('#txtFechaActualizacion').val("")
    ObtieneProductos()
}

var ObtieneProductos = function () {
    var datos = {
        productos: {
            "EstacionClave": 0,
            "ProductoClave": 0
        }
    }
    $(datos).AjaxRequest({
        url: "Principal.aspx/ProductosConsultar",
        success: function (datos) {
            if (datos) {
                $("#dataTableProductos").dataTable().fnDestroy();
                $('#dataTableProductos').DataTable({
                    "data": datos,
                    "rowId": "ProductoClave",
                    "columns": [
                        { data: "EstacionClave" },
                        { data: "ProductoClave" },
                        { data: "EstacionNombre" },
                        { data: "ProductoNombre" },
                        { data: "ProductoPrecio" },
                        { data: "FechaActualizacion" },
                    ],
                    "columnDefs": [
                        { "visible": false, "targets": 0 },
                        { "visible": false, "targets": 1 }],
                    select: false
                });
            }
        }
    });
}

$('#btnRegistroProductos').click(function () {
    ObtieneProductos()
})


$('#dataTableProductos').on('dblclick', 'tbody tr', function () {

    let tabla = $('#dataTableProductos').DataTable();
    $('#txtClaveProducto').val(tabla.row(this).data().ProductoClave)
    $('#listadoEstaciones').val(tabla.row(this).data().EstacionClave)
    $('#txtProductoNombre').val(tabla.row(this).data().ProductoNombre)
    $('#txtProductoPrecio').val(tabla.row(this).data().ProductoPrecio)
    $('#txtFechaActualizacion').val(tabla.row(this).data().FechaActualizacion)

});