var SV_DEFAULTPAGE = "3.3.91";

/*
 *  Inicializaciones globales y utilitarios
 */

if (typeof ($) != "function") {
    sntAlert('Biblioteca JQuery no pudo ser cargada o no esta activa');
}

/*
 * Inicializaciones ready() de JQuery
 */

/* ============================

Es una mala práctica detectar el navegador para determinar los flujos según las funciones disponibles.
navigator.appName esta deprecado, info: https://developer.mozilla.org/en-US/docs/Web/API/NavigatorID/appName

Actualmente appName retorna siempre 'Netscape' que es lo que se acordó entre la comunidad.

// Distinguir si se está operando con MSIE, FF, etc
// var MSIE = navigator.appName.match(/explorer/i);
// var NSS = navigator.appName.match(/netscape/i);

*Se debe detectar si existe la característica*

Momentaneamente se implementará una función para realizar esta misma función.
Se trabajará más adelante para modificar este archivo para reemplazar la detección de navegador.

===============================*/

// Entrega el nombre del navegador y su versión
navigator.miNavegador = (function () {
    var ua = navigator.userAgent, tem,
        M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return 'IE ' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\b(OPR|Edge)\/(\d+)/);
        if (tem !== null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) !== null) M.splice(1, 1, tem[1]);
    return M.join(' ');
})();

var MSIE = navigator.miNavegador.match(/ie/i) ? true : false;
var NSS = !navigator.miNavegador.match(/ie/i) ? true : false;
if (NSS) document.captureEvents(Event.KEYPRESS);

$(document).ready(function () {

    // Cambiar el cursor a mano (como en los links) para botones sonda.net
    $('.boton')
        .mouseover(function () {
            $(this).css('cursor', 'pointer');
        });

    menu_onload();
    verificar_volver();
    fixValidatorProperties();

    sntHideClock();
});


function verificar_volver() {
    var volver = true;
    if (typeof (SONDANET_PermitirVolverJavascript) != "undefined")
        if (SONDANET_PermitirVolverJavascript) {
            volver = false;
        }
    if (volver) {
        history.go(1);
    }
}

function menu_onload() {
    // Esta funcion fuerza la recarga de los menus en cada actualizacion de
    // p�gina. Esto ya que bajo ciertas condiciones los menus no se recargan
    if (parent) {
        try {
            fr = parent.frames;
            var i = 0;
            var j = 0;

            for (i = 0; i < fr.length; i++) {
                if (typeof (fr[i].arregloMenus) != "undefined") {
                    var am = fr[i].arregloMenus;
                    for (j = 0; j < am.length; j++) {
                        try {
                            eval('fr[' + i + '].Go_' + am[j].id + '();');
                        } catch (e) { }
                    }
                }
            }
        } catch (e) { }
    }
}


/*
 *	DY 20080111
 *
 *  Escapa todos los caracteres html de composici�n (i.e. <, > y &) de manera
 *  de evitar la "excepci�n" 'Se detect� un posible valor Request.Form
 *  peligroso en el cliente' que genera .net tras uns postback.
 */
function escapar_caracteres_html() {
    elementos = document.getElementsByTagName('*');
    for (i = 0; i < elementos.length; i++) {
        if (elementos[i].type == 'text' || elementos[i].type == 'textarea') {
            v = elementos[i].value;
            v = v.replace(/&/g, "&amp;"); // &amp;	%26
            v = v.replace(/</g, "&lt;"); // &lt;	 %3c
            v = v.replace(/>/g, "&gt;"); // &gt;	 %3e
            elementos[i].value = v;
        }
    }
}


// Permite traducir un numero a una fecha, agregando el separador "/",
// dependiendo del formato
function NumeroAFecha(formato, numero) {
    if (formato == "DDMMAAAA" && numero.match(/\d{7,8}/)) {
        numero = numero.replace(/(\d{1,2})(\d{2})(\d{4})/, "$1/$2/$3");
    } else if (formato == "MMAAAA" && numero.match(/\d{5,6}/)) {
        numero = numero.replace(/(\d{1,2})(\d{4})/, "$1/$2");
    }
    return numero;
}


function ValidarFecha(val) {
    var ctv = sntControlToValidate(val);
    var value = ValidatorGetValue(ctv);
    var rv = $(val).attr("RV").toUpperCase();
    var tf = $(val).attr("TipoFecha").toUpperCase();
    if (value.length === 0 && rv.toUpperCase() == "FALSE") return true;

    value = NumeroAFecha(tf, value);

    var valor = FormatoFecha(value, tf);

    if (valor !== false) {
        control = $("#" + ctv)[0];
        control.value = valor;
        return true;
    } else return false;
}

function FormatoFecha(value, tf) {
    return FormatoFechaArray(value, tf, null);
}

function FormatoFechaArray(value, tf, auxArr) {
    var cGuion = 0;
    var cSlash = 0;
    var cDptos = 0;
    var cSpace = 0;
    var sSep = "";
    var sConstSep = "/";

    // auxArr debe declararse afuera como un arreglo que corresponde a:
    // en este arreglo se devolveran los valores determinados por la funcion.
    //var auxArr = [0,0,0,0,0,0]; //Ano,Mes,Dia,Hora,Minuto,Segundo

    switch (tf.toUpperCase()) {
        case "DDMMAAAA":
            value = DelChar(value, " ");

            for (i = 0; i < value.length; i++) {
                var inner = value.substr(i, 1);
                if (inner == "-") cGuion++;
                if (inner == "/") cSlash++;
            }

            if (cSlash > 0 && cGuion > 0 || cSlash === 0 && cGuion == 0) return false;

            if (cSlash > 0) sSep = "/";
            if (cGuion > 0) sSep = "-";

            vector = value.split(sSep);

            if (parseInt(vector[0]) < 10) {
                vector[0] = "0" + Number(vector[0]);
            }
            if (parseInt(vector[1]) >= 0 && parseInt(vector[1]) < 10) {
                vector[1] = "0" + Number(vector[1]);
            }

            vector[2] = "" + Number(vector[2]);
            if (parseInt(vector[2]) > 79 && parseInt(vector[2]) < 100) {
                vector[2] = "19" + Number(vector[2]);
            }
            if (parseInt(vector[2]) > 0 && parseInt(vector[2]) < 80) {
                s = "";
                if (parseInt(vector[2]) < 10) s = "0";
                vector[2] = "20" + s + Number(vector[2]);
            }

            var yy = Number(vector[2]);
            var mm = Number(vector[1]);
            var dd = Number(vector[0]);

            if (auxArr != null) {
                auxArr[0] = yy;
                auxArr[1] = mm;
                auxArr[2] = dd;
            }

            mm--;
            var date = new Date(yy, mm, dd, 1, 30);
            var res = (typeof (date) == "object" && yy == date.getFullYear() && mm == date.getMonth() && dd == date.getDate()) ? true : false;

            if (res) var r = vector[0] + sConstSep + vector[1] + sConstSep + vector[2];
            else var r = false;
            return r;
            break;

        case "DDMMAAAAHHMMSS":
            value = $.trim(value);
            for (i = 0; i < value.length; i++) {
                var inner = value.substr(i, 1);
                if (inner == "-") cGuion++;
                if (inner == "/") cSlash++;
                if (inner == ":") cDptos++;
                if (inner == " ") cSpace++;
            }
            if (cSlash > 0 && cGuion > 0 || cSlash === 0 && cGuion == 0) return false;
            if (cDptos > 2) return false;
            if (cSpace > 1) return false;

            if (cSlash > 0) sSep = "/";
            if (cGuion > 0) sSep = "-";

            var tmpVec = value.split(" ");
            var fecha = tmpVec[0];
            var hora = tmpVec[1];
            if (hora == null) hora = "00:00:00";
            var vectorhor = hora.split(":");
            //hora
            i = vectorhor.length;
            if (i == 1) {
                ho = vectorhor[0];
                mi = 0;
                se = 0;
            }
            if (i == 2) {
                ho = vectorhor[0];
                mi = vectorhor[1];
                se = 0;
            }
            if (i == 3) {
                ho = vectorhor[0];
                mi = vectorhor[1];
                se = vectorhor[2];
            }
            if (isNaN(ho)) return false;
            if (isNaN(mi)) return false;
            if (isNaN(se)) return false;

            if (Number(ho) < 0 || Number(ho) > 23) return false;
            if (Number(mi) < 0 || Number(mi) > 59) return false;
            if (Number(se) < 0 || Number(se) > 59) return false;
            ho = parseInt(ho) < 10 ? "0" + Number(ho) : ho;
            mi = parseInt(mi) < 10 ? "0" + Number(mi) : mi;
            se = parseInt(se) < 10 ? "0" + Number(se) : se;

            //fecha
            var vectorfec = fecha.split(sSep);

            if (parseInt(vectorfec[0]) < 10) {
                vectorfec[0] = "0" + Number(vectorfec[0]);
            }
            if (parseInt(vectorfec[1]) >= 0 && parseInt(vectorfec[1]) < 10) {
                vectorfec[1] = "0" + Number(vectorfec[1]);
            }
            vectorfec[2] = "" + Number(vectorfec[2]);
            if (parseInt(vectorfec[2]) > 79 && parseInt(vectorfec[2]) < 100) {
                vectorfec[2] = "19" + Number(vectorfec[2]);
            }
            if (parseInt(vectorfec[2]) > 0 && parseInt(vectorfec[2]) < 80) {
                s = "";
                if (parseInt(vectorfec[2]) < 10) s = "0";
                vectorfec[2] = "20" + s + Number(vectorfec[2]);
            }

            var yy = Number(vectorfec[2]);
            var mm = Number(vectorfec[1]);
            var dd = Number(vectorfec[0]);

            if (auxArr !== null) {
                auxArr[0] = yy;
                auxArr[1] = mm;
                auxArr[2] = dd;
                auxArr[3] = ho;
                auxArr[4] = mi;
                auxArr[5] = se;
            }

            mm--;
            tmphor = Number(ho);
            tmpmin = Number(mi);
            tmpseg = Number(se);

            var date = new Date(yy, mm, dd, 1, 30);
            var res = (typeof (date) == "object" && yy == date.getFullYear() && mm == date.getMonth() && dd == date.getDate()) ? true : false;

            if (res) var r = vectorfec[0] + sConstSep + vectorfec[1] + sConstSep + vectorfec[2] + " " + ho + ":" + mi + ":" + se;
            else var r = false;
            return r;
            break;

        case "MMAAAA":
            value = DelChar(value, " ");
            for (i = 0; i < value.length; i++) {
                var inner = value.substr(i, 1);
                if (inner == "-") cGuion++;
                if (inner == "/") cSlash++;
            }
            if (cSlash > 0 && cGuion > 0 || cSlash == 0 && cGuion == 0) return false;

            if (cSlash > 0) sSep = "/";
            if (cGuion > 0) sSep = "-";

            vector = value.split(sSep);
            i = vector.length;
            if (i != 2) return false;

            if (parseInt(vector[0]) >= 0 && parseInt(vector[0]) < 10) {
                vector[0] = "0" + Number(vector[0]);
            }

            vector[1] = "" + Number(vector[1]);
            if (parseInt(vector[1]) > 79 && parseInt(vector[1]) < 100) {
                vector[1] = "19" + Number(vector[1]);
            }
            if (parseInt(vector[1]) > 0 && parseInt(vector[1]) < 80) {
                s = "";
                if (parseInt(vector[1]) < 10) s = "0";
                vector[1] = "20" + s + Number(vector[1]);
            }


            var yy = Number(vector[1]);
            var mm = Number(vector[0]);
            var dd = 01;

            if (auxArr != null) {
                auxArr[0] = yy;
                auxArr[1] = mm;
                auxArr[2] = dd;
            }

            mm--;
            var date = new Date(yy, mm, dd, 1, 30);
            var res = (typeof (date) == "object" && yy == date.getFullYear() && mm == date.getMonth() && dd == date.getDate()) ? true : false;

            if (res) var r = vector[0] + sConstSep + vector[1];
            else var r = false;
            return r;
            break;
    }
    return false;
}


