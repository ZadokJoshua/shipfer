using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipfer.Models;

[Table("profiles")]
public class Profile : BaseModel
{
    [PrimaryKey("id", shouldInsert: true)]
    public string Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("address")]
    public string Address { get; set; }

    [Column("country_code")]
    public string CountryCode { get; set; }

    [Column("postal")]
    public string Postal { get; set; }
}
