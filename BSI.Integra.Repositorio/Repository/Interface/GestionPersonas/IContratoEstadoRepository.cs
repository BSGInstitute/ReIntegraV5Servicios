using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IContratoEstadoRepository : IGenericRepository<TContratoEstado>
    {

        #region Metodos Base
        TContratoEstado Add(ContratoEstado entidad);
        TContratoEstado Update(ContratoEstado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TContratoEstado> Add(IEnumerable<ContratoEstado> listadoEntidad);
        IEnumerable<TContratoEstado> Update(IEnumerable<ContratoEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ContratoEstadoDTO> Obtener();
        ContratoEstado? ObtenerPorId(int id);
    }
}
