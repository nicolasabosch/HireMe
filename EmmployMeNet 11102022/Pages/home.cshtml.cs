using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmmploymeNet
{
    public class homeModel : PageModel
    {

        public string html { get; set; }


        public void OnGet()
        {

            //var x = Directory.GetCurrentDirectory();

            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\index.html")))
            {
                html = reader.ReadToEnd();
            }

        }
    }
}