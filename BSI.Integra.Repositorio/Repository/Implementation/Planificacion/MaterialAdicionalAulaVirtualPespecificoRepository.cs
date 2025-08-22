
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
    public class MaterialAdicionalAulaVirtualPespecificoRepository : GenericRepository<TMaterialAdicionalAulaVirtualPespecifico>, IMaterialAdicionalAulaVirtualPespecificoRepository
    {
        private Mapper _mapper;

        public MaterialAdicionalAulaVirtualPespecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPespecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAdicionalAulaVirtualPespecifico MapeoEntidad(MaterialAdicionalAulaVirtualPespecifico entidad)
        {
            try
            {
                TMaterialAdicionalAulaVirtualPespecifico modelo = _mapper.Map<TMaterialAdicionalAulaVirtualPespecifico>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtualPespecifico Add(MaterialAdicionalAulaVirtualPespecifico entidad)
        {
            try
            {
                var materialAdicionalAulaVirtualPespecifico = MapeoEntidad(entidad);
                base.Insert(materialAdicionalAulaVirtualPespecifico);
                return materialAdicionalAulaVirtualPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtualPespecifico Update(MaterialAdicionalAulaVirtualPespecifico entidad)
        {
            try
            {
                var materialAdicionalAulaVirtualPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                materialAdicionalAulaVirtualPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(materialAdicionalAulaVirtualPespecifico);
                return materialAdicionalAulaVirtualPespecifico;
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
        public IEnumerable<TMaterialAdicionalAulaVirtualPespecifico> Add(IEnumerable<MaterialAdicionalAulaVirtualPespecifico> listadoEntidad)
        {
            try
            {
                List<TMaterialAdicionalAulaVirtualPespecifico> listado = new List<TMaterialAdicionalAulaVirtualPespecifico>();
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
        public IEnumerable<TMaterialAdicionalAulaVirtualPespecifico> Update(IEnumerable<MaterialAdicionalAulaVirtualPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAdicionalAulaVirtualPespecifico> listado = new List<TMaterialAdicionalAulaVirtualPespecifico>();
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
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtualPespecifico relacionados a su IdMaterialAdicional.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<int> ObtenerIdsMaterialAdicionalDetallePespecifico(int idMaterialAdicional)
        {
            IEnumerable<IntDTO> rpta = new List<IntDTO>();
            var query = "SELECT IdPespecifico AS Valor FROM pla.T_MaterialAdicionalAulaVirtualPespecifico WHERE Estado = 1 AND IdMaterialAdicionalAulaVirtual = @idMaterialAdicional";
            var resultado = _dapperRepository.QueryDapper(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado);
                return rpta.Select(x => x.Valor.Value);
            }
            return new List<int>();
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtualRegistro relacionados a su IdMaterialAdicional.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional)
        {
            IEnumerable<ValorDTO> rpta = new List<ValorDTO>();
            var query = "SELECT Id, IdPEspecifico AS Valor FROM pla.T_MaterialAdicionalAulaVirtualPEspecifico WHERE Estado = 1 and IdMaterialAdicionalAulaVirtual = @idMaterialAdicional";
            var resultado = _dapperRepository.QueryDapper(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<ValorDTO>>(resultado)!;
            }
            return rpta;
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtualRegistro relacionados a su IdMaterialAdicional.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public MaterialAdicionalAulaVirtualPespecifico ObtenerPorIdPespecificoIdMaterialAdicional(int idMaterialAdicional, int idPEspecifico)
        {
            MaterialAdicionalAulaVirtualPespecifico rpta = new MaterialAdicionalAulaVirtualPespecifico();
            var query = @"SELECT Id,
                                    IdPEspecifico,
                                    IdMaterialAdicionalAulaVirtual,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion,
                                    IdMigracion 
                                FROM pla.T_MaterialAdicionalAulaVirtualPEspecifico
                                WHERE Estado = 1 AND IdMaterialAdicionalAulaVirtual = @idMaterialAdicional AND idPEspecifico = @idPEspecifico";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idMaterialAdicional, idPEspecifico });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtualPespecifico>(resultado)!;
            }
            return rpta;
        }
    }
}
