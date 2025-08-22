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
    public class PuestoTrabajoExperienciaRepository : GenericRepository<TPuestoTrabajoExperiencium>, IPuestoTrabajoExperienciaRepository
    {
        private Mapper _mapper;

        public PuestoTrabajoExperienciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoExperiencium, PuestoTrabajoExperiencia>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoExperiencium MapeoEntidad(PuestoTrabajoExperiencia entidad)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoExperiencium modelo = _mapper.Map<TPuestoTrabajoExperiencium>(entidad);

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

        public TPuestoTrabajoExperiencium Add(PuestoTrabajoExperiencia entidad)
        {
            try
            {
                var PuestoTrabajoExperiencia = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoExperiencia);
                return PuestoTrabajoExperiencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoExperiencium Update(PuestoTrabajoExperiencia entidad)
        {
            try
            {
                var PuestoTrabajoExperiencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoExperiencia.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoExperiencia);
                return PuestoTrabajoExperiencia;
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


        public IEnumerable<TPuestoTrabajoExperiencium> Add(IEnumerable<PuestoTrabajoExperiencia> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoExperiencium> listado = new List<TPuestoTrabajoExperiencium>();
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

        public IEnumerable<TPuestoTrabajoExperiencium> Update(IEnumerable<PuestoTrabajoExperiencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoExperiencium> listado = new List<TPuestoTrabajoExperiencium>();
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

        public List<PuestoTrabajoExperienciaPuestoTrabajo> ObtenerPuestoTrabajoExperiencia(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoExperienciaPuestoTrabajo> lista = new List<PuestoTrabajoExperienciaPuestoTrabajo>();
                var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdExperiencia, IdTipoExperiencia, Experiencia, TipoExperiencia, NumeroMinimo, Periodo FROM [gp].[V_TPuestoTrabajoExperiencia_ObtenerListaExperiencia] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoExperienciaPuestoTrabajo>>(res);
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
