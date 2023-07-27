var SV_MSGBOX = "2.9.4.0";

if (document.all && window.attachEvent) { 
	window.attachEvent("onload", msgbox_onload); 
} 

function msgbox_onload(){
	if (typeof(arrayMsgboxes) != "undefined")
	{
		var i;
		for (i = 0; i < arrayMsgboxes.length; i++) {
			control = document.all[arrayMsgboxes[i]];
				mMensaje=control.mensaje;
				mTipo=control.tipo;
				mTitulo=control.titulo;
				mRetorno=0;
				window.showModalDialog('msgbox.htm',  
					window,
					'dialogHeight:170px;dialogWidth:500px;status:0;resizable:0;scroll:0;help:0;unadorned:0');
				control.value=mRetorno;
				if(control.tipo != 3 )  eval(control.PostBackFunction); 
		}
	}	
}