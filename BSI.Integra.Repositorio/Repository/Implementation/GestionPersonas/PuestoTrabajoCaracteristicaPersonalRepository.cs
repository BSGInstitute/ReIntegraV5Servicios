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
    public class PuestoTrabajoCaracteristicaPersonalRepository : GenericRepository<TPuestoTrabajoCaracteristicaPersonal>, IPuestoTrabajoCaracteristicaPersonalRepository
    {
        private Mapper _mapper;

        public PuestoTrabajoCaracteristicaPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoCaracteristicaPersonal, PuestoTrabajoCaracteristicaPersonal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoCaracteristicaPersonal MapeoEntidad(PuestoTrabajoCaracteristicaPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoCaracteristicaPersonal modelo = _mapper.Map<TPuestoTrabajoCaracteristicaPersonal>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoCaracteristicaPersonal Add(PuestoTrabajoCaracteristicaPersonal entidad)
        {
            try
            {
                var PuestoTrabajoCaracteristicaPersonal = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoCaracteristicaPersonal);
                return PuestoTrabajoCaracteristicaPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoCaracteristicaPersonal Update(PuestoTrabajoCaracteristicaPersonal entidad)
        {
            try
            {
                var PuestoTrabajoCaracteristicaPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoCaracteristicaPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoCaracteristicaPersonal);
                return PuestoTrabajoCaracteristicaPersonal;
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


        public IEnumerable<TPuestoTrabajoCaracteristicaPersonal> Add(IEnumerable<PuestoTrabajoCaracteristicaPersonal> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoCaracteristicaPersonal> listado = new List<TPuestoTrabajoCaracteristicaPersonal>();
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

        public IEnumerable<TPuestoTrabajoCaracteristicaPersonal> Update(IEnumerable<PuestoTrabajoCaracteristicaPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoCaracteristicaPersonal> listado = new List<TPuestoTrabajoCaracteristicaPersonal>();
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


        public List<PuestoTrabajoCaracteristicaPersonalDTO> ObtenerPuestoTrabajoCaracteristicaPersonal(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoCaracteristicaPersonalDTO> lista = new List<PuestoTrabajoCaracteristicaPersonalDTO>();
                var _query = "SELECT Id, IdPerfilPuestoTrabajo, EdadMinima, EdadMaxima, IdSexo, IdEstadoCivil, Sexo, EstadoCivil FROM [gp].[V_TPuestoTrabajoCaracteristicaPersonal_ObtenerListaCaracteristicaPersonal] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoCaracteristicaPersonalDTO>>(res);
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
