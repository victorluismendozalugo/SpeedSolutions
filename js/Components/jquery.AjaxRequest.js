var currentAjaxRequest = [];
var _currentAjax;
//(function ($) {

$.fn.AjaxRequest = function (options) {

    if (options.url === "") {
        console.log("Debe indicar una url para enviar los datos");
        return;
    }

    var o = $.extend({
        apiprefix: '',
        url: '',
        method: "Post",
        async: true,
        cache: true,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        ajaxStart: function (e) {
            $.ajaxLoading(e);
        },
        ajaxComplete: function (e) {
            $.ajaxLoadingClose(e);
        },
        allowCancelRequest: false,
        printParamsOnScreen: false,
        validateResponse: false,
        error: function (e) {
            $.notice("Error en el Servidor. Favor de comunicarse con el Administrador.<br/> " + e.responseText, ' error');
        }
    }, options, true);

    PrepareData = function (o) {
        o.data = {};
        o.url = o.apiprefix + o.url;
        //if (o.datos) {
        //    o.data.solicitud = o.datos;
        //    //o.contentType = '';
        //}
        o.data = JSON.stringify(o.datos);
        return o;
    };

    var self = this[0];
    return (function () {
        o.type = o.method;
        o.datos = self;
        o = PrepareData(o);
        o.beforeSend = function (jqXHR, settings) {
            if (o.ajaxStart) {
                o.ajaxStart();
            }
            if (o.printParamsOnScreen) {
                if (o.data) {
                    if ($('#printParams').length > 0) {
                        $('#printParams').text(JSON.stringify(o.data));
                    } else {
                        if ($('body').length > 0) {
                            $('body').append('<div id="printParams" style="display:none">{0}</div>'.format(JSON.stringify(o.data)));
                        }
                    }
                }
            }
            if (options.beforeSend)
                options.beforeSend(jqXHR, settings);
        };
        o.success = function (evt) {
            var e = evt.d;
            if (o.ajaxComplete()) {
                o.ajaxComplete();
            }
            //var completo = o.success;

            // o.success = function (data) {
            // var datos;
            //if (data) {
            //    e = (typeof data.d) == 'string' ?
            //     eval('(' + data.d + ')') :
            //      data.d;
            //}
            //completo(e);
            //  }
            //$.ajax(o);
            if (o.validateResponse) {
                if ($(e).AjaxResult()) {
                    options.success(e.Data);
                }
            } else {
                options.success(JSON.parse(e));
            }
        };
        o.complete = function (jqXHR, textStatus) {
            if (o.ajaxComplete()) {
                o.ajaxComplete();
            }
            if (o.allowCancelRequest) {
                var index = currentAjaxRequest.indexOf(_currentAjax);
                if (index >= 0) {
                    currentAjaxRequest.splice(index, 1);
                }
                console.log("llamadasActuales", currentAjaxRequest);
            }
            if (options.complete)
                options.complete(jqXHR, textStatus);
        };
        _currentAjax = null;
        _currentAjax = $.ajax(o);

        if (o.allowCancelRequest) {
            currentAjaxRequest.push(_currentAjax);
            //console.log("agregarAjax", currentAjaxRequest);
        }
    })();
};
//})(jQuery);