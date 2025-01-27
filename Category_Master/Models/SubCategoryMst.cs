using System;
using System.Collections.Generic;

namespace Category_Master.Models;

public partial class SubCategoryMst
{
    public int Id { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual CategoryMst Category { get; set; } = null!;

    public virtual ICollection<ImageMst> ImageMsts { get; set; } = new List<ImageMst>();
}
