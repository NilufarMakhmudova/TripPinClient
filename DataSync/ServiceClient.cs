using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripPinClient.Core;
using TripPinClient.DTO;
using TripPinClient.Logs;

namespace TripPinClient.DataSync
{

    public class ServiceClient
    {
        private ODataClient _client = new ODataClient(new ODataClientSettings(new Uri(Constants.ServiceUrl))
        {
            OnTrace = (x, y) => Logger.Info(string.Format(x, y))
        });

        public async Task<List<Person>> GetAllPeople()
        {
            try
            {
                var result = new List<Person>();
                var annotations = new ODataFeedAnnotations();

                result.AddRange(await _client
                    .For<Person>()
                    .FindEntriesAsync(annotations));

                while (annotations.NextPageLink != null)
                {
                    result.AddRange(await _client
                        .For<Person>()
                        .FindEntriesAsync(annotations.NextPageLink, annotations)
                    );
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error fetching all people: {ex}");
                return new List<Person>();
            }
        }

        public async Task<List<Person>> FilterPeople(string filterCriteria, SearchOption searchOption)
        {
            try
            {
                var result = new List<Person>();
                switch (searchOption)
                {
                    case SearchOption.Email:
                        result.AddRange(await _client
                   .For<Person>()
                   .Filter($"Emails/any(d:tolower(d)  eq '{filterCriteria}')")
                   .FindEntriesAsync()); ;
                        break;

                    case SearchOption.Address:
                        result.AddRange(await _client
                  .For<Person>()
                  .Filter($"AddressInfo/any(a:contains(tolower(a/Address),'{filterCriteria}'))")
                  .Filter($"AddressInfo/any(a:contains(tolower(a/Address),'{filterCriteria}'))")
                  .FindEntriesAsync());
                        break;
                    case SearchOption.Name:

                        result.AddRange(await _client
                   .For<Person>()
                   .Filter(p => p.FirstName.ToLower().Contains(filterCriteria) || p.LastName.ToLower().Contains(filterCriteria))
                   .FindEntriesAsync());
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error fetching all people: {ex}");
                return new List<Person>();
            }
        }

        public async Task<Person> GetPersonByUsernameAsync(string username)
        {
            try
            {
                return await _client
                .For<Person>()
                .Filter(x => x.UserName == username)
                .FindEntryAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error fetching person with username {username}: {ex}");
                return null;
            }
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            try
            {
                return await _client.For<Person>().Set(person).InsertEntryAsync();                
            }
            catch (Exception ex)
            {
                Logger.Error($"Error creating new user: {ex}");
                return null;
            }
        }

        public async Task<Person> GetSingletonInfo()
        {
            try
            {
                return await _client.For<Person>("Me").FindEntryAsync();
            }
            catch (Exception ex)
            {
                Logger.Error($"Error getting singleton: {ex}");
                return null;
            }
        }
    }
}