function fmtfec(control, tf) {
    if (control.value.length === 0) return;
    var valor = FormatoFecha(control.value, tf.toUpperCase());
    if (valor !== false) control.value = valor;
}

function ValidarRut(val) {
    var ctv = sntControlToValidate(val);
    var value = ValidatorGetValue(ctv);
    value = FormatoRut(value); //FormatoRut
    var valorFinal = value;
    var rv = $(val).attr("RV").toUpperCase();
    if (value.length === 0 && rv.toUpperCase() == "FALSE") return true;
    var tmpvec = value.split("-");
    if (tmpvec.length < 2) return false;
    var rut = DelChar(tmpvec[0], ".");
    var drut = tmpvec[1];
    var douSuma = 0.0000;
    var x = 0;
    var intMultiplo = 0;

    if (rut.length < 1 || drut.length < 1) return false;

    intMultiplo = 2;
    for (x = 0; x < rut.length; x++) {
        douSuma = douSuma + (Number(rut.substr(rut.length - 1 - x, 1)) * intMultiplo);
        intMultiplo++;
        if (intMultiplo == 8) intMultiplo = 2;
    }
    var res = 11 - (douSuma % 11);

    switch (res) {
        case 11:
            var dc = "0";
            break;
        case 10:
            var dc = "K";
            break;
        default:
            var dc = res.toString();
    }
    if (dc.toUpperCase() == drut.toUpperCase()) {
        var ctv = sntControlToValidate(val);
        control = $("#" + ctv)[0];
        control.value = valorFinal;
        return true;
    } else {
        var ctv = sntControlToValidate(val);
        control = $("#" + ctv)[0];
        control.value = valorFinal;
        return false;
    }
} //function ValidarRut(val){

function FormatoRut(value) {
    if (value.length < 2) return value;
    value = DelChar(value, " ");
    if (value.indexOf("-") != -1) {
        if (value.indexOf("-") < value.length - 1) {
            value = value.substr(0, value.indexOf("-") + 2);
        } else {
            if (value.indexOf("-") == value.length - 1) {
                value = value.substr(0, value.indexOf("-"));
            }
        }
    }
    value = DelChar(value, ".");
    value = DelChar(value, "-");
    for (i = 0; i < value.length - 1; i++) {
        var a = value.substr(i, 1);
        if ("0123456789".indexOf(a) == -1) return value;
    }
    var a = value.substr(value.length - 1, 1);
    if ("0123456789Kk".indexOf(a) == -1) return value;
    for (i = 0; value.substr(i, 1) == "0"; i = i * 1) value = value.substr(1, value.length - 1);
    var newvalue = "";
    if (value.length - 1 > 3) {
        var tmpnum = (value.length - 1) % 3;
        var desfaz = 0;
        if (tmpnum !== 0) {
            for (i = 0; i < tmpnum; i++) newvalue += value.substr(i, 1);
            newvalue += ".";
            desfaz = i;
        }
        var cnt = 0;
        for (i = desfaz; i < value.length - 1; i++) {
            newvalue += value.substr(i, 1);
            cnt++;
            if (cnt % 3 === 0 && value.length - cnt > 3) newvalue += ".";
        }
    } else {
        newvalue = value.substr(0, value.length - 1);
    }
    newvalue += "-";
    newvalue += value.substr(value.length - 1, 1);
    return newvalue;
} //function FormatoRut(value){

function desfmtrut(control) {
    var value = control.value;
    if (value.indexOf(".") == -1) return; // && value.indexOf("-")==-1)
    value = DelChar(value, "."); //value=DelChar(value,"-");
    control.value = value;

    $(control).selectRange(0, value.length);
    // var rangeRef = control.createTextRange();
    // rangeRef.move("character", value.length);
    // rangeRef.select();

} //function desfmtrut(control){

function fmtrut(control) {
    if (control.value.length === 0) return;
    control.value = FormatoRut(control.value);
} //function fmtrut(control){

function DelChar(value, car) {
    if (value.length === 0) return value;
    while (value.indexOf(car) > -1) value = value.indexOf(car) === 0 ? value.substr(1) : (value.indexOf(car) == value.length - 1 ? value.substr(0, value.length - 1) : value.substr(0, value.indexOf(car)) + value.substr(value.indexOf(car) + 1));
    return value;
} //function DelChar(value,car){

function trim(value, car) {
    if (value.length == 0) return value;
    while (value.substr(0, 1) == car) value = value.substr(1);
    while (value.substr(value.length - 1, 1) == car) value = value.substr(0, value.length - 1);
    return value;
} //function trim(value,car){

var DefaultButtonColor;

function disbut(button) {

    if (!window.document.body.submitted) {
        window.document.body.submitted = true;

        sntShowClock();

        var i;
        var a;
        a = window.document.body.getElementsByTagName("INPUT");
        for (i = 0; i < a.length; i++) {
            if (a[i].type) {
                if (a[i].type.toLowerCase() == "submit") {
                    DefaultButtonColor = a[i].style.color;
                    a[i].style.color = "Gray";
                }
            }
        }
        return true;
    } else {
        return false;
    }

}

function undisbut(button) {

    window.document.body.submitted = false;

    sntHideClock();

    var i;
    var a;
    a = window.document.body.getElementsByTagName("INPUT");
    for (i = 0; i < a.length; i++) {
        if (a[i].type) {
            if (a[i].type.toLowerCase() == "submit") {
                a[i].style.color = DefaultButtonColor;
            }
        }
    }


}


function rutValidar(val) { //para RutSnd

    var ctv = sntControlToValidate(val);
    var valor = ValidatorGetValue(ctv);
    var rv = $("#" + ctv).attr("RV").toUpperCase();

    valor = valor.replace(/\s/g, '');

    // si no es requerido, puede estar vac�o
    if (valor.length === 0) {
        return (rv == 'FALSE');
    }

    // si no est� vac�o, entonces se valida via regex
    // regex para un RUT con . y - opcionales
    var reRut = /^0*(\d{1,3}(\.?\d{3})*)\-?([\dkK])$/;

    if (!reRut.test(valor)) {
        setTimeout(function () {
            sntAlert("Formato de RUT inválido", ctv);
        }, 100);
        // var res = sntAlert("Formato de RUT inválido", ctv);
        // return res;
    } else {
        var rut = reRut.exec(valor)[1].replace(/\./g, '');
        var drut = reRut.exec(valor)[3];
        var douSuma = 0.0000;
        var x = 0;
        var intMultiplo = 0;
        intMultiplo = 2;
        for (x = 0; x < rut.length; x++) {
            douSuma = douSuma + (Number(rut.substr(rut.length - 1 - x, 1)) * intMultiplo);
            intMultiplo++;
            if (intMultiplo == 8) intMultiplo = 2;
        }
        var res = 11 - (douSuma % 11);
        switch (res) {
            case 11:
                var dc = "0";
                break;
            case 10:
                var dc = "K";
                break;
            default:
                var dc = res.toString();
        }
        if (dc.toUpperCase() == drut.toUpperCase()) {
            control = $('#' + sntControlToValidate(val))[0];
            control.value = addmilsep(rut, ".", 3) + "-" + drut.toUpperCase();
            return true;
        } else {
            var res = sntAlert("Rut invalido", ctv);
            return res;
        }
    }
} //function rutValidar(val){


