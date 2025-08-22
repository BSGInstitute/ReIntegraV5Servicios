using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProgramaGeneralPuntoCorteConfiguracionRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorteConfiguracion
    /// </summary>
    public class ProgramaGeneralPuntoCorteConfiguracionRepository : GenericRepository<TProgramaGeneralPuntoCorteConfiguracion>, IProgramaGeneralPuntoCorteConfiguracionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPuntoCorteConfiguracion, ProgramaGeneralPuntoCorteConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralPuntoCorteConfiguracion MapeoEntidad(ProgramaGeneralPuntoCorteConfiguracion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralPuntoCorteConfiguracion modelo = _mapper.Map<TProgramaGeneralPuntoCorteConfiguracion>(entidad);

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

        public TProgramaGeneralPuntoCorteConfiguracion Add(ProgramaGeneralPuntoCorteConfiguracion entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorteConfiguracion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralPuntoCorteConfiguracion);
                return ProgramaGeneralPuntoCorteConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralPuntoCorteConfiguracion Update(ProgramaGeneralPuntoCorteConfiguracion entidad)
        {
            try
            {
                var ProgramaGeneralPuntoCorteConfiguracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralPuntoCorteConfiguracion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralPuntoCorteConfiguracion);
                return ProgramaGeneralPuntoCorteConfiguracion;
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


        public IEnumerable<TProgramaGeneralPuntoCorteConfiguracion> Add(IEnumerable<ProgramaGeneralPuntoCorteConfiguracion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralPuntoCorteConfiguracion> listado = new List<TProgramaGeneralPuntoCorteConfiguracion>();
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

        public IEnumerable<TProgramaGeneralPuntoCorteConfiguracion> Update(IEnumerable<ProgramaGeneralPuntoCorteConfiguracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralPuntoCorteConfiguracion> listado = new List<TProgramaGeneralPuntoCorteConfiguracion>();
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

        public List<ProgramaGeneralPuntoCorteConfiguracionDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralPuntoCorteConfiguracionDTO> rtpa = new List<ProgramaGeneralPuntoCorteConfiguracionDTO>();
                var query = @"
                    SELECT Id,
	                    IdTipoCorte,
	                    Tipo,
	                    IdAreaCapacitacion,
	                    IdSubAreaCapacitacion,
	                    IdPGeneral,
	                    Color,
	                    Texto,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM pla.T_ProgramaGeneralPuntoCorteConfiguracion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rtpa = JsonConvert.DeserializeObject<List<ProgramaGeneralPuntoCorteConfiguracionDTO>>(resultado)!;
                }
                return rtpa;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
