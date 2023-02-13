using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Pitch
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PitchType { get; set; }
    public string PitchSize { get; set; }
    public string PostCode { get; set; }
    public string Address { get; set; }
    public string Note { get; set; }

    public string? videoURL { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public int LastUpdatedBy { get; set; }
}
