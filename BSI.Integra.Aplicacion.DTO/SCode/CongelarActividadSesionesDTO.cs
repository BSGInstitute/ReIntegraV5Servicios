using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO
{
    /// Autor: Jose Vega
    /// Fecha: 09/03/2026
    /// Version: 1.0
    /// <summary>
    /// DTO para recibir la peticion de congelamiento de una actividad especifica asociada a varias sesiones
    /// </summary>
    public class CongelarActividadSesionesDTO
    {
        public int IdGestionContactoDocenteFlujo { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public List<int> IdPEspecificoSesion_Lista { get; set; }
    }
}