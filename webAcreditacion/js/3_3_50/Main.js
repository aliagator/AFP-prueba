function doLayout() {
	if ($("#iframe_body")[0]) {
		var pos = $("#iframe_body").offset();
		
		 $($("#iframe_body")[0]).height(($(window).height() - HEADER_HEIGHT - H_MENU_HEIGHT));
		 
		 if (GetCookie("SondaNetMenu4")) {
			 if (GetCookie("SondaNetMenu4") == "MenuVertical") {
				$($("#iframe_body")[0]).height(($(window).height() - HEADER_HEIGHT ));
			 }
		 }
		
		$(rf_H).css("width",ICON_WIDTH);
		$(rf_H).css("heigth",ICON_HEIGHT);
		$(rf_H).css("top", HEADER_HEIGHT + H_MENU_HEIGHT - ICON_HEIGHT);
		$(rf_H).css("left",$(window).width() - ICON_WIDTH);
		$(rf_V).css("width",ICON_WIDTH);
		$(rf_V).css("heigth",ICON_HEIGHT);
		$(rf_V).css("top",$(window).height() - ICON_HEIGHT);
		$(rf_V).css("left", V_MENU_WIDTH - ICON_WIDTH );
		$("#info").css("top", HEADER_HEIGHT/2 - 8);
		$("#info").css("left", BANER_IZQ_WIDTH);
	}
	
}
						
function doLoad() {

	var IEVERSION = null;
	
	if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) { //test for MSIE x.x;
		IEVERSION = new Number(RegExp.$1) // capture x.x portion and store as a number
	}
	
	if (IEVERSION == 6) {
		window.location = 'framesetprincipal4h.htm';
	}

	doLayout();
	setTimeout(doLoad2,100);			
}

function doLoad2() {
	var horizontal = true;				
	if (GetCookie("SondaNetMenu4")) {
		if (GetCookie("SondaNetMenu4") == "MenuVertical") {
			rf_H_click();
			horizontal = false;
		}
	}
	if (horizontal) {
		rf_V_click()
	} 
	
	$(rf_H_img).click(rf_H_click);
	$(rf_V_img).click(rf_V_click);
	
	setTimeout(doLoadUserInfo,100);			
}

function rf_V_click() {
	$(rf_H).css("display","block");
	$(rf_V).css("display","none");
	$("#col1").css("width",0);
	$("#menu_h").css("height",H_MENU_HEIGHT);
	$("#k_menu_h").css("display","block");
	$("#k_menu_v").css("display","none");
	var now = new Date();var time = now.getTime();var expireTime = time + (3600 * 1000 * 24 * 365); now.setTime(expireTime);
	SetCookie("SondaNetMenu4", "MenuHorizontal",now);
	setTimeout(doRenderMenu,1);
}

function rf_H_click() {
	$(rf_H).css("display","none");
	$(rf_V).css("display","block");
	$("#col1").css("width",V_MENU_WIDTH); 
	$("#menu_h").css("height",0);
	$("#k_menu_h").css("display","none");
	$("#k_menu_v").css("display","block");
	$("#l_menu_v").css("width",V_MENU_WIDTH);
	var now = new Date();var time = now.getTime();var expireTime = time + (3600 * 1000 * 24 * 365);now.setTime(expireTime);
	SetCookie("SondaNetMenu4", "MenuVertical",now);
	setTimeout(doRenderMenu,1);
}

function doLoadMenu(filename) {
	$.ajax({
		url: filename,
		cache : true,
		dataType: "script",
		timeout: 180000,
		success: doRenderMenu
	});
}

var k_menu_h_loaded = false;
var k_menu_v_loaded = false;

