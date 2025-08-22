using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IProcesoSeleccionRangoRepository
    {
        #region Metodos Base
        TProcesoSeleccionRango Add(ProcesoSeleccionRango entidad);

        TProcesoSeleccionRango Update(ProcesoSeleccionRango entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProcesoSeleccionRango> Add(IEnumerable<ProcesoSeleccionRango> listadoEntidad);
        IEnumerable<TProcesoSeleccionRango> Update(IEnumerable<ProcesoSeleccionRango> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProcesoSeleccionRangoDTO> Obtener();

    }
}
