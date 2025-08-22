using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
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
    public class PuestoTrabajoFormacionAcademicaRepository : GenericRepository<TPuestoTrabajoFormacionAcademica>, IPuestoTrabajoFormacionAcademicaRepository
    {
        private Mapper _mapper;

        public PuestoTrabajoFormacionAcademicaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoFormacionAcademica, PuestoTrabajoFormacionAcademica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoFormacionAcademica MapeoEntidad(PuestoTrabajoFormacionAcademica entidad)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoFormacionAcademica modelo = _mapper.Map<TPuestoTrabajoFormacionAcademica>(entidad);

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

        public TPuestoTrabajoFormacionAcademica Add(PuestoTrabajoFormacionAcademica entidad)
        {
            try
            {
                var PuestoTrabajoFormacionAcademica = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoFormacionAcademica);
                return PuestoTrabajoFormacionAcademica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoFormacionAcademica Update(PuestoTrabajoFormacionAcademica entidad)
        {
            try
            {
                var PuestoTrabajoFormacionAcademica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoFormacionAcademica.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoFormacionAcademica);
                return PuestoTrabajoFormacionAcademica;
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


        public IEnumerable<TPuestoTrabajoFormacionAcademica> Add(IEnumerable<PuestoTrabajoFormacionAcademica> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoFormacionAcademica> listado = new List<TPuestoTrabajoFormacionAcademica>();
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

        public IEnumerable<TPuestoTrabajoFormacionAcademica> Update(IEnumerable<PuestoTrabajoFormacionAcademica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoFormacionAcademica> listado = new List<TPuestoTrabajoFormacionAcademica>();
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


        public List<PuestoTrabajoFormacionAcademicaFiltroDTO> ObtenerPuestoTrabajoFormacionAcademica(int? idPerfilPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoFormacionAcademicaFiltroDTO> lista = new List<PuestoTrabajoFormacionAcademicaFiltroDTO>();
                var _query = "SELECT Id, IdPerfilPuestoTrabajo, IdTipoFormacion, IdNivelEstudio, IdAreaFormacion, IdCentroEstudio, IdGradoEstudio FROM [gp].[V_TPuestoTrabajoFormacionAcademica_ObtenerListaFormacionAcademica] WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @IdPerfilPuestoTrabajo";
                var res = _dapperRepository.QueryDapper(_query, new { IdPerfilPuestoTrabajo = idPerfilPuestoTrabajo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    lista = JsonConvert.DeserializeObject<List<PuestoTrabajoFormacionAcademicaFiltroDTO>>(res);
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
