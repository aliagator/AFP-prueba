var SV_CBOBOXEDITABLESCRIPT = "2.9.4.0";

if (document.all && window.attachEvent) { 
	window.attachEvent("onload", dropdownlistsnd_onresize); 
	window.attachEvent("onresize", dropdownlistsnd_onresize); 
} 

function dropdownlistsnd_onresize(){

	if (typeof(arrayDropDownListSnd) != "undefined")
	{
		var i;
		for (i = 0; i < arrayDropDownListSnd.length; i++) {
			
			var id = arrayDropDownListSnd[i];
			var clist = document.all[id];
			var cbut = document.all[id + '_button'];
			var cdiv = document.all[id + '_divimg'];
			var ctext = document.all[id + '_text'];
			var cimg = document.all[id + '_img'];
			
			cdiv.style.visibility = "VISIBLE";			
			cdiv.style.width = ctext.offsetHeight - 4 + "px";
			cdiv.style.height = ctext.clientHeight  + "px";
			cbut.style.height = ctext.clientHeight + "px";		
			cdiv.style.left = ctext.offsetWidth + getRealLeft(ctext) - cdiv.offsetWidth - 2 + "px"  ;
			cimg.style.left = (cdiv.offsetWidth - cimg.offsetWidth)/2 + "px";
			cimg.style.top =  (cdiv.offsetHeight - cimg.offsetHeight)/2 + "px";
			cbut.style.top = 1 + "px";		
			cbut.style.left = 1 + "px";		
			cdiv.style.top = getRealTop(ctext)  + 2 + "px";
					
			hidelist(clist);
		}
	}	 

}

function dropdownlistsnd_hideall(){

	if (typeof(arrayDropDownListSnd) != "undefined")
	{
		var i;
		for (i = 0; i < arrayDropDownListSnd.length; i++) {
			
			var id = arrayDropDownListSnd[i];
			var clist = document.all[id];
					
			hidelist(clist);
		}
	}	 

}
function showhidelist(control){
	
	dropdownlistsnd_hideall()
	
	var clist = document.all[control.id];
		
	if (typeof(clist.isListVisible) == "undefined") clist.isListVisible = false;
	
	if (clist.isListVisible == true) {
		hidelist(control);
	} else {
		showlist(control);
	}
}

function listClick(control){
	
	
	
	var llist = control.id;
	var ttext = control.id + '_text';
	var i = document.getElementById(llist).selectedIndex;
	var ctext = document.getElementById(ttext);
	
	ttext.oldValue = ttext.value;

	checkLoaded(document.all(llist));
	
	ctext.value = document.getElementById(llist).options[i].text;
	ctext.focus();
	if (document.forms[0].__LASTFOCUSEDCONTROL) {document.forms[0].__LASTFOCUSEDCONTROL.value = ttext} // SmartNavigationSonda
	hidelist(control);
	findMatch_doPostBack(ctext);
}

function hidelist(control){

	var ldiv = control.id + '_div';
	var ttext = control.id + '_text';
	var clist = document.all[control.id];
				
	document.getElementById(ldiv).style.left = - document.getElementById(ttext).offsetWidth + "px";
	document.getElementById(ldiv).style.top = - document.getElementById(ldiv).offsetHeight + "px";
	document.getElementById(ldiv).style.visibility = "Hidden";
	clist.isListVisible = false;
}

function showlist(control){
			
	var ldiv = control.id + '_div';
	var llist = control.id;
	var ttext = control.id + '_text';
	var clist = document.all[llist];
	
	checkLoaded(document.all(llist));
		
	document.getElementById(ldiv).style.left =  getRealLeft(document.getElementById(ttext)) +"px";
	document.getElementById(ldiv).style.top =  (getRealTop(document.getElementById(ttext)) + document.getElementById(ttext).offsetHeight) + "px";
	document.getElementById(ldiv).style.width = document.getElementById(ttext).offsetWidth + "px";
	document.getElementById(llist).style.width = document.getElementById(ttext).offsetWidth +" px";
	
	document.getElementById(ldiv).style.visibility = "Visible";
		
	clist.isListVisible = true;
}

function getRealLeft(el) {
    xPos = el.offsetLeft;
    tempEl = el.offsetParent;
    while (tempEl != null) {
        xPos += tempEl.offsetLeft;
        tempEl = tempEl.offsetParent;
    }
    return xPos;
}

function getRealTop(el) {
    yPos = el.offsetTop;
    tempEl = el.offsetParent;
    while (tempEl != null) {
        yPos += tempEl.offsetTop;
        tempEl = tempEl.offsetParent;
    }
    return yPos;
}

function setSelectionRange(input, selectionStart, selectionEnd) {
    if (input.setSelectionRange) {
        input.setSelectionRange(selectionStart, selectionEnd);
    }
    else if (input.createTextRange) {
        var range = input.createTextRange();
        range.collapse(true);
        range.moveEnd("character", selectionEnd);
        range.moveStart("character", selectionStart);
        range.select();
    }
    input.focus();
}

function setCaretToPosition(input, position) {
    setSelectionRange(input, position, position);
}

function replaceSelection (input, replaceString) {
	var len = replaceString.length;
    if (input.setSelectionRange) {
        var selectionStart = input.selectionStart;
        var selectionEnd = input.selectionEnd;

        input.value = input.value.substring(0, selectionStart) + replaceString + input.value.substring(selectionEnd);
		input.selectionStart  = selectionStart + len;
		input.selectionEnd  = selectionStart + len;
    }
    else if (document.selection) {
        var range = document.selection.createRange();
		var saved_range = range.duplicate();

        if (range.parentElement() == input) {
            range.text = replaceString;
			range.moveEnd("character", saved_range.selectionStart + len);
			range.moveStart("character", saved_range.selectionStart + len);
			range.select();
        }
    }
    input.focus();
}

