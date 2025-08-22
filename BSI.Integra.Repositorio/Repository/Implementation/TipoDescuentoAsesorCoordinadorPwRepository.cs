using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class TipoDescuentoAsesorCoordinadorPwRepository : GenericRepository<TTipoDescuentoAsesorCoordinadorPw>, ITipoDescuentoAsesorCoordinadorPwRepository
    {

        private Mapper _mapper;

        public TipoDescuentoAsesorCoordinadorPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoDescuentoAsesorCoordinadorPw MapeoEntidad(TipoDescuentoAsesorCoordinadorPw entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoDescuentoAsesorCoordinadorPw modelo = _mapper.Map<TTipoDescuentoAsesorCoordinadorPw>(entidad);

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

        public TTipoDescuentoAsesorCoordinadorPw Add(TipoDescuentoAsesorCoordinadorPw entidad)
        {
            try
            {
                var TipoDescuentoAsesorCoordinadorPw = MapeoEntidad(entidad);
                base.Insert(TipoDescuentoAsesorCoordinadorPw);
                return TipoDescuentoAsesorCoordinadorPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoDescuentoAsesorCoordinadorPw Update(TipoDescuentoAsesorCoordinadorPw entidad)
        {
            try
            {
                var TipoDescuentoAsesorCoordinadorPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDescuentoAsesorCoordinadorPw.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDescuentoAsesorCoordinadorPw);
                return TipoDescuentoAsesorCoordinadorPw;
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


        public IEnumerable<TTipoDescuentoAsesorCoordinadorPw> Add(IEnumerable<TipoDescuentoAsesorCoordinadorPw> listadoEntidad)
        {
            try
            {
                List<TTipoDescuentoAsesorCoordinadorPw> listado = new List<TTipoDescuentoAsesorCoordinadorPw>();
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

        public IEnumerable<TTipoDescuentoAsesorCoordinadorPw> Update(IEnumerable<TipoDescuentoAsesorCoordinadorPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDescuentoAsesorCoordinadorPw> listado = new List<TTipoDescuentoAsesorCoordinadorPw>();
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
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id registros de TipoDescuentoAsesorCoordinadorPw
        /// </summary>
        /// <returns> List<TipoDescuentoAsesorCoordinadorPwDTO> </returns>
        public IEnumerable<TipoDescuentoAsesorCoordinadorPw> ObtenerPorIdTipoDescuento(int idTipoDescuento)
        {
            try
            {
                IEnumerable<TipoDescuentoAsesorCoordinadorPw> rpta = new List<TipoDescuentoAsesorCoordinadorPw>();
                var query = @"
                    SELECT Id	,
		                Tipo,
		                IdTipoDescuento,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion 
                    FROM pla.T_TipoDescuentoAsesorCoordinador_PW
                    WHERE Estado = 1 AND idTipoDescuento=@idTipoDescuento";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<TipoDescuentoAsesorCoordinadorPw>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error en ObtenerPorId() : {ex.Message}", ex);
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id registros de TipoDescuentoAsesorCoordinadorPw
        /// </summary>
        /// <returns> List<TipoDescuentoAsesorCoordinadorPwDTO> </returns>
        public IEnumerable<string> ObtenerTiposPorIdTipoDescuento(int idTipoDescuento)
        {
            try
            {
                IEnumerable<StringDTO> rpta = new List<StringDTO>();
                var query = @"
                    SELECT Tipo AS Valor FROM pla.T_TipoDescuentoAsesorCoordinador_PW
                    WHERE Estado = 1 AND idTipoDescuento=@idTipoDescuento";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDescuento });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<StringDTO>>(resultado)!;
                }
                return rpta.Select(x => x.Valor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error en ObtenerPorId() : {ex.Message}", ex);
            }
        }
    }
}
