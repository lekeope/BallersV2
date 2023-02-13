using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Pitch")]
    public int PitchId { get; set; }
    public float Duration { get; set; }
    public DateTime AppointmentTime { get; set; }
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Id of the User who booked this event
    /// </summary>
    [ForeignKey("User")]
    public int UserId { get; set; }
}