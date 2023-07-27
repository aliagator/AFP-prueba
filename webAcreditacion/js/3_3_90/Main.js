var iframeBody, innerDoc,
    anchoMenuVertical = 250;

function doLayout() {
	if ($("#iframe_body")[0]) {
		var pos = $("#iframe_body").offset();
		
		$($("#iframe_body")[0]).height(($(window).height() - HEADER_HEIGHT));
		
		var vertical = true;

		 if (GetCookie("SondaNetMenu4")) {
			 if (GetCookie("SondaNetMenu4") == "MenuHorizontal") {
			     $($("#iframe_body")[0]).height(($(window).height() - HEADER_HEIGHT - H_MENU_HEIGHT));
			     vertical = false;
			 }
		 }

		$(rf_H).css("width",ICON_WIDTH);
		$(rf_H).css("heigth",ICON_HEIGHT);
		$(rf_H).css("top", HEADER_HEIGHT + H_MENU_HEIGHT - ICON_HEIGHT);
		$(rf_H).css("left",$(window).width() - ICON_WIDTH);
		$(rf_V).css("width",ICON_WIDTH);
		$(rf_V).css("heigth",ICON_HEIGHT);
		$(rf_V).css("top",$(window).height() - ICON_HEIGHT);
		$(rf_V).css("left", anchoMenuVertical - ICON_WIDTH );
		$("#info").css("top", HEADER_HEIGHT/2 - 8);
		$("#info").css("left", BANER_IZQ_WIDTH);

		if (vertical) { rf_H_click(); };
		

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
	// $(rf_H).css("display","none");
	// $(rf_V).css("display","none");
	$("#col1").css("width",0);
	$("#menu_h").css("height",H_MENU_HEIGHT);
	$("#k_menu_h").css("display","none");
	$("#k_menu_v").css("display", "none");
	$("#k_panelbar").css("display", "none");
	var now = new Date();var time = now.getTime();var expireTime = time + (3600 * 1000 * 24 * 365); now.setTime(expireTime);
	SetCookie("SondaNetMenu4", "MenuHorizontal",now);
	setTimeout(doRenderMenu,1);
}

function rf_H_click() {
	$(rf_H).css("display","none");
	$(rf_V).css("display", "none");
	if (V_MENU_SCROLL_VISIBLE == false) { $("#col1").css("width", anchoMenuVertical) } else { $("#col1").css("width", anchoMenuVertical + V_SCROLL_WIDTH) }
	$("#menu_h").css("height",0);
	$("#k_menu_h").css("display","none");
	$("#k_menu_v").css("display","none");
	$("#l_menu_v").css("width", anchoMenuVertical);
	$("#k_panelbar").css("display", "block");
	$("#l_panelbar").css("width", anchoMenuVertical);
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

var k_menu;
var k_menu_h_loaded = false;
var k_menu_v_loaded = false;

var k_menu_last_mode = null;

var V_MENU_SCROLL_VISIBLE = false;



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
		 
		if (k_menu_last_mode == null || k_menu_last_mode != horizontal) {
	
			k_menu_last_mode = horizontal; 
			
			if (horizontal) {
				if (k_menu_h_loaded == false) {
					$("#k_menu_h").kendoMenu({
						select: function (e) {if (onMenuClick(e)==true) {this.close();};},
						orientation: "horizontal", 
						hoverDelay: 300,
						dataSource: data,
						animation: false,
						open: onSubMenuOpen,
						close: onSubMenuClose});
					
					$("#k_menu_h").css("top", HEADER_HEIGHT);
					$("#k_menu_h").css("left", 0);
					$("#k_menu_h").find(".k-link").attr("target", "iframe_body");

					k_menu = $("#k_menu_h").data("kendoMenu");
					k_menu.bind("activate", function(e) {
						$(e.item).children(".k-animation-container").bgiframe();
						$(e.item).find("ul").css("overflow","visible");
					});

					k_menu_h_loaded = true;

				}
			} else {
				if (k_menu_v_loaded == false) {
					
					$("#k_panelbar").kendoPanelBar({
						select: function (e) {onMenuClick(e);},
						expandMode: "multiple",
						hoverDelay: 300,
						dataSource: data,
						animation: false,
						expand: onPanelBarOpen,
						collapse: onPanelBarClose
					});
						
					$("#k_panelbar").css("top", HEADER_HEIGHT);
					$("#k_panelbar").css("left", 0);
					$("#k_panelbar").css("width", anchoMenuVertical);
					$("#k_panelbar_container").css("width", anchoMenuVertical);
					$("#k_panelbar").find(".k-link").attr("target", "iframe_body");
				
					$("#k_panelbar").data("kendoPanelBar").expand(">li:first");

					k_menu_v_loaded = true;
				}
			}
		
		doLayout();	
		toggleMenu();
		}
	}
	
	
}

