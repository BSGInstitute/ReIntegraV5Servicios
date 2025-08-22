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
    /// Repositorio: SeccionTipoDetallePwRepository
    /// Autor: Gilmer Qm
    /// Fecha: 27/06/2023
    /// <summary>
    /// Gestión general de T_SeccionTipoDetallePw
    /// </summary>
    public class SeccionTipoDetallePwRepository : GenericRepository<TSeccionTipoDetallePw>, ISeccionTipoDetallePwRepository
    {
        private Mapper _mapper;
        public SeccionTipoDetallePwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeccionTipoDetallePw, SeccionTipoDetallePw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSeccionTipoDetallePw MapeoEntidad(SeccionTipoDetallePw entidad)
        {
            try
            {
                //crea la entidad padre
                TSeccionTipoDetallePw modelo = _mapper.Map<TSeccionTipoDetallePw>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSeccionTipoDetallePw Add(SeccionTipoDetallePw entidad)
        {
            try
            {
                var SeccionTipoDetallePw = MapeoEntidad(entidad);
                base.Insert(SeccionTipoDetallePw);
                return SeccionTipoDetallePw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TSeccionTipoDetallePw Update(SeccionTipoDetallePw entidad)
        {
            try
            {
                var SeccionTipoDetallePw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SeccionTipoDetallePw.RowVersion = entidadExistente.RowVersion;

                base.Update(SeccionTipoDetallePw);
                return SeccionTipoDetallePw;
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
        public IEnumerable<TSeccionTipoDetallePw> Add(IEnumerable<SeccionTipoDetallePw> listadoEntidad)
        {
            try
            {
                List<TSeccionTipoDetallePw> listado = new List<TSeccionTipoDetallePw>();
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
        public IEnumerable<TSeccionTipoDetallePw> Update(IEnumerable<SeccionTipoDetallePw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeccionTipoDetallePw> listado = new List<TSeccionTipoDetallePw>();
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
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SeccionTipoDetalle_PW Asociados al IdSeccionPw.
        /// </summary>
        /// <param name="idSeccionPw"> (PK) de T_Seccion_PW </param>
        /// <returns> IEnumerable<PlantillaPais> </returns>
        public IEnumerable<SeccionTipoDetallePw> ObtenerPorIdSeccionPw(int idSeccionPw)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdSeccionPw,
                                   NombreTitulo,
                                   IdSeccionTipoContenido,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_SeccionTipoDetalle_PW
                            WHERE Estado = 1
                                  AND IdSeccionPw = @IdSeccionPw;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdSeccionPw = idSeccionPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SeccionTipoDetallePw>>(resultado);
                }
                return new List<SeccionTipoDetallePw>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el id.
        /// </summary>
        /// <param name="id"> (PK) de T_SeccionTipoDetalle_PW </param>
        /// <returns> IEnumerable<PlantillaPais> </returns>
        public SeccionTipoDetallePw ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdSeccionPw,
                                   NombreTitulo,
                                   IdSeccionTipoContenido,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_SeccionTipoDetalle_PW
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<SeccionTipoDetallePw>(resultado);
                }
                return new SeccionTipoDetallePw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 2127/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene IdSeccionTipoDetalle_PW de Seccion y Subseccion
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (IdSeccionTipoDetallePWDTO)</returns>
        public IEnumerable<SeccionTipoDetallePwEstructuraProgramaDTO> ObtenerIdSeccionTipoDetallePorIdPGeneral(int idPGeneral)
        {
            string query = @"SELECT DISTINCT IdSeccionTipoDetalle_PW as IdSeccionTipoDetallePw, NombreTitulo FROM pla.V_ListadoEstructuraPrograma WHERE IdPGeneral=@IdPGeneral AND (NombreTitulo = 'Sesion' OR NombreTitulo='SubSeccion')";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Equals("[]"))
                return JsonConvert.DeserializeObject<IEnumerable<SeccionTipoDetallePwEstructuraProgramaDTO>>(queryDB);
            return new List<SeccionTipoDetallePwEstructuraProgramaDTO>(); 
        }
    }
}
