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
    /// Repositorio: ProbabilidadRegistroPwRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProbabilidadRegistro_Pw
    /// </summary>
    public class ProbabilidadRegistroPwRepository : GenericRepository<TProbabilidadRegistroPw>, IProbabilidadRegistroPwRepository
    {
        private Mapper _mapper;

        public ProbabilidadRegistroPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProbabilidadRegistroPw, ProbabilidadRegistroPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProbabilidadRegistroPw MapeoEntidad(ProbabilidadRegistroPw entidad)
        {
            try
            {
                //crea la entidad padre
                TProbabilidadRegistroPw modelo = _mapper.Map<TProbabilidadRegistroPw>(entidad);

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

        public TProbabilidadRegistroPw Add(ProbabilidadRegistroPw entidad)
        {
            try
            {
                var ProbabilidadRegistroPw = MapeoEntidad(entidad);
                base.Insert(ProbabilidadRegistroPw);
                return ProbabilidadRegistroPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProbabilidadRegistroPw Update(ProbabilidadRegistroPw entidad)
        {
            try
            {
                var ProbabilidadRegistroPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProbabilidadRegistroPw.RowVersion = entidadExistente.RowVersion;

                base.Update(ProbabilidadRegistroPw);
                return ProbabilidadRegistroPw;
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


        public IEnumerable<TProbabilidadRegistroPw> Add(IEnumerable<ProbabilidadRegistroPw> listadoEntidad)
        {
            try
            {
                List<TProbabilidadRegistroPw> listado = new List<TProbabilidadRegistroPw>();
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

        public IEnumerable<TProbabilidadRegistroPw> Update(IEnumerable<ProbabilidadRegistroPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProbabilidadRegistroPw> listado = new List<TProbabilidadRegistroPw>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProbabilidadRegistro_Pw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT Id, Nombre FROM mkt.T_ProbabilidadRegistro_Pw WHERE Estado=1";
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProbabilidadRegistro_Pw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_ProbabilidadRegistro_Pw WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ProbabilidadRegistro_Pw
        /// </summary>
        /// <returns> List<ProbabilidadRegistroPwDTO> </returns>
        public IEnumerable<ProbabilidadRegistroPwDTO> ObtenerProbabilidadRegistroPw()
        {
            try
            {
                List<ProbabilidadRegistroPwDTO> rpta = new List<ProbabilidadRegistroPwDTO>();
                var query = @"SELECT Id, Nombre, IdCodigo, Codigo, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion 
                            FROM mkt.T_ProbabilidadRegistro_PW
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProbabilidadRegistroPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/11/2022
        /// <summary>
        /// Obtiene información para combo box
        /// </summary>
        /// <param></param>
        /// <returns> Lista de ObjetosDTO: List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                List<ComboDTO> probabilidadesRegistro = new List<ComboDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_TProbabilidadRegistro_ParaFiltro WHERE estado = 1";
                var probabilidadRegistroPwDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(probabilidadRegistroPwDB) && !probabilidadRegistroPwDB.Contains("[]"))
                {
                    probabilidadesRegistro = JsonConvert.DeserializeObject<List<ComboDTO>>(probabilidadRegistroPwDB);
                }
                return probabilidadesRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de T_ProbabilidadRegistro_Pw por el Id
        /// </summary>
        /// <param name="id">Id de T_ProbabilidadRegistro_Pw </param>
        /// <returns> ProbabilidadRegistroPwDTO </returns>
        public ProbabilidadRegistroPwDTO ObtenerPorId(int id)
        {
            try
            {
                var probabilidadRegistroPw = new ProbabilidadRegistroPwDTO();

                var query = @"SELECT Id, Nombre, IdCodigo, Codigo, Estado,
                               UsuarioCreacion, UsuarioModificacion, FechaCreacion,
                               FechaModificacion, RowVersion, IdMigracion 
                            FROM mkt.T_ProbabilidadRegistro_PW 
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    probabilidadRegistroPw = JsonConvert.DeserializeObject<ProbabilidadRegistroPwDTO>(resultado);
                }
                return probabilidadRegistroPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
