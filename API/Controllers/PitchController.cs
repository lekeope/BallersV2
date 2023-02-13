 using Microsoft.AspNetCore.Mvc;


[Route("api/pitchAPI")]
public class PitchController : Controller
{

    private readonly AppDbContext _db;

    public PitchController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Pitch>> GetPitches()
    {
        return Ok(_db.Pitches.ToList());
    }

    [HttpGet("{id:int}", Name = "GetPitch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Pitch> GetPitch(int id)
    {
        if (id < 1)
            return BadRequest();

        var pitch = _db.Pitches.FirstOrDefault(u => u.Id == id);
        if (pitch == null) { return NotFound(); }
        return Ok(pitch);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Pitch> CreatePitch([FromBody] Pitch pitch)
    {
        if (pitch == null) { 
            Console.WriteLine("Pitch is Null");
            return BadRequest(pitch);
        }
        if (pitch.Id > 0)
            return BadRequest(pitch);

        _db.Pitches.Add(pitch);
        _db.SaveChanges();
        return CreatedAtRoute("GetPitch", new { id = pitch.Id }, pitch);
    }

    [HttpDelete("{id:int}", Name = "DeletePitch")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePitch(int id)
    {
        if (id < 1)
            return BadRequest();
        var pitch = _db.Pitches.FirstOrDefault(u => u.Id == id);
        if (pitch == null) { return NotFound(); }
        _db.Pitches.Remove(pitch);
        _db.SaveChanges();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdatePitch(int id, [FromBody] Pitch pitch)
    {
        if (pitch == null || id != pitch.Id) return BadRequest();
        _db.Pitches.Update(pitch);
        _db.SaveChanges();
        return NoContent();
    }
}