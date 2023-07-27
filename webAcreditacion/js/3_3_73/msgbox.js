var SV_MSGBOX = "3.3.73";

if (typeof($) != "function") {
  alert('Biblioteca JQuery no pudo ser cargada o no esta activa');
}
	
$(document).ready(function () {
	if (typeof(arrayMsgboxes) != "undefined")
	{
	    var i;
	    for (i = 0; i < arrayMsgboxes.length; i++) {

	        control = $("#" + arrayMsgboxes[i]);
	        var dialogArguments = { mMensaje: control.attr("mensaje"), mTipo: control.attr("tipo"), mTitulo: control.attr("titulo"), mRetorno: 0, mControlId: arrayMsgboxes[i] };

	        var wnd = window.showModalDialog('js/' + SV_MSGBOX.replace(/\./g,"_") + '/msgbox.htm',
				dialogArguments,
				'dialogHeight:170px;dialogWidth:500px;status:0;resizable:0;scroll:0;help:0;unadorned:0');
	     
			$(wnd).data("dialogArguments", dialogArguments);
		 
	        if (typeof (SONDANET_HabilitarPopupCSS) != "undefined") {
	            sntMSGBOXIntervalStart(wnd, wnd, arrayMsgboxes[i]);
	        } else {
				control.val(dialogArguments.mRetorno);
				if (control.tipo != 3) eval(control.attr("PostBackFunction"));
	        }
		}
	}	
});

function sntMSGBOXIntervalStart(wnd, obj, controlId) {
    $(obj).data("mRetorno", 0);
	$(obj).data("control", controlId);
    window.setTimeout(function () { sntMSGBOXInterval(wnd); }, 500);
}

function sntMSGBOXInterval(wnd) {
    
    var obj = null;
    var salir = false;
    var mControlId = null;

	if (typeof (wnd.closed) != "undefined") {
        if (wnd.closed) {
            salir = true;
        }
    }

    if (typeof (wnd._closing) != "undefined") {
        if (wnd._closing) {
            salir = true;
        }
    }

    if (!salir) {
        var mRetorno = $(wnd).data("mRetorno");

        if (typeof (mRetorno) == "undefined") {
            mRetorno = $(wnd.document.body).data("mRetorno");
            obj = wnd.document.body;
        } else {
            mControlId = $(wnd).data("mControlId");
        }

        var dialogArguments = null;
		
		if (typeof (wnd.dialogArguments) != "undefined") {
			dialogArguments = wnd.dialogArguments;
		}
		
		if (typeof ($(wnd).data("dialogArguments") != "undefined")) {
			dialogArguments = $(wnd).data("dialogArguments");
		}
		
        mRetorno = dialogArguments.mRetorno;
        mControlId = dialogArguments.mControlId;

        if (typeof (mRetorno) != "undefined") {
            if (mRetorno != 0) {
                var control = $("#" + mControlId);
                control.val(mRetorno);
                if (control.attr("tipo") != 3) eval(control.attr("PostBackFunction"));
                wnd.close();
                salir = true;
            }
        }
    }
    
    if (!salir) window.setTimeout(function () { sntMSGBOXInterval(wnd); }, 500);
    
}