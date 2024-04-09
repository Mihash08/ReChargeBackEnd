﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("category")]
    public class Category : BaseEntity
    {
        //todo: add recomendationImage
        [Column("name")]
        public string Name { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [ForeignKey("CategoryCategory")]
        [Column("cat_cat_id")]
        public int CategoryCategoryId {  get; set; }
        public CategoryCategory CategoryCategory { get; set; }
    }
}
