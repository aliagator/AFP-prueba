if (document.all && window.attachEvent) { 
	window.attachEvent("onload", errormsgbox_onload); 
} 

function errormsgbox_onload(){
	if (typeof(eDescripcion) != "undefined")
	{
		if (eDescripcion != "") {
			window.showModalDialog('errormsgbox.htm', window,'dialogHeight:160px;dialogWidth:500px;status:0;resizable:yes;scroll:0;help:0;unadorned:0');
			eDescripcion = "";
		}
		//alert( eDescripcion );
	}
}