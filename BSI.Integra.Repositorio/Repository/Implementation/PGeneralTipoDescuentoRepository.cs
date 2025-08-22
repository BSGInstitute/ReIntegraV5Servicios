using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PGeneralTipoDescuentoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_PGeneralTipoDescuento
    /// </summary>
    public class PGeneralTipoDescuentoRepository : GenericRepository<TPgeneralTipoDescuento>, IPGeneralTipoDescuentoRepository
    {
        private Mapper _mapper;

        public PGeneralTipoDescuentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralTipoDescuento, PGeneralTipoDescuento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralTipoDescuento, PGeneralTipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneralTipoDescuento, PGeneralTipoDescuentoDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralTipoDescuento MapeoEntidad(PGeneralTipoDescuento entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralTipoDescuento modelo = _mapper.Map<TPgeneralTipoDescuento>(entidad);

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

        public TPgeneralTipoDescuento Add(PGeneralTipoDescuento entidad)
        {
            try
            {
                var PGeneralTipoDescuento = MapeoEntidad(entidad);
                base.Insert(PGeneralTipoDescuento);
                return PGeneralTipoDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralTipoDescuento Update(PGeneralTipoDescuento entidad)
        {
            try
            {
                var PGeneralTipoDescuento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralTipoDescuento.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneralTipoDescuento);
                return PGeneralTipoDescuento;
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


        public IEnumerable<TPgeneralTipoDescuento> Add(IEnumerable<PGeneralTipoDescuento> listadoEntidad)
        {
            try
            {
                List<TPgeneralTipoDescuento> listado = new List<TPgeneralTipoDescuento>();
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

        public IEnumerable<TPgeneralTipoDescuento> Update(IEnumerable<PGeneralTipoDescuento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralTipoDescuento> listado = new List<TPgeneralTipoDescuento>();
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneralTipoDescuento.
        /// </summary>
        /// <returns> List<PGeneralTipoDescuentoDTO> </returns>
        public IEnumerable<PGeneralTipoDescuentoDTO> ObtenerPGeneralTipoDescuento()
        {
            try
            {
                List<PGeneralTipoDescuentoDTO> rpta = new List<PGeneralTipoDescuentoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPGeneral,
	                    IdTipoDescuento,
	                    FlagPromocion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_PGeneralTipoDescuento
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralTipoDescuentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PGeneralTipoDescuento para mostrarse en combo.
        /// </summary>
        /// <returns> List<PGeneralTipoDescuentoComboDTO> </returns>
        public IEnumerable<PGeneralTipoDescuentoComboDTO> ObtenerCombo()
        {
            try
            {
                List<PGeneralTipoDescuentoComboDTO> rpta = new List<PGeneralTipoDescuentoComboDTO>();
                var query = @"
                    SELECT
	                    PGTD.Id,
	                    PG.Nombre AS PGeneral,
	                    TD.Codigo AS TipoDescuento
                    FROM pla.T_PGeneralTipoDescuento AS PGTD
                    INNER JOIN pla.T_PGeneral AS PG
	                    ON PGTD.IdPGeneral = PG.Id
	                    AND PG.Estado = 1
                    INNER JOIN pla.T_TipoDescuento AS TD
	                    ON PGTD.IdTipoDescuento = TD.Id
	                    AND TD.Estado = 1
                    WHERE PGTD.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralTipoDescuentoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Flag Promocion asociado a un Programa General y un Tipo Descuento
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="idTipoDescuento">Id del Tipo Descuento</param>
        /// <returns> BoolDTO </returns>
        public BoolDTO ObtenerFlagPromocion(int idPGeneral, int idTipoDescuento)
        {
            try
            {
                BoolDTO rpta = new BoolDTO();
                var query = @"
                            SELECT
	                            FlagPromocion AS Valor
                            FROM pla.T_PGeneralTipoDescuento
                            WHERE Estado = 1
	                            AND IdPGeneral = @idPGeneral
	                            AND IdTipoDescuento = @idTipoDescuento";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral, idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Flag Promocion asociado a un Programa General y un Tipo Descuento
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="idTipoDescuento">Id del Tipo Descuento</param>
        /// <returns> BoolDTO </returns>
        public async Task<BoolDTO> ObtenerFlagPromocionAsync(int idPGeneral, int idTipoDescuento)
        {
            try
            {
                BoolDTO rpta = new BoolDTO();
                var query = @"
                            SELECT
	                            FlagPromocion AS Valor
                            FROM pla.T_PGeneralTipoDescuento
                            WHERE Estado = 1
	                            AND IdPGeneral = @idPGeneral
	                            AND IdTipoDescuento = @idTipoDescuento";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idPGeneral, idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de Programas por IdTipoDescuento
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ObtenerProgramaPorDescuento(int idTipoDescuento)
        {
            try
            {
                IEnumerable<IntDTO> rpta = new List<IntDTO>();
                var query = @"
                    SELECT IdPGeneral AS Valor FROM pla.T_PGeneralTipoDescuento
                    WHERE Estado = 1 AND idTipoDescuento=@idTipoDescuento";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                }
                return rpta.Select(x => x.Valor.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error en ObtenerPorId() : {ex.Message}", ex);
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DescuentoPromocion.
        /// </summary>
        /// <returns> List<DescuentoPromocionDTO> </returns>
        public IEnumerable<PGeneralTipoDescuento> ObtenerPorId(int id)
        {
            try
            {
                IEnumerable<PGeneralTipoDescuento> rpta = new List<PGeneralTipoDescuento>();
                var query = @"
                    SELECT
	                    Id,IdPGeneral,IdTipoDescuento,FlagPromocion,Estado,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion
                    FROM pla.T_PGeneralTipoDescuento
                    WHERE Estado = 1 AND IdTipoDescuento=@Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<PGeneralTipoDescuento>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PGeneralTipoDescuento> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                IEnumerable<PGeneralTipoDescuento> rpta = new List<PGeneralTipoDescuento>();
                var query = @"
                    SELECT
	                    Id,IdPGeneral AS IdPgeneral,IdTipoDescuento,FlagPromocion
                    FROM pla.T_PGeneralTipoDescuento
                    WHERE Estado = 1 AND IdPGeneral=@idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<PGeneralTipoDescuento>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PGeneralTipoDescuento? ObtenerPorIdTipoDescuentoYIdPGeneral(int idPGeneral, int idTipoDescuento)
        {
            try
            {
                var query = @"
                    SELECT
	                     Id,
                         IdPGeneral AS IdPgeneral,
                         IdTipoDescuento,
                         FlagPromocion,
                         Estado,
                         UsuarioCreacion,
                         UsuarioModificacion,
                         FechaCreacion,
                         FechaModificacion,
                         RowVersion,
                         IdMigracion
                    FROM pla.T_PGeneralTipoDescuento
                    WHERE Estado = 1 AND IdPGeneral=@idPGeneral AND IdTipoDescuento =@idTipoDescuento";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral, idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PGeneralTipoDescuento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
