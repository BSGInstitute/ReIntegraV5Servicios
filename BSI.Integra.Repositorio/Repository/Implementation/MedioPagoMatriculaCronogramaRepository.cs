using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MedioPagoMatriculaCronogramaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 04/08/2022
    /// <summary>
    /// Gestión general de T_MedioPagoMatriculaCronograma
    /// </summary>
    public class MedioPagoMatriculaCronogramaRepository : GenericRepository<TMedioPagoMatriculaCronograma>, IMedioPagoMatriculaCronogramaRepository
    {
        private Mapper _mapper;

        public MedioPagoMatriculaCronogramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMedioPagoMatriculaCronograma, MedioPagoMatriculaCronograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TMedioPagoMatriculaCronograma MapeoEntidad(MedioPagoMatriculaCronograma entidad)
        {
            try
            {
                //crea la entidad padre
                TMedioPagoMatriculaCronograma modelo = _mapper.Map<TMedioPagoMatriculaCronograma>(entidad);

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

        public TMedioPagoMatriculaCronograma Add(MedioPagoMatriculaCronograma entidad)
        {
            try
            {
                var MedioPagoMatriculaCronograma = MapeoEntidad(entidad);
                base.Insert(MedioPagoMatriculaCronograma);
                return MedioPagoMatriculaCronograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMedioPagoMatriculaCronograma Update(MedioPagoMatriculaCronograma entidad)
        {
            try
            {
                var MedioPagoMatriculaCronograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MedioPagoMatriculaCronograma.RowVersion = entidadExistente.RowVersion;

                base.Update(MedioPagoMatriculaCronograma);
                return MedioPagoMatriculaCronograma;
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


        public IEnumerable<TMedioPagoMatriculaCronograma> Add(IEnumerable<MedioPagoMatriculaCronograma> listadoEntidad)
        {
            try
            {
                List<TMedioPagoMatriculaCronograma> listado = new List<TMedioPagoMatriculaCronograma>();
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

        public IEnumerable<TMedioPagoMatriculaCronograma> Update(IEnumerable<MedioPagoMatriculaCronograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMedioPagoMatriculaCronograma> listado = new List<TMedioPagoMatriculaCronograma>();
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
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MedioPagoMatriculaCronograma.
        /// </summary>
        /// <returns> List<MedioPagoMatriculaCronogramaDTO> </returns>
        public IEnumerable<MedioPagoMatriculaCronogramaDTO> ObtenerMedioPagoMatriculaCronograma()
        {
            try
            {
                List<MedioPagoMatriculaCronogramaDTO> rpta = new List<MedioPagoMatriculaCronogramaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdMatriculaCabecera,
	                    IdMedioPago,
	                    Activo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM fin.T_MedioPagoMatriculaCronograma
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<MedioPagoMatriculaCronogramaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 04/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene atributos principales de MedioPagoMatriculaCronograma relacionados a un IdMatriculaCabecera.
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List< MedioPagoMatriculaCronogramaDTO> </returns>
        public  MedioPagoMatriculaCronogramaDTO ObtenerMedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                 MedioPagoMatriculaCronogramaDTO rpta = new  MedioPagoMatriculaCronogramaDTO();
                var query = @"
                    SELECT TOP 1
	                    Id,
	                    IdMatriculaCabecera,
	                    IdMedioPago,
	                    Activo,
	                    UsuarioCreacion
                    FROM fin.T_MedioPagoMatriculaCronograma
                    WHERE Estado = 1
                        AND Activo = 1
                        AND IdMatriculaCabecera = @idMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject< MedioPagoMatriculaCronogramaDTO>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el método de pago registrado según el IdMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> List< MedioPagoMatriculaCronogramaDTO> </returns>
        public  MedioPagoMatriculaCronogramaDTO MedioPagoMatriculaCronogramaPorIdMatricula(int idMatriculaCabecera)
        {
            try
            {
                 MedioPagoMatriculaCronogramaDTO rpta = new  MedioPagoMatriculaCronogramaDTO();
                var query = @"
                    SELECT TOP 1
	                    Id,
	                    IdMatriculaCabecera,
	                    IdMedioPago,
	                    Activo,
	                    UsuarioCreacion
                    FROM fin.T_MedioPagoMatriculaCronograma
                    WHERE Estado=1 AND Activo = 1 AND IdMatriculaCabecera = @idMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject< MedioPagoMatriculaCronogramaDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Cambia el método de pago registrado para la matrícula
        /// </summary>
        /// <param name="idMatriculaCabecera">Id de la Matricula Cabecera</param>
        /// <returns> bool </returns>
        public bool DesactivarMedioPagoMatriculaCronogramaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var query = @"
                    UPDATE [fin].[T_MedioPagoMatriculaCronograma]
                    SET Activo = 0
                    WHERE Estado = 1 AND Activo = 1 AND IdMatriculaCabecera = @idMatriculaCabecera;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Registra el método de pago seleccionado para la matrícula
        /// </summary>
        /// <param name="medioPagoMatricula">Datos de MedioPago a Insertar</param>
        /// <returns> RegistroMedioPagoMatriculaCronogramaDTO </returns>
        public RegistroMedioPagoMatriculaCronogramaDTO RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO medioPagoMatricula)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("fin.SP_InsertarMedioPagoMatriculaCronograma",
                    new
                    {
                        medioPagoMatricula.IdMatriculaCabecera,
                        medioPagoMatricula.IdMedioPago,
                        Activo = true,
                        UsuarioCreacion = medioPagoMatricula.Usuario,
                        UsuarioModificacion = medioPagoMatricula.Usuario
                    });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return medioPagoMatricula;
                }
                return medioPagoMatricula;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
