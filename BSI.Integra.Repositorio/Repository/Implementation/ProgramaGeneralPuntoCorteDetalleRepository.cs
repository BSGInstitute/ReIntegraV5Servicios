using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralPuntoCorteDetalleRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorteDetalle
    /// </summary>
    public class ProgramaGeneralPuntoCorteDetalleRepository : GenericRepository<TProgramaGeneralPuntoCorteDetalle>, IProgramaGeneralPuntoCorteDetalleRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPuntoCorteDetalle MapeoEntidad(ProgramaGeneralPuntoCorteDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPuntoCorteDetalle modelo = _mapper.Map<TProgramaGeneralPuntoCorteDetalle>(entidad);

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

        public TProgramaGeneralPuntoCorteDetalle Add(ProgramaGeneralPuntoCorteDetalle entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorteDetalle = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPuntoCorteDetalle);
                return ProgramaGeneralPuntoCorteDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPuntoCorteDetalle Update(ProgramaGeneralPuntoCorteDetalle entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorteDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPuntoCorteDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPuntoCorteDetalle);
                return ProgramaGeneralPuntoCorteDetalle;
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


        public IEnumerable<TProgramaGeneralPuntoCorteDetalle> Add(IEnumerable<ProgramaGeneralPuntoCorteDetalle> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPuntoCorteDetalle> listado = new List<TProgramaGeneralPuntoCorteDetalle>();
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

        public IEnumerable<TProgramaGeneralPuntoCorteDetalle> Update(IEnumerable<ProgramaGeneralPuntoCorteDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPuntoCorteDetalle> listado = new List<TProgramaGeneralPuntoCorteDetalle>();
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

        public List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerPorIdProgramaGeneralPuntoCorte(int idProgramaGeneralPuntoCorte)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteDetalleDTO> rpta = new List<ProgramaGeneralPuntoCorteDetalleDTO>();
                string query = @"
                    SELECT Id,
		                IdProgramaGeneralPuntoCorte,
		                IdPuntoCorte,
		                Tipo,
		                Descripcion,
		                ValorMinimo,
		                ValorMaximo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
	                FROM pla.T_ProgramaGeneralPuntoCorteDetalle 
	                WHERE Estado = 1 AND IdProgramaGeneralPuntoCorte = @IdProgramaGeneralPuntoCorte";

                var resultado = _dapperRepository.QueryDapper(query, new { IdProgramaGeneralPuntoCorte = idProgramaGeneralPuntoCorte });
                if (!string.IsNullOrEmpty(resultado) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteDetalleDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerPorIdProgramaGeneralPuntoCorteIdPuntoCorte(int idProgramaGeneralPuntoCorte, int idPuntoCorte)
        {
            try
            {
                List<ProgramaGeneralPuntoCorteDetalleDTO> rpta = new List<ProgramaGeneralPuntoCorteDetalleDTO>();
                string query = @"
                    SELECT Id,
		                IdProgramaGeneralPuntoCorte,
		                IdPuntoCorte,
		                Tipo,
		                Descripcion,
		                ValorMinimo,
		                ValorMaximo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
	                FROM pla.T_ProgramaGeneralPuntoCorteDetalle 
	                WHERE Estado = 1 AND IdProgramaGeneralPuntoCorte = @IdProgramaGeneralPuntoCorte AND IdPuntoCorte = idPuntoCorte";

                var resultado = _dapperRepository.QueryDapper(query, new { IdProgramaGeneralPuntoCorte = idProgramaGeneralPuntoCorte, IdPuntoCorte = idPuntoCorte });
                if (!string.IsNullOrEmpty(resultado) && !query.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteDetalleDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}