function FechaValidar(val) { //para FechaSnd
    var ctv = sntControlToValidate(val);
    var valor = ValidatorGetValue(ctv);
    var tmpvalor = valor;
    var formato = $(val).attr("formato").toUpperCase();

    valor = $.trim(valor);
    var sep = "/";
    if (valor.length === 0) return true;
    var s = "0123456789: " + sep;
    for (i = 0; i < valor.length; i++) {
        if (s.indexOf(valor.substr(i, 1)) == -1) {
            sntAlert("Caracteres Invalidos para Fecha."); //0 "Caracteres Invalidos para Fecha."
            return false;
        }
    }

    /// INI - Dinko
    valor = NumeroAFecha(formato, valor);
    /// FIN - Dinko

    formato = formato.toUpperCase();
    if (formato == "MMAAAA") formato = "MM/AAAA";
    if (formato == "DDMMAAAA") formato = "DD/MM/AAAA";
    if (formato == "DDMMAAAAHHMMSS") formato = "DD/MM/AAAA/hh:mm:ss";

    if ((formato == "MM/AAAA" || formato == "DD/MM/AAAA") && valor.indexOf(sep) == -1) {
        switch (formato) {
            case 'MM/AAAA':
                if (valor.length != 6) break;
                valor = valor.substr(0, 2) + sep + valor.substr(3);
                break;
            case 'DD/MM/AAAA':
                if (valor.length != 8) break;
                valor = valor.substr(0, 2) + sep + valor.substr(2, 2) + sep + valor.substr(4);
                break;
        }
    }

    if (valor.indexOf(sep) == -1) {
        sntAlert("No Posee Separador de Fecha Valido."); //1 "No Posee Separador de Fecha Valido."
        return false;
    }

    var f = valor.split(sep);
    if (typeof (f[0]) != "undefined") f[0] = $.trim(f[0]);
    if (typeof (f[1]) != "undefined") f[1] = $.trim(f[1]);
    if (typeof (f[2]) != "undefined") f[2] = $.trim(f[2]);

    var ff = formato.split(sep);
    var cntfmt = ff.length;

    switch (cntfmt) {
        case 2:
            var dd = 1;
            var mm = Number(f[0]);
            var yy = Number(f[1]);
            if (dd < 1 || mm < 1 || yy < 1) {
                sntAlert("Ingreso Invalido para Fecha.");
                return false;
            }
            break;
        case 3:
            var dd = Number(f[0]);
            var mm = Number(f[1]);
            var yy = Number(f[2]);
            if (dd < 1 || mm < 1 || yy < 1) {
                sntAlert("Ingreso Invalido para Fecha.");
                return false;
            }
            break;
        case 4:
            var dd = Number(f[0]);
            var mm = Number(f[1]);
            var yy = Number(f[2]);

            if (isNaN(f[0]) || isNaN(f[1])) { //no numericos
                sntAlert("Ingreso Invalido para Fecha.");
                return false;
            }

            if (isNaN(yy)) { //no es numerico
                if (typeof (f[2]) == 'string') {
                    if (f[2].indexOf(" ") > -1) {
                        if (f[2].indexOf(" ") != f[2].lastIndexOf(" ")) {
                            var tmpstr = f[2].substr(0, f[2].indexOf(" ") + 1);
                            f[2] = f[2].substr(f[2].indexOf(" ") + 1);
                            var i = 0;
                            for (i = 0; i < f[2].length; i++)
                                if (f[2].substr(i, 1) != " ") tmpstr += f[2].substr(i, 1);
                            var s = tmpstr.split(" ");
                        } else var s = f[2].split(" ");
                    } else {
                        sntAlert("Ingreso Invalido para Fecha.");
                        return false;
                    }
                    if (!isNaN(s[0])) {
                        yy = Number(s[0]);
                        if (!isNaN(s[1])) //es numerico
                            f[3] = Number(s[1]);
                        else f[3] = s[1];
                    } else {
                        sntAlert("Ingreso Invalido para Fecha.");
                        return false;
                    }

                } else {
                    sntAlert("Ingreso Invalido para Fecha.");
                    return false;
                }
            }

            var hrs = f[3];
            if (typeof (f[3]) != "undefined") {
                if (!isNaN(hrs)) { //es numerico
                    hrs = "" + hrs + "";
                    if (hrs.length == 1) {
                        hrs = "0" + hrs + ":00:00";
                    } else {
                        if (Number(hrs.substr(0, 1)) > 2) hrs = hrs + "0";
                        var a = hrs + "00000";
                        a = a.substr(0, 6);
                        hrs = "";
                        var j = 0;
                        for (j = 0; j < 3; j++) {
                            if (hrs != "") hrs = hrs + ":";
                            hrs = hrs + a.substr(0, 2);
                            a = a.substr(2);
                        }
                    }
                }
                var h;
                if (hrs.indexOf(":") > -1) h = hrs.split(":");
                else {
                    if (!isNaN(f[3])) { //es numerico
                        var a = Number(f[3]);
                        if (a < 0 || a > 23) var s = a + ":0:0";
                        else var s = "0:0:0";
                    } else {
                        var s = "0:0:0";
                    }
                    h = hrs.split(":");
                }
                switch (h.length) {
                    case 1:
                        h[1] = "00";
                        h[2] = "00";
                        var ss = h[0] + ":" + h[1] + h[2];
                        break;
                    case 2:
                        h[2] = "00";
                        var ss = h[0] + ":" + h[1] + h[2];
                        break;
                    case 3:
                        var ss = h[0] + ":" + h[1] + h[2];
                        break;
                    default:
                        var ss = h[0] + ":" + h[1] + h[2];
                }
                var hh = Number(h[0]);
                var mi = Number(h[1]);
                var ss = Number(h[2]);
                if ((hh < 0 || hh > 23) || (mi < 0 || mi > 59) || (ss < 0 || ss > 59)) {
                    sntAlert("Ingreso Invalido para hora.");
                    return false;
                }
            } else {
                var hh = "00";
                var mi = "00";
                var ss = "00";
            }
            break;
        default:
            sntAlert("Ingreso Invalido para Fecha.");
            return false;
    }

    mm--;
    if (Number(yy) < 10) yy = "0" + Number(yy);
    if (Number(yy) > 79 && yy < 100) yy = "19" + yy;
    if (Number(yy) < 80) yy = "20" + yy;


    // INI - Dinko - 2007-04-25
    // Se cambia creacion de fecha por recomendacion de Hugo Bravo y Sergio
    // Ceriche, por problema de validacion de fechas en intervalos de cambios
    // de horario (para fechan en viernes-sabado de 2da semana octubre) Penta
    var date = new Date(yy, mm, dd, 1, 30);
    // original%: var date = new Date(yy, mm, dd);
    // FINI  Dinko - 2007-04-25
    var r = (typeof (date) == "object" && yy == date.getFullYear() && mm == date.getMonth() && dd == date.getDate()) ? true : false;
    if (!r) {
        switch (formato) {
            case "MM/AAAA":
                sntAlert("Periodo " + tmpvalor + " no Valido.");
                break;
            case "DD/MM/AAAA":
                sntAlert("Fecha " + tmpvalor + " no Valida.");
                break;
            case "DD/MM/AAAA/hh:mm:ss":
                sntAlert("Fecha " + tmpvalor + " no Valida.");
                break;
        }
        return false;
    }
    mm++;

    var d = (Number(dd) < 10) ? "0" + Number(dd) : Number(dd);
    var m = (Number(mm) < 10) ? "0" + Number(mm) : Number(mm);
    var a = yy;
    switch (cntfmt) {
        case 2:
            control = $('#' + sntControlToValidate(val))[0];
            control.value = m + sep + a;
            break;
        case 3:
            control = $('#' + sntControlToValidate(val))[0];
            control.value = d + sep + m + sep + a;
            break;
        case 4:
            var hhh = (Number(hh) < 10) ? "0" + Number(hh) : Number(hh);
            var min = (Number(mi) < 10) ? "0" + Number(mi) : Number(mi);
            var seg = (Number(ss) < 10) ? "0" + Number(ss) : Number(ss);
            control = $('#' + sntControlToValidate(val))[0];
            control.value = d + sep + m + sep + a + " " + hhh + ":" + min + ":" + seg;
            break;
    }
    return true;
} //function FechaValidar(ctrl,formato){

function kptxt(ctrl, evt, tc) {
    var charCode = (window.Event) ? evt.which : evt.keyCode;
    charCode = (tc == 'M') ? (charCode >= 97 && charCode <= 122) ? charCode = charCode - 32 : charCode : (tc == 'm') ? (charCode >= 65 && charCode <= 90) ? charCode = charCode + 32 : charCode : charCode;
    if (window.Event) evt.which = charCode;
    else try {
        evt.keyCode = charCode;
    } catch (e) { };
    return true;
} //kptxt(ctrl,evt,tc,efrom)

function kpfec(ctrl, evt, efrom) {
    var valor = ctrl.value;
    var charCode = (window.Event) ? evt.which : evt.keyCode;
    switch (efrom) {
        case "ku":
            if (charCode != 8) return;
            break;
        case "kp":
            if (charCode > 31 && (!(charCode >= 47 && charCode <= 58 || charCode == 32))) {
                charCode = 0;
                return false;
            }
            break;
    } //switch
    return;
} //function kpfec(ctrl,evt,efrom){

function kpnum(ctrl, evt, milsep, decsep, numdec, efrom) { //para NumeroSnd
    var valor = ctrl.value;
    var charCode = (window.Event) ? evt.which : evt.keyCode;
    switch (efrom) {
        case "ku":
            if (charCode != 8) return;
            var strdec = "";
            var strent = "";
            valor = DelChar(valor, milsep);
            valor = DelChar(valor, decsep);
            if (numdec > 0) {
                while (valor.length < numdec) valor = "0" + valor;
                strdec = valor.substr(valor.length - numdec);
                strent = valor.substr(0, valor.length - numdec);
                strent = addmilsep(strent, milsep, 3);
                strdec = decsep + strdec;
            } else {
                strent = addmilsep(valor, milsep, 3);
            }
            ctrl.value = strent + strdec;
            break;
        case "kp":
            if (charCode != 43 && charCode != 45 && charCode > 31 && (!(charCode >= 48 && charCode <= 57))) {
                charCode = 0;
            }
            if (charCode == 45) {
                if (valor.indexOf("-") == -1) valor = "-" + valor;
                charCode = 0;
            }
            if (charCode == 43) {
                if (valor.indexOf("-") > -1) valor = valor.substr(1, valor.length - 1);
                charCode = 0;
            }
            if (charCode > 0) {
                valor = valor + String.fromCharCode(charCode);
                charCode = 0;
            }
            ctrl.value = "" + formato(valor, milsep, decsep, numdec);
            return false;
            break;
    } //switch
    return;
} //function kpnum(ctrl,evt,milsep,decsep,numdec,efrom)


