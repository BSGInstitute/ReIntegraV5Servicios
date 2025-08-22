using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class SedeRepository : GenericRepository<TSede>, ISedeRepository
    {
        private Mapper _mapper;
        public SedeRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSede, TSedeDTO>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSede MapeoEntidad(TSedeDTO entidad)
        {
            try
            {
                TSede modelo = _mapper.Map<TSede>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSede Add(TSedeDTO entidad)
        {
            try
            {
                var AsignacionAutomaticaError = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomaticaError);
                return AsignacionAutomaticaError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSede Update(TSedeDTO entidad)
        {
            try
            {
                var AsignacionAutomaticaError = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomaticaError.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomaticaError);
                return AsignacionAutomaticaError;
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


        public IEnumerable<TSede> Add(IEnumerable<TSedeDTO> listadoEntidad)
        {
            try
            {
                List<TSede> listado = new List<TSede>();
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

        public IEnumerable<TSede> Update(IEnumerable<TSedeDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSede> listado = new List<TSede>();
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
        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns>List<SedeFiltroCiudadDTO></returns>
        public List<SedeComboDTO> ObtenerComboListaSedes()
        {
            try
            {
                List<SedeComboDTO> obtenerTodoSedeCiudad = new List<SedeComboDTO>();
                var _query = "SELECT IdEmpresa,IdCiudad,IdPais, Nombre FROM [fin].[V_ObtenerComboSedes]";
                var obtenerTodoSedeCiudadDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeComboDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns>List<SedeFiltroCiudadDTO></returns>
        public List<SedeComboDTO> ObtenerComboPorNombreSede(string Sede)
        {
            try
            {
                List<SedeComboDTO> obtenerTodoSedeCiudad = new List<SedeComboDTO>();
                var _query = "SELECT IdEmpresa,IdCiudad,IdPais, Nombre FROM [fin].[V_ObtenerComboSedes] where Nombre like '%@Sede%'";
                var obtenerTodoSedeCiudadDB = _dapperRepository.QueryDapper(_query, new { Sede });
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeComboDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns>List<SedeFiltroCiudadDTO></returns>
        public List<SedeFiltroCiudadDTO> ObtenerListaSedesSegunFur()
        {
            try
            {
                List<SedeFiltroCiudadDTO> obtenerTodoSedeCiudad = new List<SedeFiltroCiudadDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_ObtenerSedesSegunFurs";
                var obtenerTodoSedeCiudadDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeFiltroCiudadDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la lIsta de Sedes segun los fur, y que esten asociados a un comprobante con detraccion (utilizado para llenar combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaSedesConComprobanteDetraccion()
        {
            try
            {
                List<FiltroDTO> obtenerTodoSedeCiudad = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_PaisComprobanteDetraccion";
                var obtenerTodoSedeCiudadDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<FiltroDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lIsta de Sedes segun los fur, y que esten asociados a un comprobante con detraccion (utilizado para llenar combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaSedesSegunTabla()
        {
            try
            {
                List<FiltroDTO> obtenerTodoSedeCiudad = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_PaisComprobanteDetraccion";
                var obtenerTodoSedeCiudadDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<FiltroDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
