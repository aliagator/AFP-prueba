# Producto AFP.NET

## Proyecto Capa de Presentaci�n (Web)

### Descripci�n

Para los proyectos de capa de presentaci�n, se debe utilizar el prefijo �web� y el proyecto debe contener las siguientes caracter�sticas:

- Debe ser del tipo �Aplicaci�n Web ASP.NET (.NET Framework)�
- El nombre debe tener el prefijo �web� + el nombre del m�dulo. 
- Se debe ubicar dentro de la carpeta definida al crear la soluci�n y se debe utilizar el .NET Framework 4.6.

Nota: En caso que se trate de la migraci�n de un proyecto existente que se est� actualizando de versi�n, se debe utilizar el mismo nombre del proyecto original.

### Configuraci�n Inicial

Se debe crear como proyecto �Vac�o� y sin ninguna opci�n seleccionada.

Se debe editar las propiedades e informaci�n del proyecto.

- Primero se debe asignar el espacio de nombre �Sonda.Gestion.Adm.Web.� + el nombre del m�dulo.

Nota: En caso que se trate de la migraci�n de un proyecto existente que se est� actualizando de versi�n, se debe utilizar el mismo espacio de nombre del proyecto original.

Luego se debe editar la informaci�n de ensamblado. 

- El t�tulo y descripci�n deben indicar el nombre del m�dulo. 
- La compa��a debe ser �SONDA SA�. 
- El producto debe ser �AFP.NET�. 
- La versi�n del ensamblado y de archivo debe inicializarse con �3.0.0.1�. A medida que se liberen nuevas versiones, se debe incrementar s�lo el �ltimo n�mero de la versi�n �3.0.0.X�.

En la opci�n �Compilar� se debe asignar la carpeta �bin\� como ruta de acceso de salida de la compilaci�n �Todas las configuraciones�.

### Referencias

Se debe agregar referencia a librer�as del SONDA.NET, al proyecto de acceso a datos, al proyecto de l�gica de negocio, a la librer�a AdmCodeCompletion y a otras librer�as que sean requeridas por el m�dulo.

Para la referencia a los proyectos de acceso a datos y l�gica de negocio, se debe buscar dentro de �Proyectos� y seleccionar los proyectos "sys" y "bl".

Para las referencias del SONDA.NET se debe examinar y buscar las referencias en la carpeta �..\referencias� ubicada en el directorio con los componentes del m�dulo. Como m�nimo se debe hacer referencia a las siguientes librer�as:

- AdmCodeCompletion 
- SoporteComun
- SoporteConfiguracionAplicacion
- sysSoporte
- SondaGestionAdmUtil
- SondaGestionAdmWEB
- SondaGestionAdmWS
- SondaNetShared
- SondaNetSystem
- wsSoporte

Las librer�as de otros m�dulos que son requeridos por dependencia directa, tambi�n se deben asignar como referencia.

### Estructura Directorio

Dentro del directorio ra�z no se deben incluir nuevos componentes, s�lo se deben mantener los copiados desde el proyecto base.

Al copiar se debe reemplazar el archivo web.config con el existente en el proyecto base.

Dentro del proyecto s�lo se deben considerar los siguientes componentes y carpetas:

- vb
- web + nombre del m�dulo
- Global.ascx
- MainAfp.htm
- navigation.xml
- Web.config

Nota: En caso que se trate de la migraci�n de un proyecto existente que se est� actualizando de versi�n, la carpeta "web + nombre del m�dulo" debe mantener el nombre original.

Los otros componentes (archivos y carpetas) se deben mantener dentro del directorio, pero sin incluirlos en el proyecto.

Nota: No se deben incluir los reportes (archivos.rpt), ya que, estos se almacenan en un proyecto adicional.