function formato(valor, milsep, decsep, numdec) {
    var strdec = "";
    var strent = "";
    var strsng = "";

    if (valor.indexOf("-") > -1) {
        valor = valor.substr(1, valor.length - 1);
        strsng = "-";
    }

    if (valor == "") {
        if (numdec > 0) {
            return "0" + decsep + replichar(numdec - 1, "0");
        }
    } else {
        valor = DelChar(valor, milsep);
        valor = DelChar(valor, decsep);
        if (numdec > 0) {
            valor += '0';
            strdec = valor.substr(valor.length - numdec);
            strent = valor.substr(0, valor.length - numdec);
            strent = addmilsep(strent, milsep, 3);
            strdec = decsep + strdec.substr(0, strdec.length - 1);
        } else {
            if (valor != "") valor += '0';
            strent = addmilsep(valor, milsep, 3);
            strent = strent.substr(0, strent.length - 1);
            strdec = "";
        }
        return strsng + strent + strdec;
    }
    return ""
} //function formato(valor,milsep,decsep,numdec){


function addmilsep(valor, milsep, d) {
    var valorf = "";
    var j = 0;
    if (valor == "" || Number(valor) == 0) return "0";
    valor = "" + Number(valor);
    for (i = valor.length - 1; i >= 0; i--) {
        valorf = valor.substr(i, 1) + valorf;
        j++;
        if (j == d) {
            valorf = milsep + valorf;
            d = 3;
            j = 0;
        }
    }
    if (valorf.indexOf(milsep) == 0) valorf = valorf.substr(1, valorf.length);
    return valorf;
} //function addmilsep(valor,milsep,d){


function replichar(num, car) {
    var i = 0;
    var d = "";
    for (i = 0; i < num; i++) {
        d += car;
    }
    return d;
} //function replichar(num,car){

/*var tituloVentanaModal="undefined";
if (typeof(dialogArguments) != "undefined")
{
	if (typeof(dialogArguments.tituloVentanaModal) != "undefined"){
		document.title=dialogArguments.tituloVentanaModal;
		dialogArguments.tituloVentanaModal="undefined";
	}
}*/


function wc() {
    window.returnValue = 'ok';
    sntWindowClose();
}

function obnum(ctrl, milsep, decsep, numdec) { //para NumeroSnd
    ctrl.value = "" + formato(ctrl.value, milsep, decsep, numdec);
}

function DataGridSnd_VerticalTab() {
    var lastTabindex = 1;
    if (typeof (arrayDataGridSndVerticalTab) != "undefined") {
        for (ii = 0; ii < arrayDataGridSndVerticalTab.length; ii++) {
            var g = arrayDataGridSndVerticalTab[ii];
            var t = document.getElementById(g);
            if (t != null) {
                if (t.rows.length > 1) {
                    var i = 0,
                        j = 0,
                        k = 0,
                        l = 0,
                        m = 0;
                    for (i = 0; i < window.document.forms[0].elements.length; i++)
                        if (window.document.forms[0].elements[i].id.indexOf(g) > -1 && window.document.forms[0].elements[i].id != g) j++;
                    if (j > 0) {
                        var cc = new Array(j);
                        var c = -1;
                        for (i = 0; i < window.document.forms[0].elements.length; i++) {
                            if (window.document.forms[0].elements[i].id.indexOf(g) > -1 && window.document.forms[0].elements[i].id != g) {
                                c++;
                                cc[c] = window.document.forms[0].elements[i].id;
                            }
                        }
                        k = (c + 1) / (t.rows.length - 2);
                        document.getElementById(cc[0]).tabIndex = lastTabindex;
                        for (i = 1; i < j; i++) {
                            if (i % k != 0) {
                                document.getElementById(cc[i]).tabIndex = document.getElementById(cc[i - 1]).tabIndex + t.rows.length - 2;
                            }
                            if (i % k == 0) {
                                if (i - k < 0) m = 0;
                                else m = i - k;
                                document.getElementById(cc[i]).tabIndex = document.getElementById(cc[m]).tabIndex + 1;
                            }
                            lastTabindex = document.getElementById(cc[i]).tabIndex;
                        }
                        lastTabindex++;
                    }
                }
            }
        }
    }
}



/* Control Hyperlink/Busqueda */

/*
 * Al ser invocada desde una ventana popup, la siguiente rutina ajusta el
 * tama�o y ubicaci�n de la ventana flotante acorde a las dimensiones de su
 * contenido, pero s�lo para la dimensi�n en que se haya especificado cero
 */
function autoAjustarTamanoVentana(w, h) {
    // Se determina ancho, alto, top y left �ptimos
    if (w == 0) w = document.forms[0].offsetWidth + 28;
    if (w > screen.availWidth) w = screen.availWidth;
    if (h == 0) h = document.forms[0].offsetHeight + 28;
    if (h > screen.availHeight) h = screen.availHeight;
    if (w == screen.availWidth && h < screen.availHeight) h += 20;
    if (h == screen.availHeight && h < screen.availWidth) w += 20;
    x = Math.round((screen.availWidth - w) / 2);
    y = Math.round((screen.availHeight - h) / 2);

    // Se mueve y resizea ventana
    w0 = document.body.offsetWidth;
    h0 = document.body.offsetHeight;
    if (window.opener) { // popup no-modal
        window.resizeTo(w, h);
        window.moveTo(x, y);
    } else { // popup modal
        parent.window.dialogWidth = w + "px";
        //if (w == screen.availWidth) {}
        parent.window.dialogHeight = h + "px";
        parent.window.dialogLeft = x + "px";
        parent.window.dialogTop = y + "px";
    }
}


/*
 *  Esta funcion es para uso de control HyperlinkBusqueda. Permite asignar
 *  los values de un conjunto de controles del formulario padre
 *	desde la ventana de seleccion.
 *
 *	  dialogArguments:	Referencia al documento padre, que contiene los
 *						  controles a modificar (generalmente no definido)
 *	  arrControles:	   Arreglo con los id de los controles a modificar
 *	  arrValores:		 Arreglo con valores a asignar a los controles
 *	  UniqueID:		   ID del control al que se har� postback en el
 *						  formulario padre una vez que se han asignado los
 *						  valores a los controles
 */
function asignarControlesPadre(dialogArguments, arrControles, arrValores, UniqueID) {

    var legacy = true;

    if (typeof (SONDANET_HabilitarPopupCSS) != "undefined") {

        var topWnd = window.top;

        legacy = false;

        var windowElement = topWnd.$('div.sntPopupWindow:last');
        var iframeDomElement = windowElement.children("iframe")[0];
        var iframeWindowObject = iframeDomElement.contentWindow;
        var iframeDocumentObject = iframeDomElement.contentDocument;

        // GCC 20170214
        // dialogArguments = topWnd.$(iframeWindowObject.frameElement).data("dialogArguments");

        // GCC 20170214
        //var w = dialogArguments["parentWindow"];
        var w = topWnd.$(iframeWindowObject.frameElement).data("parentWindow");


        for (var i = 0; i < arrControles.length; i++) {
            var ctl = w.$("#" + arrControles[i]);
            if (ctl.lenght > 0) {
                sntAlert('control "' + arrControles[i] + '" no pudo ser encontrado ' +
                    'en la pagina padre');
                return false;
            }
            ctl.val(arrValores[i]);
            try {
                enfocar(ctl[0].parentElement)
            } catch (e) { };
            try {
                enfocar(ctl[0])
            } catch (e) { };
        }

        if (UniqueID.length && arrControles.length) {
            aux = 'w.__doPostBack("' + UniqueID.toString() +
                '","AlSeleccionar")';
            eval(aux);
        }

    }

    if (legacy == true) {
        if (typeof (dialogArguments) == "undefined") {
            dialogArguments = opener;
        }

        for (var i = 0; i < arrControles.length; i++) {
            var ctl = dialogArguments.document.getElementById(arrControles[i]);
            if (!ctl) {
                sntAlert('control "' + arrControles[i] + '" no pudo ser encontrado ' +
                    'en la pagina padre');
                return false;
            }
            ctl.value = arrValores[i];
            try {
                enfocar(ctl.parentElement)
            } catch (e) { };
            try {
                enfocar(ctl)
            } catch (e) { };
        }

        if (UniqueID.length && arrControles.length) {
            aux = 'dialogArguments.__doPostBack("' + UniqueID.toString() +
                '","AlSeleccionar")';
            eval(aux);
        }
    }




}


/*
 * Enfoque "seguro" de control (incluso cuando est� disabled)
 */
function enfocar(control) {
    if (control) {
        try {
            var disabled = control.disabled;
            control.disabled = 0;
            control.focus();
            control.disabled = disabled;
        } catch (e) { };
    }
}

/* Fin Control Hyperlink/Busqueda ------------------------------------------ */



/****************************************************
 *  Control Numero
 ****************************************************/


/*
 *  Prototipo String - trim
 *  DY 20050530
 *
 *  Borra los caracteres espacios (tab, nulo, espacio, etc.) del comienzo y
 *  final de la cadena
 *
 *  Ej:  "  \rhola   \t \n ".trim() -> "hola"
 */
String.prototype.trim = function () {
    return (this.replace(/^\s+/, "").replace(/\s+$/, ""));
}


/*
 *  Prototipo String - reverse
 *  DY 20050530
 *
 *  Invierte un string, letra a letra
 *
 *  Ej: "Amor a Roma".reverse() -> "amoR a romA"
 */
String.prototype.reverse = function () {
    var s = "";
    var i = this.length;
    while (i > 0) {
        s += this.substring(i - 1, i);
        i--;
    }
    return s;
}


/*
 *  Prototipo String - reEscape
 *  DY 20080125
 *
 *  Escapa los caracteres especiales como el "." para que se tomen
 *	literalmente en una expresion regular
 *
 *  Ej: " * hola.q.tal * ".reEscape() ->  \* hola\.q\.tal \*
 */
