using ASPNETCoreMVCGridApplication.DAL;
using ASPNETCoreMVCGridApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreMVCGridApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee_DAL _dal;

        public EmployeeController(IEmployee_DAL dal)
        {
           _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

            try
            {
                employees = _dal.GetAll();

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }


            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }

                bool result = _dal.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";

                    return View();
                }

                TempData["successMessage"] = "Employee details saved";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            try
            {
                EmployeeViewModel employee = _dal.GetById(id);

                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id: {id}";
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }

                bool result = _dal.Update(model);

              
                    if(!result)
                    {
                        TempData["errorMessage"] = "Unable to update the data";
                        return View();
                    }

                    TempData["successMessage"] = "Employee details updated";
                    return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                EmployeeViewModel employee = _dal.GetById(id);

                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Employee details not found with Id: {id}";
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmation(EmployeeViewModel model)
        {
            try
            {
                bool result = _dal.Delete(model.Id);


                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }

                TempData["successMessage"] = "Employee details deleted";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }





    }
}
