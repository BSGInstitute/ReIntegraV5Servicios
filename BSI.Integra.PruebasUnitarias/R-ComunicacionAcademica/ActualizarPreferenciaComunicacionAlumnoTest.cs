using Moq;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System.Linq.Expressions;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.PruebasUnitarias
{
    [TestClass]
    public class ActualizarPreferenciaComunicacionAlumnoTest
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
        public void ActualizarPreferenciaComunicacionAlumno_ShouldAddUpdateDeleteMedios()
        {
            // Arrange
            var idAlumno = 1;
            var usuario = "TestUser";
            var existingMedios = new List<TPreferenciaComunicacionAcademica>
            {
                new TPreferenciaComunicacionAcademica { Id = 10, IdAlumno = idAlumno, IdMedioComunicacion = 1, UsuarioCreacion = "User1", FechaCreacion = DateTime.Now },
                new TPreferenciaComunicacionAcademica { Id = 20, IdAlumno = idAlumno, IdMedioComunicacion = 2, UsuarioCreacion = "User1", FechaCreacion = DateTime.Now }
            };

            _preferenciaRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<TPreferenciaComunicacionAcademica, bool>>>()))
                .Returns(existingMedios); // Assuming GetBy returns IEnumerable or List directly, usually implemented as such in mock if interface returns IEnumerable

            var dto = new PreferenciaConfiguracionDTO
            {
                IdAlumno = idAlumno,
                MediosComunicacion = new List<VPreferenciaComunicacionAcademicaMedioComunicacionDTO>
                {
                    new VPreferenciaComunicacionAcademicaMedioComunicacionDTO { IdPreferenciaComunicacionAcademica = 10, IdMedioComunicacion = 1, Nombre = "WhatsApp"}, // Update
                    new VPreferenciaComunicacionAcademicaMedioComunicacionDTO { IdPreferenciaComunicacionAcademica = 0, IdMedioComunicacion = 3, Nombre = "Correo Electronico"}   // Add
                },
                BloqueHorario = new List<PreferenciaComunicacionAcademicaHorarioDTO>() // Empty
            };

            _horarioRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<TPreferenciaComunicacionAcademicaHorario, bool>>>()))
                .Returns(new List<TPreferenciaComunicacionAcademicaHorario>());

            // Act
            var result = _service.ActualizarPreferenciaComunicacionAlumno(dto, usuario);

            // Assert
            _preferenciaRepoMock.Verify(r => r.Delete(20, usuario), Times.Once); // Delete Id 20
            _preferenciaRepoMock.Verify(r => r.Add(It.Is<PreferenciaComunicacionAcademica>(x => x.IdMedioComunicacion == 3)), Times.Once); // Add New
            _preferenciaRepoMock.Verify(r => r.Update(It.Is<PreferenciaComunicacionAcademica>(x => x.Id == 10)), Times.Once); // Update Id 10
            
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
        /*
        [TestMethod]
        public void ActualizarPreferenciaComunicacionAlumno_ShouldAddUpdateDeleteHorarios()
        {
            // Arrange
            var idAlumno = 1;
            var usuario = "TestUser";
            var existingHorarios = new List<PreferenciaComunicacionAcademicaHorario>
            {
                new PreferenciaComunicacionAcademicaHorario { Id = 100, IdAlumno = idAlumno, IdBloqueHorarioDetalle = 1, UsuarioCreacion = "User1", FechaCreacion = DateTime.Now },
                new PreferenciaComunicacionAcademicaHorario { Id = 200, IdAlumno = idAlumno, IdBloqueHorarioDetalle = 2, UsuarioCreacion = "User1", FechaCreacion = DateTime.Now }
            };

            _horarioRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademicaHorario, bool>>>()))
                .Returns(existingHorarios);
            
            _preferenciaRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademica, bool>>>()))
                .Returns(new List<PreferenciaComunicacionAcademica>());

            var dto = new PreferenciaConfiguracionDTO
            {
                IdAlumno = idAlumno,
                MediosComunicacion = new List<PreferenciaComunicacionAcademicaDTO>(),
                BloqueHorario = new List<PreferenciaComunicacionAcademicaHorarioDTO>
                {
                    new PreferenciaComunicacionAcademicaHorarioDTO { Id = 100, IdBloqueHorarioDetalle = 1, Estado = true }, // Update
                    new PreferenciaComunicacionAcademicaHorarioDTO { Id = 0, IdBloqueHorarioDetalle = 3, Estado = true }   // Add
                }
            };

            // Act
            var result = _service.ActualizarPreferenciaComunicacionAlumno(dto, usuario);

            // Assert
            _horarioRepoMock.Verify(r => r.Delete(200, usuario), Times.Once);
            _horarioRepoMock.Verify(r => r.Add(It.Is<PreferenciaComunicacionAcademicaHorario>(x => x.IdBloqueHorarioDetalle == 3)), Times.Once);
            _horarioRepoMock.Verify(r => r.Update(It.Is<PreferenciaComunicacionAcademicaHorario>(x => x.Id == 100)), Times.Once);
            
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
        
        [TestMethod]
        public void ActualizarPreferenciaComunicacionAlumno_NullLists_ShouldDeleteAllExisting()
        {
            // Arrange
            var idAlumno = 1;
            var usuario = "TestUser";
            var existingMedios = new List<PreferenciaComunicacionAcademica>
            {
                new PreferenciaComunicacionAcademica { Id = 10, IdAlumno = idAlumno }
            };
            var existingHorarios = new List<PreferenciaComunicacionAcademicaHorario>
            {
                new PreferenciaComunicacionAcademicaHorario { Id = 100, IdAlumno = idAlumno }
            };

            _preferenciaRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademica, bool>>>()))
                .Returns(existingMedios);
            _horarioRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademicaHorario, bool>>>()))
                .Returns(existingHorarios);

            var dto = new PreferenciaConfiguracionDTO
            {
                IdAlumno = idAlumno,
                MediosComunicacion = null, // Trigger delete all
                BloqueHorario = null       // Trigger delete all
            };

            // Act
            var result = _service.ActualizarPreferenciaComunicacionAlumno(dto, usuario);

            // Assert
            _preferenciaRepoMock.Verify(r => r.Delete(10, usuario), Times.Once);
            _horarioRepoMock.Verify(r => r.Delete(100, usuario), Times.Once);
            _unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ActualizarPreferenciaComunicacionAlumno_Exception_ShouldRollback()
        {
            // Arrange
            var dto = new PreferenciaConfiguracionDTO { IdAlumno = 1, MediosComunicacion = new List<PreferenciaComunicacionAcademicaDTO>() };
            
            _preferenciaRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademica, bool>>>()))
                .Throws(new Exception("DB Error"));

            // Act
            _service.ActualizarPreferenciaComunicacionAlumno(dto, "User");

            // Assert handled by ExpectedException
            // Verification of Rollback needs to be done manually if ExpectedException catches it?
            // Actually it's better to try-catch in test to verify rollback or use FluentAssertions if available (not here).
            // But Moq verification runs after.
            // Wait, if exception is thrown, we can't assert below the call.
        }

        [TestMethod]
        public void ActualizarPreferenciaComunicacionAlumno_Exception_ShouldCallRollback()
        {
             // Arrange
            var dto = new PreferenciaConfiguracionDTO { IdAlumno = 1, MediosComunicacion = new List<PreferenciaComunicacionAcademicaDTO>() };
             _preferenciaRepoMock.Setup(r => r.GetBy(It.IsAny<Expression<Func<PreferenciaComunicacionAcademica, bool>>>()))
                .Throws(new Exception("DB Error"));

            // Act
            try
            {
                _service.ActualizarPreferenciaComunicacionAlumno(dto, "User");
            }
            catch
            {
                // Ignore for verification
            }

            // Assert
            _unitOfWorkMock.Verify(u => u.Rollback(), Times.Once);
        }
        */
    }
}
