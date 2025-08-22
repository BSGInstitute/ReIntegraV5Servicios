
using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class MaterialAdicionalAulaVirtualRepository : GenericRepository<TMaterialAdicionalAulaVirtual>, IMaterialAdicionalAulaVirtualRepository
    {
        private Mapper _mapper;

        public MaterialAdicionalAulaVirtualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialAdicionalAulaVirtual, MaterialAdicionalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<MaterialAdicionalAulaVirtual, TMaterialAdicionalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistro>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialAdicionalAulaVirtualPespecifico, MaterialAdicionalAulaVirtualPespecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAdicionalAulaVirtual MapeoEntidad(MaterialAdicionalAulaVirtual entidad)
        {
            try
            {
                TMaterialAdicionalAulaVirtual modelo = _mapper.Map<TMaterialAdicionalAulaVirtual>(entidad);
                if (entidad.MaterialAdicionalAulaVirtualRegistros != null && entidad.MaterialAdicionalAulaVirtualRegistros.Count >= 1)
                {
                    modelo.TMaterialAdicionalAulaVirtualRegistros = _mapper.Map<List<TMaterialAdicionalAulaVirtualRegistro>>(entidad.MaterialAdicionalAulaVirtualRegistros);
                }
                if (entidad.MaterialAdicionalAulaVirtualPespecificos != null && entidad.MaterialAdicionalAulaVirtualPespecificos.Count >= 1)
                {
                    modelo.TMaterialAdicionalAulaVirtualPespecificos = _mapper.Map<List<TMaterialAdicionalAulaVirtualPespecifico>>(entidad.MaterialAdicionalAulaVirtualPespecificos);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtual Add(MaterialAdicionalAulaVirtual entidad)
        {
            try
            {
                var materialAdicionalAulaVirtual = MapeoEntidad(entidad);
                base.Insert(materialAdicionalAulaVirtual);
                return materialAdicionalAulaVirtual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtual Update(MaterialAdicionalAulaVirtual entidad)
        {
            try
            {
                var materialAdicionalAulaVirtual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                materialAdicionalAulaVirtual.RowVersion = entidadExistente.RowVersion;

                base.Update(materialAdicionalAulaVirtual);
                return materialAdicionalAulaVirtual;
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
        public IEnumerable<TMaterialAdicionalAulaVirtual> Add(IEnumerable<MaterialAdicionalAulaVirtual> listadoEntidad)
        {
            try
            {
                List<TMaterialAdicionalAulaVirtual> listado = new List<TMaterialAdicionalAulaVirtual>();
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
        public IEnumerable<TMaterialAdicionalAulaVirtual> Update(IEnumerable<MaterialAdicionalAulaVirtual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAdicionalAulaVirtual> listado = new List<TMaterialAdicionalAulaVirtual>();
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
        public IEnumerable<MaterialAdicionalAulaVirtualDTO> ObtenerMaterialAdicional()
        {
            IEnumerable<MaterialAdicionalAulaVirtualDTO> rpta = new List<MaterialAdicionalAulaVirtualDTO>();
            var query = "SELECT Id, NombreConfiguracion, IdPGeneral, Nombre FROM pla.V_ProgramaGeneralMaterialAdicionalAulaVirtual";
            var resultado = _dapperRepository.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<MaterialAdicionalAulaVirtualDTO>>(resultado);
            }
            return rpta;
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtual relacionados a su IdMaterialAdicional.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public MaterialAdicionalAulaVirtualDTO ObtenerMaterialAdicionalDetalle(int idMaterialAdicional)
        {
            MaterialAdicionalAulaVirtualDTO rpta = new MaterialAdicionalAulaVirtualDTO();
            var query = "SELECT Id, NombreConfiguracion, IdPgeneral, EsOnline FROM pla.T_MaterialAdicionalAulaVirtual WHERE Estado = 1 AND Id = @idMaterialAdicional";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtualDTO>(resultado);
            }
            return rpta;
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoMarcador relacionados
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<ComboDTO> ObtenerMarcadorCombo()
        {
            IEnumerable<ComboDTO> rpta = new List<ComboDTO>();
            var query = "SELECT Id, Nombre FROM ope.T_TipoMarcador WHERE Estado = 1";
            var resultado = _dapperRepository.QueryDapper(query, null);
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);
            }
            return rpta;
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoMarcador relacionados
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public MaterialAdicionalAulaVirtual ObtenerPorId(int idMaterialAdicional)
        {
            MaterialAdicionalAulaVirtual rpta = new MaterialAdicionalAulaVirtual();
            var query = @"SELECT Id,
	                                NombreConfiguracion,
	                                IdPgeneral,
	                                EsOnline,
	                                Estado,
	                                UsuarioCreacion,
	                                UsuarioModificacion,
	                                FechaCreacion,
	                                FechaModificacion,
	                                RowVersion,
	                                IdMigracion
                                FROM pla.T_MaterialAdicionalAulaVirtual WHERE Id = @idMaterialAdicional";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtual>(resultado);
            }
            return rpta;
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtualRegistro relacionados
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional)
        {
            IEnumerable<ValorDTO> rpta = new List<ValorDTO>();
            var query = @"SELECT Id, IdMaterialAdicionalAulaVirtual AS Valor FROM pla.T_MaterialAdicionalAulaVirtualRegistro WHERE Estado = 1 AND IdMaterialAdicionalAulaVirtual = @idMaterialAdicional";
            var resultado = _dapperRepository.QueryDapper(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<ValorDTO>>(resultado)!;
            }
            return rpta;
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Envia notificaciones a los alumnos relacionados al pespecifico si se a asignado un material adicional
        /// </summary>
        /// <returns> true o false </returns>
        public bool NotificacionMaterialAdicional(int idMaterialAdicional, int idPEspecifico, string usuario)
        {
            try
            {
                var query = "pla.SP_CrearNotificacionMaterialAdicionalIntegra";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdMaterialAdicionalAulaVirtual = idMaterialAdicional, idPEspecifico, usuario });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
