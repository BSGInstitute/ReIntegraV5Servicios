using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ChatIntegraHistorialAsesorRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Gestión del Historial de Chat Integra de Asesores
    /// </summary>
    public class ChatIntegraHistorialAsesorRepository : GenericRepository<TChatIntegraHistorialAsesor>, IChatIntegraHistorialAsesorRepository
    {
        private Mapper _mapper;

        public ChatIntegraHistorialAsesorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TChatIntegraHistorialAsesor, ChatIntegraHistorialAsesor>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        public List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatPorAsesor(int idPersonal)
        {
            try
            {
                List<ChatHistorialAsesorDTO> chatHistorialAsesor = new List<ChatHistorialAsesorDTO>();
                var query = string.Empty;
                query = @"SELECT IdInteraccionChat AS IdInteraccionChat, IdAlumno AS IdAlumno,NombreAlumno AS NombreAlumno,IdAsesor 
                      AS IdAsesor,FechaFin AS FechaFin,Ubicacion AS Ubicacion,Mensajes AS Mensajes,Chatsession AS Chatsession,IdPersonal 
                      AS IdPersonal FROM [com].[V_ObtenerHistorialChatsPorAsesor] WHERE  IdPersonal = @IdPersonal ORDER BY FechaFin DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    chatHistorialAsesor = JsonConvert.DeserializeObject<List<ChatHistorialAsesorDTO>>(resultado)!;
                }
                return chatHistorialAsesor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los chats de un asesor soporte
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto : List<ChatHistorialAsesor> </returns>
        public List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatsPorAlumno(int idAlumno)
        {
            List<ChatHistorialAsesorDTO> chatHistorialAsesorSoporte = new List<ChatHistorialAsesorDTO>();
            var query = string.Empty;
            query = @"SELECT 
                        IdInteraccionChat, IdAlumno ,NombreAlumno ,IdAsesor,FechaFin ,Ubicacion ,Mensajes ,Chatsession ,IdPersonal,Leido 
                    FROM 
                        [com].[V_ObtenerHistorialChatsPorAsesorSoporte] 
                    WHERE  
                        IdAlumno = @IdAlumno ORDER BY FechaFin DESC";
            var chatIntegraHistorialAsesorDB = _dapperRepository.QueryDapper(query, new { IdAlumno = idAlumno });
            chatHistorialAsesorSoporte = JsonConvert.DeserializeObject<List<ChatHistorialAsesorDTO>>(chatIntegraHistorialAsesorDB)!;
            return chatHistorialAsesorSoporte;
        }
    }
}
