using System.Collections.Generic;
using CMS.Models;

namespace CMS.Repository
{
    public interface IConsultationRepository
    {
        List<PatientHistory> GetAllDetailsForHistory(int patientId, bool includeNew);
        void AddConsultationDetails(PatientHistory history);
    }
}
