using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PasarelaPagoPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_PasarelaPagoPw
    /// </summary>
    public class PasarelaPagoPwRepository : GenericRepository<TPasarelaPagoPw>, IPasarelaPagoPwRepository
    {
        private Mapper _mapper;

        public PasarelaPagoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPasarelaPagoPw, PasarelaPagoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPasarelaPagoPw MapeoEntidad(PasarelaPagoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPasarelaPagoPw modelo = _mapper.Map<TPasarelaPagoPw>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPasarelaPagoPw Add(PasarelaPagoPw entidad)
        {
            try
            {
                var PasarelaPagoPw = MapeoEntidad(entidad);
                base.Insert(PasarelaPagoPw);
                return PasarelaPagoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPasarelaPagoPw Update(PasarelaPagoPw entidad)
        {
            try
            {
                var PasarelaPagoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PasarelaPagoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PasarelaPagoPw);
                return PasarelaPagoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TPasarelaPagoPw> Add(IEnumerable<PasarelaPagoPw> listadoEntidad)
        {
            try
            {
                List<TPasarelaPagoPw> listado = new List<TPasarelaPagoPw>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPasarelaPagoPw> Update(IEnumerable<PasarelaPagoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPasarelaPagoPw> listado = new List<TPasarelaPagoPw>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// Autor Modificación: Griselberto Huaman.
        /// Fecha Modificación: 06/09/2022
        /// Descripción Modificación: Se implemento la obteneción de datos
        /// a traves de una vista.
        /// <summary>
        /// Obtiene todos los registros de T_PasarelaPagoPw.
        /// </summary>
        /// <returns> List<PasarelaPagoPwDTO> </returns>
        public IEnumerable<RegistroPasarelaPagoPWDTO> ObtenerPasarelaPagoPw()
        {
            try
            {
                List<RegistroPasarelaPagoPWDTO> rpta = new List<RegistroPasarelaPagoPWDTO>();
                var query = @"
                    SELECT [Id]
                          ,[IdProveedor]
                          ,[RazonSocial]
                          ,[Nombre]
                          ,[IdPais]
                          ,[NombrePais]
                          ,[Prioridad]
                          ,[Estado]
                      FROM [pla].[V_ListaRegistroPasarelaPagoPW]
                      WHERE [Estado]=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroPasarelaPagoPWDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PasarelaPagoPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<PasarelaPagoPwComboDTO> </returns>
        public IEnumerable<PasarelaPagoPwComboDTO> ObtenerCombo()
        {
            try
            {
                List<PasarelaPagoPwComboDTO> rpta = new List<PasarelaPagoPwComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_PasarelaPago_PW WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PasarelaPagoPwComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_PasarelaPagoPw relacionados a un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<PasarelaPagoPwAgendaDTO> </returns>
        public IEnumerable<PasarelaPagoPwAgendaDTO> ObtenerPasarelaPagoPorIdAlumno(int idAlumno)
        {
            try
            {
                List<PasarelaPagoPwAgendaDTO> rpta = new List<PasarelaPagoPwAgendaDTO>();
                var query = @"
                    SELECT Id,Nombre,IdProveedor,IdPais,Prioridad
                    FROM com.V_MetodoPagoAlumno
                    WHERE IdAlumno = @idAlumno
                    ORDER BY Prioridad ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PasarelaPagoPwAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Identificador de la Matricula basado en su Codigo
        /// </summary>
        /// <param name="codigoMatricula">Codigo Matricula</param>
        /// <returns> int </returns>
        public int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                RegistroMedioPagoMatriculaCronogramaDTO registro = new RegistroMedioPagoMatriculaCronogramaDTO();
                string _query = @"
                    SELECT TOP 1 Id AS IdMatriculaCabecera
                    FROM fin.T_MatriculaCabecera
                    WHERE Estado = 1 AND CodigoMatricula = @codigoMatricula";
                var registroQuery = _dapperRepository.FirstOrDefault(_query, new { codigoMatricula });

                if (!string.IsNullOrEmpty(registroQuery) && !registroQuery.Contains("[]") && !registroQuery.Contains("null"))
                {
                    registro = JsonConvert.DeserializeObject<RegistroMedioPagoMatriculaCronogramaDTO>(registroQuery);
                }

                return registro.IdMatriculaCabecera;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int BuscarIdMatriculaCabeceraPrueba(string codigoMatricula)
        {
            try
            {
                var registro = new ValorIntDTO();
                registro.Id = 0;
                string _query = @"SELECT TOP 1 PMA.Id FROM fin.T_MatriculaCabecera AS MC INNER JOIN ope.T_PEspecificoMatriculaAlumno AS PMA ON PMA.IdMatriculaCabecera = MC.Id WHERE MC.Estado = 1 AND PMA.Estado=1 AND MC.CodigoMatricula =@codigoMatricula";
                var registroQuery = _dapperRepository.FirstOrDefault(_query, new { codigoMatricula });
                if (!string.IsNullOrEmpty(registroQuery) && registroQuery != "null")
                {
                    registro = JsonConvert.DeserializeObject<ValorIntDTO>(registroQuery);
                }
                return registro.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el (PK)
        /// </summary>
        /// <param name="id"> (PK) Primary Key </param>
        /// <returns> PasarelaPagoPw </returns>
        public PasarelaPagoPw ObtenerPorId(int id)
        {
            try
            {
                string _query = @"SELECT Id,
                                       Nombre,
                                       IdProveedor,
                                       IdPais,
                                       Prioridad,
                                       Estado,
                                       FechaCreacion,
                                       FechaModificacion,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM pla.T_PasarelaPago_PW
                                WHERE Estado = 1
                                      AND Id = @Id;";
                var registroQuery = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(registroQuery) && registroQuery != "null")
                    return JsonConvert.DeserializeObject<PasarelaPagoPw>(registroQuery);
                return new PasarelaPagoPw();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
