using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PersonalLogRepository : GenericRepository<TPersonalLog>, IPersonalLogRepository
    {
        private Mapper _mapper;

        public PersonalLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPersonalLog, PersonalLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<PersonalLog, TPersonalLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPersonalLog MapeoEntidad(PersonalLog entidad)
        {
            try
            {
                //crea la entidad padre
                TPersonalLog modelo = _mapper.Map<TPersonalLog>(entidad);

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

        public TPersonalLog Add(PersonalLog entidad)
        {
            try
            {
                var PersonalLog = MapeoEntidad(entidad);
                base.Insert(PersonalLog);
                return PersonalLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersonalLog Update(PersonalLog entidad)
        {
            try
            {
                var PersonalLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PersonalLog.RowVersion = entidadExistente.RowVersion;

                base.Update(PersonalLog);
                return PersonalLog;
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


        public IEnumerable<TPersonalLog> Add(IEnumerable<PersonalLog> listadoEntidad)
        {
            try
            {
                List<TPersonalLog> listado = new List<TPersonalLog>();
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

        public IEnumerable<TPersonalLog> Update(IEnumerable<PersonalLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPersonalLog> listado = new List<TPersonalLog>();
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

        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la fecha fin del utlimo registro del personal
        /// </summary>
        /// <param name="id"> Id de Personal </param>
        /// <returns>  DateTime? </returns>
        public DateTime? ObtenerFechaFin(int id)
        {
            try
            {
                var _resultado = new DateTime?();
                string query = "SELECT FechaFin FROM gp.T_PersonalLog WHERE Estado = 1 AND IdPersonal = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<DateTime>(resultado);
                }
                return _resultado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la registro histórico de Jefe Inmediato por Personal
        /// </summary>
        /// <param name="idPersonal"> Id de Personal</param>
        /// <returns> List<PersonalJefeInmediatoDTO> </returns>
        public List<PersonalJefeInmediatoDTO> ObtenerJefeInmediatoHistorico(int idPersonal)
        {
            try
            {
                List<PersonalJefeInmediatoDTO> listaJefeInmediato = new List<PersonalJefeInmediatoDTO>();
                string query = "SELECT Id,IdJefe,DatosJefe,FechaInicio,FechaFin FROM [gp].[V_TPersonalLog_ObtenerHistoricoJefeInmediato] WHERE Estado = 1 AND EstadoIdJefe = 1 AND IdPersonal = @idPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaJefeInmediato = JsonConvert.DeserializeObject<List<PersonalJefeInmediatoDTO>>(resultado);
                }
                return listaJefeInmediato;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalLogRepositorio
        ///Autor: Luis H.,Edgar S.
        ///Fecha: 19/03/2021
        /// <summary>
        /// Obtiene la registro histórico de Tipo de Asesor
        /// </summary>
        /// <param name="idPersonal"> Id de Personal</param>
        /// <returns> List<PersonalTipoAsesorDTO> </returns>
        public List<PersonalTipoAsesorDTO> ObtenerTipoAsesorHistorico(int idPersonal)
        {
            try
            {
                List<PersonalTipoAsesorDTO> listaJefeInmediato = new List<PersonalTipoAsesorDTO>();
                string query = "SELECT Id,IdCerrador,EsCerrador, FechaInicio, FechaFin FROM [gp].[V_TPersonalLog_ObtenerHistoricoTipoAsesor] WHERE EstadoCerrador = 1 AND IdPersonal = @idPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaJefeInmediato = JsonConvert.DeserializeObject<List<PersonalTipoAsesorDTO>>(resultado);
                }
                return listaJefeInmediato;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PersonalLog> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                var query = @"SELECT Id,
                       IdPersonal, 
                        Rol,
                        TipoPersonal,
                        IdJefe,
                        EstadoRol,
                        EstadoTipoPersonal,
                        EstadoIdJefe,
                        FechaInicio,
                        FechaFin,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion,
                        IdCerrador,
                        EsCerrador,
                        EstadoCerrador,
                        IdPuestoTrabajoNivel  
                      FROM gp.T_PersonalLog 
                      WHERE Estado=1 AND IdPersonal = @IdPersonal";

                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var personalLog = JsonConvert.DeserializeObject< List<PersonalLog>>(resultado);
                    return personalLog;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
