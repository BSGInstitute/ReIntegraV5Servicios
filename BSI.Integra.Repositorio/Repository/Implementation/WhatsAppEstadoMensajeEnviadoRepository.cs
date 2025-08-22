using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppEstadoMensajeEnviadoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_WhatsAppEstadoMensajeEnviado
    /// </summary>
    public class WhatsAppEstadoMensajeEnviadoRepository : GenericRepository<TWhatsAppEstadoMensajeEnviado>, IWhatsAppEstadoMensajeEnviadoRepository
    {
        private Mapper _mapper;

        public WhatsAppEstadoMensajeEnviadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppEstadoMensajeEnviado, WhatsAppEstadoMensajeEnviado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppEstadoMensajeEnviado MapeoEntidad(WhatsAppEstadoMensajeEnviado entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppEstadoMensajeEnviado modelo = _mapper.Map<TWhatsAppEstadoMensajeEnviado>(entidad);

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

        public TWhatsAppEstadoMensajeEnviado Add(WhatsAppEstadoMensajeEnviado entidad)
        {
            try
            {
                var WhatsAppEstadoMensajeEnviado = MapeoEntidad(entidad);
                base.Insert(WhatsAppEstadoMensajeEnviado);
                return WhatsAppEstadoMensajeEnviado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppEstadoMensajeEnviado Update(WhatsAppEstadoMensajeEnviado entidad)
        {
            try
            {
                var WhatsAppEstadoMensajeEnviado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppEstadoMensajeEnviado.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppEstadoMensajeEnviado);
                return WhatsAppEstadoMensajeEnviado;
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


        public IEnumerable<TWhatsAppEstadoMensajeEnviado> Add(IEnumerable<WhatsAppEstadoMensajeEnviado> listadoEntidad)
        {
            try
            {
                List<TWhatsAppEstadoMensajeEnviado> listado = new List<TWhatsAppEstadoMensajeEnviado>();
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

        public IEnumerable<TWhatsAppEstadoMensajeEnviado> Update(IEnumerable<WhatsAppEstadoMensajeEnviado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppEstadoMensajeEnviado> listado = new List<TWhatsAppEstadoMensajeEnviado>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppEstadoMensajeEnviado.
        /// </summary>
        /// <returns> List<WhatsAppEstadoMensajeEnviadoDTO> </returns>
        public IEnumerable<WhatsAppEstadoMensajeEnviadoDTO> ObtenerWhatsAppEstadoMensajeEnviado()
        {
            try
            {
                List<WhatsAppEstadoMensajeEnviadoDTO> rpta = new List<WhatsAppEstadoMensajeEnviadoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    WaId,
	                    WaRecipientId,
	                    WaStatus,
	                    WaTimeStamp,
	                    IdPais,
	                    EsMigracion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_WhatsAppEstadoMensajeEnviado
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppEstadoMensajeEnviadoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppEstadoMensajeEnviado para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppEstadoMensajeEnviadoComboDTO> </returns>
        public IEnumerable<WhatsAppEstadoMensajeEnviadoComboDTO> ObtenerCombo()
        {
            try
            {
                List<WhatsAppEstadoMensajeEnviadoComboDTO> rpta = new List<WhatsAppEstadoMensajeEnviadoComboDTO>();
                var query = @"SELECT Id,WaId,WaStatus FROM mkt.T_WhatsAppEstadoMensajeEnviado WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppEstadoMensajeEnviadoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Edson Mayta Escobedo
        /// Descripcion: Valida  si existe un registro de envio masivo por persona 
        /// </summary>
        /// <param name="celularWhatsappEstadoMensajeEnviado">el numero de celular no debe existir en la tabla WhatsAppEstadoMensajeEnviado por el día del envío masivo, para ser un envío correcto</param>
        /// <returns>Retorna 1 si existe el registro</returns>
        /// <returns>Retorna 2 si no existe el registro</returns>
        /// <returns>Retorna 3 si no se puede verificar la existencia por error de comunicacion con la base de datos</returns>
        public int VerificadEnvioDuplicadoPorEnvioMasivo(string celularWhatsappEstadoMensajeEnviado)
        {
            try
            {
                //ValorStringDTO rpta = new ValorStringDTO();
                WhatsAppEstadoMensajeEnviadoDTO WhatsappEstadoMensajeEnviado = new WhatsAppEstadoMensajeEnviadoDTO();
                string Query = "SELECT WaRecipientId , WaStatus FROM mkt.T_WhatsAppEstadoMensajeEnviado WHERE WaRecipientId=@celularWhatsappEstadoMensajeEnviado AND FechaCreacion > CAST(GETDATE() AS DATE) AND WaStatus = 'sent' ";
                string QueryRespuesta = _dapperRepository.FirstOrDefault(Query, new { celularWhatsappEstadoMensajeEnviado });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    WhatsappEstadoMensajeEnviado = JsonConvert.DeserializeObject<WhatsAppEstadoMensajeEnviadoDTO>(QueryRespuesta);
                    return 1;
                }
                return 2;
            }
            catch
            {
                return 3;
            }
        }



        //public List<MensajeEnviadoErroneoWhatsAppDTO> ObtenerReporteMensajesEnviadosErroneos(FiltroMensajesEnviadosErroneosDTO filtros)
        //{
        //    try
        //    {
        //        List<MensajeEnviadoErroneoWhatsAppDTO> listadoMensajesEnviadosErroneos = new List<MensajeEnviadoErroneoWhatsAppDTO>();

        //        string query = @"
        //    SELECT 
        //         Id,
        //        CelularWhatsapp,
        //        IdAlumno,
        //        IdCampaniaGeneralDetalleResponsableWhatsapp,
        //        IdPlantilla,
        //        MensajePlantillaHtml,
        //        ObjetoPlantilla,
        //        IdPais,
        //        NumeroEnviado,
        //        MensajeErroneo,
        //        WaId,
        //        FechaCreacion


        //    FROM 
        //        mkt.T_MensajeEnviadoErroneoWhatsAppLog 
        //    WHERE 
        //        FechaCreacion >= @fechaInicio AND FechaCreacion <= @fechaFin ORDER BY Id DESC;";

        //        // Ejecutar la consulta solo si las fechas están presentes en el filtro
        //        if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
        //        {


        //            // Ejecutar la consulta con los parámetros de fecha
        //            string registrosMensajesEnviadosErroneosDB = _dapperRepository.QueryDapper(query, new { fechaInicio = filtros.FechaInicial.Value, fechaFin = filtros.FechaFinal.Value });

        //            // Deserializar los resultados si hay datos
        //            if (!string.IsNullOrEmpty(registrosMensajesEnviadosErroneosDB) && !registrosMensajesEnviadosErroneosDB.Contains("[]"))
        //            {
        //                listadoMensajesEnviadosErroneos = JsonConvert.DeserializeObject<List<MensajeEnviadoErroneoWhatsAppDTO>>(registrosMensajesEnviadosErroneosDB);
        //            }
        //        }

        //        // Aquí podrías realizar cualquier manipulación adicional de los datos antes de devolverlos

        //        return listadoMensajesEnviadosErroneos;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}




        public List<MensajeEnviadoErroneoWhatsAppDTO> ObtenerReporteMensajesEnviadosErroneos(FiltroMensajesEnviadosErroneosDTO filtros)
        {
            try
            {
                List<MensajeEnviadoErroneoWhatsAppDTO> listadoMensajesEnviadosErroneos = new List<MensajeEnviadoErroneoWhatsAppDTO>();

                string query = @"
            SELECT 
                 Id,
                CelularWhatsapp,
                IdAlumno,
                IdCampaniaGeneralDetalleResponsableWhatsapp,
                IdPlantilla,
                MensajePlantillaHtml,
                ObjetoPlantilla,
                IdPais,
                NumeroEnviado,
                MensajeErroneo,
                WaId,
                FechaCreacion
            FROM 
                mkt.T_MensajeEnviadoErroneoWhatsAppLog ";

                // Verificar si se especificaron fechas en los filtros
                if (filtros.FechaInicial.HasValue && filtros.FechaFinal.HasValue)
                {
                    query += @"
            WHERE 
                (FechaCreacion >= @fechaInicio AND FechaCreacion <= @fechaFin) OR (FechaCreacion IS NULL) ";
                }

                query += "ORDER BY Id DESC;";

                // Ejecutar la consulta
                string registrosMensajesEnviadosErroneosDB = _dapperRepository.QueryDapper(query, new { fechaInicio = filtros.FechaInicial, fechaFin = filtros.FechaFinal });

                // Deserializar los resultados si hay datos
                if (!string.IsNullOrEmpty(registrosMensajesEnviadosErroneosDB) && !registrosMensajesEnviadosErroneosDB.Contains("[]"))
                {
                    listadoMensajesEnviadosErroneos = JsonConvert.DeserializeObject<List<MensajeEnviadoErroneoWhatsAppDTO>>(registrosMensajesEnviadosErroneosDB);
                }

                // Aquí podrías realizar cualquier manipulación adicional de los datos antes de devolverlos

                return listadoMensajesEnviadosErroneos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
