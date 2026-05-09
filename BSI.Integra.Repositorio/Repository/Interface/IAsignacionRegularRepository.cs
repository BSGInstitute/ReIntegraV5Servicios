using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionRegularRepository : IGenericRepository<TAsignacionRegular>
    {
        #region Metodos Base
        TAsignacionRegular Add(AsignacionRegular entidad);
        TAsignacionRegular Update(AsignacionRegular entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TAsignacionRegular> Add(IEnumerable<AsignacionRegular> listadoEntidad);
        IEnumerable<TAsignacionRegular> Update(IEnumerable<AsignacionRegular> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ValidacionPaisConfiguracionAsignacionRegularDTO> ObtenerListaDeAsignacionRegular();
        bool VerificarConfiguracionPorPais(int idAsignacionRegular);
        RecibirConfiguracionPrincipalPorPaisDTO ObtenerConfiguracionesPorPais(int IdAsignacionRegular, int IdPais);
        List<RecibirConfiguracionPrincipalDTO> ObtenerCOnfiguracionPrincipal(int IdGrupoFiltroProgramaCritico);
        List<ObtenerBloquePorProgramaCriticoDTO> ObtenerBloquePorProgramaCritico();
        int ActualizarAsignacionRegular(int Id, bool DatoCalidad, bool AplicaProporcionPorPais, bool EsLimiteCola, int LimiteCola, int Prioridad, int PorcentajeTolerancia, string UsuarioModificacion);
        int ActualizarPaisAsignacionRegular(int Id, bool EsProporcionManual, int ProporcionManual, int ProporcionPorPais, string UsuarioModificacion);
        bool RegularizarPaisProgramaOtraArea(int IdAsignacionRegular);
        List<ObtenerConfiguracionProgramasOtrasAreasDTO> ObtenerConfiguracionProgramasOtrasAreas(int IdGrupoFiltroProgramaCritico);
        List<ListaProgramasGeneralesDTO> ObtenerComboListaProgramasGenerales();
        int ActualizarProgramaOtrasAreas(int IdProgramaOtraArea, int IdGrupoFiltroProgramaCritico, int IdAsignacionRegular, string Coordinador, string Asesor, string PGventa, bool BaseHistorica, bool DatoCalidad, bool EsLimitePeru, int LimitePeru, bool EsLimiteColombia, int LimiteColombia, bool EsLimiteMexico, int LimiteMexico, bool EsLimiteBolivia, int LimiteBolivia, bool EsLimiteInternacional, int LimiteInternacional, string UsuarioModificacion);
        List<IdProgramaGeneralDTO> ObtenerListaProgramas(int IdProgramaOtraArea);
        int EliminarPaisProgramaOtrasAreas(int? IdProgramaOtraArea, int? IdProgramaGeneral);
        int AgregarPaisProgramaOtrasAreas(int? IdProgramaOtraArea, int? IdProgramaGeneral);
        List<ComboProgramaGeneralDTO> ComboProgramaGeneral();
        List<ComboProgramaCriticoDTO> ComboGrupoVenta();
        List<ComboCoordinadorDTO> ComboPersonalJefe();
        List<ComboAsesorDTO> ComboAsesor();
        List<ListaIdAsignacionRegularDTO> BuscarPorComboSeleccionados(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador);
        List<ObtenerConfiguracionProgramasOtrasAreasDTO> ObtenerListaConfiguracionesSeleccionadas(int? IdAsignacionRegular);
        List<RecibirConfiguracionPrincipalDTO> ObtenerCOnfiguracionPrincipalCombo(int? IdAsignacionRegular);
        bool AsignacionAutomatizadaAsesor();
        List<ObtenerOportunidadConfiguradaV2DTO> ObtenerOportunidadConfigurada();
        List<VerificarSiAplicaProporcionAsignacionRegularDTO> VerificarSiAplicaProporcionAsignacionRegular();
        List<AddresseeDTO> ObtenerAddressee();
        SenderDTO ObtenerSender();
        List<AlgoritmoAsignacionAutomaticaPorPaisesDTO> AlgoritmoAsignacionAutomaticaPorPaises(int? IdPais, int? IdProgramaGeneral);
        bool? ActualizarRegistroPorInsertarAsesor(int? IdOportunidadPreAsignada, int? IdAsignacionRegular);
        bool? ActualizarRegistroPorInsertarAsesorCola(int? IdOportunidadPreAsignada, int? IdAsignacionRegular);
        bool? RegularizarConfiguracionTemporalAsignacionRegular();
        List<ObtenerListaAsesorDTO> ObtenerListaAsesor();
        public List<ComboAsesoresDTO> ComboAsesores();
        bool? InsertarAsignacionRegular(string ListaIdAsignacionRegular, String UsuarioCreacion);
        bool? EliminarAsignacionRegular(int IdAsignacionRegular, String UsuarioCreacion);
        bool? ActivarAsignacionAutomatica(int idAsignacionRegular, bool Activar, String UsuarioModificacion);
        ObtenerListaAsesorDTO ObtenerAsesorConfiguracion(int id);
        List<ObtenerAsesorConfiguracionPorPaisDTO> ObtenerAsesorConfiguracionPorPais(int idAsignacionRegular);
        bool? InsertarConfiguracionAsignacionRegular(int IdAsignacionRegular, int IdProgramaGeneral, String UsuarioCreacion);
        bool? EliminarPaisConfiguracionAsignacionRegular(int Id, String UsuarioModificacion);
        bool? InsertarOrigenSector(string nombre, string descripcion, string UsuarioCreacion);
        bool? ActualizarConfiguracionAsignacionRegular(PaisConfiguracionAsignacionRegularDTO paisConfiguracionAsignacionRegular, string usuario);
        bool? ActualizarTopeOportunidad(int idAsignacionRegular, int TopeOportunidad, String UsuarioModificacion);
        List<ObtenerAsesoresPorOportunidadDTO> ObtenerAsesoresPorOportunidad(int? IdProgramaGeneral);
        bool? EliminarOrigenSector(int Id, string UsuarioModificacion);
        List<ListaCategoriaOrigenNoConfigurada> ObtenerCategoriaOrigen();
        List<CategoriaOrigenPorSectorDTO> ObtenerCategoriaOrigenPorSector(int IdOrigenSector);
        bool? InsertarCategoriaOrigenPorSector(string ListaCategoriaOrigen, int IdOrigenSector, String UsuarioCreacion);
        bool? AgruparCategoriaOrigen(bool Agrupar, int IdCategoriaOrigen, String UsuarioModificacion);
        bool? EliminarConfiguracionCategoriaOrigen(int Id, String UsuarioModificacion);
        bool? ActualizarPrioridad(int idAsignacionRegular, int Prioridad, String UsuarioModificacion);
        public List<AlumnoOp> ObtenerAlumnoPorOportunidad(int IdOportunidad);
        public ContadorBicDias ObtenerContadorBic(int IdOportunidad);
        public PGeneralOportunidad ObtenerPGeneralPorOportunidad(int idoportunidad);
        public List<AsesoresValidacionDTO> ObtenerAsesoresActivos();
        public IdDTO ObtenerPaisPorOportunidad(int idoportunidad);
        public EstadoDTO ValidarEnvioPorDias(string celular);
        public EnviosDTO ObtenerEnvioWhatsappAsignacion(int idoportunidad);
        public bool InsertarEnviosWhatsappAsignacion(int idoportunidad);
        public List<DetallePaisConfiguracionAsignacionRegularDTO> ObtenerConfiguracionDetalle(int IdPaisConfiguracionAsignacionRegular);
        public List<DetallePaisConfiguracionAsignacionRegularDTO> ObtenerConfiguracionDetallePais(int IdAsignacionRegular);
        public DetallePaisConfiguracionAsignacionRegularDTO? ObtenerConfiguracionDetallePorIdPais(int IdPaisConfiguracionAsignacionRegular, int idPais);

        public bool? InsertarConfiguracionAsignacioDetalle(DetallePaisConfiguracionAsignacionRegularDTO detalleConfiguracion, string usuario);



        public bool? ActualizarConfiguracionDetalle(DetallePaisConfiguracionAsignacionRegularDTO paisConfiguracionAsignacionRegular, string usuario);
        bool? ActualizarTopeAsignacionDiaria(int idAsignacionRegular, int topeAsignacionDiaria, string usuarioModificacion);
        bool? ActualizarAsignacionPaisAsesor(int idAsignacionRegular, string asignacionPais, string usuarioModificacion);
        ObtenerAsignacionPaisAsesorDTO ObtenerAsignacionPaisAsesor(int idAsignacionRegular);
    }
}