
using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// Repositorio: MaterialAdicionalAulaVirtualRepository
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de T_MaterialAdicionalAulaVirtual
    /// </summary>
    public class PespecificoCursoAdicional : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int IdPespecificoAdicional { get; set; }
    }
}
