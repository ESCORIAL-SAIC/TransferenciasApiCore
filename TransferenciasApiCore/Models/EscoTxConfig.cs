#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferenciasApiCore.Models;

[Table("EscoTxConfig", Schema = "web")]
public partial class EscoTxConfig
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("clave")]
    public string Clave { get; set; }
    
    [Column("valor")]
    public string Valor { get; set; }
    
    [Column("usuario")]
    public string Usuario { get; set; } 
}