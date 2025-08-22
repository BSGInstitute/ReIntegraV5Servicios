
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
    public class MaterialAdicionalAulaVirtual: BaseIntegraEntity
    {
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public bool EsOnline { get; set; }
        public virtual List<MaterialAdicionalAulaVirtualPespecifico>? MaterialAdicionalAulaVirtualPespecificos { get; set; }
        public virtual List<MaterialAdicionalAulaVirtualRegistro>? MaterialAdicionalAulaVirtualRegistros { get; set; }
    }
}
