using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{

    /// Repositorio: GradoEstudioRepository
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 04/04/2024
    /// <summary>
    /// Gestión general de T_GradoEstudio
    /// </summary>
    public class GradoEstudioRepository : GenericRepository<TGradoEstudio>, IGradoEstudioRepository
    {
        private Mapper _mapper;

        public GradoEstudioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGradoEstudio, GradoEstudio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGradoEstudio MapeoEntidad(GradoEstudio entidad)
        {
            try
            {
                TGradoEstudio modelo = _mapper.Map<TGradoEstudio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGradoEstudio Add(GradoEstudio entidad)
        {
            try
            {
                var gradoEstudio = MapeoEntidad(entidad);
                base.Insert(gradoEstudio);
                return gradoEstudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGradoEstudio Update(GradoEstudio entidad)
        {
            try
            {
                var gradoEstudio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                gradoEstudio.RowVersion = entidadExistente.RowVersion;

                base.Update(gradoEstudio);
                return gradoEstudio;
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


        public IEnumerable<TGradoEstudio> Add(IEnumerable<GradoEstudio> listadoEntidad)
        {
            try
            {
                List<TGradoEstudio> listado = new List<TGradoEstudio>();
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

        public IEnumerable<TGradoEstudio> Update(IEnumerable<GradoEstudio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TGradoEstudio>();
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
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GradoEstudio.
        /// </summary>
        /// <returns>IEnumerable GradoEstudioDTO</returns>
        IEnumerable<GradoEstudioDTO> IGradoEstudioRepository.Obtener()
        {
            try
            {
                var rpta = new List<GradoEstudioDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM gp.T_GradoEstudio
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GradoEstudioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_GradoEstudio asociado a un identificador.
        /// </summary>
        /// <param name="idGradoEstudio">Id de GradoEstudio</param>
        /// <returns>GradoEstudioDTO</returns>
        public GradoEstudio? ObtenerPorId(int idGradoEstudio)
        {
            try
            {
                GradoEstudio rpta = new();
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
                    FROM gp.T_GradoEstudio
                    WHERE Estado = 1 AND Id = @idGradoEstudio";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idGradoEstudio });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<GradoEstudio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 03-02-2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoEstudio.
        /// </summary>
        /// <returns>IEnumerable GradoEstudioDTO</returns>
        public IEnumerable<GradoEstudioDTO> ObtenerEstadoEstudio()
        {
            try
            {
                var rpta = new List<GradoEstudioDTO>();
                var query = @"
                    SELECT Id, Nombre FROM gp.T_EstadoEstudio WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GradoEstudioDTO>>(resultado);
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
