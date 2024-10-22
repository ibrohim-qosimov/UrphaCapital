using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrphaCapital.Domain.Entities;

public class Result
{
    public long Id { get; set; }
    public string PictureUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
