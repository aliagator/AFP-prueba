var SV_SMARTNAVIGATIONSONDA = "3.3.77";

$(document).ready(sms_onload);
$(window).scroll(sms_onscroll);

var sms_focusables;

function sms_onload() {

	sms_focusables = $(":focusable");

	try {
		if (document.forms[0].__SCROLLPOS.value != "") theBody.scrollTop=document.forms[0].__SCROLLPOS.value;
		if (document.forms[0].__SCROLLPOS_X.value != "") theBody.scrollLeft=document.forms[0].__SCROLLPOS_X.value;

		$("input, button, select").focus(sms_onfocus);
		
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
        			    try {if (a[i].type.toUpperCase() != "SUBMIT") { a[i].focus(); /*console.log("focus#1:" + a[i].id);*/ exit=1; } } catch(e) {} finally { break; }
        			case "SELECT":
        			    try { a[i].focus();  /*console.log("focus#2:" + a[i].id); */ exit=1; } catch(e) {} finally { break; }
        		}
        		if (exit == 1) break;
        	}
        } else {
        	//console.log("control_id:LastEventTarget " + control_id +":" + document.forms[0].__LASTEVENTTARGET.value);
    		if (control_id == document.forms[0].__LASTEVENTTARGET.value) {
    			sms_FocusNextControl(document.getElementById(control_id));
    		} else {
    	        document.getElementById(control_id).focus();
    	        //console.log("focus#3:" + control_id);
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

	  current = sms_focusables.index($(control));
	  next = sms_focusables.eq(current+1).length ? sms_focusables.eq(current+1) : sms_focusables.eq(0);
	  next.focus();
	  //console.log("focus_next#1:" + next.id)

}

function sms_onscroll(){
	try {
		document.forms[0].__SCROLLPOS.value = theBody.scrollTop;
		document.forms[0].__SCROLLPOS_X.value = theBody.scrollLeft;
	} catch(e) {}
}

function sms_onfocus(event){
	try {
		document.forms[0].__LASTFOCUSEDCONTROL.value = event.currentTarget.id;
		//console.log("sms_onfocus: " + event.currentTarget.id);
	} catch(e) {}
}
