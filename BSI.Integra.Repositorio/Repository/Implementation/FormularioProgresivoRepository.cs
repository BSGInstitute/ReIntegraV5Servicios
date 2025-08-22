using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoRepository
    /// Autor: Jorge Gamero
    /// Fecha: 04/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivo
    /// </summary>
    public class FormularioProgresivoRepository : GenericRepository<TFormularioProgresivo>, IFormularioProgresivoRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivo, FormularioProgresivo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFormularioProgresivo MapeoEntidad(FormularioProgresivo entidad)
        {
            try
            {
                TFormularioProgresivo modelo = _mapper.Map<TFormularioProgresivo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFormularioProgresivo> Add(FormularioProgresivo entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivo_Insertar";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    Tipo = entidad.Tipo,
                    Activado = entidad.Activado,
                    IdFormularioProgresivo_Inicial = entidad.IdFormularioProgresivoInicial,
                    CondicionMostrar = entidad.CondicionMostrar,
                    TiempoProgramasPublicidad = entidad.TiempoProgramasPublicidad,
                    Titulo = entidad.Titulo,
                    TituloTexto = entidad.TituloTexto ?? (object)DBNull.Value,
                    CabeceraMensajeSup = entidad.CabeceraMensajeSup,
                    CabeceraMensajeSupTexto = entidad.CabeceraMensajeSupTexto ?? (object)DBNull.Value,
                    CabeceraMensaje = entidad.CabeceraMensaje,
                    CabeceraMensajeIndexCurso = entidad.CabeceraMensajeIndexCurso,
                    CabeceraMensajeTexto = entidad.CabeceraMensajeTexto ?? (object)DBNull.Value,
                    CabeceraMensajeTextoCurso = entidad.CabeceraMensajeTextoCurso ?? (object)DBNull.Value,
                    CabeceraMensajeBordes = entidad.CabeceraMensajeBordes,
                    CabeceraMensajeInf = entidad.CabeceraMensajeInf,
                    CabeceraMensajeInfIndexCurso = entidad.CabeceraMensajeInfIndexCurso,
                    CabeceraMensajeInfTexto = entidad.CabeceraMensajeInfTexto ?? (object)DBNull.Value,
                    CabeceraMensajeInfTextoCurso = entidad.CabeceraMensajeInfTextoCurso ?? (object)DBNull.Value,
                    CabeceraBoton = entidad.CabeceraBoton,
                    CabeceraBotonTexto = entidad.CabeceraBotonTexto ?? (object)DBNull.Value,
                    CabeceraBotonAccion = entidad.CabeceraBotonAccion,
                    CuerpoMensajeSup = entidad.CuerpoMensajeSup,
                    CuerpoMensajeSupTexto = entidad.CuerpoMensajeSupTexto ?? (object)DBNull.Value,
                    CuerpoCorreo = entidad.CuerpoCorreo,
                    CuerpoCorreoOrden = entidad.CuerpoCorreoOrden,
                    CuerpoCorreoObl = entidad.CuerpoCorreoObl,
                    CuerpoNombres = entidad.CuerpoNombres,
                    CuerpoNombresOrden = entidad.CuerpoNombresOrden,
                    CuerpoNombresObl = entidad.CuerpoNombresObl,
                    CuerpoApellidos = entidad.CuerpoApellidos,
                    CuerpoApellidosOrden = entidad.CuerpoApellidosOrden,
                    CuerpoApellidosObl = entidad.CuerpoApellidosObl,
                    CuerpoPais = entidad.CuerpoPais,
                    CuerpoPaisOrden = entidad.CuerpoPaisOrden,
                    CuerpoPaisObl = entidad.CuerpoPaisObl,
                    CuerpoTelefono = entidad.CuerpoTelefono,
                    CuerpoTelefonoOrden = entidad.CuerpoTelefonoOrden,
                    CuerpoTelefonoObl = entidad.CuerpoTelefonoObl,
                    CuerpoCargo = entidad.CuerpoCargo,
                    CuerpoCargoOrden = entidad.CuerpoCargoOrden,
                    CuerpoCargoObl = entidad.CuerpoCargoObl,
                    CuerpoAreaFormacion = entidad.CuerpoAreaFormacion,
                    CuerpoAreaFormacionOrden = entidad.CuerpoAreaFormacionOrden,
                    CuerpoAreaFormacionObl = entidad.CuerpoAreaFormacionObl,
                    CuerpoAreaTrabajo = entidad.CuerpoAreaTrabajo,
                    CuerpoAreaTrabajoOrden = entidad.CuerpoAreaTrabajoOrden,
                    CuerpoAreaTrabajoObl = entidad.CuerpoAreaTrabajoObl,
                    CuerpoIndustria = entidad.CuerpoIndustria,
                    CuerpoIndustriaOrden = entidad.CuerpoIndustriaOrden,
                    CuerpoIndustriaObl = entidad.CuerpoIndustriaObl,
                    CuerpoMensajeInf = entidad.CuerpoMensajeInf,
                    CuerpoMensajeInfTexto = entidad.CuerpoMensajeInfTexto ?? (object)DBNull.Value,
                    CuerpoBoton = entidad.CuerpoBoton,
                    CuerpoBotonTexto = entidad.CuerpoBotonTexto ?? (object)DBNull.Value,
                    CuerpoBotonAccion = entidad.CuerpoBotonAccion,
                    Pie = entidad.Pie,
                    PieTexto = entidad.PieTexto ?? (object)DBNull.Value,
                    Boton = entidad.Boton,
                    BotonTexto = entidad.BotonTexto ?? (object)DBNull.Value,
                    BotonAccion = entidad.BotonAccion,
                    Usuario = entidad.UsuarioCreacion,
                    TiempoProgramasOrganico = entidad.TiempoProgramasOrganico,
                    TiempoBlogsWhite = entidad.TiempoBlogsWhite,
                    TiempoIndexTags = entidad.TiempoIndexTags,
                    CabeceraMensajeTextoWhitepaper = entidad.CabeceraMensajeTextoWhitepaper ?? (object)DBNull.Value,
                    CabeceraMensajeInfTextoWhitepaper = entidad.CabeceraMensajeInfTextoWhitepaper ?? (object)DBNull.Value,
                    CabeceraMensajeSupIndexCurso = entidad.CabeceraMensajeSupIndexCurso,
                    CabeceraMensajeSupTextoCurso = entidad.CabeceraMensajeSupTextoCurso ?? (object)DBNull.Value,
                    CabeceraMensajeSupTextoWhitepaper = entidad.CabeceraMensajeSupTextoWhitepaper ?? (object)DBNull.Value,
                    CuerpoMensajeSupIndexCurso = entidad.CuerpoMensajeSupIndexCurso,
                    CuerpoMensajeSupTextoCurso = entidad.CuerpoMensajeSupTextoCurso ?? (object)DBNull.Value,
                    CuerpoMensajeSupTextoWhitepaper = entidad.CuerpoMensajeSupTextoWhitepaper ?? (object)DBNull.Value,

                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivo>>(queryInsert)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TFormularioProgresivo> Update(FormularioProgresivo entidad)
        {
            try
            {
                string query = "mkt.SP_FormularioProgresivo_Actualizar";
                var queryUpdate = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    Tipo = entidad.Tipo,
                    Activado = entidad.Activado,
                    IdFormularioProgresivo_Inicial = entidad.IdFormularioProgresivoInicial,
                    CondicionMostrar = entidad.CondicionMostrar,
                    TiempoProgramasPublicidad = entidad.TiempoProgramasPublicidad,
                    Titulo = entidad.Titulo,
                    TituloTexto = entidad.TituloTexto ?? (object)DBNull.Value,
                    CabeceraMensajeSup = entidad.CabeceraMensajeSup,
                    CabeceraMensajeSupTexto = entidad.CabeceraMensajeSupTexto ?? (object)DBNull.Value,
                    CabeceraMensaje = entidad.CabeceraMensaje,
                    CabeceraMensajeIndexCurso = entidad.CabeceraMensajeIndexCurso,
                    CabeceraMensajeTexto = entidad.CabeceraMensajeTexto ?? (object)DBNull.Value,
                    CabeceraMensajeTextoCurso = entidad.CabeceraMensajeTextoCurso ?? (object)DBNull.Value,
                    CabeceraMensajeBordes = entidad.CabeceraMensajeBordes,
                    CabeceraMensajeInf = entidad.CabeceraMensajeInf,
                    CabeceraMensajeInfIndexCurso = entidad.CabeceraMensajeInfIndexCurso,
                    CabeceraMensajeInfTexto = entidad.CabeceraMensajeInfTexto ?? (object)DBNull.Value,
                    CabeceraMensajeInfTextoCurso = entidad.CabeceraMensajeInfTextoCurso ?? (object)DBNull.Value,
                    CabeceraBoton = entidad.CabeceraBoton,
                    CabeceraBotonTexto = entidad.CabeceraBotonTexto ?? (object)DBNull.Value,
                    CabeceraBotonAccion = entidad.CabeceraBotonAccion,
                    CuerpoMensajeSup = entidad.CuerpoMensajeSup,
                    CuerpoMensajeSupTexto = entidad.CuerpoMensajeSupTexto ?? (object)DBNull.Value,
                    CuerpoCorreo = entidad.CuerpoCorreo,
                    CuerpoCorreoOrden = entidad.CuerpoCorreoOrden,
                    CuerpoCorreoObl = entidad.CuerpoCorreoObl,
                    CuerpoNombres = entidad.CuerpoNombres,
                    CuerpoNombresOrden = entidad.CuerpoNombresOrden,
                    CuerpoNombresObl = entidad.CuerpoNombresObl,
                    CuerpoApellidos = entidad.CuerpoApellidos,
                    CuerpoApellidosOrden = entidad.CuerpoApellidosOrden,
                    CuerpoApellidosObl = entidad.CuerpoApellidosObl,
                    CuerpoPais = entidad.CuerpoPais,
                    CuerpoPaisOrden = entidad.CuerpoPaisOrden,
                    CuerpoPaisObl = entidad.CuerpoPaisObl,
                    CuerpoTelefono = entidad.CuerpoTelefono,
                    CuerpoTelefonoOrden = entidad.CuerpoTelefonoOrden,
                    CuerpoTelefonoObl = entidad.CuerpoTelefonoObl,
                    CuerpoCargo = entidad.CuerpoCargo,
                    CuerpoCargoOrden = entidad.CuerpoCargoOrden,
                    CuerpoCargoObl = entidad.CuerpoCargoObl,
                    CuerpoAreaFormacion = entidad.CuerpoAreaFormacion,
                    CuerpoAreaFormacionOrden = entidad.CuerpoAreaFormacionOrden,
                    CuerpoAreaFormacionObl = entidad.CuerpoAreaFormacionObl,
                    CuerpoAreaTrabajo = entidad.CuerpoAreaTrabajo,
                    CuerpoAreaTrabajoOrden = entidad.CuerpoAreaTrabajoOrden,
                    CuerpoAreaTrabajoObl = entidad.CuerpoAreaTrabajoObl,
                    CuerpoIndustria = entidad.CuerpoIndustria,
                    CuerpoIndustriaOrden = entidad.CuerpoIndustriaOrden,
                    CuerpoIndustriaObl = entidad.CuerpoIndustriaObl,
                    CuerpoMensajeInf = entidad.CuerpoMensajeInf,
                    CuerpoMensajeInfTexto = entidad.CuerpoMensajeInfTexto ?? (object)DBNull.Value,
                    CuerpoBoton = entidad.CuerpoBoton,
                    CuerpoBotonTexto = entidad.CuerpoBotonTexto ?? (object)DBNull.Value,
                    CuerpoBotonAccion = entidad.CuerpoBotonAccion,
                    Pie = entidad.Pie,
                    PieTexto = entidad.PieTexto ?? (object)DBNull.Value,
                    Boton = entidad.Boton,
                    BotonTexto = entidad.BotonTexto ?? (object)DBNull.Value,
                    BotonAccion = entidad.BotonAccion,
                    Usuario = entidad.UsuarioModificacion,
                    TiempoProgramasOrganico = entidad.TiempoProgramasOrganico,
                    TiempoBlogsWhite = entidad.TiempoBlogsWhite,
                    TiempoIndexTags = entidad.TiempoIndexTags,
                    CabeceraMensajeTextoWhitepaper = entidad.CabeceraMensajeTextoWhitepaper ?? (object)DBNull.Value,
                    CabeceraMensajeInfTextoWhitepaper = entidad.CabeceraMensajeInfTextoWhitepaper ?? (object)DBNull.Value,
                    CabeceraMensajeSupIndexCurso = entidad.CabeceraMensajeSupIndexCurso,
                    CabeceraMensajeSupTextoCurso = entidad.CabeceraMensajeSupTextoCurso ?? (object)DBNull.Value,
                    CabeceraMensajeSupTextoWhitepaper = entidad.CabeceraMensajeSupTextoWhitepaper ?? (object)DBNull.Value,
                    CuerpoMensajeSupIndexCurso = entidad.CuerpoMensajeSupIndexCurso,
                    CuerpoMensajeSupTextoCurso = entidad.CuerpoMensajeSupTextoCurso ?? (object)DBNull.Value,
                    CuerpoMensajeSupTextoWhitepaper = entidad.CuerpoMensajeSupTextoWhitepaper ?? (object)DBNull.Value
                });
                return JsonConvert.DeserializeObject<List<TFormularioProgresivo>>(queryUpdate)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivo por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivo </returns>
        public FormularioProgresivo ObtenerPorId(int id)
        {
            try
            {
                var rpta = new FormularioProgresivo();
                var query = @"SELECT * FROM mkt.T_FormularioProgresivo WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FormularioProgresivo>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<FormularioProgresivo> </returns>
        public IEnumerable<FormularioProgresivo> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<FormularioProgresivo>();
                var query = @"SELECT Id, Nombre, Descripcion, Tipo, Activado, IdFormularioProgresivo_Inicial as IdFormularioProgresivoInicial, CondicionMostrar, TiempoProgramasPublicidad, 
                    Titulo, TituloTexto, CabeceraMensajeSup, CabeceraMensajeSupTexto, CabeceraMensaje, CabeceraMensajeIndexCurso, CabeceraMensajeTexto, CabeceraMensajeTextoCurso, 
                    CabeceraMensajeBordes, CabeceraMensajeInf, CabeceraMensajeInfIndexCurso, CabeceraMensajeInfTexto, CabeceraMensajeInfTextoCurso, CabeceraBoton, CabeceraBotonTexto, 
                    CabeceraBotonAccion, CuerpoMensajeSup, CuerpoMensajeSupTexto, CuerpoCorreo, CuerpoCorreoOrden, CuerpoCorreoObl, CuerpoNombres, CuerpoNombresOrden, CuerpoNombresObl, 
                    CuerpoApellidos, CuerpoApellidosOrden, CuerpoApellidosObl, CuerpoPais, CuerpoPaisOrden, CuerpoPaisObl, CuerpoTelefono, CuerpoTelefonoOrden, CuerpoTelefonoObl, CuerpoCargo, 
                    CuerpoCargoOrden, CuerpoCargoObl, CuerpoAreaFormacion, CuerpoAreaFormacionOrden, CuerpoAreaFormacionObl, CuerpoAreaTrabajo, CuerpoAreaTrabajoOrden, CuerpoAreaTrabajoObl, 
                    CuerpoIndustria, CuerpoIndustriaOrden, CuerpoIndustriaObl, CuerpoMensajeInf, CuerpoMensajeInfTexto, CuerpoBoton, CuerpoBotonTexto, CuerpoBotonAccion, Pie, PieTexto, Boton, 
                    BotonTexto, BotonAccion, TiempoProgramasOrganico, TiempoBlogsWhite, TiempoIndexTags, CabeceraMensajeTextoWhitepaper, CabeceraMensajeInfTextoWhitepaper, CabeceraMensajeSupIndexCurso,
                    CabeceraMensajeSupTextoCurso, CabeceraMensajeSupTextoWhitepaper, CuerpoMensajeSupIndexCurso, CuerpoMensajeSupTextoCurso, CuerpoMensajeSupTextoWhitepaper
                    FROM mkt.T_FormularioProgresivo WHERE Estado = 1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivo>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1)
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosIniciales()
        {
            try
            {
                var rpta = new List<FormularioProgresivoInicialDTO>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.V_ObtenerFormulariosIniciales ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoInicialDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1) sin formulario de respuesta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosInicialesSinFormularioRespuesta()
        {
            try
            {
                var rpta = new List<FormularioProgresivoInicialDTO>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.V_ObtenerFormulariosInicialesSinRespuesta ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoInicialDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
