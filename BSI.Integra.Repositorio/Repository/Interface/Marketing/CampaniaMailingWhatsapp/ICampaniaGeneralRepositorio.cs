
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniaGeneralRepositorio
    {
        #region Metodos Base
        TCampaniaGeneral Add(CampaniaGeneral entidad);
        TCampaniaGeneral Update(CampaniaGeneralDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneral> Add(IEnumerable<CampaniaGeneralDTO> listadoEntidad);
        IEnumerable<TCampaniaGeneral> Update(IEnumerable<CampaniaGeneralDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CampaniaGeneralDTO Obtener(int idCampaniaGeneral);
        List<CampaniaGeneralDTO> ObtenerListaCampaniaGeneral();
        List<PrioridadesCampaniaGeneralDetalleDTO> ObtenerListaCampaniaGeneralDetalleConProgramas(int idCampaniaGeneral);
        List<CampaniaGeneralDetalleEstadoEnEjecucionDTO> ObtenerEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneral);
        List<ResponsablesDTO> ObtenerListaCampaniaGeneralDetalleResponsables(int idCampaniaGeneralDetalle);
        List<CampaniaGeneralDTO> ObtenerTodosPorIdCampaniaGeneral(int id);
        string ObtenerUrlFormularioPrioridad(int idCampaniaGeneralDetalle);
        bool CrearUrlFormularioPrioridad(int idCampaniaGeneralDetalle, string usuarioResponsable);
        List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar();
        TCampaniaGeneral FirstBy(Expression<Func<TCampaniaGeneral, bool>> filter);
    }
}
