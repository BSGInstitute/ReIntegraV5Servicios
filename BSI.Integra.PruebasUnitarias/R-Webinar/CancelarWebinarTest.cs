using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Moq;

namespace BSI.Integra.PruebasUnitarias;

[TestClass]
public class CancelarWebinarTest
{
    private AsistenciaWebinarService _service;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    [TestInitialize]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new AsistenciaWebinarService(_unitOfWorkMock.Object);
    }
    [TestMethod]
    public void CancelarWebinar_CuandoTodoEsValido_RetornarTrue()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock.Setup(r => r.ObtenerPorId(It.IsAny<int>())).Returns(new PEspecificoSesion { EsWebinarConfirmado = true });
        sesionRepoMock.Setup(r => r.Update(It.IsAny<PEspecificoSesion>()));
        sesionRepoMock
        .Setup(r => r.ObtenerDetalleSesionesPorAlumnosFiltrado(It.IsAny<SesionFiltroDTO>()))
        .Returns(new List<DetalleSesionesAlumnosDTO>
        {
                new DetalleSesionesAlumnosDTO
                {
                    Confirmo = "CONFIRMADO",
                    Email = "ctumir@bsginstitute.com"
                },
                new DetalleSesionesAlumnosDTO
                {
                    Confirmo = "CONFIRMADO",
                    Email = null
                }
        });
        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);
        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };
        var result = _service.CancelarWebinar(dto, "usuarioPrueba");
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void CancelarWebinar_SinEnvioDeCorreo_RetornarTrue()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock.Setup(r => r.ObtenerPorId(It.IsAny<int>())).Returns(new PEspecificoSesion { EsWebinarConfirmado = true });
        sesionRepoMock.Setup(r => r.Update(It.IsAny<PEspecificoSesion>()));
        sesionRepoMock
        .Setup(r => r.ObtenerDetalleSesionesPorAlumnosFiltrado(It.IsAny<SesionFiltroDTO>()))
        .Returns(new List<DetalleSesionesAlumnosDTO>
        {
            new DetalleSesionesAlumnosDTO
            {
                Confirmo = "CONFIRMADO",
                Email = null
            }
        });
        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);
        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };
        var result = _service.CancelarWebinar(dto, "usuarioPrueba");
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void CancelarWebinar_NoExistenAlumnosConfirmados_RetornarTrue()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock.Setup(r => r.ObtenerPorId(It.IsAny<int>())).Returns(new PEspecificoSesion { EsWebinarConfirmado = true });
        sesionRepoMock.Setup(r => r.Update(It.IsAny<PEspecificoSesion>()));
        sesionRepoMock
        .Setup(r => r.ObtenerDetalleSesionesPorAlumnosFiltrado(It.IsAny<SesionFiltroDTO>()))
        .Returns(new List<DetalleSesionesAlumnosDTO>
        {
            new DetalleSesionesAlumnosDTO
            {
                Confirmo = "NO CONFIRMADO",
                Email = "ctumir@bsginstitute.com"
            }
        });
        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);
        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };
        var result = _service.CancelarWebinar(dto, "usuarioPrueba");
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void CancelarWebinar_ExistenCorreosDuplicados_RetornarTrue()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock.Setup(r => r.ObtenerPorId(It.IsAny<int>())).Returns(new PEspecificoSesion { EsWebinarConfirmado = true });
        sesionRepoMock.Setup(r => r.Update(It.IsAny<PEspecificoSesion>()));
        sesionRepoMock
        .Setup(r => r.ObtenerDetalleSesionesPorAlumnosFiltrado(It.IsAny<SesionFiltroDTO>()))
        .Returns(new List<DetalleSesionesAlumnosDTO>
        {
            new DetalleSesionesAlumnosDTO
            {
                Confirmo = "CONFIRMADO",
                Email = "ctumir@bsginstitute.com"
            },
            new DetalleSesionesAlumnosDTO
            {
                Confirmo = "CONFIRMADO",
                Email = "ctumir@bsginstitute.com"
            },
            new DetalleSesionesAlumnosDTO
            {
                Confirmo = "CONFIRMADO",
                Email = "ctumir@bsginstitute.com"
            }
        });
        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);
        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };
        var result = _service.CancelarWebinar(dto, "usuarioPrueba");
        Assert.IsTrue(result);
    }
    [TestMethod]
    public void CancelarWebinar_ErrorAlObtenerSesion_ThrowException()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
        .Setup(r => r.ObtenerPorId(It.IsAny<int>()))
        .Throws(new Exception("Error al obtener la sesión"));

        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);
        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };
        Assert.ThrowsException<Exception>(() =>
        _service.CancelarWebinar(dto, "usuarioPrueba"));
    }
    [TestMethod]
    public void CancelarWebinar_ErrorEnElCommit_ThrowException()
    {
        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock.Setup(r => r.ObtenerPorId(It.IsAny<int>())).Returns(new PEspecificoSesion { EsWebinarConfirmado = true });
        _unitOfWorkMock.Setup(u => u.PEspecificoSesionRepository)
                       .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
        .Setup(u => u.Commit())
        .Throws(new Exception("Error en Commit"));

        var dto = new CancelarWebinarDTO
        {
            IdPEspecificoSesion = 1,
            ComentarioCancelacion = "Motivo de prueba",
            Confirmo = false
        };

        Assert.ThrowsException<Exception>(() =>
        _service.CancelarWebinar(dto, "usuarioPrueba"));
    }
}
