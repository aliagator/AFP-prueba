		var framesetQuery = "";
		if ( window.top.location.search != 0 ) {
			framesetQuery = window.top.location.search;
		} else {
			framesetQuery = "?OpenFrameset";
		}
		
		function getParameter ( queryString, parameterName ) {
			// Add "=" to the parameter name (i.e. parameterName=value)
			var parameterName = parameterName + "=";
			if ( queryString.length > 0 ) {
				// Find the beginning of the string
				begin = queryString.indexOf ( parameterName );
				// If the parameter name is not found, skip it, otherwise return the value
				if ( begin != -1 ) {
					// Add the length (integer) to the beginning
					begin += parameterName.length;
					// Multiple parameters are separated by the "&" sign
					end = queryString.indexOf ( "&" , begin );
					
					if ( end == -1 ) {
						end = queryString.length
					}
					// Return the string
					return unescape ( queryString.substring ( begin, end ) );
				}
				// Return "null" if no parameter has been found
				return null;
			}
		}