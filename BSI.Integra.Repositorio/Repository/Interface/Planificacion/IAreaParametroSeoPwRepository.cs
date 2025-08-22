using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IAreaParametroSeoPwRepository
    {
        #region Metodos Base
        TAreaParametroSeoPw Add(AreaParametroSeoPw entidad);
        TAreaParametroSeoPw Update(AreaParametroSeoPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAreaParametroSeoPw> Add(IEnumerable<AreaParametroSeoPw> listadoEntidad);
        IEnumerable<TAreaParametroSeoPw> Update(IEnumerable<AreaParametroSeoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        IEnumerable<AreaParametrosSeoPorIdAreaDTO> ObtenerAreaParametrosSeoPorIdArea(int idTag);
        List<AreaParametroSeoPw> ObtenerPorId(int id);
        IEnumerable<AreaParametroSeoPw> ObtenerPorIdAreaCapacitacion(int idAreaCapacitacion);

       
    }
}
