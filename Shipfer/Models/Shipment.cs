using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Shipfer.Models;

[Table("shipment")]
public class Shipment : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public string Id { get; set; }
    [Column("destination")]

    public string Destination { get; set; }

    [Column("origin")]
    public string Origin { get; set; }

    [Column("is_delivered")]
    public bool IsDelivered { get; set; }
}
