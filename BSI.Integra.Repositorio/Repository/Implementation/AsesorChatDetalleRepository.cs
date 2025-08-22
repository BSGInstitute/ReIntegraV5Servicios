using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AsesorChatDetalleRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 17/11/2022
    /// <summary>
    /// Gestión general de T_AsesorChatDetalle
    /// </summary>
    public class AsesorChatDetalleRepository : GenericRepository<TAsesorChatDetalle>, IAsesorChatDetalleRepository
    {
        private Mapper _mapper;

        public AsesorChatDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsesorChatDetalle, AsesorChatDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAsesorChatDetalle MapeoEntidad(AsesorChatDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TAsesorChatDetalle modelo = _mapper.Map<TAsesorChatDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsesorChatDetalle Add(AsesorChatDetalle entidad)
        {
            try
            {
                var AsesorChatDetalle = MapeoEntidad(entidad);
                base.Insert(AsesorChatDetalle);
                return AsesorChatDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsesorChatDetalle Update(AsesorChatDetalle entidad)
        {
            try
            {
                var AsesorChatDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsesorChatDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(AsesorChatDetalle);
                return AsesorChatDetalle;
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


        public IEnumerable<TAsesorChatDetalle> Add(IEnumerable<AsesorChatDetalle> listadoEntidad)
        {
            try
            {
                List<TAsesorChatDetalle> listado = new List<TAsesorChatDetalle>();
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

        public IEnumerable<TAsesorChatDetalle> Update(IEnumerable<AsesorChatDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsesorChatDetalle> listado = new List<TAsesorChatDetalle>();
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

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de pais asignados para un asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerPaisesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdPais AS Id FROM com.V_TAsesorChatDetalle_ObtenerPaisAgrupadosPorAsesorChatDetalle WHERE Estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdPais";
                var asesorChatsDB = _dapperRepository.QueryDapper(_query, new { idAsesorChat });

                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]") && asesorChatsDB != null && asesorChatsDB != "null")
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de programa generales asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerProgramasGeneralesPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdPGeneral AS Id FROM com.V_TAsesorChatDetalle_ObtenerProgramaGeneralAgrupadosPorAsesorChatDetalle WHERE estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdPGeneral";
                var asesorChatsDB = _dapperRepository.QueryDapper(_query, new { idAsesorChat });
                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]") && asesorChatsDB != null && asesorChatsDB != "null")
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            try
            {
                List<IdDTO> asesorChats = new List<IdDTO>();
                var _query = "SELECT IdAreaCapacitacion AS Id FROM com.V_TAsesorChatDetalle_ObtenerAreaCapacitacionAgrupadosPorAsesorChatDetalle WHERE Estado = 1 AND IdAsesorChat = @idAsesorChat GROUP BY IdAreaCapacitacion";
                var asesorChatsDB = _dapperRepository.QueryDapper(_query, new { idAsesorChat });
                if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]") && asesorChatsDB != null && asesorChatsDB != "null")
                {
                    asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
                }
                return asesorChats;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de sub area capacitacion asignados por asesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorChat (PK de la tabla com.T_AsesorChat)</param>
        /// <returns>Lista de objetos de clase IdDTO</returns>
        public List<IdDTO> ObtenerSubAreasCapacitacionPorIdAsesorChat(int idAsesorChat)
        {
            List<IdDTO> asesorChats = new List<IdDTO>();
            var _query = "SELECT IdSubAreaCapacitacion AS Id FROM com.V_TAsesorChatDetalle_ObtenerSubAreaCapacitacionAgrupadosPorAsesorChatDetalle where Estado = 1 and  IdAsesorChat = @idAsesorChat GROUP BY IdSubAreaCapacitacion";
            var asesorChatsDB = _dapperRepository.QueryDapper(_query, new { idAsesorChat });
            if (!string.IsNullOrEmpty(asesorChatsDB) && !asesorChatsDB.Contains("[]") && asesorChatsDB != null && asesorChatsDB != "null")
            {
                asesorChats = JsonConvert.DeserializeObject<List<IdDTO>>(asesorChatsDB);
            }
            return asesorChats;
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualizar el asesor chat detalle e inserta un log en la tabla com.T_ChatIntegraHistorialAsesor
        /// </summary>
        /// <param name="idAsesorChat">Id del asesor chat enlazado a la configuracion (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="idPersonal">Id del personal asignado a la configuracion (PK de la tabla gp.T_Personal)</param>
        /// <param name="usuario">Cadena con el usuario que ejecuto la actualizacion</param>
        /// <param name="listaProgramas">Lista de indices de los programas generales que estan habilitados para esa configuracion (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="listaPaises">Lista de indices de los paises que estan habilitados para esa configuracion (PK de la tabla conf.T_Pais)</param>
        /// <returns></returns>
        public void ActualizarAsesorChaDetalleYLog(int idAsesorChat, int idPersonal, string usuario, string listaProgramas, string listaPaises)
        {
            try
            {
                var aux=_dapperRepository.QuerySPDapper("com.SP_ActualizarAsesorChatDetalle_Log", new { idAsesorChat, idPersonal, usuario, listaProgramas, listaPaises });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza a 0 el idAsesorChat en la tabla com.T_AsesorChatDetalle
        /// </summary>
        /// <param name="idAsesorChat">Id del asesorchat (PK de la tabla com.T_AsesorChat)</param>
        /// <param name="nombreUsuario">Nombre del usuario que ejecuta el eliminado</param>
        /// <returns></returns>
        public void EliminarAsesorChatDetalle(int idAsesorChat, string nombreUsuario)
        {
            try
            {
                _dapperRepository.QuerySPDapper("com.SP_EliminarAsesorChatDetalle", new { idAsesorChat, nombreUsuario });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
