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
    public class ActividadCabeceraDiaSemanaRepository : GenericRepository<TActividadCabeceraDiaSemana>, IActividadCabeceraDiaSemanaRepository
    {
        private Mapper _mapper;

        public ActividadCabeceraDiaSemanaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadCabeceraDiaSemana, ActividadCabeceraDiaSemana>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TActividadCabeceraDiaSemana MapeoEntidad(ActividadCabeceraDiaSemana entidad)
        {
            try
            {
                TActividadCabeceraDiaSemana modelo = _mapper.Map<TActividadCabeceraDiaSemana>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadCabeceraDiaSemana Add(ActividadCabeceraDiaSemana entidad)
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

        public bool Insert(IEnumerable<TActividadCabeceraDiaSemana> listadoBO)
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

        public ActividadCabeceraDiaSemana FirstById(int id)
        {
            try
            {

                TActividadCabeceraDiaSemana entidad = base.FirstById(id);
                ActividadCabeceraDiaSemana objetoBO = new ActividadCabeceraDiaSemana();
                _mapper.Map<ConfiguracionEnvioMailingDetalle>(entidad);

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TActividadCabeceraDiaSemana Update(ActividadCabeceraDiaSemana entidad)
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
        public TActividadCabeceraDiaSemana Update(TActividadCabeceraDiaSemana entidad)
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


        public IEnumerable<TActividadCabeceraDiaSemana> Add(IEnumerable<ActividadCabeceraDiaSemana> listadoEntidad)
        {
            try
            {
                List<TActividadCabeceraDiaSemana> listado = new List<TActividadCabeceraDiaSemana>();
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

        public IEnumerable<TActividadCabeceraDiaSemana> Update(IEnumerable<ActividadCabeceraDiaSemana> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TActividadCabeceraDiaSemana> listado = new List<TActividadCabeceraDiaSemana>();
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



        public IEnumerable<TActividadCabeceraDiaSemana> GetBy(Expression<Func<TActividadCabeceraDiaSemana, bool>> filter)
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

        #endregion
        public bool Insert(ActividadCabeceraDiaSemana objetoBO)
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

        private void AsignacionId(TActividadCabeceraDiaSemana entidad, ActividadCabeceraDiaSemana objetoBO)
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


        public List<ActividadCabeceraDiaSemana> ObtenerListaEstadosValidacionNumeroWhatsApp()
        {
            try
            {
                List<ActividadCabeceraDiaSemana> TipoPersona = new List<ActividadCabeceraDiaSemana>();
                string QueryEstadoValidacion = string.Empty;
                QueryEstadoValidacion = "SELECT Id, Nombre FROM mkt.V_TActividadCabeceraDiaSemana_ObtenerParaFiltro";
                var QueryLista = _dapperRepository.QueryDapper(QueryEstadoValidacion, null);
                if (!string.IsNullOrEmpty(QueryLista) && !QueryLista.Contains("[]"))
                {
                    TipoPersona = JsonConvert.DeserializeObject<List<ActividadCabeceraDiaSemana>>(QueryLista);
                }
                return TipoPersona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<ActividadCabeceraDiaSemana> ObtenerActividadDiaPorID(int idActividadCabecera)
        {
            try
            {
                List<ActividadCabeceraDiaSemana> plantilla = new List<ActividadCabeceraDiaSemana>();
                var query = string.Empty;
                query = @"	SELECT Id, IdActividadCabecera, IdDiaSemana FROM mkt.T_ActividadCabeceraDiaSemana WHERE Estado = 1 and idActividadCabecera  = @idActividadCabecera;";
                var resp = _dapperRepository.QueryDapper(query, new { idActividadCabecera = idActividadCabecera });
                if (!string.IsNullOrEmpty(resp) && !resp.Contains("[]") && resp != "null")
                {
                    plantilla = JsonConvert.DeserializeObject<List<ActividadCabeceraDiaSemana>>(resp)!;
                }
                return plantilla;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
