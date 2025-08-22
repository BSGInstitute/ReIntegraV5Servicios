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
    public class PuestoTrabajoFuncionRepository : GenericRepository<TPuestoTrabajoFuncion>, IPuestoTrabajoFuncionRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoFuncionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoFuncion, PuestoTrabajoFuncion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPuestoTrabajoFuncion MapeoEntidad(PuestoTrabajoFuncion entidad)
        {
            try
            {
                TPuestoTrabajoFuncion modelo = _mapper.Map<TPuestoTrabajoFuncion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoFuncion Add(PuestoTrabajoFuncion entidad)
        {
            try
            {
                var PuestoTrabajoFuncion = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoFuncion);
                return PuestoTrabajoFuncion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajoFuncion Update(PuestoTrabajoFuncion entidad)
        {
            try
            {
                var PuestoTrabajoFuncion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoFuncion.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoFuncion);
                return PuestoTrabajoFuncion;
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
        public IEnumerable<TPuestoTrabajoFuncion> Add(IEnumerable<PuestoTrabajoFuncion> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoFuncion> listado = new List<TPuestoTrabajoFuncion>();
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
        public IEnumerable<TPuestoTrabajoFuncion> Update(IEnumerable<PuestoTrabajoFuncion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoFuncion> listado = new List<TPuestoTrabajoFuncion>();
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

        public List<PuestoTrabajoFuncionDTO> ObtenerPuestoTrabajoFuncion(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoFuncionDTO> lista = new List<PuestoTrabajoFuncionDTO>();
                var query = "SELECT Id, IdPerfilPuestoTrabajo, NroOrden, Funcion, IdPersonalTipoFuncion, PersonalTipoFuncion, IdFrecuenciaPuestoTrabajo, FrecuenciaPuestoTrabajo FROM [gp].[V_TPuestoTrabajoFuncion_ObtenerPuestoTrabajoFuncion] WHERE Estado = 1  AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoFuncionDTO>>(res);
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
