function menuHorizontal_onresize() {
	var x;
	x = window.document.body.clientWidth - 16;
	div = window.document.all['divCloseMenuHorizontal'];
	div.style.left = x + 'px';
	div.attachEvent("onclick", divCloseMenuHorizontal_click);
}
function menuVertical_onresize() {
	var x;var y;
	x = window.document.body.clientWidth - 16;
	y = window.document.body.clientHeight - 16;
	div = window.document.all['divCloseMenuHorizontal'];
	div.style.left = x + 'px';
	div.style.top = y + 'px';
	div.attachEvent("onclick", divCloseMenuVertical_click); 
}
function divCloseMenuVertical_click() {
	parent.document.getElementById("MenuHorizontal").parentElement.rows="20,16,*";
	parent.document.getElementById("Sidebar").parentElement.cols="0,*";
	if (parent.document.getElementById("MenuHorizontal").contentWindow.arregloMenus) 
		{
			parent.document.getElementById("MenuHorizontal").contentWindow.RePos(null);
		}
	//alert(parent.parent.document.getElementById("Header"));
	parent.document.getElementById("Header").contentWindow.location = "defaultblank.aspx?url=CabeceraPequena";
	SetCookie("SondaNetMenu","MenuHorizontal",100);
}
function divCloseMenuHorizontal_click() {
	parent.document.getElementById("MenuHorizontal").parentElement.rows="60,0,*";
	parent.document.getElementById("Sidebar").parentElement.cols="204,*";
	if (parent.document.getElementById("Sidebar").contentWindow.arregloMenus)
		{
			parent.document.getElementById("Sidebar").contentWindow.RePos(null);
		}
		//parent.document.getElementById("Header").contentWindow.location.search
	parent.document.getElementById("Header").contentWindow.location = "defaultblank.aspx?url=CabeceraGrande";
	SetCookie("SondaNetMenu","MenuVertical",100);
}
function alCargarFrameset() {
	if (GetCookie("SondaNetMenu")) {
		if (GetCookie("SondaNetMenu") == "MenuHorizontal") {
			divCloseMenuVertical_click();
		}
	}
}