function onPanelBarOpen(e) {

    var item = $(e.item);
    var menuElement = item.closest(".k-menu");

    $(e.item).find(".k-group").css("top", 0);
    var menu = e.sender; // k-menu-object
    var mnu = $(e.item.children[1]).text();
    var mnuarr = mnu.split(":");
    
    if (mnuarr[0] == "MNU") {

        var jsStr = mnuarr[1];

        index = item.parentsUntil(menuElement, ".k-item").map(function () {
            return $(this).index();
        }).get().reverse();
        index.push(item.index());

        var nodeIndex = index.join(".") + ".0";

        var level = nodeIndex.split(".").length - 1;

        var items = eval(jsStr);

        menu.append(items, e.item);
        $(e.item).find(".k-link").attr("target", "iframe_body");

        var htmlItems = $(e.item).find(".k-link");
        
        for (var i = 0, len = htmlItems.length; i < len; i++) {
            htmlItem = htmlItems[i];
            if (htmlItem.innerText.split(":")[0] != "MNU") {
                $(htmlItem).prop("title", htmlItem.innerText);
                $(htmlItem).addClass("k-panelbar-l" + level);
            }
        }
        
        var w = $(e.item).find(".k-group").width();

        menu.remove(sntGetMenuItem(menu, nodeIndex));
    }

    setTimeout(panelBarLayout, 100);

}

function onPanelBarClose(e) {
    setTimeout(panelBarLayout, 100);
}

function panelBarLayout() {


    var kgroup = $("#k_panelbar");
    var h1 = kgroup.height();

    var doc_H = $(window).height();
    var menu_T = $("#k_panelbar_container").offset().top;

    var h2 = doc_H - menu_T;
    $("#k_panelbar_container").css("height", h2);


    if (h1 > h2) {
        $("#k_panelbar_container").css("overflow-y", "visible");
        $("#col1").css("width", anchoMenuVertical + V_SCROLL_WIDTH );
        $("#k_panelbar_container").css("width", anchoMenuVertical + V_SCROLL_WIDTH + 2);
        V_MENU_SCROLL_VISIBLE = true;
    } else {
        $("#k_panelbar_container").css("overflow-y", "hidden");
        $("#col1").css("width", anchoMenuVertical  );
        $("#k_panelbar_container").css("width", anchoMenuVertical + 2 );
        V_MENU_SCROLL_VISIBLE = false;
    }


    /* Mantiene el alto del panelbar dentro de los limites de la pantalla */
    /*
    var q1 = $("#k_panelbar").children("li");
    q1.css("height", "");
    
    var panel_H;
    var menu_H;
    var menu_T = $("#k_panelbar").offset().top;
    var doc_H = $(window).height();

    panel_H = $("#k_panelbar").height();

    var selected_H = doc_H - panel_H;

    var q2 = $("#k_panelbar").children(".k-state-active");
    var actual_H = q2.height();
    q2.css("height", actual_H + selected_H - menu_T);
    */
}


