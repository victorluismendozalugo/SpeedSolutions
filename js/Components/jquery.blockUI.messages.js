/**
 * Created by Erick Zamora on 17/01/2017.
 */

/**
 * Bloquea toda la pantalla y muestra un mensaje de carga
 */


$.fn.ajaxLoading = function (options) {
    var $el = $(this);
    var o = $.extend(true, {
        css: {
            border: '1p solid #EEE',
            padding: '15px',
            backgroundColor: '#FFF',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .9,
            color: '#000',
            zIndex: '11000'
        },
        message: '<i class="fa fa-spinner fa-pulse fa-2x fa-fw"></i><span>Cargando...</span>'
    }, options);

    $el.block(o);
}

$.fn.ajaxLoadingClose = function (options) {
    $(this).unblock();
};

$.ajaxLoading = function (title, progress, options) {
    if (!title) {
        title = "Cargando Datos...";
    }
    if (!progress) {
        progress = 100;
    }

    var o = $.extend(true, {
        css: {
            border: '1p solid #EEE',
            padding: '15px',
            backgroundColor: '#FFF',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .9,
            color: '#000',
            zIndex: '11000'
        },
        onBlock: function (e) {
            $(document).off('click', '.blockUI').on('click', '.blockUI', function () {
                $.unblockUI();
            });
        },
        message: '<h4>' + title + '</h4>\
                  <div class="progress">\
                    <div class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: '+ progress + '%">\
                      <span class="sr-only">Espere un momento por favor.</span>\
                    </div>\
                  </div>',
    }, options);

    $.blockUI(o);
};

$.ajaxLoadingClose = function (options) {
    $.unblockUI(options);
};

/**
 * Muestra una notificacion en la esquina superior derecha de la pantalla
 * @param message message String mensaje a mostrar
 * @param type String Tipo de notificacion a mostrar (success, alert, session, error, info)
 * @param title String Titulo a mostrar en la parte de arriba del texto
 * @param timeout int cantidad de milisegundos que tardara e desvanecerse el mensaje,
 */
$.notice = function (message, type, title, timeout) {
    if (timeout === undefined) {
        timeout = 5000;
    }
    if (title) {
        title = "<h5>" + title + "</h5>";
    } else {
        title = "";
    }

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": timeout,
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    if (['success', 'info', 'error', 'warning', 'alert', 'session'].indexOf(type) == -1) {
        type = 'error';
    }
    if (type == 'session') {
        type = 'warning';
    }
    if (type == 'alert') {
        type = 'warning';
    }
    toastr[type](message, title)

    /*
    var border_back_Colors = {
        'success': ['#449D44', '#DFF0D8'],
        'alert': ['#EC971F', '#FCF8E3'],
        'session': ['#EC971F', '#FCF8E3'],
        'error': ['#C9302C', '#F2DEDE'],
        'info': ['#31B0D5', '#D9EDF7']
    };
    
   */

};

$.noticeAlert = function (message, title, timeout) {
    $.notice(message, 'warning', title, timeout);
};
$.noticeError = function (message, title, timeout) {
    $.notice(message, 'error', title, timeout);
};
$.noticeSuccess = function (message, title, timeout) {
    $.notice(message, 'success', title, timeout);
};
$.noticeInfo = function (message, title, timeout) {
    $.notice(message, 'info', title, timeout);
};
$.noticeSession = function (message, title, timeout) {
    $.notice(message, 'session', title, timeout);
};

