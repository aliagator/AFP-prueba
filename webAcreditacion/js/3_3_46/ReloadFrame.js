var SV_RELOADFRAME = "3.3.46"; 

function rf_menuHorizontal_onresize(pWidth) {
	var x;
	if (typeof(pWidth) != "undefined") {
		x = pWidth;
	} else {
		x = $(window).width() - 16;
	}
	div = window.document.getElementById('rf_divCloseMenuHorizontal');
	div.style.left = x + 'px';
	$(div).click(rf_divCloseMenuHorizontal_click);
	div.style.display = "block";
}
function rf_menuVertical_onresize(pWidth) {
	var x;var y;
	if (typeof(pWidth) != "undefined") {
		x = pWidth;
	} else {
		x = $(window).width() - 16;
	}
	y = $(window).height() - 16;
	div = window.document.getElementById('rf_divCloseMenuVertical');
	div.style.left = x + 'px';
	div.style.top = y + 'px';
	$(div).click(rf_divCloseMenuVertical_click); 
	div.style.display = "block";
}
function rf_divCloseMenuVertical_click(obj) {
	div = window.document.getElementById('rf_divCloseMenuVertical');
	if ($(div).attr("href")) {
		parent.location.href = $(div).attr("href");
	} else {
		parent.location.href = "framesetprincipal3h.htm";
	}
}
function rf_divCloseMenuHorizontal_click(obj) {
	div = window.document.getElementById('rf_divCloseMenuHorizontal');
	if ($(div).attr("href")) {
		parent.location.href = $(div).attr("href");
	} else {
		parent.location.href = "framesetprincipal3v.htm";
	}
}
function rf_alCargarFrameset() {
	if (GetCookie("SondaNetMenu")) {
		if (GetCookie("SondaNetMenu") == "MenuHorizontal") {
			rf_divCloseMenuVertical_click();
		}
	}
}

function rf_setIframeHeight(pWidth, pHeight) {

	var x;
	if (typeof(pWidth) != "undefined") {
		x = $(window).width() - pWidth;
	} else {
		x = $(window).width();
	}
	
	var y;
	if (typeof(pHeight) != "undefined") {
		y =  $(window).height() - pHeight;
	} else {
		y =  $(window).height();
	}

	$("#iframe_body").height(y);
	$("#iframe_body").width(x);
}

var rf_iframe_body;
var rf_scrollTop = 0;
var rf_scrollLeft = 0;
var rf_lastUserControl = '';
var rf_scrollTimeout = null;


function rf_onLoad(b) {
	rf_iframe_body = b;
    	
	//try { // por si da acceso denegado al presionar back del browser
		if (typeof b.contentWindow != 'undefined') {
			var cw = b.contentWindow;

			if (cw) {
				var resetScroll = true;
				// Verifica posicion de scrolls	
				if(typeof cw.userControl != 'undefined') {
					if (rf_lastUserControl != '') {
						if (rf_lastUserControl == cw.userControl) {
							//restore scrolls
							$(cw).scrollTop(rf_scrollTop);
							$(cw).scrollLeft(rf_scrollLeft);
							resetScroll = false;
						}
					}
					rf_lastUserControl = cw.userControl;
				}
				
				if (resetScroll == true) {
					$(cw).scrollTop(0);
					$(cw).scrollLeft(0);
					
				}
						
				$(cw).scroll(function() {rf_onScroll(b)});
			}
		}
	//} catch (e) {}
}

function rf_onScroll(){
	if (rf_scrollTimeout) clearTimeout(rf_scrollTimeout);
    rf_scrollTimeout = setTimeout(rf_saveScroll, 1);
}

function rf_saveScroll(){
	var b = rf_iframe_body;
	var cw = b.contentWindow;
	rf_scrollTop = $(cw).scrollTop();
	rf_scrollLeft = $(cw).scrollLeft();
}

