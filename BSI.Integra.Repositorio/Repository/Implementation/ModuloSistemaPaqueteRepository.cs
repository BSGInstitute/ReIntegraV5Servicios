using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class ModuloSistemaPaqueteRepository : GenericRepository<TModuloSistemaPaqueteV5>, IModuloSistemaPaqueteRepository
    {
        private Mapper _mapper;
        public ModuloSistemaPaqueteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TModuloSistemaPaqueteV5, ModuloSistemaPaqueteV5DTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModuloSistemaPaqueteV5 MapeoEntidad(TModuloSistemaPaqueteV5 entidad)
        {
            try
            {
                TModuloSistemaPaqueteV5 modelo = _mapper.Map<TModuloSistemaPaqueteV5>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TModuloSistemaPaqueteV5 Add(TModuloSistemaPaqueteV5 entidad)
        {
            try
            {
                var moduloSistemaPaquete = MapeoEntidad(entidad);
                base.Insert(moduloSistemaPaquete);
                return moduloSistemaPaquete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModuloSistemaPaqueteV5 Update(TModuloSistemaPaqueteV5 entidad)
        {
            try
            {
                var moduloSistemaPaquete = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                moduloSistemaPaquete.RowVersion = entidadExistente.RowVersion;

                base.Update(moduloSistemaPaquete);
                return moduloSistemaPaquete;
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
        #endregion
        public TModuloSistemaPaqueteV5 ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT * FROM conf.T_ModuloSistemaPaqueteV5 WHERE Estado = 1";
                var res = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(res) && res != "null")
                {
                    return JsonConvert.DeserializeObject<TModuloSistemaPaqueteV5>(res);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ModuloSistemaPaqueteV5DTO> Obtener()
        {
            try
            {
                var query = @"SELECT Id, Nombre, IdModuloSistema, Descripcion FROM conf.T_ModuloSistemaPaqueteV5 WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ModuloSistemaPaqueteV5DTO>>(res);
                }
                return new List<ModuloSistemaPaqueteV5DTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerModulos(int idPaquete)
        {
            try
            {
                var query = @"conf.SP_PaqueteModuloV5";
                var res = _dapperRepository.QuerySPDapper(query, new { idPaquete });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ModuloSistemaPaqueteModulosV5DTO>>(res);
                }
                return new List<ModuloSistemaPaqueteModulosV5DTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ModuloSistemaPaqueteModulosV5DTO> ObtenerListaModulos(int idPaquete)
        {
            try
            {
                var query = @"conf.SP_PaqueteListaModuloV5";
                var res = _dapperRepository.QuerySPDapper(query, new { idPaquete });
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ModuloSistemaPaqueteModulosV5DTO>>(res);
                }
                return new List<ModuloSistemaPaqueteModulosV5DTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
