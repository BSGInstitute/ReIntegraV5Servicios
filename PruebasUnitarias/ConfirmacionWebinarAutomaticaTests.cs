using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Moq;

namespace PruebasUnitarias;

[TestClass]
public class ConfirmacionWebinarAutomaticaTests
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
    public void ConfirmacionWebinarAutomatica_WebinarYaFinalizado_RetornaEstadoFalse()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(true);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Estado);
        Assert.AreEqual("El webinar ya finalizó", result.Mensaje);

        sesionRepoMock.Verify(
            r => r.EsWebinarPasado(idSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Never);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_WebinarNoEncontrado_RetornaEstadoFalse()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns((WebinarDetalleSesionDTO)null);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Estado);
        Assert.AreEqual("Webinar no encontrado", result.Mensaje);

        generalRepoMock.Verify(
            r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Never);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_WebinarCancelado_RetornaEstadoFalse()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(idSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = false
            });

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns(new WebinarDetalleSesionDTO
            {
                TotalParticipantesConfirmados = 5
            });

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Estado);
        Assert.AreEqual("Este webinar ya fue cancelado", result.Mensaje);

        sesionRepoMock.Verify(
            r => r.ObtenerPorId(idSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Never);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_WebinarYaConfirmado_RetornaEstadoFalse()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(idSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = true
            });

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns(new WebinarDetalleSesionDTO
            {
                TotalParticipantesConfirmados = 10
            });

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Estado);
        Assert.AreEqual("Este webinar ya fue confirmado", result.Mensaje);

        sesionRepoMock.Verify(
            r => r.ObtenerPorId(idSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Never);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_ConParticipantesConfirmaWebinar_RetornaEstadoTrue()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        var sesionEntity = new PEspecificoSesion
        {
            EsWebinarConfirmado = null
        };

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(idSesion))
            .Returns(sesionEntity);

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns(new WebinarDetalleSesionDTO
            {
                TotalParticipantesConfirmados = 3
            });

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.Commit());

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Estado);
        Assert.AreEqual("Webinar confirmado", result.Mensaje);

        Assert.IsTrue(sesionEntity.EsWebinarConfirmado);

        sesionRepoMock.Verify(
            r => r.Update(It.IsAny<PEspecificoSesion>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Once);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_SinParticipantes_DejaWebinarPorConfirmar_RetornaEstadoTrue()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        var sesionEntity = new PEspecificoSesion
        {
            EsWebinarConfirmado = true
        };

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(idSesion))
            .Returns(sesionEntity);

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns(new WebinarDetalleSesionDTO
            {
                TotalParticipantesConfirmados = 0
            });

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.Commit());

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.ConfirmacionWebinarAutomatica(idSesion);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Estado);
        Assert.AreEqual("Webinar por confirmar, sin participantes", result.Mensaje);

        Assert.IsNull(sesionEntity.EsWebinarConfirmado);

        sesionRepoMock.Verify(
            r => r.Update(It.IsAny<PEspecificoSesion>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Once);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_ErrorAlValidarWebinarPasado_LanzaExcepcion()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Throws(new Exception("Error al validar estado del webinar"));

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        Assert.ThrowsException<Exception>(() =>
            service.ConfirmacionWebinarAutomatica(idSesion));

        sesionRepoMock.Verify(
            r => r.EsWebinarPasado(idSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Never);
    }
    [TestMethod]
    public void ConfirmacionWebinarAutomatica_ErrorEnCommit_LanzaExcepcion()
    {
        int idSesion = 1;

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(idSesion))
            .Returns(false);

        var sesionEntity = new PEspecificoSesion
        {
            EsWebinarConfirmado = null
        };

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(idSesion))
            .Returns(sesionEntity);

        var generalRepoMock = new Mock<IPGeneralRepository>();
        generalRepoMock
            .Setup(r => r.ObtenerWebinarPorIdPEspecificoSesion(idSesion))
            .Returns(new WebinarDetalleSesionDTO
            {
                TotalParticipantesConfirmados = 2
            });

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PGeneralRepository)
            .Returns(generalRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.Commit())
            .Throws(new Exception("Error en Commit"));

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        Assert.ThrowsException<Exception>(() =>
            service.ConfirmacionWebinarAutomatica(idSesion));

        sesionRepoMock.Verify(
            r => r.Update(It.IsAny<PEspecificoSesion>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.Commit(),
            Times.Once);
    }
}