function autocompleteMatch (selectBox, text, values) {

    re = new RegExp("\n>"+ text ,'i');
    if(re.test(values)){
		 var endString = RegExp.rightContext.substr(0,RegExp.rightContext.indexOf("<"));
         RegExp.rightContext.search(/<(\d+);/)
         selectBox.selectedIndex = RegExp.$1*1;
         return selectBox.options[selectBox.selectedIndex].text;
	} 

    return null;
}

//function autocomplete(textbox, event, values) {
function findMatch(selectBox, textbox, event) {

	checkLoaded(selectBox);
	findMatch_fillAllSel(selectBox);
  
    if (textbox.setSelectionRange || textbox.createTextRange) {
        switch (event.keyCode) {
            case 38:    // up arrow
			case 40:    // down arrow
            case 37:    // left arrow
            case 39:    // right arrow
            case 33:    // page up
            case 34:    // page down
            case 36:    // home
            case 35:    // end
            case 13:    // enter
            case 9:     // tab
            case 27:    // esc
            case 16:    // shift
            case 17:    // ctrl
            case 18:    // alt
            case 20:    // caps lock
            case 8:     // backspace
            case 46:    // delete
                return true;
                break;

            default:
                var c = String.fromCharCode(
                    (event.charCode == undefined) ? event.keyCode : event.charCode
                );
                replaceSelection(textbox, c);
                sMatch = autocompleteMatch(selectBox, textbox.value, selectBox.allSel);
                var len = textbox.value.length;
				
                if (sMatch != null) {
                    textbox.value = sMatch;
                    setSelectionRange(textbox, len, textbox.value.length);
                }
                
                return false;
        }
    }
    else {
        return true;
    }
}

function trim(str)
{
   return str.replace(/^\s*|\s*$/g,"");
}

// retorna verdadero cuando el valor ha cambiado
function findMatch_onBlur(selectBox, textbox, event) {

	/*checkLoaded(selectBox);*/
	
	if (selectBox.selectedIndex != null) 
		if (selectBox.selectedIndex != -1)
			textbox.value = selectBox.options[selectBox.selectedIndex].text;
	
	if (selectBox.selectedIndex == -1)
		textbox.value = textbox.oldValue;
		
	return findMatch_doPostBack(textbox);

}

function findMatch_onFocus(selectBox, textbox, event) {
	/*checkLoaded(selectBox);*/
	setSelectionRange(textbox, 0, textbox.value.length);
	textbox.oldValue = textbox.value;
}

function findMatch_doPostBack(textbox) {

	if (typeof(textbox.oldValue) != "undefined") 
		if (trim(textbox.oldValue.toUpperCase()) == trim(textbox.value.toUpperCase())) return false;
	
	if (textbox.AutoPostBack.toUpperCase() == "TRUE") 
		if (textbox.PostBackFunction) 
			{eval(textbox.PostBackFunction);return true;}
}


function findMatch_fillAllSel(selectBox) {
	if (!selectBox.allSel) {
     {
		selectBox.allSel = '';
		for(si=0;si<selectBox.length;si++)
			selectBox.allSel += '\n>'+selectBox.options[si].text+'<'+si+';'
		}	
	}
}

function findMatch_onKeyDown(selectBox, textbox, event) {
	
	checkLoaded(selectBox);
	findMatch_fillAllSel(selectBox);
	
	switch (event.keyCode) {
            case 38:    // up arrow
            case 33:    // page up
				if (selectBox.selectedIndex > 0) {
					selectBox.selectedIndex -= 1;
					textbox.value = selectBox.options[selectBox.selectedIndex].text;
					setSelectionRange(textbox, 0, textbox.value.length);
				}
				return false;
				break;
            case 40:    // down arrow
            case 34:    // page down
            	if (selectBox.selectedIndex < (selectBox.length - 1)) {
					selectBox.selectedIndex += 1;
					textbox.value = selectBox.options[selectBox.selectedIndex].text;
					setSelectionRange(textbox, 0, textbox.value.length);
				}
				return false;
				break;
            case 36:    // home
            	selectBox.selectedIndex = 0;
				textbox.value = selectBox.options[selectBox.selectedIndex].text;
				setSelectionRange(textbox, 0, textbox.value.length);
				return false;
				break;
            case 35:    // end
				selectBox.selectedIndex = (selectBox.length - 1);
				textbox.value = selectBox.options[selectBox.selectedIndex].text;
				setSelectionRange(textbox, 0, textbox.value.length);
				return false;
				break;
            case 27:    // esc
				textbox.value = textbox.oldValue;
				autocompleteMatch(selectBox,textbox,selectBox.allSel);
				setSelectionRange(textbox, 0, textbox.value.length);
				return false;
				break;
			case 37:    // left arrow
            case 39:    // right arrow
			case 13:    // enter
            case 9:     // tab
            case 16:    // shift
            case 17:    // ctrl
            case 18:    // alt
            case 20:    // caps lock
            case 8:     // backspace
            case 46:    // delete
                return true;
                break;
            default:
				return true;
	}
}

function checkLoaded(control) {
	if (typeof(control.cargado) == "undefined") {
		control.document.controlacargar = control;
		control.document.getElementById("ifrOne").src=control.src;
	}
}