function onSubMenuOpen(e) {

	var item = $(e.item);
	var menuElement = item.closest(".k-menu");

	$(e.item).find(".k-group").css("top",0);
	var menu =  e.sender; // k-menu-object
	var mnu = $(e.item.children[1]).text();
	var mnuarr = mnu.split(":");
	if (mnuarr[0] == "MNU") {
		
		var jsStr = mnuarr[1];
		
		index = item.parentsUntil(menuElement, ".k-item").map(function () {
            return $(this).index();
        }).get().reverse();
		index.push(item.index());
		
		var nodeIndex = index.join(".") + ".0";
		
		var items = eval(jsStr);
		
		menu.append(items, e.item);
		$(e.item).find(".k-link").attr("target", "iframe_body");
				
		var w = $(e.item).find(".k-group").width();
		$(e.item).children().find(".k-link").width(w);
		
		menu.remove(sntGetMenuItem(menu,nodeIndex)); 
		
		$(e.item).find(".k-group").on('DOMMouseScroll mousewheel', function (e) {

			var dx = e.originalEvent.wheelDelta / 2;
		
			var kgroup = $(e.currentTarget).closest(".k-group");
			if (kgroup.data("enable-scroll") === true) {
			
				var h = kgroup.data("menuheight");
				var delta =  kgroup.data("delta");
								
				var iframe = $(e.currentTarget).closest("iframe");
				var top = parseInt(kgroup.css('top'), 10);
				if (isNaN(top)) {top = 0};
				var newTop = top + dx;
				
				if (dx < 0 ) { 
					if (newTop > 0) {
						iframe.css("top",newTop);
						kgroup.css("top",newTop);
					} else {
						iframe.css("top",0);
						kgroup.css("top",0);
					}
				}
				
				 if (dx > 0 ) { 
				     if (newTop < delta ) {
						 iframe.css("top",newTop);
						 kgroup.css("top",newTop);
					 } else {
						 iframe.css("top",delta);
						 kgroup.css("top",delta);
					 }
				 }
				
				
				
			}
		  
		  return false;
		});

	}

	var p = menuElement.position();
	var kgroup = $(e.item).find(".k-group");
	var h = kgroup.height();
	
	if (h > 0) {
		kgroup.data("menuheight", h);
	} else {
		h = kgroup.data("menuheight");
	}
	 
	 var delta;
	 if (menu.options.orientation == "horizontal") {
		var y = p.top + menuElement.height() + 4;
		delta =  -1 * ($(document).height() - (h + y)) ;
	 } else {
		
		if (h > $(document).height()) {
			delta = ( h -$(document).height()/2 - p.top - 100) ;
			var t = kgroup.offset().top;
			
		} else {
			delta = 0;
		}
	}
	 	 	 
	 kgroup.data("delta",delta);
	 	 	 	 
	 if (delta > 0) {
		kgroup.css("top",delta);
		$(e.item).find("iframe").css("top", delta );
		kgroup.data("enable-scroll",true);
	  } else {
		 kgroup.data("enable-scroll",false);
	  }
	
}



function onSubMenuClose(e) {
		// uso futuro
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
		return true;
	} else {
		return false;
	}
}

	
/**************************************************************************************************************
** muestra y oculta reloj
**************************************************************************************************************/

var spinnerVisible = false;

function sntShowClock() {

	if (!spinnerVisible) {
			/*
            $("div#spinner").fadeIn("fast");
            spinnerVisible = true;
			*/
            /* Por ahora no se anima el logo
			$("#logo").attr('src','img/sonda/alogo.gif'); 
			spinnerVisible = true;
            */
	}

}

function sntHideClock() {

	 if (spinnerVisible) {
			/*
            var spinner = $("div#spinner");
            spinner.stop();
            spinner.fadeOut("fast");
            spinnerVisible = false;
			*/
			$("#logo").attr('src','img/sonda/slogo.gif'); 
			spinnerVisible = false;
     }

}