String.prototype.reEscape = function () {
    s = this.toString();
    s = s.replace(/\\/g, "\\\\");
    s = s.replace(/\./g, "\\.");
    s = s.replace(/\*/g, "\\*");
    s = s.replace(/\+/g, "\\+");
    s = s.replace(/\-/g, "\\-");
    s = s.replace(/\(/g, "\\(");
    s = s.replace(/\)/g, "\\)");
    s = s.replace(/\[/g, "\\[");
    s = s.replace(/\]/g, "\\]");
    return s;
}


/*
 *  Funcion - numeroFocus
 *  DY 20050720
 *
 *  (Des)formatea un numero en un textbox segun los locales definidos en los
 *  parametros:
 *
 *	  control = control contenedor del n�mero (textbox)
 *	  sepMil = caracter separador de miles
 *	  sepDec = caracter separador de decimales
 *	  numDec = cantidad de decimales a considerar
 *	  ingresando = booleano que indica si se est� ingresando o saliendo del
 *		  control. En caso verdadero se formatea desde "bonito" (p.e.
 *		  1.234,56) a editable (p.e. 1234,56), o inversamente si es falso.
 *
 *  En MSIE usar de la manera (p.e.):
 *  <input ... onActivate="return numeroFocus(this, '.', ',', 2, true)"
 *   onDeactivate="return numeroFocus(this, '.', ',', 2, false)" />
 *
 *  En NSS usar con onFocus (ingresando = true) y onBlur (ingresando = false).
 */
function numeroFocus(control, sepMil, sepDec, numDec, ingresando) {
    var salida = $.trim(control.value);

    if (ingresando) {
        while (salida.indexOf(sepMil) >= 0) {
            salida = salida.replace(sepMil, "");
        }
    } else if (salida.length > 0) {
        /* Redondea acorde a la cantidad de decimales numDec */
        // salida = ("" + Math.round(parseFloat(salida.replace(sepDec, ".")) * Math.pow(10, numDec)) / Math.pow(10, numDec));
        salida = salida.replace(sepDec, ".");

        /* Se invierte el string que representa al numero, asi es mas facil
		   operar sobre �ste para agregar los separadores de miles. Ademas se
		   reemplaza el punto decimal por "d". Si no tiene, entonces se agrega
		   una "d" al comienzo (que antes era el final) */
        salida = salida.reverse().replace('.', "d");
        if (salida.indexOf("d") < 0) salida = "d" + salida;

        /* Se agregan tantos ceros a la derecha (el string esta al reves)
		   como cantidad de decimales se hayan parametrizado en numDec */
        while (salida.indexOf("d") < numDec) salida = "0" + salida;
        var re = new RegExp('(d.*?)([0-9]{3})([0-9]+)');
        while (re.test(salida)) {
            /* Los puntos de miles se simbolizaran como "m" */
            salida = salida.replace(re, "$1$2m$3");
        }
        salida = salida.reverse().replace(/d$/, "");

        /* Se reemplazan la "d" y las "m" por los separadores reales */
        salida = salida.replace(/d/, sepDec).replace(/m/g, sepMil);
    }
    control.value = salida;
    return true;
}


/*
 *  Funcion para evento - numeroKeypress - FALTA NSS (debe usar onkeydown
 *  para cancelar)
 *  DY 20050721
 *
 *  Para usarse contra el evento onKeyPress, permitiendo que solo se acepte
 *  el ingreso al control de d�gitos y el caracter separador de decimales
 *  (solo si numDec > 0)
 *
 *	  evento  = evento "burbujeado" desde la pagina. Debe usarse "event"
 *	  control = control contenedor del n�mero (textbox)
 *	  sepDec  = caracter separador de decimales
 *	  numDec  = cantidad de decimales a considerar
 *
 *  Utilizar en MSIE en evento onKeyPress de la manera:
 *  <input ... onKeyPress="return numeroKeypress(event, this, ',', 2)" />
 */

function numeroKeypress(evento, control, tamano, numDec, sepDec, aceptaNeg) {
    //var key = (MSIE) ? window.event.keyCode : evento.which;

    var key = (MSIE) ? window.event.keyCode : evento.charCode;
    var car = String.fromCharCode(key);
    var ctrl = evento.ctrlKey;
    if (typeof (ctrl) == "undefined") ctrl = false;

    if (key == 8 || key == 0) {
        return true;
    }

    if (ctrl && (car == 'C' || car == 'c' || car == 'V' || car == 'v')) {
        return true;
    }

    if (numDec < 0) numDec = 0;
    numeroAdv_valor_anterior = control.value;

    // Se aceptan las pulsaciones de los digitos
    if (key >= 48 && key <= 57) {
        var nuevo_value = control.value.slice(0, posicionCursor(control)) + String.fromCharCode(key) + control.value.slice(posicionCursor(control));

        // pero validando la cantidad de enteros y decimales
        partes = /-?(\d*)\D?(\d*)/.exec(nuevo_value);
        ok_enteros = (tamano == 0) || (tamano > 0 && partes[1].length <= (tamano - numDec));
        if (typeof (partes[2]) == "undefined") partes[2] = '';
        ok_decimales = partes[2].length <= numDec;

        // o bien, q se tenga todo el número seleccionado

        if (typeof (document.selection) !== 'undefined') {
            seleccion = document.selection.createRange().text
        } else {
            seleccion = window.getSelection();
        }

        if (MSIE) {
            ok_seleccion = seleccion.length == nuevo_value.length - 1 || /^\d+$/.test(seleccion);
        } else {
            ok_seleccion = seleccion.getRangeAt(0).endOffset - 1 == nuevo_value.length - 1 || /^\d+$/.test(seleccion.getRangeAt(0).startContainer);
        }

        return ok_enteros && ok_decimales || ok_seleccion;
    }

    if (aceptaNeg) {
        // Se acepta la pulsacion de '-', el cual agrega/quita alternadamente
        // tal s�mbolo al comienzo del string
        if (key == 45) {
            if (control.value.indexOf("-") < 0) {
                control.value = '-' + control.value;
            } else if (!control.value.indexOf('-')) {
                control.value = control.value.replace(/^-/, '');
            }
        }
    }

    // Se aceptan las pulsaciones del separador de decimales que se haya
    // parametrizado en sepDec, siempre que no exista otro previo
    if (numDec > 0 && key == sepDec.charCodeAt(0)) {
        if (control.value.indexOf(sepDec) == -1) return true;
    }

    // Tambien se acepta la pulsaci�n de . (para poder utilizar el numpad),
    // el cual se reemplazar� por el separador decimal sepDec
    if (numDec > 0 && key == 46) {
        if (control.value.indexOf(sepDec) == -1) {
            event.keyCode = sepDec.charCodeAt(0);
            event.char = sepDec;
            // if (MSIE) window.event.keyCode = sepDec.charCodeAt(0);
            // else evento.which = sepDec.charCodeAt(0);
            return true;
        }
    }

    event.keyCode = 0;

    return false;
}


/* Fin Control Numero ------------------------------------------------------ */

/****************************************************
 *  Control NumeroAdv
 ****************************************************/


(function ($, undefined) {
    $.fn.getCursorPosition = function () {
        var el = $(this).get(0);
        var pos = 0;
        if ('selectionStart' in el) {
            pos = el.selectionStart;
        } else if ('selection' in document) {
            el.focus();
            var Sel = document.selection.createRange();
            var SelLength = document.selection.createRange().text.length;
            Sel.moveStart('character', -el.value.length);
            pos = Sel.text.length - SelLength;
        }
        return pos;
    }
})(jQuery);


/*
 * Obtiene la posici�n del cursor dentro de un control textbox o textarea
 */
function posicionCursor(control) {

    //return $(control).caret();
    return $(control).getCursorPosition();

}



var numeroAdv_valor_anterior;

/*
 *  Funcion - numeroAdvFocus
 *  DY 20071227
 *
 *  (Des)formatea un numero en un textbox segun los locales definidos en los
 *  parametros:
 *
 *	  control = control contenedor del n�mero (textbox)
 *	  sepMil = caracter separador de miles
 *	  sepDec = caracter separador de decimales
 *	  numDec = cantidad de decimales a considerar
 *	  ingresando = booleano que indica si se est� ingresando o saliendo del
 *		  control. En caso verdadero se formatea desde "bonito" (p.e.
 *		  1.234,56) a editable (p.e. 1234,56), o inversamente si es falso.
 *
 *  En MSIE usar de la manera (p.e.):
 *  <input ... onActivate="return numeroAdvFocus(this, '.', ',', 2, true)"
 *   onDeactivate="return numeroAdvFocus(this, '.', ',', 2, false)" />
 *
 *  En NSS usar con onFocus (ingresando = true) y onBlur (ingresando = false).
 */

function numeroAdvFocus(control, tamano, numDec, sepMil, sepDec, ingresando) {
    var salida = $.trim(control.value);

    // Se borran los separadores de miles
    while (sepMil.length > 0 && salida.indexOf(sepMil) >= 0) {
        salida = salida.replace(sepMil, "");
    }

    if (ingresando) {

        numeroAdv_valor_anterior = salida;

    } else if (salida.length > 0) {
        // Si el numero parte con el separador decimal, se le a�ade un 0
        if (salida.charAt(0) == sepDec) salida = '0' + salida;

        // Reconocimiento de parte entera y decimal
        partes = /-?(\d*)\D?(\d*)/.exec(salida);
        if (partes == null) {
            sntAlert("Se ha ingresado un formato num�rico err�neo.");
            try {
                control.focus()
            } catch (e) { }
            return false;
        }
        parteEnt = Number(partes[1]);
        parteDec = typeof (partes[2]) != "undefined" ? partes[2] : '0';

        // Se a�aden 0 a la derecha de la parte decimal (si es necesario)
        while (parteDec.length < numDec) {
            parteDec += "0";
        }

        // Se redondea parte decimal (si es necesario)
        if (parteDec.length > numDec) {
            parteDec = Math.round(parteDec.slice(0, numDec + 1) / 10);
        }
        if (numDec == 0) parteEnt += Number(parteDec);

        // Se agregan los separadores de miles a la parte entera
        re = new RegExp('(\\d{3})(\\d+)');
        parteEnt = parteEnt.toString().reverse();
        while (sepMil.length > 0 && re.test(parteEnt)) {
            parteEnt = parteEnt.replace(re, "$1" + sepMil + "$2");
        }
        parteEnt = parteEnt.reverse();

        // y se restaura el signo negativo (si habia)
        if (salida.charAt(0) == '-') parteEnt = '-' + parteEnt;

        salida = numDec > 0 ? parteEnt + sepDec + parteDec : parteEnt;
    }
    control.value = salida;
    return true;
}

