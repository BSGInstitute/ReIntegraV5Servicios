using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniaGeneralDetalleResponsableRepositorio
    {
        TCampaniaGeneralDetalleResponsable Add(CampaniaGeneralDetalleResponsableDTO entidad);
        TCampaniaGeneralDetalleResponsable Update(CampaniaGeneralDetalleResponsableDTO entidad);
        TCampaniaGeneralDetalleResponsable UpdateByEntity(TCampaniaGeneralDetalleResponsable entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCampaniaGeneralDetalleResponsable> Add(IEnumerable<CampaniaGeneralDetalleResponsableDTO> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetalleResponsable> Update(IEnumerable<CampaniaGeneralDetalleResponsableDTO> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetalleResponsable> UpdateByEntity(IEnumerable<TCampaniaGeneralDetalleResponsable> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        bool ExistFunction(int? item, int id);
        TCampaniaGeneralDetalleResponsable FirstBy(int Id, int detalleId);
        TCampaniaGeneralDetalleResponsable Add(TCampaniaGeneralDetalleResponsable entidad);
        IEnumerable<TCampaniaGeneralDetalleResponsable> Add(IEnumerable<TCampaniaGeneralDetalleResponsable> listadoEntidad);
    }
}
