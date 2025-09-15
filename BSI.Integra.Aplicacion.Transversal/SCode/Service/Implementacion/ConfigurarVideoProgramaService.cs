using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Repositorio: ConfigurarVideoProgramaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 01/09/2022
    /// <summary>
    /// Gestión general de T_ConfigurarVideoPrograma
    /// </summary>
    public class ConfigurarVideoProgramaService : IConfigurarVideoProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfigurarVideoProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfigurarVideoPrograma, ConfigurarVideoPrograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfigurarVideoProgramaDTO, ConfigurarVideoPrograma>(MemberList.None).ReverseMap();
                cfg.CreateMap<SesionConfigurarVideoDTO, SesionConfigurarVideo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la preconfiguracion de los videos segun el programa general y numero de fila
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Encuestas = PreEstructuraCapituloProgramaDTO</returns>
        public List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEncuestas(int idPGeneral, int numeroFila)
        {
            try
            {
                return _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaEncuestas(idPGeneral, numeroFila);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreEstructuraCapituloProgramaDTO> ObtenerPreConfigurarVideoProgramaEvaluaciones(int idPGeneral, int numeroFila)
        {
            try
            {
                return _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaEvaluaciones(idPGeneral, numeroFila);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfigurarSecuenciaVideo
        /// </summary>
        /// <param name="ArchivoExcel">IFormFile a importar de formato Excel</param> 
        /// <param name="usuario"> Usuario integrao autor de la modificacion </param> 
        /// <returns> Response 200 o 400, dependiendo del flujo </returns>
        public (int cantidadCorrecto, int cantidadIncorrecto) ImportarExcelConfigurarSecuenciaVideo(IFormFile ArchivoExcel, string usuario)
        {
            try
            {
                int cantidadCorrecto = 0;
                int cantidadIncorrecto = 0;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var paquete = new ExcelPackage(ArchivoExcel.OpenReadStream()))
                {
                    var worksheet = paquete.Workbook.Worksheets[0];

                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;
                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Programa", Columna = 0, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "ID del Programa", Columna = 1, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroCap", Columna = 2, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Capitulo", Columna = 3, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Sesion", Columna = 4, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Subsesion", Columna = 5, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Orden Fila", Columna = 6, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Video", Columna = 7, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Total segundos", Columna = 8, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Archivo de diapositiva", Columna = 9, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nro. de diapositivas", Columna = 10, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Habilitar sello en video", Columna = 11, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nombre de imagen - ImgVideo", Columna = 12, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Ancho (px) - ImgVideo", Columna = 13, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Alto (px) - ImgVideo", Columna = 14, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion X - ImgVideo", Columna = 15, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion Y - ImgVideo", Columna = 16, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Habilitar sello en diapositivas", Columna = 17, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nombre de imagen - ImgDiapositiva", Columna = 18, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Ancho (px) - ImgDiapositiva", Columna = 19, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Alto (px) - ImgDiapositiva", Columna = 20, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion X - ImgDiapositiva", Columna = 21, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Posicion Y - ImgDiapositiva", Columna = 22, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Brightcove", Columna = 23, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Vimeo", Columna = 24, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "ReproduccionVideo", Columna = 25, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "DescargaVideo", Columna = 26, FlagObligatorio = false });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    var idPGeneralExcel = Convert.ToInt32(valoresExcel[1, 1]);
                    List<ConfigurarVideoPrograma> listaConfigurarVideo = new List<ConfigurarVideoPrograma>();

                    var listaIdSeccionTipoDetalle_PW = _unitOfWork.SeccionTipoDetallePwRepository.ObtenerIdSeccionTipoDetallePorIdPGeneral(idPGeneralExcel);
                    //int idSubseccion = listaIdSeccionTipoDetalle_PW.FirstOrDefault(x => x.NombreTitulo.ToLower() == "subseccion").IdSeccionTipoDetallePw;
                    //int idSesion = listaIdSeccionTipoDetalle_PW.FirstOrDefault(x => x.NombreTitulo.ToLower() == "sesion").IdSeccionTipoDetallePw;
                    var seccionSub = listaIdSeccionTipoDetalle_PW.FirstOrDefault(x => x.NombreTitulo.Equals("subseccion", StringComparison.OrdinalIgnoreCase));
                    var seccionSesion = listaIdSeccionTipoDetalle_PW.FirstOrDefault(x => x.NombreTitulo.Equals("sesion", StringComparison.OrdinalIgnoreCase));

 
                    int idSubseccion = seccionSub?.IdSeccionTipoDetallePw ?? 0;
                    int idSesion = seccionSesion?.IdSeccionTipoDetallePw ?? 0;
                    if (idSubseccion == 0)
                    {
                        Console.WriteLine("No se encontró 'subseccion'.");
                    }

                    var listaConfigurarVideoExistente = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPorIdPGeneral(idPGeneralExcel).ToList();

                    bool existeSubsesion;

                    for (int i = inicio.Row; i < final.Row; i++)
                    {
                        try
                        {
                            var configurarVideoPrograma = new ConfigurarVideoPrograma();

                            configurarVideoPrograma.IdPgeneral = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "ID del Programa").Columna]);
                            existeSubsesion = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Subsesion").Columna] != null && valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Subsesion").Columna] != string.Empty;
                            configurarVideoPrograma.IdDocumentoSeccionPw = existeSubsesion ? idSubseccion : idSesion;
                            configurarVideoPrograma.VideoId = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Video").Columna].ToString();
                            configurarVideoPrograma.VideoIdBrightcove = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Brightcove").Columna].ToString();
                            configurarVideoPrograma.VideoIdVimeo = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Vimeo").Columna].ToString();
                            configurarVideoPrograma.ReproduccionVideo = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "ReproduccionVideo").Columna]);
                            configurarVideoPrograma.DescargaVideo = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "DescargaVideo").Columna]);
                            configurarVideoPrograma.TotalMinutos = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Total segundos").Columna].ToString();
                            configurarVideoPrograma.Archivo = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Archivo de diapositiva").Columna].ToString();
                            configurarVideoPrograma.NroDiapositivas = valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nro. de diapositivas").Columna].ToString();
                            configurarVideoPrograma.Configurado = true;

                            int altoPxImgVideo = 0, altoPyImgVideo = 0, posicionXImgVideo = 0, posicionYImgVideo = 0;
                            int altoPxImgDiapositiva = 0, altoPyImgDiapositiva = 0, posicionXImgDiapositiva = 0, posicionYImgDiapositiva = 0;

                            configurarVideoPrograma.ConImagenVideo = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Habilitar sello en video").Columna] ?? "no").ToString().ToLower().Trim() == "si" ? true : false;
                            configurarVideoPrograma.ImagenVideoNombre = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nombre de imagen - ImgVideo").Columna] ?? string.Empty).ToString();
                            configurarVideoPrograma.ImagenVideoAlto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Alto (px) - ImgVideo").Columna] ?? "0").ToString(), out altoPxImgVideo) ? altoPxImgVideo.ToString() : "0";
                            configurarVideoPrograma.ImagenVideoAncho = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Ancho (px) - ImgVideo").Columna] ?? "0").ToString(), out altoPyImgVideo) ? altoPyImgVideo.ToString() : "0";
                            configurarVideoPrograma.ImagenVideoPosicionX = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion X - ImgVideo").Columna] ?? 0).ToString(), out posicionXImgVideo) ? posicionXImgVideo : 0;
                            configurarVideoPrograma.ImagenVideoPosicionY = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion Y - ImgVideo").Columna] ?? "0").ToString(), out posicionYImgVideo) ? posicionYImgVideo : 0;

                            configurarVideoPrograma.ConImagenDiapositiva = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Habilitar sello en diapositivas").Columna] ?? "no").ToString().ToLower().Trim() == "si" ? true : false;
                            configurarVideoPrograma.ImagenDiapositivaNombre = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Nombre de imagen - ImgDiapositiva").Columna] ?? string.Empty).ToString();
                            configurarVideoPrograma.ImagenDiapositivaAlto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Alto (px) - ImgDiapositiva").Columna] ?? "0").ToString(), out altoPxImgDiapositiva) ? altoPxImgDiapositiva.ToString() : "0";
                            configurarVideoPrograma.ImagenDiapositivaAncho = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Ancho (px) - ImgDiapositiva").Columna] ?? "0").ToString(), out altoPyImgDiapositiva) ? altoPyImgDiapositiva.ToString() : "0";
                            configurarVideoPrograma.ImagenDiapositivaPosicionX = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion X - ImgDiapositiva").Columna] ?? "0").ToString(), out posicionXImgDiapositiva) ? posicionXImgDiapositiva : 0;
                            configurarVideoPrograma.ImagenDiapositivaPosicionY = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Posicion Y - ImgDiapositiva").Columna] ?? "0").ToString(), out posicionYImgDiapositiva) ? posicionYImgDiapositiva : 0;

                            configurarVideoPrograma.NumeroFila = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Orden Fila").Columna]);
                            configurarVideoPrograma.Estado = true;
                            configurarVideoPrograma.FechaCreacion = DateTime.Now;
                            configurarVideoPrograma.FechaModificacion = DateTime.Now;
                            configurarVideoPrograma.UsuarioCreacion = usuario;
                            configurarVideoPrograma.UsuarioModificacion = usuario;
                            configurarVideoPrograma.Activo = true;

                            listaConfigurarVideo.Add(configurarVideoPrograma);

                            cantidadCorrecto++;
                        }
                        catch (Exception e)
                        {
                            cantidadIncorrecto++;
                            continue;
                        }
                    }

                    if (listaConfigurarVideo.Count > 0)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            _unitOfWork.ConfigurarVideoProgramaRepository.EliminarConfiguracionVideo(idPGeneralExcel);
                            var nuevosId = _unitOfWork.ConfigurarVideoProgramaRepository.Add(listaConfigurarVideo);
                            _unitOfWork.Commit();

                            foreach (var item in nuevosId)
                            {
                                var idantiguo = listaConfigurarVideoExistente.Where(x => x.NumeroFila == item.NumeroFila).FirstOrDefault();
                                if (idantiguo != null)
                                {
                                    _unitOfWork.SesionConfigurarVideoRepository.ActualizarPadreSesionConfiguracionVideo(idantiguo.Id, item.Id);
                                }
                            }
                            scope.Complete();
                        }
                    }
                }
                return (cantidadCorrecto, cantidadIncorrecto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion del video programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<EstructuraCapituloProgramaAlternoDTO> </returns>
        public List<EstructuraCapituloProgramaAlternoDTO> ObtenerConfiguracionVideoPrograma(int idPGeneral)
        {
            try
            {
                var respuesta = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoPrograma(idPGeneral);
                var _listadoEstructura = (from x in respuesta
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();
                var estructuraCapituloProgramas = new List<EstructuraCapituloProgramaAlternoDTO>();
                foreach (var item in _listadoEstructura)
                {
                    var estructuraCapituloPrograma = new EstructuraCapituloProgramaAlternoDTO();
                    estructuraCapituloPrograma.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                                estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                                estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                break;
                            case "SubSeccion":
                                estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                                {
                                    estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                }
                                break;
                            default:
                                estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    estructuraCapituloProgramas.Add(estructuraCapituloPrograma);
                }

                var estructuraCapitulos = estructuraCapituloProgramas.OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                return (estructuraCapitulos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuracion del video programa Examen por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<EstructuraCapituloProgramaAlternoDTO> </returns>
        public List<EstructuraCapituloProgramaAlternoDTO> ObtenerConfiguracionExamenPrograma(int idPGeneral)
        {
            try
            {
                string _auxCapitulo = string.Empty;
                var preEstructuraCapituloProgramas = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoPrograma(idPGeneral);
                var _listadoEstructura = (from x in preEstructuraCapituloProgramas
                                          group x by x.NumeroFila into newGroup
                                          select newGroup).ToList();
                var estructuraCapituloProgramas = new List<EstructuraCapituloProgramaAlternoDTO>();
                foreach (var item in _listadoEstructura)
                {
                    EstructuraCapituloProgramaAlternoDTO objeto = new EstructuraCapituloProgramaAlternoDTO();
                    objeto.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                objeto.Nombre = itemRegistros.Nombre;
                                objeto.Capitulo = itemRegistros.Contenido;
                                objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                break;
                            case "Sesion":
                            case "SubSeccion":
                                break;
                            default:
                                objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    if (_auxCapitulo != objeto.Capitulo)
                    {
                        _auxCapitulo = objeto.Capitulo;
                        estructuraCapituloProgramas.Add(objeto);
                    }
                }
                var estructuraCapitulos = estructuraCapituloProgramas.OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                return (estructuraCapitulos);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el enuncuado pregunta por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param> 
        /// <returns> List<PreguntaPorProgramaDTO> </returns>
        public List<PreguntaPorProgramaDTO> ObtenerEnunciadoPreguntaPorIdPGeneral(int idPGeneral)
        {
            return _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerEnunciadoPreguntaPorIdPGeneral(idPGeneral).ToList();
        }
        /// Autor: Gilmer Qm
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerDocumentoProgramaGeneral(int idPGeneral)
        {
            return _unitOfWork.PGeneralDocumentoPwRepository.ObtenerDocumentoProgramaGeneralTrabajosEvaluacion(idPGeneral).ToList();
        }
        /// Autor: Gilmer Qm
        /// Fecha: 12/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> List<ComboDTO> </returns>
        public List<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConfigurarProyecto(int idPGeneral)
        {
            try
            {
                var videos = _unitOfWork.PGeneralRepository.ValidarPRogramaPadreParaProyectoAPlicacion(idPGeneral);
                if (videos)
                {
                    var respuesta = _unitOfWork.ConfigurarEvaluacionTrabajoRepository.ObtenerConDetallePorIdPGeneral(idPGeneral);
                    return (respuesta.ToList());
                }
                else
                {
                    throw new Exception($"El programa es hijo");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de DocumentoProgramaGeneralTrabajosEvaluacion 
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <param name="idDocumentoSeccionPw"> PK de T_DocumentoSeccion_PW </param>
        /// <param name="numeroFila"> Numero de fila </param>
        /// <returns> ConfigurarVideoProgramaDTO </returns>
        public ConfigurarVideoProgramaDTO ObtenerConfiguracionSesionPrograma(int idPGeneral, int idDocumentoSeccionPw, int numeroFila)
        {
            try
            {
                var configuracionVideoPrograma = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerConfigurarVideoPrograma(idPGeneral, idDocumentoSeccionPw, numeroFila);
                if (configuracionVideoPrograma != null && configuracionVideoPrograma.Id != 0)
                    configuracionVideoPrograma.SesionConfigurarVideos = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorIdConfigurarVideoPrograma(configuracionVideoPrograma.Id.Value);

                return configuracionVideoPrograma;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="seccion"> Orden del capitulo </param>
        /// <param name="fila"> Orden final de sesion </param>
        /// <summary>
        /// Obtiene pregunta programa estructura agrupado por filtros
        /// </summary>
        public List<GrupoPreguntaProgramaCapacitacionDTO> ObtenerConfiguracionPreguntasEstructura(int idPGeneral, int seccion, int fila)
        {
            try
            {
                return _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerConfiguracionGrupoPreguntasEstructura(idPGeneral, seccion, fila).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Eliminacion lógica Sesiones Configuracion Video segun Id Programa General
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>        
        /// <param name="usuario"> Usuario autor de Integra </param>        
        /// <returns> IntDTO </returns>
        public IntDTO EliminarConfiguracionPrograma(int idPGeneral, string usuario)
        {
            try
            {
                return _unitOfWork.SesionConfigurarVideoRepository.EliminarSesionesConfiguracionVideo(idPGeneral, usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaBO)</returns>
        public List<EstructuraCapituloProgramaDTO> ObtenerEstructuraCapituloProgramaPorIdPGeneral(int idPGeneral)
        {
            List<EstructuraCapituloProgramaDTO> lista = new List<EstructuraCapituloProgramaDTO>();

            var listadoVideosPrograma = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoPrograma(idPGeneral);

            var listadoEstructura = (from x in listadoVideosPrograma
                                     group x by x.NumeroFila into newGroup
                                     select newGroup).ToList();

            foreach (var item in listadoEstructura)
            {
                EstructuraCapituloProgramaDTO estructuraCapituloPrograma = new EstructuraCapituloProgramaDTO();
                estructuraCapituloPrograma.OrdenFila = item.Key;
                foreach (var itemRegistros in item)
                {
                    switch (itemRegistros.NombreTitulo)
                    {
                        case "Capitulo":
                            estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                            estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                            estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                            estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                            estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                            estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                            estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                            estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                            estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                            estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                            estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                            estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                            estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                            estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                            estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                            estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                            estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                            estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                            //2
                            estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                            estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                            estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                            estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                            estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                            estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                            estructuraCapituloPrograma.VideoIdBrightcove = itemRegistros.VideoIdBrightcove;
                            estructuraCapituloPrograma.VideoIdVimeo = itemRegistros.VideoIdVimeo;
                            estructuraCapituloPrograma.ReproduccionVideo = itemRegistros.ReproduccionVideo;
                            estructuraCapituloPrograma.DescargaVideo = itemRegistros.DescargaVideo;
                            break;
                        case "Sesion":
                            estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                            estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                            estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                            estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                            estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                            estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                            estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                            estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                            estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                            estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                            estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                            estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                            estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                            estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                            estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                            estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                            estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                            estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                            //2
                            estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                            estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                            estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                            estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                            estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                            estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                            estructuraCapituloPrograma.VideoIdBrightcove = itemRegistros.VideoIdBrightcove;
                            estructuraCapituloPrograma.VideoIdVimeo = itemRegistros.VideoIdVimeo;
                            estructuraCapituloPrograma.ReproduccionVideo = itemRegistros.ReproduccionVideo;
                            estructuraCapituloPrograma.DescargaVideo = itemRegistros.DescargaVideo;
                            break;
                        case "SubSeccion":
                            estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                            if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                            {
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                estructuraCapituloPrograma.VideoIdBrightcove = itemRegistros.VideoIdBrightcove;
                                estructuraCapituloPrograma.VideoIdVimeo = itemRegistros.VideoIdVimeo;
                                estructuraCapituloPrograma.ReproduccionVideo = itemRegistros.ReproduccionVideo;
                                estructuraCapituloPrograma.DescargaVideo = itemRegistros.DescargaVideo;
                            }
                            break;
                        default:
                            estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                            break;
                    }
                }
                lista.Add(estructuraCapituloPrograma);
            }

            return lista;
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 13/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Descarga la plantilla para llenar de ConfigurarSecuenciaVideo
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 (Archivo Excel) o 400, dependiendo del flujo</returns>
        /// <summary>
        /// Obtiene la plantilla para llenar y realizar la importacion de la seccion de Configurar Secuencia Video
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Archivo excel de la plantilla de configurar secuencia de video</returns>
        public byte[] ObtenerPlantillaExcelConfigurarSecuenciaVideo(int idPGeneral)
        {
            try
            {
                string pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral).Nombre;
                #region Campos Generados
                var listaCompletaProgramaSesionSubsesion = ObtenerEstructuraCapituloProgramaPorIdPGeneral(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();

                var camposGenerados = new List<CampoObligatorioDTO>();

                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "ID del Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "NroCap", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Capitulo", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Sesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Subsesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Orden Fila", FlagObligatorio = true });
                #endregion

                #region Campos Adicionales
                var camposAdicionales = new List<CampoObligatorioDTO>();

                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Id Video", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Total segundos", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Archivo de diapositiva", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nro. de diapositivas", FlagObligatorio = true });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Habilitar sello en video", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nombre de imagen - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Ancho (px) - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Alto (px) - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion X - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion Y - ImgVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Habilitar sello en diapositivas", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Nombre de imagen - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Ancho (px) - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Alto (px) - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion X - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Posicion Y - ImgDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Id Brightcove", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Id Vimeo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "ReproduccionVideo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "DescargaVideo", FlagObligatorio = false });
                #endregion

                #region Creacion Plantilla
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                MemoryStream memoryStreamPlantilla = new MemoryStream();

                using (var package = new ExcelPackage(memoryStreamPlantilla))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PlantillaConfigurarSecuenciaVideo");
                    var listaNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();

                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 10.5f;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                    Color colorCabeceraGenerado = Color.FromArgb(200, 200, 200);
                    Color colorCabeceraAdicional = Color.FromArgb(185, 200, 225);
                    Color colorCabeceraObligatoria = Color.FromArgb(225, 100, 100);

                    // Encabezado
                    int fila = 1, columna = 1;

                    foreach (var campo in camposGenerados)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(colorCabeceraGenerado);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    foreach (var campo in camposAdicionales)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(campo.FlagObligatorio ? colorCabeceraObligatoria : colorCabeceraAdicional);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    fila++;

                    foreach (var dato in listaCompletaProgramaSesionSubsesion)
                    {
                        columna = 1;
                        worksheet.Cells[fila, columna].Value = pGeneral;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.IdPgeneral;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.OrdenCapitulo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Capitulo ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Sesion ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.SubSesion ?? string.Empty;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.OrdenFila;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.VideoId;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.TotalSegundos;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.Archivo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.NroDiapositivas;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ConImagenVideo == true ? "si" : dato.ConImagenVideo == false ? "no" : "";
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoNombre;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoAncho;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoAlto;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoPosicionX;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenVideoPosicionY;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ConImagenDiapositiva == true ? "si" : dato.ConImagenDiapositiva == false ? "no" : "";
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaNombre;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaAncho;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaAlto;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaPosicionX;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ImagenDiapositivaPosicionY;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.VideoIdBrightcove;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.VideoIdVimeo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.ReproduccionVideo;
                        columna++;
                        worksheet.Cells[fila, columna].Value = dato.DescargaVideo;
                        fila++;
                    }

                    package.Save();
                }

                byte[] excel = memoryStreamPlantilla.ToArray();
                memoryStreamPlantilla.Close();
                #endregion

                return excel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <param name="idSeccion"> (PK) T_Seccion_PW </param>
        /// <param name="fila"> numero de fila </param>
        /// <summary>
        /// Obtiene el el registro con detalles por el IdPGeneral
        /// </summary>
        public List<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConfigurarEvaluacionTrabajoPorConfiguracion(int idPGeneral, int idSeccion, int fila)
        {
            return _unitOfWork.ConfigurarEvaluacionTrabajoRepository.ObtenerPorIdPGeneralIdSeccionFila(idPGeneral, idSeccion, fila).ToList();
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="configurarVideoProgramaDTO"> Nuevo objeto </param> 
        /// <summary>
        /// Realizar una insercion basica a la tabla y detalles
        /// </summary>
        public bool Insertar(ConfigurarVideoProgramaDTO configurarVideoProgramaDTO, string usuario)
        {
            var configurarVideoPrograma = new ConfigurarVideoPrograma();
            configurarVideoPrograma = _mapper.Map<ConfigurarVideoPrograma>(configurarVideoProgramaDTO);
            configurarVideoPrograma.Estado = true;
            configurarVideoPrograma.FechaCreacion = DateTime.Now;
            configurarVideoPrograma.FechaModificacion = DateTime.Now;
            configurarVideoPrograma.UsuarioCreacion = usuario;
            configurarVideoPrograma.UsuarioModificacion = usuario;

            configurarVideoPrograma.SesionConfigurarVideos = new List<SesionConfigurarVideo>();
            foreach (var item in configurarVideoProgramaDTO.SesionConfigurarVideos)
            {
                var sesionConfigurarVideo = new SesionConfigurarVideo();
                sesionConfigurarVideo = _mapper.Map<SesionConfigurarVideo>(item);
                sesionConfigurarVideo.Estado = true;
                sesionConfigurarVideo.FechaCreacion = DateTime.Now;
                sesionConfigurarVideo.FechaModificacion = DateTime.Now;
                sesionConfigurarVideo.UsuarioCreacion = usuario;
                sesionConfigurarVideo.UsuarioModificacion = usuario;
                configurarVideoPrograma.SesionConfigurarVideos.Add(sesionConfigurarVideo);
            }
            _unitOfWork.ConfigurarVideoProgramaRepository.Add(configurarVideoPrograma);
            _unitOfWork.Commit();
            return true;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/07/2023
        /// <param name="configurarVideoProgramaDTO"> Nuevo objeto </param> 
        /// <summary>
        /// Realizar una actualizacion basica a la tabla y detalles
        /// </summary>
        public bool Actualizar(ConfigurarVideoProgramaDTO configurarVideoProgramaDTO, string usuario)
        {
            ConfigurarVideoPrograma configurarVideoPrograma;

            if (configurarVideoProgramaDTO.Id != 0)
            {
                #region EliminacionDetalle 
                /*Eliminamos los detalles que ya no pertenecen al padre*/
                var sesionConfigurarVideos = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorIdConfigurarVideoPrograma(configurarVideoProgramaDTO.Id.Value);
                sesionConfigurarVideos.RemoveAll(x => configurarVideoProgramaDTO.SesionConfigurarVideos.Any(y => y.Id == x.Id));
                _unitOfWork.SesionConfigurarVideoRepository.Delete(sesionConfigurarVideos.Select(x => x.Id.Value), usuario);
                #endregion

                configurarVideoPrograma = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPorId(configurarVideoProgramaDTO.Id.Value);
                configurarVideoPrograma.IdPgeneral = configurarVideoProgramaDTO.IdPgeneral;
                configurarVideoPrograma.IdDocumentoSeccionPw = configurarVideoProgramaDTO.IdDocumentoSeccionPw;
                configurarVideoPrograma.VideoId = configurarVideoProgramaDTO.VideoId;
                configurarVideoPrograma.VideoIdBrightcove = configurarVideoProgramaDTO.VideoIdBrightcove;
                configurarVideoPrograma.VideoIdVimeo = configurarVideoProgramaDTO.VideoIdVimeo;
                configurarVideoPrograma.DescargaVideo = configurarVideoProgramaDTO.DescargaVideo;
                configurarVideoPrograma.ReproduccionVideo = configurarVideoProgramaDTO.ReproduccionVideo;
                configurarVideoPrograma.TotalMinutos = configurarVideoProgramaDTO.TotalMinutos;
                configurarVideoPrograma.Archivo = configurarVideoProgramaDTO.Archivo;
                configurarVideoPrograma.NroDiapositivas = configurarVideoProgramaDTO.NroDiapositivas;
                configurarVideoPrograma.Configurado = configurarVideoProgramaDTO.Configurado;
                configurarVideoPrograma.ConImagenVideo = configurarVideoProgramaDTO.ConImagenVideo;
                configurarVideoPrograma.ImagenVideoNombre = configurarVideoProgramaDTO.ImagenVideoNombre;
                configurarVideoPrograma.ImagenVideoAlto = configurarVideoProgramaDTO.ImagenVideoAlto;
                configurarVideoPrograma.ImagenVideoAncho = configurarVideoProgramaDTO.ImagenVideoAncho;
                configurarVideoPrograma.ImagenVideoPosicionX = configurarVideoProgramaDTO.ImagenVideoPosicionX;
                configurarVideoPrograma.ImagenVideoPosicionY = configurarVideoProgramaDTO.ImagenVideoPosicionY;
                configurarVideoPrograma.ConImagenDiapositiva = configurarVideoProgramaDTO.ConImagenDiapositiva;
                configurarVideoPrograma.ImagenDiapositivaNombre = configurarVideoProgramaDTO.ImagenDiapositivaNombre;
                configurarVideoPrograma.ImagenDiapositivaAlto = configurarVideoProgramaDTO.ImagenDiapositivaAlto;
                configurarVideoPrograma.ImagenDiapositivaAncho = configurarVideoProgramaDTO.ImagenDiapositivaAncho;
                configurarVideoPrograma.ImagenDiapositivaPosicionX = configurarVideoProgramaDTO.ImagenDiapositivaPosicionX;
                configurarVideoPrograma.ImagenDiapositivaPosicionY = configurarVideoProgramaDTO.ImagenDiapositivaPosicionY;
                configurarVideoPrograma.NumeroFila = configurarVideoProgramaDTO.NumeroFila;
                configurarVideoPrograma.FechaModificacion = DateTime.Now;
                configurarVideoPrograma.UsuarioModificacion = usuario;

                configurarVideoPrograma.SesionConfigurarVideos = new List<SesionConfigurarVideo>();
                foreach (var item in configurarVideoProgramaDTO.SesionConfigurarVideos)
                {
                    SesionConfigurarVideo sesionConfigurarVideo;
                    if (item.Id != 0)
                    {
                        sesionConfigurarVideo = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorId(item.Id.Value);
                        sesionConfigurarVideo.Minuto = item.Minuto;
                        sesionConfigurarVideo.IdTipoVista = item.IdTipoVista;
                        sesionConfigurarVideo.NroDiapositiva = item.NroDiapositiva;
                        sesionConfigurarVideo.IdEvaluacion = item.IdEvaluacion;
                        sesionConfigurarVideo.FechaModificacion = DateTime.Now;
                        sesionConfigurarVideo.UsuarioModificacion = usuario;
                        sesionConfigurarVideo.ConLogoVideo = item.ConLogoVideo;
                        sesionConfigurarVideo.ConLogoDiapositiva = item.ConLogoDiapositiva;
                    }
                    else
                    {
                        sesionConfigurarVideo = new SesionConfigurarVideo();
                        sesionConfigurarVideo.Minuto = item.Minuto;
                        sesionConfigurarVideo.IdTipoVista = item.IdTipoVista;
                        sesionConfigurarVideo.NroDiapositiva = item.NroDiapositiva;
                        sesionConfigurarVideo.IdEvaluacion = item.IdEvaluacion;
                        sesionConfigurarVideo.Estado = true;
                        sesionConfigurarVideo.FechaCreacion = DateTime.Now;
                        sesionConfigurarVideo.FechaModificacion = DateTime.Now;
                        sesionConfigurarVideo.UsuarioCreacion = usuario;
                        sesionConfigurarVideo.UsuarioModificacion = usuario;
                        sesionConfigurarVideo.ConLogoVideo = item.ConLogoVideo;
                        sesionConfigurarVideo.ConLogoDiapositiva = item.ConLogoDiapositiva;
                    }
                    configurarVideoPrograma.SesionConfigurarVideos.Add(sesionConfigurarVideo);
                }
                _unitOfWork.ConfigurarVideoProgramaRepository.Update(configurarVideoPrograma);
                _unitOfWork.Commit();
                return (true);
            }
            else return false;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/07/2023
        /// <param name="videoId"> VideoId de T_ConfigurarVideoPrograma </param> 
        /// <param name="usuario"> usuario integra </param> 
        /// <summary>
        /// Obtiene los registros de T_SesionConfigurarVideo asociados al ConfigurarVideoPrograma, esta a su ves asociados al VideoId
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarSesionConfigurarVideoPorVideoId(string videoId, string usuario)
        {
            var configurarVideo = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPorVideoId(videoId);
            if (configurarVideo.Count() >= 1)
            {
                foreach (var item in configurarVideo)
                {
                    var configurarPrograma = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorIdConfigurarVideoPrograma(item.Id);
                    if (configurarPrograma.Count >= 1)
                    {
                        _unitOfWork.SesionConfigurarVideoRepository.Delete(configurarPrograma.Select(x => x.Id.Value), usuario);
                        _unitOfWork.Commit();
                    }
                }
                return true;
            }
            else return false;

        }
        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaDTO)</returns>
        public List<EstructuraCapituloProgramaDTO> ObtenerEstructuraCapituloProgramaPorIdPGeneralDescarga(int idPGeneral)
        {
            List<EstructuraCapituloProgramaDTO> lista = new List<EstructuraCapituloProgramaDTO>();

            var listadoVideosPrograma = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaDescarga(idPGeneral);

            if (listadoVideosPrograma.Count() >= 1)
            {
                var listadoEstructura = (from x in listadoVideosPrograma
                                         group x by x.NumeroFila into newGroup
                                         select newGroup).ToList();

                foreach (var item in listadoEstructura)
                {
                    EstructuraCapituloProgramaDTO estructuraCapituloPrograma = new EstructuraCapituloProgramaDTO();
                    estructuraCapituloPrograma.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                                estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "Sesion":
                                estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                                estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                //2
                                estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "SubSeccion":
                                estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                                {
                                    estructuraCapituloPrograma.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    estructuraCapituloPrograma.IdDocumentoSeccionPw = itemRegistros.Id;
                                    estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                    estructuraCapituloPrograma.VideoId = itemRegistros.VideoId;
                                    estructuraCapituloPrograma.Archivo = itemRegistros.Archivo;
                                    estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                    estructuraCapituloPrograma.ConImagenVideo = itemRegistros.ConImagenVideo;
                                    estructuraCapituloPrograma.ImagenVideoNombre = itemRegistros.ImagenVideoNombre;
                                    estructuraCapituloPrograma.ImagenVideoAncho = itemRegistros.ImagenVideoAncho;
                                    estructuraCapituloPrograma.ImagenVideoAlto = itemRegistros.ImagenVideoAlto;
                                    estructuraCapituloPrograma.ConImagenDiapositiva = itemRegistros.ConImagenDiapositiva;
                                    estructuraCapituloPrograma.ImagenDiapositivaNombre = itemRegistros.ImagenDiapositivaNombre;
                                    estructuraCapituloPrograma.ImagenDiapositivaAncho = itemRegistros.ImagenDiapositivaAncho;
                                    estructuraCapituloPrograma.ImagenDiapositivaAlto = itemRegistros.ImagenDiapositivaAlto;
                                    estructuraCapituloPrograma.ImagenVideoPosicionX = itemRegistros.ImagenVideoPosicionX;
                                    estructuraCapituloPrograma.ImagenVideoPosicionY = itemRegistros.ImagenVideoPosicionY;
                                    estructuraCapituloPrograma.ImagenDiapositivaPosicionX = itemRegistros.ImagenDiapositivaPosicionX;
                                    estructuraCapituloPrograma.ImagenDiapositivaPosicionY = itemRegistros.ImagenDiapositivaPosicionY;
                                    //2
                                    estructuraCapituloPrograma.Minuto = itemRegistros.Minuto;
                                    estructuraCapituloPrograma.IdTipoVista = itemRegistros.IdTipoVista;
                                    estructuraCapituloPrograma.NroDiapositiva = itemRegistros.NroDiapositiva;
                                    estructuraCapituloPrograma.ConLogoVideo = itemRegistros.ConLogoVideo;
                                    estructuraCapituloPrograma.ConLogoDiapositiva = itemRegistros.ConLogoDiapositiva;
                                    estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                }
                                break;
                            default:
                                estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    //Comentado
                    lista.Add(estructuraCapituloPrograma);
                }
            }
            return lista;
        }
        /// <summary>
        /// Obtiene la estructura en formato de Lista de un programa general
        /// </summary>
        /// <param name="idPGeneral">ID del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto (EstructuraCapituloProgramaDTO)</returns>
        public List<EstructuraCapituloProgramaDTO> ObtenerEstructuraCapituloProgramaPorIdPGeneralDescargaSinDatos(int idPGeneral)
        {
            List<EstructuraCapituloProgramaDTO> lista = new List<EstructuraCapituloProgramaDTO>();

            var listadoVideosPrograma = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoProgramaDescargaSinDatos(idPGeneral);

            if (listadoVideosPrograma.Count() >= 1)
            {
                var listadoEstructura = (from x in listadoVideosPrograma
                                         group x by x.NumeroFila into newGroup
                                         select newGroup).ToList();

                foreach (var item in listadoEstructura)
                {
                    EstructuraCapituloProgramaDTO estructuraCapituloPrograma = new EstructuraCapituloProgramaDTO();
                    estructuraCapituloPrograma.OrdenFila = item.Key;
                    foreach (var itemRegistros in item)
                    {
                        switch (itemRegistros.NombreTitulo)
                        {
                            case "Capitulo":
                                estructuraCapituloPrograma.Nombre = itemRegistros.Nombre;
                                estructuraCapituloPrograma.Capitulo = itemRegistros.Contenido;
                                estructuraCapituloPrograma.IdPgeneral = itemRegistros.IdPgeneral;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                //2                                
                                break;
                            case "Sesion":
                                estructuraCapituloPrograma.Sesion = itemRegistros.Contenido;
                                estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                //2
                                estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                break;
                            case "SubSeccion":
                                estructuraCapituloPrograma.SubSesion = itemRegistros.Contenido;
                                if (!string.IsNullOrEmpty(estructuraCapituloPrograma.SubSesion))
                                {
                                    estructuraCapituloPrograma.TotalSegundos = itemRegistros.TotalSegundos;
                                    estructuraCapituloPrograma.NroDiapositivas = itemRegistros.NroDiapositivas;
                                    //2
                                    estructuraCapituloPrograma.IdConfigurarVideoPrograma = itemRegistros.IdConfigurarVideoPrograma;
                                }
                                break;
                            default:
                                estructuraCapituloPrograma.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                break;
                        }
                    }
                    //Comentado
                    lista.Add(estructuraCapituloPrograma);
                }
            }
            return lista;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/07/2023
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>  
        /// <summary>
        /// Obtiene los registros de T_SesionConfigurarVideo asociados al ConfigurarVideoPrograma, esta a su ves asociados al VideoId
        /// </summary>
        /// <returns> byte[] </returns>
        public byte[] ObtenerPlantillaExcelConfiguracionDeVideo(int idPGeneral)
        {
            try
            {
                string pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idPGeneral).Nombre;
                #region Campos Generados
                var listaCompletaProgramaSesionSubsesion = ObtenerEstructuraCapituloProgramaPorIdPGeneralDescarga(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();
                var listaCompletaProgramaSesionSubsesionv2 = ObtenerEstructuraCapituloProgramaPorIdPGeneralDescargaSinDatos(idPGeneral).OrderBy(c => c.OrdenCapitulo).ThenBy(c => c.OrdenFila).ToList();
                var configuracionPorIdPGeneral = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPorIdPGeneral(idPGeneral).Select(x => new ConfigurarVideoProgramaBasicoDTO { Id = x.Id, IdPGeneral = x.IdPgeneral, NumeroFila = x.NumeroFila.Value, NroDiapositivas = x.NroDiapositivas, TotalMinutos = x.TotalMinutos }).ToList();

                var camposGenerados = new List<CampoObligatorioDTO>();

                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "ID del Programa", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "NroCap", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Capitulo", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Sesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Subsesion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Orden Fila", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Id Configuracion", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Duracion segundos", FlagObligatorio = true });
                camposGenerados.Add(new CampoObligatorioDTO() { Campo = "Nro diapositivas", FlagObligatorio = true });
                #endregion

                #region Campos Adicionales
                var camposAdicionales = new List<CampoObligatorioDTO>();

                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Segundo", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Tipo Vista", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "NroDiapositiva", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Logo video", FlagObligatorio = false });
                camposAdicionales.Add(new CampoObligatorioDTO() { Campo = "Logo diapositiva", FlagObligatorio = false });
                #endregion

                #region Creacion Plantilla
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                MemoryStream memoryStreamPlantilla = new MemoryStream();

                using (var package = new ExcelPackage(memoryStreamPlantilla))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("PlantillaConfigurarSecuenciaVideo");
                    var listaNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();

                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells.Style.Font.Size = 10.5f;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                    Color colorCabeceraGenerado = Color.FromArgb(200, 200, 200);
                    Color colorCabeceraAdicional = Color.FromArgb(255, 230, 150);
                    Color colorCabeceraObligatoria = Color.FromArgb(225, 100, 100);

                    // Encabezado
                    int fila = 1, columna = 1;

                    foreach (var campo in camposGenerados)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(colorCabeceraGenerado);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    foreach (var campo in camposAdicionales)
                    {
                        worksheet.Cells[fila, columna].Value = campo.Campo;
                        worksheet.Cells[fila, columna].Style.Font.Bold = true;
                        worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[fila, columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[fila, columna].Style.Fill.BackgroundColor.SetColor(campo.FlagObligatorio ? colorCabeceraObligatoria : colorCabeceraAdicional);

                        listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        columna++;
                    }

                    fila++;
                    if (listaCompletaProgramaSesionSubsesion.Count >= listaCompletaProgramaSesionSubsesionv2.Count)
                    {
                        foreach (var dato in listaCompletaProgramaSesionSubsesion)
                        {
                            var configuracionIndividual = configuracionPorIdPGeneral.Where(x => x.IdPGeneral == dato.IdPgeneral && x.NumeroFila == dato.OrdenFila).FirstOrDefault();
                            var columnasAdicionalExcel = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorIdConfigurarVideoPrograma(dato.IdConfigurarVideoPrograma);

                            for (int i = 0; i < Convert.ToInt32(configuracionIndividual.NroDiapositivas); i++)
                            {
                                columna = 1;
                                worksheet.Cells[fila, columna].Value = pGeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.IdPgeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.OrdenCapitulo;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.Capitulo ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.Sesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.SubSesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = dato.OrdenFila;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.Id;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.TotalMinutos;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.NroDiapositivas;
                                columna++;
                                if(i < columnasAdicionalExcel.Count())
                                {
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].Minuto;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].IdTipoVista;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].NroDiapositiva;
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].ConLogoVideo == true ? "si" : columnasAdicionalExcel[i].ConLogoVideo == false ? "no" : "";
                                columna++;
                                worksheet.Cells[fila, columna].Value = columnasAdicionalExcel[i].ConLogoDiapositiva == true ? "si" : columnasAdicionalExcel[i].ConLogoDiapositiva == false ? "no" : "";
                                }
                                fila++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var temp in listaCompletaProgramaSesionSubsesionv2)
                        {

                            var configuracionIndividual = configuracionPorIdPGeneral.Where(x => x.IdPGeneral == temp.IdPgeneral && x.NumeroFila == temp.OrdenFila).FirstOrDefault();

                            for (int i = 0; i < Convert.ToInt32(configuracionIndividual.NroDiapositivas); i++)
                            {
                                columna = 1;
                                worksheet.Cells[fila, columna].Value = pGeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.IdPgeneral;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.OrdenCapitulo;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.Capitulo ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.Sesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.SubSesion ?? string.Empty;
                                columna++;
                                worksheet.Cells[fila, columna].Value = temp.OrdenFila;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.Id;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.TotalMinutos;
                                columna++;
                                worksheet.Cells[fila, columna].Value = configuracionIndividual.NroDiapositivas;
                                fila++;
                            }
                        }
                    }
                    package.Save();
                }
                byte[] excel = memoryStreamPlantilla.ToArray();
                memoryStreamPlantilla.Close();
                #endregion

                return excel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/07/2023
        /// <param name="archivoExcel"> Archivo Excel </param>   
        /// <param name="usuario"> usuario integra </param>   
        /// <summary>
        /// Importa el archivo Excel de la seccion ConfiguracionDeVideo
        /// </summary>
        /// <returns> int, int </returns>
        public (int cantidadCorrecto, int cantidadIncorrecto) ImportarExcel(IFormFile archivoExcel, string usuario)
        {
            try
            {
                int cantidadCorrecto = 0;
                int cantidadIncorrecto = 0;
                SesionConfigurarVideo SesionConfigurarVideo;
                var listaTipoVista = _unitOfWork.TipoVistumRepository.ObtenerCombo();
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (var paquete = new ExcelPackage(archivoExcel.OpenReadStream()))
                {

                    var worksheet = paquete.Workbook.Worksheets[0];

                    var inicio = worksheet.Dimension.Start;
                    var final = worksheet.Dimension.End;

                    #region Inicializacion Valores
                    List<CampoObligatorioCeldaDTO> listaValoresExcel = new List<CampoObligatorioCeldaDTO>();
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Programa", Columna = 0, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "ID del Programa", Columna = 1, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroCap", Columna = 2, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Capitulo", Columna = 3, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Sesion", Columna = 4, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Subsesion", Columna = 5, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Orden Fila", Columna = 6, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Id Configuracion", Columna = 7, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Duracion segundos", Columna = 8, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Nro. de diapositivas", Columna = 9, FlagObligatorio = true });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Segundo", Columna = 10, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Tipo Vista", Columna = 11, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "NroDiapositiva", Columna = 12, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Logo video", Columna = 13, FlagObligatorio = false });
                    listaValoresExcel.Add(new CampoObligatorioCeldaDTO { Campo = "Logo diapositiva", Columna = 14, FlagObligatorio = false });
                    #endregion

                    object[,] valoresExcel = worksheet.Cells.GetValue<object[,]>();
                    var idPGeneralExcel = Convert.ToInt32(valoresExcel[1, 1]);
                    List<SesionConfigurarVideo> listaSesionConfigurarVideo = new List<SesionConfigurarVideo>();
                    var listaSesionConfigurarVideoExistente = _unitOfWork.SesionConfigurarVideoRepository.ObtenerPorIdPGeneral(idPGeneralExcel);

                    for (int i = inicio.Row; i < final.Row; i++)
                    {
                        try
                        {
                            SesionConfigurarVideo = new SesionConfigurarVideo();

                            int minuto = 0, nroDiapositiva = 0;

                            SesionConfigurarVideo.IdConfigurarVideoPrograma = Convert.ToInt32(valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Id Configuracion").Columna]);
                            SesionConfigurarVideo.Minuto = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Segundo").Columna] ?? 0).ToString(), out minuto) ? minuto : 0;
                            SesionConfigurarVideo.IdTipoVista = listaTipoVista.First(x => x.Id.ToString() == (valoresExcel[i, listaValoresExcel.First(y => y.Campo == "Tipo Vista").Columna] ?? "video/diapositiva").ToString().ToLower().Trim()).Id;
                            SesionConfigurarVideo.NroDiapositiva = int.TryParse((valoresExcel[i, listaValoresExcel.First(x => x.Campo == "NroDiapositiva").Columna] ?? 0).ToString(), out nroDiapositiva) ? nroDiapositiva : 0;
                            SesionConfigurarVideo.ConLogoVideo = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Logo video").Columna] ?? "no").ToString().Trim().ToLower() == "si" ? true : false;
                            SesionConfigurarVideo.ConLogoDiapositiva = (valoresExcel[i, listaValoresExcel.First(x => x.Campo == "Logo diapositiva").Columna] ?? "no").ToString().Trim().ToLower() == "si" ? true : false;

                            SesionConfigurarVideo.Estado = true;
                            SesionConfigurarVideo.FechaCreacion = DateTime.Now;
                            SesionConfigurarVideo.FechaModificacion = DateTime.Now;
                            SesionConfigurarVideo.UsuarioCreacion = usuario;
                            SesionConfigurarVideo.UsuarioModificacion = usuario;

                            listaSesionConfigurarVideo.Add(SesionConfigurarVideo);
                            cantidadCorrecto++;
                        }
                        catch (Exception e)
                        {
                            cantidadIncorrecto++;
                            continue;
                        }
                    }

                    if (listaSesionConfigurarVideo.Count > 0)
                    {
                        //using (TransactionScope scope = new TransactionScope())
                        //{
                            var eliminarSesionVideo = _unitOfWork.SesionConfigurarVideoRepository.Delete(listaSesionConfigurarVideoExistente.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                            _unitOfWork.SesionConfigurarVideoRepository.Add(listaSesionConfigurarVideo);
                            _unitOfWork.Commit();
                          //  scope.Complete();
                        //}
                    }
                }
                return (cantidadCorrecto, cantidadIncorrecto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/08/2023 
        /// <summary>
        /// Obtiene combos para el modulo
        /// </summary>
        /// <returns> (List<EstructuraProgramaCapituloDTO> estructuraProgramaCapitulo, List<SesionSubSesionPreguntaInteractivaDTO> estructuraProgramaSesion) </returns>
        public (List<EstructuraProgramaCapituloDTO> estructuraProgramaCapitulo, List<SesionSubSesionPreguntaInteractivaDTO> estructuraProgramaSesion) ObtenerCombosModulo()
        {
            try
            { 
                var estructuraProgramaCapitulo = _unitOfWork.DocumentoSeccionPwRepository.ObtenerCapituloPrograma().ToList();
                var estructuraProgramaSesion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerEstructuraProgramaSesion().ToList();
                return  (estructuraProgramaCapitulo, estructuraProgramaSesion);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool ActualizarDescargaReproduccionVideo(ActualizarDescargaReproduccionDTO dto, string usuario)
        {
            try
            {
                
                var respuesta = _unitOfWork.SesionConfigurarVideoRepository.ActualizarDescargaReproduccionVideo(dto, usuario);
                return respuesta;
                  
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConfigurarConteodeVideosPorTipo ObtenerConteosdeVideosTipo(int idPGeneral) 
        {
            return _unitOfWork.SesionConfigurarVideoRepository.ObtenerConteosdeVideosTipo(idPGeneral);
        }
    }
}