/*
 *	Funcion para evento OnPaste - numeroAdvPaste - FALTA NSS (el cual debe
 *	no tiene implementado este evento)
 *	DY 20080908
 *
 *	Limita que se pegue desde el clipboard s�lo un n�mero que respete el
 *	formato.
 *
 *	  control   = control contenedor del n�mero (textbox)
 *	  tamano	= cantidad de d�gitos que soportar� el control (0 = infinito)
 *	  numDec	= cantidad de decimales a considerar
 *	  sepMil	= caracter separador de decimales
 *	  sepDec	= caracter separador de decimales
 *	  aceptaNeg = indica si acepta negativos
 *
 *  Utilizar en MSIE en evento onPaste de la manera:
 *  <input ... onPaste="return numeroAdvPaste(this, 10, 3, ',', true)" />
 */
function numeroAdvPaste(control, tamano, numDec, sepMil, sepDec, aceptaNeg) {
    // obtenci�n de texto desde el clipboard

    var auxcp = window.event.clipboardData || window.clipboardData;
    var texto = auxcp.getData('Text');
    // eliminacion de espacios
    texto = texto.replace(/\s+/g, '');
    // eliminacion de separadores de miles
    texto = texto.replace(new RegExp("\\" + sepMil, "g"), '');

    // c�lculo de cantidad de enteros permitidos
    var numEnt = numDec > 0 ? (tamano - numDec) : tamano;
    if (tamano == 0) numEnt = 99;

    // Formaci�n de expresi�n regular que cuadre
    var s_re = "^";
    s_re += aceptaNeg ? "(-?" : "(";
    s_re += "\\d{1," + numEnt + "})";
    s_re += numDec > 0 ? "(\\" + sepDec + "\\d{1," + numDec + "})?" : "";

    var re = new RegExp(s_re);
    if (!re.test(texto)) {
        sntAlert("Numero siendo pegado (" + texto + ") no se ajusta al\n" +
            "formato numerico especificado para este control.");
        //		 "formato de numero " + (!aceptaNeg ? "positivo " : "") +
        //		 (tamano > 0 ? "de " + numEnt + " enteros y " : "") +
        //		 numDec + " decimales.");
        return false;
    }
    partes = re.exec(texto);
    if (typeof (partes[2]) == "undefined") partes[2] = '';
    control.value = partes[1] + (numDec > 0 ? partes[2] : "");
    return false;
}

/* Fin Control NumeroAdv ------------------------------------------------- */




/****************************************************
 *  Control TextBoxAdv
 ****************************************************/


/*
 *  Imponer maxlength en TextArea (TextBox con TextMode = MultiLine)
 */
function textboxAdvMaxLength(evento, control, tamano) {
    if (control.value.length < tamano) return true;
    if (MSIE) window.event.keyCode = 0;
    else evento.which = 0;
    return false;
}

/* Fin Control TextBoxAdv ------------------------------------------------- */



/* ReloadIFrame (Treeview) */
function reloadIFrame(iframe, url) {


    if (!url.match(/-=ABORT=-/)) {

        var fbody = null;
        //JAL 20161111: bugid 301 https://cld-sonda.visualstudio.com/Sonda.NET/_workitems/edit/301
        if (typeof (SONDANET_HabilitarPopupCSS) == "undefined") {

            var ff = window.top.frames;

            try {
                fbody = fbody || ff['body'];
            } catch (e) { };
            try {
                if (ff['Body']) fbody = fbody || ff['Body'].frames['iframe_body'];
            } catch (e) { };
            try {
                fbody = fbody || ff['Body'];
            } catch (e) { };
            try {
                if (ff['side']) fbody = fbody || ff['side'].frames['iframe_body'];
            } catch (e) { };
            try {
                if (ff['header']) fbody = fbody || ff['header'].frames['iframe_body'];
            } catch (e) { };
            try {
                if (ff[iframe]) fbody = fbody || window.top;
            } catch (e) { };
            try {
                if (ff['iframe_body']) fbody = ff['iframe_body'] || window.top;
            } catch (e) { };

            if (fbody) {
                fbody.frames[iframe].location.href = url;
            } else {
                sntAlert("La funcion reloadIFrame no pudo encontrar el frame interno '" + iframe + "'");
            }
        } else {
            var fbody = findElement(window.top, iframe);
            if (fbody) {
                if (fbody.contentWindow) {
                    fbody.contentWindow.location.href = url;
                }
            }
        }
    }
}

function findElement(wdw, id) {
    var el = wdw.document.getElementById(id);
    try { if (!el) el = wdw.document.getElementsByName(id)[0]; } catch (e) { };
    if (el) return el;
    for (var i = 0; i < wdw.frames.length; i++) {
        var el = findElement(wdw.frames[i].window, id);
        if (el) return el;
    }

    return null;
}

/**********************
 * Exportacion a formatos
 * ********************/

function abrirVentanaExportacion(url) {
    var width = 1;
    var height = 1;
    var name = '';
    var parms = 'status=not,scrollbars=not,menubar=not';
    var left = screen.width;
    var top = screen.height;
    var winParms = 'top=' + top + ',left=' + left + ',height=' + height +
        ',width=' + width;
    if (parms) {
        winParms += ',' + parms;
    }
    newWindow = window.open(url, name, winParms);
    if (parseInt(navigator.appVersion) >= 4) {
        newWindow.window.focus();
    }
    setTimeout('cerrarVentanaExportacion(newWindow)', 4000);
}


function cerrarVentanaExportacion(newWindow) {
    if (newWindow) {
        newWindow.close();
        if (!newWindow.closed) {
            if (parseInt(navigator.appVersion) >= 4) {
                newWindow.window.focus();
            }
            setTimeout('cerrarVentanaExportacion(newWindow)', 4000);
        }
    }
}


/****
 * Deshabilitar doble submit (plt, evitar multiples postbacks)
 ****/

/* Desactiva los botones justo antes de un submit */
function window_onbeforeunload() {
    var a;
    a = window.document.body.getElementsByTagName("INPUT");
    for (i = 0; i < a.length; i++) {
        if ((a[i].type == "button") || (a[i].type == "submit")) {
            a[i].disabled = 1;
        }
    }
}

var isNewPostBackLoaded = 0;

function deshabilitarDobleSubmit() {
    if (!isNewPostBackLoaded) {
        isNewPostBackLoaded = 1;

        var isSubmitting = 0;
        var __oldDoPostBack = __doPostBack;

        function __BeforeDoPostBack(eventTarget, eventArgument) {
            if (isSubmitting++) return;
            return __oldDoPostBack(eventTarget, eventArgument);
        }
        var __doPostBack = __BeforeDoPostBack;
    }

    window.onbeforeunload = window_onbeforeunload;
}

// vim:ts=2:sw=2:et:si

/*****
 ** Funciones que se llaman al clickear un item de menu
 ******/
function sntOpenMenuFrame(target, url) {
    closeAllMenus();
}

function sntOpenMenu(url) {
    closeAllMenus();
}

/*****
 ** muestra y oculta reloj
 ******/

var snt_clock = null;
var snt_clock_div = null;
var snt_clock_visible = false;

function sntShowClock() {

    if (window.parent != null) {
        try {
            if (typeof (window.parent.sntShowClock) != "undefined") {
                if (window.parent != window) {
                    window.parent.sntShowClock();
                } else {
                    SONDANET_MostrarReloj = true;
                }
            }
        } catch (e) { }

    }

    if (typeof (SONDANET_MostrarReloj) != "undefined") {
        if (SONDANET_MostrarReloj) {

            var imagen;
            if (snt_clock == null) {
                snt_clock = document.createElement("div");
                imagen = document.createElement("img");
                imagen.src = "img/sondanet/processing.gif";
                snt_clock.appendChild(imagen);
                snt_clock.style.position = "absolute";
                document.getElementsByTagName("body").item(0).appendChild(snt_clock);
            } else {
                imagen = $(snt_clock).children().first();
            }
            var cX = document.body.scrollWidth / 2;
            var cY = document.body.scrollHeight / 2;
            var cW = $(imagen).width() / 2;
            var cH = $(imagen).height() / 2;

            snt_clock.style.left = (cX - cW) + "px";
            snt_clock.style.top = (cY - cH) + "px";
            snt_clock.style.display = "block";

        }
    }

}

function sntHideClock() {

    if (window.parent != null) {
        if (typeof (window.parent.sntHideClock) != "undefined") {
            if (window.parent != window) {
                window.parent.sntHideClock();
            } else {
                SONDANET_MostrarReloj = true;
            }
        }
    }

    if (typeof (SONDANET_MostrarReloj) != "undefined") {
        if (SONDANET_MostrarReloj) {
            if (snt_clock != null) {
                snt_clock.style.display = "none";
            }
        }
    }
}

function sntOnSubmit() {
    //JAL: Permite agregar javascript cuando hace submit, para uso futuro
}

function __newDoPostBack(eventTarget, eventArgument) {
    sntShowClock();
    __oldDoPostBack(eventTarget, eventArgument);
}

$(document).ready(function ($) {
    if (typeof (SONDANET_MostrarReloj) != "undefined") {
        if (SONDANET_MostrarReloj) {
            if (typeof (__doPostBack) != "undefined") {
                __oldDoPostBack = __doPostBack;
                __doPostBack = __newDoPostBack;
            }
        }
    }
});

