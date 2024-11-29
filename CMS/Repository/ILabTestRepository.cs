using CMS.Models;

namespace CMS.Repository
{
    public interface ILabTestRepository
    {
        LabTest GetLabTestById(int testId);
        List<LabTest> GetAllLabTests();
    }
}
