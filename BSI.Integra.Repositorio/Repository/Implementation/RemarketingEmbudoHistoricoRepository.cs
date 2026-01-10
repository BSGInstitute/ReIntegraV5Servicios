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
    /// Repositorio: RemarketingEmbudoHistoricoRepository
    /// Autor: Max Mantilla
    /// Fecha: 06/01/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoHistorico
    /// </summary>
    public class RemarketingEmbudoHistoricoRepository : GenericRepository<TRemarketingEmbudoHistorico>, IRemarketingEmbudoHistoricoRepository
    {
        private Mapper _mapper;

        public RemarketingEmbudoHistoricoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemarketingEmbudoHistorico, RemarketingEmbudoHistorico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRemarketingEmbudoHistorico MapeoEntidad(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                //crea la entidad padre
                TRemarketingEmbudoHistorico modelo = _mapper.Map<TRemarketingEmbudoHistorico>(entidad);

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

        public TRemarketingEmbudoHistorico Add(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var RemarketingEmbudoHistorico = MapeoEntidad(entidad);
                base.Insert(RemarketingEmbudoHistorico);
                return RemarketingEmbudoHistorico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemarketingEmbudoHistorico Update(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var RemarketingEmbudoHistorico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RemarketingEmbudoHistorico.RowVersion = entidadExistente.RowVersion;

                base.Update(RemarketingEmbudoHistorico);
                return RemarketingEmbudoHistorico;
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


        public IEnumerable<TRemarketingEmbudoHistorico> Add(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                List<TRemarketingEmbudoHistorico> listado = new List<TRemarketingEmbudoHistorico>();
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

        public IEnumerable<TRemarketingEmbudoHistorico> Update(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemarketingEmbudoHistorico> listado = new List<TRemarketingEmbudoHistorico>();
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

        public async Task<List<OportunidadRemarketingEmbudoDTO>> ObtenerInformacionOportunidadRemarketing(DateTime? FechaCorte = null)
        {
            try
            {
                List<OportunidadRemarketingEmbudoDTO> informacionOportunidad = new List<OportunidadRemarketingEmbudoDTO>();
                string _query = "ia.SP_TRemarketingEmbudoHistorico_Insertar";
                var parametros = new { FechaCorte };

                // NO usar .Result - usar await directamente
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    _query,
                    parametros,
                    timeoutMinutos: 5  // 5 minutos
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    informacionOportunidad = JsonConvert.DeserializeObject<List<OportunidadRemarketingEmbudoDTO>>(resultado)!;
                    return informacionOportunidad;
                }

                return new List<OportunidadRemarketingEmbudoDTO>();
            }
            catch (Exception e)
            {
                // No solo relanzar, agregar contexto
                throw new Exception($"Error obteniendo información de remarketing: {e.Message}", e);
            }
        }
        public List<RemarketingEmbudoNivelDescripcionDTO> ObtenerInformacionRemarketingEmbudoNivel()
        {
            try
            {
                List<RemarketingEmbudoNivelDescripcionDTO> informacionRemarketingEmbudoNivel = new List<RemarketingEmbudoNivelDescripcionDTO>();
                var query = @"SELECT Id,Codigo,Nombre FROM ia.T_RemarketingEmbudoNivel";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    informacionRemarketingEmbudoNivel = JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelDescripcionDTO>>(resultado);
                    return informacionRemarketingEmbudoNivel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void RegistrarEmbudoRemarketing(int IdRemarketingEmbudoNivel, int IdAlumno, DateTime FechaClasificacion)
        {
            try
            {
                var query = "ia.SP_RemarketingEmbudoHistorico_Insertar";

                var parametros = new
                {
                    IdRemarketingEmbudoNivel = IdRemarketingEmbudoNivel,
                    IdAlumno = IdAlumno,
                    FechaClasificacion = FechaClasificacion,
                    Usuario = "EmbudoRemarketing"
                };
                _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Error al insertar en RegistrarEmbudoRemarketing", ex);
            }
        }
        public List<RemarketingEmbudoNivelLlamadaEfectivaDTO> ObtenerLlamadasEfectivasOportunidadAlumno()
        {
            try
            {
                var query = "ia.SP_RemarketingEmbudoObtenerLlamadasEfectivas";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelLlamadaEfectivaDTO>>(resultado)!;
                }
                return new List<RemarketingEmbudoNivelLlamadaEfectivaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerColorPerfilProgramaPorIdOportunidad: {ex.Message}", ex);
            }
        }
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing()
        {
            try
            {
                List<RemarketingEmbudoEsquemaNivelDTO> informacionRemarketingEmbudoNivel = new List<RemarketingEmbudoEsquemaNivelDTO>();
                var query = @"SELECT REN.Id AS IdNivel,Ren.Nombre AS Nivel,REE.Id AS IdEsquema,REE.Nombre AS Esquema, REN.IdRemarketingEmbudoEsquema  
                            FROM ia.T_RemarketingEmbudoNivel AS REN
                            INNER JOIN ia.T_RemarketingEmbudoEsquema AS REE ON REE.Id=REN.IdRemarketingEmbudoEsquema AND REE.Estado=1
                            WHERE REN.Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    informacionRemarketingEmbudoNivel = JsonConvert.DeserializeObject<List<RemarketingEmbudoEsquemaNivelDTO>>(resultado);
                    return informacionRemarketingEmbudoNivel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<RemarketingEmbudoNivelInteraccionProgresivoDTO> ObtenerInteraccionFormularioProgresivo()
        {
            try
            {
                var query = @"SELECT Correo, FechaCreacion  
                            FROM [192.168.2.5].integraDB_PortalWeb.mkt.T_ProgressiveProfilingCodigoDescuentoCorreo
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelInteraccionProgresivoDTO>>(resultado)!;
                }
                return new List<RemarketingEmbudoNivelInteraccionProgresivoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerInteraccionFormularioProgresivo: {ex.Message}", ex);
            }
        }
        public List<OportunidadScoreDTO> ObtenerScoreOportunidadAlumno(int registrosPorPagina)
        {
            try
            {
                var todosLosScores = new List<OportunidadScoreDTO>();
                var totalRegistros = 0;
                var queryCount = "SELECT COUNT(*) AS Valor FROM ia.T_RemarketingIAScoreContacto WHERE estado = 1";
                var resultadoCount = _dapperRepository.FirstOrDefault(queryCount, null);
                if (!string.IsNullOrEmpty(resultadoCount) && resultadoCount != "[]")
                {
                    var total = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoCount);
                    totalRegistros = total.Valor.Value;
                }
                if (totalRegistros == 0)
                    return todosLosScores;

                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
                for (int paginaActual = 1; paginaActual <= totalPaginas; paginaActual++)
                {
                    var parametros = new
                    {
                        NroPagina = paginaActual,
                        RegistrosPagina = registrosPorPagina
                    };

                    var query = "ia.SP_RemarketingIAObtenerScoresEnBloque";
                    var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                    if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    {
                        var scoresPagina = JsonConvert.DeserializeObject<List<OportunidadScoreDTO>>(resultado);

                        if (scoresPagina != null && scoresPagina.Any())
                        {
                            todosLosScores.AddRange(scoresPagina);
                        }
                    }
                }
                return todosLosScores;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerTodosScoresOportunidadAlumno: {ex.Message}", ex);
            }
        }
    }
}

