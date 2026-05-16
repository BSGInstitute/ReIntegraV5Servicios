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
    /// Repositorio: MatriculaCabeceraBeneficiosRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabeceraBeneficios
    /// </summary>
    public class MatriculaCabeceraBeneficiosRepository : GenericRepository<TMatriculaCabeceraBeneficio>, IMatriculaCabeceraBeneficiosRepository
    {
        private Mapper _mapper;

        public MatriculaCabeceraBeneficiosRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabeceraBeneficio, MatriculaCabeceraBeneficios>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMatriculaCabeceraBeneficio MapeoEntidad(MatriculaCabeceraBeneficios entidad)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraBeneficio modelo = _mapper.Map<TMatriculaCabeceraBeneficio>(entidad);

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

        public TMatriculaCabeceraBeneficio Add(MatriculaCabeceraBeneficios entidad)
        {
            try
            {
                var MatriculaCabeceraBeneficios = MapeoEntidad(entidad);
                base.Insert(MatriculaCabeceraBeneficios);
                return MatriculaCabeceraBeneficios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaCabeceraBeneficio Update(MatriculaCabeceraBeneficios entidad)
        {
            try
            {
                var MatriculaCabeceraBeneficios = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MatriculaCabeceraBeneficios.RowVersion = entidadExistente.RowVersion;

                base.Update(MatriculaCabeceraBeneficios);
                return MatriculaCabeceraBeneficios;
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


        public IEnumerable<TMatriculaCabeceraBeneficio> Add(IEnumerable<MatriculaCabeceraBeneficios> listadoEntidad)
        {
            try
            {
                List<TMatriculaCabeceraBeneficio> listado = new List<TMatriculaCabeceraBeneficio>();
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

        public IEnumerable<TMatriculaCabeceraBeneficio> Update(IEnumerable<MatriculaCabeceraBeneficios> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMatriculaCabeceraBeneficio> listado = new List<TMatriculaCabeceraBeneficio>();
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MatriculaCabeceraBeneficios.
        /// </summary>
        /// <returns> List<MatriculaCabeceraBeneficiosDTO> </returns>
        public IEnumerable<MatriculaCabeceraBeneficiosDTO> ObtenerMatriculaCabeceraBeneficios()
        {
            try
            {
                List<MatriculaCabeceraBeneficiosDTO> rpta = new List<MatriculaCabeceraBeneficiosDTO>();
                var query = @"
                    SELECT
	                    Id,IdMatriculaCabecera,Nombre,IdSuscripcionProgramaGeneral,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,
	                    IdConfiguracionBeneficioProgramaGeneral,IdEstadoMatriculaCabeceraBeneficio,FechaSolicitud,IdEstadoSolicitudBeneficio,Duracion,
	                    FechaEntrega,FechaProgramada,FechaAprobacion,UsuarioAprobacion,UsuarioEntregoBeneficio
                    FROM com.T_MatriculaCabeceraBeneficios
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MatriculaCabeceraBeneficiosDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MatriculaCabeceraBeneficios para mostrarse en combo.
        /// </summary>
        /// <returns> List<MatriculaCabeceraBeneficiosComboDTO> </returns>
        public IEnumerable<MatriculaCabeceraBeneficiosComboDTO> ObtenerCombo()
        {
            try
            {
                List<MatriculaCabeceraBeneficiosComboDTO> rpta = new List<MatriculaCabeceraBeneficiosComboDTO>();
                var query = @"SELECT Id,IdMatriculaCabecera,Nombre FROM com.T_MatriculaCabeceraBeneficios WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MatriculaCabeceraBeneficiosComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Nombre de los Beneficios asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="version">Version de matricula alumno</param>
        /// <returns> List<string> </returns>
        public IEnumerable<StringDTO> ObtenerBeneficiosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<StringDTO> rpta = new List<StringDTO>();
                var query = @"
                    SELECT Nombre AS Valor
                    FROM com.T_MatriculaCabeceraBeneficios
                    WHERE Estado = 1 AND IdMatriculaCabecera = @idMatriculaCabecera
                    ORDER BY IdSuscripcionProgramaGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<StringDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MatriculaCabeceraBeneficios ObtenerPorId(int Id)
        {
            try
            {
                MatriculaCabeceraBeneficios IdMat = new MatriculaCabeceraBeneficios();
                var _query = @"SELECT Id,
                                IdMatriculaCabecera,
                                Nombre,
                                IdSuscripcionProgramaGeneral,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdConfiguracionBeneficioProgramaGeneral,
                                IdEstadoMatriculaCabeceraBeneficio,
                                FechaSolicitud,
                                IdEstadoSolicitudBeneficio,
                                Duracion,
                                FechaEntrega,
                                FechaProgramada,
                                FechaAprobacion,
                                UsuarioAprobacion,
                                UsuarioEntregoBeneficio FROM com.T_MatriculaCabeceraBeneficios WHERE Estado = 1 AND Id =@Id ";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { Id=Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    IdMat = JsonConvert.DeserializeObject<MatriculaCabeceraBeneficios>(resultado);
                }
                return IdMat;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: --
        /// Fecha: --
        /// Version: 2.0
        /// Autor Modificacion: Gilmer Qm
        /// Fecha Modifcacion: 2026-05-12
        /// Descripcion Modificacion: Se sustituye la vista por SP para mejorar el rendimiento, se agrega ordenamiento por beneficio y se cambia el nombre del metodo para reflejar mejor su funcionalidad.
        /// Version: 2.0
        /// <summary>
        /// Obtiene el Nombre de los Beneficios asociados a una Matricula Cabecera.
        /// </summary>
        /// <param name="version">Version de matricula alumno</param>
        /// <returns> List<string> </returns>
        public IEnumerable<BeneficiosSolicitadosDTO> ObtenerBeneficiosSolicitadosSinRepetir()
        {
            try
            {
                List<BeneficiosSolicitadosDTO> BeneficioSolicitado = new List<BeneficiosSolicitadosDTO>();
                var _query = "ope.SP_MatriculaBeneficiosAgrupado";
                var resultado = _dapperRepository.QuerySPDapper(_query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    BeneficioSolicitado = JsonConvert.DeserializeObject<List<BeneficiosSolicitadosDTO>>(resultado);
                }
                return BeneficioSolicitado.OrderBy(x=> x.Beneficio);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}