using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Models.DTO
{
    public class ApiResponseDTO<T>
    {
        public T Data { get; set; }
    }
}
