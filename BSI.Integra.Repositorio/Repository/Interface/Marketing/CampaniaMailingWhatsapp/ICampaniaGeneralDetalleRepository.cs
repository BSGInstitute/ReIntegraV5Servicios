using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniaGeneralDetalleRepository : IGenericRepository<TCampaniaGeneralDetalle>
    {
        #region Metodos Base
        TCampaniaGeneralDetalle Add(CampaniaGeneralDetalle entidad);
        TCampaniaGeneralDetalle Update(CampaniaGeneralDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneralDetalle> Add(IEnumerable<CampaniaGeneralDetalle> listadoEntidad);
        IEnumerable<TCampaniaGeneralDetalle> Update(IEnumerable<CampaniaGeneralDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CampaniaGeneralDetalleDTO> ObtenerCampaniaGeneralDetalle();
        void Eliminarrelacion(List<TCampaniaGeneralDetalleArea> listaBorrar, List<int> nuevos, string usuario);
        int AddSqlString(CampaniaGeneralDetalle entidad);
        CampaniaGeneralDTO ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerlaCompleta(int IdcampaniaGeneral);
        List<TCampaniaGeneralDetalle> ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(int IdcampaniaGeneral);
        void ActualizarEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneralDetalle, bool flagEjecucion, string usuario);
        void AgregarUrl(string uri, int id,int count);
        void AgregarCantidad(int id,int count);
        List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn> ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(int idCampaniaGeneralDetalle, int idCamapniaGeneral);
        CampaniaGeneralDetalle BuscarCampaniaGeneralDetallePorId(int idCampaniaGeneralDetalle);
        List<AlumnoInformacionBasicaDTO> ObtenerAlumnosPorCampaniaGeneralDetalle(int idCampaniaGeneralDetalle);
    }
}
