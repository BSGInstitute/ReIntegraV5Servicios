using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PespecificoPadrePespecificoHijo
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PespecificoPadrePespecificoHijo
    /// </summary>
    public class PespecificoPadrePespecificoHijoRepository : GenericRepository<TPespecificoPadrePespecificoHijo>, IPespecificoPadrePespecificoHijoRepository
    {
        private Mapper _mapper;

        public PespecificoPadrePespecificoHijoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoPadrePespecificoHijo, PespecificoPadrePespecificoHijo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoPadrePespecificoHijo MapeoEntidad(PespecificoPadrePespecificoHijo entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoPadrePespecificoHijo modelo = _mapper.Map<TPespecificoPadrePespecificoHijo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoPadrePespecificoHijo Add(PespecificoPadrePespecificoHijo entidad)
        {
            try
            {
                var PespecificoPadrePespecificoHijo = MapeoEntidad(entidad);
                base.Insert(PespecificoPadrePespecificoHijo);
                return PespecificoPadrePespecificoHijo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoPadrePespecificoHijo Update(PespecificoPadrePespecificoHijo entidad)
        {
            try
            {
                var PespecificoPadrePespecificoHijo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoPadrePespecificoHijo.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoPadrePespecificoHijo);
                return PespecificoPadrePespecificoHijo;
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
        public IEnumerable<TPespecificoPadrePespecificoHijo> Add(IEnumerable<PespecificoPadrePespecificoHijo> listadoEntidad)
        {
            try
            {
                List<TPespecificoPadrePespecificoHijo> listado = new List<TPespecificoPadrePespecificoHijo>();
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
        public IEnumerable<TPespecificoPadrePespecificoHijo> Update(IEnumerable<PespecificoPadrePespecificoHijo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoPadrePespecificoHijo> listado = new List<TPespecificoPadrePespecificoHijo>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de pespecificos hijos
        /// </summary>
        /// <returns> Datos Pespecifico Hijo </returns>
        public PespecificoPadrePespecificoHijo? ObtenerPorIdPadreIdHijo(int pespecificoPadreId, int pespecificoHijoId)
        {
            try
            {
                string query = @"
                    SELECT Id,
	                    PEspecificoPadreId AS PespecificoPadreId,
	                    PEspecificoHijoId AS PespecificoHijoId,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PEspecificoPadrePEspecificoHijo
                    WHERE Estado=1 AND PespecificoPadreId=@pespecificoPadreId AND PespecificoHijoId=@pespecificoHijoId";
                string resultado = _dapperRepository.FirstOrDefault(query, new { pespecificoPadreId, pespecificoHijoId });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadrePespecificoHijo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-PDPEH-001@Error en ObtenerPorIdPadreIdHijo: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de pespecificos hijos
        /// </summary>
        /// <returns> Datos Pespecifico Hijo </returns>
        public PespecificoPadrePespecificoHijo? ObtenerPespecificoPadrePorId(int idPespecificoPadre)
        {
            try
            {
                string query = @"
                    SELECT Id,
	                    PEspecificoPadreId AS PespecificoPadreId,
	                    PEspecificoHijoId AS PespecificoHijoId,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PEspecificoPadrePEspecificoHijo
                    WHERE Estado=1 AND PespecificoHijoId=@idPespecificoPadre AND PespecificoHijoId!=PespecificoPadreId";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPespecificoPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadrePespecificoHijo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-PDPEH-001@Error en ObtenerPorIdPadreIdHijo: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de pespecificos hijos
        /// </summary>
        /// <returns> Datos Pespecifico Hijo </returns>
        public IEnumerable<DatosPEspecificoHijoDTO> ObtenerDetallePEspecificoHijosPorIdPespecificoPadre(int idPespecifico)
        {
            try
            {
                string query = @"
                    SELECT
	                    PEspecificoHijoId,
                        PEspecificoPadreId,
	                    IdProgramaGeneral,
	                    Nombre,
	                    Duracion,
	                    IdCiudad,
	                    TipoAmbiente
                    FROM pla.V_DetallePespecificoFrecuancia
                    WHERE
	                    Estado = 1
	                    AND PEspecificoPadreId = @idPespecifico;";
                string resultado = _dapperRepository.QueryDapper(query, new { idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<DatosPEspecificoHijoDTO>>(resultado)!;
                }
                return new List<DatosPEspecificoHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-PDPEH-001@Error en ObtenerDetallePEspecificoHijosPorIdPespecificoPadre: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
        /// Obtiene Informacion  de los Programas Relacionados
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public IEnumerable<InformacionPespecificoHijoDTO> ObtenerPespecificosRelacionados(int idPespecifico)
        {
            try
            {
                string query = "pla.SP_ProgramasEspecificosHijosPorPEspecifico";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<InformacionPespecificoHijoDTO>>(resultado)!;
                return new List<InformacionPespecificoHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPespecificosRelacionados(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de pespecificos hijos
        /// </summary>
        /// <returns> Datos Pespecifico Hijo </returns>
        public PespecificoPadrePespecificoHijo? ObtenerPorPEspecificoHijoId(int idPespecificoHijoId)
        {
            try
            {
                string query = @"
                    SELECT Id,
	                    PEspecificoPadreId AS PespecificoPadreId,
	                    PEspecificoHijoId AS PespecificoHijoId,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PEspecificoPadrePEspecificoHijo
                    WHERE Estado=1 AND PEspecificoHijoId=@idPespecificoHijoId";
                string resultado = _dapperRepository.FirstOrDefault(query, new { idPespecificoHijoId });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoPadrePespecificoHijo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-PDPEH-001@Error en ObtenerPorPEspecificoHijoId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el detalle de pespecificos hijos
        /// </summary>
        /// <returns> Datos Pespecifico Hijo </returns>
        public IEnumerable<PespecificoPadrePespecificoHijo> ObtenerPorPEspecificoPadreId(int idPespecificoPadre)
        {
            try
            {
                string query = @"
                    SELECT Id,
	                    PEspecificoPadreId AS PespecificoPadreId,
	                    PEspecificoHijoId AS PespecificoHijoId,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_PEspecificoPadrePEspecificoHijo
                    WHERE Estado=1 AND PEspecificoPadreId=@idPespecificoPadre";
                string resultado = _dapperRepository.QueryDapper(query, new { idPespecificoPadre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoPadrePespecificoHijo>>(resultado)!;
                }
                return new List<PespecificoPadrePespecificoHijo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-OPEPI-001@Error en ObtenerPorPEspecificoPadreId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
        /// Obtiene toda la informacion de los programas hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public IEnumerable<InformacionPespecificoHijoDTO> ObtenerInformacionPespecificoSesion(int idPespecifico)
        {
            try
            {
                string query = "SELECT Id, Nombre, IdProgramaGeneral, Duracion, IdCiudad, TipoAmbiente, IdAmbiente FROM pla.V_ListaProgramaEspecificoHijo WHERE Estado=1 AND Id=@IdPespecifico;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<InformacionPespecificoHijoDTO>>(resultado)!;
                }
                return new List<InformacionPespecificoHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-OIPS-001@Error en ObtenerInformacionPespecificoSesion: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
        /// Obtiene toda la informacion de los programas hijos
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public IEnumerable<InformacionPespecificoHijoDTO> ObtenerInformacionPespecificosHijos(int idPespecifico)
        {
            try
            {
                string query = "Select Id,Nombre,IdProgramaGeneral,Duracion,IdCiudad,TipoAmbiente,IdAmbiente, IdModalidadCurso, IdExpositor_Referencia From pla.V_ListaProgramaEspecificoHijo Where Estado=1 and PEspecificoPadreId=@IdPespecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<InformacionPespecificoHijoDTO>>(resultado)!;
                }
                return new List<InformacionPespecificoHijoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PEPHR-OIPH-001@Error en ObtenerInformacionPespecificosHijos: {ex.Message}", ex);
            }
        }
    }
}
