using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class CategoriaEvaluacionRepository : GenericRepository<TEvaluacionCategorium>, ICategoriaEvaluacionRepository
    {
        private Mapper _mapper;

        public CategoriaEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEvaluacionCategorium, CategoriaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriaEvaluacion, CategoriaEvaluacionDTO>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEvaluacionCategorium MapeoEntidad(CategoriaEvaluacion entidad)
        {
            try
            {
                //crea la entidad padre
                TEvaluacionCategorium modelo = _mapper.Map<TEvaluacionCategorium>(entidad);

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

        public TEvaluacionCategorium Add(CategoriaEvaluacion entidad)
        {
            try
            {
                var CategoriaEvaluacion = MapeoEntidad(entidad);
                base.Insert(CategoriaEvaluacion);
                return CategoriaEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEvaluacionCategorium Update(CategoriaEvaluacion entidad)
        {
            try
            {
                var CategoriaEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CategoriaEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(CategoriaEvaluacion);
                return CategoriaEvaluacion;
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


        public IEnumerable<TEvaluacionCategorium> Add(IEnumerable<CategoriaEvaluacion> listadoEntidad)
        {
            try
            {
                List<TEvaluacionCategorium> listado = new List<TEvaluacionCategorium>();
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

        public IEnumerable<TEvaluacionCategorium> Update(IEnumerable<CategoriaEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEvaluacionCategorium> listado = new List<TEvaluacionCategorium>();
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
        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 03/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaEvaluacion.
        /// </summary>
        /// <returns> List<CategoriaEvaluacion> </returns>
        public IEnumerable<CategoriaEvaluacionDTO> Obtener()
        {
            try
            {
                List<CategoriaEvaluacionDTO> rpta = new List<CategoriaEvaluacionDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_EvaluacionCategoria
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CategoriaEvaluacionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 03/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CategoriaEvaluacion || null</returns>
        public CategoriaEvaluacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_EvaluacionCategoria
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CategoriaEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

    }
}
