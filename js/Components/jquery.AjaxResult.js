RESULT_TYPE_SUCCESS = 'success';
RESULT_TYPE_ALERT = 'alert';
RESULT_TYPE_INFO = 'info';
RESULT_TYPE_SESSION = 'session';
RESULT_TYPE_ERROR = 'error';
RESULT_TYPE_PERMISO = 'permiso';

(function ($) {
    $.fn.AjaxResult = function (options) {
        var o = $.extend({
            OnInfo: function (r) {
                $.noticeInfo(r.Message, 'Aviso');
            }, //1
            OnSuccess: function (r) {
                $.noticeSuccess(r.Message, 'Operación realizada con éxito');
            },//0
            OnWarning: function (r) {
                $.noticeAlert(r.Message, 'Alerta');
            },//2
            OnError: function (r) {
                $.noticeError(r.Message, 'Ocurrió un error');
            }, //3
            OnSession: function (r) {
                $.noticeSession(r.Message, 'Se ha perdido la sesion');
                setTimeout(function () {
                    parent.location = g_config.baseUrl;
                }, 3000);
            }, //4
            OnPermiso: function (r) {
                $.notice(r.Message, RESULT_TYPE_PERMISO, 'Permiso denegado');
            }, //5
            GlobalFunction: function (r) {
            },
            isGlobal: false,
            showMessage: true,
            callback: false
        }, options);

        var showMessage = function (r) {
            if (o.showMessage && r.HasMessages) {
                // $.unblockUI();
                switch (r.Type) {
                    case RESULT_TYPE_SESSION:
                        o.OnSession(r);
                        break;
                    case RESULT_TYPE_ERROR:
                        o.OnError(r);
                        break;
                    case RESULT_TYPE_INFO:
                        o.OnInfo(r);
                        break;
                    case RESULT_TYPE_ALERT:
                        o.OnWarning(r);
                        break;
                    case RESULT_TYPE_SUCCESS:
                        o.OnSuccess(r);
                        break;
                    case RESULT_TYPE_PERMISO:
                        o.OnPermiso(r);
                        break;
                    default:
                        o.OnError(r);
                }
            }
        };

        var valido = false;
        this.each(function () {
            var r = $.extend({
                IsValid: false,
                HasMessages: true,
                Message: '',
                Data: {},
                Type: RESULT_TYPE_SUCCESS
            }, this);
            if (o.isGlobal) {
                o.OnInfo = o.GlobalFunction;
                o.OnSuccess = o.GlobalFunction;
                o.OnWarning = o.GlobalFunction;
                o.OnError = o.GlobalFunction;
            }
            r.HasMessages = (r.Message && ($.trim(r.Message) != ""));
            showMessage(r);

            valido = r.IsValid;
        });

        return valido;

    };
})
(jQuery);