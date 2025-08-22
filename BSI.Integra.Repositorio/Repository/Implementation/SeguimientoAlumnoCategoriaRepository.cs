using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AreaTrabajoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_SeguimientoAlumnoCategoria
    /// </summary>
    public class SeguimientoAlumnoCategoriaRepository : GenericRepository<TSeguimientoAlumnoCategorium>, ISeguimientoAlumnoCategoriaRepository
    {
        private Mapper _mapper;

        public SeguimientoAlumnoCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeguimientoAlumnoCategorium, SeguimientoAlumnoCategoria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSeguimientoAlumnoCategorium MapeoEntidad(SeguimientoAlumnoCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TSeguimientoAlumnoCategorium modelo = _mapper.Map<TSeguimientoAlumnoCategorium>(entidad);

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

        public TSeguimientoAlumnoCategorium Add(SeguimientoAlumnoCategoria entidad)
        {
            try
            {
                var SeguimientoAlumnoCategoria = MapeoEntidad(entidad);
                base.Insert(SeguimientoAlumnoCategoria);
                return SeguimientoAlumnoCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSeguimientoAlumnoCategorium Update(SeguimientoAlumnoCategoria entidad)
        {
            try
            {
                var SeguimientoAlumnoCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SeguimientoAlumnoCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(SeguimientoAlumnoCategoria);
                return SeguimientoAlumnoCategoria;
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


        public IEnumerable<TSeguimientoAlumnoCategorium> Add(IEnumerable<SeguimientoAlumnoCategoria> listadoEntidad)
        {
            try
            {
                List<TSeguimientoAlumnoCategorium> listado = new List<TSeguimientoAlumnoCategorium>();
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

        public IEnumerable<TSeguimientoAlumnoCategorium> Update(IEnumerable<SeguimientoAlumnoCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeguimientoAlumnoCategorium> listado = new List<TSeguimientoAlumnoCategorium>();
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
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de Seguiemiento Alumno Categoría
        /// </summary>
        /// <returns></returns>
        public List<FiltroSeguimientoAlumnoCategoriaDTO> ObtenerSeguimientoAlumnoCategoria()
        {
            try
            {
                var listaSeguimientoAlumnoCategoria = new List<FiltroSeguimientoAlumnoCategoriaDTO>();
                string query = $@"SELECT 
                                    Id, Nombre, IdTipoSeguimientoAlumnoCategoria, AplicaModalidadOnline, AplicaModalidadAonline, AplicaModalidadPresencial
                                FROM 
                                    mkt.V_ObtenerSeguimientoAlumnoCategoria
                                WHERE 
                                    EstadoSeguimientoAlumnoCategoria = 1 AND EstadoTipoSeguimientoAlumnoCategoria = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaSeguimientoAlumnoCategoria = JsonConvert.DeserializeObject<List<FiltroSeguimientoAlumnoCategoriaDTO>>(resultado);
                }
                return listaSeguimientoAlumnoCategoria;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 11/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de Seguiemiento Alumno Categoría
        /// </summary>
        /// <returns></returns>
        public List<ComentarioConfiguracionDTO> ObtenerConfiguracion()
        {
            try
            {
                var listaConfiguracion = new List<ComentarioConfiguracionDTO>();
                string query = $@"SELECT IdTipoSeguimiento,
                                    NombreTipoSeguimiento,
                                    IdTipoSeguimientoCategoria,
                                    NombreSeguimientoCategoria,
                                    IdEstadoMatricula,
                                    EstadoMatricula,
                                    IdSubEstadoMatricula,
                                    SubEstadoMatricula FROM [ope].[V_ObtenerConfiguracionCategoriaComentario]  WHERE Estado=1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaConfiguracion = JsonConvert.DeserializeObject<List<ComentarioConfiguracionDTO>>(resultado);
                }
                return listaConfiguracion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public SeguimientoAlumnoCategoria ObtenerPorId(int id)
        {
            try
            {
                var rpta = new SeguimientoAlumnoCategoria();
                var query = @"SELECT Id,
                                Nombre,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                AplicaModalidadOnline,
                                AplicaModalidadAonline,
                                AplicaModalidadPresencial,
                                IdSeguimientoAlumnoDetalle,
                                IdTipoSeguimientoAlumnoCategoria FROM ope.T_SeguimientoAlumnoCategoria
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<SeguimientoAlumnoCategoria>(resultado);
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
