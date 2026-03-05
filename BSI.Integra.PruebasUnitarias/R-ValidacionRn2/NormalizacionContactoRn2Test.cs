using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BSI.Integra.PruebasUnitarias.Comercial.Rn2
{
    /// <summary>
    /// Pruebas de normalización de teléfono y correo dentro de ValidarLeadRn2Async.
    ///
    /// LÓGICA DE NORMALIZACIÓN DE TELÉFONO:
    ///   1. Extrae solo dígitos (elimina +, espacios, guiones, paréntesis, puntos, letras)
    ///   2. Si IdPais tiene valor → prefijo = IdPais.ToString()
    ///      Si el número empieza con ese prefijo Y queda al menos 1 dígito → se quita el prefijo
    ///   3. Si queda como placeholder (todos dígitos iguales) → descarta
    ///   4. Si queda con menos de 6 dígitos → descarta
    ///   5. En otro caso → se pasa al SP de búsqueda LIKE
    ///
    /// LÓGICA DE NORMALIZACIÓN DE CORREO:
    ///   1. Trim + ToLower
    ///   2. Debe contener '@' y tener al menos 5 caracteres
    ///   3. Split('@') debe dar exactamente 2 partes ambas no vacías
    ///   4. En otro caso → descarta
    ///
    /// ESTRATEGIA DE TEST: los métodos de normalización son privados, por lo que
    /// se verifica el comportamiento observable: qué argumentos recibe
    /// BuscarAlumnosSimilaresPorCelularOCorreo, o si no se llama cuando el contacto
    /// es descartado por la normalización.
    ///
    /// CÓDIGOS DE PAÍS LATINOAMÉRICA (ITU-T E.164):
    ///   1 digit : Dominican Republic (+1)
    ///   2 digits: Peru(51) Chile(56) Colombia(57) Mexico(52) Argentina(54)
    ///             Venezuela(58) Brazil(55) Cuba(53)
    ///   3 digits: Bolivia(591) Ecuador(593) Uruguay(598) Paraguay(595)
    ///             Panama(507) Costa Rica(506) Guatemala(502) Honduras(504)
    ///             El Salvador(503) Nicaragua(505)
    /// </summary>
    [TestClass]
    public class NormalizacionContactoRn2Test
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = null!;
        private Mock<IOportunidadRepository> _oportunidadRepoMock = null!;
        private ValidacionRn2Service _service = null!;

        private const int IdOportunidadValida = 2000;
        private const int IdAlumnoActual = 6000;

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

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN A: TELÉFONO CON PREFIJO EMBEBIDO — 2 DÍGITOS DE CÓDIGO DE PAÍS
        // ═════════════════════════════════════════════════════════════════

        /// <summary>
        /// Países con código de país de 2 dígitos donde el número YA viene con el código.
        /// El servicio debe detectar el prefijo vía IdPais, quitarlo y buscar con el número local.
        ///
        /// Formato DataRow: telefonoRaw | idPais | expectedNormalizado
        /// </summary>
        [DataTestMethod]
        [DataRow("+51987654321",   51, "987654321",    "Peru: +51 + 9 dígitos locales")]
        [DataRow("51987654321",    51, "987654321",    "Peru: 51 sin + prefijo")]
        [DataRow("+56912345678",   56, "912345678",    "Chile: +56 + 9 dígitos")]
        [DataRow("56912345678",    56, "912345678",    "Chile: 56 sin +")]
        [DataRow("+573001234567",  57, "3001234567",   "Colombia: +57 + 10 dígitos")]
        [DataRow("573001234567",   57, "3001234567",   "Colombia: 57 sin +")]
        [DataRow("+521234567890",  52, "1234567890",   "Mexico: +52 + 10 dígitos")]
        [DataRow("521234567890",   52, "1234567890",   "Mexico: 52 sin +")]
        [DataRow("+541112345678",  54, "1112345678",   "Argentina: +54 + 10 dígitos")]
        [DataRow("541112345678",   54, "1112345678",   "Argentina: 54 sin +")]
        [DataRow("+5511912345678", 55, "11912345678",  "Brazil: +55 + 11 dígitos")]
        [DataRow("5511912345678",  55, "11912345678",  "Brazil: 55 sin +")]
        [DataRow("+584141234567",  58, "4141234567",   "Venezuela: +58 + 10 dígitos")]
        [DataRow("584141234567",   58, "4141234567",   "Venezuela: 58 sin +")]
        [DataRow("+535812345678",  53, "5812345678",   "Cuba: +53 + 8 dígitos locales")]
        public void NormalizarTelefono_PrefijoDosDigitosEmbebido_QuitaPrefijoYBuscaLike(
            string telefonoRaw, int idPais, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado con el número normalizado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN B: TELÉFONO CON PREFIJO EMBEBIDO — 3 DÍGITOS DE CÓDIGO DE PAÍS
        // ═════════════════════════════════════════════════════════════════

        /// <summary>
        /// Países con código de país de 3 dígitos donde el número YA viene con el código embebido.
        /// </summary>
        [DataTestMethod]
        [DataRow("59170123456",    591, "70123456",    "Bolivia: 591 + 8 dígitos")]
        [DataRow("+59170123456",   591, "70123456",    "Bolivia: +591 + 8 dígitos")]
        [DataRow("+593991234567",  593, "991234567",   "Ecuador: +593 + 9 dígitos")]
        [DataRow("593991234567",   593, "991234567",   "Ecuador: 593 sin +")]
        [DataRow("+59891234567",   598, "91234567",    "Uruguay: +598 + 8 dígitos")]
        [DataRow("59891234567",    598, "91234567",    "Uruguay: 598 sin +")]
        [DataRow("+595971234567",  595, "971234567",   "Paraguay: +595 + 9 dígitos")]
        [DataRow("595971234567",   595, "971234567",   "Paraguay: 595 sin +")]
        [DataRow("+50761234567",   507, "61234567",    "Panama: +507 + 8 dígitos")]
        [DataRow("50761234567",    507, "61234567",    "Panama: 507 sin +")]
        [DataRow("+50688001234",   506, "88001234",    "Costa Rica: +506 + 8 dígitos")]
        [DataRow("50688001234",    506, "88001234",    "Costa Rica: 506 sin +")]
        [DataRow("+50212345678",   502, "12345678",    "Guatemala: +502 + 8 dígitos")]
        [DataRow("50298765432",    502, "98765432",    "Guatemala: 502 sin +")]
        [DataRow("+50498765432",   504, "98765432",    "Honduras: +504 + 8 dígitos")]
        [DataRow("+50312345678",   503, "12345678",    "El Salvador: +503 + 8 dígitos")]
        [DataRow("+50512345678",   505, "12345678",    "Nicaragua: +505 + 8 dígitos")]
        public void NormalizarTelefono_PrefijoTresDigitosEmbebido_QuitaPrefijoYBuscaLike(
            string telefonoRaw, int idPais, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado con el número normalizado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN C: TELÉFONO CON CÓDIGO DE PAÍS 1 DÍGITO (CARIBE / RD)
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("+18001234567", 1, "8001234567", "Republica Dominicana: +1 + 10 dígitos")]
        [DataRow("18291234567",  1, "8291234567", "Republica Dominicana: 1 + 9 dígitos (area 829)")]
        [DataRow("+18491234567", 1, "8491234567", "Republica Dominicana: +1 + 9 dígitos (area 849)")]
        public void NormalizarTelefono_PrefijoUnDigito_QuitaPrefijoYBuscaLike(
            string telefonoRaw, int idPais, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado con el número normalizado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN D: TELÉFONO SIN PREFIJO (número local solamente)
        // El número NO empieza con el código de país → no se quita nada
        // ═════════════════════════════════════════════════════════════════

        /// <summary>
        /// Cuando el número en la BD es solo el número local (sin código de país),
        /// el servicio lo usa tal cual sin quitar ningún prefijo.
        /// </summary>
        [DataTestMethod]
        [DataRow("987654321",   51,  "987654321",   "Peru: número local sin prefijo 51")]
        [DataRow("912345678",   56,  "912345678",   "Chile: número local sin prefijo 56")]
        [DataRow("3001234567",  57,  "3001234567",  "Colombia: número local sin prefijo 57")]
        [DataRow("1234567890",  52,  "1234567890",  "Mexico: número local sin prefijo 52")]
        [DataRow("1112345678",  54,  "1112345678",  "Argentina: número local sin prefijo 54")]
        [DataRow("4141234567",  58,  "4141234567",  "Venezuela: número local sin prefijo 58")]
        [DataRow("11912345678", 55,  "11912345678", "Brazil: número local sin prefijo 55")]
        [DataRow("70123456",    591, "70123456",    "Bolivia: número local sin prefijo 591")]
        [DataRow("991234567",   593, "991234567",   "Ecuador: número local sin prefijo 593")]
        [DataRow("91234567",    598, "91234567",    "Uruguay: número local sin prefijo 598")]
        [DataRow("971234567",   595, "971234567",   "Paraguay: número local sin prefijo 595")]
        [DataRow("61234567",    507, "61234567",    "Panama: número local sin prefijo 507")]
        [DataRow("88001234",    506, "88001234",    "Costa Rica: número local sin prefijo 506")]
        public void NormalizarTelefono_SinPrefijoEmbebido_UsaNumeroLocalTalCual(
            string telefonoRaw, int idPais, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado con el número local para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN E: TELÉFONO FORMATEADO (caracteres especiales en el campo)
        // El campo puede venir con +, espacios, paréntesis, guiones, puntos
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("(51) 987-654-321",       51,  "987654321",   "Peru: parentesis, espacios y guiones")]
        [DataRow("+57 300 123 4567",        57,  "3001234567",  "Colombia: + y espacios")]
        [DataRow("+54 (11) 1234-5678",      54,  "1112345678",  "Argentina: formato local con area code")]
        [DataRow("+507 6123.4567",          507, "61234567",    "Panama: + y puntos")]
        [DataRow("+593 99 123 4567",        593, "991234567",   "Ecuador: + y espacios agrupados")]
        [DataRow("+56 9 1234 5678",         56,  "912345678",   "Chile: + y espacios entre grupos")]
        [DataRow("+55 11 9 1234-5678",      55,  "11912345678", "Brazil: formato con 9 de celular")]
        [DataRow("+52 (55) 1234-5678",      52,  "5512345678",  "Mexico CDMX: area 55 sin prefijo 52 strip")]
        [DataRow("0051 987 654 321",        51,  "0051987654321", "Peru: marcación internacional 00+código → no strip")]
        public void NormalizarTelefono_ConFormatoEspecial_ExtractaDigitosYNormaliza(
            string telefonoRaw, int idPais, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado correctamente para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN F: TELÉFONO SIN IdPais (null) — no se intenta quitar prefijo
        // El número completo (con código de país embebido) va al LIKE tal cual
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("+51987654321",  "51987654321",  "Peru sin IdPais: lleva dígitos completos al LIKE")]
        [DataRow("+56912345678",  "56912345678",  "Chile sin IdPais: lleva dígitos completos al LIKE")]
        [DataRow("+573001234567", "573001234567", "Colombia sin IdPais: lleva dígitos completos al LIKE")]
        [DataRow("59170123456",   "59170123456",  "Bolivia sin IdPais: número completo al LIKE")]
        [DataRow("+50761234567",  "50761234567",  "Panama sin IdPais: número completo al LIKE")]
        public void NormalizarTelefono_SinIdPais_UsaNumeroCompletoSinQuitarPrefijo(
            string telefonoRaw, string expectedNormalizado, string descripcion)
        {
            // Arrange — IdPais = null
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: null);
            ConfigurarBuscarSimilaresYContar(expectedNormalizado, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedNormalizado, null),
                Times.Once, $"LIKE no fue llamado con número completo para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN G: TELÉFONO DESCARTADO TRAS QUITAR PREFIJO (queda < 6 dígitos)
        // Si el número local resultante tiene menos de 6 dígitos → null → no busca LIKE
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("+5931234",  593, "Ecuador: strip 593 deja 1234 (4 dígitos) → descarta")]
        [DataRow("+59112",    591, "Bolivia: strip 591 deja 12 (2 dígitos) → descarta")]
        [DataRow("+59512",    595, "Paraguay: strip 595 deja 12 (2 dígitos) → descarta")]
        [DataRow("+50612",    506, "Costa Rica: strip 506 deja 12 (2 dígitos) → descarta")]
        [DataRow("+5112",     51,  "Peru: strip 51 deja 12 (2 dígitos) → descarta")]
        [DataRow("+5612345",  56,  "Chile: strip 56 deja 12345 (5 dígitos) → descarta (< 6)")]
        public void NormalizarTelefono_StripDejaPocosDígitos_DescartaYNoLlamaLike(
            string telefonoRaw, int idPais, string descripcion)
        {
            // Arrange — correo null también → sin contacto normalizable → no llama LIKE
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: null, idPais: idPais);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()),
                Times.Never, $"LIKE fue llamado cuando no debería para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN H: AMBIGÜEDAD — número local que comienza con el código de país
        // Documenta el comportamiento real: el servicio SIEMPRE quita el prefijo
        // si el número empieza con él, aunque sea un número local que coincide por azar.
        // ═════════════════════════════════════════════════════════════════

        [TestMethod]
        [Description("Peru IdPais=51: número local '51234567' empieza con '51' → strip → '234567' (6 dígitos, límite mínimo)")]
        public void NormalizarTelefono_NumeroLocalEmpiezaConCodigo_StripPorCoincidencia_DocumentaComportamiento()
        {
            // Arrange — "51234567" es un número de 8 dígitos que empieza con "51"
            // El servicio lo interpreta como "número con prefijo" y quita "51"
            ConfigurarSpAlumno(telefono: "51234567", correo: null, idPais: 51);
            ConfigurarBuscarSimilaresYContar("234567", correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert — documenta el comportamiento: "234567" va al LIKE
            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo("234567", null), Times.Once);
        }

        [TestMethod]
        [Description("Argentina IdPais=54: '549111234567' — quita '54' → '9111234567' (formato celular con 9 prefijo local Argentina)")]
        public void NormalizarTelefono_ArgentinaFormatoCelularCon9_QuitaPrefijoPaisCorrectamente()
        {
            // Formato argentino celular: +549 + area + número = +549 11 1234-5678
            ConfigurarSpAlumno(telefono: "+549111234567", correo: null, idPais: 54);
            ConfigurarBuscarSimilaresYContar("9111234567", correo: null);

            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo("9111234567", null), Times.Once);
        }

        [TestMethod]
        [Description("Mexico IdPais=52: '5215551234567' — quita '52' → '15551234567' (formato LD Mexico)")]
        public void NormalizarTelefono_MexicoFormatoLD_QuitaPrefijoPaisCorrectamente()
        {
            ConfigurarSpAlumno(telefono: "5215551234567", correo: null, idPais: 52);
            ConfigurarBuscarSimilaresYContar("15551234567", correo: null);

            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo("15551234567", null), Times.Once);
        }

        [TestMethod]
        [Description("Ecuador IdPais=593 número sin prefijo pero empieza con '59' — NO se quita nada (no empieza con '593')")]
        public void NormalizarTelefono_EcuadorNumeroCasualmenteEmpieza59_NoStrip()
        {
            // "599xxxxxxx" no empieza con "593" → sin strip
            ConfigurarSpAlumno(telefono: "599123456", correo: null, idPais: 593);
            ConfigurarBuscarSimilaresYContar("599123456", correo: null);

            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            Assert.IsTrue(resultado);
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo("599123456", null), Times.Once);
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN I: CORREO NORMALIZADO — FORMATOS VÁLIDOS LATINOAMÉRICA
        // ═════════════════════════════════════════════════════════════════

        /// <summary>
        /// Verifica que correos en distintos formatos y TLDs latinoamericanos
        /// se normalizan correctamente (trim + lowercase) y se pasan al LIKE.
        ///
        /// Formato DataRow: correoRaw | expectedNormalizado
        /// </summary>
        [DataTestMethod]
        [DataRow("usuario@gmail.com",             "usuario@gmail.com",             "Gmail estándar")]
        [DataRow("juan.perez@empresa.com.pe",      "juan.perez@empresa.com.pe",     "Peru .pe TLD con punto en local")]
        [DataRow("maria@mail.cl",                  "maria@mail.cl",                 "Chile .cl TLD")]
        [DataRow("carlos@empresa.co",              "carlos@empresa.co",             "Colombia .co TLD")]
        [DataRow("info@empresa.com.mx",            "info@empresa.com.mx",           "Mexico .mx TLD")]
        [DataRow("ventas@empresa.com.ar",          "ventas@empresa.com.ar",         "Argentina .ar TLD")]
        [DataRow("suporte@empresa.com.br",         "suporte@empresa.com.br",        "Brazil .br TLD")]
        [DataRow("user@empresa.bo",                "user@empresa.bo",               "Bolivia .bo TLD")]
        [DataRow("user@empresa.ec",                "user@empresa.ec",               "Ecuador .ec TLD")]
        [DataRow("user@empresa.uy",                "user@empresa.uy",               "Uruguay .uy TLD")]
        [DataRow("user@empresa.py",                "user@empresa.py",               "Paraguay .py TLD")]
        [DataRow("user@empresa.pa",                "user@empresa.pa",               "Panama .pa TLD")]
        [DataRow("user@empresa.cr",                "user@empresa.cr",               "Costa Rica .cr TLD")]
        [DataRow("user@empresa.do",                "user@empresa.do",               "Rep Dominicana .do TLD")]
        [DataRow("user@empresa.gt",                "user@empresa.gt",               "Guatemala .gt TLD")]
        [DataRow("user@empresa.hn",                "user@empresa.hn",               "Honduras .hn TLD")]
        [DataRow("user@empresa.sv",                "user@empresa.sv",               "El Salvador .sv TLD")]
        [DataRow("user@empresa.ni",                "user@empresa.ni",               "Nicaragua .ni TLD")]
        [DataRow("user@empresa.ve",                "user@empresa.ve",               "Venezuela .ve TLD")]
        [DataRow("user@empresa.cu",                "user@empresa.cu",               "Cuba .cu TLD")]
        public void NormalizarCorreo_FormatosValidosLatam_NormalizaYBuscaLike(
            string correoRaw, string expectedNormalizado, string descripcion)
        {
            // Arrange — teléfono null para que solo el correo vaya al LIKE
            ConfigurarSpAlumno(telefono: null, correo: correoRaw, idPais: null);
            ConfigurarBuscarSimilaresYContar(telefono: null, correo: expectedNormalizado);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, expectedNormalizado),
                Times.Once, $"LIKE no fue llamado con el correo normalizado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN J: CORREO — NORMALIZACIÓN DE CASE Y ESPACIOS
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("JUAN@EMPRESA.COM.PE",        "juan@empresa.com.pe",        "Todo mayúsculas → lowercase")]
        [DataRow("Juan.Perez@Gmail.COM",        "juan.perez@gmail.com",       "Mixto → lowercase")]
        [DataRow("  usuario@empresa.cl  ",      "usuario@empresa.cl",         "Espacios leading/trailing → trim")]
        [DataRow("  MARIA@MAIL.CL  ",           "maria@mail.cl",              "Espacios + mayúsculas → trim+lowercase")]
        [DataRow("Carlos.Lopez@EMPRESA.COM.AR", "carlos.lopez@empresa.com.ar","Nombre mixto + dominio caps")]
        [DataRow("\tcontacto@empresa.mx\t",     "contacto@empresa.mx",        "Tabs → trim")]
        public void NormalizarCorreo_CaseYEspacios_TrimYLowercase(
            string correoRaw, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: correoRaw, idPais: null);
            ConfigurarBuscarSimilaresYContar(telefono: null, correo: expectedNormalizado);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, expectedNormalizado),
                Times.Once, $"LIKE no fue llamado con correo normalizado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN K: CORREO — CARACTERES ESPECIALES VÁLIDOS EN PARTE LOCAL
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("juan.perez@gmail.com",    "juan.perez@gmail.com",    "Punto en local")]
        [DataRow("user+tag@gmail.com",      "user+tag@gmail.com",      "Plus en local (Split solo divide en @)")]
        [DataRow("user_name@company.com",   "user_name@company.com",   "Guión bajo en local")]
        [DataRow("user123@hotmail.com",     "user123@hotmail.com",     "Números en local")]
        [DataRow("user-name@company.com",   "user-name@company.com",   "Guión en local")]
        [DataRow("user@sub.domain.co",      "user@sub.domain.co",      "Subdominio en dominio")]
        [DataRow("user@my-company.com.pe",  "user@my-company.com.pe",  "Guión en dominio + .pe")]
        [DataRow("a@b.c",                   "a@b.c",                   "Mínimo válido exactamente 5 chars")]
        public void NormalizarCorreo_CaracteresEspecialesEnLocal_ValidoYBuscaLike(
            string correoRaw, string expectedNormalizado, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: null, correo: correoRaw, idPais: null);
            ConfigurarBuscarSimilaresYContar(telefono: null, correo: expectedNormalizado);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, expectedNormalizado),
                Times.Once, $"LIKE no fue llamado para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN L: CORREO INVÁLIDO → null → no llama LIKE (si teléfono también null)
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("sindominio.com",           "Sin @ en el correo")]
        [DataRow("a@b",                      "Solo 3 caracteres (< 5 mínimo)")]
        [DataRow("a@bc",                     "Solo 4 caracteres (< 5 mínimo)")]
        [DataRow("user@domain@com",          "Doble @ → split da 3 partes")]
        [DataRow("a@@dominio.com",           "Doble @ consecutivo → 3+ partes")]
        [DataRow("@dominio.com",             "Parte local vacía")]
        [DataRow("usuario@",                 "Dominio vacío")]
        [DataRow("",                         "Cadena vacía")]
        [DataRow("    ",                     "Solo espacios")]
        public void NormalizarCorreo_FormatoInvalido_DescartaYNoLlamaLike(
            string correoRaw, string descripcion)
        {
            // Arrange — teléfono null también → sin contacto normalizable → no llama LIKE
            ConfigurarSpAlumno(telefono: null, correo: correoRaw, idPais: null);
            _oportunidadRepoMock.Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual)).Returns(1);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(It.IsAny<string?>(), It.IsAny<string?>()),
                Times.Never, $"LIKE fue llamado cuando no debería para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN M: COMBINACIONES — TELÉFONO + CORREO AMBOS VÁLIDOS
        // Verifica que ambos se pasan correctamente al SP de búsqueda LIKE
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        [DataRow("+51987654321",  51,  "juan@empresa.com.pe",   "987654321", "juan@empresa.com.pe",   "Peru: tel+correo")]
        [DataRow("+56912345678",  56,  "maria@mail.cl",          "912345678", "maria@mail.cl",          "Chile: tel+correo")]
        [DataRow("+573001234567", 57,  "CARLOS@EMPRESA.CO",      "3001234567","carlos@empresa.co",      "Colombia: tel+correo+maiúscula")]
        [DataRow("59170123456",   591, "user@empresa.bo",        "70123456",  "user@empresa.bo",        "Bolivia: tel 3-digit+correo")]
        [DataRow("+50761234567",  507, "CONTACTO@EMPRESA.PA",    "61234567",  "contacto@empresa.pa",    "Panama: tel+correo mayúsculas")]
        public void NormalizarContacto_TelefonoYCorreoAmbosValidos_PasaAmbosAlLike(
            string telefonoRaw, int idPais, string correoRaw,
            string expectedTel, string expectedCorreo, string descripcion)
        {
            // Arrange
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: correoRaw, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedTel, expectedCorreo);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedTel, expectedCorreo),
                Times.Once, $"LIKE no fue llamado con ambos parámetros correctos para: {descripcion}");
        }

        [DataTestMethod]
        // "+511234"  → dígitos "511234" → strip "51" → "1234"  → 4 dígitos < 6 → null
        // "+5601234" → dígitos "5601234" → strip "56" → "01234" → 5 dígitos < 6 → null
        // "+5931234" → dígitos "5931234" → strip "593" → "1234"  → 4 dígitos < 6 → null
        [DataRow("+511234",  51,  "juan@empresa.pe",  "Peru: strip deja 4 digs + correo válido")]
        [DataRow("+5601234", 56,  "maria@mail.cl",    "Chile: strip deja 5 digs + correo válido")]
        [DataRow("+5931234", 593, "user@empresa.ec",  "Ecuador: strip deja 4 digs + correo válido")]
        public void NormalizarContacto_TelefonoInvalidoCorreoValido_PasaSoloCorreoAlLike(
            string telefonoRaw, int idPais, string correoRaw, string descripcion)
        {
            // El teléfono queda inválido tras strip pero el correo es válido
            // → LIKE se llama con (null, correoNormalizado)
            var correoNormalizado = correoRaw.ToLower().Trim();
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: correoRaw, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(telefono: null, correo: correoNormalizado);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(null, correoNormalizado),
                Times.Once, $"LIKE no fue llamado con solo el correo para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // SECCIÓN N: CORREO VÁLIDO + TELÉFONO INVÁLIDO POR CORREO INVÁLIDO + TELÉFONO VÁLIDO
        // ═════════════════════════════════════════════════════════════════

        [DataTestMethod]
        // correos inválidos: sin @ → null | 4 chars < 5 → null
        [DataRow("+51987654321", 51,  "sindominio.com",  "987654321", "Peru: tel válido + correo sin @")]
        [DataRow("+57300123456", 57,  "noemail.com",     "300123456", "Colombia: tel válido + correo sin @")]
        [DataRow("59170123456",  591, "noemail",         "70123456",  "Bolivia: tel válido + correo sin @")]
        [DataRow("987654321",    51,  "a@bc",            "987654321", "Peru: tel válido + correo 4 chars < 5")]
        public void NormalizarContacto_TelefonoValidoCorreoInvalido_PasaSoloTelefonoAlLike(
            string telefonoRaw, int idPais, string correoRaw, string expectedTel, string descripcion)
        {
            // El correo queda null tras normalización, pero el teléfono es válido
            // → LIKE se llama con (telNormalizado, null)
            ConfigurarSpAlumno(telefono: telefonoRaw, correo: correoRaw, idPais: idPais);
            ConfigurarBuscarSimilaresYContar(expectedTel, correo: null);

            // Act
            var resultado = _service.ValidarLeadRn2Async(IdOportunidadValida);

            // Assert
            Assert.IsTrue(resultado, $"Falló para: {descripcion}");
            _oportunidadRepoMock.Verify(
                r => r.BuscarAlumnosSimilaresPorCelularOCorreo(expectedTel, null),
                Times.Once, $"LIKE no fue llamado con solo el teléfono para: {descripcion}");
        }

        // ═════════════════════════════════════════════════════════════════
        // Helpers privados
        // ═════════════════════════════════════════════════════════════════

        /// <summary>Configura el SP para retornar datos del alumno con los valores indicados.</summary>
        private void ConfigurarSpAlumno(string? telefono, string? correo, int? idPais)
        {
            _oportunidadRepoMock
                .Setup(r => r.ObtenerIdAlumnoPorValidacionRN2(IdOportunidadValida))
                .Returns(new ValidacionRn2SpResultDTO
                {
                    IdAlumno  = IdAlumnoActual,
                    Telefono  = telefono,
                    Correo    = correo,
                    IdPais    = idPais
                });
        }

        /// <summary>
        /// Configura la búsqueda LIKE para retornar lista vacía (sin similares)
        /// y ContarOportunidades = 1 (sin conflicto propio), para que el resultado
        /// del flujo completo sea true y podamos verificar solo el argumento del LIKE.
        /// </summary>
        private void ConfigurarBuscarSimilaresYContar(string? telefono, string? correo)
        {
            _oportunidadRepoMock
                .Setup(r => r.BuscarAlumnosSimilaresPorCelularOCorreo(telefono, correo))
                .Returns(new List<AlumnoSimilarRn2DTO>());

            _oportunidadRepoMock
                .Setup(r => r.ContarOportunidadesPorIdAlumno(IdAlumnoActual))
                .Returns(1);
        }
    }
}
