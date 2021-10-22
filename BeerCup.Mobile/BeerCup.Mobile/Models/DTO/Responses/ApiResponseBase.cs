using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models.DTO
{
    public class ApiResponseBase<T>
    {
        public T Data { get; set; }

        public string Error { get; set; }
    }
}
