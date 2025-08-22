using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CargoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_Cargo
    /// </summary>
    public class CargoRepository : GenericRepository<TCargo>, ICargoRepository
    {
        private Mapper _mapper;

        public CargoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCargo, Cargo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCargo MapeoEntidad(Cargo entidad)
        {
            try
            {
                //crea la entidad padre
                TCargo modelo = _mapper.Map<TCargo>(entidad);

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

        public TCargo Add(Cargo entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                base.Insert(Cargo);
                return Cargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCargo Update(Cargo entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Cargo.RowVersion = entidadExistente.RowVersion;

                base.Update(Cargo);
                return Cargo;
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


        public IEnumerable<TCargo> Add(IEnumerable<Cargo> listadoEntidad)
        {
            try
            {
                List<TCargo> listado = new List<TCargo>();
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

        public IEnumerable<TCargo> Update(IEnumerable<Cargo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCargo> listado = new List<TCargo>();
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
        /// Autor: Klebert Layme
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Cargo.
        /// </summary>
        /// <returns> List<CargoDTO> </returns>
        public IEnumerable<Cargo> ObtenerTodo()
        {
            try
            {
                List<Cargo> rpta = new();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    Descripcion,
	                    Orden,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
                        RowVersion,
                        IdMigracion
                    FROM pla.T_Cargo
                    WHERE Estado = 1 ORDER BY FechaCreacion DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Cargo>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 26/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de CargoDTO
        /// </summary>
        /// <returns> List<CargoDTO> </returns>
        public Cargo ObtenerPorId(int id)
        {
            try
            {
                Cargo rpta = new();
                var query = @"
                        SELECT 
                Id,
                Nombre,
                Descripcion,
                Orden,
                UsuarioCreacion,
                UsuarioModificacion,
                FechaCreacion,
                FechaModificacion,
                RowVersion,
                IdMigracion
                    FROM pla.T_Cargo
                        WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Cargo>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Klebert Layme.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Cargo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_Cargo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,Nombre FROM pla.T_Cargo WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ComboDTO> ObtenerCargoFiltro()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                        SELECT Id, Nombre FROM pla.V_TCargo_ObtenerIdNombre WHERE  Estado = 1";
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
    }
}
