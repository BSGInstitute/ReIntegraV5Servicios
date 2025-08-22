using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2013.Excel;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class GestionRemuneracionPuestoTrabajoService : IGestionRemuneracionPuestoTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public GestionRemuneracionPuestoTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<GestionRemuneracionPuestoTrabajoDTO, PuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PuestoTrabajoRemuneracionDetalleDTO, PuestoTrabajoRemuneracionDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PuestoTrabajo, TPuestoTrabajo>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// <summary>
        /// Criterio Evaluacion Proceso
        /// </summary>
        /// <returns> Lista GestionRemuneracionPuestoTrabajoDTO </returns>
        public IEnumerable<GestionRemuneracionPuestoTrabajoDTO> Obtener()
        {
            var listaCompleta = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Obtener();
            foreach (var item in listaCompleta)
            {
                var detallesMapeados = _mapper.Map<IEnumerable<PuestoTrabajoRemuneracionDetalle>>
                                        (_unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPorIdPuestoTrabajo(item.Id));
                item.ListaPuestoTrabajoRemuneracionDetalle = detallesMapeados.Select(detalle => new PuestoTrabajoRemuneracionDetalleDTO
                {
                    Id = detalle.Id,
                    IdRemuneracion = detalle.IdRemuneracionTipo,
                    IdTipoRemuneracion = detalle.IdRemuneracionTipoCobro,
                    IdClaseRemuneracion = detalle.IdRemuneracionFormaCobro,
                    IdPeriodoRemuneracion = detalle.IdRemuneracionPeriodoCobro,
                    IdRemuneracionFormaCobro = detalle.IdRemuneracionFormaCobro,
                    IdPuestoTrabajoRemuneracion = detalle.IdPuestoTrabajoRemuneracion,
                    PorcentajeTasa = detalle.PorcentajeTasa,
                    IdMoneda = detalle.IdMonedaMontoFijo,
                    DescripcionEquipo = detalle.DescripcionEquipo,
                    Tasa = detalle.EsTasa,
                    Monto = detalle.MontoFijo,
                    TieneCondicion = detalle.TieneCondicion,
                    IdDescripcionMonetaria = detalle.IdRemuneracionDescripcionMonetaria,
                    ValorMinimo = detalle.RangoValorMinimo,
                    ValorMaximo = detalle.RangoValorMaximo,
                    IdMonedaValorVariable = detalle.IdMonedaRangoValor,
                    IngresoMensual = detalle.IngresoMensual,
                    Estado = detalle.Estado,
                }).ToList();
                //item.ListaPuestoTrabajoRemuneracionDetalle = _mapper.Map<IEnumerable<PuestoTrabajoRemuneracionDetalleDTO>>(_unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPorIdPuestoTrabajo(item.Id));
            }
            return listaCompleta;
        }

        /// 
        /// Autor: Sergio Yepez Pillco
        /// Fecha 2024-12-23
        /// <summary>
        /// Función que trae data para llenar los combos Area, Puesto Trabajo, Pais y Categoria
        /// </summary>
        /// <returns>Retorma una lista</returns>
        public ObtenerDataComboPuestoTrabajoDTO ObtenerCombosModulo()
        {

            try
            {
                var comboDataCombo = new ObtenerDataComboPuestoTrabajoDTO();
                comboDataCombo.ObtenerArea = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerComboAsync().Result.ToList();
                comboDataCombo.ObtenerPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerCategoria = _unitOfWork.TableroComercialCategoriaAsesorRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerPais = _unitOfWork.PaisRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerRemuneracion = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerRemuneracion();
                comboDataCombo.ObtenerTipoRemuneracion = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerTipoRemuneracion();
                comboDataCombo.ObtenerClaseRemuneracion = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerClaseRemuneracion();
                comboDataCombo.ObtenerPeriodoRemuneracion = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPeriodoRemuneracion();
                comboDataCombo.ObtenerMoneda = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerMonedaParaTableroComercial();
                comboDataCombo.ObtenerDescripcionMonetaria = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerDescripcionMonetaria();
                return comboDataCombo;
            }
            catch
            {
                throw;
            }

        }

        public List<PuestoTrabajoRemuneracionDetalleDTO> ObtenerPuestoTrabajoRemuneracionVariableRegistrado(int IdPuestoTrabajoRemuneracion)
        {
            try
            {
                List<PuestoTrabajoRemuneracionDetalleDTO> listaRemuneracionVariable = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPuestoTrabajoRemuneracionVariableRegistrado(IdPuestoTrabajoRemuneracion);

                return listaRemuneracionVariable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo CategoriaPregunta
        /// </summary>
        /// <param name="dto">CategoriaPreguntaDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>CategoriaPreguntaDTO</returns>
        public GestionRemuneracionPuestoTrabajoDTO Insertar(GestionRemuneracionPuestoTrabajoDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    foreach (var item in dto.ListaPuestoTrabajoRemuneracionDetalle)
                    {
                        if ( item.IdTipoRemuneracion == 0 || item.IdClaseRemuneracion == 0 || item.IdPeriodoRemuneracion == 0 )
                        {
                            throw new Exception("Debe ingresar los campos requeridos en el detalle de remuneración");
                        }
                    }
                    if (dto != null)
                    {
                        PuestoTrabajoRemuneracion entidad = new()
                        {
                            IdPuestoTrabajo = dto.IdPuestoTrabajo,
                            IdPersonalAreaTrabajo = dto.IdPersonalAreaTrabajo,
                            IdPais = dto.IdPais,
                            IdTableroComercialCategoriaAsesor = dto.IdCategoria,
                            Estado = true,
                            UsuarioCreacion = usuario ?? throw new ArgumentNullException(nameof(usuario), "El usuario de creación no puede ser nulo."),
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            TPuestoTrabajoRemuneracionDetalles = dto.ListaPuestoTrabajoRemuneracionDetalle.Select(detalle => new PuestoTrabajoRemuneracionDetalle
                            {
                                IdRemuneracionTipo = detalle.IdTipoRemuneracion.Value,
                                IdRemuneracionTipoCobro = detalle.IdClaseRemuneracion.Value,
                                IdRemuneracionFormaCobro = 1, 
                                IdPuestoTrabajoRemuneracion = detalle.IdPuestoTrabajoRemuneracion ?? 0,
                                IdRemuneracionPeriodoCobro = detalle.IdPeriodoRemuneracion.Value,
                                EsTasa = detalle.Tasa ?? false, 
                                MontoFijo = detalle.Monto, 
                                IdMonedaMontoFijo = detalle.IdMoneda ?? 0, 
                                PorcentajeTasa = detalle.PorcentajeTasa, 
                                DescripcionEquipo = detalle.DescripcionEquipo,
                                TieneCondicion = false, 
                                IdRemuneracionDescripcionMonetaria = detalle.IdDescripcionMonetaria, 
                                RangoValorMinimo = detalle.ValorMinimo, 
                                RangoValorMaximo = detalle.ValorMaximo, 
                                IdMonedaRangoValor = detalle.IdMonedaValorVariable, 
                                IngresoMensual = detalle.IngresoMensual, 
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            }).ToList()
                        };
                        //var respuesta = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Add(entidad);
                        //_unitOfWork.Commit();
                        var retorno = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Add(entidad);
                        _unitOfWork.Commit();
                        scope.Complete();
                        //var resultado = _mapper.Map<GestionRemuneracionPuestoTrabajoDTO>(respuesta);
                    }
                    else
                        throw new BadRequestException("Entidad Nula");

                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Metodo Actualizar
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="GestionRemuneracionPuestoTrabajoDTO"> parametros de la nueva CriterioEvaluacionProceso y sus detalles </param>

        public GestionRemuneracionPuestoTrabajoDTO Actualizar(GestionRemuneracionPuestoTrabajoDTO dto, string usuario)
        {
            try
            {
                PuestoTrabajoRemuneracion puestoTrabajo = new PuestoTrabajoRemuneracion();
                
                if (_unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Exist(dto.Id))
                {
                    puestoTrabajo = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPorId(dto.Id);
                    puestoTrabajo.IdPuestoTrabajo = dto.IdPuestoTrabajo;
                    puestoTrabajo.IdTableroComercialCategoriaAsesor = dto.IdCategoria;
                    puestoTrabajo.FechaCreacion = DateTime.Now;
                    puestoTrabajo.UsuarioCreacion = usuario;
                    puestoTrabajo.FechaModificacion = DateTime.Now;
                    puestoTrabajo.UsuarioModificacion = usuario;
                    puestoTrabajo.IdPais = dto.IdPais;
                    puestoTrabajo.IdPersonalAreaTrabajo = dto.IdPersonalAreaTrabajo;


                    var listadoExistente = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerDetallePuestoTrabajoRemuneracionPorId(dto.Id);

                    List<int> listadoIdExistente = new List<int>();
                    var listadoEliminar = new List<int>();

                    //añade la lista de detalles
                    if (dto.ListaPuestoTrabajoRemuneracionDetalle != null && dto.ListaPuestoTrabajoRemuneracionDetalle.ToList().Count > 0)
                    {
                        puestoTrabajo.TPuestoTrabajoRemuneracionDetalles ??= new List<PuestoTrabajoRemuneracionDetalle>();
                        listadoIdExistente = listadoExistente.Select(s => s.Id).ToList();

                        var listadoNuevo = new List<PuestoTrabajoRemuneracionDetalle>();
                        var listadoActualizar = new List<PuestoTrabajoRemuneracionDetalle>();

                        listadoNuevo.AddRange(dto.ListaPuestoTrabajoRemuneracionDetalle.Where(w => w.Id == null || w.Id == 0).Select(s =>
                            new PuestoTrabajoRemuneracionDetalle()
                            {
                                IdRemuneracionTipo = s.IdRemuneracion ?? 0,
                                IdRemuneracionTipoCobro= s.IdTipoRemuneracion.Value,
                                IdRemuneracionPeriodoCobro = s.IdPeriodoRemuneracion.Value,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdRemuneracionFormaCobro = s.IdClaseRemuneracion.Value,
                                IdPuestoTrabajoRemuneracion = s.IdPuestoTrabajoRemuneracion ?? 0,
                                EsTasa = s.Tasa ?? false,
                                MontoFijo = s.Monto,
                                IdMonedaMontoFijo = s.IdMoneda ?? 0,
                                PorcentajeTasa = s.PorcentajeTasa,
                                DescripcionEquipo = s.DescripcionEquipo,
                                TieneCondicion = s.TieneCondicion ?? false,
                                IdRemuneracionDescripcionMonetaria = s.IdDescripcionMonetaria,
                                RangoValorMinimo = s.ValorMinimo,
                                RangoValorMaximo = s.ValorMaximo,
                                IdMonedaRangoValor = s.IdMonedaValorVariable,
                                IngresoMensual = s.IngresoMensual,
                            }));
                        foreach (var detalleExistente in listadoExistente.Where(w => dto.ListaPuestoTrabajoRemuneracionDetalle.Select(s => s.Id).Contains(w.Id)))
                        {
                            var itemActualizado = dto.ListaPuestoTrabajoRemuneracionDetalle.FirstOrDefault(f => f.Id == detalleExistente.Id);
                            PuestoTrabajoRemuneracionDetalle detalleDb = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerDetallePorId(itemActualizado.Id);
                            if (itemActualizado != null) { 
                                detalleExistente.Id = itemActualizado.Id;
                                detalleExistente.Estado = true;
                                //detalleExistente.IdRemuneracionFormaCobro = 1;
                                //detalleExistente.IdPuestoTrabajoRemuneracion = itemActualizado.IdTipoRemuneracion;
                                detalleExistente.EsTasa = itemActualizado.Tasa ?? false;
                                detalleExistente.TieneCondicion = itemActualizado.TieneCondicion ?? false;
                                detalleExistente.MontoFijo = itemActualizado.Monto;
                                detalleExistente.PorcentajeTasa = itemActualizado.PorcentajeTasa;
                                detalleExistente.DescripcionEquipo = itemActualizado.DescripcionEquipo;
                                detalleExistente.TieneCondicion = false;
                                detalleExistente.IdRemuneracionDescripcionMonetaria = itemActualizado.IdDescripcionMonetaria;
                                detalleExistente.RangoValorMinimo = itemActualizado.ValorMinimo;
                                detalleExistente.RangoValorMaximo = itemActualizado.ValorMaximo;
                                detalleExistente.IdMonedaRangoValor = itemActualizado.IdMonedaValorVariable;
                                detalleExistente.IngresoMensual = itemActualizado.IngresoMensual;
                                detalleExistente.IdRemuneracionTipo = itemActualizado.IdRemuneracion ?? 0;
                                detalleExistente.IdRemuneracionTipoCobro = itemActualizado.IdTipoRemuneracion.Value;
                                detalleExistente.IdRemuneracionFormaCobro = itemActualizado.IdClaseRemuneracion.Value;
                                detalleExistente.IdRemuneracionPeriodoCobro = itemActualizado.IdPeriodoRemuneracion.Value;
                                detalleExistente.IdMonedaMontoFijo = itemActualizado.IdMoneda;
                                detalleExistente.UsuarioModificacion = usuario;
                                detalleExistente.FechaModificacion = DateTime.Now;
                                detalleExistente.RowVersion = detalleDb.RowVersion;
                                detalleExistente.FechaCreacion = detalleDb.FechaCreacion;
                                detalleExistente.UsuarioCreacion = detalleDb.UsuarioCreacion;
                                listadoActualizar.Add(detalleExistente);
                            }
                        }

                        puestoTrabajo.TPuestoTrabajoRemuneracionDetalles = puestoTrabajo.TPuestoTrabajoRemuneracionDetalles.Concat(listadoActualizar).ToList();
                        puestoTrabajo.TPuestoTrabajoRemuneracionDetalles = puestoTrabajo.TPuestoTrabajoRemuneracionDetalles.Concat(listadoNuevo).ToList();


                    }
                    if (dto.ListaPuestoTrabajoRemuneracionDetalle.ToList().Count == 0 && listadoExistente != null) {
                        listadoIdExistente = listadoExistente.Select(s => s.Id).ToList();
                    }
                    if (listadoIdExistente != null && listadoIdExistente.Count > 0)
                    {
                        listadoEliminar.AddRange(listadoIdExistente.Where(w =>
                            !dto.ListaPuestoTrabajoRemuneracionDetalle.Select(s => s.Id).Contains(w)));
                    }

                    _unitOfWork.GestionRemuneracionPuestoTrabajoDetalleRepository.Delete(listadoEliminar, usuario);
                    _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Update(puestoTrabajo);
                    _unitOfWork.Commit();

                    var resultado = _mapper.Map<GestionRemuneracionPuestoTrabajoDTO>(puestoTrabajo);
                }
                return null;
                
            }
            catch (Exception e)
            {
                throw new Exception($"Error en insertar puestoTrabajo: {e.Message}");
            }
        }

        /// Metodo Eliminar.
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.GestionRemuneracionPuestoTrabajoRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Metodo Eliminar.
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de un excel para imprimirlos en la vista
        /// </summary>   
        /// <param name="file"> file </param>
        public object ProcesarPuestoTrabajoRemuneracionExcel(IFormFile file, string usuario)
        {
            if (file == null || file.Length <= 0)
            {
                throw new BadRequestException("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            try
            {
                List<PuestoTrabajoRemuneracionDetalleDTO> listaDatos = new List<PuestoTrabajoRemuneracionDetalleDTO>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        PuestoTrabajoRemuneracionDetalleDTO datos = new PuestoTrabajoRemuneracionDetalleDTO();

                        datos.IdRemuneracion = int.Parse(worksheet.Cells[row, 1]?.Value?.ToString() ?? "0");
                        datos.IdTipoRemuneracion = int.Parse(worksheet.Cells[row, 2]?.Value?.ToString() ?? "0");
                        datos.IdClaseRemuneracion = int.Parse(worksheet.Cells[row, 3]?.Value?.ToString() ?? "0");
                        datos.IdPeriodoRemuneracion = int.Parse(worksheet.Cells[row, 4]?.Value?.ToString() ?? "0");
                        datos.IdMoneda = int.Parse(worksheet.Cells[row, 5]?.Value?.ToString() ?? "0");
                        datos.Tasa = bool.Parse(worksheet.Cells[row, 6]?.Value?.ToString() ?? "false");
                        datos.Monto = int.Parse(worksheet.Cells[row, 7]?.Value?.ToString() ?? "0");
                        datos.PorcentajeTasa = int.Parse(worksheet.Cells[row, 8]?.Value?.ToString() ?? "0");
                        datos.TieneCondicion = bool.Parse(worksheet.Cells[row, 9]?.Value?.ToString() ?? "false");
                        datos.IdDescripcionMonetaria = int.Parse(worksheet.Cells[row, 10]?.Value?.ToString() ?? "0");
                        datos.ValorMinimo = int.Parse(worksheet.Cells[row, 11]?.Value?.ToString() ?? "0");
                        datos.ValorMaximo = int.Parse(worksheet.Cells[row, 12]?.Value?.ToString() ?? "0");
                        datos.IdMonedaValorVariable = int.Parse(worksheet.Cells[row, 13]?.Value?.ToString() ?? "0");
                        datos.IngresoMensual = int.Parse(worksheet.Cells[row, 14]?.Value?.ToString() ?? "0");

                        listaDatos.Add(datos);
                    }
                }
                return listaDatos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
