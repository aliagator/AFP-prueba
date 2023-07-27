var isModal = true;

var SONDANET_HabilitarPopupCSS;

function fixDialogArguments() {

	if (typeof (dialogArguments) == "undefined") {
		if (window.frameElement != null) {
			if (typeof (window.frameElement) != "undefined") {
				if (typeof (parent.window.$(window.frameElement).data("dialogArguments")) != "undefined") {
					dialogArguments = parent.window.$(window.frameElement).data("dialogArguments");
					SONDANET_HabilitarPopupCSS = true;
				}
			}
		} else {
			if (opener != null)
				if (typeof (window.opener.$("#dialogArguments").val()) != "undefined") {
					dialogArguments = JSON.parse(window.opener.$("#dialogArguments").val());
				}
		}
	}
	
}

function clickit(thisbutton) {
	fixDialogArguments();
	var sData = dialogArguments;
	if(sData.mTipo==1){
		if(thisbutton.id=="btn1") sData.mRetorno=1;
		if(thisbutton.id=="btn2") sData.mRetorno=2;
	}
	if(sData.mTipo==2){
		if(thisbutton.id=="btn1") sData.mRetorno=1;
		if(thisbutton.id=="btn2") sData.mRetorno=2;
		if(thisbutton.id=="btn3") sData.mRetorno=3;
	}
	if(sData.mTipo==3){
		if(thisbutton.id=="btn1") sData.mRetorno=4;
	}

	var kw = dialogArguments["parentKendoWindow"];

	if (typeof kw != "undefined") {
		$(kw).data("mControlId", sData.mControlId);
		$(kw).data("mRetorno", sData.mRetorno);
	}

	if (opener != null) {
		window.opener.$(window.document.body).data("mControlId", sData.mControlId);
		window.opener.$(window.document.body).data("mRetorno", sData.mRetorno);
	}
	
	if (isModal == true) {
		// window.close();
		sntWindowClose();
	} 

}

$(document).ready(function () {
	setTimeout(init,1);
});


function init() {
	
	fixDialogArguments();
	var sData = dialogArguments;
	lblDescripcion.innerHTML = sData.mMensaje;
	document.title = sData.mTitulo;

	if (sData.mTipo == 1) {
		dlgImg.src = "../../img/msgbox/pregunta.gif"
		btn1.value = "  Si  ";
		btn2.value = "  No  ";
		btn3.value = "x";
		btn3.style.visibility = "hidden";
	}
	if (sData.mTipo == 2) {
		dlgImg.src = "../../img/msgbox/pregunta.gif"
		btn1.value = "   Si   ";
		btn2.value = "   No   ";
		btn3.value = "Cancelar";
	}

	if (sData.mTipo == 3) {
		dlgImg.src = "../../img/msgbox/informacion.gif"
		btn1.value = "  Ok  ";
		btn2.value = "x";
		btn3.value = "x";
		btn2.style.visibility = "hidden";
		btn3.style.visibility = "hidden";
	}
}