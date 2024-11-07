using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace chatapi.Model
{

    [Keyless]
    public class models
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePicUrl { get; set; }
        public string StatusMessage { get; set; }
        public string CreatedAt { get; set; }
    }
    [Keyless]

    public class loggedin
    {
     //   public string Action { set; get; }
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string images { get; set; }
        public string UserID { get; set; }
        public string TargAud { get; set; }

    }
    [Keyless]

    public class Login
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    [Keyless]
    public class gPrice
    {
        public string PlanName { get; set; }
        public string Price { get; set; }
        public string Details { get; set; }
        public string Color { get; set; }
        public string Packno { get; set; }

    }
    [Keyless]

    public class paymentt
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryDate { get; set; }
        public string Packno { get; set; }
    }
}
