using System.ComponentModel.DataAnnotations;

namespace ReShareBlazorApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(8, ErrorMessage = "Username must be at least 8 characters long.")]
        [MaxLength(24, ErrorMessage = "Username cannot exceed 24 characters.")]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
        public bool AcceptTerms { get; set; }
    }
}
