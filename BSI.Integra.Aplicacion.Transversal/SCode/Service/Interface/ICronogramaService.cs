using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICronogramaService
    {
        #region Metodos Base

        #endregion

 
        public object ObtenerCodigoMatricula(Dictionary<string, string> Valor);

        public List<CodigoMatriculaPEspecificoDTO> ObtenerCodigoMatriculaPEspecificoPorAlumnos(int idAlumno);
        public List<AlumnoFiltroAutocompleteDTO> ObtenerAlumnoPorValor(Dictionary<string, string> Valor);
        public object ObtenerTodoEstadoMatricula();
        public object ObtenerDatosPago();
        public object ObtenerAsesorPorApellidos();
        public object ObtenerCoordinadorPorApellidos();
        public object ObtenerCuotasNoPagadas(string CodigoMatricula, int Version);
        public object ObtenerPEspecificoPorCentroCosto(Dictionary<string, string> Valor);
        public object ObtenerAlumnoProgramaEspecifico(string CodigoMatricula);
        public object ActualizarMatricula(MatriculaActualizarDTO Json);
        public object ObtenerCronograma(string CodigoMatricula);
        public object ObtenerCostosAdministrativosCodigoMatricula(string CodigoMatricula);
        public object ObtenerTodoPersonal();
        public object ObtenerPersonalAprobadoPorApellido(Dictionary<string, string> Valor);
        public object ActualizarFormaPago(int IdCuota, int? IdFormaPago, string Usuario);
        public object ActualizarFechaDeposito(PagoActualizadoFechaDepositoDTO Json);
        public object GuardarPagoCuota(PagoCuotaCronogramaDTO Json);
        public object ActualizarEntregaControlDocs(ListaControlDocumentosDTO Json);
        public object ObtenerDetalleTasasAcademicas(Dictionary<string, string> Valor);
        public object ObtenerDocumentosMatricula(string CodigoMatricula, int IdPEspecifico);
        public object GuardarCronograma(CronogramaModificadoDTO Json);
        public object ActualizarFechaPago(PagoActualizadoFechaDTO Json);
        public object ActualizarMoraCAdelanto(MoraActualizadoDTO Json);
        public object EliminarMatricula(string CodigoMatricula, int Modoeliminacion, string Usuario);
        public List<AlumnoMatriculaDTO> ObtenerListadoAlumnosMatricula(EscrituraCrepDTO escrituraCrepDTO);
        public object ObtenerCuotasCrepPorCodigoMatricula(string CodigoMatricula);
        public object ObtenerCuentasCorrientes();
        public List<DTO.ComboDTO> ObtenerComboCodigoMatricula(Dictionary<string, string> Valor);
        public object GuardarCambioMonedaCronograma(CambioMonedaCronogramaModificadoDTO Json);
        public object ActualizarGestionDeCobranza(PagoActualizadoMoraTarifarioDTO Json);
        public Boolean GuardarPagoPostulante(int idPostulante, string Usuario);
        public object ObtenerCronogramFacturacion(string CodigoMatricula);


    }
}
