using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAsignacionRegularService
    {
        #region Metodos Base
        AsignacionRegular Add(AsignacionRegular entidad);
        AsignacionRegular Update(AsignacionRegular entidad);
        bool Delete(int id, string usuario);

        List<AsignacionRegular> Add(List<AsignacionRegular> listadoEntidad);
        List<AsignacionRegular> Update(List<AsignacionRegular> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool RegularizarConfiguracionAsignacionRegular();
        List<ConfiguracionPrincipalDTO> ObtenerConfiguracionAsignacionRegular(int IdGrupoFiltroProgramaCritico);
        List<ObtenerBloquePorProgramaCriticoDTO> ObtenerBloquePorProgramaCritico();
        int ActualizarAsignacionRegular(List<ConfiguracionPrincipalDTO> ListaActualizar);
        int ActualizarConfiguracionProgramasOtrasAreas(List<ConfiguracionPrincipalDTO> ListaActualizar);
        List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> ObtenerConfiguracionProgramasOtrasAreas(int IdGrupoFiltroProgramaCritico);
        List<ListaProgramasGeneralesDTO> ObtenerComboListaProgramasGenerales();
        int ActualizarProgramasOtrasAreas(List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> ListaProgramasOtrasAreas);
        ComboBusquedaDTO ObtenerComboBusqueda();
        List<ObtenerConfiguracionPrincipalProgramasOtrasAreasDTO> BuscarPorComboSeleccionadosProgramasOtrasAreas(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador);
        List<ConfiguracionPrincipalDTO> BuscarPorComboSeleccionadosProgramasCriticos(int? IdProgramasGeneral, int? IdGrupoFiltroProgramaCritico, int? IdAsesor, int? IdCoordinador);
        bool AsignacionAutomatizadaAsesor(string Usuario);
        public bool AsignacionAutomatizadaAsesorWhatsapp(string Usuario);
        bool EnvioCorreo(string displayname, string subject, string mensaje, List<AddresseeDTO> listaReceptores);
        bool? AsignarAsesor(int? IdOportunidad, int? IdasignacionRegular);
        public bool UrlPost(string UrlBase, string jsonStringResult);
        List<ObtenerListaAsesorDTO> ObtenerListaAsesor();
        List<ComboAsesoresDTO> ObtenerComboAsesores();
        bool? InsertarAsignacionRegular(InsertarAsignacionRegularDTO ListaIdAsignacionRegular, String UsuarioCreacion);
        bool? EliminarAsignacionRegular(int IdAsignacionRegular, String UsuarioCreacion);
        bool? ActivarAsignacionAutomatica(int IdAsignacionRegular, bool activar, String UsuarioCreacion);
        List<ObtenerAsesorConfiguracionPorPaisDTO> ObtenerAsesorConfiguracionPorPais(int id);
        ObtenerListaAsesorDTO ObtenerAsesorConfiguracion(int id);
        bool? InsertarConfiguracionAsignacionRegular(int IdAsignacionRegular, InsertarProgramaGeneralAsignacionRegularDTO ListaIdAsignacionRegular, String UsuarioCreacion);
        bool? EliminarPaisConfiguracionAsignacionRegular(int id, String UsuarioModificacion);
        bool? InsertarOrigenSector(InsertarOrigenSectorDTO OrigenSector, string UsuarioCreacion);
        bool? ActualizarConfiguracionAsignacionRegular(List<ObtenerAsesorConfiguracionPorPaisDTO> ListaConfiguracionAsignacionRegular, String UsuarioModificacion);
        bool? ActualizarTopeOportunidad(int idAsignacionRegular, int TopeOportunidad, String UsuarioModificacion);
        bool? ActualizarTopeAsignacionDiaria(int idAsignacionRegular, int TopeAsignacionDiaria, String UsuarioModificacion);
        bool? EliminarOrigenSector(int Id, string UsuarioModificacion);

        List<ListaCategoriaOrigenNoConfigurada> ObtenerCategoriaOrigen();
        List<CategoriaOrigenPorSectorDTO> ObtenerCategoriaOrigenPorSector(int IdOrigenSector);
        bool? InsertarCategoriaOrigenPorSector(int IdOrigenSector, ListaCategoriaOrigenDTO ListaCategoriaOrigen, String UsuarioCreacion);
        bool? AgruparCategoriaOrigen(bool Agrupar, int IdCategoriaOrigen, String UsuarioModificacion);
        bool? ActualizarPrioridad(int idAsignacionRegular, int Prioridad, String UsuarioModificacion);

    }


}
