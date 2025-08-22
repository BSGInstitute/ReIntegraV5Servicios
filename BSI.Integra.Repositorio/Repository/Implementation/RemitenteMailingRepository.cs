using AutoMapper;
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
    /// Repositorio: RemitenteMailingRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 09/11/2022
    /// <summary>
    /// Gestión general de T_RemitenteMailing
    /// </summary>
    public class RemitenteMailingRepository : GenericRepository<TRemitenteMailing>, IRemitenteMailingRepository
    {
        private Mapper _mapper;

        public RemitenteMailingRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemitenteMailing, RemitenteMailing>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRemitenteMailing MapeoEntidad(RemitenteMailing entidad)
        {
            try
            {
                //crea la entidad padre
                TRemitenteMailing modelo = _mapper.Map<TRemitenteMailing>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemitenteMailing Add(RemitenteMailing entidad)
        {
            try
            {
                var AsignacionAutomatica = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomatica);
                return AsignacionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemitenteMailing Update(RemitenteMailing entidad)
        {
            try
            {
                var AsignacionAutomatica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomatica.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomatica);
                return AsignacionAutomatica;
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


        public IEnumerable<TRemitenteMailing> Add(IEnumerable<RemitenteMailing> listadoEntidad)
        {
            try
            {
                List<TRemitenteMailing> listado = new List<TRemitenteMailing>();
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

        public IEnumerable<TRemitenteMailing> Update(IEnumerable<RemitenteMailing> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemitenteMailing> listado = new List<TRemitenteMailing>();
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
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los RemitenteMailing para ser mostrados en una grilla (para su propio CRUD), 
        /// </summary>
        /// <returns></returns>
        public List<RemitenteMailingDTO> ObtenerTodosRemitenteMailing()
        {
            try
            {
                List<RemitenteMailingDTO> RemitenteMailing = new List<RemitenteMailingDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Descripcion FROM mkt.T_RemitenteMailing WHERE  Estado = 1 order by id desc";
                var RemitenteMailingDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(RemitenteMailingDB) && !RemitenteMailingDB.Contains("[]") && RemitenteMailingDB != null && RemitenteMailingDB != "null")
                {
                    RemitenteMailing = JsonConvert.DeserializeObject<List<RemitenteMailingDTO>>(RemitenteMailingDB);
                }
                return RemitenteMailing;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista con los asesores y sus emails dado el Id de RemitenteMailing
        /// </summary>
        /// <returns></returns>
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing)
        {
            try
            {
                List<RemitenteMailingAsesorDTO> listaRemitenteMailingAsesor = new List<RemitenteMailingAsesorDTO>();
                string query = "SELECT IdRemitenteMailing, IdPersonal, NombreCompleto, CorreoElectronico FROM [mkt].[V_ObtenerRemitenteMeilingPersonalActivo] WHERE IdRemitenteMailing=" + IdRemitenteMailing + " AND Estado = 1";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(responseQuery) && !responseQuery.Contains("[]") && responseQuery != null && responseQuery != "null")
                {
                    listaRemitenteMailingAsesor = JsonConvert.DeserializeObject<List<RemitenteMailingAsesorDTO>>(responseQuery);
                }
                return listaRemitenteMailingAsesor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
