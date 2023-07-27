var SV_SMARTNAVIGATIONSONDA = "2.9.4.0";

if (document.all && window.attachEvent) { 
	window.attachEvent("onload", sms_onload);
	window.attachEvent("onscroll", sms_onscroll);
}

function sms_onload() {

	if (document.forms[0].__SCROLLPOS.value != "") theBody.scrollTop=document.forms[0].__SCROLLPOS.value;

	var a;var i;
		
	a = window.document.body.getElementsByTagName("INPUT");
	for (i=0; i<a.length; i++) {
		if (a[i].type.toUpperCase() != "SUBMIT") {
			a[i].attachEvent("onfocus",sms_onfocus);
		}
	}
	a = window.document.body.getElementsByTagName("SELECT");
	for (i=0; i<a.length; i++) a[i].attachEvent("onfocus",sms_onfocus);
	
	if (document.forms[0].__LASTFOCUSEDCONTROL.value == "") {
		a = window.document.body.getElementsByTagName("*");
		for (i=0; i<a.length; i++) {
			var exit=0;
			switch (a[i].tagName){ 
				case "INPUT": try {if (a[i].type.toUpperCase()!="SUBMIT") {a[i].focus();exit=1;} } catch(e) { } finally {break;}
				case "SELECT":try {a[i].focus();exit=1;} catch(e) { } finally {break;}
			}
			if (exit == 1) break;
		}
	} else {
			if (document.forms[0].__LASTFOCUSEDCONTROL.value == document.forms[0].__LASTEVENTTARGET.value) {
				sms_FocusNextControl(document.getElementById(document.forms[0].__LASTFOCUSEDCONTROL.value));
			} else try {document.getElementById(document.forms[0].__LASTFOCUSEDCONTROL.value).focus()} catch(e) {}
	}
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
	document.forms[0].__SCROLLPOS.value = theBody.scrollTop;
}

function sms_onfocus(){
	document.forms[0].__LASTFOCUSEDCONTROL.value = event.srcElement.id;
}
