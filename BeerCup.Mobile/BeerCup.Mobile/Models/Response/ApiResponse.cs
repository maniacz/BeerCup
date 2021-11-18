using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models
{
    public class ApiResponse<T> : ErrorResponseBase
    {
        public T Data { get; set; }
    }
}
