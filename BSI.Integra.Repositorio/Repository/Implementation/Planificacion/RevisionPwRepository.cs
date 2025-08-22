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

{  /// Repositorio: RevisionPwRepository
   /// Autor: Gilmer Qm
   /// Fecha: 23/06/2023
   /// <summary>
   /// Gestión general de T_RevisionPw
   /// </summary>
    public class RevisionPwRepository : GenericRepository<TRevisionPw>, IRevisionPwRepository
    {
        private Mapper _mapper;
        public RevisionPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRevisionPw, RevisionPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TRevisionPw MapeoEntidad(RevisionPw entidad)
        {
            try
            {
                //crea la entidad padre
                TRevisionPw modelo = _mapper.Map<TRevisionPw>(entidad);

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
        public TRevisionPw Add(RevisionPw entidad)
        {
            try
            {
                var RevisionPw = MapeoEntidad(entidad);
                base.Insert(RevisionPw);
                return RevisionPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public TRevisionPw Update(RevisionPw entidad)
        {
            try
            {
                var RevisionPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RevisionPw.RowVersion = entidadExistente.RowVersion;

                base.Update(RevisionPw);
                return RevisionPw;
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
        public IEnumerable<TRevisionPw> Add(IEnumerable<RevisionPw> listadoEntidad)
        {
            try
            {
                List<TRevisionPw> listado = new List<TRevisionPw>();
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

        public IEnumerable<TRevisionPw> Update(IEnumerable<RevisionPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRevisionPw> listado = new List<TRevisionPw>();
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
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo.
        /// </summary>
        /// <returns> IEnumerable<RevisionPwDTO> </returns>
        public IEnumerable<RevisionPwDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Descripcion
                            FROM pla.T_Revision_PW
                            WHERE Estado = 1
                            ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<RevisionPwDTO>>(resultado)!;
                return new List<RevisionPwDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
    }
}
