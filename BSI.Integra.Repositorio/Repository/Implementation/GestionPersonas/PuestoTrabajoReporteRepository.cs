using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoReporteRepository : GenericRepository<TPuestoTrabajoReporte>, IPuestoTrabajoReporteRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoReporteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoReporte, PuestoTrabajoReporte>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoReporte MapeoEntidad(PuestoTrabajoReporte entidad)
        {
            try
            {
                TPuestoTrabajoReporte modelo = _mapper.Map<TPuestoTrabajoReporte>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoReporte Add(PuestoTrabajoReporte entidad)
        {
            try
            {
                var PuestoTrabajoReporte = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoReporte);
                return PuestoTrabajoReporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoReporte Update(PuestoTrabajoReporte entidad)
        {
            try
            {
                var PuestoTrabajoReporte = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoReporte.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoReporte);
                return PuestoTrabajoReporte;
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
        public IEnumerable<TPuestoTrabajoReporte> Add(IEnumerable<PuestoTrabajoReporte> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoReporte> listado = new List<TPuestoTrabajoReporte>();
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
        public IEnumerable<TPuestoTrabajoReporte> Update(IEnumerable<PuestoTrabajoReporte> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoReporte> listado = new List<TPuestoTrabajoReporte>();
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


        public List<PuestoTrabajoReporteDTO> ObtenerPuestoTrabajoReporte(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoReporteDTO> lista = new List<PuestoTrabajoReporteDTO>();
                var _query = "SELECT Id, IdPerfilPuestoTrabajo, NroOrden, Reporte, IdFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajo FROM [gp].[V_TPuestoTrabajoReporte_ObtenerPuestoTrabajoReporte] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoReporteDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
