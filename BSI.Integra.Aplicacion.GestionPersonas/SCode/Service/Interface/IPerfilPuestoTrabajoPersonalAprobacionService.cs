using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public  interface IPerfilPuestoTrabajoPersonalAprobacionService
    {
        DatosCombosPersonalAprobacionDTO ObtenerCombos();
        IEnumerable<PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO> ObtenerPerfilPuestoTrabajoPersonalAprobacion();
        bool Insertar(PerfilPuestoTrabajoPersonalAprobacionDTO dto, string usuario);
        bool Actualizar(PerfilPuestoTrabajoPersonalAprobacionDTO dto, string usuario);
        bool Eliminar(EliminarPuestoTrabajoDTO dto, string usuario);
    }
}
