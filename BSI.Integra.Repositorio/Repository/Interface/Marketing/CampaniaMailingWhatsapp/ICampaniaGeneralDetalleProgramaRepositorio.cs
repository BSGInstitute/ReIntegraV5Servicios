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
    public interface ICampaniaGeneralDetalleProgramaRepositorio
    {
        #region Metodos Base
        TCampaniaGeneralDetallePrograma Add(CampaniaGeneralDetallePrograma entidad);
        TCampaniaGeneralDetallePrograma Add(TCampaniaGeneralDetallePrograma entidad);
        TCampaniaGeneralDetallePrograma Update(CampaniaGeneralDetallePrograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneralDetallePrograma> Add(IEnumerable<CampaniaGeneralDetallePrograma> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetallePrograma> Add(IEnumerable<TCampaniaGeneralDetallePrograma> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetallePrograma> Update(IEnumerable<CampaniaGeneralDetallePrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        CampaniaGeneralDetallePrograma AddEntity(TCampaniaGeneralDetallePrograma entidad);

    }
}
