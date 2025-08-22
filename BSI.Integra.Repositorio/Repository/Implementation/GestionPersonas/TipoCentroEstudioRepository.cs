using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using Newtonsoft.Json;
using AutoMapper;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: TipoCentroEstudioRepository
    /// Autor: Juan D. Huanaco Quispe
    /// Fecha: 08/04/2024
    /// <summary>
    /// Gestión general de T_TipoCentroEstudio
    /// </summary>
    public class TipoCentroEstudioRepository : GenericRepository<TTipoCentroEstudio>, ITipoCentroEstudioRepository
    {
        private Mapper _mapper;

        public TipoCentroEstudioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCentroEstudio, TipoCentroEstudio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCentroEstudio MapeoEntidad(TipoCentroEstudio entidad)
        {
            try
            {
                TTipoCentroEstudio modelo = _mapper.Map<TTipoCentroEstudio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoCentroEstudio Add(TipoCentroEstudio entidad)
        {
            try
            {
                var tipoCentroEstudio = MapeoEntidad(entidad);
                base.Insert(tipoCentroEstudio);
                return tipoCentroEstudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCentroEstudio Update(TipoCentroEstudio entidad)
        {
            try
            {
                var tipoCentroEstudio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                tipoCentroEstudio.RowVersion = entidadExistente.RowVersion;

                base.Update(tipoCentroEstudio);
                return tipoCentroEstudio;
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
            base.Delete(id, usuario);
        }


        public IEnumerable<TTipoCentroEstudio> Add(IEnumerable<TipoCentroEstudio> listadoEntidad)
        {
            try
            {
                List<TTipoCentroEstudio> listado = new List<TTipoCentroEstudio>();
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

        public IEnumerable<TTipoCentroEstudio> Update(IEnumerable<TipoCentroEstudio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TTipoCentroEstudio>();
                foreach (var entidad in listadoEntidad)
                    listado.Add(MapeoEntidad(entidad));

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

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 08/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCentroEstudio.
        /// </summary>
        /// <returns>IEnumerable TipoCentroEstudioDTO</returns>
        IEnumerable<TipoCentroEstudioDTO> ITipoCentroEstudioRepository.Obtener()
        {
            try
            {
                var rpta = new List<TipoCentroEstudioDTO>();
                var query = @"
                    SELECT Id, Nombre
                    FROM gp.T_TipoCentroEstudio
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCentroEstudioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan D. Huanaco Quispe
        /// Fecha: 09/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_TipoCentroEstudio asociado a un identificador.
        /// </summary>
        /// <param name="idTipoCentroEstudio">Id de TipoCentroEstudio</param>
        /// <returns>TipoCentroEstudioDTO</returns>
        TipoCentroEstudio? ITipoCentroEstudioRepository.ObtenerPorId(int idTipoCentroEstudio)
        {
            try
            {
                TipoCentroEstudio rpta = new();
                var query = @"
                    SELECT Nombre
                    FROM gp.T_TipoCentroEstudio
                    WHERE Estado = 1 AND Id = @idTipoCentroEstudio";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idTipoCentroEstudio });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoCentroEstudio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }

    }
}
