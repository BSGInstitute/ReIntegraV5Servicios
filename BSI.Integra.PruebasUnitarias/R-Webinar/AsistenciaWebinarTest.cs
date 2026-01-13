using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Moq;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

namespace BSI.Integra.PruebasUnitarias;

[TestClass]
public class AsistenciaWebinarTest
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
    public void AsistenciaWebinar_MatriculaNoExiste_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(false);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual("La matrícula no es válida.", ex.Message);

        _unitOfWorkMock.Verify(
            u => u.PEspecificoSesionRepository,
            Times.Never);
    }

    [TestMethod]
    public void AsistenciaWebinar_WebinarNoExiste_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(false);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual("El webinar no es válido.", ex.Message);

        sesionRepoMock.Verify(
            r => r.Exist(asistenciaDto.IdPEspecificoSesion),
            Times.Once);

        sesionRepoMock.Verify(
            r => r.EsWebinarPasado(It.IsAny<int>()),
            Times.Never);
    }

    [TestMethod]
    public void AsistenciaWebinar_WebinarFinalizado_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual("El webinar ya finalizó", ex.Message);

        sesionRepoMock.Verify(
            r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion),
            Times.Once);

        _unitOfWorkMock.Verify(
            u => u.ConfirmacionWebinarRepository,
            Times.Never);
    }

    [TestMethod]
    public void AsistenciaWebinar_WebinarCancelado_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };
        var confirmacionRepoMock = new Mock<IConfirmacionWebinarRepository>();
        confirmacionRepoMock
            .Setup(r => r.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(
                asistenciaDto.IdMatriculaCabecera,
                asistenciaDto.IdPEspecificoSesion))
            .Returns((AsistenciaConfirmacionWebinarDTO)null);

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(asistenciaDto.IdPEspecificoSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = false
            });
        _unitOfWorkMock
        .Setup(u => u.ConfirmacionWebinarRepository)
        .Returns(confirmacionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual("Este webinar ya fue cancelado", ex.Message);

        confirmacionRepoMock.Verify(
            r => r.Insertar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
         Times.Never);

        confirmacionRepoMock.Verify(
            r => r.Actualizar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);

    }

    [TestMethod]
    public void AsistenciaWebinar_ConfirmacionExiste_EstadoDiferente_ActualizaConfirmacion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(asistenciaDto.IdPEspecificoSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = true
            });

        var confirmacionExistente = new AsistenciaConfirmacionWebinarDTO
        {
            Id = 5,
            IdMatriculaCabecera = asistenciaDto.IdMatriculaCabecera,
            IdPEspecificoSesion = asistenciaDto.IdPEspecificoSesion,
            Confirmo = false,
            Asistio = false,
            Estado = true,
            UsuarioCreacion = "system",
            UsuarioModificacion = "system",
            FechaCreacion = DateTime.Now,
            FechaModificacion = DateTime.Now
        };

        var confirmacionRepoMock = new Mock<IConfirmacionWebinarRepository>();
        confirmacionRepoMock
            .Setup(r => r.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(
                asistenciaDto.IdMatriculaCabecera,
                asistenciaDto.IdPEspecificoSesion))
            .Returns(confirmacionExistente);

        confirmacionRepoMock
            .Setup(r => r.Actualizar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()));

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.ConfirmacionWebinarRepository)
            .Returns(confirmacionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var result = service.AsistenciaWebinar(asistenciaDto);

        Assert.AreEqual(
            "Su participación en este webinar se confirmo. Agradecemos su participación.",
            result.Mensaje);

        Assert.IsTrue(confirmacionExistente.Confirmo);
        Assert.IsFalse(confirmacionExistente.Asistio);
        Assert.AreEqual("system", confirmacionExistente.UsuarioModificacion);

        confirmacionRepoMock.Verify(
            r => r.Actualizar(confirmacionExistente),
            Times.Once);

        confirmacionRepoMock.Verify(
            r => r.Insertar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);
    }


    [TestMethod]
    public void AsistenciaWebinar_ConfirmacionExiste_YaConfirmado_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = true
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(asistenciaDto.IdPEspecificoSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = true
            });

        var confirmacionExistente = new AsistenciaConfirmacionWebinarDTO
        {
            Id = 7,
            IdMatriculaCabecera = asistenciaDto.IdMatriculaCabecera,
            IdPEspecificoSesion = asistenciaDto.IdPEspecificoSesion,
            Confirmo = true
        };

        var confirmacionRepoMock = new Mock<IConfirmacionWebinarRepository>();
        confirmacionRepoMock
            .Setup(r => r.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(
                asistenciaDto.IdMatriculaCabecera,
                asistenciaDto.IdPEspecificoSesion))
            .Returns(confirmacionExistente);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.ConfirmacionWebinarRepository)
            .Returns(confirmacionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual(
            "Su participación en este webinar ya fue confirmada. Solo puede realizar esta acción una vez por webinar.",
            ex.Message);

        confirmacionRepoMock.Verify(
            r => r.Actualizar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);

        confirmacionRepoMock.Verify(
            r => r.Insertar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);
    }

    [TestMethod]
    public void AsistenciaWebinar_ConfirmacionExiste_YaCancelado_LanzaExcepcion()
    {
        var asistenciaDto = new WebinarAlumnoAsistenciaDTO
        {
            IdMatriculaCabecera = 1,
            IdPEspecificoSesion = 10,
            EstadoAsistencia = false
        };

        var matriculaRepoMock = new Mock<IMatriculaCabeceraRepository>();
        matriculaRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdMatriculaCabecera))
            .Returns(true);

        var sesionRepoMock = new Mock<IPEspecificoSesionRepository>();
        sesionRepoMock
            .Setup(r => r.Exist(asistenciaDto.IdPEspecificoSesion))
            .Returns(true);

        sesionRepoMock
            .Setup(r => r.EsWebinarPasado(asistenciaDto.IdPEspecificoSesion))
            .Returns(false);

        sesionRepoMock
            .Setup(r => r.ObtenerPorId(asistenciaDto.IdPEspecificoSesion))
            .Returns(new PEspecificoSesion
            {
                EsWebinarConfirmado = true
            });

        var confirmacionExistente = new AsistenciaConfirmacionWebinarDTO
        {
            Id = 8,
            IdMatriculaCabecera = asistenciaDto.IdMatriculaCabecera,
            IdPEspecificoSesion = asistenciaDto.IdPEspecificoSesion,
            Confirmo = false
        };

        var confirmacionRepoMock = new Mock<IConfirmacionWebinarRepository>();
        confirmacionRepoMock
            .Setup(r => r.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(
                asistenciaDto.IdMatriculaCabecera,
                asistenciaDto.IdPEspecificoSesion))
            .Returns(confirmacionExistente);

        _unitOfWorkMock
            .Setup(u => u.MatriculaCabeceraRepository)
            .Returns(matriculaRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.PEspecificoSesionRepository)
            .Returns(sesionRepoMock.Object);

        _unitOfWorkMock
            .Setup(u => u.ConfirmacionWebinarRepository)
            .Returns(confirmacionRepoMock.Object);

        var service = new AsistenciaWebinarService(_unitOfWorkMock.Object);

        var ex = Assert.ThrowsException<Exception>(() =>
            service.AsistenciaWebinar(asistenciaDto));

        Assert.AreEqual(
            "Su participación en este webinar ya fue cancelada. No puede realizar esta acción nuevamente.",
            ex.Message);

        confirmacionRepoMock.Verify(
            r => r.Actualizar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);

        confirmacionRepoMock.Verify(
            r => r.Insertar(It.IsAny<AsistenciaConfirmacionWebinarDTO>()),
            Times.Never);
    }

}
