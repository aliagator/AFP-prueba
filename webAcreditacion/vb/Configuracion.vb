Imports System.Configuration.ConfigurationSettings
Public Class Configuracion
    Inherits Sonda.Net.ConfiguracionBase

    Private Const BASE = "Sonda.Gestion.Adm.WS.IngresoEgreso.Configuracion"

    Public Shared ReadOnly Property NroHilosAcreditacion() As Integer
        Get
            Return leerConfiguracion(BASE & ".NroHilosAcreditacion", 1)
        End Get
    End Property
    
    Public Shared ReadOnly Property idAcreditador() As Integer
        Get
            Return leerConfiguracion(BASE & ".idAcreditador", 1)
        End Get
    End Property
    Public Shared ReadOnly Property pathControl() As String
        Get
            Return leerConfiguracion(BASE & ".pathControl", "\\PEKIN\ARCHIVOS\ACR\LOG_SVR")
        End Get
    End Property
End Class
