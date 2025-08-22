namespace BSI.Integra.Aplicacion.Base.Enums
{
    public static class Enums
    {
        public enum TipoPersona
        {
            Alumno = 1,
            Personal = 2,
            Docente = 3,
            Proveedor = 4,
            Postulante = 5,
        }

        public enum ClasificacionUbicacionDocente
        {
            DocenteLocal = 1,
            DocenteNacional = 2,
            DocenteExtranjero = 3
        }

        public enum FormatoHTMLMostrar
        {
            Lista,
            Tabla
        }

        public enum FormaCalculoEvaluacion
        {
            Suma = 1,
            Promedio = 2
        }
        public enum PlantillaBaseEstatico
        {
            Certificado = 12,
            Constancia = 13
        }
        public enum CorreoNotificacionTipo
        {
            CondicionesCaracteristicas = 1,
            ComprobantePago = 2,
            ArchivoCrep = 3

        }
    }
}
