using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class TestModel : PageModel
    {
        public String? Greeting { get; set; }
        
        public void OnGet(string? name, int? age)
        {
            if (age != null && name != null)
            {
                Greeting = $"Hello, {name}. Your age is {age + DateTime.Now.Second}";
                
            } else if (name != null)
            {
                Greeting = $"Hello, {name}. Your age is {DateTime.Now.Second}";
            }
        }
        
    }
}