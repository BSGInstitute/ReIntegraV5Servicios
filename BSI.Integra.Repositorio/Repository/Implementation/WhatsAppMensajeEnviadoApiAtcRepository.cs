using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.Interface;

using Dapper;

using System;
using System.Data;
using System.Threading.Tasks;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiAtcDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppMensajeEnviadoApiAtcRepository
    /// Autor: Alexis Arroyo
    /// Fecha: 15/04/2026
    /// Version: 1.0
    /// <summary>
    /// Acceso a datos para la logica del chatbot ATC de WhatsApp.
    /// Opera sobre tablas ia.* y ope.T_WhatsAppMensajeClasificacion
    /// que no tienen modelo EF, por lo que se usa Dapper con stored procedures.
    /// </summary>
    public class WhatsAppMensajeEnviadoApiAtcRepository : IWhatsAppMensajeEnviadoApiAtcRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDapperRepository  _dapperRepository;

        public WhatsAppMensajeEnviadoApiAtcRepository(IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _connectionFactory = connectionFactory;
            _dapperRepository  = dapperRepository;
        }

        /// <summary>
        /// Verifica si el personal tiene un BOT de ATC asignado y activo.
        /// </summary>
        public async Task<bool> TieneBotAsignado(int idPersonal)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdPersonal", idPersonal, DbType.Int32);

                var id = await conn.QueryFirstOrDefaultAsync<int?>(
                    "ia.SP_ChatbotAtcWhatsApp_TieneBotAsignado",
                    parametros,
                    commandType: CommandType.StoredProcedure);

                return id.HasValue && id.Value > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene el hilo chat abierto (no cerrado) asociado al alumno.
        /// Retorna null si no existe hilo abierto.
        /// </summary>
        public async Task<HiloChatAtcDTO?> ObtenerHiloAbierto(int idAlumno)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdAlumno", idAlumno, DbType.Int32);

                return await conn.QueryFirstOrDefaultAsync<HiloChatAtcDTO>(
                    "ia.SP_ChatbotWhatsAppAtcHiloChat_ObtenerHiloAbierto",
                    parametros,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Crea un nuevo hilo chat con estado ASESOR (3).
        /// Retorna el Id generado.
        /// </summary>
        public async Task<int> CrearHiloChat(int? idAlumno, string numeroWhatsApp, string usuario)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdAlumno",                     idAlumno,       DbType.Int32);
                parametros.Add("@NumeroWhatsApp",               numeroWhatsApp, DbType.String);
                parametros.Add("@FechaInicio",                  null,           DbType.DateTime);
                parametros.Add("@IdChatbotConversacionEstado",  3,              DbType.Int32);  // 3 = ASESOR
                parametros.Add("@Usuario",                      usuario,        DbType.String);

                return await conn.QueryFirstOrDefaultAsync<int>(
                    "ia.SP_TChatbotWhatsAppAtcHiloChat_Insertar",
                    parametros,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el estado del hilo a ASESOR (3) y registra la fecha de derivacion.
        /// Se usa cuando el hilo estaba siendo atendido por el BOT.
        /// </summary>
        public async Task ActualizarHiloAsesor(int idHilo, string usuario)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdHilo",  idHilo,  DbType.Int32);
                parametros.Add("@Usuario", usuario, DbType.String);

                await conn.QueryFirstOrDefaultAsync<int>(
                    "ia.SP_ChatbotWhatsAppAtcHiloChat_ActualizarEstadoAsesor",
                    parametros,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta clasificacion y mensaje chatbot en un solo SP.
        /// Llama a ia.SP_TChatbotWhatsAppAtcMensaje_Insertar que internamente
        /// ejecuta ope.SP_TWhatsAppMensajeClasificacion_Insertar e inserta en ia.T_ChatbotWhatsAppAtcMensaje.
        /// Retorna el Id generado en ia.T_ChatbotWhatsAppAtcMensaje.
        /// </summary>
        public async Task<long> InsertarMensajeChatbotCompleto(long idWhatsAppMensaje, int idHilo, int idActor, string usuario)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdWhatsAppMensaje",             idWhatsAppMensaje, DbType.Int64);
                parametros.Add("@IdWhatsAppMensajeTipo",         1,                 DbType.Int32);  // 1 = Mensaje Enviado ATC
                parametros.Add("@IdChatbotWhatsAppAtcHiloChat",  idHilo,            DbType.Int32);
                parametros.Add("@IdChatbotActor",                idActor,           DbType.Int32);
                parametros.Add("@Usuario",                       usuario,           DbType.String);

                return await conn.QueryFirstOrDefaultAsync<long>(
                    "ia.SP_TChatbotWhatsAppAtcMensaje_Insertar",
                    parametros,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Finaliza la conversacion activa de un alumno cambiando el estado a CERRADA_ASESOR (6).
        /// Busca el hilo por IdAlumno; si IdAlumno es 0 usa WaTo como fallback.
        /// Retorna true si se cerro al menos un hilo.
        /// </summary>
        public async Task<bool> FinalizarConversacion(int idAlumno, string waTo, string usuario)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var parametros = new DynamicParameters();
                parametros.Add("@IdAlumno", idAlumno, DbType.Int32);
                parametros.Add("@WaTo",     waTo,     DbType.String);
                parametros.Add("@Usuario",  usuario,  DbType.String);

                var filasAfectadas = await conn.QueryFirstOrDefaultAsync<int>(
                    "ia.SP_ChatbotWhatsAppAtcHiloChat_FinalizarConversacionPorIdAlumno",
                    parametros,
                    commandType: CommandType.StoredProcedure);

                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
