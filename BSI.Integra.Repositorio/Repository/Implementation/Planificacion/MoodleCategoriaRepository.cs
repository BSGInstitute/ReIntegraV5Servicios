using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MoodleCategoriaRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión general de T_MoodleCategoria
    /// </summary>
    public class MoodleCategoriaRepository : GenericRepository<TMoodleCategorium>, IMoodleCategoriaRepository
    {
        private Mapper _mapper;
        public MoodleCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCategorium, MoodleCategoria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMoodleCategorium MapeoEntidad(MoodleCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TMoodleCategorium modelo = _mapper.Map<TMoodleCategorium>(entidad);

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

        public TMoodleCategorium Add(MoodleCategoria entidad)
        {
            try
            {
                var MoodleCategoria = MapeoEntidad(entidad);
                base.Insert(MoodleCategoria);
                return MoodleCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMoodleCategorium Update(MoodleCategoria entidad)
        {
            try
            {
                var MoodleCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MoodleCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(MoodleCategoria);
                return MoodleCategoria;
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


        public IEnumerable<TMoodleCategorium> Add(IEnumerable<MoodleCategoria> listadoEntidad)
        {
            try
            {
                List<TMoodleCategorium> listado = new List<TMoodleCategorium>();
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

        public IEnumerable<TMoodleCategorium> Update(IEnumerable<MoodleCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMoodleCategorium> listado = new List<TMoodleCategorium>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns> List<CategoriaMoodleDTO> </returns>
        public List<MoodleCategoriaDetalle> Obtener()
        {
            try
            {
                List<MoodleCategoriaDetalle> listaMoodleCategoria = new List<MoodleCategoriaDetalle>();
                var query = @"SELECT Id,
                                       IdCategoriaMoodle,
                                       NombreCategoria,
                                       MoodleCategoriaTipo,
                                       IdMoodleCategoriaTipo,
                                       AplicaProyecto
                                FROM [ope].[V_TMoodleCategoria_CategoriaTipoNombre]
                                WHERE Estado = 1 ORDER BY Id DESC";
                var res = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaMoodleCategoria = JsonConvert.DeserializeObject<List<MoodleCategoriaDetalle>>(res);
                }
                return listaMoodleCategoria;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Alumno asociado a un Id.
        /// </summary>
        /// <param name="id">Id del MoodleCategoria</param>
        /// <returns> MoodleCategoria </returns>
        public MoodleCategoria? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,IdMoodleCategoriaTipo,
                                       IdCategoriaMoodle,
                                       NombreCategoria,
                                       AplicaProyecto,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       IdMigracion
                                FROM ope.T_MoodleCategoria
                                WHERE Id = @Id
                                AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MoodleCategoria>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Sergio Yepez.
        /// Fecha: 11/12/2024
        /// Version: 1.0
        /// <summary>
        /// Valida si una Categoria Moodle existe y está vigente.
        /// </summary>
        /// <param name="id"> IdCategoriaMoodle </param>
        /// <returns> boolean </returns>
        public bool ExisteCategoriaMoodle(int idCategoriaMoodle)
        {
            try
            {
                var query = @"SELECT *
                            FROM ope.T_MoodleCategoria
                            WHERE IdCategoriaMoodle = @idCategoriaMoodle
                            AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdCategoriaMoodle = idCategoriaMoodle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
