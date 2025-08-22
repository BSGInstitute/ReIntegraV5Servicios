using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class EvaluacionCategoriaRepository: GenericRepository<TEvaluacionCategorium>, IEvaluacionCategoriaRepository
    {
        private Mapper _mapper;
        public EvaluacionCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentralLlamadaDireccion, EvaluacionCategoria>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEvaluacionCategorium MapeoEntidad(EvaluacionCategoria entidad)
        {
            try
            {
                TEvaluacionCategorium modelo = _mapper.Map<TEvaluacionCategorium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEvaluacionCategorium Add(EvaluacionCategoria entidad)
        {
            try
            {
                var evaluacionCategoria = MapeoEntidad(entidad);
                base.Insert(evaluacionCategoria);
                return evaluacionCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEvaluacionCategorium Update(EvaluacionCategoria entidad)
        {
            try
            {
                var evaluacionCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                evaluacionCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(evaluacionCategoria);
                return evaluacionCategoria;
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


        public IEnumerable<TEvaluacionCategorium> Add(IEnumerable<EvaluacionCategoria> listadoEntidad)
        {
            try
            {
                List<TEvaluacionCategorium> listado = new List<TEvaluacionCategorium>();
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

        public IEnumerable<TEvaluacionCategorium> Update(IEnumerable<EvaluacionCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                var listado = new List<TEvaluacionCategorium>();
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
        public List<FiltroDTO> ObtenerCategoriasEvaluacionRegistradas()

        {

            try
            {
                List<FiltroDTO> rpta = new List<FiltroDTO>();
                var query = @" SELECT Id, Nombre FROM gp.T_EvaluacionCategoria WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
