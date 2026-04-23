using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoDescuentoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_TipoDescuento
    /// </summary>
    public class TipoDescuentoRepository : GenericRepository<TTipoDescuento>, ITipoDescuentoRepository
    {
        private Mapper _mapper;

        public TipoDescuentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDescuento, TipoDescuento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoDescuento MapeoEntidad(TipoDescuento entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoDescuento modelo = _mapper.Map<TTipoDescuento>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoDescuento Add(TipoDescuento entidad)
        {
            try
            {
                var TipoDescuento = MapeoEntidad(entidad);
                base.Insert(TipoDescuento);
                return TipoDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoDescuento Update(TipoDescuento entidad)
        {
            try
            {
                var TipoDescuento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDescuento.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDescuento);
                return TipoDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
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


        public IEnumerable<TTipoDescuento> Add(IEnumerable<TipoDescuento> listadoEntidad)
        {
            try
            {
                List<TTipoDescuento> listado = new List<TTipoDescuento>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TTipoDescuento> Update(IEnumerable<TipoDescuento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDescuento> listado = new List<TTipoDescuento>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento.
        /// </summary>
        /// <returns> List<TipoDescuentoDTO> </returns>
        public IEnumerable<TipoDescuentoDTO> ObtenerTipoDescuento()
        {
            try
            {
                IEnumerable<TipoDescuentoDTO> rpta = new List<TipoDescuentoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    Descripcion,
	                    Formula,
	                    PorcentajeGeneral,
	                    PorcentajeMatricula,
	                    FraccionesMatricula,
	                    PorcentajeCuotas,
	                    CuotasAdicionales,
	                    IdTipoDescuentoNivelAprobacion
                    FROM pla.T_TipoDescuento
                    WHERE Estado = 1 ORDER BY id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<TipoDescuentoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/03/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento.
        /// </summary>
        /// <returns> List<TipoDescuento> </returns>
        public TipoDescuento ObtenerPorId(int idTipoDescuento)
        {
            try
            {
                TipoDescuento rpta = new TipoDescuento();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    Descripcion,
	                    Formula,
	                    PorcentajeGeneral,
	                    PorcentajeMatricula,
	                    FraccionesMatricula,
	                    PorcentajeCuotas,
	                    CuotasAdicionales,
	                    IdTipoDescuentoNivelAprobacion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_TipoDescuento
                    WHERE Estado = 1 AND Id=@idTipoDescuento";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoDescuento>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/03/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento.
        /// </summary>
        /// <returns> List<TipoDescuento> </returns>
        public IEnumerable<TipoDescuento> ObtenerPorIds(int idTipoDescuento)
        {
            try
            {
                IEnumerable<TipoDescuento> rpta = new List<TipoDescuento>();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    Descripcion,
	                    Formula,
	                    PorcentajeGeneral,
	                    PorcentajeMatricula,
	                    FraccionesMatricula,
	                    PorcentajeCuotas,
	                    CuotasAdicionales,
	                    IdTipoDescuentoNivelAprobacion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_TipoDescuento
                    WHERE Estado = 1 AND Id=@idTipoDescuento";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<TipoDescuento>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/03/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento.
        /// </summary>
        /// <returns> List<TipoDescuento> </returns>
        public async Task<TipoDescuento> ObtenerPorIdAsync(int idTipoDescuento)
        {
            try
            {
                TipoDescuento rpta = new TipoDescuento();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    Descripcion,
	                    Formula,
	                    PorcentajeGeneral,
	                    PorcentajeMatricula,
	                    FraccionesMatricula,
	                    PorcentajeCuotas,
	                    CuotasAdicionales,
	                    IdTipoDescuentoNivelAprobacion,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_TipoDescuento
                    WHERE Estado = 1 AND Id=@idTipoDescuento";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoDescuento>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoDescuento para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoDescuentoComboDTO> </returns>
        public IEnumerable<TipoDescuentoComboDTO> ObtenerCombo()
        {
            try
            {
                List<TipoDescuentoComboDTO> rpta = new List<TipoDescuentoComboDTO>();
                var query = @"SELECT Id,Codigo,Descripcion FROM pla.T_TipoDescuento WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDescuentoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<TipoDescuentoComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,Codigo,Descripcion FROM pla.T_TipoDescuento WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TipoDescuentoComboDTO>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.1
        /// Modificado: Lolo Zaa - 15/01/2026
        /// <summary>
        /// Obtiene Tipos de Descuento asociados a una Oportunidad y un Tipo de Personal.
        /// Incluye descuentos de la vista mkt.V_TiposDescuentos y descuentos solicitados de pla.V_TiposDescuentosSolicitud
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="tipoPersonal">Tipo de Personal</param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoOportunidad(int idOportunidad, string tipoPersonal)
        {
            try
            {
                List<TipoDescuentoOportunidadDTO> tiposDescuento = new List<TipoDescuentoOportunidadDTO>();
                var query = @"
                    SELECT Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo, AplicaPrograma
                    FROM mkt.V_TiposDescuentos
                    WHERE IdOportunidad = @idOportunidad AND Tipo = @tipoPersonal and Id>226
                    UNION
                    SELECT IdTipoDescuento AS Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo, AplicaPrograma
                    FROM pla.V_TiposDescuentosSolicitudOportunidad
                    WHERE IdOportunidad = @idOportunidad and IdTipoDescuento>226";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad, tipoPersonal });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    tiposDescuento = JsonConvert.DeserializeObject<List<TipoDescuentoOportunidadDTO>>(resultadoQuery);
                }
                return tiposDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        /// Autor: Lolo Zaa
        /// Fecha: 16/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tipos de Descuento de solicitudes activas asociadas a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoSolicitudOportunidad(int idOportunidad)
        {
            try
            {
                List<TipoDescuentoOportunidadDTO> tiposDescuento = new List<TipoDescuentoOportunidadDTO>();
                var query = @"
                    SELECT IdTipoDescuento AS Id,Codigo,Descripcion,Formula,PorcentajeGeneral,PorcentajeMatricula,FraccionesMatricula,PorcentajeCuotas,CuotasAdicionales,Tipo
                    FROM pla.V_TiposDescuentosSolicitudOportunidad
                    WHERE IdOportunidad = @idOportunidad";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    tiposDescuento = JsonConvert.DeserializeObject<List<TipoDescuentoOportunidadDTO>>(resultadoQuery);
                }
                return tiposDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tipos de descuento con su nivel de aprobación asociado
        /// </summary>
        /// <returns> List<TipoDescuentoConNivelAprobacionDTO> </returns>
        public IEnumerable<TipoDescuentoConNivelAprobacionDTO> ObtenerTipoDescuentoConNivelAprobacion()
        {
            try
            {
                IEnumerable<TipoDescuentoConNivelAprobacionDTO> rpta = new List<TipoDescuentoConNivelAprobacionDTO>();
                var query = @"
                    SELECT
                        Id,
                        Codigo,
                        Descripcion,
                        Formula,
                        PorcentajeGeneral,
                        PorcentajeMatricula,
                        FraccionesMatricula,
                        PorcentajeCuotas,
                        CuotasAdicionales,
                        IdTipoDescuentoNivelAprobacion,
	                    AplicaProgramaCompleto AS AplicaPrograma
                    FROM pla.T_TipoDescuento
                    WHERE Estado = 1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<TipoDescuentoConNivelAprobacionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los niveles de aprobación activos
        /// </summary>
        /// <returns> List<TipoDescuentoNivelAprobacionDTO> </returns>
        public IEnumerable<TipoDescuentoNivelAprobacionDTO> ObtenerNivelesAprobacion()
        {
            try
            {
                IEnumerable<TipoDescuentoNivelAprobacionDTO> rpta = new List<TipoDescuentoNivelAprobacionDTO>();
                var query = @"
                    SELECT
                        Id,
                        Nombre,
                        Descripcion,
                        Estado
                    FROM pla.T_TipoDescuentoNivelAprobacion
                    WHERE Estado = 1
                    ORDER BY Id ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<TipoDescuentoNivelAprobacionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: jllanque
        /// Fecha: 16/04/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene información del tipo de programa (Programa/Curso) a partir de un IdCentroCosto.
        /// Cadena: CentroCosto → PEspecifico → PGeneral → TipoPrograma
        /// </summary>
        /// <param name="idCentroCosto">Id del Centro de Costo</param>
        /// <returns> InfoProgramaCentroCostoDTO </returns>
        public InfoProgramaCentroCostoDTO ObtenerInfoProgramaPorCentroCosto(int idCentroCosto)
        {
            try
            {
                InfoProgramaCentroCostoDTO rpta = null;
                var query = @"
                    SELECT
                        cc.Id AS IdCentroCosto,
                        cc.Nombre AS NombreCentroCosto,
                        pe.Id AS IdPEspecifico,
                        pe.Nombre AS NombrePEspecifico,
                        pg.Id AS IdPGeneral,
                        pg.Nombre AS NombrePGeneral,
                        pg.IdTipoPrograma,
                        tp.Nombre AS NombreTipoPrograma
                    FROM pla.T_CentroCosto cc
                    LEFT JOIN pla.T_PEspecifico pe ON pe.IdCentroCosto = cc.Id AND pe.Estado = 1
                    LEFT JOIN pla.T_PGeneral pg ON pg.Id = pe.IdProgramaGeneral AND pg.Estado = 1
                    LEFT JOIN pla.T_TipoPrograma tp ON tp.Id = pg.IdTipoPrograma AND tp.Estado = 1
                    WHERE cc.Id = @idCentroCosto AND cc.Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<InfoProgramaCentroCostoDTO>(resultado)!;
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