function doRenderMenu() {
	
	var data = null;
	if (typeof (menuPrincipal_datasource) != "undefined") data = menuPrincipal_datasource;
	
	if (data != null) {

		var horizontal = true;
		 if (GetCookie("SondaNetMenu4")) {
			 if (GetCookie("SondaNetMenu4") == "MenuVertical") {
				horizontal = false;
			 }
		 }

		if (horizontal) {
			if (k_menu_h_loaded == false) {
				$("#k_menu_h").kendoMenu({
					select: function (e) {onMenuClick(e);this.close();},
					orientation: "horizontal", 
					hoverDelay: 300,
					dataSource: data,
					open: onSubMenuOpen});
				
				$("#k_menu_h").css("top", HEADER_HEIGHT);
				$("#k_menu_h").css("left", 0);
				$("#k_menu_h").find(".k-link").attr("target", "iframe_body");

				var mh = $("#k_menu_h").data("kendoMenu");
				mh.bind("activate", function(e) {
					$(e.item).find(".k-group").bgiframe();
					$(e.item).find("ul").css("overflow","visible");
				});

				k_menu_h_loaded = true;
			}
		} else {
			if (k_menu_v_loaded == false) {
				$("#k_menu_v").kendoMenu({
					select: function (e) {onMenuClick(e);this.close();},
					orientation: "vertical", 
					hoverDelay: 300,
					dataSource: data,
					open: onSubMenuOpen});
					
				$("#k_menu_v").css("top", HEADER_HEIGHT);
				$("#k_menu_v").css("left", 0);
				$("#k_menu_v").css("width", V_MENU_WIDTH);
				$("#k_menu_v").find(".k-link").attr("target", "iframe_body");
				
				var mv = $("#k_menu_v").data("kendoMenu");
				mv.bind("activate", function(e) {
					$(e.item).find(".k-group").bgiframe();
					$(e.item).find("ul").css("overflow","visible");
				});

				
				k_menu_v_loaded = true;
			}
		}
		
	}
	
	doLayout();
}

function onSubMenuOpen(e) {
	var mnu = $(e.item.children[1]).text();
	var mnuarr = mnu.split(":");
	if (mnuarr[0] == "MNU") {
		//$(e.item.children[1].firstChild).css("display","none");
		//e.item.children[1].firstChild.innerText = '';
		$(e.item.children[1]).text("");
		var menu =  e.sender; // k-menu-object
		var items = eval(mnuarr[1]);
		menu.append(items, e.item);
		$(e.item).find(".k-link").attr("target", "iframe_body");
		$(e.item).find(".k-group").bgiframe();
		var w = $(e.item).find(".k-group").width();
		$(e.item).children().find(".k-link").width(w);
		
	}
	
}

var doDelayedLayout_ort = null;
function doDelayedLayout() {
	if (doDelayedLayout_ort) clearTimeout(doDelayedLayout_ort);
	doDelayedLayout_ort = setTimeout(doLayout, 100);
}

function doLoadUserInfo() {
	$.ajax({
	type: "POST",
	url: "SondaNet.asmx/Usuario",
	data: "",
	dataType: "xml",
	timeout: 180000,
	success: doProcessUserInfo
	});
	
}

function doProcessUserInfo(data) {

	var idUsuario = $(data).find('idUsuario').text();
	var Nombre = $(data).find('Nombre').text();
	var menuFilename = $(data).find('menuFilename').text();
	if (Nombre != "") {
		$("#nombre").text(Nombre);
		$("#info").css("display","block");
	}				
	
	if (menuFilename != "") {
		doLoadMenu(menuFilename);
	}
}


function onMenuClick(e) {
	if (typeof (e.item.firstChild.href) != "undefined") {
		sntShowClock();
	}
}

	
/**************************************************************************************************************
** muestra y oculta reloj
**************************************************************************************************************/

var spinnerVisible = false;
function sntShowClock() {
	if (!spinnerVisible) {
            $("div#spinner").fadeIn("fast");
            spinnerVisible = true;
	}
}

function sntHideClock() {
	 if (spinnerVisible) {
            var spinner = $("div#spinner");
            spinner.stop();
            spinner.fadeOut("fast");
            spinnerVisible = false;
     }
}

$(window).unload(function() {
  sntShowClock();
});

/***************************************************************************************************************/

function sntIframeLoaded() {
	sntHideClock();
}

function sntLoadPage(url) {
	sntShowClock();
	$("#iframe_body").attr("src",url);
}