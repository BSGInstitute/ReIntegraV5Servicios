using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: SeccionTipoContenidoPwRepository
    /// Autor: Gilmer Qm
    /// Fecha: 23/06/2023
    /// <summary>
    /// Gestión general de T_SeccionTipoContenidoPw
    /// </summary>
    public class SeccionTipoContenidoPwRepository : GenericRepository<TSeccionTipoContenidoPw>, ISeccionTipoContenidoPwRepository
    {
        private Mapper _mapper;
        public SeccionTipoContenidoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeccionTipoContenidoPw, SeccionTipoContenidoPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSeccionTipoContenidoPw MapeoEntidad(SeccionTipoContenidoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TSeccionTipoContenidoPw modelo = _mapper.Map<TSeccionTipoContenidoPw>(entidad);

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
        public TSeccionTipoContenidoPw Add(SeccionTipoContenidoPw entidad)
        {
            try
            {
                var SeccionTipoContenidoPw = MapeoEntidad(entidad);
                base.Insert(SeccionTipoContenidoPw);
                return SeccionTipoContenidoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSeccionTipoContenidoPw Update(SeccionTipoContenidoPw entidad)
        {
            try
            {
                var SeccionTipoContenidoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SeccionTipoContenidoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(SeccionTipoContenidoPw);
                return SeccionTipoContenidoPw;
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


        public IEnumerable<TSeccionTipoContenidoPw> Add(IEnumerable<SeccionTipoContenidoPw> listadoEntidad)
        {
            try
            {
                List<TSeccionTipoContenidoPw> listado = new List<TSeccionTipoContenidoPw>();
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

        public IEnumerable<TSeccionTipoContenidoPw> Update(IEnumerable<SeccionTipoContenidoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeccionTipoContenidoPw> listado = new List<TSeccionTipoContenidoPw>();
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
        /// <returns> IEnumerable<SeccionTipoContenidoPwDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_SeccionTipoContenido_PW
                            WHERE Estado = 1
                            ORDER BY Id DESC;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        } 
    }
}
