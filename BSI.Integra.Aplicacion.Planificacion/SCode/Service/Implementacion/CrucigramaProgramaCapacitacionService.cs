using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class CrucigramaProgramaCapacitacionService: ICrucigramaProgramaCapacitacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CrucigramaProgramaCapacitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<CrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacionDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        public CrucigramaProgramaCapacitacionCombosDTO ObtenerCombos()
        {
            try
            {
                CrucigramaProgramaCapacitacionCombosDTO respuesta = new CrucigramaProgramaCapacitacionCombosDTO()
                {
                    ListaPgeneral = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro(),
                    ListaTipoMarcador = _unitOfWork.MaterialAdicionalAulaVirtualRepository.ObtenerMarcadorCombo(),
                    ListaPespecifico = _unitOfWork.PEspecificoRepository.ObtenerComboSinValidacion()
                };
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de crucigramas para su exportación en excel
        /// </summary>
        /// <returns> Lista de objetos de tipo CrucigramaProgramaCapacitacionDetalleDTO </returns>
        public IEnumerable<ReporteExcelCrucigramasDTO> ObtenerReporteCrucigramasExportacionExcel()
        {
            try
            {
                return _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerReporteCrucigramasExportacionExcel().ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los crucigramas de programa de capacitacion registrados en el sistema
        /// </summary>
        /// <returns> Lista de objetos de tipo (CrucigramaProgramaCapacitacionDTO) </returns>
        public IEnumerable<CrucigramaProgramaCapacitacionRespuestaDTO> ObtenerCrucigramasRegistrados()
        {
            try
            {
                return _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerCrucigramasRegistrados().ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene respuestas asociadas a una pregunta
        /// </summary>
        /// <param name="idCrucigramaProgramaCapacitacion"></param>
        /// <returns></returns>
        public IEnumerable<CrucigramaProgramaCapacitacionDetalleDTO> ObtenerRespuestaCrucigramaProgramaCapacitacion(int idCrucigramaProgramaCapacitacion)
        {
            try
            {
                return _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerRespuestaCrucigramaProgramaCapacitacion(idCrucigramaProgramaCapacitacion).ToList();
            }
             catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina el crucigrama por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool EliminarCrucigrama(int id, string usuario)
        {
            try
            {
                var crucigrama = _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerPorId(id);
                if (crucigrama != null)
                {
                    var crucigramaDetalle = _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.ObtenerPorIdCrucigramaProgramaCapacitacionDetalle(crucigrama.Id).Select(x => x.Id);
                    _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Delete(crucigramaDetalle, usuario);
                    _unitOfWork.CrucigramaProgramaCapacitacionRepository.Delete(crucigrama.Id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                else 
                    return false;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina el crucigrama por medio de ids
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool EliminarCrucigramasSeleccionados(List<int> ids, string usuario)
        {
            try
            {
                if(ids.Count() > 0)
                {
                    foreach (var registro in ids)
                    {
                        var crucigrama = _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerPorId(registro);
                        if (crucigrama != null)
                        {
                            var crucigramaDetalle = _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.ObtenerPorIdCrucigramaProgramaCapacitacionDetalle(crucigrama.Id).Select(x => x.Id);
                            _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Delete(crucigramaDetalle, usuario);
                            _unitOfWork.CrucigramaProgramaCapacitacionRepository.Delete(crucigrama.Id, usuario);
                        }
                    }
                    _unitOfWork.Commit();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Importa xls, csv a Crucigramas
        /// </summary>
        /// <param name="files"></param>
        /// <returns> DTO - ImportarExcelRespuestaDTO </returns>
        public ImportarExcelRespuestaDTO ImportarExcel(IFormFile files)
        {
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                List<ImportacionCrucigramaProgramaCapacitacionDTO> listaExcel = new List<ImportacionCrucigramaProgramaCapacitacionDTO>();
                StreamReader sw = new StreamReader(files.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                var csvConfig = new CsvConfiguration(new System.Globalization.CultureInfo("es-ES"))
                {
                    Delimiter = ";",
                    MissingFieldFound = null
                };
                using (var cvs = new CsvReader(sw, csvConfig))
                {
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        ImportacionCrucigramaProgramaCapacitacionDTO crucigramaProgramaCapacitacion = new ImportacionCrucigramaProgramaCapacitacionDTO()
                        {
                            IdPGeneral = cvs.GetField<int>("IdPGeneral"),
                            IdPEspecifico = cvs.GetField<int>("IdPEspecifico"),
                            OrdenFilaCapitulo = cvs.GetField<int?>("OrdenFilaCapitulo"),
                            Sesion = cvs.GetField<string>("Sesion"),
                            SubSeccion = cvs.GetField<string>("Subsesion"),
                            IdTipoMarcador = cvs.GetField<int>("IdTipoMarcador"),
                            ValorMarcador = cvs.GetField<decimal>("ValorMarcador"),
                            CodigoCrucigrama = cvs.GetField<string>("CodigoCrucigrama"),
                            CantidadFila = cvs.GetField<int>("CantidadFila"),
                            CantidadColumna = cvs.GetField<int>("CantidadColumna"),

                            //============RESPUESTAS=============================
                            ColumnaInicio = cvs.GetField<int>("ColumnaInicio"),
                            FilaInicio = cvs.GetField<int>("FilaInicio"),
                            Definicion = cvs.GetField<string>("Definicion"),
                            Palabra = cvs.GetField<string>("Palabra"),
                            NumeroPalabra = cvs.GetField<int>("NumeroPalabra"),
                            Tipo = cvs.GetField<int>("Tipo")
                        };
                        listaExcel.Add(crucigramaProgramaCapacitacion);
                    }
                }
                var agrupado = listaExcel.GroupBy(x => new
                {
                    x.IdPGeneral,
                    x.IdPEspecifico,
                    x.OrdenFilaCapitulo,
                    x.Sesion,
                    x.SubSeccion,
                    x.IdTipoMarcador,
                    x.ValorMarcador,
                    x.CodigoCrucigrama,
                    x.CantidadFila,
                    x.CantidadColumna
                }).Select(x => new CrucigramaProgramaCapacitacionExcelCompuestoDTO
                {
                    CrucigramaProgramaCapacitacion = new CrucigramaProgramaCapacitacionAgrupadoDTO
                    {
                        IdPGeneral = x.Key.IdPGeneral,
                        IdPEspecifico = x.Key.IdPEspecifico,
                        OrdenFilaCapitulo = x.Key.OrdenFilaCapitulo,
                        Sesion = x.Key.Sesion,
                        SubSeccion = x.Key.SubSeccion,
                        IdTipoMarcador = x.Key.IdTipoMarcador,
                        ValorMarcador = x.Key.ValorMarcador,
                        CodigoCrucigrama = x.Key.CodigoCrucigrama,
                        CantidadFila = x.Key.CantidadFila,
                        CantidadColumna = x.Key.CantidadColumna,
                        CrucigramaProgramaCapacitacionDetalle = x.GroupBy(z => new { z.Tipo, z.NumeroPalabra, z.Palabra, z.Definicion, z.ColumnaInicio, z.FilaInicio }).Select(z => new CrucigramaProgramaCapacitacionDetalleDTO
                        {
                            Tipo = z.Key.Tipo,
                            NumeroPalabra = z.Key.NumeroPalabra,
                            Palabra = z.Key.Palabra,
                            Definicion = z.Key.Definicion,
                            ColumnaInicio = z.Key.ColumnaInicio,
                            FilaInicio = z.Key.FilaInicio,
                        }).ToList()
                    }
                }).ToList();
                IPreguntaProgramaCapacitacionService preguntaProgramaCapacitacionService = new PreguntaProgramaCapacitacionService(_unitOfWork);
                foreach (var item in agrupado)
                {
                    var listaCompuesta = preguntaProgramaCapacitacionService.ObtenerCapituloSesionProgramaCapacitacion(item.CrucigramaProgramaCapacitacion.IdPGeneral);
                    var listaCapitulos = listaCompuesta.Where(x => x.IdCapituloProgramaCapacitacion == item.CrucigramaProgramaCapacitacion.OrdenFilaCapitulo).FirstOrDefault();
                    int? ordenFilaSesion = null;
                    if (listaCapitulos != null)
                    {
                        var sesion = listaCapitulos.ListaSesionesProgramaCapacitacion.Where(x => x.SesionProgramaCapacitacion.ToLower().Equals(item.CrucigramaProgramaCapacitacion.Sesion.ToLower())).FirstOrDefault();

                        int idSesionTemporal = 0;

                        if (sesion != null)
                        {
                            idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                        }

                        if (!string.IsNullOrEmpty(item.CrucigramaProgramaCapacitacion.SubSeccion))
                        {
                            try
                            {
                                idSesionTemporal = sesion.ListaSubSeccionProgramaCapacitacion.FirstOrDefault(y => y.SubSeccionProgramaCapacitacion.ToLower().Equals(item.CrucigramaProgramaCapacitacion.SubSeccion.ToLower())).IdSesionProgramaCapacitacion;
                            }
                            catch (Exception e)
                            {
                                idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                            }
                        }

                        if (idSesionTemporal > 0)
                        {
                            ordenFilaSesion = idSesionTemporal;
                        }
                    }
                    try
                    {
                        indexTotal++;
                        CrucigramaProgramaCapacitacion crucigrama = new CrucigramaProgramaCapacitacion()
                        {
                            IdPgeneral = item.CrucigramaProgramaCapacitacion.IdPGeneral,
                            IdPespecifico = item.CrucigramaProgramaCapacitacion.IdPEspecifico,
                            OrdenFilaCapitulo = item.CrucigramaProgramaCapacitacion.OrdenFilaCapitulo,
                            OrdenFilaSesion = ordenFilaSesion,
                            IdTipoMarcador = item.CrucigramaProgramaCapacitacion.IdTipoMarcador,
                            ValorMarcador = item.CrucigramaProgramaCapacitacion.ValorMarcador,
                            CodigoCrucigrama = item.CrucigramaProgramaCapacitacion.CodigoCrucigrama,
                            CantidadFila = item.CrucigramaProgramaCapacitacion.CantidadFila,
                            CantidadColumna = item.CrucigramaProgramaCapacitacion.CantidadColumna,
                            Estado = true,
                            UsuarioCreacion = "ImportarExcel",
                            UsuarioModificacion = "ImportarExcel",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        var crucigramaProgramaCapacitacion = _unitOfWork.CrucigramaProgramaCapacitacionRepository.Add(crucigrama);
                        _unitOfWork.Commit();

                        foreach (var respuesta in item.CrucigramaProgramaCapacitacion.CrucigramaProgramaCapacitacionDetalle)
                        {
                            CrucigramaProgramaCapacitacionDetalle crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalle()
                            {
                                IdCrucigramaProgramaCapacitacionDetalle = crucigramaProgramaCapacitacion.Id,
                                ColumnaInicio = respuesta.ColumnaInicio,
                                FilaInicio = respuesta.FilaInicio,
                                NumeroPalabra = respuesta.NumeroPalabra,
                                Palabra = respuesta.Palabra,
                                Tipo = respuesta.Tipo,
                                Definicion = respuesta.Definicion,
                                Estado = true,
                                UsuarioCreacion = "ImportarExcel",
                                UsuarioModificacion = "ImportarExcel",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };
                            _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Add(crucigramaDetalle);
                            _unitOfWork.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        indexError++;
                        listaErrores.Add("Error - " + e.Message);
                    }
                }
                ImportarExcelRespuestaDTO resultadoImportarExcel = new()
                {
                    Total = indexTotal,
                    Correcto = (indexTotal - indexError),
                    Error = indexError,
                    Errores = listaErrores
                };
                return resultadoImportarExcel;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo crucigrama
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool InsertarCrucigrama(CompuestoCrucigramaProgramaCapacitacionDTO dto, string usuario)
        {
            try
            {
                CrucigramaProgramaCapacitacion crucigrama = new CrucigramaProgramaCapacitacion();

                crucigrama.IdPgeneral = dto.Crucigrama.IdPgeneral;
                crucigrama.IdPespecifico = dto.Crucigrama.IdPespecifico;
                crucigrama.OrdenFilaCapitulo = dto.Crucigrama.IdCapitulo;
                crucigrama.OrdenFilaSesion = dto.Crucigrama.IdSesion;
                crucigrama.IdTipoMarcador = dto.Crucigrama.IdTipoMarcador;
                crucigrama.ValorMarcador = dto.Crucigrama.ValorMarcador;
                crucigrama.CodigoCrucigrama = dto.Crucigrama.CodigoCrucigrama;
                crucigrama.CantidadFila = dto.Crucigrama.CantidadFila;
                crucigrama.CantidadColumna = dto.Crucigrama.CantidadColumna;
                crucigrama.Estado = true;
                crucigrama.UsuarioCreacion = usuario;
                crucigrama.UsuarioModificacion = usuario;
                crucigrama.FechaCreacion = DateTime.Now;
                crucigrama.FechaModificacion = DateTime.Now;

                var crucigramaInsertado = _unitOfWork.CrucigramaProgramaCapacitacionRepository.Add(crucigrama);
                _unitOfWork.Commit();

                foreach (var item in dto.CrucigramaDetalle)
                {
                    CrucigramaProgramaCapacitacionDetalle crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalle();

                    crucigramaDetalle.IdCrucigramaProgramaCapacitacionDetalle = crucigramaInsertado.Id;
                    crucigramaDetalle.ColumnaInicio = item.ColumnaInicio;
                    crucigramaDetalle.FilaInicio = item.FilaInicio;
                    crucigramaDetalle.NumeroPalabra = item.NumeroPalabra;
                    crucigramaDetalle.Palabra = item.Palabra;
                    crucigramaDetalle.Tipo = item.Tipo;
                    crucigramaDetalle.Definicion = item.Definicion;
                    crucigramaDetalle.Estado = true;
                    crucigramaDetalle.UsuarioCreacion = usuario;
                    crucigramaDetalle.UsuarioModificacion = usuario;
                    crucigramaDetalle.FechaCreacion = DateTime.Now;
                    crucigramaDetalle.FechaModificacion = DateTime.Now;

                    _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Add(crucigramaDetalle);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el crucigrama seleccionado
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool ActualizarCrucigrama(CompuestoCrucigramaProgramaCapacitacionDTO dto, string usuario)
        {
            try
            {
                var crucigrama = _unitOfWork.CrucigramaProgramaCapacitacionRepository.ObtenerPorId(dto.Crucigrama.Id);
                if (crucigrama != null)
                {
                    crucigrama.IdPgeneral = dto.Crucigrama.IdPgeneral;
                    crucigrama.IdPespecifico = dto.Crucigrama.IdPespecifico;
                    crucigrama.OrdenFilaCapitulo = dto.Crucigrama.IdCapitulo;
                    crucigrama.OrdenFilaSesion = dto.Crucigrama.IdSesion;
                    crucigrama.IdTipoMarcador = dto.Crucigrama.IdTipoMarcador;
                    crucigrama.ValorMarcador = dto.Crucigrama.ValorMarcador;
                    crucigrama.CodigoCrucigrama = dto.Crucigrama.CodigoCrucigrama;
                    crucigrama.CantidadFila = dto.Crucigrama.CantidadFila;
                    crucigrama.CantidadColumna = dto.Crucigrama.CantidadColumna;
                    crucigrama.UsuarioModificacion = usuario;
                    crucigrama.FechaModificacion = DateTime.Now;

                    _unitOfWork.CrucigramaProgramaCapacitacionRepository.Update(crucigrama);

                    var ids = _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.ObtenerPorIdCrucigramaProgramaCapacitacionDetalle(crucigrama.Id).
                        Where(x => !dto.CrucigramaDetalle.Any(y => y.Id == x.Id)).Select(x => x.Id).ToList();

                    _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Delete(ids, usuario);
                    _unitOfWork.Commit();

                    foreach (var item in dto.CrucigramaDetalle)
                    {
                        var crucigramaDetalle = _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.ObtenerPorId(item.Id);
                        if (crucigramaDetalle != null)
                        {
                            crucigramaDetalle.IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id;
                            crucigramaDetalle.ColumnaInicio = item.ColumnaInicio;
                            crucigramaDetalle.FilaInicio = item.FilaInicio;
                            crucigramaDetalle.NumeroPalabra = item.NumeroPalabra;
                            crucigramaDetalle.Palabra = item.Palabra;
                            crucigramaDetalle.Tipo = item.Tipo;
                            crucigramaDetalle.Definicion = item.Definicion;
                            crucigramaDetalle.UsuarioModificacion = usuario;
                            crucigramaDetalle.FechaModificacion = DateTime.Now;
                            _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Update(crucigramaDetalle);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            crucigramaDetalle = new CrucigramaProgramaCapacitacionDetalle()
                            {
                                IdCrucigramaProgramaCapacitacionDetalle = crucigrama.Id,
                                ColumnaInicio = item.ColumnaInicio,
                                FilaInicio = item.FilaInicio,
                                NumeroPalabra = item.NumeroPalabra,
                                Palabra = item.Palabra,
                                Tipo = item.Tipo,
                                Definicion = item.Definicion,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };
                            _unitOfWork.CrucigramaProgramaCapacitacionDetalleRepository.Add(crucigramaDetalle);
                            _unitOfWork.Commit();
                        }
                    }
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
