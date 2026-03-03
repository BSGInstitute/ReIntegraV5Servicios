using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BSI.Integra.PruebasUnitarias.Comercial.Rn2
{
    /// <summary>
    /// Pruebas unitarias para ValidacionRn2Service.ValidarLeadRn2Async
    ///
    /// Flujo general validado:
    ///   PASO 0: Guard idOportunidad <= 0 | Guard idPersonalAsignado <= 0
    ///   PASO 1: SP ObtenerIdAlumnoPorValidacionRN2 — si null o IdAlumno null → true
    ///   PASO 2: Normalización de teléfono y correo
    ///   PASO 3: Búsqueda LIKE de alumnos similares + existencia de oportunidades → false si bloquea
    ///   PASO 4: Contar oportunidades propias del alumno → false si > 1
    /// </summary>
    [TestClass]
    public class ValidarLeadRn2AsyncTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = null!;
        private Mock<IOportunidadRepository> _oportunidadRepoMock = null!;
        private ValidacionRn2Service _service = null!;

        // Datos fijos reutilizables
        private const int IdOportunidadValida = 1001;
        private const int IdAlumnoActual = 5000;
        private const int IdPersonalAsignadoValido = 42;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _oportunidadRepoMock = new Mock<IOportunidadRepository>();

            _unitOfWorkMock
                .Setup(u => u.OportunidadRepository)
                .Returns(_oportunidadRepoMock.Object);

            _service = new ValidacionRn2Service(_unitOfWorkMock.Object);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 1: Guard de entrada — idOportunidad inválido
        // El servicio lanza ArgumentOutOfRangeException para IDs <= 0
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("idOportunidad = 0 debe lanzar ArgumentOutOfRangeException sin consultar la BD")]
        public void ValidarLeadRn2Async_IdOportunidadCero_LanzaArgumentOutOfRangeSinConsultarRepo()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(0, IdPersonalAsignadoValido));

            _oportunidadRepoMock.Verify(r => r.ObtenerIdAlumnoPorValidacionRN2(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("idOportunidad negativo debe lanzar ArgumentOutOfRangeException sin consultar la BD")]
        public void ValidarLeadRn2Async_IdOportunidadNegativo_LanzaArgumentOutOfRangeSinConsultarRepo()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(-99, IdPersonalAsignadoValido));

            _oportunidadRepoMock.Verify(r => r.ObtenerIdAlumnoPorValidacionRN2(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("int.MinValue debe lanzar ArgumentOutOfRangeException")]
        public void ValidarLeadRn2Async_IdOportunidadIntMinValue_LanzaArgumentOutOfRange()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(int.MinValue, IdPersonalAsignadoValido));
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 1b: Guard de entrada — idPersonalAsignado inválido
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("idPersonalAsignado = 0 debe lanzar ArgumentOutOfRangeException sin consultar la BD")]
        public void ValidarLeadRn2Async_IdPersonalAsignadoCero_LanzaArgumentOutOfRangeSinConsultarRepo()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(IdOportunidadValida, 0));

            _oportunidadRepoMock.Verify(r => r.ObtenerIdAlumnoPorValidacionRN2(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("idPersonalAsignado negativo debe lanzar ArgumentOutOfRangeException sin consultar la BD")]
        public void ValidarLeadRn2Async_IdPersonalAsignadoNegativo_LanzaArgumentOutOfRangeSinConsultarRepo()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(IdOportunidadValida, -1));

            _oportunidadRepoMock.Verify(r => r.ObtenerIdAlumnoPorValidacionRN2(It.IsAny<int>()), Times.Never);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 2: SP devuelve null / IdAlumno null (no aplica RN2)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("SP retorna null → oportunidad no es de Ventas o no está en fases 41/10 → true")]
        public void ValidarLeadRn2Async_SpRetornaNull_DebeRetornarTrue()
        {
            // Arrange
            _oportunidadRepoMock
                .Setup(r => r.ObtenerIdAlumnoPorValidacionRN2(IdOportunidadValida))
                .Returns((ValidacionRn2SpResultDTO?)null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("SP retorna DTO con IdAlumno null → no aplica RN2 → true")]
        public void ValidarLeadRn2Async_SpRetornaDtoConIdAlumnoNull_DebeRetornarTrue()
        {
            // Arrange
            _oportunidadRepoMock
                .Setup(r => r.ObtenerIdAlumnoPorValidacionRN2(IdOportunidadValida))
                .Returns(new ValidacionRn2SpResultDTO { IdAlumno = null, Correo = "a@b.com", Telefono = "987654321" });

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 3: Sin datos de contacto normalizables (skip búsqueda LIKE)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Teléfono null y correo null → skip LIKE → count=1 → true")]
        public void ValidarLeadRn2Async_SinTelefonoNiCorreo_SkipLike_ConUnaOportunidad_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Teléfono vacío y correo vacío → skip LIKE → count=1 → true")]
        public void ValidarLeadRn2Async_TelefonoYCorreoVacios_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "", correo: "   ", idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Teléfono placeholder '000000000' → normaliza a null → skip LIKE → true")]
        public void ValidarLeadRn2Async_TelefonoPlaceholderCeros_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "000000000", correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Teléfono con todos dígitos iguales '88888888' → placeholder → skip LIKE → true")]
        public void ValidarLeadRn2Async_TelefonoDigitoUnico_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "88888888", correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Teléfono < 6 dígitos '12345' → descartado → skip LIKE → true")]
        public void ValidarLeadRn2Async_TelefonoCortoDe5Digitos_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "12345", correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Teléfono solo letras 'abcdef' → 0 dígitos → descartado → skip LIKE → true")]
        public void ValidarLeadRn2Async_TelefonoSoloLetras_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "abcdef", correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 4: Correo inválido normalizado a null
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Correo sin '@' → normaliza a null → si tel también null → skip LIKE → true")]
        public void ValidarLeadRn2Async_CorreoSinArroba_NormalizaANull_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "sindominio.com", idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Correo muy corto 'a@b' (< 5 chars) → normaliza a null → skip LIKE → true")]
        public void ValidarLeadRn2Async_CorreoMuyCorto_NormalizaANull_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "a@b", idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Correo con parte local vacía '@dominio.com' → normaliza a null → skip LIKE → true")]
        public void ValidarLeadRn2Async_CorreoParteLocalVacia_NormalizaANull_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "@dominio.com", idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        [TestMethod]
        [Description("Correo con dominio vacío 'usuario@' → normaliza a null → skip LIKE → true")]
        public void ValidarLeadRn2Async_CorreoDominioVacio_NormalizaANull_SkipLike_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "usuario@", idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()), Times.Never);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 5: Normalización de teléfono (prefijo de país)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Teléfono con prefijo país embebido '51987654321', IdPais=51 → normaliza a '987654321' → busca LIKE")]
        public void ValidarLeadRn2Async_TelefonoConPrefijoPaisEmbebido_QuitaPrefijo_BuscaLike()
        {
            // Arrange — con prefijo país 51 en el número
            ConfigurarSpAlumno(telefono: "51987654321", correo: null, idPais: 51);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null), Times.Once);
        }

        [TestMethod]
        [Description("Teléfono con '+', espacios y guiones '+51 987-654-321', IdPais=51 → extrae dígitos → quita prefijo → '987654321'")]
        public void ValidarLeadRn2Async_TelefonoConCaracteresEspeciales_NormalizaYQuitaPrefijo()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "+51 987-654-321", correo: null, idPais: 51);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null), Times.Once);
        }

        [TestMethod]
        [Description("Teléfono sin prefijo '987654321', IdPais=51 → no empieza con '51' en ese orden → se usa tal cual")]
        public void ValidarLeadRn2Async_TelefonoSinPrefijoPaisNoAplicaStrip_BuscaLikeConNumeroOriginal()
        {
            // Arrange — el número no empieza con el código de país
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: 51);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null), Times.Once);
        }

        [TestMethod]
        [Description("Teléfono sin IdPais null → no se intenta quitar prefijo → se usa tal cual")]
        public void ValidarLeadRn2Async_TelefonoSinIdPais_UsaNumeroTalCual()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 6: Búsqueda LIKE sin alumnos similares
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares → lista vacía → ExistenOportunidades NO se llama → count=1 → true")]
        public void ValidarLeadRn2Async_SinAlumnosSimilares_NoLlamaExistenOportunidades_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: "test@mail.com", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("BuscarSimilares retorna null (SP falla) → se coalesca a lista vacía → count=1 → true")]
        public void ValidarLeadRn2Async_BuscarSimilaresRetornaNull_CoalescaAVacio_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns((List<AlumnoSimilarRn2DTO>?)null!);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 7: Similares que coinciden con el alumno actual (filtrado)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares solo devuelve el mismo alumno → filtrado → ExistenOportunidades NO se llama → count=1 → true")]
        public void ValidarLeadRn2Async_SimilaresSoloElMismoAlumno_Filtrado_NoLlamaExistenOportunidades()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = IdAlumnoActual } });

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("BuscarSimilares devuelve el alumno actual + otro → solo el otro pasa el filtro → ExistenOportunidades se llama")]
        public void ValidarLeadRn2Async_SimilaresConElMismoYOtro_SoloOtroFiltra_LlamaExistenOportunidades()
        {
            // Arrange
            var otroAlumno = 9999;
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = otroAlumno }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids => ids.Count == 1 && ids[0] == otroAlumno), IdPersonalAsignadoValido), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 8: Alumnos similares con oportunidades activas → BLOQUEADO
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares → otro alumno → ExistenOportunidades=true → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_SimilarConOportunidadActiva_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 9001 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        [Description("Correo duplicado con oportunidad activa en otro alumno → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_CorreoDuplicadoConOportunidadActiva_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "duplicado@empresa.com", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, "duplicado@empresa.com"))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 8888 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        [Description("Teléfono Y correo ambos con similares con oportunidades activas → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_TelefonoYCorreoConSimilarActivo_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: "test@mail.com", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 7777 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
            _oportunidadRepoMock.Verify(r => r.ContarOportunidadesPorIdAlumno(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 9: Alumnos similares sin oportunidades → continúa al PASO 4
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares → otro alumno → ExistenOportunidades=false → continúa → count=1 → true")]
        public void ValidarLeadRn2Async_SimilarSinOportunidadActiva_ContinuaAlPaso4_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 9002 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 10: Oportunidades propias del alumno (PASO 4)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Alumno sin similares + ContarOportunidades=1 (solo la actual) → true")]
        public void ValidarLeadRn2Async_SinSimilares_UnaOportunidadPropia_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        [Description("Alumno sin similares + ContarOportunidades=0 (edge case: alumno nuevo) → true")]
        public void ValidarLeadRn2Async_SinSimilares_CeroOportunidadesPropias_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(0);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        [Description("Alumno sin similares + ContarOportunidades=2 → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_SinSimilares_DosOportunidadesPropias_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(2);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        [Description("Alumno sin similares + ContarOportunidades=10 → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_SinSimilares_MuchsOportunidadesPropias_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: null, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(10);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 11: Límite MaxAlumnosSimilares (200)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares → 250 alumnos distintos → se truncan a 200 → ExistenOportunidades se llama con máx 200 IDs")]
        public void ValidarLeadRn2Async_MasDe200Similares_TruncaA200Ids()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            var similares = Enumerable.Range(1, 250)
                .Select(i => new AlumnoSimilarRn2DTO { Id = i })
                .ToList();

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(similares);

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids => ids.Count == 200), IdPersonalAsignadoValido), Times.Once);
        }

        [TestMethod]
        [Description("BuscarSimilares → exactamente 200 alumnos → pasan todos → ExistenOportunidades se llama con 200 IDs")]
        public void ValidarLeadRn2Async_ExactamenteCientoSimilares_PasanTodos()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            var similares = Enumerable.Range(1, 200)
                .Select(i => new AlumnoSimilarRn2DTO { Id = i })
                .ToList();

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(similares);

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids => ids.Count == 200), IdPersonalAsignadoValido), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 12: Deduplicación de IDs similares
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("BuscarSimilares retorna IDs duplicados → Distinct los elimina → ExistenOportunidades recibe lista deduplicada")]
        public void ValidarLeadRn2Async_SimilaresConIdsDuplicados_DeduplicaAntesDeLlamarExisten()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = 100 },
                    new AlumnoSimilarRn2DTO { Id = 100 },
                    new AlumnoSimilarRn2DTO { Id = 200 },
                    new AlumnoSimilarRn2DTO { Id = 200 },
                    new AlumnoSimilarRn2DTO { Id = 300 }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids => ids.Count == 3 && ids.Distinct().Count() == 3), IdPersonalAsignadoValido), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 13: Correo normalizado a minúsculas + trim
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Correo con mayúsculas y espacios ' TEST@MAIL.COM ' → normaliza a 'test@mail.com'")]
        public void ValidarLeadRn2Async_CorreoConMayusculasYEspacios_NormalizaAMinusculas()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "  TEST@MAIL.COM  ", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, "test@mail.com"))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, "test@mail.com"), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 14: Flujos combinados end-to-end
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Teléfono válido + correo válido → sin similares → count=1 → PASA (happy path completo)")]
        public void ValidarLeadRn2Async_TelefonoYCorreoValidos_SinSimilares_UnaOportunidad_DebeRetornarTrue()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: "alumno@gmail.com", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", "alumno@gmail.com"))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        [Description("Solo teléfono válido (correo inválido) + similar con oportunidad activa → BLOQUEADO → false")]
        public void ValidarLeadRn2Async_SoloTelefonoValido_CorreoInvalido_SimilarConOportunidad_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: "noesvalido", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo("987654321", null))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 1111 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        [Description("Solo correo válido (teléfono null) + sin similares + count=2 → BLOQUEADO por oportunidades propias → false")]
        public void ValidarLeadRn2Async_SoloCorreoValido_SinSimilares_DosOportunidadesPropias_DebeRetornarFalse()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: "solo@correo.com", idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, "solo@correo.com"))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(2);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 15: Propagación de excepciones
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("ObtenerIdAlumnoPorValidacionRN2 lanza excepción → se propaga (stack trace preservado)")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidarLeadRn2Async_ExcepcionEnSP1_SePropagaAlLlamador()
        {
            // Arrange
            _oportunidadRepoMock
                .Setup(r => r.ObtenerIdAlumnoPorValidacionRN2(IdOportunidadValida))
                .Throws(new InvalidOperationException("Error de BD en SP1"));

            // Act
            _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: manejado por [ExpectedException]
        }

        [TestMethod]
        [Description("BuscarAlumnosSimilares lanza excepción → se propaga")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidarLeadRn2Async_ExcepcionEnBuscarSimilares_SePropagaAlLlamador()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Throws(new InvalidOperationException("Error de BD en BuscarSimilares"));

            // Act
            _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: manejado por [ExpectedException]
        }

        [TestMethod]
        [Description("ExistenOportunidadesParaAlumnos lanza excepción → se propaga")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidarLeadRn2Async_ExcepcionEnExistenOportunidades_SePropagaAlLlamador()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 9001 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Throws(new InvalidOperationException("Error de BD en ExistenOportunidades"));

            // Act
            _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: manejado por [ExpectedException]
        }

        [TestMethod]
        [Description("ContarOportunidadesPorIdAlumno lanza excepción → se propaga")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidarLeadRn2Async_ExcepcionEnContarOportunidades_SePropagaAlLlamador()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido))
                .Throws(new InvalidOperationException("Error de BD en ContarOportunidades"));

            // Act
            _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: manejado por [ExpectedException]
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 16: Verificación de invocaciones (no se llama ContarOportunidades innecesariamente)
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("Cuando ya se bloquea en PASO 3 (similares con oportunidad), NO se llama ContarOportunidades")]
        public void ValidarLeadRn2Async_BloqueoEnPaso3_NoLlamaContarOportunidades()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO> { new AlumnoSimilarRn2DTO { Id = 9999 } });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
            _oportunidadRepoMock.Verify(r => r.ContarOportunidadesPorIdAlumno(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("Cuando idOportunidad es inválido, lanza excepción y ningún método del repo es llamado")]
        public void ValidarLeadRn2Async_IdInvalido_NingunMetodoRepoEsLlamado()
        {
            // Act & Assert — la excepción se lanza antes de acceder al repo
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _service.ValidarLeadRn2Async(-1, IdPersonalAsignadoValido));

            _oportunidadRepoMock.VerifyNoOtherCalls();
        }

        // ─────────────────────────────────────────────────────────────────
        // CATEGORÍA 17: IdAlumno actual repetido en la lista de similares
        // Valida que el Where(a.Id != IdAlumnoActual) elimina TODAS las
        // apariciones del alumno actual, aunque aparezca 2, 3 o más veces.
        // ─────────────────────────────────────────────────────────────────

        [TestMethod]
        [Description("IdAlumno actual aparece 3 veces en similares → todas filtradas → lista vacía → ExistenOportunidades NO se llama → count=1 → true")]
        public void ValidarLeadRn2Async_IdAlumnoActualRepetido3Veces_FiltraCompletamente_NoLlamaExistenOportunidades()
        {
            // Arrange
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            // El alumno actual aparece 3 veces — todos deben ser filtrados
            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual }
                });

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("IdAlumno actual repetido 2 veces + otro alumno repetido 2 veces → filtrado y deduplicado → ExistenOportunidades recibe solo [otroAlumno]")]
        public void ValidarLeadRn2Async_IdAlumnoActualYOtroAmbosRepetidos_FiltraYDeduplicaCorrectamente()
        {
            // Arrange
            var otroAlumno = 9999;
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            // [5000, 5000, 9999, 9999] → Where(!=5000) → [9999, 9999] → Distinct → [9999]
            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = otroAlumno },
                    new AlumnoSimilarRn2DTO { Id = otroAlumno }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: ExistenOportunidades recibe exactamente [9999], sin el actual y sin duplicados
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids => ids.Count == 1 && ids[0] == otroAlumno), IdPersonalAsignadoValido), Times.Once);
        }

        [TestMethod]
        [Description("IdAlumno actual repetido 3 veces + múltiples otros también repetidos → todos deduplicados correctamente")]
        public void ValidarLeadRn2Async_IdAlumnoActualRepetidoYVariosOtrosRepetidos_DeduplicaCorrectamente()
        {
            // Arrange
            // [5000, 5000, 5000, 9999, 9999, 8888, 8888, 7777] → Where(!=5000) → [9999,9999,8888,8888,7777] → Distinct → [9999,8888,7777]
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = 9999 },
                    new AlumnoSimilarRn2DTO { Id = 9999 },
                    new AlumnoSimilarRn2DTO { Id = 8888 },
                    new AlumnoSimilarRn2DTO { Id = 8888 },
                    new AlumnoSimilarRn2DTO { Id = 7777 }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: ExistenOportunidades recibe exactamente 3 IDs distintos, sin IdAlumnoActual
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(
                It.Is<List<int>>(ids =>
                    ids.Count == 3 &&
                    !ids.Contains(IdAlumnoActual) &&
                    ids.Contains(9999) &&
                    ids.Contains(8888) &&
                    ids.Contains(7777)), IdPersonalAsignadoValido), Times.Once);
        }

        [TestMethod]
        [Description("IdAlumno actual repetido 2 veces + otro con oportunidad activa → se filtra el actual, el otro bloquea → false")]
        public void ValidarLeadRn2Async_IdAlumnoActualRepetido_OtroConOportunidadActiva_DebeBloquear()
        {
            // Arrange
            // [5000, 5000, 9999] → Where(!=5000) → [9999] → ExistenOportunidades=true → BLOQUEADO
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = 9999 }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(true);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsFalse(resultado);
            // El bloqueo ocurre en PASO 3 → ContarOportunidades nunca se llama
            _oportunidadRepoMock.Verify(r => r.ContarOportunidadesPorIdAlumno(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        [Description("IdAlumno actual repetido 2 veces + otro sin oportunidad activa → filtra actual, otro no bloquea → continúa PASO 4 → count=1 → true")]
        public void ValidarLeadRn2Async_IdAlumnoActualRepetido_OtroSinOportunidadActiva_ContinuaPaso4_True()
        {
            // Arrange
            // [5000, 5000, 9999] → Where(!=5000) → [9999] → ExistenOportunidades=false → PASO 4 → count=1 → true
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = 9999 }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido), Times.Once);
        }

        [TestMethod]
        [Description("IdAlumno actual repetido 2 veces + otro sin oportunidad activa → continúa PASO 4 → count=2 → BLOQUEADO por oportunidades propias → false")]
        public void ValidarLeadRn2Async_IdAlumnoActualRepetido_OtroSinOportunidad_MultiplesOportunidadesPropias_DebeBloquear()
        {
            // Arrange
            // PASO 3 no bloquea → PASO 4: count=2 → BLOQUEADO
            ConfigurarSpAlumno(telefono: "987654321", correo: null, idPais: null);

            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()))
                .Returns(new List<AlumnoSimilarRn2DTO>
                {
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = IdAlumnoActual },
                    new AlumnoSimilarRn2DTO { Id = 9999 }
                });

            _oportunidadRepoMock
                .Setup(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), IdPersonalAsignadoValido))
                .Returns(false);

            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido)).Returns(2);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida, IdPersonalAsignadoValido);

            // Assert: no bloqueó en PASO 3 pero sí en PASO 4
            Assert.IsFalse(resultado);
            _oportunidadRepoMock.Verify(r => r.ExistenOportunidadesParaAlumnos(It.IsAny<List<int>>(), It.IsAny<int>()), Times.Once);
            _oportunidadRepoMock.Verify(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual, IdPersonalAsignadoValido), Times.Once);
        }

        // ─────────────────────────────────────────────────────────────────
        // Helper privado
        // ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// Configura el mock de ObtenerIdAlumnoPorValidacionRN2 retornando un alumno válido.
        /// </summary>
        private void ConfigurarSpAlumno(string? telefono, string? correo, int? idPais)
        {
            _oportunidadRepoMock
                .Setup(r => r.ObtenerIdAlumnoPorValidacionRN2(IdOportunidadValida))
                .Returns(new ValidacionRn2SpResultDTO
                {
                    IdAlumno = IdAlumnoActual,
                    Telefono = telefono,
                    Correo = correo,
                    IdPais = idPais
                });
        }
    }
}
