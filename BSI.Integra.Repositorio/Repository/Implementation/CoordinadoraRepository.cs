using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CoordinadoraRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 18/08/2023
    /// <summary>
    /// Gestión general de T_Coordinadora
    /// </summary>
    public class CoordinadoraRepository : GenericRepository<TCoordinadora>, ICoordinadoraRepository
    {
        private Mapper _mapper;

        public CoordinadoraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCoordinadora, Coordinadora>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCoordinadora MapeoEntidad(Coordinadora entidad)
        {
            try
            {
                //crea la entidad padre
                TCoordinadora modelo = _mapper.Map<TCoordinadora>(entidad);

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

        public TCoordinadora Add(Coordinadora entidad)
        {
            try
            {
                var Coordinadora = MapeoEntidad(entidad);
                base.Insert(Coordinadora);
                return Coordinadora;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCoordinadora Update(Coordinadora entidad)
        {
            try
            {
                var Coordinadora = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Coordinadora.RowVersion = entidadExistente.RowVersion;

                base.Update(Coordinadora);
                return Coordinadora;
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


        public IEnumerable<TCoordinadora> Add(IEnumerable<Coordinadora> listadoEntidad)
        {
            try
            {
                List<TCoordinadora> listado = new List<TCoordinadora>();
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

        public IEnumerable<TCoordinadora> Update(IEnumerable<Coordinadora> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCoordinadora> listado = new List<TCoordinadora>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro de T_Coordinadora por id
        /// </summary>
        /// <returns> Coordinadora </returns>
        public Coordinadora? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    AliasCorreo,
	                    Clave,
	                    Firma,
	                    Usuario,
	                    Anexo,
	                    Modalidad,
	                    Genero,
	                    IdSede,
	                    HtmlNumero,
	                    HtmlHorario,
	                    Iniciales,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM ope.T_Coordinadora 
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Coordinadora>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CR-OPI-001@Error en ObtenerPorId, {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de coordinadoras para combo
        /// </summary>
        /// <returns> Lista ComboDTO </returns>
        public IEnumerable<ComboDTO> ObtenerCoordinadoresDocentes()
        {
            try
            {
                var query = @"
                    SELECT Id, Nombre 
                    FROM pla.V_Coordinador_NombreCompleto
                    WHERE Estado = 1 AND Id IN (17,4215,4108,4661)";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CR-O-001@Error en ObtenerCoordinadoresDocentes, {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de coordinadoras para combo
        /// </summary>
        /// <returns> Lista ComboDTO </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerCoordinadoresDocentesAsync()
        {
            try
            {
                var query = @"
                    SELECT Id, Nombre 
                    FROM pla.V_Coordinador_NombreCompleto
                    WHERE Estado = 1 AND Id IN (17,4215,4108,4661)";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CR-O-001@Error en ObtenerCoordinadoresDocentes, {ex.Message}");
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 06/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de coordinadoras para combo
        /// </summary>
        public bool ExistePorNombreUsuario(string usuario)
        {
            try
            {
                var query = @"SELECT Id, Usuario FROM ope.T_Coordinadora WHERE Usuario = @usuario";
                var res = _dapperRepository.FirstOrDefault(query, new { usuario });
                if (!string.IsNullOrEmpty(res) && res != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
