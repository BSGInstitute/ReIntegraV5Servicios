using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniaGeneralDetalleSubAreaRepositorio
    {
        void Eliminarrelacion(List<TCampaniaGeneralDetalleSubArea> listaBorrar, List<int> nuevos, string usuario);
        TCampaniaGeneralDetalleSubArea FirstBy(int item, int Id);

        bool ExistFunction(int data, int id);

        #region Metodos Base
        TCampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubArea entidad);
        TCampaniaGeneralDetalleSubArea Add(TCampaniaGeneralDetalleSubArea entidad);
        TCampaniaGeneralDetalleSubArea Update(CampaniaGeneralDetalleSubArea entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneralDetalleSubArea> Add(IEnumerable<CampaniaGeneralDetalleSubArea> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetalleSubArea> Update(IEnumerable<CampaniaGeneralDetalleSubArea> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TCampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubAreaDTO entidad);
        IEnumerable<TCampaniaGeneralDetalleSubArea> Add(IEnumerable<CampaniaGeneralDetalleSubAreaDTO> listadoEntidad);
        TCampaniaGeneralDetalleSubArea UpdateByEntity(TCampaniaGeneralDetalleSubArea entidad);
        void EliminacionLogicoPorCampaniaGeneral(int idCampaniaGeneralDetalle, string usuario, List<int> nuevos);
    }
}
