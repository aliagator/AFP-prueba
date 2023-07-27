var SV_ERRORMSGBOX = "3.100.52";

if (typeof($) != "function") {
  alert('Biblioteca JQuery no pudo ser cargada o no esta activa');
}
	
if (document.all && window.attachEvent) { 
	window.attachEvent("onload", errormsgbox_onload); 
}   

$(document).ready( function() {
	if (typeof(eDescripcion) != "undefined" || typeof(eDescripcionHTML) != "undefined")
	{
		window.showModalDialog('errormsgbox.htm', window,'dialogHeight:168px;dialogWidth:500px;status:0;resizable:yes;scroll:0;help:0;unadorned:0');
		eDescripcion = "";
	}
});