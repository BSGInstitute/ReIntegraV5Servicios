using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRemarketingEmbudoHistoricoRepository : IGenericRepository<TRemarketingEmbudoHistorico>
    {
        #region Metodos Base
        TRemarketingEmbudoHistorico Add(RemarketingEmbudoHistorico entidad);
        TRemarketingEmbudoHistorico Update(RemarketingEmbudoHistorico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRemarketingEmbudoHistorico> Add(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad);
        IEnumerable<TRemarketingEmbudoHistorico> Update(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public long ObtenerInformacionOportunidadRemarketingTotal(DateTime? FechaCorte = null);
        public List<OportunidadRemarketingEmbudoDTO> ObtenerInformacionOportunidadRemarketing(int Pagina, int RegistrosPorPagina, DateTime? FechaCorte = null);
        public List<RemarketingEmbudoNivelDescripcionDTO> ObtenerInformacionRemarketingEmbudoNivel();
        public void RegistrarEmbudoRemarketing(int IdRemarketingEmbudoNivel, int IdAlumno,DateTime FechaClasificacion,string Usuario);
        public List<RemarketingEmbudoNivelLlamadaEfectivaDTO> ObtenerLlamadasEfectivasOportunidadAlumno();
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing();
        public List<RemarketingEmbudoNivelInteraccionProgresivoDTO> ObtenerInteraccionFormularioProgresivo();
        public List<OportunidadScoreDTO> ObtenerScoreOportunidadAlumno(int registrosPorPagina);
        public List<OportunidadScoreDTO> ObtenerScoreOportunidadAlumnoIndividual(int IdOportunidad);
        public List<InteracccionPortalUltimaInteraccionDTO> ObtenerInteraccionPortalUltimaInteraccion();
        public InteracccionPortalUltimaInteraccionDTO ObtenerInteraccionPortalUltimaInteraccionAlumno(int IdAlumno);
        public List<ActividadEjecutadaReporteDTO> ObtenerOcurrenciaEjecutada();
        public ActividadEjecutadaReporteDTO ObtenerOcurrenciaEjecutadaAlumno(int IdAlumno);
        public List<AlumnoCentroCostoRegistroDTO> ObtenerCentroCostoRegistro();
        public AlumnoCentroCostoRegistroDTO ObtenerCentroCostoRegistroAlumno(int IdAlumno);
        public List<WhatsappUltimoMensajeEnviadoDTO> ObtenerWhatsAppMensajeUltimo();
        public WhatsappUltimoMensajeEnviadoDTO ObtenerWhatsAppMensajeUltimoAlumno(int IdAlumno);
        public List<OportunidadUltimoCambioDTO> ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambio();
        public OportunidadUltimoCambioDTO ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambioAlumno(int IdAlumno);
    }
}

