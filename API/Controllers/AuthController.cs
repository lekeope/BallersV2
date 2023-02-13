using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public AuthController(/**/AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }
    [HttpGet("users")]
    public async Task<ActionResult<string>> Users()
    {
        return Ok(_db.Users.ToList());
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody]UserDTO userDTO)
    {
        CreatePasswordHash(userDTO.Password, out byte[] passwordHash,
        out byte[] passwordSalt);
        User user = new User();
        user.Username = userDTO.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _db.Users.Add(user);
        _db.SaveChanges();


        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UserDTO userDTO)
    {
        //var user = _db.Users.FirstOrDefault(u => u.Username == userDTO.Username);
        //if (user ==  null)
        if (userDTO.Username != user.Username)
            return BadRequest("User Not Found!");
 
        if (!VerifyPasswordHash(userDTO.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Invalid Password");
        }
        var token = CreateteToken(user);
        return Ok(token);
    }

    private string CreateteToken(User user)
    {
        List<Claim> claims = new List<Claim>
         {
             new Claim(ClaimTypes.Name, user.Username)
         };
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value)
            ) ;

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials : creds
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private void CreatePasswordHash(string password,
        out byte[] passwordHash,
        out byte[] passwordSalt
        )
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }
    }

    private bool VerifyPasswordHash(string password,
         byte[] passwordHash,
          byte[] passwordSalt
        )
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
             
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash); 

        }
    }
       
}
 
