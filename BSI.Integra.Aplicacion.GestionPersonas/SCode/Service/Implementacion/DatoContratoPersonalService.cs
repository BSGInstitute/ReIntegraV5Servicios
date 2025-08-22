using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Exceptions;
using Org.BouncyCastle.Crypto;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;
using iText.Kernel.Pdf.Event;
using iText.Commons.Actions;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class DatoContratoPersonalService : IDatoContratoPersonalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public DatoContratoPersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDatoContratoPersonal, DatoContratoPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<DatoContratoPersonal, DatoContratoPersonalDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDatoContratoPersonal, DatoContratoPersonalDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 28/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo para DatoContrato
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public Object ObtenerCombos()
        {
            try
            {
                var ComboContrato = new
                {
                    ListaAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerCombo(),
                    ListaTipoContrato = _unitOfWork.TipoContratoRepository.ObtenerCombo(),
                    ListaPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo(),
                    ListaSede = _unitOfWork.SedeTrabajoRepository.ObtenerCombo(),
                    ListaCargo = _unitOfWork.CargoRepository.ObtenerCombo(),
                    ListaContratoEstado = _unitOfWork.ContratoEstadoRepository.Obtener(),

                };
                return ComboContrato;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/01/2025 
        /// Versión: 3.0
        /// <summary>
        /// Obtiene combo para DatoContrato
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public Object ObtenerComboContrato()
        {
            try
            {
                

                var ComboContrato = new
                {
                    ComboPais = _unitOfWork.PaisRepository.ObtenerCombo(),
                    ComboCiudad = _unitOfWork.CiudadRepository.ObtenerCombo(),
                    ComboTipoDocumento = _unitOfWork.TipoDocumentoPersonalRepository.ObtenerComboDocumentos(),
                    ComboMoneda = _unitOfWork.MonedaRepository.ObtenerCombo(),
                    ComboRemuneracionTipo = _unitOfWork.DatoContratoPersonalRepository.ObtenerComboRemuneracionTipo(),
                    ComboPlantillaContrato = _unitOfWork.PlantillaBaseRepository.ObtenerPlantillasContrato(),
                    ComboFuncionPuesto = _unitOfWork.PuestoTrabajoRepository.ObtenerFuncionPuestoTrabajo()

                };
                return ComboContrato;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 28/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Contrato por filto, o toda la lista de contratos
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public List<DatoContratoPersonalFiltroDTO> ObtenerContratosPorFiltro(ContratoFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.DatoContratoPersonalRepository.ObtenerContratosPorFiltrov2(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para el formulario de contrato
        /// </summary>
        /// <returns> Objeto generico </returns>
        public DatosFormularioPersonalDTO ObtenerDataFormulario(int IdPersonal)
        {
            try
            {
                //var obj = new
                //{
                //    ListaPersonal = _unitOfWork.PersonalRepository.ObtenerInfoContrato(IdPersonal),
                //    ListaPersonalExperiencia = _unitOfWork.PersonalRepository.ObtenerPersonalExperiencia(IdPersonal),
                //    ListaPersonalFormacion = _unitOfWork.PersonalRepository.ObtenerPersonalFormacion(IdPersonal),
                //    ListaPersonalIdioma = _unitOfWork.PersonalRepository.ObtenerPersonalIdioma(IdPersonal),
                //};
                //return obj;
                return _unitOfWork.DatoContratoPersonalRepository.ObtenerDatosPersonalesFormulario(IdPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DatosFormularioPersonalDTO asd(int idPersonal)
        {
            try
            {
                return _unitOfWork.DatoContratoPersonalRepository.ObtenerDatosPersonalesFormulario(idPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista del historico de contratos
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public List<ContratoHistoricoRegistroRDTO> ObtenerContratosHistoricos(int IdPersonal)
        {
            try
            {
                var listaContratos = _unitOfWork.PersonalRepository.ObtenerContratoHistorico(IdPersonal);
                var agrupado = listaContratos.GroupBy(x => new
                {
                    x.Id,
                    x.IdPersonal,
                    x.Nombres,
                    x.ApellidoMaterno,
                    x.ApellidoPaterno,
                    x.TipoContrato,
                    x.EstadoContrato,
                    x.FechaInicio,
                    x.FechaFin,
                    x.RemuneracionFija,
                    x.PuestoTrabajo,
                    x.SedeTrabajo,
                    x.PersonalAreaTrabajo,
                    x.Cargo,
                    x.ContratoEstado,
                    x.Estado
                }).Select(g => new ContratoHistoricoRegistroRDTO
                {
                    Id = g.Key.Id.Value,
                    IdPersonal = g.Key.IdPersonal,
                    Nombres = g.Key.Nombres,
                    Apellidos = g.Key.ApellidoPaterno + g.Key.ApellidoMaterno,
                    TipoContrato = g.Key.TipoContrato,
                    EstadoContrato = g.Key.EstadoContrato,
                    FechaInicio = g.Key.FechaInicio,
                    FechaFin = g.Key.FechaFin,
                    RemuneracionFija = g.Key.RemuneracionFija,
                    PuestoTrabajo = g.Key.PuestoTrabajo,
                    SedeTrabajo = g.Key.SedeTrabajo,
                    PersonalAreaTrabajo = g.Key.PersonalAreaTrabajo,
                    Cargo = g.Key.Cargo,
                    ContratoEstado = g.Key.ContratoEstado,
                    Estado = g.Key.Estado,
                    ListaRemuneracionVariable = g.GroupBy(y => new { y.Monto, y.Concepto, y.TipoRemuneracionVariable }).Select(y => new ContratoHistoricoRegistroRVDTO
                    {
                        Monto = y.Key.Monto,
                        Concepto = y.Key.Concepto,
                        TipoRemuneracionVariable = y.Key.TipoRemuneracionVariable,
                    }).ToList()

                }).ToList();

                return agrupado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la lista del historico de contratos
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public Object ObtenerRemuneracionVariableDisplay(int IdPuestoTrabajo)
        {
            try
            {
                //var PTRemuneracion = _repPuestoTrabajoRemuneracion.FirstBy(x => x.IdPuestoTrabajo == IdPuestoTrabajo);
                var PTRemuneracion = _unitOfWork.PuestoTrabajoRepository.ObtenerComboRemuneracion(IdPuestoTrabajo);
                if (PTRemuneracion != null)
                {
                    var ListaPTRemuneracionVariable = _unitOfWork.PuestoTrabajoRepository.ObtenerPuestoTrabajoRemuneracionDet(PTRemuneracion.Id);
                    var obj = new
                    {
                        PTRemuneracion,
                        ListaPTRemuneracionVariable,
                    };
                    return obj;
                }
                else
                {
                    var obj = new
                    {
                        PTRemuneracion,
                    };
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Inserta un nuevo Contrato
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public Boolean InsertarContrato(DatoContratoPersonalDTO ContratoPersonal)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var listaContratoAnterior = _unitOfWork.DatoContratoPersonalRepository.GetBy(x => x.IdPersonal == ContratoPersonal.IdPersonal && x.EstadoContrato == true).ToList();


                    foreach (var item in listaContratoAnterior)
                    {
                        var listaRemuneracionVariable = _unitOfWork.DatoContratoComisionBonoRepository.GetBy(x => x.IdDatoContratoPersonal == item.Id).ToList();

                        foreach (var item2 in listaRemuneracionVariable)
                        {

                            _unitOfWork.DatoContratoComisionBonoRepository.Delete(item2.Id, ContratoPersonal.Usuario);
                            _unitOfWork.Commit();
                            _unitOfWork.DetachAll();
                        }
                        item.EstadoContrato = false;
                        item.UsuarioModificacion = ContratoPersonal.Usuario;
                        item.FechaModificacion = DateTime.Now;
                        _unitOfWork.DatoContratoPersonalRepository.Update(item);
                    }

                    TDatoContratoPersonal contratoPersonal = new TDatoContratoPersonal()
                    {
                        IdPersonal = ContratoPersonal.IdPersonal,
                        IdTipoContrato = ContratoPersonal.IdTipoContrato,
                        EstadoContrato = true,
                        FechaInicio = ContratoPersonal.FechaInicio,
                        FechaFin = ContratoPersonal.FechaFin,
                        RemuneracionFija = ContratoPersonal.RemuneracionFija,
                        IdPuestoTrabajo = ContratoPersonal.IdPuestoTrabajo,
                        IdSedeTrabajo = ContratoPersonal.IdSedeTrabajo,
                        IdPersonalAreaTrabajo = ContratoPersonal.IdPersonalAreaTrabajo,
                        IdCargo = ContratoPersonal.IdCargo,
                        IdContratoEstado = ContratoPersonal.IdContratoEstado,
                        Estado = true,
                        UsuarioCreacion = ContratoPersonal.Usuario,
                        UsuarioModificacion = ContratoPersonal.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    var res = _unitOfWork.DatoContratoPersonalRepository.Insert(contratoPersonal);
                    _unitOfWork.Commit();
                    _unitOfWork.DetachAll();


                    foreach (var item in ContratoPersonal.ListaRemuneracionVariable)
                    {
                        TDatoContratoComisionBono remuneracionVariable = new TDatoContratoComisionBono()
                        {
                            IdDatoContratoPersonal = contratoPersonal.Id,
                            TipoRemuneracionVariable = item.TipoRemuneracionVariable,
                            Concepto = item.Concepto,
                            Monto = item.Monto,
                            Estado = true,
                            UsuarioCreacion = ContratoPersonal.Usuario,
                            UsuarioModificacion = ContratoPersonal.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _unitOfWork.DatoContratoComisionBonoRepository.Insert(remuneracionVariable);
                    }
                    _unitOfWork.Commit();
                    scope.Complete();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
                       
        public byte[] GenerarPDFDatoContratoPersonal(string datosControPersonal)
        {
            StringReader sr = new StringReader(datosControPersonal.ToString());

            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //pdfDoc.SetMargins(50f, 50f, 50f, 50f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();

            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();

            //    byte[] bytes = memoryStream.ToArray();
            //    memoryStream.Close();
            //    return bytes;
            //}
            return null;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// genera pdf de contrato y formulario
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public byte[] GenerarPDFDatoContratoPersonalV2(string html)
        {
            try
            {
                //using (var memoryStream = new MemoryStream())
                //{
                //    PdfWriter writer = new PdfWriter(memoryStream);
                //    PdfDocument pdf = new PdfDocument(writer);
                //    Document document = new Document(pdf);

                //    // Propiedades para la conversión con soporte de BouncyCastle
                //    ConverterProperties converterProperties = new ConverterProperties();
                //    converterProperties.SetFontProvider(new DefaultFontProvider());

                //    // Convertir HTML directamente a PDF con seguridad de BouncyCastle
                //    HtmlConverter.ConvertToPdf(datosContratoPersonal, pdf, converterProperties);

                //    document.Close();
                //    return memoryStream.ToArray();
                //}
                var pdfCerrado = GenerarPDFDesdeHTML(html);
                string imagePath = "https://repositorioweb.blob.core.windows.net/documentos/integra/LogoPrincipal.png";
                var pdfConMembrete = AgregarMembrete(pdfCerrado, imagePath);

                return pdfConMembrete;
            }
            catch (Exception ex)
            {

                throw new Exception("Execcion al generar el pdf: " + ex.Message);
            }

        }

        public byte[] GenerarPDFDesdeHTML(string html)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Convertir HTML a PDF directamente
                HtmlConverter.ConvertToPdf(html, memoryStream);
                return memoryStream.ToArray(); // El PDF ya está cerrado
            }
        }

        public byte[] AgregarMembrete(byte[] pdfBytes, string imagePath)
        {
            using (var msEntrada = new MemoryStream(pdfBytes))
            using (var msSalida = new MemoryStream())
            {
                // Reabrir el PDF (modo lectura)
                PdfReader reader = new PdfReader(msEntrada);
                PdfWriter writer = new PdfWriter(msSalida);
                PdfDocument pdfDoc = new PdfDocument(reader, writer);

                // Añadir membrete en cada página
                ImageData imageData = ImageDataFactory.Create(imagePath);

                //Margen de la imagen
                float margenCm = 22f;

                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    PdfPage page = pdfDoc.GetPage(i);
                    PdfCanvas canvas = new PdfCanvas(page);

                    Image membrete = new Image(imageData).ScaleToFit(200, 100);

                    new Canvas(canvas, pdfDoc.GetDefaultPageSize())
                        .Add(membrete.SetFixedPosition(margenCm, pdfDoc.GetDefaultPageSize().GetTop() - margenCm - 65));
                }

                pdfDoc.Close(); // Cierra el PDF ya modificado con el membrete
                return msSalida.ToArray();
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 31/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos para autocomplete de personal
        /// </summary>
        /// <returns> List<PersonalComboDTO> </returns>
        public List<PersonalAutocompleteDTO> CargarPersonalAutoCompleteContrato(string nombre)
        {
            try
            {
                return _unitOfWork.PersonalRepository.CargarPersonalAutoCompleteContrato(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DatosRemuneracionVariableDTO> ObtenerComboDatosRemuneracionVariable()
        {
            try
            {
                return _unitOfWork.DatoContratoPersonalRepository.ObtenerComboDatosRemuneracionVariable();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener datos personales de formulario: {ex.Message}", ex);
            }
        }

    }
}
