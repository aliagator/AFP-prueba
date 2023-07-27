var SV_STATUSBAR = "2.9.4.0";

if (document.all && window.attachEvent) { 
	window.attachEvent("onload", statusbar_onload); 
} 

function statusbar_onload(){
	if (typeof(statusBarText) != "undefined")
	{
		try {parent.frames("sidebar").statusText = statusBarText;} catch (e) {}
		try {parent.frames("Header").UserControl = UserControl;} catch (e) {}
		try {parent.frames("Header").FUN = FUN;} catch (e) {}
		try {parent.frames("Header").Modo = Modo;} catch (e) {}
	}
}