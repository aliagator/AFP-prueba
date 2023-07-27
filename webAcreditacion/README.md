# Producto AFP.NET

## Proyecto Capa de Presentación (Web)

### Descripción

Para los proyectos de capa de presentación, se debe utilizar el prefijo “web” y el proyecto debe contener las siguientes características:

- Debe ser del tipo “Aplicación Web ASP.NET (.NET Framework)”
- El nombre debe tener el prefijo “web” + el nombre del módulo. 
- Se debe ubicar dentro de la carpeta definida al crear la solución y se debe utilizar el .NET Framework 4.6.

Nota: En caso que se trate de la migración de un proyecto existente que se esté actualizando de versión, se debe utilizar el mismo nombre del proyecto original.

### Configuración Inicial

Se debe crear como proyecto “Vacío” y sin ninguna opción seleccionada.

Se debe editar las propiedades e información del proyecto.

- Primero se debe asignar el espacio de nombre “Sonda.Gestion.Adm.Web.” + el nombre del módulo.

Nota: En caso que se trate de la migración de un proyecto existente que se esté actualizando de versión, se debe utilizar el mismo espacio de nombre del proyecto original.

Luego se debe editar la información de ensamblado. 

- El título y descripción deben indicar el nombre del módulo. 
- La compañía debe ser “SONDA SA”. 
- El producto debe ser “AFP.NET”. 
- La versión del ensamblado y de archivo debe inicializarse con “3.0.0.1”. A medida que se liberen nuevas versiones, se debe incrementar sólo el último número de la versión “3.0.0.X”.

En la opción “Compilar” se debe asignar la carpeta “bin\” como ruta de acceso de salida de la compilación “Todas las configuraciones”.

### Referencias

Se debe agregar referencia a librerías del SONDA.NET, al proyecto de acceso a datos, al proyecto de lógica de negocio, a la librería AdmCodeCompletion y a otras librerías que sean requeridas por el módulo.

Para la referencia a los proyectos de acceso a datos y lógica de negocio, se debe buscar dentro de “Proyectos” y seleccionar los proyectos "sys" y "bl".

Para las referencias del SONDA.NET se debe examinar y buscar las referencias en la carpeta “..\referencias” ubicada en el directorio con los componentes del módulo. Como mínimo se debe hacer referencia a las siguientes librerías:

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

Las librerías de otros módulos que son requeridos por dependencia directa, también se deben asignar como referencia.

### Estructura Directorio

Dentro del directorio raíz no se deben incluir nuevos componentes, sólo se deben mantener los copiados desde el proyecto base.

Al copiar se debe reemplazar el archivo web.config con el existente en el proyecto base.

Dentro del proyecto sólo se deben considerar los siguientes componentes y carpetas:

- vb
- web + nombre del módulo
- Global.ascx
- MainAfp.htm
- navigation.xml
- Web.config

Nota: En caso que se trate de la migración de un proyecto existente que se esté actualizando de versión, la carpeta "web + nombre del módulo" debe mantener el nombre original.

Los otros componentes (archivos y carpetas) se deben mantener dentro del directorio, pero sin incluirlos en el proyecto.

Nota: No se deben incluir los reportes (archivos.rpt), ya que, estos se almacenan en un proyecto adicional.
