using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: NotaIngresoCajaService
    /// Autor: Griselberto Huaman
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_NotaIngresoCaja
    /// </summary>
    public class NotaIngresoCajaService : INotaIngresoCajaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public NotaIngresoCajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TNotaIngresoCaja, NotaIngresoCaja>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public NotaIngresoCaja Add(NotaIngresoEnvioDTO Data)
        {
            try
            {
                var repNotaIngresoCaja = _unitOfWork.NotaIngresoCajaRepository;
                int correlativo = 0;
                var listaCondigoNIC = repNotaIngresoCaja.GetBy(x => x.Estado == true && x.CodigoNic.Contains(Data.CodigoNic)).ToList();

                if (listaCondigoNIC != null && listaCondigoNIC.Count() != 0)
                {
                    var CodigoNicMayor = listaCondigoNIC.OrderByDescending(x => x.Id).FirstOrDefault().CodigoNic;

                    correlativo = Int32.Parse(CodigoNicMayor.Substring(CodigoNicMayor.LastIndexOf(".") + 1).Trim());
                    correlativo++;
                }
                else correlativo = 1;

                NotaIngresoCaja entidad = new NotaIngresoCaja();
                entidad.Id = 0;
                entidad.IdCaja = Data.IdCaja;
                entidad.CodigoNic = Data.CodigoNic + (correlativo).ToString();
                entidad.IdOrigenIngresoCaja = Data.IdOrigenIngresoCaja;
                entidad.IdPersonalEmitido = Data.IdPersonalEmitido;
                entidad.Monto = Data.Monto;
                entidad.FechaGiro = Data.FechaGiro;
                entidad.Concepto = Data.Concepto;
                entidad.FechaCobro = Data.FechaCobro;
                entidad.Anho = DateTime.Now.Year;
                entidad.UsuarioCreacion = Data.Usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.UsuarioModificacion = Data.Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = repNotaIngresoCaja.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<NotaIngresoCaja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NotaIngresoCaja Update(NotaIngresoEnvioDTO Data)
        {
            try
            {

                var repNotaIngresoCaja = _unitOfWork.NotaIngresoCajaRepository;
                int correlativo = 0;
                NotaIngresoCaja entidad = new NotaIngresoCaja();
                entidad = _mapper.Map<NotaIngresoCaja>(repNotaIngresoCaja.FirstById(Data.Id));

                if (entidad.IdCaja != Data.IdCaja)
                {
                    var listaCondigoNIC = repNotaIngresoCaja.GetBy(x => x.Estado == true && x.CodigoNic.Contains(Data.CodigoNic)).ToList();
                    if (listaCondigoNIC != null && listaCondigoNIC.Count() != 0)
                    {
                        var CodigoNicMayor = listaCondigoNIC.OrderByDescending(x => x.Id).FirstOrDefault().CodigoNic;

                        correlativo = Int32.Parse(CodigoNicMayor.Substring(CodigoNicMayor.LastIndexOf(".") + 1).Trim());
                        correlativo++;
                    }
                    else correlativo = 1;

                    entidad.IdCaja = Data.IdCaja;
                    entidad.CodigoNic = Data.CodigoNic + (correlativo).ToString();
                }

                entidad.IdOrigenIngresoCaja = Data.IdOrigenIngresoCaja;
                entidad.IdPersonalEmitido = Data.IdPersonalEmitido;
                entidad.Monto = Data.Monto;
                entidad.FechaGiro = Data.FechaGiro;
                entidad.Concepto = Data.Concepto;
                entidad.FechaCobro = Data.FechaCobro;
                entidad.Anho = DateTime.Now.Year;
                entidad.UsuarioModificacion = Data.Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = repNotaIngresoCaja.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<NotaIngresoCaja>(modelo);
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
                _unitOfWork.NotaIngresoCajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotaIngresoCaja> Add(List<NotaIngresoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.NotaIngresoCajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<NotaIngresoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotaIngresoCaja> Update(List<NotaIngresoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.NotaIngresoCajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<NotaIngresoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.NotaIngresoCajaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NotaIngresoCaja
        /// </summary>
        /// <returns> List<NotaIngresoCajaDTO> </returns>
        public IEnumerable<NotaIngresoCajaDTO> ObtenerNotaIngresoCaja(int id)
        {
            try
            {
                return _unitOfWork.NotaIngresoCajaRepository.ObtenerNotaIngresoCaja(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <returns> List<NotaIngresoCajaComboDTO> </returns>
        public IEnumerable<NotaIngresoCajaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.NotaIngresoCajaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <param name="FechaInicial">es la fecha inicial de fechas Giro</param>
        ///  <param name="FechaFinal">es la fecha final de fechas Giro</param>
        ///  <param name="IdCaja">el id de Caja</param>
        /// <returns> List<NotaIngresoCajaComboDTO> </returns>
        public IEnumerable<NotaIngresoCajaDTO> ObtenerCajaIngresoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                return _unitOfWork.NotaIngresoCajaRepository.ObtenerCajaIngresoByFecha(FechaInicial, FechaFinal, IdCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <param name="IdIngresoCaja">Lista de IDs</param>
        /// <returns> List<NotaIngresoCajaComboDTO> </returns>
        public IEnumerable<NotaIngresoCajaDatosPdfDTO> ObtenerDatosCajaIngreso(int[] IdIngresoCaja)
        {
            try
            {
                return _unitOfWork.NotaIngresoCajaRepository.ObtenerDatosCajaIngreso(IdIngresoCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <param name="listaEnteros">es la fecha inicial de fechas Giro</param>
        /// <returns> List<byte[]> </returns>
        public IEnumerable<byte[]> ObtenerDocumentosNIC(List<int> listaEnteros)
        {
            try
            {


                List<byte[]> listaPDFbytes = new List<byte[]>();
                string listaEnterosString = String.Join(",", listaEnteros.Select(i => i.ToString()).ToArray());
                var listaIngresoCajaDTO = this.ObtenerDatosCajaIngreso(listaEnteros.ToArray());

                foreach (var datosIngresoCaja in listaIngresoCajaDTO)
                {
                    var pdf = this.GenerarPDFNotaIngresoCaja(datosIngresoCaja);
                    listaPDFbytes.Add(pdf);
                }

                //listaPDFbytes.Add(3);
                //NotaIngresoCajaDTO datosCajaIngreso=

                return listaPDFbytes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <param name="datosGenerarPdf">Datos requeridos para llenar los datos de los Pdf</param>
        /// <returns> byte[]</returns>
        public byte[] GenerarPDFNotaIngresoCaja(NotaIngresoCajaDatosPdfDTO datosGenerarPdf)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 72f, 65f, 70f, 65f);
                PdfWriter writer;
                writer = PdfWriter.GetInstance(doc, ms);
                doc.AddTitle("Recibo" + datosGenerarPdf.CodigoNic);
                doc.AddCreator("BS grupo");
                doc.Open();
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font ForLine = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font _standardFont_normal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                PdfPTable TablaTop = new PdfPTable(3);
                TablaTop.WidthPercentage = 100;
                TablaTop.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths = new float[] { 25f, 46.5f, 28.5f };
                TablaTop.SetWidths(widths);
                PdfPCell SeccionEmpresa = new PdfPCell(new Phrase(datosGenerarPdf.RazonSocial, _standardFont));
                SeccionEmpresa.BorderWidth = 1;
                SeccionEmpresa.BackgroundColor = BaseColor.LIGHT_GRAY;
                PdfPCell SeccionTitulo = new PdfPCell(new Phrase("                  NOTA DE INGRESO", _standardFont));
                SeccionTitulo.BorderWidth = 0;
                PdfPCell SeccioNumeroRecibo = new PdfPCell(new Phrase(datosGenerarPdf.CodigoNic, _standardFont));
                SeccioNumeroRecibo.BorderWidth = 1;
                SeccioNumeroRecibo.BackgroundColor = BaseColor.LIGHT_GRAY;
                TablaTop.AddCell(SeccionEmpresa);
                TablaTop.AddCell(SeccionTitulo);
                TablaTop.AddCell(SeccioNumeroRecibo);
                doc.Add(TablaTop);
                //Segunda 
                Paragraph paDir = new Paragraph("Dirección: " + datosGenerarPdf.Direccion, _standardFont_normal); paDir.SpacingBefore = 10f; doc.Add(paDir);
                doc.Add(new Paragraph("Ruc: " + datosGenerarPdf.Ruc, _standardFont_normal));
                Paragraph paCentral = new Paragraph("Central: " + datosGenerarPdf.Central, _standardFont_normal); paCentral.SpacingAfter = 8f; doc.Add(paCentral);
                //Tercera seccion
                PdfPTable TablaBottom = new PdfPTable(2);
                TablaBottom.DefaultCell.Padding = 10f;
                //TablaBottom.DefaultCell.CellEvent = new CellSpacingEvent(20);
                TablaBottom.WidthPercentage = 80;
                TablaBottom.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths2 = new float[] { 22f, 78f };
                TablaBottom.SetWidths(widths2);
                PdfPCell SeccionCuentaTitulo = new PdfPCell(new Phrase("Cuenta", _standardFont2));
                SeccionCuentaTitulo.PaddingTop = 6f;
                SeccionCuentaTitulo.BorderWidthTop = 1;
                SeccionCuentaTitulo.BorderWidth = 0;
                PdfPCell SeccionCuentaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.CuentaCaja, _standardFont_normal));
                SeccionCuentaValor.BorderWidth = 0;
                SeccionCuentaValor.PaddingTop = 6f;
                SeccionCuentaValor.BorderWidthTop = 1;
                PdfPCell SeccionNroFurTitulo = new PdfPCell(new Phrase("Origen", _standardFont2));
                SeccionNroFurTitulo.BorderWidth = 0;
                PdfPCell SeccionNroFurValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Origen, _standardFont_normal));
                SeccionNroFurValor.BorderWidth = 0;
                PdfPCell SeccionFechaTitulo = new PdfPCell(new Phrase("Fecha Giro", _standardFont2));
                SeccionFechaTitulo.BorderWidth = 0;
                PdfPCell SeccionFechaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.FechaGiro, _standardFont_normal));
                SeccionFechaValor.BorderWidth = 0;
                PdfPCell SeccionSeentregoaTitulo = new PdfPCell(new Phrase("Se entregó a", _standardFont2));
                SeccionSeentregoaTitulo.BorderWidth = 0;
                PdfPCell SeccionSeentregoaValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.PersonalEmitido, _standardFont_normal));
                SeccionSeentregoaValor.BorderWidth = 0;
                PdfPCell SeccionImporteTitulo = new PdfPCell(new Phrase("Importe", _standardFont2));
                SeccionImporteTitulo.BorderWidth = 0;
                PdfPCell SeccionImporteValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Monto + "   " + datosGenerarPdf.Moneda, _standardFont_normal));
                SeccionImporteValor.BorderWidth = 0;
                PdfPCell SeccionDetalleTitulo = new PdfPCell(new Phrase("Concepto", _standardFont2));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleTitulo.BorderWidth = 0;
                PdfPCell SeccionDetalleValor = new PdfPCell(new Phrase(":  " + datosGenerarPdf.Concepto, _standardFont_normal));
                SeccionDetalleTitulo.PaddingBottom = 43f;
                SeccionDetalleValor.BorderWidth = 0;
                TablaBottom.AddCell(SeccionCuentaTitulo);
                TablaBottom.AddCell(SeccionCuentaValor);
                TablaBottom.AddCell(SeccionFechaTitulo);
                TablaBottom.AddCell(SeccionFechaValor);
                TablaBottom.AddCell(SeccionNroFurTitulo);
                TablaBottom.AddCell(SeccionNroFurValor);
                TablaBottom.AddCell(SeccionSeentregoaTitulo);
                TablaBottom.AddCell(SeccionSeentregoaValor);
                TablaBottom.AddCell(SeccionImporteTitulo);
                TablaBottom.AddCell(SeccionImporteValor);
                TablaBottom.AddCell(SeccionDetalleTitulo);
                TablaBottom.AddCell(SeccionDetalleValor);
                doc.Add(TablaBottom);
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("\n"));
                doc.Add(new iTextSharp.text.Paragraph("  ________________________                             ________________________", _standardFont2));
                doc.Add(new iTextSharp.text.Paragraph("     ENTREGUE CONFORME                                       RECIBI CONFORME", _standardFont2));
                PdfPTable TablaBottom2 = new PdfPTable(2);
                TablaBottom2.WidthPercentage = 100;
                TablaBottom2.HorizontalAlignment = Element.ALIGN_LEFT;
                float[] widths3 = new float[] { 48, 60 };
                TablaBottom2.SetWidths(widths3);
                PdfPCell SeccionCuentaTitulo2 = new PdfPCell(new Phrase(datosGenerarPdf.PersonalResponsable, _standardFont_normal));
                SeccionCuentaTitulo2.PaddingTop = 10f;
                SeccionCuentaTitulo2.BorderWidth = 0;
                PdfPCell SeccionCuentaValor2 = new PdfPCell(new Phrase("              " + datosGenerarPdf.PersonalEmitido, _standardFont_normal));
                SeccionCuentaValor2.BorderWidth = 0;
                SeccionCuentaValor2.PaddingTop = 10f;
                PdfPCell SeccionNroFurTitulo2 = new PdfPCell(new Phrase("DNI: ", _standardFont_normal));
                SeccionNroFurTitulo2.BorderWidth = 0;
                PdfPCell SeccionNroFurValor2 = new PdfPCell(new Phrase("              DNI: ", _standardFont_normal));
                SeccionNroFurValor2.BorderWidth = 0;
                TablaBottom2.AddCell(SeccionCuentaTitulo2);
                TablaBottom2.AddCell(SeccionCuentaValor2);
                TablaBottom2.AddCell(SeccionNroFurTitulo2);
                TablaBottom2.AddCell(SeccionNroFurValor2);
                doc.Add(TablaBottom2);
                doc.Close();
                writer.Close();
                doc.Dispose();

                return ms.ToArray();
            }
        }


    }
}
