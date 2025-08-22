using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ArticuloSeoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 01/12/2022
    /// <summary>
    /// Gestión general de T_Error
    /// </summary>
    public class ErrorRepository : GenericRepository<TError>, IErrorRepository
    {
        private Mapper _mapper;

        public ErrorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TError, Error>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base

        private TError MapeoEntidad(Error entidad)
        {
            try
            {
                //crea la entidad padre
                TError modelo = _mapper.Map<TError>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TError Add(Error entidad)
        {
            try
            {
                var Error = MapeoEntidad(entidad);
                base.Insert(Error);
                return Error;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TError Update(Error entidad)
        {
            try
            {
                var Error = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Error.RowVersion = entidadExistente.RowVersion;

                base.Update(Error);
                return Error;
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


        public IEnumerable<TError> Add(IEnumerable<Error> listadoEntidad)
        {
            try
            {
                List<TError> listado = new List<TError>();
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

        public IEnumerable<TError> Update(IEnumerable<Error> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TError> listado = new List<TError>();
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
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el error del sistema
        /// </summary>
        /// <returns></returns>
        public List<ErrorDTO> ObtenerTodosErroresSistema()
        {
            List<ErrorDTO> errorDTO = new List<ErrorDTO>();
            var query = string.Empty;
            query = "SELECT Codigo, IdErrorTipo, Descripcion, Estado, DescripcionPersonalizada FROM conf.V_TError_StartupDatos WHERE Estado = 1";
            var errores = _dapperRepository.QueryDapper(query, null);
            errorDTO = JsonConvert.DeserializeObject<List<ErrorDTO>>(errores)!;
            return errorDTO;
        }
    }
}
