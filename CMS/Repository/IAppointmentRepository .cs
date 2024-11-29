using CMS.Models;
using System.Collections.Generic;
namespace CMS.Repository
{
    public interface IAppointmentRepository
    {

        Appointment GetAppointmentById(int appointmentId);
        IEnumerable<Appointment> GetTodaysAppointments(int doctorId);

    }
}
