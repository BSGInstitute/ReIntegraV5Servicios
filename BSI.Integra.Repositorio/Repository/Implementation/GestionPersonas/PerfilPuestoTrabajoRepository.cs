using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    public class PerfilPuestoTrabajoRepository : GenericRepository<TPerfilPuestoTrabajo>, IPerfilPuestoTrabajoRepository
    {
        private Mapper _mapper;
        public PerfilPuestoTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilPuestoTrabajo, PerfilPuestoTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPerfilPuestoTrabajo MapeoEntidad(PerfilPuestoTrabajo entidad)
        {
            try
            {
                TPerfilPuestoTrabajo modelo = _mapper.Map<TPerfilPuestoTrabajo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPerfilPuestoTrabajo Add(PerfilPuestoTrabajo entidad)
        {
            try
            {
                var PerfilPuestoTrabajo = MapeoEntidad(entidad);
                base.Insert(PerfilPuestoTrabajo);
                return PerfilPuestoTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPerfilPuestoTrabajo Update(PerfilPuestoTrabajo entidad)
        {
            try
            {
                var PerfilPuestoTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PerfilPuestoTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(PerfilPuestoTrabajo);
                return PerfilPuestoTrabajo;
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
        public IEnumerable<TPerfilPuestoTrabajo> Add(IEnumerable<PerfilPuestoTrabajo> listadoEntidad)
        {
            try
            {
                List<TPerfilPuestoTrabajo> listado = new List<TPerfilPuestoTrabajo>();
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
        public IEnumerable<TPerfilPuestoTrabajo> Update(IEnumerable<PerfilPuestoTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPerfilPuestoTrabajo> listado = new List<TPerfilPuestoTrabajo>();
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


        public List<PuestoTrabajoRelacionDetalleDTO> ObtenerPuestoTrabajoRelacion(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoRelacionDetalleDTO> lista = new List<PuestoTrabajoRelacionDetalleDTO>();
                var query = "SELECT Id, IdPuestoTrabajoRelacionDetalle, IdPerfilPuestoTrabajo, IdPuestoTrabajo_Dependencia, IdPuestoTrabajo_PuestoACargo, IdPersonalAreaTrabajo, PuestoTrabajo_Dependencia, PuestoTrabajo_PuestoACargo, PersonalAreaTrabajo FROM [gp].[V_TPuestoTrabajoRelacion_ObtenerPuestoTrabajoRelacion] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoRelacionDetalleDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PuestoTrabajoVersionesDTO> ObtenerListaPerfilPuestoTrabajoHistorico(int? idPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoVersionesDTO> lista = new List<PuestoTrabajoVersionesDTO>();
                var query = "SELECT Id, IdPuestoTrabajo, PuestoTrabajo, Version, Objetivo, Descripcion, Personal_Solicitud, Personal_Aprobacion, FechaSolicitud, FechaAprobacion, IdPerfilPuestoTrabajoEstadoSolicitud, PerfilPuestoTrabajoEstadoSolicitud, Observacion, EsActual FROM [gp].[V_TPerfilPuestoTrabajo_ObtenerPerfilPuestoTrabajoHistorico] WHERE Estado = 1 AND IdPuestoTrabajo = @IdPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(query, new { IdPuestoTrabajo = idPuestoTrabajo });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoVersionesDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PerfilPuestoTrabajo ObtenerIdPuestoTrabajoANDIdPerfilPuestoTrabajoEstadoSolicitud(int idPuestoTrabajo, int idPerfilPuestoTrabajoEstadoSolicitud)
        {
            try
            {
                PerfilPuestoTrabajo EvaluacionGrupo = new PerfilPuestoTrabajo();
                var query = "SELECT Id,IdPuestoTrabajo,Descripcion,Objetivo,Version,EsActual,IdPersonal_Solicitud,FechaSolicitud,IdPersonal_Aprobacion,FechaAprobacion,Observacion,IdPerfilPuestoTrabajoEstadoSolicitud FROM gp.T_PerfilPuestoTrabajo WHERE Estado = 1 AND IdPuestoTrabajo = @idPuestoTrabajo AND  IdPerfilPuestoTrabajoEstadoSolicitud  = @idPerfilPuestoTrabajoEstadoSolicitud";
                var res = _dapperRepository.FirstOrDefault(query, new { idPuestoTrabajo, idPerfilPuestoTrabajoEstadoSolicitud });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<PerfilPuestoTrabajo>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PersonalAprobacionDTO> ObtenerPersonalAprobacionVersion(int idPersonal)
        {
            try
            {
                List<PersonalAprobacionDTO> lista = new List<PersonalAprobacionDTO>();
                var query = "SELECT IdPuestoTrabajo from gp.T_PerfilPuestoTrabajoPersonalAprobacion WHERE Estado = 1 AND IdPersonal  = @idPersonal";
                var res = _dapperRepository.QueryDapper(query, new { idPersonal = idPersonal });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PersonalAprobacionDTO>>(res);
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