$(window).on("unload", function () {

    sntShowClock();

    // Solo muestra el reloj cuando se termina de cargar un pantalla de primer nivel, no dentro de iframes
    // if (window.parent != null) {
    // if (window.parent == window.top) {
    // sntShowClock();
    // }
    // }
});

// Previene que la tecla backspace gatille el volver del browser
$(document).unbind('keydown').bind('keydown', function (event) {
    var doPrevent = false;
    if (event.keyCode === 8) {
        var d = event.srcElement || event.target;
        if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD')) || d.tagName.toUpperCase() === 'TEXTAREA') {
            doPrevent = d.readOnly || d.disabled;
        } else {
            doPrevent = true;
        }
    }

    if (doPrevent) {
        event.preventDefault();
    }
});

/* redimensiona el iframe */

var defaultpage_ort; // On resize timeout

var defaultpage_disable_InitResize = false; // On resize timeout

function defaultpage_InitResize() {
    var ucEj = $("#iframe_body");
    if (ucEj[0]) {
        if (!defaultpage_disable_InitResize) {
            defaultpage_DoLayout();
            $(window).resize(defaultpage_OnResize);
            $(ucEj[0]).load(defaultpage_OnResize);
        }
    }
}

function defaultpage_OnResize() {
    if (defaultpage_ort) clearTimeout(defaultpage_ort);
    defaultpage_ort = setTimeout(defaultpage_DoLayout, 100);
}

function defaultpage_DoLayout() {
    try {
        var ucEj = $("#iframe_body");
        if (ucEj[0]) {
            var pos = ucEj.position();
            $(ucEj[0]).height(($(document).height() - pos.top));
            $(ucEj[0]).width(($(document).width() - pos.left));
        }
    } catch (e) { }
}

$(document).ready(defaultpage_InitResize);

var IEVERSION = null;

if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) { //test for MSIE x.x;
    IEVERSION = new Number(RegExp.$1) // capture x.x portion and store as a number
}

function sntLoaded() {

    if (window.parent != null) {
        try {
            if (typeof (window.parent.sntIframeLoaded) != "undefined") {
                if (window.parent != window) {
                    window.parent.sntIframeLoaded();
                }
            }
        } catch (e) { }
    }

}

function sntVolverAlInicio() {
    /*if (parent.frames['iframe_body']) { document.body.scrollIntoView(true); }*/
}

function fixValidatorProperties() {
    if (typeof (Page_Validators) != "undefined") {
        if (Page_Validators && Page_Validators.length > 0) {
            var val = null;
            for (i = 0; i < Page_Validators.length; i++) {
                val = Page_Validators[i];

                if (val != null && typeof (val.evaluationfunction) == "undefined") {
                    if (val.initialvalue == undefined)
                        val.initialvalue = "";
                    if ($(val).attr("evaluationfunction"))
                        eval("val.evaluationfunction = " + $(val).attr("evaluationfunction") + ";");
                    if ($(val).attr("controltovalidate"))
                        val.controltovalidate = $(val).attr("controltovalidate");
                    if ($(val).attr("errormessage"))
                        val.errormessage = $(val).attr("errormessage");
                    if ($(val).attr("Dynamic"))
                        val.Dynamic = $(val).attr("Dynamic");
                    if ($(val).attr("initialvalue"))
                        val.initialvalue = $(val).attr("initialvalue");
                    if ($(val).attr("ValidationExpression"))
                        val.validationexpression = $(val).attr("ValidationExpression");
                }
            }
        }
    }

    if (typeof (ValidatorOnLoad) == "function") {
        ValidatorOnLoad();
    }
}


var oldWindowOpen = window.open;

window.open = function (url, name, features) {

    if (typeof (SONDANET_HabilitarPopupCSS) != "undefined") {
        sntWindowOpen(url, name, features);
    } else {
        return oldWindowOpen(url, name, features);
    }
}

function sntWindowOpen(url, name, features) {

    var w;
    var h;
    var resizable = "no";
    var scroll = "no";
    var status = "no";

    // get the modal specs
    var mdattrs = features.split(",");
    for (i = 0; i < mdattrs.length; i++) {
        var mdattr = mdattrs[i].split("=");

        var n = mdattr[0];
        var v = mdattr[1];
        if (n) {
            n = n.trim().toLowerCase();
        }
        if (v) {
            v = v.trim().toLowerCase();
        }

        if (n == "height") {
            h = v.replace("px", "");
        } else if (n == "width") {
            w = v.replace("px", "");
        } else if (n == "resizable") {
            resizable = v;
        } else if (n == "scrollbars") {
            scroll = v;
        } else if (n == "status") {
            status = v;
        }
    }

    var viewportWidth = $(window).width();
    var viewportHeight = $(window).height();

    var left = (viewportWidth / 2) - (w / 2);
    var top = (viewportHeight / 2) - (h / 2);

    options = {
        position: {
            top: top,
            left: left
        }
    };

    return openKendoWindow(url, "", h, w, options, null, false);
}

var oldShowModalDialog = window.showModalDialog;

