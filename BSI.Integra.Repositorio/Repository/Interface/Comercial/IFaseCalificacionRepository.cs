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
    public interface IFaseCalificacionRepository : IGenericRepository<TFaseCalificacion>
    {
        #region Metodos Base
        TFaseCalificacion Add(FaseCalificacion entidad);
        TFaseCalificacion Update(FaseCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFaseCalificacion> Add(IEnumerable<FaseCalificacion> listadoEntidad);
        IEnumerable<TFaseCalificacion> Update(IEnumerable<FaseCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FaseCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<FaseCalificacion> ObtenerFases();

    }
}
