using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TestREST_210422.Data
{
    public class TestManager
    {
        //BaseAddress => Your Get http address
        static readonly string BaseAddress = "http....";
        static readonly string Url = $"{BaseAddress}";

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            return client;
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            // TODO: use GET to retrieve books
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Test>>(result);
        }
        public async Task<List<Test>> Add(string eventTitle_, string eventDescription_, string avenue_)
        {
            ////// TODO: use POST to add a book
            //(1) eventid 추가
            Test test = new Test()
            {
                EventTitle = eventTitle_,
                EventDescription = eventDescription_,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Avenue = avenue_,
                MaxMembers = 9999, //FOR TEST
            };

            //BaseAddressAdd => Your Post http address
            string BaseAddressAdd = "http....";
            string UrlAdd = $"{BaseAddressAdd}";
            HttpClient client = await GetClient();
            var reponse = await client.PostAsync(UrlAdd,
                new StringContent
                (
                    JsonConvert.SerializeObject(test),
                    Encoding.UTF8, "application/json")
                );
            return JsonConvert.DeserializeObject<List<Test>>
                (
                  await reponse.Content.ReadAsStringAsync()
                );
        }
    }
}
