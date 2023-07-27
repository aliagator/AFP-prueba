function lblMostrarDetalles_onclick() {
    var aux = lblDescripcion.clientHeight;
    if (aux < 103) aux = 103;
    dialogHeight = (300 + 50 + 65 + aux) + "px";
    lblMostrarDetalles.innerHTML = "";
    dialogTop = ((parseInt(window.screen.height) - parseInt(dialogHeight)) / 2) + "px";
}

function ajustarAlto() {
    var aux = lblDescripcion.clientHeight;
    if (aux < 103) aux = 103;
    dialogHeight = (65 + aux) + "px";
    dialogTop = ((parseInt(window.screen.height) - parseInt(dialogHeight)) / 2) + "px";
    window.scrollTo(0, 0);
}

var isModal = null;

function fixDialogArguments() {

    if (typeof(dialogArguments) == "undefined") {
        if (window.frameElement != null) {
            if (typeof(window.frameElement) != "undefined") {
                if (typeof(parent.window.$(window.frameElement).data("dialogArguments")) != "undefined") {
                    dialogArguments = parent.window.$(window.frameElement).data("dialogArguments");
                }
            }
        } else {
            if (opener != null)
                if (typeof(window.opener.$("#dialogArguments").val()) != "undefined") {
                    dialogArguments = JSON.parse(window.opener.$("#dialogArguments").val());
                }
        }
    } else {
        if (isModal == null) isModal = true;
    }

    if (isModal == null) isModal = false;

}

$(document).ready(function() {
    
    fixDialogArguments();

    var sData = dialogArguments;

    if (typeof(sData.eDescripcionHTML) != "undefined" && sData.eDescripcionHTML != "") {
        lblDescripcion.innerHTML = sData.eDescripcionHTML;
    }

    if (typeof(sData.eDescripcion) != "undefined" && sData.eDescripcion != "") {
        lblDescripcion.innerHTML = lblDescripcion.innerHTML + "<strong>Descripci&oacute;n: </strong>" + sData.eDescripcion;
    }

    lblDescripcion.style.visibility = "visible";

    if (typeof(sData.eErrorCompleto) != "undefined" && sData.eErrorCompleto != "") {
        var aux = sData.eErrorCompleto;
        aux = aux.replace(/-=NewLine=-/g, String.fromCharCode(13, 10));
        aux = aux.replace(/-=Slash=-/g, "\\");
        lblErrorCompleto.innerText = aux;
        lblMostrarDetalles.innerHTML = "<u><strong>Mostrar detalles</u></strong>"
    }

    if (typeof(sData.eProceso) != "undefined" && sData.eProceso != "") {
        lblDescripcion.innerHTML = lblDescripcion.innerHTML + "<br><strong>Proceso de negocio: </strong>" + sData.eProceso;
    }

    if (typeof(sData.eDetalle) != "undefined" && sData.eDetalle != "") {
        lblDescripcion.innerHTML = lblDescripcion.innerHTML + "<br><strong>Detalle: </strong>" + sData.eDetalle;
    }

    if (typeof(sData.statusBarText) != "undefined" && sData.statusBarText != "") {
        lblStatusBarText.innerHTML = "<strong>Aplicaci&oacute;n: </strong>" + sData.statusBarText.replace(/,/g, ", ");
        lblStatusBarText.style.visibility = "visible";
    } else document.getElementsByName["lblStatusBarText"].removeNode();


    var txtCodigo = "";
    if (typeof(sData.eCodigo) != "undefined") {

        if (sData.eCodigo != "") txtCodigo = txtCodigo + "<strong>C&oacute;digo: </strong>" + sData.eCodigo;
        if (typeof(sData.eSeveridad) != "undefined")
            if (sData.eSeveridad != "") {
                txtCodigo = txtCodigo + "&nbsp;<strong>Severidad: </strong>" + sData.eSeveridad;
                imgSeveridad.src = "../../img/severidad/sev" + sData.eSeveridad + ".gif";
            }
        if (typeof(sData.eTipo) != "undefined")
            if (sData.eTipo != "") txtCodigo = txtCodigo + "&nbsp;<strong>Tipo: </strong>" + sData.eTipo;
    }
    if (txtCodigo != "") {
        lblDescripcion.innerHTML = lblDescripcion.innerHTML + "<br>" + txtCodigo;
    }
    setTimeout("ajustarAlto()", 1);
});