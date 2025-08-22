using AutoMapper;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class MatriculasMoodleService : IMatriculasMoodleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculasMoodleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        /// Autor: Jashin Salazar
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Quita la matricula 
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPespecifico"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool QuitarMatricula(int idMatriculaCabecera, int idPespecifico, string usuario)
        {
            try
            {
                MoodleCronogramaEvaluacionService moodleCronogramaEvaluacionService = new MoodleCronogramaEvaluacionService(_unitOfWork);
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
