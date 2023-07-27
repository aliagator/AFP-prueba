var SV_ERRORMSGBOX = "3.3.73";

if (typeof($) != "function") {
  alert('Biblioteca JQuery no pudo ser cargada o no esta activa');
}

$(document).ready( function() {
	if (typeof(eDescripcion) != "undefined" || typeof(eDescripcionHTML) != "undefined")
	{
		window.showModalDialog('js/' + SV_ERRORMSGBOX.replace(/\./g,'_') + '/errormsgbox.htm', window,'dialogHeight:168px;dialogWidth:500px;status:0;resizable:yes;scroll:0;help:0;unadorned:0');
		eDescripcion = "";
	}
});