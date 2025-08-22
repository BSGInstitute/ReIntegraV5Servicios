using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReporteTipoDeCambioFinancieroMensualRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ReporteTipoDeCambioFinancieroMensual
    /// </summary>
    public class ReporteTipoDeCambioFinancieroMensualRepository : GenericRepository<TReporteTipoDeCambioFinancieroMensual>, IReporteTipoDeCambioFinancieroMensualRepository
    {
        private Mapper _mapper;

        public ReporteTipoDeCambioFinancieroMensualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReporteTipoDeCambioFinancieroMensual, ReporteTipoDeCambioFinancieroMensual>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public bool Detached(ReporteTipoDeCambioFinancieroMensual entidad)
        {
            try
            {
                var ReporteTipoDeCambioFinancieroMensual = MapeoEntidad(entidad);
                return base.SetState(ReporteTipoDeCambioFinancieroMensual, EntityState.Detached);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Metodos Base
        private TReporteTipoDeCambioFinancieroMensual MapeoEntidad(ReporteTipoDeCambioFinancieroMensual entidad)
        {
            try
            {
 
                TReporteTipoDeCambioFinancieroMensual modelo = _mapper.Map<TReporteTipoDeCambioFinancieroMensual>(entidad);

 

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReporteTipoDeCambioFinancieroMensual Add(ReporteTipoDeCambioFinancieroMensual entidad)
        {
            try
            {
                var ReporteTipoDeCambioFinancieroMensual = MapeoEntidad(entidad);
                base.Insert(ReporteTipoDeCambioFinancieroMensual);
                return ReporteTipoDeCambioFinancieroMensual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReporteTipoDeCambioFinancieroMensual Update(ReporteTipoDeCambioFinancieroMensual entidad)
        {
            try
            {
                var ReporteTipoDeCambioFinancieroMensual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ReporteTipoDeCambioFinancieroMensual.RowVersion = entidadExistente.RowVersion;

                base.Update(ReporteTipoDeCambioFinancieroMensual);
                return ReporteTipoDeCambioFinancieroMensual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TReporteTipoDeCambioFinancieroMensual UpdateAlterno(ReporteTipoDeCambioFinancieroMensual entidad)
        {
            try
            {
                var ReporteTipoDeCambioFinancieroMensual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ReporteTipoDeCambioFinancieroMensual.RowVersion = entidadExistente.RowVersion;

                base.UpdateAlterno(ReporteTipoDeCambioFinancieroMensual);
                return ReporteTipoDeCambioFinancieroMensual;
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


        public IEnumerable<TReporteTipoDeCambioFinancieroMensual> Add(IEnumerable<ReporteTipoDeCambioFinancieroMensual> listadoEntidad)
        {
            try
            {
                List<TReporteTipoDeCambioFinancieroMensual> listado = new List<TReporteTipoDeCambioFinancieroMensual>();
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

        public IEnumerable<TReporteTipoDeCambioFinancieroMensual> Update(IEnumerable<ReporteTipoDeCambioFinancieroMensual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TReporteTipoDeCambioFinancieroMensual> listado = new List<TReporteTipoDeCambioFinancieroMensual>();
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

        public List<ReporteTipoDeCambioFinancieroMensualEnvioDTO> ObtenerTipoPagoPorAnioYMes(ReporteTipoDeCambioFinanzcieroMensualGrillaDTO entidad)
        {
            try
            {
                var rpta = new ActividadDetalle();
                var query = @" ";
                var resultado = _dapperRepository.QuerySPDapper("fin.SP_ReporteTipoCambioMensualFiltro", new { entidad.IdMoneda,  entidad.Anio, entidad.Mes });
                var lista = new List<ReporteTipoDeCambioFinancieroMensualEnvioDTO>();
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    lista = JsonConvert.DeserializeObject<List<ReporteTipoDeCambioFinancieroMensualEnvioDTO>>(resultado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO> ObtenerTipoCambioTotal()
        {
            try
            {
                List<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO> rpta = new List<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO>();
                var query = @"SELECT idMoneda, Mes, Anio, Id
                            FROM fin.T_ReporteTipoDeCambioFinancieroMensual
                            WHERE Estado=1 order by fechaCreacion desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO>>(resultado);
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
