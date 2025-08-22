using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICampaniaGeneralDetalleService
    {
        CampaniaGeneralDetalle Add(CampaniaGeneralDetalleEnvioDTO entidad,string usuario);
        CampaniaGeneralDetalle Update(CampaniaGeneralDetalleEnvioDTO entidad);
        bool Delete(int id, string usuario);

        List<CampaniaGeneralDetalle> Add(List<CampaniaGeneralDetalle> listadoEntidad,string usuario);
        List<CampaniaGeneralDetalle> Update(List<CampaniaGeneralDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);

        IEnumerable<CampaniaGeneralDetalleDTO> ObtenerCampaniaGeneralDetalle();


    }
}
