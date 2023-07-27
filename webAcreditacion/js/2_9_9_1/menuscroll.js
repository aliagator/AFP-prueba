var SV_MENUSCROLL = "2.9.9.1";
scroll_nav = (document.layers) ? true : false;
scroll_iex = (document.all) ? true : false;

function scroll_up(ctlID) {
	var ctl = document.getElementById(ctlID);
	if(ctl.scroll_pos > ctl.scroll_upr) ctl.scroll_pos -= ctl.scroll_stp;
	if(ctl.scroll_pos < ctl.scroll_upr) ctl.scroll_pos = ctl.scroll_upr;
	do_scroll(ctl,ctl.scroll_pos);
	ctl.scroll_tim = setTimeout("scroll_up('" + ctlID + "')", ctl.scroll_spd);
}

function scroll_dn(ctlID) {
	var ctl = document.getElementById(ctlID);
	if(ctl.scroll_pos < ctl.scroll_lwr) ctl.scroll_pos += ctl.scroll_stp;
	if(ctl.scroll_pos > ctl.scroll_lwr) ctl.scroll_pos = ctl.scroll_lwr;
	do_scroll(ctl,ctl.scroll_pos);
	ctl.scroll_tim = setTimeout("scroll_dn('" + ctlID + "')", ctl.scroll_spd);
}

function do_scroll(ctl,pos) {
	if(scroll_iex) ctl.style.top = pos;
	if(scroll_nav) ctl.top = pos;
	ctl.scroll_pos = pos;
}

function no_scroll(ctlID) {
	var ctl = document.getElementById(ctlID);
	clearTimeout(ctl.scroll_tim);
}

/*  INI - CAMBIO CODIGO
    ORIGINAL:
function scroll_build(ctlID) {
    AGREGADO: */
function scroll_build(ctlID, divUpID, divDnID) {
/*  FIN - CAMBIO CODIGO */
	var ctl = document.getElementById(ctlID);
	ctl.scroll_pos = 0;     // initial top position
	ctl.scroll_stp = 10;    // step increment size
	ctl.scroll_spd = 50;    // speed of increment
	ctl.scroll_upr = -5000; // upper limiter
	ctl.scroll_lwr = 0;    // lower limiter
	ctl.scroll_tim;         // timer variable
/*  INI - CAMBIO CODIGO
    AGREGADO: */
    var divup = document.getElementById(divUpID);
    var divdn = document.getElementById(divDnID);
/*  FIN - CAMBIO CODIGO */
}


/*  INI - CAMBIO CODIGO
    ORIGINAL:
function scroll_repos(ctlID,divbotID) {
	var ctl = document.getElementById(ctlID);
	var divbot = document.getElementById(divbotID);

	try {
		ctl.scroll_upr = window.frameElement.offsetHeight - ctl.clientHeight - 40;
		if(scroll_iex) divbot.style.top = window.frameElement.offsetHeight - 20;
		if(scroll_nav) divbot.top = window.frameElement.offsetHeight - 20;


	}
	catch (e) {}
}
    AGREGADO: */
function scroll_repos_vert(ctlID, divUpId, divDnId) {
	var ctl = document.getElementById(ctlID);
	var divup = document.getElementById(divUpId);
	var divdn = document.getElementById(divDnId);

	try {
		ctl.scroll_upr = window.frameElement.offsetHeight - ctl.clientHeight - 40;
		if(scroll_iex) {
		    with (divup.style) {
                left = document.body.clientWidth / 2 + "px";
                width = document.body.clientWidth / 2 + "px";
                top = (document.body.clientHeight - 20) + "px";
		    }
		    with (divdn.style) {
                left = "0px";
                width = document.body.clientWidth / 2 + "px";
                top = (document.body.clientHeight - 20) + "px";
		    }
		}
		if(scroll_nav) {
		    with (divup) {
                left = document.body.clientWidth / 2;
                width = document.body.clientWidth / 2;
                top = document.body.clientHeight - 20;
		    }
		    with (divdn) {
                left = "0px";
                width = document.body.clientWidth / 2;
                top = document.body.clientHeight - 20;
		    }
		}
	}
	catch (e) {}
}
/*  FIN - CAMBIO CODIGO */

