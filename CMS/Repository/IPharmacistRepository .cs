using CMS.Models;
using CMS.Models.CMS.Models;

namespace CMS.Repository
{
    public interface IPharmacistRepository
    {
        List<MedicinePrescriptionModel> GetMedicinePrescriptions();

        public AppointmentDetailsModel GetAppointmentDetails(int? appointmentId);
        //public BillDetailsModel GetBillDetails(int appointmentId);

        public PrescriptionDetailModel GetBillDetails(int? appointmentId);
    }
}
