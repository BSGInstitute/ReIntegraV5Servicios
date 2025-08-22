using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class SeguimientoPreProcesoListaWhatsAppRepository : GenericRepository<TSeguimientoPreProcesoListaWhatsApp>, ISeguimientoPreProcesoListaWhatsAppRepository
    {
        private Mapper _mapper;

        public SeguimientoPreProcesoListaWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeguimientoPreProcesoListaWhatsApp, SeguimientoPreProcesoListaWhatsApp>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSeguimientoPreProcesoListaWhatsApp MapeoEntidad(SeguimientoPreProcesoListaWhatsApp entidad)
        {
            try
            {
                TSeguimientoPreProcesoListaWhatsApp modelo = _mapper.Map<TSeguimientoPreProcesoListaWhatsApp>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSeguimientoPreProcesoListaWhatsApp Add(SeguimientoPreProcesoListaWhatsApp entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                base.Insert(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(IEnumerable<TSeguimientoPreProcesoListaWhatsApp> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SeguimientoPreProcesoListaWhatsApp FirstById(int id)
        {
            try
            {

                TSeguimientoPreProcesoListaWhatsApp entidad = base.FirstById(id);
                SeguimientoPreProcesoListaWhatsApp objetoBO = new SeguimientoPreProcesoListaWhatsApp();
                _mapper.Map<ConfiguracionEnvioMailingDetalle>(entidad);

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TSeguimientoPreProcesoListaWhatsApp Update(SeguimientoPreProcesoListaWhatsApp entidad)
        {
            try
            {
                var PgeneralConfiguracionPlantilla = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralConfiguracionPlantilla.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralConfiguracionPlantilla);
                return PgeneralConfiguracionPlantilla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSeguimientoPreProcesoListaWhatsApp Update(TSeguimientoPreProcesoListaWhatsApp entidad)
        {
            try
            {
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                entidad.RowVersion = entidadExistente.RowVersion;

                base.Update(entidad);
                return entidad;
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


        public IEnumerable<TSeguimientoPreProcesoListaWhatsApp> Add(IEnumerable<SeguimientoPreProcesoListaWhatsApp> listadoEntidad)
        {
            try
            {
                List<TSeguimientoPreProcesoListaWhatsApp> listado = new List<TSeguimientoPreProcesoListaWhatsApp>();
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

        public IEnumerable<TSeguimientoPreProcesoListaWhatsApp> Update(IEnumerable<SeguimientoPreProcesoListaWhatsApp> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeguimientoPreProcesoListaWhatsApp> listado = new List<TSeguimientoPreProcesoListaWhatsApp>();
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

        public IEnumerable<TSeguimientoPreProcesoListaWhatsApp> GetBy(Expression<Func<TSeguimientoPreProcesoListaWhatsApp, bool>> filter)
        {
            try
            {
                return base.GetBy(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(SeguimientoPreProcesoListaWhatsApp objetoBO)
        {
            try
            {
                //mapeo de la entidad
                var entidad = MapeoEntidad(objetoBO);

                var resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void AsignacionId(TSeguimientoPreProcesoListaWhatsApp entidad, SeguimientoPreProcesoListaWhatsApp objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<SeguimientoPreProcesoListaWhatsApp> ObtenerListaEstadosValidacionNumeroWhatsApp()
        {
            try
            {
                List<SeguimientoPreProcesoListaWhatsApp> TipoPersona = new List<SeguimientoPreProcesoListaWhatsApp>();
                string QueryEstadoValidacion = string.Empty;
                QueryEstadoValidacion = "SELECT Id, Nombre FROM mkt.V_TSeguimientoPreProcesoListaWhatsApp_ObtenerParaFiltro";
                var QueryLista = _dapperRepository.QueryDapper(QueryEstadoValidacion, null);
                if (!string.IsNullOrEmpty(QueryLista) && !QueryLista.Contains("[]"))
                {
                    TipoPersona = JsonConvert.DeserializeObject<List<SeguimientoPreProcesoListaWhatsApp>>(QueryLista);
                }
                return TipoPersona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
