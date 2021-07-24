function formatoFecha(fecha) {

    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    fechacorta = formatodia.substring(0, 2) + "/" + formatomes.substring(3, 5) + "/" + formatoaño.substring(6, 10);

    return fechacorta;

}

function formatoFecha99(fecha) {

    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    //fechacorta = formatodia.substring(0, 2) + "/" + formatomes.substring(3, 5) + "/" + formatoaño.substring(6, 10);
    fechacorta = formatomes.substring(3, 5) + "/" + formatodia.substring(0, 2) + "/" + formatoaño.substring(6, 10);

    return fechacorta;

}

function formatoFecha2(fecha) {

    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    fechacorta = formatoaño.substring(6, 10) + "-" + formatomes.substring(3, 5) + "-" + formatodia.substring(0, 2);

    return fechacorta;

}

function formatoFecha3(fecha) {

    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    fechacorta = formatoaño.substring(0, 4) + "-" + formatomes.substring(5, 8) + formatodia.substring(8, 10);

    return fechacorta;

}

function formatoFecha4(fecha) {

    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    console.log(fecha);
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    fechacorta = formatoaño.substring(0, 4) + "-" + formatodia.substring(8, 10) + "-" + formatomes.substring(5, 7);

    return fechacorta;

}


function formatoFechaServidor(fecha) {
    //console.log(fecha);
    var formatodia = "";
    var formatomes = "";
    var formatoaño = "";
    var fechacorta = "";
    formatodia = fecha;//$(this).attr("data-fechaFin");
    formatomes = fecha;//$(this).attr("data-fechaFin");
    formatoaño = fecha;//$(this).attr("data-fechaFin");
    fechacorta = formatomes.substring(2, 5).replace('/', '') + "/" + formatodia.substring(0, 2).replace('/', '') + "/" + formatoaño.substring(5, 10);

    return fechacorta;

}

$.fn.llenarCombo = function (Data, idProperty, textproperty, selectedId) {
    var $cmb = this;
    for (var i = 0; i < Data.length; i++) {
        $cmb.append('<option value=' + Data[i][idProperty] + '>' + Data[i][textproperty] + '</option>');
    }
    if (selectedId !== undefined) {
        $cmb.val(selectedId);
    }

    if (selectedId) {
        $cmb.val(selectedId);
    }
};

function filterFloat(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
function filter(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}


$.fn.llenarTabla = function (Data, par1, par2, par3, par4, par5, par6, par7, par8) {

    var dat = "";
    $("#dataTable tbody").html("");

    for (var i = 0; i < Data.length; i++) {
        dat = "<tr role='row' class='role' data-id='" + Data[i][par1] + "'>";
        dat += "<td class='sorting_1'>" + Data[i][par2] + "</td>";
        dat += "<td class=''>" + Data[i][par3] + "</td>";
        dat += "<td>" + Data[i][par4] + "</td>";
        dat += "<td>" + Data[i][par5] + "</td>";
        if (par6) {
            dat += "<td>" + Data[i][par6] + "</td>";
        }
        if (par7) {
            dat += "<td>" + Data[i][par7] + "</td>";
        }
        if (par8) {
            dat += "<td>" + Data[i][par8] + "</td>";
        }
        dat += "</tr>";
        $("#dataTable tbody").append(dat);
    }
    $("#dataTable").DataTable();
};



/* LOAD JQUERY PROCESS */

function formato_numero(numero, decimales, separador_decimal, separador_miles) { // v2007-08-06
    numero = parseFloat(numero);
    if (isNaN(numero)) {
        return "";
    }

    if (decimales !== undefined) {
        // Redondeamos
        numero = numero.toFixed(decimales);
    }

    // Convertimos el punto en separador_decimal
    numero = numero.toString().replace(".", separador_decimal !== undefined ? separador_decimal : ",");

    if (separador_miles) {
        // Añadimos los separadores de miles
        var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
        while (miles.test(numero)) {
            numero = numero.replace(miles, "$1" + separador_miles + "$2");
        }
    }

    return numero;
}
function getURLParameter(name) {
    return decodeURI((RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, ""])[1]);
}

