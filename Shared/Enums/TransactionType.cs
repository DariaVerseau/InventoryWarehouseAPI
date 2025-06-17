using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionType
{
    [Display(Name = "Incoming")]
    Incoming = 1,
    
    [Display(Name = "Outgoing")]
    Outgoing = 2,
    
    [Display(Name = "Transfer")]
    Transfer = 3
    
}
