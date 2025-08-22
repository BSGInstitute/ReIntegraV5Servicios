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
    /// Repositorio: ProcedenciaFormularioDetalleRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ProcedenciaFormularioDetalle
    /// </summary>
    public class ProcedenciaFormularioDetalleRepository : GenericRepository<TProcedenciaFormularioDetalle>, IProcedenciaFormularioDetalleRepository
    {
        private Mapper _mapper;

        public ProcedenciaFormularioDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcedenciaFormularioDetalle, ProcedenciaFormularioDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProcedenciaFormularioDetalle MapeoEntidad(ProcedenciaFormularioDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TProcedenciaFormularioDetalle modelo = _mapper.Map<TProcedenciaFormularioDetalle>(entidad);

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

        public TProcedenciaFormularioDetalle Add(ProcedenciaFormularioDetalle entidad)
        {
            try
            {
                var ProcedenciaFormularioDetalle = MapeoEntidad(entidad);
                base.Insert(ProcedenciaFormularioDetalle);
                return ProcedenciaFormularioDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProcedenciaFormularioDetalle Update(ProcedenciaFormularioDetalle entidad)
        {
            try
            {
                var ProcedenciaFormularioDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcedenciaFormularioDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ProcedenciaFormularioDetalle);
                return ProcedenciaFormularioDetalle;
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


        public IEnumerable<TProcedenciaFormularioDetalle> Add(IEnumerable<ProcedenciaFormularioDetalle> listadoEntidad)
        {
            try
            {
                List<TProcedenciaFormularioDetalle> listado = new List<TProcedenciaFormularioDetalle>();
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

        public IEnumerable<TProcedenciaFormularioDetalle> Update(IEnumerable<ProcedenciaFormularioDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcedenciaFormularioDetalle> listado = new List<TProcedenciaFormularioDetalle>();
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
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProcedenciaFormularioDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ProcedenciaFormularioDetalle WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProcedenciaFormularioDetalle
        /// </summary>
        /// <returns> List<ProcedenciaFormularioDetalleDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDetalleDTO> ObtenerProcedenciaFormularioDetalle()
        {
            try
            {
                List<ProcedenciaFormularioDetalleDTO> rpta = new List<ProcedenciaFormularioDetalleDTO>();
                var query = @"SELECT Id, IdProcedenciaFormulario, IdTipoInteraccion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM mkt.T_ProcedenciaFormularioDetalle
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcedenciaFormularioDetalleDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_PasarelaPagoPw relacionados a un Alumno.
        /// </summary>
        /// <param name="IdProcedenciaFormulario">Id del Alumno</param>
        /// <returns> List<PasarelaPagoPwAgendaDTO> </returns>
        public IEnumerable<ProcedenciaFormularioDetalleInteraccionDTO> ObtenerProcedenciaFormularioDetallePorIdProcedenciaFormulario(int IdProcedenciaFormulario)
        {
            try
            {
                List<ProcedenciaFormularioDetalleInteraccionDTO> rpta = new List<ProcedenciaFormularioDetalleInteraccionDTO>();
                //var query = @"SELECT Id, IdProcedenciaFormulario, IdTipoInteraccion, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                //        FROM mkt.T_ProcedenciaFormularioDetalle
                //        WHERE IdProcedenciaFormulario = @IdProcedenciaFormulario AND Estado=1";

                var query = @"SELECT detalle.Id, detalle.IdProcedenciaFormulario, detalle.IdTipoInteraccion, tipo.Nombre AS TipoInteraccion, tipo.Canal AS Canal, detalle.UsuarioModificacion, detalle.FechaModificacion 
                        FROM mkt.T_ProcedenciaFormularioDetalle AS detalle
                        INNER JOIN mkt.T_TipoInteracccion AS tipo ON tipo.Id = detalle.IdTipoInteraccion AND tipo.Estado=1
                        WHERE detalle.IdProcedenciaFormulario = @IdProcedenciaFormulario AND detalle.Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcedenciaFormulario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProcedenciaFormularioDetalleInteraccionDTO>>(resultado);
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