function isNumeric(e) {
    if (e.shiftKey || e.ctrlKey || e.altKey) { // if shift, ctrl or alt keys held down 
        e.preventDefault();         // Prevent character input 
    } else {
        var n = e.keyCode;
        if (!((n == 8)           // backspace
            || (n == 190)            // Point
            || (n == 46)            // Point
            || (n == 9)              //Tab
            || (n == 46)             // delete 
            || (n >= 35 && n <= 40)  // arrow keys/home/end 
            || (n >= 48 && n <= 57)  // numbers on keyboard 
            || (n >= 96 && n <= 105))// number on keypad 
        ) {
            e.preventDefault();  // Prevent character input 
        }
    }
}


/**
 * Reemplaza el texto especificado dentro de una cadena por otro
 * @param txt texto donde se buscara los caracteres a reemplazar
 * @param replace texto a remplazar
 * @param with_this c. que texto se a a reemplazar
 * @returns {XML|string|*|void}
 */
function replaceAll(txt, replace, with_this) {
    return txt.replace(new RegExp(replace, 'g'), with_this);
}

String.prototype.replaceAll = function (replace, with_this) {
    return replaceAll(this, replace, with_this);
};

/**
 * Identifica si la variable especificada esta declarada y definida
 * @param variable
 * @returns {boolean}
 */
isDefined = function (variable) {
    return typeof window[variable] == "undefined" ? !1 : !0
};

/**
 * Valida si un caracter es alfanumerico
 * @param n
 * @returns {boolean}
 */
isAlpha = function (n) {
    var t = document.all ? n.keyCode : n.which, i;
    return t == 0 || t == 8 ? !0 : (t = String.fromCharCode(t), i = /^[Ã±Ã‘A-Za-z0-9@_\-.\s]*$/, i.test(t))
};
/**
 * Valida si un caracter es alfabetico
 * @param n
 */
isAlphabetic = function (n) {
    var t, i;
    return t = document.all ? n.keyCode : n.which, i = String.fromCharCode(t), !/[^Ã±Ã‘a-zA-Z]/.test(i)
};
/**
 * valida si un caracter es numerico
 * @param n
 * @returns {boolean}
 */
isInteger = function (n) {
    var t = document.all ? n.keyCode : n.which, i, r;
    return t == 8 || t == 0 ? !0 : (i = /\d/, r = String.fromCharCode(t), i.test(r))
};
/**
 * Valida si un campo es decimal
 * @param n evento que indica el caracter a validar
 * @param t objeto dom que obtiene el texto a validar si corresponde a un decimal
 * @returns {boolean}
 */
isDecimal = function (n, t) {
    var i = document.all ? n.keyCode : n.which, u = t.value, r = u.indexOf("."), f, e;
    return i == 0 || i == 8 ? !0 : i == 46 && r != -1 ? !1 : r != -1 && (f = r + 2, f == u.length - 1) ? !1 : (i = String.fromCharCode(i), e = /^[0-9.]*$/, e.test(i) ? !0 : !1)
};
/**
 * Valida si el caracter es valido para introducir en un correo
 * @param n objeto del evento keypress
 * @returns {boolean}
 */
isEmail = function (n) {
    var t = document.all ? n.keyCode : n.which, i;
    return t == 0 || t == 8 ? !0 : (t = String.fromCharCode(t), i = /^[A-Za-z0-9@_\-.]*$/, i.test(t) ? !0 : !1)
};
/**
 * valida si un texto tiene el formato correcto para un Email
 * @param n objeto DOM que contiene el texto a validar
 */
