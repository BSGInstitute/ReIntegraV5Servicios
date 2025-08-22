using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: NivelEstudioRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_NivelEstudio
    /// </summary>
    public class NivelEstudioRepository : GenericRepository<TNivelEstudio>, INivelEstudioRepository
    {
        private Mapper _mapper;

        public NivelEstudioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNivelEstudio, NivelEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<NivelEstudio, NivelEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<NivelEstudio, TNivelEstudio>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TNivelEstudio MapeoEntidad(NivelEstudio entidad)
        {
            try
            {
                //crea la entidad padre
                TNivelEstudio modelo = _mapper.Map<TNivelEstudio>(entidad);

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

        public TNivelEstudio Add(NivelEstudio entidad)
        {
            try
            {
                var NivelEstudio = MapeoEntidad(entidad);
                base.Insert(NivelEstudio);
                return NivelEstudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TNivelEstudio Update(NivelEstudio entidad)
        {
            try
            {
                var NivelEstudio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                NivelEstudio.RowVersion = entidadExistente.RowVersion;

                base.Update(NivelEstudio);
                return NivelEstudio;
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


        public IEnumerable<TNivelEstudio> Add(IEnumerable<NivelEstudio> listadoEntidad)
        {
            try
            {
                List<TNivelEstudio> listado = new List<TNivelEstudio>();
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

        public IEnumerable<TNivelEstudio> Update(IEnumerable<NivelEstudio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TNivelEstudio> listado = new List<TNivelEstudio>();
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
        
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NivelEstudio para mostrarse en combo.
        /// </summary>
        /// <returns> List<NivelEstudioComboDTO> </returns>
        public IEnumerable<NivelEstudioComboDTO> ObtenerCombo()
        {
            try
            {
                List<NivelEstudioComboDTO> rpta = new List<NivelEstudioComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT Id,Nombre,IdTipoFormacion
                    FROM gp.T_NivelEstudio
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<NivelEstudioComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 02/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NivelEstudio.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<NivelEstudioDTO> Obtener()
        {
            try
            {
                List<NivelEstudioDTO> rpta = new List<NivelEstudioDTO>();
                var query = @"
                    SELECT Id, Nombre, IdTipoFormacion, TipoFormacion FROM [gp].[V_TNivelEstudio_ObtenerListaNivelEstudio] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<NivelEstudioDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Marco Jose Villanueva Torres
        /// Fecha: 02/05/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>NivelEstudio || null</returns>
        public NivelEstudio? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        IdTipoFormacion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_NivelEstudio
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<NivelEstudio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

    }
}
