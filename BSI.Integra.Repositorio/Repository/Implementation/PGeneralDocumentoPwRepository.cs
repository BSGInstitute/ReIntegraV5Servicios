using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PGeneralDocumentoPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/08/2022
    /// <summary>
    /// Gestión general de T_PGeneralDocumentoPw
    /// </summary>
    public class PGeneralDocumentoPwRepository : GenericRepository<TPgeneralDocumentoPw>, IPGeneralDocumentoPwRepository
    {
        private Mapper _mapper;

        public PGeneralDocumentoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralDocumentoPw, PGeneralDocumentoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralDocumentoPw MapeoEntidad(PGeneralDocumentoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralDocumentoPw modelo = _mapper.Map<TPgeneralDocumentoPw>(entidad);

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

        public TPgeneralDocumentoPw Add(PGeneralDocumentoPw entidad)
        {
            try
            {
                var PGeneralDocumentoPw = MapeoEntidad(entidad);
                base.Insert(PGeneralDocumentoPw);
                return PGeneralDocumentoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralDocumentoPw Update(PGeneralDocumentoPw entidad)
        {
            try
            {
                var PGeneralDocumentoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralDocumentoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PGeneralDocumentoPw);
                return PGeneralDocumentoPw;
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


        public IEnumerable<TPgeneralDocumentoPw> Add(IEnumerable<PGeneralDocumentoPw> listadoEntidad)
        {
            try
            {
                List<TPgeneralDocumentoPw> listado = new List<TPgeneralDocumentoPw>();
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

        public IEnumerable<TPgeneralDocumentoPw> Update(IEnumerable<PGeneralDocumentoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralDocumentoPw> listado = new List<TPgeneralDocumentoPw>();
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
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PGeneralDocumentoPw.
        /// </summary>
        /// <returns> List<PGeneralDocumentoPwDTO> </returns>
        public IEnumerable<PGeneralDocumentoPwDTO> ObtenerPGeneralDocumentoPw()
        {
            try
            {
                List<PGeneralDocumentoPwDTO> rpta = new List<PGeneralDocumentoPwDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdDocumento,
	                    IdPGeneral,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_PGeneralDocumento_PW
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PGeneralDocumentoPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="tituloSeccion">Titulo de la Seccion</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public PGeneralDocumentoSeccionDTO ObtenerSeccionDocumentoPGeneral(int idPGeneral, string tituloSeccion)
        {
            try
            {
                PGeneralDocumentoSeccionDTO rpta = new PGeneralDocumentoSeccionDTO();
                var query = @"
                    SELECT
                        Titulo,
                        Contenido,
                        IdSeccionTipoDetalle_PW AS IdSeccionTipoDetallePw,
                        NumeroFila,
                        Cabecera,
                        PiePagina,
                        OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento
                    WHERE IdPGeneral = @idPGeneral AND Titulo = @tituloSeccion";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral, tituloSeccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PGeneralDocumentoSeccionDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_CategoriaOrigen por el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad: PGeneralDocumentoPw </returns>
        public PGeneralDocumentoPw ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdDocumento,
                                   IdPGeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralDocumento_PW
                            WHERE Id = @Id
                            AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<PGeneralDocumentoPw>(resultado)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PGeneralDocumentoPw por el FK IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> Entidad: DocumentoPw </returns>
        public List<PGeneralDocumentoPw> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdDocumento,
                                   IdPGeneral,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralDocumento_PW
                            WHERE IdPGeneral = @IdPGeneral
                            AND Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<List<PGeneralDocumentoPw>>(resultado)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerDocumentoProgramaGeneralTrabajosEvaluacion(int idPGeneral)
        {
            try
            {
                var _queryfiltrocapitulo = "Select Id,Nombre FROM pla.V_DocumentoProgramaGeneralTrabajosEvaluacion Where IdPGeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(SubfiltroCapitulo);

                return new List<ComboDTO>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public IEnumerable<PreEstructuraProgramaDTO> ObtenerPreConfigurarVideoPrograma(int idPGeneral)
        {

            string query = @"SELECT Id,
                                   IdConfigurarVideoPrograma,
                                   IdPGeneral IdPgeneral,
                                   Nombre,
                                   Titulo,
                                   Contenido,
                                   NombreTitulo,
                                   IdSeccionTipoDetalle_PW IdSeccionTipoDetallePw,
                                   NumeroFila,
                                   VideoId,
                                   Archivo,
                                   NroDiapositivas,
                                   ConImagenVideo,
                                   ImagenVideoNombre,
                                   ImagenVideoAncho,
                                   ImagenVideoAlto,
                                   ConImagenDiapositiva,
                                   ImagenDiapositivaNombre,
                                   ImagenDiapositivaAncho,
                                   ImagenDiapositivaAlto,
                                   ImagenVideoPosicionX,
                                   ImagenVideoPosicionY,
                                   ImagenDiapositivaPosicionX,
                                   ImagenDiapositivaPosicionY,
                                   Minuto,
                                   IdTipoVista,
                                   NroDiapositiva,
                                   ConLogoVideo,
                                   ConLogoDiapositiva,
                                   TotalSegundos,
                                   VideoIdBrightcove
                            FROM pla.V_ListadoEstructuraPrograma
                            WHERE IdPGeneral = @IdPGeneral
                            ORDER BY NumeroFila,
                                     IdSeccionTipoDetalle_PW;";
            string queryDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
            if (!string.IsNullOrEmpty(queryDB) && !queryDB.Contains("[]"))
                return JsonConvert.DeserializeObject<IEnumerable<PreEstructuraProgramaDTO>>(queryDB);
            else
                return new List<PreEstructuraProgramaDTO>();
        }
    }
}