hasEmailFormat = function (n) {
    var t = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/.test(n.value);
    return t || (n.value = ""), t
};
/***
 * Valida la captura de controles segun la clase que se le aplique
 * .alpha valida caracteres alfanumericos (A-Z a-z 0-9)
 * .alphabetic valida caracteres alfabetioos (A-Z a-z)
 * .integer valida caracteres numericos (0-9)
 * .decimal valida caracteres decimales (solo numeros y 1 punto decimal)
 * .email valida que solo se puede introducir caracteres validos para un email (a-z A-Z 0-9 _-.@)
 * Ademas agrega una validacion a todos los campos textarea para que funcione correctamente el atributo maxlength
 */
validarCapturaCampos = function () {

    $(document).on("keypress", ".alpha", function (n) {
        return isAlpha(n)
    });
    $(document).on("keypress", ".alphabetic", function (n) {
        return isAlphabetic(n)
    });
    $(document).on("keypress", ".integer", function (n) {
        return isInteger(n)
    });
    $(document).on("change", ".integer", function () {
        isNaN($(this).val()) && $(this).val("")
    });
    $(document).on("keypress", ".decimal", function (n) {
        return isDecimal(n, this)
    });
    $(document).on("change", ".decimal", function () {
        isNaN($(this).val()) && $(this).val("")
    });
    $(document).on("keypress", ".email", function (n) {
        return isEmail(n)
    });

    $(document).off("keyup blur", "textarea[maxlength]").on("keyup blur", "textarea[maxlength]", function () {
        var n = $(this).attr("maxlength"), t = $(this).val();
        t.length > n && $(this).val(t.slice(0, n))
    })
};

/**
 * Indica si el elemento Jquery esta visible
 * @returns {*|jQuery|boolean}
 */
$.fn.isVisible = function () {
    return ($(this).is(':visible') && $(this).parents(':hidden').length == 0);
};


/**
 * Permite reemplazar los valores de una cadena por los k estan en {}
 * ejemplo:
 *        "Juan {0} {1}".format(["Perez","Garcia"]) //regresa "Juan Perez Garcia"
 * @param values cadena o arreglo de parametros
 * @returns {String}
 */
String.prototype.format = function (values) {
    var pattern = this;
    if (values.push == undefined) {
        values = [values];
    }

    for (var i = 0; i < values.length; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "g");
        pattern = pattern.replace(reg, values[i]);
    }

    return pattern;
};

/**
 * redirecciona a una url especifica
 * @param url string url a donde va a redireccionar
 * @param timeout (null), en caso de especificarse es el tiempo en ms en tardar para ejecutar el redireccionamiento
 */
var redireccionar = function (url, timeout) {
    if (timeout) {
        setTimeout(function () {
            document.location.href = url;
        }, timeout);
    } else {
        document.location.href = url;
    }
};


/**
 * arrayChunk
 * replicates the array_chunk php function by splitting the the specified
 * array into an array of arrays of the specified size
 *
 * @param array
 *    the array to be chunked
 * @param size
 *    the size of the chunks
 * @return an array of arrays, each of at most the specified size
 */
Array.prototype.arrayChunk = function (size) {
    var array = this;
    //declare vars
    var output = [];
    var i = 0; //the loop counter
    var n = 0; //the index of array chunks

    for (item in array) {
        if (array.hasOwnProperty(item)) {
            //if i is > size, iterate n and reset i to 0
            if (i >= size) {
                i = 0;
                n++;
            }

            //set a value for the array key if it's not already set
            if (!output[n] || output[n] == 'undefined') {
                output[n] = [];
            }

            output[n][i] = array[item];

            i++;

        }
    }
    //se quita el ultimo elemento que al parecer esta guardando una funcion anonima
    //output[output.length].pop();

    return output;
};

/**
 * obtiene un arreglo con las claves o nombres del arreglo de objetos
 * Ejemplo {'nombre':'juan', 'edad':2} regresa ['nombre','edad']
 * @param obj json objeto a buscar
 * @return array
 */
