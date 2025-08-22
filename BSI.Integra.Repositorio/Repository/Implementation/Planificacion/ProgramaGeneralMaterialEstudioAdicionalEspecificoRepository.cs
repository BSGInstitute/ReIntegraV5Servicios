
using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository : GenericRepository<TProgramaGeneralMaterialEstudioAdicionalEspecifico>, IProgramaGeneralMaterialEstudioAdicionalEspecificoRepository
    {
        private Mapper _mapper;
        public ProgramaGeneralMaterialEstudioAdicionalEspecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralMaterialEstudioAdicionalEspecifico, ProgramaGeneralMaterialEstudioAdicionalEspecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralMaterialEstudioAdicionalEspecifico MapeoEntidad(ProgramaGeneralMaterialEstudioAdicionalEspecifico entidad)
        {
            try
            {
                TProgramaGeneralMaterialEstudioAdicionalEspecifico modelo = _mapper.Map<TProgramaGeneralMaterialEstudioAdicionalEspecifico>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralMaterialEstudioAdicionalEspecifico Add(ProgramaGeneralMaterialEstudioAdicionalEspecifico entidad)
        {
            try
            {
                var programaGeneralMaterialEstudioAdicionalEspecifico = MapeoEntidad(entidad);
                base.Insert(programaGeneralMaterialEstudioAdicionalEspecifico);
                return programaGeneralMaterialEstudioAdicionalEspecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProgramaGeneralMaterialEstudioAdicionalEspecifico Update(ProgramaGeneralMaterialEstudioAdicionalEspecifico entidad)
        {
            try
            {
                var programaGeneralMaterialEstudioAdicionalEspecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                programaGeneralMaterialEstudioAdicionalEspecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(programaGeneralMaterialEstudioAdicionalEspecifico);
                return programaGeneralMaterialEstudioAdicionalEspecifico;
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
        public IEnumerable<TProgramaGeneralMaterialEstudioAdicionalEspecifico> Add(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralMaterialEstudioAdicionalEspecifico> listado = new List<TProgramaGeneralMaterialEstudioAdicionalEspecifico>();
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
        public IEnumerable<TProgramaGeneralMaterialEstudioAdicionalEspecifico> Update(IEnumerable<ProgramaGeneralMaterialEstudioAdicionalEspecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralMaterialEstudioAdicionalEspecifico> listado = new List<TProgramaGeneralMaterialEstudioAdicionalEspecifico>();
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

        public IEnumerable<ValorDTO> ObtenerIdsPorIdPgeneral(int idPGeneral)
        {
            IEnumerable<ValorDTO> rpta = new List<ValorDTO>();
            var query = @"SELECT Id, IdPEspecifico AS Valor
                                FROM pla.T_ProgramaGeneralMaterialEstudioAdicionalEspecificos
                                WHERE Estado = 1 AND MaterialEstudioAdicionalPorPGeneralId = @idPGeneral";
            var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<List<ValorDTO>>(resultado);
            }
            return rpta;
        }
        public ProgramaGeneralMaterialEstudioAdicionalEspecifico ObtenerPorIdyIdPgeneral(int idPGeneral, int idPEspecifico)
        {
            ProgramaGeneralMaterialEstudioAdicionalEspecifico rpta = new ProgramaGeneralMaterialEstudioAdicionalEspecifico();
            var query = @"SELECT Id,
                                        MaterialEstudioAdicionalPorPGeneralId,
                                        IdPEspecifico,
                                        Estado,
                                        UsuarioCreacion,
                                        UsuarioModificacion,
                                        FechaCreacion,
                                        FechaModificacion,
                                        RowVersion,
                                        IdMigracion
                                FROM pla.T_ProgramaGeneralMaterialEstudioAdicionalEspecificos
                                WHERE Estado = 1 AND IdPEspecifico = @idPEspecifico AND MaterialEstudioAdicionalPorPGeneralId = @idPGeneral";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico, idPGeneral });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<ProgramaGeneralMaterialEstudioAdicionalEspecifico>(resultado);
            }
            return rpta;
        }
    }
}
