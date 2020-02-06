using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConduitAutomation.Contracts;
using Newtonsoft.Json;

namespace ConduitAutomation
{
    public class ConduitApiClient : HttpClient
    {
        public async Task<Article> GetArticle(string slug)
        {
            var endpoint = $"http://localhost:5000/articles/{slug}";
            var response = await GetAsync(endpoint).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<ArticleResponse>(content).Article;
        }

        public async Task<User> GetUser(string username)
        {
            var endpoint = $"http://localhost:5000/user/username?username={username}";
            var response = await GetAsync(endpoint).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<UserResponse>(content).User;
        }
    }
}