using System.Linq;
using TripPinClient.Core;
using TripPinClient.DTO;

namespace TripPinClient.UI
{
    public class PersonFormatter
    {
        public string GetDescriptionStringForPerson(Person person)
        {
            var emailString = person.Emails.Any() ? person.Emails[0] : string.Empty;
            return $"{person.UserName}{Constants.ConsoleLineDelimeter}{person.FirstName}{Constants.ConsoleLineDelimeter}{person.LastName}{Constants.ConsoleLineDelimeter}{emailString}";
        }

        public string GetDetailsStringForPerson(Person person)
        {
            var contactInfoString = person.Emails.Any() ? person.Emails.First() : "No email address";
            return @$"
Details for {person.UserName}
Name: {person.FullName}
Gender: {person.Gender}
Contact info: {contactInfoString}
";
        }

        public string GetListDescriptionString()
        {
            return $"User name{Constants.ConsoleLineDelimeter}First name{Constants.ConsoleLineDelimeter}Last name{Constants.ConsoleLineDelimeter}Email";
        }
    }
}
