using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ContenidoCertificadoIrcaController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ContenidoCertificadoIrcaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("[Action]")]
        public ActionResult MostrarContenidoIrca([FromForm] IFormFile files)
        {
            var listaContenidoIrcaDTO = new List<ContenidoCertificadoIrcaDTO>();
            CsvFile file = new CsvFile();
            try
            {
                ICentroCostoService centroCostoService = new CentroCostoService(_unitOfWork);
                IMatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                int index = 0;


                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null
                };

                using (var cvs = new CsvReader(new StreamReader(files.OpenReadStream()), config))
                {
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        ContenidoCertificadoIrcaDTO contenidoIrca = new ContenidoCertificadoIrcaDTO();
                        try
                        {
                            index++;

                            contenidoIrca.CodigoMatricula = cvs.GetField<string>("CodigoMatricula");
                            contenidoIrca.IdMatriculaCabecera = matriculaCabeceraService.obtenerIdMatriculaporCodigo(contenidoIrca.CodigoMatricula).Id;
                            contenidoIrca.CursoIrcaId = cvs.GetField<int>("CursoIrcaId");
                            contenidoIrca.NombreCurso = cvs.GetField<string>("NombreCurso");
                            contenidoIrca.CodigoCurso = cvs.GetField<string>("CodigoCurso");
                            string fechaI = cvs.GetField<string>("FechaInicio");
                            contenidoIrca.FechaInicio = DateTime.ParseExact(fechaI, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string fechaF = cvs.GetField<string>("FechaFin");
                            contenidoIrca.FechaFin = DateTime.ParseExact(fechaF, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            contenidoIrca.DuracionCurso = cvs.GetField<int>("DuracionCurso");
                            contenidoIrca.ResultadoCurso = cvs.GetField<string>("ResultadoCurso");
                            contenidoIrca.CentroCostoIrca = cvs.GetField<string>("CentroCostoIrca");
                            contenidoIrca.IdCentroCostoIrca = centroCostoService.Obtener().Find(w => w.Nombre == contenidoIrca.CentroCostoIrca).Id;

                            listaContenidoIrcaDTO.Add(contenidoIrca);
                        }
                        catch (Exception ex)
                        {
                            if (listaContenidoIrcaDTO.Count == 0)
                            {
                                return BadRequest(ex.Message);
                            }
                        }

                    }
                }
                var nroRegistros = index;
                return Ok(new { listaContenidoIrcaDTO, nroRegistros });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[Action]")]
        public ActionResult InsertarContenidoCertificadoIrca([FromBody] List<ContenidoCertificadoIrcaDTO> obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IContenidoCertificadoIrcaService contenidoCertificadoIrcaService = new ContenidoCertificadoIrcaService(_unitOfWork);
                contenidoCertificadoIrcaService.InsertarListaContenidoCertificadoIrca(obj);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
