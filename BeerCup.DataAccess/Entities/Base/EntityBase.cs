﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerCup.DataAccess.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
