var SV_AUTOCOMPLETE = "3.3.37"; 

  (function( $ ) {
    $.widget( "custom.combobox", {
      _create: function() {
        this.wrapper = $( "<span>" )
          .addClass( "custom-combobox" )
          .insertAfter( this.element );

		var w = $(this.element).width();
		
        this.element.hide();
        this._createAutocomplete();
        this._createShowAllButton();
        
		this.input.css("background-color", $(this.element).css("background-color"));
		this.input.css("color", $(this.element).css("color"));
		
        this.wrapper.css("width", (w + 4 ) + "px");
        this.input.css("width", (w - 15) + "px");
        this.button.css("left", (w - 15) + "px");
		
				
      },
 
      _createAutocomplete: function() {
        var selected = this.element.children( ":selected" ),
          value = selected.val() ? selected.text() : "";
		
        this.input = $( "<input>" )
          .appendTo( this.wrapper )
          .val( value )
          .attr( "title", "" )
          .addClass( "custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left" )
          .autocomplete({
            delay: 0,
            minLength: 0,
            source: $.proxy( this, "_source" )
          });
          
          
          /*
          .tooltip({
            tooltipClass: "ui-state-highlight"
          })
          */
 
        this._on( this.input, {
          autocompleteselect: function( event, ui ) {
          
            var prev = null;

			this.element.children( "option" ).each(function() {
				if (this.selected == true) {
					prev = $(this).text().toLowerCase()
				}
			});
        
            ui.item.option.selected = true;
            
            this._trigger( "select", event, {
              item: ui.item.option
            });

			
			this._delay(function() {$(this.input).caret(0);}, 1);
		
			
            if (prev != null) {
				if ($(ui.item.option).text().toLowerCase() != prev) {
					$(this.element).trigger('change');		  
				}
			}
		    
          },
 
          autocompletechange: "_removeIfInvalid"
        });
      },
 
      _createShowAllButton: function() {
        var input = this.input,
          wasOpen = false;
 
        this.button = $( "<a>" )
          .attr( "tabIndex", -1 )
          .attr( "title", "" )
          .tooltip()
          .appendTo( this.wrapper )
          .button({
            icons: {
              primary: "ui-icon-triangle-1-s"
            },
            text: false
          })
          .removeClass( "ui-corner-all" )
          .addClass( "custom-combobox-toggle ui-corner-right" )
          .mousedown(function() {
            wasOpen = input.autocomplete( "widget" ).is( ":visible" );
          })
          .click(function() {
            input.focus();
			
			
			
			
			// Close if already visible
            if ( wasOpen ) {
              return;
            }
			
            // Pass empty string as value to search for, displaying all results
            input.autocomplete( "search", "" );
		
            /* Intento de modificar ancho de control
            $(this.menu).css("width", "300px");			
			$(this.menu).css("height", "300px");			
            $(this.menu).css("overflow", "auto");
			*/
          });
          
          
          
          $("<img>")
          .attr( "src", "img/dropdownlistsnd/tgu.gif" )
          .appendTo( this.button );
          
      },
 
      _source: function( request, response ) {
        var matcher = new RegExp( $.ui.autocomplete.escapeRegex(request.term), "i" );
        response( this.element.children( "option" ).map(function() {
          var text = $( this ).text();
          if ( this.value && ( !request.term || matcher.test(text) ) )
            return {
              label: text,
              value: text,
              option: this
            };
        }) );
      },
 
      _removeIfInvalid: function( event, ui ) {
 
 
        // Selected an item, nothing to do
        if ( ui.item ) {
          return;
        }
        
        var prev = null;

		this.element.children( "option" ).each(function() {
			if (this.selected == true) {
				prev = $(this).text().toLowerCase()
			}
		});
                 
        // Search for a match (case-insensitive)
        var value = this.input.val(),
          valueLowerCase = value.toLowerCase(),
          valid = false; 
                  
        this.element.children( "option" ).each(function() {
          if ( $( this ).text().toLowerCase() === valueLowerCase ) {
			this.selected = valid = true;
			return false;
          }
        });
 
		this._delay(function() {$(this.input).caret(0);}, 1);
		 
        // Found a match, nothing to do
        if ( valid ) {
			if (prev != null) {
				if ($(this).text().toLowerCase() != prev) {
					$(this.element).trigger('change');		  
				}
			}
          return;
        }
 
        // Remove invalid value
        this.input
          .val( "" )
          .attr( "title", value + " didn't match any item" )
          .tooltip( "open" );
        this.element.val( "" );
        this._delay(function() {
		  this.input.tooltip( "close" ).attr( "title", "" );
        }, 2500 );
        this.input.data( "ui-autocomplete" ).term = "";
      },
 
      _destroy: function() {
        this.wrapper.remove();
        this.element.show();
      }
    });
  })( jQuery );
