using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: BandejaPendientePwRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 23/10/2023
    /// <summary>
    /// Gestión general de T_BandejaPendiente_PW
    /// </summary>
    public class BandejaPendientePwRepository : GenericRepository<TBandejaPendientePw>, IBandejaPendientePwRepository
    {
        private Mapper _mapper;
        public BandejaPendientePwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBandejaPendientePw, BandejaPendientePw>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TBandejaPendientePw MapeoEntidad(BandejaPendientePw entidad)
        {
            try
            {
                //crea la entidad padre
                TBandejaPendientePw modelo = _mapper.Map<TBandejaPendientePw>(entidad);

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
        public TBandejaPendientePw Add(BandejaPendientePw entidad)
        {
            try
            {
                var bandejaPendientePw = MapeoEntidad(entidad);
                base.Insert(bandejaPendientePw);
                return bandejaPendientePw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBandejaPendientePw Update(BandejaPendientePw entidad)
        {
            try
            {
                var bandejaPendientePw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                bandejaPendientePw.RowVersion = entidadExistente.RowVersion;

                base.Update(bandejaPendientePw);
                return bandejaPendientePw;
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


        public IEnumerable<TBandejaPendientePw> Add(IEnumerable<BandejaPendientePw> listadoEntidad)
        {
            try
            {
                List<TBandejaPendientePw> listado = new List<TBandejaPendientePw>();
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

        public IEnumerable<TBandejaPendientePw> Update(IEnumerable<BandejaPendientePw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBandejaPendientePw> listado = new List<TBandejaPendientePw>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el todo T_BandejaPendiente_PW por medio del idDocumento
        /// </summary>
        /// <param name="idDocumentoPw"></param>
        /// <returns> Entidad - DocumentoSeccionPw </returns>
        public List<BandejaPendientePw> ObtenerPorIdDocumento(int idDocumentoPw)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id,
                                IdDocumentoPW,
                                IdRevisionNivelPW AS IdRevisionNivelPw,
                                Secuencia,
                                EsFinal,
                                EsInicio,
                                IdPersonal,
                                EstadoRevisar,
                                Comentario,
                                ComentarioRechazar,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                            FROM 
                                pla.T_BandejaPendiente_PW
                            WHERE 
                                IdDocumentoPW = @IdDocumentoPW AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPW = idDocumentoPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<BandejaPendientePw>>(resultado)!;
                }
                return new List<BandejaPendientePw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#BPPwR-OPID-001@Error en ObtenerPorIdDocumento() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el todo T_BandejaPendiente_PW por medio del idDocumento
        /// </summary>
        /// <param name="idDocumentoPw"></param>
        /// <returns> Entidad - DocumentoSeccionPw </returns>
        public BandejaPendientePw? ObtenerPorIdDocumentoIdRevisionNivel(int idDocumentoPw, int idRevisionPw)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id,
                                IdDocumentoPW,
                                IdRevisionNivelPW AS IdRevisionNivelPw,
                                Secuencia,
                                EsFinal,
                                EsInicio,
                                IdPersonal,
                                EstadoRevisar,
                                Comentario,
                                ComentarioRechazar,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                            FROM 
                                pla.T_BandejaPendiente_PW
                            WHERE 
                                IdDocumentoPW = @IdDocumentoPW AND IdRevisionNivelPW = @IdRevisionNivelPW AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPW = idDocumentoPw, IdRevisionNivelPW = idRevisionPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<BandejaPendientePw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#BPPwR-OPIDIR-001@Error en ObtenerPorIdDocumentoIdRevisionNivel() {ex.Message}", ex);
            }
        }
    }
}
