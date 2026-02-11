using Microsoft.AspNetCore.Mvc;
using Student_WebApp.Models;
using Student_WebApp.Services;

namespace Student_WebApp.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ApiService _api;

        public StudentController(ApiService api)
        {
            _api = api;
        }
        // READ
        public async Task<IActionResult> Index()
        {
            var students = await _api.GetAsync<List<StudentViewModel>>("v1/students");
            return View(students);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _api.PostAsync<StudentViewModel>("v1/students", model);
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var student =
                await _api.GetByIdAsync<StudentViewModel>($"v1/students/{id}");

            return View(student);
        }

        // EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _api.PutAsync($"v1/students/{id}", model);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            await _api.DeleteAsync($"v1/students/{id}");
            return RedirectToAction(nameof(Index));
        }
    }

}
