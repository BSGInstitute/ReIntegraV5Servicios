using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CronogramaOriginalesCongeladoPorDiumRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_CronogramaOriginalesCongeladoPorDium
    /// </summary>
    public class CronogramaOriginalesCongeladoPorDiumRepository : GenericRepository<TCronogramaOriginalesCongeladoPorDium>, ICronogramaOriginalesCongeladoPorDiumRepository
    {
        private Mapper _mapper;

        public CronogramaOriginalesCongeladoPorDiumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCronogramaOriginalesCongeladoPorDium, CronogramaOriginalesCongeladoPorDium>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCronogramaOriginalesCongeladoPorDium MapeoEntidad(CronogramaOriginalesCongeladoPorDium entidad)
        {
            try
            {
                //crea la entidad padre
                TCronogramaOriginalesCongeladoPorDium modelo = _mapper.Map<TCronogramaOriginalesCongeladoPorDium>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaOriginalesCongeladoPorDium Add(CronogramaOriginalesCongeladoPorDium entidad)
        {
            try
            {
                var CronogramaOriginalesCongeladoPorDium = MapeoEntidad(entidad);
                base.Insert(CronogramaOriginalesCongeladoPorDium);
                return CronogramaOriginalesCongeladoPorDium;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCronogramaOriginalesCongeladoPorDium Update(CronogramaOriginalesCongeladoPorDium entidad)
        {
            try
            {
                var CronogramaOriginalesCongeladoPorDium = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CronogramaOriginalesCongeladoPorDium.RowVersion = entidadExistente.RowVersion;

                base.Update(CronogramaOriginalesCongeladoPorDium);
                return CronogramaOriginalesCongeladoPorDium;
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


        public IEnumerable<TCronogramaOriginalesCongeladoPorDium> Add(IEnumerable<CronogramaOriginalesCongeladoPorDium> listadoEntidad)
        {
            try
            {
                List<TCronogramaOriginalesCongeladoPorDium> listado = new List<TCronogramaOriginalesCongeladoPorDium>();
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

        public IEnumerable<TCronogramaOriginalesCongeladoPorDium> Update(IEnumerable<CronogramaOriginalesCongeladoPorDium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCronogramaOriginalesCongeladoPorDium> listado = new List<TCronogramaOriginalesCongeladoPorDium>();
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

        public bool ActualizarCronogramaCongeladosOriginales(ActualizarCronogramaCongeladoOriginalesDTO datos, string usuario)
        {
            try
            {

                var _query = "[fin].[SP_ActualizarCronogramaCongelado]";
                var inHouseDB = _dapperRepository.QuerySPDapper(_query, new { datos.CoordinadoraAcademica, UsuarioModificacion = usuario, datos.FechaModificacion, datos.CodigoMatricula, datos.FechaCongelamiento, datos.NroCuota, datos.NroSubCuota });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
