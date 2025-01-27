using System;
using System.Collections.Generic;

namespace Category_Master.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ImageMst1> ImageMst1s { get; set; } = new List<ImageMst1>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
