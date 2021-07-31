using System;
using System.Linq;
using System.Threading.Tasks;
using TripPinClient.Core;
using TripPinClient.DataSync;
using TripPinClient.DTO;

namespace TripPinClient.UI
{
    public class ConsoleHelper
    {
        private static readonly ServiceClient _client = new();
        private static readonly PersonFormatter _personFormatter = new();


        private async Task PrintAllPeopleOnScreen()
        {
            var people = await _client.GetAllPeople();

            Console.WriteLine(_personFormatter.GetListDescriptionString());
            Console.WriteLine();
            foreach (var person in people)
            {
                Console.WriteLine(_personFormatter.GetDescriptionStringForPerson(person));
            }
        }

        private async Task SearchTravellers()
        {
            Console.WriteLine($"We can search by different parameters. Choose the appropriate:");
            Console.WriteLine("1) Name");
            Console.WriteLine("2) Email");
            Console.WriteLine("3) Home address");
            Console.WriteLine("4) Back to main menu");

            SearchOption option;

            switch (Console.ReadLine())
            {
                case "1":
                    option = SearchOption.Name;
                    break;
                case "2":
                    option = SearchOption.Email;
                    break;
                case "3":
                    option = SearchOption.Address;
                    break;

                default:
                    return;
            }

            Console.WriteLine("What is your criteria?:");
            var criteria = Console.ReadLine().ToLower();
            var result = await _client.FilterPeople(criteria, option);

            if (!result.Any())
            {
                Console.WriteLine("Nobody matches the search!");
            }
            else
            {
                Console.WriteLine("Here are the results:");
                Console.WriteLine(_personFormatter.GetListDescriptionString());
                Console.WriteLine();
                foreach (var person in result)
                {
                    Console.WriteLine(_personFormatter.GetDescriptionStringForPerson(person));
                }
                Console.WriteLine();
            }

            Console.WriteLine("Do you want to give another try? Insert Y for Yes");
            var shouldContinue = Console.ReadLine().ToLower() == "y";

            if (shouldContinue)
                await SearchTravellers();
        }

        private async Task PrintSelectedPerson()
        {
            Console.WriteLine($"Whose details do you want to see? Insert username:");
            var userName = Console.ReadLine();
            var person = await _client.GetPersonByUsernameAsync(userName);

            if (person == null)
            {
                Console.WriteLine($"Could not find such person!");
                return;
            }

            Console.WriteLine(_personFormatter.GetDetailsStringForPerson(person));

            Console.WriteLine($"Anyone else? Press Y to continue");
            var shouldContinue = Console.ReadLine().ToLower() == "y";

            if (shouldContinue)
                await PrintSelectedPerson();

            return;
        }

        private async Task RegisterNewUser()
        {
            Console.WriteLine($"We need your details to register you. Please answer tbe questions.");
            Console.WriteLine($"First name:");
            var firstName = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Can not proceed without your first name");
                return;
            }

            Console.WriteLine($"Last name:");
            var lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Can not proceed without your last name");
                return;
            }

            Console.WriteLine($"Choose your username:");
            var userName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Can not proceed without your user name");
                return;
            }

            Console.WriteLine($"Email address:");
            var email = Console.ReadLine();


            var newPerson = new Person()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Emails = new System.Collections.Generic.List<string> { email }
            };

            var person = await _client.CreatePersonAsync(newPerson);

            if (person == null)
            {
                Console.WriteLine($"Could not register!");
                return;
            }

            Console.WriteLine($"You are in:");
            Console.WriteLine(_personFormatter.GetDetailsStringForPerson(person));

            Console.WriteLine($"Anyone else? Press Y to continue");
            var shouldContinue = Console.ReadLine().ToLower() == "y";

            if (shouldContinue)
                await RegisterNewUser();
        }

        private async Task PrintServiceUserInfo()
        {
            Console.WriteLine($"Let me introduce myself:");
            var person = await _client.GetSingletonInfo();

            if (person == null)
            {
                Console.WriteLine($"Oops, can't do it now!");
                return;
            }

            Console.WriteLine(_personFormatter.GetDetailsStringForPerson(person));
        }

        public async Task<bool> PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) See full list");
            Console.WriteLine("2) Search for travelers");
            Console.WriteLine("3) View person details");
            Console.WriteLine("4) Register as traveller");
            Console.WriteLine("5) Who am I talking to?");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    await PrintAllPeopleOnScreen();
                    return true;
                case "2":
                    await SearchTravellers();
                    return true;
                case "3":
                    await PrintSelectedPerson();
                    return true;
                case "4":
                    await RegisterNewUser();
                    return true;
                case "5":
                    await PrintServiceUserInfo();
                    return true;
                case "6":
                    return false;
                default:
                    return false;
            }
        }
    }
}
