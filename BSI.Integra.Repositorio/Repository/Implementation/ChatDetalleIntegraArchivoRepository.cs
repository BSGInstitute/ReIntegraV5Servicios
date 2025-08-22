using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ChatDetalleIntegraArchivoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_ChatDetalleIntegraArchivo
    /// </summary>
    public class ChatDetalleIntegraArchivoRepository : GenericRepository<TChatDetalleIntegraArchivo>, IChatDetalleIntegraArchivoRepository
    {
        private Mapper _mapper;

        public ChatDetalleIntegraArchivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TChatDetalleIntegraArchivo, ChatDetalleIntegraArchivo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TChatDetalleIntegraArchivo MapeoEntidad(ChatDetalleIntegraArchivo entidad)
        {
            try
            {
                //crea la entidad padre
                TChatDetalleIntegraArchivo modelo = _mapper.Map<TChatDetalleIntegraArchivo>(entidad);

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

        public TChatDetalleIntegraArchivo Add(ChatDetalleIntegraArchivo entidad)
        {
            try
            {
                var ChatDetalleIntegraArchivo = MapeoEntidad(entidad);
                base.Insert(ChatDetalleIntegraArchivo);
                return ChatDetalleIntegraArchivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TChatDetalleIntegraArchivo Update(ChatDetalleIntegraArchivo entidad)
        {
            try
            {
                var ChatDetalleIntegraArchivo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ChatDetalleIntegraArchivo.RowVersion = entidadExistente.RowVersion;

                base.Update(ChatDetalleIntegraArchivo);
                return ChatDetalleIntegraArchivo;
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


        public IEnumerable<TChatDetalleIntegraArchivo> Add(IEnumerable<ChatDetalleIntegraArchivo> listadoEntidad)
        {
            try
            {
                List<TChatDetalleIntegraArchivo> listado = new List<TChatDetalleIntegraArchivo>();
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

        public IEnumerable<TChatDetalleIntegraArchivo> Update(IEnumerable<ChatDetalleIntegraArchivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TChatDetalleIntegraArchivo> listado = new List<TChatDetalleIntegraArchivo>();
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
        /// Obtiene todos los registros de T_ChatDetalleIntegraArchivo.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraArchivoDTO> </returns>
        public IEnumerable<ChatDetalleIntegraArchivoDTO> ObtenerChatDetalleIntegraArchivo()
        {
            try
            {
                List<ChatDetalleIntegraArchivoDTO> rpta = new List<ChatDetalleIntegraArchivoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NombreArchivo,
	                    RutaArchivo,
	                    MimeType,
	                    EsImagen,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_ChatDetalleIntegraArchivo
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatDetalleIntegraArchivoDTO>>(resultado);
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
        /// Obtiene registros de T_ChatDetalleIntegraArchivo para mostrarse en combo.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraArchivoComboDTO> </returns>
        public IEnumerable<ChatDetalleIntegraArchivoComboDTO> ObtenerCombo()
        {
            try
            {
                List<ChatDetalleIntegraArchivoComboDTO> rpta = new List<ChatDetalleIntegraArchivoComboDTO>();
                var query = @"SELECT Id,NombreArchivo FROM com.T_ChatDetalleIntegraArchivo WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatDetalleIntegraArchivoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
