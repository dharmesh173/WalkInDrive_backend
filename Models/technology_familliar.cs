﻿using System.ComponentModel.DataAnnotations;

namespace WalkInDrive.Models
{
    public class technology_familliar
    {
        public int professional_details_work_id {  get; set; }
        [Key]
        public int technology_id { get; set; }
    }
}
