using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System.Collections.Generic;

namespace BSI.Integra.PruebasUnitarias
{
    [TestClass]
    public class ObtenerPreferenciaComunicacionAlumnoTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ComunicacionAcademicaService _service;
        private Mock<IPreferenciaComunicacionAcademicaRepository> _preferenciaRepoMock;
        private Mock<IPreferenciaComunicacionAcademicaHorarioRepository> _horarioRepoMock;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _preferenciaRepoMock = new Mock<IPreferenciaComunicacionAcademicaRepository>();
            _horarioRepoMock = new Mock<IPreferenciaComunicacionAcademicaHorarioRepository>();

            _unitOfWorkMock.Setup(u => u.PreferenciaComunicacionAcademicaRepository).Returns(_preferenciaRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.PreferenciaComunicacionAcademicaHorarioRepository).Returns(_horarioRepoMock.Object);

            _service = new ComunicacionAcademicaService(_unitOfWorkMock.Object);
        }

        [TestMethod]
        public void ObtenerPreferenciaComunicacionAlumno_ShouldReturnDataFromRepos()
        {
            // Arrange
            var idAlumno = 123;
            var expectedMedios = new List<VPreferenciaComunicacionAcademicaMedioComunicacionDTO>
            {
                new VPreferenciaComunicacionAcademicaMedioComunicacionDTO { IdPreferenciaComunicacionAcademica = 8, IdAlumno = idAlumno, IdMedioComunicacion = 2, Nombre = "Llamada Telefónica"}
            };
            var expectedHorarios = new List<PreferenciaComunicacionAcademicaHorarioDTO>
            {
                new PreferenciaComunicacionAcademicaHorarioDTO { Id = 1, IdAlumno = idAlumno, IdBloqueHorarioDetalle = 5 } 
            };

            _preferenciaRepoMock.Setup(r => r.ObtenerPreferenciaMedioComunicacionByIdAlumno(idAlumno))
                .Returns(expectedMedios);

            _horarioRepoMock.Setup(r => r.ObtenerPreferenciaHorarioComunicacionByIdAlumno(idAlumno))
                .Returns(expectedHorarios);

            // Act
            var result = _service.ObtenerPreferenciaComunicacionAlumno(idAlumno);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MediosComunicacion);
            Assert.AreEqual(1, result.MediosComunicacion.Count);
            Assert.AreEqual(2, result.MediosComunicacion[0].IdMedioComunicacion);

            Assert.IsNotNull(result.BloqueHorario);
            Assert.AreEqual(1, result.BloqueHorario.Count);
            Assert.AreEqual(5, result.BloqueHorario[0].IdBloqueHorarioDetalle);
        }

        [TestMethod]
        public void ObtenerPreferenciaComunicacionAlumno_EmptyRepos_ShouldReturnEmptyDTOs()
        {
             // Arrange
            var idAlumno = 456;
            _preferenciaRepoMock.Setup(r => r.ObtenerPreferenciaMedioComunicacionByIdAlumno(idAlumno))
                .Returns(new List<VPreferenciaComunicacionAcademicaMedioComunicacionDTO>());

            _horarioRepoMock.Setup(r => r.ObtenerPreferenciaHorarioComunicacionByIdAlumno(idAlumno))
                .Returns(new List<PreferenciaComunicacionAcademicaHorarioDTO>());

            // Act
            var result = _service.ObtenerPreferenciaComunicacionAlumno(idAlumno);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.MediosComunicacion.Count);
            Assert.AreEqual(0, result.BloqueHorario.Count);
        }
    }
}
