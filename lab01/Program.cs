using System.Text.Json;

namespace PA_lab01
{
    public class RootObject
    {
        public Person person { get; set; }
    }

    public class Person
    {
        public string Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Roles { get; set; }

        public Person() { }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            DownloadWithThreads(200);
        }

        public static List<Person> DownloadWithThreads(int numberOfNames)
        {
            List<Person> result = new List<Person>();

            for (int i = 0; i < numberOfNames; i++)
            {
                result.Add(DownloadName(i).Result);
            }

            return result;
        }

        public static async Task<Person> DownloadName(int nameId)
        {
            Person? result = new Person();

            string uri = $"https://name-service-phi.vercel.app/api/v1/names/{nameId}.json";

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string tmp = await response.Content.ReadAsStringAsync();
                RootObject? res = await JsonSerializer.DeserializeAsync<RootObject>(tmp);
                result = res.person;
            }

            return null;
        }
    }
}
