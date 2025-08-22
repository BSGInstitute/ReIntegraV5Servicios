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
    public class MaterialAdicionalAulaVirtualRegistroRepository : GenericRepository<TMaterialAdicionalAulaVirtualRegistro>, IMaterialAdicionalAulaVirtualRegistroRepository
    {
        private Mapper _mapper;

        public MaterialAdicionalAulaVirtualRegistroRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialAdicionalAulaVirtualRegistro, MaterialAdicionalAulaVirtualRegistro>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialAdicionalAulaVirtualRegistro MapeoEntidad(MaterialAdicionalAulaVirtualRegistro entidad)
        {
            try
            {
                TMaterialAdicionalAulaVirtualRegistro modelo = _mapper.Map<TMaterialAdicionalAulaVirtualRegistro>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtualRegistro Add(MaterialAdicionalAulaVirtualRegistro entidad)
        {
            try
            {
                var materialAdicionalAulaVirtualRegistro = MapeoEntidad(entidad);
                base.Insert(materialAdicionalAulaVirtualRegistro);
                return materialAdicionalAulaVirtualRegistro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialAdicionalAulaVirtualRegistro Update(MaterialAdicionalAulaVirtualRegistro entidad)
        {
            try
            {
                var materialAdicionalAulaVirtualRegistro = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                materialAdicionalAulaVirtualRegistro.RowVersion = entidadExistente.RowVersion;

                base.Update(materialAdicionalAulaVirtualRegistro);
                return materialAdicionalAulaVirtualRegistro;
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
        public IEnumerable<TMaterialAdicionalAulaVirtualRegistro> Add(IEnumerable<MaterialAdicionalAulaVirtualRegistro> listadoEntidad)
        {
            try
            {
                List<TMaterialAdicionalAulaVirtualRegistro> listado = new List<TMaterialAdicionalAulaVirtualRegistro>();
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
        public IEnumerable<TMaterialAdicionalAulaVirtualRegistro> Update(IEnumerable<MaterialAdicionalAulaVirtualRegistro> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialAdicionalAulaVirtualRegistro> listado = new List<TMaterialAdicionalAulaVirtualRegistro>();
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
        /// Obtiene todos los registros de T_MaterialAdicionalAulaVirtualRegistro relacionados a su IdMaterialAdicional.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<MaterialAdicionalAulaVirtualRegistroDTO> ObtenerMaterialAdicionalDetalleRegistro(int idMaterialAdicional)
        {
            IEnumerable<MaterialAdicionalAulaVirtualRegistroDTO> rpta = new List<MaterialAdicionalAulaVirtualRegistroDTO>();
            var query = "SELECT Id,NombreArchivo,RutaArchivo,EsEnlace,SoloLectura FROM pla.T_MaterialAdicionalAulaVirtualRegistro WHERE Estado = 1 AND IdMaterialAdicionalAulaVirtual=@idMaterialAdicional";
            var resultado = _dapperRepository.QueryDapper(query, new { idMaterialAdicional });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<IEnumerable<MaterialAdicionalAulaVirtualRegistroDTO>>(resultado);
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
        public IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional)
        {
            IEnumerable<ValorDTO> rpta = new List<ValorDTO>();
            var query = "SELECT Id, IdMaterialAdicionalAulaVirtual AS Valor FROM pla.T_MaterialAdicionalAulaVirtualRegistro WHERE Estado = 1 and IdMaterialAdicionalAulaVirtual = @idMaterialAdicional";
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
        public MaterialAdicionalAulaVirtualRegistro ObtenerPorId(int id)
        {
            MaterialAdicionalAulaVirtualRegistro rpta = new MaterialAdicionalAulaVirtualRegistro();
            var query = @"SELECT
                                    Id,
                                    IdMaterialAdicionalAulaVirtual,
                                    NombreArchivo,
                                    RutaArchivo ,
                                    EsEnlace,
                                    SoloLectura,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
RowVersion
                                FROM pla.T_MaterialAdicionalAulaVirtualRegistro
                                WHERE Estado = 1 AND Id = @id";
            var resultado = _dapperRepository.FirstOrDefault(query, new { id });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtualRegistro>(resultado)!;
            }
            return rpta;
        }

        public MaterialAdicionalAulaVirtualRegistro ObtenerPorIdYIdMaterialAdicionalAulaVirtual(int id, int idMaterialAdicionalAulaVirtual)
        {
            MaterialAdicionalAulaVirtualRegistro rpta = new MaterialAdicionalAulaVirtualRegistro();
            var query = @"SELECT
                                    Id,
                                    IdMaterialAdicionalAulaVirtual,
                                    NombreArchivo,
                                    RutaArchivo ,
                                    EsEnlace,
                                    SoloLectura,
                                    Estado,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    FechaCreacion,
                                    FechaModificacion,
                                    RowVersion
                                FROM pla.T_MaterialAdicionalAulaVirtualRegistro
                                WHERE Estado = 1 AND Id = @Id  and IdMaterialAdicionalAulaVirtual = @IdMaterialAdicionalAulaVirtual ";
            var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id, IdMaterialAdicionalAulaVirtual = idMaterialAdicionalAulaVirtual });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                rpta = JsonConvert.DeserializeObject<MaterialAdicionalAulaVirtualRegistro>(resultado)!;
            }
            return rpta;
        }
    }
}
