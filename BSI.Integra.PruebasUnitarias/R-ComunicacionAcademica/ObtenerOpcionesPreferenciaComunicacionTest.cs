using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using BSI.Integra.Repositorio.UnitOfWork;
using Moq;

namespace BSI.Integra.PruebasUnitarias
{
    [TestClass]
    public class ObtenerOpcionesPreferenciaComunicacionTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ComunicacionAcademicaService _service;
        private Mock<IMedioComunicacionRepository> _medioComunicacionRepoMock;
        private Mock<IBloqueHorarioRepository> _bloqueHorarioRepoMock;
        private Mock<IBloqueHorarioDetalleRepository> _bloqueHorarioDetalleRepoMock;

        [TestInitialize]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _medioComunicacionRepoMock = new Mock<IMedioComunicacionRepository>();
            _bloqueHorarioRepoMock = new Mock<IBloqueHorarioRepository>();
            _bloqueHorarioDetalleRepoMock = new Mock<IBloqueHorarioDetalleRepository>();

            _unitOfWorkMock.Setup(u => u.MedioComunicacionRepository).Returns(_medioComunicacionRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.BloqueHorarioRepository).Returns(_bloqueHorarioRepoMock.Object);
            _unitOfWorkMock.Setup(u => u.BloqueHorarioDetalleRepository).Returns(_bloqueHorarioDetalleRepoMock.Object);

            _service = new ComunicacionAcademicaService(_unitOfWorkMock.Object);
        }

        [TestMethod]
        public void ObtenerOpcionesPreferenciaComunicacion_ShouldReturnActiveItems()
        {
            // Arrange
            var medios = new List<TMedioComunicacion>
            {
                new TMedioComunicacion { Id = 1, Nombre = "WhatsApp", Estado = true },
                new TMedioComunicacion { Id = 2, Nombre = "Llamada Telefónica", Estado = true },
                new TMedioComunicacion { Id = 3, Nombre = "Correo Electrónico", Estado = true },
            };
            var bloques = new List<TBloqueHorario>
            {
                new TBloqueHorario { Id = 2, Nombre = "Tarde", Descripcion = null, Estado = true},
                new TBloqueHorario { Id = 3, Nombre = "Mañana", Descripcion = null, Estado = true},
            };
            var detalles = new List<TBloqueHorarioDetalle>
            {
                new TBloqueHorarioDetalle { Id = 1, IdBloqueHorario = 3, HoraInicio = TimeSpan.Parse("09:00"), HoraFin = TimeSpan.Parse("10:00"), Estado = true },
                new TBloqueHorarioDetalle { Id = 2, IdBloqueHorario = 3, HoraInicio = TimeSpan.Parse("11:00"), HoraFin = TimeSpan.Parse("12:00"), Estado = true }
            };
            _medioComunicacionRepoMock.Setup(r => r.GetAll()).Returns(medios.AsQueryable());
            _bloqueHorarioRepoMock.Setup(r => r.GetAll()).Returns(bloques.AsQueryable());
            _bloqueHorarioDetalleRepoMock.Setup(r => r.GetAll()).Returns(detalles.AsQueryable());

            // Act
            var result = _service.ObtenerOpcionesPreferenciaComunicacion();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.MediosComunicacion.Count);
            Assert.AreEqual("WhatsApp", result.MediosComunicacion[0].Nombre);

            Assert.AreEqual(2, result.BloqueHorario.Count);
            Assert.AreEqual("Tarde", result.BloqueHorario[1].Nombre);

            Assert.AreEqual(2, result.BloqueHorarioDetalle.Count);
            Assert.AreEqual(1, result.BloqueHorarioDetalle[0].Id);
        }

        [TestMethod]
        public void ObtenerOpcionesPreferenciaComunicacion_EmptyRepos_ShouldReturnEmptyLists()
        {
            // Arrange
            _medioComunicacionRepoMock.Setup(r => r.GetAll()).Returns(new List<TMedioComunicacion>().AsQueryable());
            _bloqueHorarioRepoMock.Setup(r => r.GetAll()).Returns(new List<TBloqueHorario>().AsQueryable());
            _bloqueHorarioDetalleRepoMock.Setup(r => r.GetAll()).Returns(new List<TBloqueHorarioDetalle>().AsQueryable());

            // Act
            var result = _service.ObtenerOpcionesPreferenciaComunicacion();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.MediosComunicacion.Count);
            Assert.AreEqual(0, result.BloqueHorario.Count);
            Assert.AreEqual(0, result.BloqueHorarioDetalle.Count);
        }
    }
}