var array_keys = function (obj) {
    var a = obj;
    var array_keys = [];

    for (var key in a) {
        array_keys.push(key);
    }
    return array_keys;
};

/**
 * obtiene un arreglo con los valores del arreglo de objetos
 * Ejemplo {'nombre':'juan', 'edad':2} regresa ['juan','2']
 * @param obj json objeto a buscar
 * @return array
 */
var array_values = function (obj) {
    var a = obj;
    var array_values = [];

    for (var key in a) {
        array_values.push(a[key]);
    }
    return array_values;
};

/**
 * Realiza un scroll en pantalla hacia el elemento especificado
 */
$.fn.scrollOn = function () {
    var $self = this;
    $('html, body').animate({
        scrollTop: $self.offset().top
    }, 500)
};


String.prototype.leftPad = function (digits, char) {
    var num = this;
    if (!char) char = '0';
    while (num.length < digits) {
        num = char + num;
    }
    return (num);
};
String.prototype.rightPad = function (digits, char) {
    var num = this;
    if (!char) char = '0';
    while (num.length < digits) {
        num = num + char;
    }
    return (num);
};

var validarCamposCapturar = function () {
    $('input[required],select[required]').each(function () {
        var $el = $(this);
        if ($el.val() == '') {
            $el.addClass('required')
        } else {
            $el.removeClass('required')
        }
    });

    if ($('input[required],select[required]').length > 0 && $('.required').length > 0) {
        $.noticeAlert('Existen campos requeridos sin capturar, verifique', 'Campos requeridos');
        return false;
    }
    return true;
}

$(document).ready(function () {
    /*Seleccionar texto input*/
    $("input[type=text]").focus(function () {
        this.select();
    });

    $("input[type=password]").focus(function () {
        this.select();
    });


    /*codigo para aparecer y desaparecer un div como si fuera tootiptext
 $("#txtusuario").mouseenter(function (e) {
     $("#tooltip").html('');      
         $("#tooltip").css("left", e.pageX + 5);
         $("#tooltip").css("top", e.pageY + 5);
         $("#tooltip").css("display", "block");
         $("#tooltip").addClass("border");
         $("#tooltip").append("<strong>Favor de Ingresar el Usuario<strong>")       

 });


 $("#txtusuario").mouseleave(function (e) {
     $("#tip1").css("display", "none");
 });
*/









    //$('#txtFechaCotizacion').datepicker({ format: 'dd/mm/yyyy' }).on('changeDate', function (ev) {
    //    if (ev.date.valueOf() < endDate.valueOf()) startDate = ev.date - 1;
    //    else {
    //        //$('#txtDateIni').val(startDate.format('dd/mm/yyyy'));
    //        //$('#txtDateIni').datepicker('update');
    //        //$('#txtDateIni').datepicker('hide');
    //    }
    //});





});


var CodigoBarrasProductoID = function (codigo) {

    var id = 0;
    id = codigo.substring(0, 9);
    return parseInt(id);
    // 000001202 * 00000009
}

var CodigoBarrasProductoIDAnterior = function (codigo) {
    var id = 0;
    return id = codigo.substring(0, 9);
    // 000001202 * 00000009
}

var CodigoBarrasProductoTalla = function (codigo) {
    var id = 0;
    return id = codigo.substring(9, 18);
    // 000001202 * 00000009
}

var GeneraCB = function (id) {

    var numberOutput = Math.abs(id); /* Valor absoluto del número */
    var length = 9; /* Largo del número */
    var zero = "0"; /* String de cero */

    var n = "";

    if (width <= length) {
        if (number < 0) {
            n = ("-" + numberOutput.toString());
        } else {
            n = numberOutput.toString();
        }
    } else {
        if (number < 0) {
            n = ("-" + (zero.repeat(width - length)) + numberOutput.toString());
        } else {
            n = ((zero.repeat(width - length)) + numberOutput.toString());
        }
    }
    return n;

}
