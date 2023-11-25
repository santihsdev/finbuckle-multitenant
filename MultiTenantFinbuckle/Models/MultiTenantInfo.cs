using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finbuckle.MultiTenant;

namespace MulltiTenantFinbuckle.Models;

[Table("tenant")]
public class MultiTenantInfo : ITenantInfo
{
    [Key] public string? Id { get; set; }
    [StringLength(120)] public string? Identifier { get; set; }

    [StringLength(120)] public string? Name { get; set; }
    [StringLength(120)] public string? ConnectionString { get; set; }
    [StringLength(120)] public string? Realm { get; set; }
    [StringLength(120)] public string? AuthServerUrl { get; set; }
    [StringLength(120)] public string? Resource { get; set; }
    [StringLength(120)] public string? Secret { get; set; }
}