$(window).on("unload", function() {
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

function sntGetMenuItem(menu, target) {
	var itemIndexes = target.split(/[.,]/),
		item = menu.element;

	if (itemIndexes[0] !== "") {
		for (var i = 0, len = itemIndexes.length; i < len; i++) {
			item = item.children("li").eq(itemIndexes[i]);
			if (i < len-1) {
				item = item.find("ul:first");
			}
		}
	}

	return item;
}


/*!
Math.uuid.js (v1.4)
http://www.broofa.com
mailto:robert@broofa.com

Copyright (c) 2010 Robert Kieffer
Dual licensed under the MIT and GPL licenses.
*/

/*
* Generate a random uuid.
*
* USAGE: Math.uuid(length, radix)
*   length - the desired number of characters
*   radix  - the number of allowable values for each character.
*
* EXAMPLES:
*   // No arguments  - returns RFC4122, version 4 ID
*   >>> Math.uuid()
*   "92329D39-6F5C-4520-ABFC-AAB64544E172"
*
*   // One argument - returns ID of the specified length
*   >>> Math.uuid(15)     // 15 character ID (default base=62)
*   "VcydxgltxrVZSTV"
*
*   // Two arguments - returns ID of the specified length, and radix. (Radix must be <= 62)
*   >>> Math.uuid(8, 2)  // 8 character ID (base=2)
*   "01001010"
*   >>> Math.uuid(8, 10) // 8 character ID (base=10)
*   "47473046"
*   >>> Math.uuid(8, 16) // 8 character ID (base=16)
*   "098F4D35"
*/
(function () {
    // Private array of chars to use
    var CHARS = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');

    Math.uuid = function (len, radix) {
        var chars = CHARS, uuid = [], i;
        radix = radix || chars.length;

        if (len) {
            // Compact form
            for (i = 0; i < len; i++) uuid[i] = chars[0 | Math.random() * radix];
        } else {
            // rfc4122, version 4 form
            var r;

            // rfc4122 requires these characters
            uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
            uuid[14] = '4';

            // Fill in random data.  At i==19 set the high bits of clock sequence as
            // per rfc4122, sec. 4.1.5
            for (i = 0; i < 36; i++) {
                if (!uuid[i]) {
                    r = 0 | Math.random() * 16;
                    uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
                }
            }
        }

        return uuid.join('');
    };

    // A more performant, but slightly bulkier, RFC4122v4 solution.  We boost performance
    // by minimizing calls to random()
    Math.uuidFast = function () {
        var chars = CHARS, uuid = new Array(36), rnd = 0, r;
        for (var i = 0; i < 36; i++) {
            if (i == 8 || i == 13 || i == 18 || i == 23) {
                uuid[i] = '-';
            } else if (i == 14) {
                uuid[i] = '4';
            } else {
                if (rnd <= 0x02) rnd = 0x2000000 + (Math.random() * 0x1000000) | 0;
                r = rnd & 0xf;
                rnd = rnd >> 4;
                uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
            }
        }
        return uuid.join('');
    };

    // A more compact, but less performant, RFC4122v4 solution:
    Math.uuidCompact = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
})();


function toggleMenu(){

    // rf_V_click();
	
	// Menú Vertical
	var menu = $('#menu_v');

	// Agrega clase a menú para identificar que está visible
	menu.addClass('visible');
	
	// Contenido - td siguiente
	var content = menu.next('td');
	
	// Agrega botón al menú
	menu.closest('table').before('<span id="btn-toggle-menu"></span>');
	
	// Modifica CSS menú
	menu.css({
		position: 'relative',
		width: menu.width()
	});

	// Selecciona boton para ocultar
	var btnToggleMenu = $('#btn-toggle-menu');
	
	// CSS botón
	btnToggleMenu.css({
		display: 'block',
		width: 15,
		height: 40,
		position: 'fixed',
		top: menu.offset().top,
		left: menu.outerWidth(true),
		backgroundColor: '#13548c',
		borderRadius: '0px 5px 5px 0px',
		cursor: 'pointer',
		zIndex: 999
	});

	// Ícono botón
	btnToggleMenu.append('<span id="arrow-toggle-menu"></span>');

	// Ícono botón
	var arrowToggleMenu = $('#arrow-toggle-menu');

	// CSS ícono
	var iconoIzq = {
		display: 'block',
		width: 0,
		height: 0,
		borderTop: '5px solid transparent',
		borderBottom: '5px solid transparent',
		borderLeft: '5px solid transparent',
		borderRight: '5px solid white',
		position: 'absolute',
		left: 0,
		top: 15
	}

	var iconoDer = {
		display: 'block',
		width: 0,
		height: 0,
		borderTop: '5px solid transparent',
		borderBottom: '5px solid transparent',
		borderLeft: '5px solid white',
		borderRight: '5px solid transparent',
		position: 'absolute',
		left: 4,
		top: 15
	}
	
	arrowToggleMenu.css(iconoIzq);

	btnToggleMenu.on('click', function(){
		// Si está visible lo oculta...
		if(menu.hasClass('visible')){
			
			menu.css('display', 'none');
			menu.removeClass('visible');
			content.attr('colspan', 2);
			btnToggleMenu.css('left', 0);
			arrowToggleMenu.css(iconoDer);
		} else {
			menu.css('display', 'block');
			menu.addClass('visible');
			content.attr('colspan', 1);
			btnToggleMenu.css('left', menu.width());
			arrowToggleMenu.css(iconoIzq);
		}
	});
}

// Elimina elementos entre infobar y tabla contenido principal
$(document).ready(function(){
    $("#iframe_body").on('load', function(){
        iframeBody = $("#iframe_body")[0];
        innerDoc = (iframeBody.contentDocument) ? iframeBody.contentDocument : iframeBody.contentWindow.document;

		$('form > p', innerDoc).remove();
		
        $('form link', innerDoc).remove();

    });
});