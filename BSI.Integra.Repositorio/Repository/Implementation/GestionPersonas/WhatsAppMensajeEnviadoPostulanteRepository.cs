using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class WhatsAppMensajeEnviadoPostulanteRepository : GenericRepository<TWhatsAppMensajeEnviadoPostulante>, IWhatsAppMensajeEnviadoPostulanteRepository
    {
        private Mapper _mapper;
        public WhatsAppMensajeEnviadoPostulanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<WhatsAppMensajeEnviadoPostulante, WhatsAppMensajeEnviadoPostulanteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<WhatsAppMensajeEnviadoPostulante, TWhatsAppMensajeEnviadoPostulante>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppMensajeEnviadoPostulante MapeoEntidad(WhatsAppMensajeEnviadoPostulante entidad)
        {
            try
            {
                TWhatsAppMensajeEnviadoPostulante modelo = _mapper.Map<TWhatsAppMensajeEnviadoPostulante>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TWhatsAppMensajeEnviadoPostulante Add(WhatsAppMensajeEnviadoPostulante entidad)
        {
            try
            {
                var WhatsAppMensajeEnviadoPostulante = MapeoEntidad(entidad);
                base.Insert(WhatsAppMensajeEnviadoPostulante);
                return WhatsAppMensajeEnviadoPostulante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TWhatsAppMensajeEnviadoPostulante Update(WhatsAppMensajeEnviadoPostulante entidad)
        {
            try
            {
                var WhatsAppMensajeEnviadoPostulante = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppMensajeEnviadoPostulante.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppMensajeEnviadoPostulante);
                return WhatsAppMensajeEnviadoPostulante;
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
        public IEnumerable<TWhatsAppMensajeEnviadoPostulante> Add(IEnumerable<WhatsAppMensajeEnviadoPostulante> listadoEntidad)
        {
            try
            {
                List<TWhatsAppMensajeEnviadoPostulante> listado = new List<TWhatsAppMensajeEnviadoPostulante>();
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
        public IEnumerable<TWhatsAppMensajeEnviadoPostulante> Update(IEnumerable<WhatsAppMensajeEnviadoPostulante> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppMensajeEnviadoPostulante> listado = new List<TWhatsAppMensajeEnviadoPostulante>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_WhatsAppMensajeEnviadoPostulante por el Primary Key
        /// </summary>
        /// <returns>WhatsAppMensajeEnviadoPostulante o Nulo</returns>
        public WhatsAppMensajeEnviadoPostulante? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
		                WaTo,
		                WaId,
		                WaType,
		                WaTypeMensaje,
		                WaRecipientType,
		                WaBody,
		                WaFile,
		                WaFileName,
		                WaMimeType,
		                WaSha256,
		                WaLink,
		                WaCaption,
		                IdPais,
		                EsMigracion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdPersonal,
		                IdPostulante
                    FROM gp.T_WhatsAppMensajeEnviadoPostulante
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<WhatsAppMensajeEnviadoPostulante>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 05/12/2024
        /// <summary>
        /// Obtiene historial de chats de un postulante
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="Numero"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public List<WhatsAppHistorialMensajesPostulanteDTO> ListaHistorialMensajeChatPostulante(int idPersonal, string Numero, string Area)
        {
            try
            {
                List<WhatsAppHistorialMensajesPostulanteDTO> listaMensajes = new List<WhatsAppHistorialMensajesPostulanteDTO>();
                var _query = string.Empty;

                if (idPersonal == 0)
                {
                    _query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdPostulante,0) IdPostulante,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM gp.V_HistorialChatWhatsAppPostulante WHERE Numero=@Numero Order by FechaCreacion Asc";
                }
                else
                {
                    _query = "SELECT Numero, Tipo, Mensaje, IdPersonal, ISNULL(IdPostulante,0) IdPostulante,IdPais, Registro, FechaCreacion, NombrePersonal " +
                         "FROM gp.V_HistorialChatWhatsAppPostulante WHERE IdPersonal=@idPersonal AND Numero=@Numero Order by FechaCreacion Asc";

                }

                var rpta = _dapperRepository.QueryDapper(_query, new { idPersonal, Numero });
                listaMensajes = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesPostulanteDTO>>(rpta);
                return listaMensajes;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 23/10/2023
        /// <summary>
        /// Valida plantillas de mensajes enviados.
        /// </summary>
        /// <param name="plantilla"> Tipo de plantilla </param>
        /// <param name="numero"> Numero de WhatsApp </param>
        /// <returns> Bool </returns>
        public bool ValidarUltimaPlantillaEnviada(string plantilla, string numero)
        {
            try
            {
                // Query SQL
                string _query = @"
                            SELECT TOP 1 Id 
                            FROM gp.T_WhatsAppMensajeEnviadoPostulante 
                            WHERE WaType = 'hsm' 
                                AND WaBody = @Plantilla 
                                AND WaTo = @Numero 
                                AND FechaCreacion > DATEADD(HOUR, -24, GETDATE())";

                var result = _dapperRepository.FirstOrDefault(
                    _query,
                    new { Plantilla = plantilla, Numero = numero }
                );
                if (result == "null")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar la última plantilla enviada: " + ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 20/12/2023
        /// <summary>
        /// Registra en el log de mensajes de whastapp
        /// </summary>
        /// <param name="IdPostulante"> id del postulante </param>
        /// <param name="Numero"> Numero del postulante </param>
        /// <param name="Mensaje"> Mensaje </param>
        /// <returns> Bool </returns>
        public bool InsertarMensajesLogJsonEnvios(int? IdPostulante, string Numero, string Mensaje)
        {
            try
            {
                var _resultado = new IntDTO();
                //Mensaje = Mensaje.Replace("#", "").Replace("%","");
                var resultado = _dapperRepository.QuerySPDapper("[gp].[SP_InsertarWhatsappPostulanteEnvioLog]", new { IdPostulante, Numero, Mensaje });
                //_resultado = JsonConvert.DeserializeObject<IntDTO>(resultado);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
