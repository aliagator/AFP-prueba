var SV_FIRMADIGITAL = "2.9.4.0";
    // CAPICOM constants 
    var CAPICOM_STORE_OPEN_READ_ONLY = 0;
    var CAPICOM_CURRENT_USER_STORE = 2;
    var CAPICOM_CERTIFICATE_FIND_SHA1_HASH = 0;
    var CAPICOM_CERTIFICATE_FIND_EXTENDED_PROPERTY = 6;
    var CAPICOM_CERTIFICATE_FIND_TIME_VALID = 9;
    var CAPICOM_CERTIFICATE_FIND_KEY_USAGE = 12;
    var CAPICOM_DIGITAL_SIGNATURE_KEY_USAGE = 0x00000080;
    var CAPICOM_AUTHENTICATED_ATTRIBUTE_SIGNING_TIME = 0;
    var CAPICOM_INFO_SUBJECT_SIMPLE_NAME = 0;
    var CAPICOM_ENCODE_BASE64 = 0;
    var CAPICOM_E_CANCELLED = -2138568446;
    var CERT_KEY_SPEC_PROP_ID = 6;
 

	function firmadigital_cargarcombos() {
	
		// Filter the certificates to only those that are good for my purpose
	    var FilteredCertificates = FilterCertificates();
	   	
	   	if (typeof(arrayButtonFirmaDigital) != "undefined") {
	   		if (FilteredCertificates.Count >= 1) {
				var i;
			
				for (i = 0; i < arrayButtonFirmaDigital.length; i++) {
					var id = arrayButtonFirmaDigital[i];
					var combo = document.all[id + "_certificados"];
					
					if (combo) {
					
						var j;
						var newOpt;
					
						for (j = 0; j < FilteredCertificates.Count; j++) {
							combo.options[j+1]= new Option(FilteredCertificates.Item(j+1).GetInfo(CAPICOM_INFO_SUBJECT_SIMPLE_NAME));
							combo.options[j+1].value = FilteredCertificates.Item(j+1).Thumbprint;
						}
					} else {
					// si no esta el combo es porque debe utilizar el hash predefinido
					// debiera validar el hash, si no esta instalado entonces error
						
					}
				}
			}
		}
	}
		
	function verificarCertificadoInstalado(hash) {
		// Filter the certificates to only those that are good for my purpose
	    var FilteredCertificates = FilterCertificates();
	    var instalado = false;
	    
	   	for (j = 0; j < FilteredCertificates.Count; j++) {
	   		if (FilteredCertificates.Item(j+1).Thumbprint == hash) {
	   			instalado = true;
	   		}
		}
		return instalado;
	}


   function IsCAPICOMInstalled()
   {
	if(typeof(oCAPICOM) == "object")
	{
		if( (oCAPICOM.object != null) )
     		{
      			// We found CAPICOM!
     			return true;
     		}
    	}
   }
  
   function Init(txtCertificate)
   {
	   // Filter the certificates to only those that are good for my purpose
	   var FilteredCertificates = FilterCertificates();

	   // if only one certificate was found then lets show that as the selected certificate
	   if (FilteredCertificates.Count == 1)
	   {
		txtCertificate.value = FilteredCertificates.Item(1).GetInfo(CAPICOM_INFO_SUBJECT_SIMPLE_NAME);
		txtCertificate.hash = FilteredCertificates.Item(1).Thumbprint;
	   }
	   else
	   {
		txtCertificate.value = "";
		txtCertificate.hash = "";
	   }

	   // clean up	
	   FilteredCertificates = null;
   }
      
   function firmadigital_firmar(control) {
      
      var id = control.id;
      var combo = document.all[id + "_certificados"];
      
   	  if (combo) {
		if (!combo.selectedIndex) {
			alert("Debe seleccionar un certificado con el cual firmar el documento.");
			return false;
		}
	  }

      var hash = combo.options[combo.selectedIndex].value;
           
      return firmadigital_firmar2(control,hash);
   }
   
   function firmadigital_firmar2(control, hash) {

	  var id = control.id;
      var contenido = document.all[control.ControlAFirmar];
      var ctlFirma = document.all[id + "_Firma"];
      
	  return Sign(hash,contenido,ctlFirma);
   }
   
   function Sign(hash,txtPlainText,txtSignature) {
   		// CAPICOM Constants
		CAPICOM_VERIFY_SIGNATURE_ONLY = 0;
		CAPICOM_VERIFY_SIGNATURE_AND_CERTIFICATE = 1;
		// instantiate the CAPICOM objects
		var SignedData = new ActiveXObject('CAPICOM.SignedData');
    	var Signer = new ActiveXObject("CAPICOM.Signer");
 	    var TimeAttribute = new ActiveXObject("CAPICOM.Attribute");
	
		SignedData.Content = txtPlainText.value;
		try
		{
			// Set the Certificate we would like to sign with
		   	Signer.Certificate = FindCertificateByHash(hash);
		   	
		   	// Set the time in which we are applying the signature
			var Today = new Date();
			TimeAttribute.Name = CAPICOM_AUTHENTICATED_ATTRIBUTE_SIGNING_TIME;
			TimeAttribute.Value = Today.getVarDate();
			Today = null;
		   	Signer.AuthenticatedAttributes.Add(TimeAttribute);
			
		   	// Do the Sign operation
			txtSignature.value  = SignedData.Sign(Signer, true, CAPICOM_ENCODE_BASE64);
		}
		catch (e)
		{
			if (e.number != CAPICOM_E_CANCELLED)
			{
				alert("Ha ocurrido un error cuando se intentaba firmar el contenido, Error: " + e.description);
				return false;
			}
		}
		
		/*
		try
		{
			SignedData.Content=txtPlainText.value;
			SignedData.Verify(txtSignature.value, true, CAPICOM_VERIFY_SIGNATURE_AND_CERTIFICATE);
								
			
		}
		catch (e)
		{
			alert(e.description);
			return false;
		}
		*/
		
        return true;
   }


   function FilterCertificates()
   {
	   // instantiate the CAPICOM objects
	   var MyStore = new ActiveXObject("CAPICOM.Store");
	   var FilteredCertificates = new ActiveXObject("CAPICOM.Certificates");

	   // open the current users personal certificate store
	   try
	   {
	   	MyStore.Open(CAPICOM_CURRENT_USER_STORE, "My", CAPICOM_STORE_OPEN_READ_ONLY);
	   }
	   catch (e)
	   {
		if (e.number != CAPICOM_E_CANCELLED)
		{
	   		alert("Ha ocurrido un error cuando se intentaba abrir la información de sus de certificados personales, operación abortada");
			return false;
		}
	   }

	   // find all of the certificates that:
	   //   * Are good for signing data
	   //	* Have PrivateKeys associated with then - Note how this is being done :)
	   //   * Are they time valid
	   var FilteredCertificates = MyStore.Certificates.Find(CAPICOM_CERTIFICATE_FIND_KEY_USAGE,CAPICOM_DIGITAL_SIGNATURE_KEY_USAGE).Find(CAPICOM_CERTIFICATE_FIND_TIME_VALID).Find(CAPICOM_CERTIFICATE_FIND_EXTENDED_PROPERTY,CERT_KEY_SPEC_PROP_ID);
	   //var FilteredCertificates = MyStore.Certificates.Find(CAPICOM_CERTIFICATE_FIND_TIME_VALID);
	   return FilteredCertificates;

	   // Clean Up
	   MyStore = null;
	   FilteredCertificates = null;
  }

  function FindCertificateByHash(szThumbprint)
   {
	   // instantiate the CAPICOM objects
	   var MyStore = new ActiveXObject("CAPICOM.Store");

	   // open the current users personal certificate store
	   try
	   {
	   	MyStore.Open(CAPICOM_CURRENT_USER_STORE, "My", CAPICOM_STORE_OPEN_READ_ONLY);
	   }
	   catch (e)
	   {
		if (e.number != CAPICOM_E_CANCELLED)
		{
	   		alert("Ha ocurrido un error cuando se intentaba abrir la información de sus de certificados personales, operación abortada");
			return false;
		}
	   }

	   // find all of the certificates that have the specified hash
	   var FilteredCertificates = MyStore.Certificates.Find(CAPICOM_CERTIFICATE_FIND_SHA1_HASH, szThumbprint);
	   return FilteredCertificates.Item(1);

	   // Clean Up
	   MyStore = null;
	   FilteredCertificates = null;
  }