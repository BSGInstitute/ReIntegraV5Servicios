using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentacionComercialPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_DocumentacionComercialPw
    /// </summary>
    public class DocumentacionComercialPwRepository : GenericRepository<TDocumentacionComercialPw>, IDocumentacionComercialPwRepository
    {
        private Mapper _mapper;

        public DocumentacionComercialPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentacionComercialPw, DocumentacionComercialPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentacionComercialPw MapeoEntidad(DocumentacionComercialPw entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentacionComercialPw modelo = _mapper.Map<TDocumentacionComercialPw>(entidad);

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

        public TDocumentacionComercialPw Add(DocumentacionComercialPw entidad)
        {
            try
            {
                var DocumentacionComercialPw = MapeoEntidad(entidad);
                base.Insert(DocumentacionComercialPw);
                return DocumentacionComercialPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentacionComercialPw Update(DocumentacionComercialPw entidad)
        {
            try
            {
                var DocumentacionComercialPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentacionComercialPw.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentacionComercialPw);
                return DocumentacionComercialPw;
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


        public IEnumerable<TDocumentacionComercialPw> Add(IEnumerable<DocumentacionComercialPw> listadoEntidad)
        {
            try
            {
                List<TDocumentacionComercialPw> listado = new List<TDocumentacionComercialPw>();
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

        public IEnumerable<TDocumentacionComercialPw> Update(IEnumerable<DocumentacionComercialPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentacionComercialPw> listado = new List<TDocumentacionComercialPw>();
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentacionComercialPw.
        /// </summary>
        /// <returns> List<DocumentacionComercialPwDTO> </returns>
        public IEnumerable<DocumentacionComercialPwDTO> ObtenerDocumentacionComercialPw()
        {
            try
            {
                List<DocumentacionComercialPwDTO> rpta = new List<DocumentacionComercialPwDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Titulo,
	                    Contenido,
	                    Tipo,
	                    Modalidad,
	                    IdPais,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_DocumentacionComercial_PW
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentacionComercialPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentacionComercialPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentacionComercialPwComboDTO> </returns>
        public IEnumerable<DocumentacionComercialPwComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentacionComercialPwComboDTO> rpta = new List<DocumentacionComercialPwComboDTO>();
                var query = @"SELECT Id,Titulo FROM pla.T_DocumentacionComercial_PW WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentacionComercialPwComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Contenido de DocumentacionComercialPw segun el Tipo de Documento, la modalidad del Programa y el Pais
        /// </summary>
        /// <param name="tipoDocumento">Tipo de Documento</param>
        /// <param name="modalidad">Modalidad del Programa</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerContenidoDocumentoComercial(string tipoDocumento, string modalidad, int idPais)
        {
            try
            {
                StringDTO contenido = new StringDTO();
                var query = @"
                    SELECT Contenido AS Valor
                    FROM pla.T_DocumentacionComercial_PW
                    WHERE Tipo = @tipoDocumento
	                    AND Modalidad = @modalidad
	                    AND IdPais = @idPais
	                    AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { tipoDocumento, modalidad, idPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    contenido = JsonConvert.DeserializeObject<StringDTO>(resultado);
                }
                return contenido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la el contenido de la Documentación Comercial
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="modalidad"></param>
        /// <param name="idPais"></param>
        /// <returns> DocumentoComercialContenidoDTO </returns>
        public DocumentoComercialContenidoDTO DocumentoComercialContenido(string tipoDocumento, string modalidad, int idPais)
        {
            try
            {
                string queryContenidoDocumento = @"SELECT 
                                                        Contenido 
                                                   FROM 
                                                        pla.V_TDocumentacionComercial_pw_Contenido 
                                                   WHERE 
                                                        Tipo = @TipoDocumento AND Modalidad = @Modalidad AND IdPais = @IdPais AND Estado = 1";
                var contenidoDocumento = _dapperRepository.FirstOrDefault(queryContenidoDocumento, new { TipoDocumento = tipoDocumento, Modalidad = modalidad, IdPais = idPais });
                return JsonConvert.DeserializeObject<DocumentoComercialContenidoDTO>(contenidoDocumento)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
   }
}
