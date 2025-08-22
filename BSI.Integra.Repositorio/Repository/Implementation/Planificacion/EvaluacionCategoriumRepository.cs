using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CriterioEvaluacionRepository
    /// Autor: Klebert Layme.
    /// Fecha: 08/05/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacionCategorium
    /// </summary>
    public class EvaluacionCategoriumRepository : GenericRepository<TCriterioEvaluacionCategorium>, IEvaluacionCategoriumRepository
    {
        private Mapper _mapper;

        public EvaluacionCategoriumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionCategorium, CriterioEvaluacionCategorium>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCriterioEvaluacionCategorium MapeoEntidad(CriterioEvaluacionCategorium entidad)
        {
            try
            {
                //crea la entidad padre
                TCriterioEvaluacionCategorium modelo = _mapper.Map<TCriterioEvaluacionCategorium>(entidad);

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

        public TCriterioEvaluacionCategorium Add(CriterioEvaluacionCategorium entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                base.Insert(Cargo);
                return Cargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionCategorium Update(CriterioEvaluacionCategorium entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Cargo.RowVersion = entidadExistente.RowVersion;

                base.Update(Cargo);
                return Cargo;
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


        public IEnumerable<TCriterioEvaluacionCategorium> Add(IEnumerable<CriterioEvaluacionCategorium> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionCategorium> listado = new List<TCriterioEvaluacionCategorium>();
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

        public IEnumerable<TCriterioEvaluacionCategorium> Update(IEnumerable<CriterioEvaluacionCategorium> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionCategorium> listado = new List<TCriterioEvaluacionCategorium>();
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

        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 07/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EvaluacionCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,
                                   Nombre
                            FROM gp.T_EvaluacionCategoria
                            WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 07/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EvaluacionCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,
                                   Nombre
                            FROM gp.T_EvaluacionCategoria
                            WHERE Estado = 1;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
        
    }
}