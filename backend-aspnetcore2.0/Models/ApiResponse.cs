using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Woa.ApiModels
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
