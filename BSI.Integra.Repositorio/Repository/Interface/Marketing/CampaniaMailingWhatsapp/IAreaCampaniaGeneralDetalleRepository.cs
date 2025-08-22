using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface IAreaCampaniaGeneralDetalleRepository
    {
        #region Metodos Base
        TCampaniaGeneralDetalleArea Add(CampaniaGeneralDetalleAreaDTO entidad);

        TCampaniaGeneralDetalleArea Update(CampaniaGeneralDetalleAreaDTO entidad);
        bool Delete(int id, string usuario);
         IEnumerable<TCampaniaGeneralDetalleArea> Add(IEnumerable<CampaniaGeneralDetalleAreaDTO> listadoEntidad);

        IEnumerable<TCampaniaGeneralDetalleArea> Update(IEnumerable<CampaniaGeneralDetalleAreaDTO> listadoEntidad);

        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<FiltroSegmentoValorTipoDTO> ObtenerPorIdCampaniaGeneralDetalle(int idCampaniaGeneralDetalle);
        void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos);
        bool ExistFunction(int data, int id);

        TCampaniaGeneralDetalleArea FirstBy(int item, int Id);
        IEnumerable<TCampaniaGeneralDetalleArea> AddByEntity(IEnumerable<TCampaniaGeneralDetalleArea> listado);
        TCampaniaGeneralDetalleArea AddByEntity(TCampaniaGeneralDetalleArea entidad);
        TCampaniaGeneralDetalleArea Update(TCampaniaGeneralDetalleArea entidad);
    }
}
