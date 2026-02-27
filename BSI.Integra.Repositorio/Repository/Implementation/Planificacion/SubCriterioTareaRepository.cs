using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: SubCriterioTareaRepository
    /// <summary>
    /// Gestión de T_SubCriterioTarea usando stored procedures (sin ORM)
    /// </summary>
    public class SubCriterioTareaRepository : ISubCriterioTareaRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public SubCriterioTareaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Inserta un nuevo SubCriterioTarea mediante SP
        /// </summary>
        public bool Insertar(SubCriterioTareaDTO subCriterioDTO, string usuario)
        {
            try
            {
                var sp = "pla.SP_TareaSubCriterio_Insertar";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Nombre              = subCriterioDTO.Nombre,
                    Descripcion         = subCriterioDTO.Descripcion,
                    Escala              = subCriterioDTO.Escala,
                    UsuarioCreacion     = usuario,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaRepository.Insertar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza un SubCriterioTarea existente mediante SP
        /// </summary>
        public bool Actualizar(SubCriterioTareaDTO subCriterioDTO, string usuario)
        {
            try
            {
                var sp = "pla.SP_TareaSubCriterio_Actualizar";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Id                  = subCriterioDTO.Id,
                    Nombre              = subCriterioDTO.Nombre,
                    Descripcion         = subCriterioDTO.Descripcion,
                    Escala              = subCriterioDTO.Escala,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaRepository.Actualizar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina lógicamente un SubCriterioTarea mediante SP
        /// </summary>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var sp = "pla.SP_TareaSubCriterio_Eliminar";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Id                  = id,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaRepository.Eliminar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene un SubCriterioTarea activo por su Id
        /// </summary>
        public SubCriterioTareaDTO ObtenerPorId(int idSubCriterio)
        {
            try
            {
                var query = @"SELECT Id, Nombre, Descripcion, Escala, Estado AS Activo
                              FROM pla.T_TareaSubCriterio
                              WHERE Estado = 1 AND Id = @Id";

                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idSubCriterio });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<SubCriterioTareaDTO>(resultado);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaRepository.ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todos los SubCriterioTarea activos
        /// </summary>
        public List<SubCriterioTareaDTO> ListarSubCriterios()
        {
            try
            {
                var query = @"SELECT Id, Nombre, Descripcion, Escala, Estado AS Activo
                              FROM pla.T_TareaSubCriterio
                              WHERE Estado = 1
                              ORDER BY Id DESC";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<List<SubCriterioTareaDTO>>(resultado);

                return new List<SubCriterioTareaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en SubCriterioTareaRepository.ListarSubCriterios: {ex.Message}", ex);
            }
        }
    }
}
