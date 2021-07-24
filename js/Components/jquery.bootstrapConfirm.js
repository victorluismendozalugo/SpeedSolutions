

(function ($) {

    $.bootstrapConfirm = function (options) {
        var confirmClassHTML = 'bootstrapConfirm';
        var confirmClass = '.bootstrapConfirm';
        var btnAccept = '.bootstrapConfirm_btn_accept';
        var btnCancel = '.bootstrapConfirm_btn_cancel';

        var o = $.extend(true, $.bootstrapConfirm.Options, options);

        $.bootstrapModal({
            target: document.body,
            title: o.title,              //Titulo del modal
            width: '30%',                       //ancho del modal en porcentaje
            message: o.message,                        //mensaje o contenido del modal
            closeButton: false,                  //Si se desea que aparezca el boton Cerrar
            closeOnClick: true,                 //si se desea que se cierre el modal al dar click en cualquier boton
            onLoad: function ($container) {
                $container.addClass(confirmClassHTML);
            },                       //evento a ejecutar al cargar el modal
            onHide: function () {
                if (o.callBack) {
                    console.log('bye');
                    var retorno = o.callBack(e);
                    o.callBack = null;
                    return retorno;
                }
            },                       //evento a ejecutar al cerrar un modal
            isStatic: true,                    //indica si al presionar el boton fuera del modal este quedara estatico(true) o se cerrara(false)
            buttons: [
                {
                    text: o.acceptButtonText,
                    onclick: o.onAccept,
                    icon: "glyphicon glyphicon-ok",
                    classes: "btn-primary"
                },
                {
                    text: o.cancelButtonText,
                    onclick: o.onCancel,
                    icon: "glyphicon glyphicon-ban-circle",
                    classes: "btn-default"
                }
            ]
        });

        //$(confirmClass).css({ "z-index": 20001 });
        /*
        $('.modal-backdrop:last').css({
            "z-index": "2000",
            'background-color': '#ccc'
        });
        */
    };

    $.bootstrapConfirm.Options = {
        title: "Confirmacion",              //Titulo del modal
        message: "",                        //mensaje o contenido del modal
        acceptButtonText: "Aceptar",        //Texto del boton Aceptar
        cancelButtonText: "Cancelar",       //Texto del boton Cancelar
        onAccept: null,                     //Evento a ejecutar una vez presionado el boton Aceptar
        onCancel: null,                      //Evento a ejecutar una vez presionado el boton Cancelar
        callBack: null                      //funcion que se ejecutara una vez cerrado el modal
    }
})(jQuery);
