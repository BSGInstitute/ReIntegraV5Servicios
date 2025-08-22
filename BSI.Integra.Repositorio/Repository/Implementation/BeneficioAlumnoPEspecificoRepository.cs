using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: BeneficioAlumnoPEspecificoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_BeneficioAlumnoPEspecifico
    /// </summary>
    public class BeneficioAlumnoPEspecificoRepository : GenericRepository<TBeneficiosAlumnoPespecifico>, IBeneficioAlumnoPEspecificoRepository
    {
        private Mapper _mapper;

        public BeneficioAlumnoPEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBeneficiosAlumnoPespecifico, BeneficioAlumnoPEspecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TBeneficiosAlumnoPespecifico MapeoEntidad(BeneficioAlumnoPEspecifico entidad)
        {
            try
            {
                //crea la entidad padre
                TBeneficiosAlumnoPespecifico modelo = _mapper.Map<TBeneficiosAlumnoPespecifico>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBeneficiosAlumnoPespecifico Add(BeneficioAlumnoPEspecifico entidad)
        {
            try
            {
                var BeneficioAlumnoPEspecifico = MapeoEntidad(entidad);
                base.Insert(BeneficioAlumnoPEspecifico);
                return BeneficioAlumnoPEspecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBeneficiosAlumnoPespecifico Update(BeneficioAlumnoPEspecifico entidad)
        {
            try
            {
                var BeneficioAlumnoPEspecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                BeneficioAlumnoPEspecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(BeneficioAlumnoPEspecifico);
                return BeneficioAlumnoPEspecifico;
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


        public IEnumerable<TBeneficiosAlumnoPespecifico> Add(IEnumerable<BeneficioAlumnoPEspecifico> listadoEntidad)
        {
            try
            {
                List<TBeneficiosAlumnoPespecifico> listado = new List<TBeneficiosAlumnoPespecifico>();
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

        public IEnumerable<TBeneficiosAlumnoPespecifico> Update(IEnumerable<BeneficioAlumnoPEspecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBeneficiosAlumnoPespecifico> listado = new List<TBeneficiosAlumnoPespecifico>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene [IdPGeneral, IdPEspecifico, Paquete, IdMatriculaCabecera, IdAlumno] existentes en una lista 
        /// para ser guardalas
        /// </summary>
        /// <returns></returns>
        public List<BeneficioAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                List<BeneficioAlumnoPEspecificoDTO> rpta = new List<BeneficioAlumnoPEspecificoDTO>();
                var query = "SELECT IdPGeneral, IdPEspecifico, Paquete, IdMatriculaCabecera, IdAlumno, IdPais FROM fin.V_ObtenerDatosCodigoMatricula WHERE CodigoMatricula = @CodigoMatricula";
                var resultado = _dapperRepository.QueryDapper(query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioAlumnoPEspecificoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene [IdPGeneral, IdPEspecifico, IdMatriculaCabecera, IdAlumno] existentes en una lista cuando no tienne pAquetes
        /// para ser guardalas
        /// </summary>
        /// <returns></returns>
        public List<BeneficioAlumnoPEspecificoDTO> ObtenerDatosPorCodigoMatriculaSinPaquete(string codigoMatricula)
        {
            try
            {
                List<BeneficioAlumnoPEspecificoDTO> rpta = new List<BeneficioAlumnoPEspecificoDTO>();
                var query = "SELECT IdPGeneral, IdPEspecifico, IdMatriculaCabecera, IdAlumno FROM fin.V_ObtenerDatosCodigoMatricula_SinPaquete WHERE CodigoMatricula = @CodigoMatricula";
                var resultado = _dapperRepository.QueryDapper(query, new { CodigoMatricula = codigoMatricula });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioAlumnoPEspecificoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios del programa tipo 1 es decira aquellos programas que tienes versiones
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosProgramaTipo1DTO> ObtenerBeneficiosProgramaTipo1(int idPGeneral, int idPais, int? paquete)
        {
            try
            {
                List<BeneficiosProgramaTipo1DTO> beneficiosAlumnoPEspecificoes = new List<BeneficiosProgramaTipo1DTO>();

                if (paquete == 1)
                {

                    var query = string.Empty;
                    query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                        " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1) and OrdenBeneficio <> 1";
                    var beneficiosAlumnoPEspecificoesDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral, IdPais = idPais });
                    if (!string.IsNullOrEmpty(beneficiosAlumnoPEspecificoesDB) && !beneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        beneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(beneficiosAlumnoPEspecificoesDB)!;
                    }
                }
                else if (paquete == 2)
                {

                    var query = string.Empty;
                    query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                         " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1,2) and OrdenBeneficio <> 1";
                    var beneficiosAlumnoPEspecificoesDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral, IdPais = idPais });
                    if (!string.IsNullOrEmpty(beneficiosAlumnoPEspecificoesDB) && !beneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        beneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(beneficiosAlumnoPEspecificoesDB)!;
                    }
                }
                else
                {

                    var query = string.Empty;
                    query = "SELECT Paquete, Descripcion, OrdenBeneficio FROM pla.V_BeneficiosProgramaTipo1 WHERE Id = @IdPGeneral and CodigoPais = @IdPais AND EstadoMontoPago = 1 AND" +
                         " EstadoMontoPagoSuscripcion = 1 AND EstadoSuscripcionProgramaGeneral = 1 AND Paquete IS NOT NULL and Paquete in (1,2,3) and OrdenBeneficio <> 1";
                    var beneficiosAlumnoPEspecificoesDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral, IdPais = idPais });
                    if (!string.IsNullOrEmpty(beneficiosAlumnoPEspecificoesDB) && !beneficiosAlumnoPEspecificoesDB.Contains("[]"))
                    {
                        beneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo1DTO>>(beneficiosAlumnoPEspecificoesDB)!;
                    }
                }
                return beneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios del programa tipo 2 es decir aquellos programas que no tienen versiones
        /// </summary>
        /// <returns></returns>
        public List<BeneficiosProgramaTipo2DTO> ObtenerBeneficiosProgramaTipo2(int idPGeneral)
        {
            try
            {
                List<BeneficiosProgramaTipo2DTO> beneficiosAlumnoPEspecificoes = new List<BeneficiosProgramaTipo2DTO>();
                var query = string.Empty;
                query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = 'Beneficios' AND IdProgramaGeneral = @IdPGeneral AND " +
                    "EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
                var beneficiosAlumnoPEspecificoesDB = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(beneficiosAlumnoPEspecificoesDB) && !beneficiosAlumnoPEspecificoesDB.Contains("[]"))
                {
                    beneficiosAlumnoPEspecificoes = JsonConvert.DeserializeObject<List<BeneficiosProgramaTipo2DTO>>(beneficiosAlumnoPEspecificoesDB)!;
                }
                return beneficiosAlumnoPEspecificoes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