window.showModalDialog = function (arg1, arg2, arg3, onCloseFunction) {
    var w;
    var h;
    var resizable = "no";
    var scroll = "no";
    var status = "no";

    // get the modal specs
    var mdattrs = arg3.split(";");
    for (i = 0; i < mdattrs.length; i++) {
        var mdattr = mdattrs[i].split(":");

        var n = mdattr[0];
        var v = mdattr[1];
        if (n) {
            n = n.trim().toLowerCase();
        }
        if (v) {
            v = v.trim().toLowerCase();
        }

        if (n == "dialogheight") {
            h = v.replace("px", "");
        } else if (n == "dialogwidth") {
            w = v.replace("px", "");
        } else if (n == "resizable") {
            resizable = v;
        } else if (n == "scroll") {
            scroll = v;
        } else if (n == "status") {
            status = v;
        }
    }

    var viewportWidth = $(window).width();
    var viewportHeight = $(window).height();

    var left = (viewportWidth / 2) - (w / 2);
    var top = (viewportHeight / 2) - (h / 2);

    if (typeof (SONDANET_HabilitarPopupCSS) != "undefined") {
        options = {
            position: {
                top: top,
                left: left
            }
        };
        return openKendoWindow(arg1, "", h, w, options, arg2, true, onCloseFunction);
    } else {

        if (typeof (oldShowModalDialog) != "undefined" && (oldShowModalDialog != null)) {
            return oldShowModalDialog(arg1, arg2, arg3);
        } else {

            oldShowModalDialog = null;

            $("#dialogArguments").remove();

            var input = $('<input>').attr({
                type: 'hidden',
                id: 'dialogArguments',
                name: 'dialogArguments'
            });

            $('form').append(input);

            input.val(JSON.stringify(arg2));

            var div = $('<div id="SONDANET_overlay" class="k-overlay" style="z-index: 10002; opacity: 0.5;"></div>').appendTo(document.body);

            var targetWin = window.open(arg1, '', 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            targetWin.focus();

            // No esta funcionando en chrome
            $(targetWin).blur(function () {
                $(targetWin).blur();
                setTimeout(function () {
                    $(targetWin).focus()
                }, 1)
            });

            return targetWin;
        }

    }
}

function openKendoWindow(url, title, height, width, additionalOptions, dialogArguments, modal, onCloseFunction) {

    var container,
        content,
        options,
        wnd,
        name,
        topWnd;

    topWnd = window.top;

    if (title.length === 0) {
        var d = Date.now();
        name = "sntPopupWindow" + d;
    } else {
        // Replace non-alphanumeric characters
        name = title.replace(/[^a-zA-Z0-9_]/, '');
    }

    // Add the div to initialize to the DOM
    container = topWnd.$('<div class="sntPopupWindow" />').attr('id', name).appendTo(topWnd.document.body);
    topWnd.containerGlobal = name;

    var fx = function () { onKendoWindowClose(container, onCloseFunction); }

    // Default options
    options = {
        title: title,
        height: parseInt(height) + 24,
        width: parseInt(width) + 24,
        content: url,
        iframe: true,
        modal: modal,
        close:  fx
    };

    // Merge options
    try {
        if (additionalOptions != null) {
            options = mergeObjects(options, additionalOptions);
        }
    } catch (e) {
        //console.log('Failed while merging options:');
        //console.log(e);
    }

    try {
        // Initialize Kendo Window object
        var windowElement = container.kendoWindow(options);
        var iframeDomElement = windowElement.children("iframe")[0];
        var iframeWindowObject = iframeDomElement.contentWindow;
        var iframeDocumentObject = iframeDomElement.contentDocument;

        // Div que contiene el iframe del popup
        wnd = windowElement.data('kendoWindow');
        wnd.center();

        topWnd.$(iframeWindowObject.frameElement).data("parentKendoWindow", wnd);

        if (dialogArguments == null) {
            dialogArguments = new Object();
        }

        dialogArguments["parentKendoWindow"] = wnd;
        // GCC 20170214
        dialogArguments["parentWindow"] = window;

        topWnd.$(iframeWindowObject.frameElement).data("dialogArguments", dialogArguments);

        // GCC 20170214
        topWnd.$(iframeWindowObject.frameElement).data("parentWindow", window);

        topWnd.$(iframeWindowObject.frameElement).data("onKendoWindowCloseFn", fx);

        return wnd;

        //console.log('\nKendo Window object:');
        //console.log(wnd);
    } catch (e) {
        //console.log('Failed while creating Kendo window:\n');
        //console.log(e);
    }
}

function onKendoWindowClose(container, onCloseFunction) {
    if(onCloseFunction !== undefined){
        setTimeout(onCloseFunction, 1);
    }

    window.top.$(container).data("kendoWindow").destroy();
    sntHideClock();
}

//
// Source: http://stackoverflow.com/questions/171251/how-can-i-merge-properties-of-two-javascript-objects-dynamically
function mergeObjects(obj1, obj2) {
    var attrname, obj3 = {};

    for (attrname in obj1) {
        obj3[attrname] = obj1[attrname];
    }
    for (attrname in obj2) {
        obj3[attrname] = obj2[attrname];
    }

    return obj3;
}


/*function sntWindowClose() {

    sntHideClock();
	
    if (typeof (SONDANET_HabilitarPopupCSS) == "undefined") {
        window.close();
    } else {
        var windowElement = window.top.$('div.sntPopupWindow:last');
        var iframeDomElement = windowElement.children("iframe")[0];
        var iframeWindowObject = iframeDomElement.contentWindow;
        var iframeDocumentObject = iframeDomElement.contentDocument;
        var topWnd = window.top;

        kw = topWnd.$(iframeWindowObject.frameElement).data("parentKendoWindow");

        //var fx = topWnd.$(iframeWindowObject.frameElement).data("onKendoWindowCloseFn");	
        //fx(); //se comenta 03-04-2019 RMA sol:3013
        if (typeof (kw) != "undefined") {
            kw.close();// se agrega 03-04-2019 RMA 3013			
           //  setTimeout(function () { kw.destroy(); }, 1);
        } else {
			
            window.close();
        }
    }

}*/

function sntWindowClose() {

    sntHideClock();
    if (typeof (SONDANET_HabilitarPopupCSS) == "undefined") {
        window.close();
    } else {
        var windowElement = window.top.$('div.sntPopupWindow:last');
        var iframeDomElement = windowElement.children("iframe")[0];
        var iframeWindowObject = iframeDomElement.contentWindow;
        var iframeDocumentObject = iframeDomElement.contentDocument;
        var topWnd = window.top;

        kw = topWnd.$(iframeWindowObject.frameElement).data("parentKendoWindow");

        var fx = topWnd.$(iframeWindowObject.frameElement).data("onKendoWindowCloseFn");
        //fx();

        if (typeof (kw) !== undefined) {
            setTimeout(function () { kw.destroy(); }, 1);
        } else {
            window.close();
        }
		var windowElementArr = window.top.$('div.sntPopupWindow');
		if (windowElementArr.length>=2)
		{	
			windowElement = windowElementArr[windowElementArr.length-2];
			iframeDomElement = $(windowElement).children("iframe")[0];
			iframeWindowObject = iframeDomElement.contentWindow;
		
			var kw2 = topWnd.$(iframeWindowObject.frameElement).data("parentKendoWindow");
			kw2.toFront();
		}
		
		fx();				
    }

}

var alertCount = 0;
var arrSntAlert = [];

function sntAlert(msg, origen) {

    if (typeof (SONDANET_HabilitarPopupCSS) != "undefined") {

        alertCount = alertCount + 1;

        var name = 'sntAlertContainer' + alertCount;
        container = $('<div style="text-align:center;font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 14px;" />').attr('id', name).appendTo(document.body);
        container.html(msg);
        $('<br/>').appendTo(container);
        $('<br/>').appendTo(container);
        button = $('<button type="button">Aceptar</button>').appendTo(container);
        try {

            var kwOptions = {
                title: "Atencion",
                visible: false,
                modal: true,
                actions: ["Close"]
            }

            var kw = container.kendoWindow(kwOptions).data("kendoWindow");

            if (arrSntAlert.indexOf(origen) === -1) {
                kw.center().open();
            }

            button.click(function () {
                kw.destroy();
                arrSntAlert = [];
            });

            // GCC 20170726
            if (origen !== undefined) {
                if ($('#' + origen).length) {
                    if ($('#' + origen).attr('onchange').indexOf('PostBack') > -1) {
                        var idControl = origen.replace('_', '$');
                        old___doPostBack = __doPostBack;
                        __doPostBack = null;
                        button.click(function () {
                            $('#' + origen).val('');
                            old___doPostBack();
                        });
                        return false;

                    } else {
                        return false;
                    }
                }
            }

        } catch (e) {
            //console.log('Failed while creating Kendo window:\n');
            //console.log(e);
        }


    } else {
        alert(msg);
    }

    arrSntAlert.push(origen);

}

/* nuevas funciones javascript fin700v7.5 */

function sntControlPrint(ctrl) {
    var objeto = $("#" + ctrl)[0]; //obtenemos el objeto a imprimir
    var ventana = window.open('', '_blank'); //abrimos una ventana vac�a nueva
    ventana.document.write(objeto.value); //imprimimos el HTML del objeto en la nueva ventana
    ventana.document.close(); //cerramos el documento
    ventana.print(); //imprimimos la ventana
    ventana.close(); //cerramos la ventana
    window.close();
}

function sntWindowPrint() {
    window.print();
}

/* redimensionar preimpresion */
function sntResizePdf() {
    var col;

    col = document.getElementsByTagName("OBJECT");

    if (col.length > 0) {
        for (i = 0; i < col.length; i++) {
            var h = pipgetViewportHeight() - 100;
            if (h > 1000) h = 451;
            col[i].style.width = (pipgetViewportWidth() - 50) + "px";
            col[i].style.height = h + "px";
        }
    }
}

function pipgetViewportHeight() {
    if (window.innerHeight != window.undefined) return window.innerHeight;
    if (document.compatMode == 'CSS1Compat') return document.documentElement.clientHeight;
    if (document.body) return document.body.clientHeight;

    return window.undefined;
}

function pipgetViewportWidth() {
    var offset = 17;
    var width = null;
    if (window.innerWidth != window.undefined) return window.innerWidth;
    if (document.compatMode == 'CSS1Compat') return document.documentElement.clientWidth;
    if (document.body) return document.body.clientWidth;
}

/* desactivar enter */

function sntDesactivarEnter() {
    $(document).keypress(function (e) {
        e = e || event;
        var txtArea = /textarea/i.test((e.target || e.srcElement).tagName);
        return txtArea || (e.keyCode || e.which || e.charCode || 0) !== 13;
    })
}

function sntWindowTopReload() {
    top.location.reload();
}

function sntControlToValidate(ctl) {
    return $(ctl).prop("controltovalidate");
}

/* numeroadv multinavegador */
function numeroAdvKeyPressMN(evt, control, tamano, numDec, sepDec, aceptaNeg) {

    /*
        var key = (MSIE) ? window.event.keyCode : evt.charCode;
        var car = String.fromCharCode(key);
        var ctrl = evt.ctrlKey;
        if (typeof(ctrl) == "undefined") ctrl = false;
    
        if (key == 8 || key == 0) {
            return true;
        }
    
        if (ctrl && (car == 'C' || car == 'c' || car == 'V' || car == 'v')) {
            return true;
        }
    
        if (numDec < 0) numDec = 0;
        numeroAdv_valor_anterior = control.value;
    
        // Se aceptan las pulsaciones de los digitos
        if (key >= 48 && key <= 57) {
            var nuevo_value = control.value.slice(0, posicionCursor(control)) + String.fromCharCode(key) + control.value.slice(posicionCursor(control));
    
            // pero validando la cantidad de enteros y decimales
            partes = /-?(\d*)\D?(\d*)/.exec(nuevo_value);
            ok_enteros = (tamano == 0) || (tamano > 0 && partes[1].length <= (tamano - numDec));
            if (typeof(partes[2]) == "undefined")  partes[2] = '';
            ok_decimales = partes[2].length <= numDec;
    
            // o bien, q se tenga todo el n�mero seleccionado
            seleccion = (MSIE) ? seleccion = document.selection.createRange().text : window.getSelection();
            ok_seleccion = seleccion.length == nuevo_value.length - 1 ||
                /^\d+$/.test(seleccion);
    
            return ok_enteros && ok_decimales || ok_seleccion;
        }
    
        if (aceptaNeg) {
            // Se acepta la pulsacion de '-', el cual agrega/quita alternadamente
            // tal s�mbolo al comienzo del string
            if (key == 45) {
                if (control.value.indexOf("-") < 0) {
                    control.value = '-' + control.value;
                } else if (!control.value.indexOf('-')) {
                    control.value = control.value.replace(/^-/, '');
                }
            }
        }
    
        // Se aceptan las pulsaciones del separador de decimales que se haya
        // parametrizado en sepDec, siempre que no exista otro previo
        if (numDec > 0 && key == sepDec.charCodeAt(0)) {
            if (control.value.indexOf(sepDec) == -1) return true;
        }
        */

    if (evt.which) {
        var charStr = String.fromCharCode(evt.which);
        var transformedChar = transformTypedCharacter(charStr, sepDec);
        if (transformedChar != charStr) {
            insertTextAtCursor(transformedChar);
            return false;
        }
    }
    return true;

}

function transformTypedCharacter(typedChar, sepDec) {
    return typedChar == "." ? sepDec : typedChar;
}

function insertTextAtCursor(text) {
    var sel, range, textNode;
    if (window.getSelection) {
        sel = window.getSelection();
        if (sel.getRangeAt && sel.rangeCount) {
            range = sel.getRangeAt(0).cloneRange();
            range.deleteContents();
            textNode = document.createTextNode(text);
            range.insertNode(textNode);

            // Move caret to the end of the newly inserted text node
            range.setStart(textNode, textNode.length);
            range.setEnd(textNode, textNode.length);
            sel.removeAllRanges();
            sel.addRange(range);
        }
    } else if (document.selection && document.selection.createRange) {
        range = document.selection.createRange();
        range.pasteHTML(text);
    }
}


function fixTableWidth() {

    if (navigator.userAgent.toLowerCase().match(/firefox/i)) {
        // mejorar: invocar solo cuando sea firefox
        $('table[width]').each(function (index) {
            var w = $(this).attr('width');
            $(this).removeAttr('width');

            var sw = this.style['width'];
            if (sw == '') {
                $(this).css('width', w);
                //console.log('style set #1: ' + w);
            }
        });

        $('table:not([width])').each(function (index) {
            var sw = this.style['width'];
            if (sw == '') {
                var w = '100%';
                $(this).css('width', w);
                //console.log('style set #2: ' + w);
            }
        });
    };

}