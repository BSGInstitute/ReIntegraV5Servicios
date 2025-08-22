using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ExcelService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class ExcelService : IExcelService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ExcelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TExcel, Excel>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        public byte[]? ReporteAmbientePespecifico(IEnumerable<ReporteAmbienteDTO> listadoReporte)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (var package = new ExcelPackage(ms))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte Programa Especifico Ambientes");

                    worksheet.Cells.Style.Font.Name = "Arial";
                    worksheet.Cells.Style.Font.Size = 10;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.A4; //orientacion de la pagina

                    ///valores a considerar
                    int fila = 1, columna = 2;

                    Color color_fondo = Color.FromArgb(217, 217, 217);  //gris oscuro

                    Color color_encabezado = Color.FromArgb(0, 0, 0); //negro

                    #region Colores del excel
                    Color _colorCabecera = Color.FromArgb(191, 191, 191); // gris
                    Color _colorSubSubNivel = Color.FromArgb(197, 217, 241); // azul claro
                    Color _colorNivel = Color.FromArgb(33, 182, 193); // integra 
                    Color _colorSubNivel = Color.FromArgb(169, 208, 142); //rojo claro
                    #endregion

                    var listaNivel = new List<CeldaDTO>();
                    var listaSubNivel = new List<CeldaDTO>();
                    var listaSubSubNivel = new List<CeldaDTO>();
                    var listaCabecera = new List<CeldaDTO>();


                    #region Encabezado
                    //primerafila
                    fila = 1;
                    columna = 1;

                    worksheet.Cells[fila, columna].Value = "Centro de Costo Padre";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 40;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Programa Especifico Padre";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 80;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Estado Padre";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });


                    columna++;
                    worksheet.Cells[fila, columna].Value = "Centro de Costo Hijo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 40;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });


                    columna++;
                    worksheet.Cells[fila, columna].Value = "Programa Especifico Hijo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 80;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Estado Hijo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 12;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Modalidad Hijo";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 17;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Año";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 10;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "SemanaCalendario";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 5;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Fecha";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Horario";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Sede";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Aula";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 12;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroSesión";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 15;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Docente";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 50;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "Coordinador";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 50;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroAmbientesProgramados";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 10;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    columna++;
                    worksheet.Cells[fila, columna].Value = "NroAmbientesDisponibles";
                    worksheet.Cells[fila, columna].Style.Font.Bold = true;
                    worksheet.Cells[fila, columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Column(columna).Width = 10;
                    listaCabecera.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                    fila++;
                    columna = 1;

                    columna += 1;
                    foreach (var item in listadoReporte)
                    {
                        columna = 1;

                        worksheet.Cells[fila, columna].Value = item.CentroCostoPadre;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.ProgramaEspecificoPadre;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.EstadoPadre;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.CentroCostoHijo;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.ProgramaEspecificoHijo;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.EstadoHijo;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.ModalidadHijo;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Anio;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.SemanaCalendario;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Fecha;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Horario;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Sede;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Aula;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.NroSesión;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Docente;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.Coordinador;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.NroAmbientesProgramados;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });
                        columna++;

                        worksheet.Cells[fila, columna].Value = item.NroAmbientesDisponibles;
                        listaNivel.Add(new CeldaDTO() { Fila = fila, Columna = columna });

                        fila++;
                    }
                    columna = 1;
                    //colores
                    foreach (var item in listaCabecera)
                    {
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[item.Fila, item.Columna].Style.Fill.BackgroundColor.SetColor(_colorCabecera);
                    }
                    #endregion
                    package.Save();
                }
                byte[] excel = ms.ToArray();

                ms.Close();
                return excel;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
