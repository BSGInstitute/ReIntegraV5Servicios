using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class CampaniaRemarketingGeneralRepository : ICampaniaRemarketingGeneralRepository
    {
        public List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania()
        {
            try
            {
                var listadoCampania = new List<CampaniaRemarketingGeneralDTO>
                {
                    new CampaniaRemarketingGeneralDTO
                    {
                        Id = 1,
                        NombreCampania = "Campaña Test 1",
                        tipoCampania = "Email",
                        usuarioCreacion = "usuario1",
                        fechaEnvio = DateTime.Now.AddDays(-2),
                        cantidad = 100
                    },
                    new CampaniaRemarketingGeneralDTO
                    {
                        Id = 2,
                        NombreCampania = "Campaña Test 2",
                        tipoCampania = "WhatsApp",
                        usuarioCreacion = "usuario2",
                        fechaEnvio = DateTime.Now.AddDays(-1),
                        cantidad = 200
                    }
                };
                return listadoCampania;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<object> ObtenerRendimientoListadoCampanias(List<int> ids)
        {
            try
            {
                var mock = new List<object>
                {
                    new { CampaniaId = 1, Rendimiento = 0.85 }
                };
                return mock.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CombosConfiguracionCampaniaDTO ObtenerCombosConfiguracionCampania()
        {
            try
            {
                var mock = new CombosConfiguracionCampaniaDTO
                {
                    MedioEnvio = new List<ElementoConfiguracionCampania>
                    {
                        new ElementoConfiguracionCampania { Id = 1, Nombre = "WhatsApp" },
                        new ElementoConfiguracionCampania { Id = 2, Nombre = "Correo Electrónico" },
                        new ElementoConfiguracionCampania { Id = 3, Nombre = "SMS" }
                    },
                    TipoMensaje = new List<ElementoConfiguracionCampania>
                    {
                        new ElementoConfiguracionCampania { Id = 1, Nombre = "Promocional" },
                        new ElementoConfiguracionCampania { Id = 2, Nombre = "Informativo" },
                        new ElementoConfiguracionCampania { Id = 3, Nombre = "Recordatorio" }
                    },
                    LogicaEnvio = new List<ElementoConfiguracionCampania>
                    {
                        new ElementoConfiguracionCampania { Id = 1, Nombre = "Envío inmediato" },
                        new ElementoConfiguracionCampania { Id = 2, Nombre = "Programado" },
                        new ElementoConfiguracionCampania { Id = 3, Nombre = "Condicional" }
                    },
                    Argumento = new List<ElementoConfiguracionCampania>
                    {
                        new ElementoConfiguracionCampania { Id = 1, Nombre = "Interés del alumno" },
                        new ElementoConfiguracionCampania { Id = 2, Nombre = "Estado de oportunidad" },
                        new ElementoConfiguracionCampania { Id = 3, Nombre = "Fecha especial" }
                    }
                };

                return mock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DetallesCampaniaDTO VerDetallesCampania(int id)
        {
            try
            {
                var mock = new DetallesCampaniaDTO
                {
                    Programados = 15,
                    Aperturas = 10,
                    Clicks = 5,
                    Rebotados = 2,
                    AlumnosContactados = new List<AlumnoContactadoDTO>
                        {
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 1,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Juan Perez",
                                Apertura = true,
                                Click = false
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 2,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Maria Gomez",
                                Apertura = true,
                                Click = true
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 3,
                                EstadoEnvio = "En Proceso",
                                NombreAlumno = "Daniel Gomez",
                                Apertura = true,
                                Click = false
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 4,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Juan Gomez",
                                Apertura = false,
                                Click = true
                            }
                        }
                };
                return mock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditarCampania()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EliminarCampania(int id)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id)
        {
            try
            {
                var mensaje = new MensajeGeneradoDTO
                {
                    Id = 1,
                    Contenido = "Hola estimado alumno, te saluda una asesora de BSG Institute, queremos comentarte sobre el curso de BI",
                };

                return mensaje;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
