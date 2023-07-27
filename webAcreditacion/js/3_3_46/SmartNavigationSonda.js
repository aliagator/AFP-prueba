var SV_SMARTNAVIGATIONSONDA = "3.3.46";

$(document).ready(sms_onload);
$(window).scroll(sms_onscroll);

function sms_onload() {

	try {
		if (document.forms[0].__SCROLLPOS.value != "") theBody.scrollTop=document.forms[0].__SCROLLPOS.value;
		if (document.forms[0].__SCROLLPOS_X.value != "") theBody.scrollLeft=document.forms[0].__SCROLLPOS_X.value;

		var a;var i;

		a = window.document.body.getElementsByTagName("INPUT");
		for (i=0; i<a.length; i++) {
			if (a[i].type.toUpperCase() != "SUBMIT") {
				$(a[i]).focus(sms_onfocus);
			}
		}
		a = window.document.body.getElementsByTagName("SELECT");
		for (i=0; i<a.length; i++) $(a[i]).focus(sms_onfocus);

	    sms_onload_focus_control(document.forms[0].__LASTFOCUSEDCONTROL.value, false);
	} catch(e) {}
}


/*
 *  Factorizacion de conveniencia de sms_onload():
 *  Enfoca al control de id del parámetro "control_id", y opcionalmente
 *  selecciona su contenido si el parámetro "seleccionar" es verdadero
 */
function sms_onload_focus_control(control_id, seleccionar) {
    try {
        if (control_id == "") {
        	a = window.document.body.getElementsByTagName("*");
        	for (i=0; i<a.length; i++) {
        		var exit=0;
        		switch (a[i].tagName) {
        			case "INPUT":
        			    try {if (a[i].type.toUpperCase() != "SUBMIT") { a[i].focus(); exit=1; } } catch(e) {} finally { break; }
        			case "SELECT":
        			    try { a[i].focus(); exit=1; } catch(e) {} finally { break; }
        		}
        		if (exit == 1) break;
        	}
        } else {
    		if (control_id == document.forms[0].__LASTEVENTTARGET.value) {
    			sms_FocusNextControl(document.getElementById(control_id));
    		} else {
    	        document.getElementById(control_id).focus();
    	        if (typeof(seleccionar) != "undefined" && seleccionar) {
    		        document.getElementById(control_id).select();
    	        }
    		}
        }
	} catch(e) {}
}


// intenta setear el foco al siguiente control en el orden de tabIndex
// si falla intenta setear el foco al mismo control que lo tenia
// si esto falla el foco no queda seteado
function sms_FocusNextControl(control) {
	var exit = false;
	var tabIndex = control.tabIndex;
	var next=9999;
	var nextCtrl;
	var found = false;

	a = window.document.body.getElementsByTagName("*");

		while(exit==false) {

		exit = true;
		for (i=0; i<a.length; i++) {
			if (a[i].tabIndex)
				if (a[i].tabIndex>tabIndex)
					if (a[i].tabIndex<next) {
						nextCtrl = a[i];
						next = a[i].tabIndex;
						exit = false;
					}
		}

		if (exit == false)
			try {nextCtrl.focus();exit=true;found = true;} catch (e) {tabIndex=next;next=9999;exit=false;}
	}

	if (found == false)
		try {control.focus();} catch(e) {};
}

function sms_onscroll(){
	try {
		document.forms[0].__SCROLLPOS.value = theBody.scrollTop;
		document.forms[0].__SCROLLPOS_X.value = theBody.scrollLeft;
	} catch(e) {}
}

function sms_onfocus(){
	try {
		document.forms[0].__LASTFOCUSEDCONTROL.value = event.srcElement.id;
	} catch(e) {}
}
