using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WebApp.Models.Entiteti;

namespace WebApp.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    //custum
    public class UserRegistrationBindingModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        public string BirthdayDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int PassengerType { get; set; }            // djak, penzioner, regularan

        public byte[] Document { get; set; }
    }

    public class UserChangeInfoBindingModel
    {
        //[Required, EmailAddress]

        public string Email { get; set; }


        //[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public string Name { get; set; }

        //[Required]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string UserName { get; set; }

        //[Required]
        public string BirthdayDate { get; set; }

        //[Required]
        public string Address { get; set; }

        //[Required]
        public int PassengerType { get; set; }            // djak, penzioner, regularan

        public byte[] Document { get; set; }
    }


    public class LoginBindingModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "password")]
        public string password { get; set; }

    }

    public class LineBindingModell
    {
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "RouteNumber")]
        public int RouteNumber { get; set; }

        [Required]
        [Display(Name = "RouteType")]
        public TipLinije RouteType { get; set; }

    }
    public class CenovnikBindingModel
    {
        [Required]
        [Display(Name = "OD")]
        public string OD { get; set; }

        [Required]
        [Display(Name = "DO")]
        public string DO { get; set; }

        [Required]
        [Display(Name = "cenaVremenska")]
        public double cenaVremenska { get; set; }

        [Required]
        [Display(Name = "cenaDnevna")]
        public double cenaDnevna { get; set; }

        [Required]
        [Display(Name = "cenaMesecna")]
        public double cenaMesecna { get; set; }

        [Required]
        [Display(Name = "cenaGodisnja")]
        public double cenaGodisnja { get; set; }
    }
    public class StationBindingModel
    {
        public string Name { get; set; }         // za stanicu
        public string Address { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }

    }
    public class LineStBindingModel
    {
        public string LineId { get; set; }              // za liniju
        public string LineType { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public List<StationBindingModel> Stations { get; set; }
    }
    public class UserTicketBindingModel
    {
        public string TicketId { get; set; }
        public string IssuingTime { get; set; }
        public string ExpirationTime { get; set; }
        public string TicketType { get; set; }
        public double Cena { get; set; }

        public UserTicketBindingModel(string t, string i, string x, string ti, double c)
        {
            TicketId = t;
            IssuingTime = i;
            ExpirationTime = x;
            TicketType = ti;
            Cena = c;
        }

        //public double Price { get; set; }
    }
    public class redVoznje
    {
        public string red { get; set; }
    }





}
