using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: TipoExperienciaRepository
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 09/04/2024
    /// <summary>
    /// Gestión general de T_TipoExperiencia
    /// </summary>
    public class TipoExperienciaRepository : GenericRepository<TTipoExperiencium>, ITipoExperienciaRepository
    {
        private Mapper _mapper;

        public TipoExperienciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoExperiencium, TipoExperiencia>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoExperiencium MapeoEntidad(TipoExperiencia entidad)
        {
            try
            {
                TTipoExperiencium modelo = _mapper.Map<TTipoExperiencium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoExperiencium Add(TipoExperiencia entidad)
        {
            try
            {
                var tipoExperiencia = MapeoEntidad(entidad);
                base.Insert(tipoExperiencia);
                return tipoExperiencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoExperiencium Update(TipoExperiencia entidad)
        {
            try
            {
                var tipoExperiencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                tipoExperiencia.RowVersion = entidadExistente.RowVersion;

                base.Update(tipoExperiencia);
                return tipoExperiencia;
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
            base.Delete(id, usuario);
        }


        public IEnumerable<TTipoExperiencium> Add(IEnumerable<TipoExperiencia> listadoEntidad)
        {
            try
            {
                List<TTipoExperiencium> listado = new List<TTipoExperiencium>();
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

        public IEnumerable<TTipoExperiencium> Update(IEnumerable<TipoExperiencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TTipoExperiencium>();
                foreach (var entidad in listadoEntidad)
                    listado.Add(MapeoEntidad(entidad));

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


        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoExperiencia.
        /// </summary>
        /// <returns>IEnumerable TipoExperienciaDTO</returns>
        IEnumerable<TipoExperienciaDTO> ITipoExperienciaRepository.Obtener()
        {
            try
            {
                var rpta = new List<TipoExperienciaDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM gp.T_TipoExperiencia
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoExperienciaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_TipoExperiencia asociado a un identificador.
        /// </summary>
        /// <param name="idTipoExperiencia">Id de Tipo Experiencia</param>
        /// <returns>TipoExperienciaDTO</returns>
        public TipoExperiencia? ObtenerPorId(int idTipoExperiencia)
        {
            try
            {
                TipoExperiencia rpta = new();
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
                    FROM gp.T_TipoExperiencia
                    WHERE Estado = 1 AND Id = @idTipoExperiencia";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idTipoExperiencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoExperiencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }

    }
}
