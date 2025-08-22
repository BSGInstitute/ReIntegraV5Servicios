using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: RevisionNivelPwRepository
    /// Autor: Gilmer Qm
    /// Fecha: 23/06/2023
    /// <summary>
    /// Gestión general de T_RevisionNivel_Pw
    /// </summary>
    public class RevisionNivelPwRepository : GenericRepository<TRevisionNivelPw>, IRevisionNivelPwRepository
    {
        private Mapper _mapper;
        public RevisionNivelPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRevisionNivelPw, RevisionNivelPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TRevisionNivelPw MapeoEntidad(RevisionNivelPw entidad)
        {
            try
            {
                //crea la entidad padre
                TRevisionNivelPw modelo = _mapper.Map<TRevisionNivelPw>(entidad);

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
        public TRevisionNivelPw Add(RevisionNivelPw entidad)
        {
            try
            {
                var RevisionNivelPw = MapeoEntidad(entidad);
                base.Insert(RevisionNivelPw);
                return RevisionNivelPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TRevisionNivelPw Update(RevisionNivelPw entidad)
        {
            try
            {
                var RevisionNivelPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RevisionNivelPw.RowVersion = entidadExistente.RowVersion;

                base.Update(RevisionNivelPw);
                return RevisionNivelPw;
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
        public IEnumerable<TRevisionNivelPw> Add(IEnumerable<RevisionNivelPw> listadoEntidad)
        {
            try
            {
                List<TRevisionNivelPw> listado = new List<TRevisionNivelPw>();
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
        public IEnumerable<TRevisionNivelPw> Update(IEnumerable<RevisionNivelPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRevisionNivelPw> listado = new List<TRevisionNivelPw>();
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
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Versio: 1.0
        /// <summary>
        /// Obtiene todos los registros de RevisionNivel_Pw por el IdRevision
        /// </summary>
        /// <returns> IEnumerable<RevisionNivelPwDTO> </returns>
        public IEnumerable<RevisionNivelPwDTO> ObtenerPorIdRevisionPw(int idRevisionPw)
        {
            try
            {
                var _query = "SELECT Id,Nombre,Prioridad,IdTipoRevisionPw,IdRevisionPw FROM pla.V_ObtenerRevisonNivelPorIdRevisionPw WHERE IdRevisionPw = @IdRevisionPw and Estado = 1 ";
                var revisionNivel = _dapperRepository.QueryDapper(_query, new { IdRevisionPw = idRevisionPw });
                if (!revisionNivel.Equals(null) && !revisionNivel.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<RevisionNivelPwDTO>>(revisionNivel);

                return new List<RevisionNivelPwDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
