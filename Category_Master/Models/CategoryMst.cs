using System;
using System.Collections.Generic;

namespace Category_Master.Models;

public partial class CategoryMst
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<ImageMst> ImageMsts { get; set; } = new List<ImageMst>();

    public virtual ICollection<SubCategoryMst> SubCategoryMsts { get; set; } = new List<SubCategoryMst>();
}
