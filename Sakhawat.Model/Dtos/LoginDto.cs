using System.ComponentModel.DataAnnotations;

namespace Sakhawat.Model.Dtos
{
  public class LoginDto
  {
        [Required]
    public string LoginNameEnglish { get; set; }
        [Required]
    public string Password { get; set; }

    public decimal BranchId { get; set; }
  }
}
