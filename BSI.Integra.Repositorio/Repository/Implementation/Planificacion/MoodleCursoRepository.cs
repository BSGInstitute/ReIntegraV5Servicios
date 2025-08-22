using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MoodleCursoRepository
    /// Autor Modificacion: Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_MoodleCurso
    /// </summary>
    public class MoodleCursoRepository : GenericRepository<TMoodleCurso>, IMoodleCursoRepository
    {
        private Mapper _mapper;
        public MoodleCursoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCurso, MoodleCurso>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMoodleCurso MapeoEntidad(MoodleCurso entidad)
        {
            try
            {
                //crea la entidad padre
                TMoodleCurso modelo = _mapper.Map<TMoodleCurso>(entidad);

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

        public TMoodleCurso Add(MoodleCurso entidad)
        {
            try
            {
                var MoodleCurso = MapeoEntidad(entidad);
                base.Insert(MoodleCurso);
                return MoodleCurso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMoodleCurso Update(MoodleCurso entidad)
        {
            try
            {
                var MoodleCurso = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MoodleCurso.RowVersion = entidadExistente.RowVersion;

                base.Update(MoodleCurso);
                return MoodleCurso;
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


        public IEnumerable<TMoodleCurso> Add(IEnumerable<MoodleCurso> listadoEntidad)
        {
            try
            {
                List<TMoodleCurso> listado = new List<TMoodleCurso>();
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

        public IEnumerable<TMoodleCurso> Update(IEnumerable<MoodleCurso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMoodleCurso> listado = new List<TMoodleCurso>();
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
        public bool Exist(int id)
        {
            try
            {
                base.Exist(id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Sergio Yepez
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns> List<CategoriaMoodleDTO> </returns>
        public List<MoodleCursoDTO> ObtenerCursosMoodleRegistradas()
        {
            try
            {
                List<MoodleCursoDTO> listaMoodleCurso = new List<MoodleCursoDTO>();
                                var query = @"SELECT Id, IdCursoMoodle, idCategoriaMoodle, NombreCategoria, NombreCursoMoodle FROM[ope].[V_TMoodleCurso_ObtenerCursos] WHERE Estado = 1 AND EstadoCategoria = 1 ORDER BY FechaCreacion DESC";

                var res = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaMoodleCurso = JsonConvert.DeserializeObject<List<MoodleCursoDTO>>(res);
                }
                return listaMoodleCurso;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor Modificacion: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Alumno asociado a un Id.
        /// </summary>
        /// <param name="id">Id del MoodleCurso</param>
        /// <returns> MoodleCurso </returns>
        public MoodleCurso ObtenerPorId(int id)
        {
            try
            {
                MoodleCurso rpta = new MoodleCurso();
                var query = @" SELECT 
                                       Id,
                                       IdCursoMoodle,
                                       IdCategoriaMoodle,
                                       Nombre, 
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       IdMigracion
                                FROM ope.T_MoodleCurso
                                WHERE Id = @Id
                                AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id=id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MoodleCurso>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de una Vista asociado a un Id.
        /// </summary>
        /// <param name="id">Id del MoodleCurso</param>
        /// <returns> MoodleCurso </returns>
        /// 
        public MoodleCursoDTO ObtenerCursoPorId(int id)
        {
            try
            {
                MoodleCursoDTO rpta = new MoodleCursoDTO();
                var query = @" SELECT Id, IdCursoMoodle, idCategoriaMoodle, NombreCategoria, NombreCursoMoodle FROM[ope].[V_TMoodleCurso_ObtenerCursos] WHERE Estado = 1 AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<MoodleCursoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Creación: Sergio Yepez
        /// Fecha: 11/12/2024
        /// Version: 1.0
        /// <summary>
        /// Verifica si algun Curso Moodle existe y está vigente.
        /// </summary>
        /// <param name="id">IdMoodleCurso</param>
        /// <returns> true / false </returns>
        /// 
        public bool ExisteCursoMoodle(int idCursoMoodle)
        {
            try
            {
                var query = @" SELECT * FROM [ope].[T_MoodleCurso] WHERE Estado = 1 AND idCursoMoodle = @idCursoMoodle;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCursoMoodle = idCursoMoodle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

    }
}
