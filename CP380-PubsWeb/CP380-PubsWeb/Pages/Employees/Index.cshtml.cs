using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CP380_PubsLab.Models;
using Microsoft.EntityFrameworkCore;

namespace CP380_PubsWeb.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly PubsDbContext _db;
        public List<CP380_PubsLab.Models.Employee> Employee { get; set; }

        public IndexModel(PubsDbContext db)
        {
            _db = db;
        }
        public void OnGet(Int16 id)
        {
            var job = _db.Jobs.Include(j => j.Employee).First(j => j.J_ID == id);
            this.Employee = job.Employee.ToList();
        }
    }
}
