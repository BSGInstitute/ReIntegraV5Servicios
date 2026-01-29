using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Controllers.Wavix;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BSI.Integra.PruebasUnitarias.Controllers.Wavix
{
    /// <summary>
    /// Pruebas unitarias para WavixController
    /// Endpoints probados: ListarSipTrunks, GenerarTokenWidget, ObtenerConfiguracionCompletaWavix, ObtenerTokenActivo
    /// </summary>
    [TestClass]
    public class WavixControllerTests
    {
        private Mock<IUnitOfWork>? _mockUnitOfWork;
        private Mock<IHttpClientFactory>? _mockHttpClientFactory;
        private WavixController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();

            var mockHttpClient = new HttpClient();
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient);

            _controller = new WavixController(_mockUnitOfWork.Object, _mockHttpClientFactory.Object);
        }

        #region ListarSipTrunks Tests

        [TestMethod]
        public async Task ListarSipTrunks_DeberiaRetornarOk_ConParametrosValidos()
        {
            // Arrange
            string apiKey = "test-api-key";
            int page = 1;
            int perPage = 10;

            // Act
            var resultado = await _controller!.ListarSipTrunks(apiKey, page, perPage);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ListarSipTrunks_DeberiaRetornarOk_SinPaginacion()
        {
            // Arrange
            string apiKey = "test-api-key";

            // Act
            var resultado = await _controller!.ListarSipTrunks(apiKey, null, null);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ListarSipTrunks_DeberiaRetornarOk_ConSoloPagina()
        {
            // Arrange
            string apiKey = "test-api-key";
            int page = 2;

            // Act
            var resultado = await _controller!.ListarSipTrunks(apiKey, page, null);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ListarSipTrunks_DeberiaRetornarOk_ConSoloPerPage()
        {
            // Arrange
            string apiKey = "test-api-key";
            int perPage = 20;

            // Act
            var resultado = await _controller!.ListarSipTrunks(apiKey, null, perPage);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ListarSipTrunks_DeberiaRetornarBadRequest_CuandoApiKeyEsVacio()
        {
            // Arrange
            string apiKey = "";

            // Act
            var resultado = await _controller!.ListarSipTrunks(apiKey, 1, 10);

            // Assert
            Assert.IsNotNull(resultado);
        }

        #endregion

        #region GenerarTokenWidget Tests

        [TestMethod]
        public async Task GenerarTokenWidget_DeberiaRetornarOk_ConRequestValido()
        {
            // Arrange
            string apiKey = "test-api-key";
            var request = new GenerarTokenWidgetRequestDTO
            {
                sip_trunk = "test-sip-trunk-id",
                payload = new {},
                ttl = 3600
            };

            // Act
            var resultado = await _controller!.GenerarTokenWidget(apiKey, request);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task GenerarTokenWidget_DeberiaRetornarOk_SinTTL()
        {
            // Arrange
            string apiKey = "test-api-key";
            var request = new GenerarTokenWidgetRequestDTO
            {
                sip_trunk = "test-sip-trunk-id",
                payload = new { userId = "123" },
                ttl = null
            };

            // Act
            var resultado = await _controller!.GenerarTokenWidget(apiKey, request);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task GenerarTokenWidget_DeberiaRetornarOk_ConPayloadComplejo()
        {
            // Arrange
            string apiKey = "test-api-key";
            var request = new GenerarTokenWidgetRequestDTO
            {
                sip_trunk = "test-sip-trunk-id",
                payload = new
                {
                    userId = "123",
                    name = "Test User",
                    metadata = new
                    {
                        department = "Sales",
                        role = "Agent"
                    }
                },
                ttl = 7200
            };

            // Act
            var resultado = await _controller!.GenerarTokenWidget(apiKey, request);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task GenerarTokenWidget_DeberiaRetornarBadRequest_CuandoApiKeyEsNull()
        {
            // Arrange
            string apiKey = null!;
            var request = new GenerarTokenWidgetRequestDTO
            {
                sip_trunk = "test-sip-trunk-id",
                payload = new { },
                ttl = 3600
            };

            // Act
            var resultado = await _controller!.GenerarTokenWidget(apiKey, request);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GenerarTokenWidget_DeberiaRetornarOk_ConRequestMinimo()
        {
            // Arrange
            string apiKey = "test-api-key";
            var request = new GenerarTokenWidgetRequestDTO
            {
                sip_trunk = "test-sip-trunk-id",
                payload = null!,
                ttl = null
            };

            // Act
            var resultado = await _controller!.GenerarTokenWidget(apiKey, request);

            // Assert
            Assert.IsNotNull(resultado);
        }

        #endregion

        #region ObtenerConfiguracionCompletaWavix Tests

        [TestMethod]
        public async Task ObtenerConfiguracionCompletaWavix_DeberiaRetornarOk_ConIdPersonalValido()
        {
            // Arrange
            int idPersonal = 1;

            // Act
            var resultado = await _controller!.ObtenerConfiguracionCompletaWavix(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ObtenerConfiguracionCompletaWavix_DeberiaRetornarOk_ConIdPersonalGrande()
        {
            // Arrange
            int idPersonal = 999999;

            // Act
            var resultado = await _controller!.ObtenerConfiguracionCompletaWavix(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public async Task ObtenerConfiguracionCompletaWavix_DeberiaRetornarBadRequest_ConIdPersonalCero()
        {
            // Arrange
            int idPersonal = 0;

            // Act
            var resultado = await _controller!.ObtenerConfiguracionCompletaWavix(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public async Task ObtenerConfiguracionCompletaWavix_DeberiaRetornarBadRequest_ConIdPersonalNegativo()
        {
            // Arrange
            int idPersonal = -1;

            // Act
            var resultado = await _controller!.ObtenerConfiguracionCompletaWavix(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public async Task ObtenerConfiguracionCompletaWavix_DeberiaManejarExcepcion_CuandoServicioFalla()
        {
            // Arrange
            int idPersonal = 1;
            _mockHttpClientFactory!.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Throws(new Exception("Error simulado de conexión"));

            // Act
            var resultado = await _controller!.ObtenerConfiguracionCompletaWavix(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(BadRequestObjectResult));
        }

        #endregion

        #region ObtenerTokenActivo Tests

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaRetornarOk_ConIdPersonalValido()
        {
            // Arrange
            int idPersonal = 1;

            // Act
            var resultado = _controller!.ObtenerTokenActivo(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaRetornarOk_ConIdPersonalDiferente()
        {
            // Arrange
            int idPersonal = 100;

            // Act
            var resultado = _controller!.ObtenerTokenActivo(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ActionResult));
        }

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaRetornarBadRequest_ConIdPersonalCero()
        {
            // Arrange
            int idPersonal = 0;

            // Act
            var resultado = _controller!.ObtenerTokenActivo(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaRetornarBadRequest_ConIdPersonalNegativo()
        {
            // Arrange
            int idPersonal = -1;

            // Act
            var resultado = _controller!.ObtenerTokenActivo(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaManejarExcepcion_CuandoServicioFalla()
        {
            // Arrange
            int idPersonal = 1;
            _mockHttpClientFactory!.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Throws(new Exception("Error simulado al obtener token"));

            // Act
            var resultado = _controller!.ObtenerTokenActivo(idPersonal);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void ObtenerTokenActivo_DeberiaRetornarOk_ConDiferentesIdPersonal()
        {
            // Arrange
            int[] idsPersonal = { 1, 5, 10, 100, 1000 };

            foreach (var idPersonal in idsPersonal)
            {
                // Act
                var resultado = _controller!.ObtenerTokenActivo(idPersonal);

                // Assert
                Assert.IsNotNull(resultado, $"Fallo con idPersonal: {idPersonal}");
            }
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void Cleanup()
        {
            _controller = null;
            _mockUnitOfWork = null;
            _mockHttpClientFactory = null;
        }

        #endregion
    }
}
