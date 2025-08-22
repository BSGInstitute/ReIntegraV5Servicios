using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 08/08/2022
    /// <summary>
    /// Gestión general de T_PlantillaPw
    /// </summary>
    public class PlantillaPwRepository : GenericRepository<TPlantillaPw>, IPlantillaPwRepository
    {
        private Mapper _mapper;

        public PlantillaPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaPw, PlantillaPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPlantillaRevisionPw, PlantillaRevisionPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPlantillaPai, PlantillaPais>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSeccionPw, SeccionPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSeccionTipoDetallePw, SeccionTipoDetallePw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPlantillaPw MapeoEntidad(PlantillaPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaPw modelo = _mapper.Map<TPlantillaPw>(entidad);

                //mapea los hijos  
                if (entidad.PlantillaPais != null && entidad.PlantillaPais.Count > 0)
                    modelo.TPlantillaPais = _mapper.Map<ICollection<TPlantillaPai>>(entidad.PlantillaPais);

                modelo.TSeccionPws = new List<TSeccionPw>();
                if (entidad.SeccionPws != null)
                {
                    foreach (var item in entidad.SeccionPws)
                    {
                        TSeccionPw seccionPw = _mapper.Map<TSeccionPw>(item);
                        //Mapea el hijo nivel2
                        seccionPw.TSeccionTipoDetallePws = _mapper.Map<ICollection<TSeccionTipoDetallePw>>(item.SeccionTipoDetallePws);
                        modelo.TSeccionPws.Add(seccionPw);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPlantillaPw Add(PlantillaPw entidad)
        {
            try
            {
                var PlantillaPw = MapeoEntidad(entidad);
                base.Insert(PlantillaPw);
                return PlantillaPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaPw Update(PlantillaPw entidad)
        {
            try
            {
                var PlantillaPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaPw);
                return PlantillaPw;
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


        public IEnumerable<TPlantillaPw> Add(IEnumerable<PlantillaPw> listadoEntidad)
        {
            try
            {
                List<TPlantillaPw> listado = new List<TPlantillaPw>();
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

        public IEnumerable<TPlantillaPw> Update(IEnumerable<PlantillaPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaPw> listado = new List<TPlantillaPw>();
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
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaPw.
        /// </summary>
        /// <returns> List<PlantillaPwDTO> </returns>
        public IEnumerable<PlantillaPwDTO> Obtener()
        {
            try
            {
                List<PlantillaPwDTO> rpta = new List<PlantillaPwDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    IdPlantillaMaestroPw,
	                    IdRevisionPw 
                    FROM pla.T_Plantilla_PW
                    WHERE Estado = 1 Order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Plantilla_PW WHERE Estado = 1";
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

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Plantilla para mostrarse en combo de modulo whatssapp marketing.
        /// </summary>R
        /// <returns> List<PlantillaPwComboWhatsappDTO> </returns>
        public IEnumerable<PlantillaPwComboWhatsappDTO> ObtenerComboWhatsapp()
        {
            try
            {
                List<PlantillaPwComboWhatsappDTO> rpta = new List<PlantillaPwComboWhatsappDTO>();
                var query = @"SELECT * FROM mkt.V_TPlantilla_Nombre_WhatsApp";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaPwComboWhatsappDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registros asociado al Id.
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> PlantillaPw </returns>
        public PlantillaPw ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Descripcion,
                                   IdPlantillaMaestroPw,
                                   IdRevisionPw,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_Plantilla_PW
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PlantillaPw>(resultado);
                }
                return new PlantillaPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
