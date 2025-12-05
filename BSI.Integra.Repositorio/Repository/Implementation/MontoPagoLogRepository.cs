using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class MontoPagoLogRepository : GenericRepository<TMontoPagoLog>, IMontoPagoLogRepository
    {
        private Mapper _mapper;

        public MontoPagoLogRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoLog, MontoPagoLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<MontoPagoLog, MontoPagoLogDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoLog, TMontoPagoLog>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMontoPagoLog MapeoEntidad(MontoPagoLog entidad)
        {
            try
            {
                //crea la entidad padre
                TMontoPagoLog modelo = _mapper.Map<TMontoPagoLog>(entidad);

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

        public TMontoPagoLog Add(MontoPagoLog entidad)
        {
            try
            {
                var MontoPagoLog = MapeoEntidad(entidad);
                base.Insert(MontoPagoLog);
                return MontoPagoLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMontoPagoLog Update(MontoPagoLog entidad)
        {
            try
            {
                var MontoPagoLog = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MontoPagoLog.RowVersion = entidadExistente.RowVersion;

                base.Update(MontoPagoLog);
                return MontoPagoLog;
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


        public IEnumerable<TMontoPagoLog> Add(IEnumerable<MontoPagoLog> listadoEntidad)
        {
            try
            {
                List<TMontoPagoLog> listado = new List<TMontoPagoLog>();
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

        public IEnumerable<TMontoPagoLog> Update(IEnumerable<MontoPagoLog> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMontoPagoLog> listado = new List<TMontoPagoLog>();
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

        public MontoPagoLog? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
                       IdMontoPago,
                       PrecioOriginal,
                       PrecioModificado,
                       PrecioLetrasOriginal,
                       PrecioLetrasModificado,
                       IdMoneda_Original,
                       IdMoneda_Modificado,
                       MatriculaOriginal,
                       MatriculaModificado,
                       CuotasOriginal,
                       CuotasModificado,
                       NroCuotasOriginal,
                       NroCuotasModificado,
                       IdTipoDescuento_Original,
                       IdTipoDescuento_Modificado,
                       IdPGeneral_Original,
                       IdPGeneral_Modificado,
                       IdTipoPago_Original,
                       IdTipoPago_Modificado,
                       IdPais_Original,
                       IdPais_Modificado,
                       VencimientoOriginal,
                       VencimientoModificado,
                       PrimeraCuotaOriginal,
                       PrimeraCuotaModificado,
                       CuotaDobleOriginal,
                       CuotaDobleModificado,
                       DescripcionOriginal,
                       DescripcionModificado,
                       VisibleWebOriginal,
                       VisibleWebModificado,
                       PaqueteOriginal,
                       PaqueteModificado,
                       PorDefectoOriginal,
                       PorDefectoModificado,
                       MontoDescontadoOriginal,
                       MontoDescontadoModificado,
                       Accion,
                       Estado,
                       UsuarioCreacion,
                       UsuarioModificacion,
                       FechaCreacion,
                       FechaModificacion,
                       RowVersion 
                    FROM pla.T_MontoPagoLog
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MontoPagoLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public MontoPagoLog? ObtenerPorIdMontoPago(int IdMontoPago)
        {
            try
            {
                var query = @"
                    SELECT Id,
                       IdMontoPago,
                       PrecioOriginal,
                       PrecioModificado,
                       PrecioLetrasOriginal,
                       PrecioLetrasModificado,
                       IdMoneda_Original,
                       IdMoneda_Modificado,
                       MatriculaOriginal,
                       MatriculaModificado,
                       CuotasOriginal,
                       CuotasModificado,
                       NroCuotasOriginal,
                       NroCuotasModificado,
                       IdTipoDescuento_Original,
                       IdTipoDescuento_Modificado,
                       IdPGeneral_Original,
                       IdPGeneral_Modificado,
                       IdTipoPago_Original,
                       IdTipoPago_Modificado,
                       IdPais_Original,
                       IdPais_Modificado,
                       VencimientoOriginal,
                       VencimientoModificado,
                       PrimeraCuotaOriginal,
                       PrimeraCuotaModificado,
                       CuotaDobleOriginal,
                       CuotaDobleModificado,
                       DescripcionOriginal,
                       DescripcionModificado,
                       VisibleWebOriginal,
                       VisibleWebModificado,
                       PaqueteOriginal,
                       PaqueteModificado,
                       PorDefectoOriginal,
                       PorDefectoModificado,
                       MontoDescontadoOriginal,
                       MontoDescontadoModificado,
                       Accion,
                       Estado,
                       UsuarioCreacion,
                       UsuarioModificacion,
                       FechaCreacion,
                       FechaModificacion,
                       RowVersion 
                    FROM pla.T_MontoPagoLog
                    WHERE IdMontoPago=@IdMontoPago AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMontoPago=IdMontoPago });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MontoPagoLog>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        public IEnumerable<MontoPagoLogDTO> ObtenerReporteMontoPagoHistorico(FiltroMontoPagoHistoricoDTO filtro)
        {
            try
            {
                List<MontoPagoLogDTO> rpta = new List<MontoPagoLogDTO>();

                var query = "pla.sp_ObtenerMontoPagoHistorico";
                var parametros = new
                {
                    FechaInicio = filtro.FechaInicial,
                    FechaFin = filtro.FechaFinal,
                    IdPGeneral = filtro.IdPGeneral,
                    IdTipoPago = filtro.IdTipoPago,
                    IdVersion = filtro.IdVersion,
                    IdPais = filtro.IdPais

                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MontoPagoLogDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerReporteLeadsByFecha() {ex.Message}", ex);
            }
        }

    }
}
