using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
