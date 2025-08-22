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
    public class WhatsAppEstadoValidacionRepository : GenericRepository<TWhatsAppEstadoValidacion>, IWhatsAppEstadoValidacionRepository
    {
        private Mapper _mapper;

        public WhatsAppEstadoValidacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppEstadoValidacion, WhatsAppEstadoValidacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TWhatsAppEstadoValidacion MapeoEntidad(WhatsAppEstadoValidacionDTO entidad)
        {
            try
            {
                TWhatsAppEstadoValidacion modelo = _mapper.Map<TWhatsAppEstadoValidacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppEstadoValidacion Add(WhatsAppEstadoValidacionDTO entidad)
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

        public bool Insert(IEnumerable<TWhatsAppEstadoValidacion> listadoBO)
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


        public TWhatsAppEstadoValidacion Update(WhatsAppEstadoValidacionDTO entidad)
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
        public TWhatsAppEstadoValidacion Update(TWhatsAppEstadoValidacion entidad)
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


        public IEnumerable<TWhatsAppEstadoValidacion> Add(IEnumerable<WhatsAppEstadoValidacionDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppEstadoValidacion> listado = new List<TWhatsAppEstadoValidacion>();
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

        public IEnumerable<TWhatsAppEstadoValidacion> Update(IEnumerable<WhatsAppEstadoValidacionDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppEstadoValidacion> listado = new List<TWhatsAppEstadoValidacion>();
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

        public IEnumerable<TWhatsAppEstadoValidacion> GetBy(Expression<Func<TWhatsAppEstadoValidacion, bool>> filter)
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


        public List<WhatsAppEstadoValidacionDTO> ObtenerListaEstadosValidacionNumeroWhatsApp()
        {
            try
            {
                List<WhatsAppEstadoValidacionDTO> TipoPersona = new List<WhatsAppEstadoValidacionDTO>();
                string QueryEstadoValidacion = string.Empty;
                QueryEstadoValidacion = "SELECT Id, Nombre FROM mkt.V_TWhatsAppEstadoValidacion_ObtenerParaFiltro";
                var QueryLista = _dapperRepository.QueryDapper(QueryEstadoValidacion, null);
                if (!string.IsNullOrEmpty(QueryLista) && !QueryLista.Contains("[]"))
                {
                    TipoPersona = JsonConvert.DeserializeObject<List<WhatsAppEstadoValidacionDTO>>(QueryLista);
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
