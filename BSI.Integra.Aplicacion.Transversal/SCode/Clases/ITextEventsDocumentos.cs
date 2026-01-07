using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Clases
{
    public class ITextEventsDocumentos : PdfPageEventHelper
    {
        public int contadorPagina = 0;
        public ITextEventsDocumentos()
        {
        }
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        private string _header;


        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Image jpg2 = Image.GetInstance("https://repositorioweb.blob.core.windows.net/repositorioweb/img/logotiposilabo.png");

            PdfPTable TablaTop = new PdfPTable(3);
            float[] widths = new float[] { 10, 65, 25f };
            TablaTop.SetWidths(widths);

            PdfPCell cellx1 = new PdfPCell(jpg2, false);
            cellx1.Padding = 8f;
            cellx1.HorizontalAlignment = 1;
            cellx1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx1.Rowspan = 4;
            cellx1.BorderWidth = 1f;
            cellx1.BorderColor = BaseColor.LIGHT_GRAY;
            TablaTop.AddCell(cellx1);

            iTextSharp.text.Font _FSGC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
            iTextSharp.text.Font _FUColumns = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
            iTextSharp.text.Font _FUColumnsNegrita = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
            PdfPCell cellx2 = new PdfPCell(new Phrase("SISTEMA DE GESTIÓN DE LA CALIDAD", _FSGC));
            cellx2.HorizontalAlignment = 1;
            cellx2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx2.BorderColor = BaseColor.LIGHT_GRAY;
            cellx2.PaddingTop = 3f;
            cellx2.PaddingBottom = 3f;
            cellx2.BorderWidth = 1f;
            TablaTop.AddCell(cellx2);

            PdfPCell cellx3 = new PdfPCell(new Phrase("RE-I&D-041", _FUColumns));
            cellx3.HorizontalAlignment = 1;
            cellx3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx3.BorderColor = BaseColor.LIGHT_GRAY;
            cellx3.BorderWidth = 1f;
            TablaTop.AddCell(cellx3);

            iTextSharp.text.Font _FCronogramaAlumnos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            PdfPCell cellx4 = new PdfPCell(new Phrase("Sílabo - Cursos Online sincrónicos", _FUColumnsNegrita));
            cellx4.HorizontalAlignment = 1;
            cellx4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx4.Rowspan = 2;
            cellx4.BorderColor = BaseColor.LIGHT_GRAY;
            cellx4.BorderWidth = 1f;
            TablaTop.AddCell(cellx4);

            PdfPCell cellx5 = new PdfPCell(new Phrase("Revisión 09", _FUColumns));
            cellx5.HorizontalAlignment = 1;
            cellx5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx5.BorderColor = BaseColor.LIGHT_GRAY;
            cellx5.BorderWidth = 1f;
            TablaTop.AddCell(cellx5);

            PdfPCell cellx6 = new PdfPCell(new Phrase("23 Jun 2021", _FUColumns));
            cellx6.HorizontalAlignment = 1;
            cellx6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellx6.BorderColor = BaseColor.LIGHT_GRAY;
            cellx6.BorderWidth = 1f;
            TablaTop.AddCell(cellx6);
            TablaTop.TotalWidth = document.PageSize.Width - 80f;
            TablaTop.WidthPercentage = 70;

            TablaTop.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);

            contadorPagina++;

            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                cb.EndText();
                float len = bf.GetWidthPoint("hello", 12);
                cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45), true);
            }
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8.5f);

                Paragraph p = new Paragraph(string.Format("Pagina ", document.PageNumber.ToString()));
                p.Alignment = Element.ALIGN_RIGHT;

                cb.SetColorStroke(BaseColor.DARK_GRAY);
                cb.SetTextMatrix(document.PageSize.GetLeft(60f), document.PageSize.GetBottom(35));
                cb.ShowTextAligned(1, "Pagina " + document.PageNumber.ToString(), 520, 10, 0);
                cb.EndText();
            }

        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.EndText();
        }
    }
}
