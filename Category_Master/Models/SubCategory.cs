using System;
using System.Collections.Generic;

namespace Category_Master.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ImageMst1> ImageMst1s { get; set; } = new List<ImageMst1>();
}
