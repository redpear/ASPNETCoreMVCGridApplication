using ASPNETCoreMVCGridApplication.Models;

namespace ASPNETCoreMVCGridApplication.DAL
{
    public interface IEmployee_DAL
    {
        List<EmployeeViewModel> GetAll();
        bool Insert(EmployeeViewModel model);
        EmployeeViewModel GetById(int id);
        bool Update(EmployeeViewModel model);
        bool Delete(int id);
    }
}