using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ICriticidadCalificacionRepository : IGenericRepository<TCriticidadCalificacion>
    {
        #region Metodos Base
        TCriticidadCalificacion Add(CriticidadCalificacion entidad);
        TCriticidadCalificacion Update(CriticidadCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriticidadCalificacion> Add(IEnumerable<CriticidadCalificacion> listadoEntidad);
        IEnumerable<TCriticidadCalificacion> Update(IEnumerable<CriticidadCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CriticidadCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<CriticidadCalificacion> ObtenerCriticidad();

    }

}
