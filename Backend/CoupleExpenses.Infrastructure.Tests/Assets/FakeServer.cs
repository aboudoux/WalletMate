using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoupleExpenses.Domain.Common;
using CoupleExpenses.Domain.Common.Events;
using CoupleExpenses.WebApp;
using CoupleExpenses.WebApp.Controllers;
using CoupleExpenses.WebApp.Dto;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CoupleExpenses.Infrastructure.Tests.Assets
{
    public class FakeServer : IDisposable
    {
        private readonly TestServerBase<Startup> _testServer;

        public FakeServer()
        {
            _testServer = new TestServerBase<Startup>("Test", "CoupleExpenses.WebApp", s => s.AddSingleton(CreateEventStoreInstance));

            IEventStore CreateEventStoreInstance(IServiceProvider provider)
            {
                var serializer = provider.GetService<ISerializer>();
                var tempDatabase = Path.Combine(FileEventStoreWithCache.EventStoreDirectory, Guid.NewGuid() + ".es");
                return new FileEventStoreWithCache(serializer, tempDatabase);
            }
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var postRequest = await Post(new User(username, password), "/api/Authentication/authenticate");
            if (postRequest.GetStatusCode() != HttpStatusCode.OK)
                return false;

            var authKey = await postRequest.ReadContentAs<AuthResult>();
            _testServer.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Guid", $"{authKey.Username}:{authKey.AuthKey}".ToBase64());
            return true;
        }

        public async Task<HttpStatusCode> CreatePeriod(int month, int year)
        {
            var post = await Post(new Period(month, year), "/api/Period/Create");
            await post.ThrowIfError();
            return post.GetStatusCode();
        }

        public async Task<IEnumerable<PeriodOperation>> GetAllOperations()
        {
            var result = await Get("/api/Period/Operations");
            return await result.ReadContentAs<IEnumerable<PeriodOperation>>();
        }

        public async Task<IReadOnlyList<string>> GetAllPeriod()
        {
            var result = await Get("/api/Period/All");
            return await result.ReadContentAs<IReadOnlyList<string>>();
        }

        private async Task<HttpResult> Post<T>(T data, string url)
        {
            var dataToPost = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _testServer.Client.PostAsync(url, dataToPost);
            return await BuildResult(response);
        }

        private async Task<HttpResult> Get(string url)
        {
            var response = await _testServer.Client.GetAsync(url);
            return await BuildResult(response);
        }

        private static async Task<HttpResult> BuildResult(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            var result = new HttpResult(responseMessage);
            await result.ThrowIfError();
            return result;

        }

        private class HttpResult
        {
            private readonly HttpResponseMessage _response;

            public HttpResult(HttpResponseMessage response)
            {
                _response = response ?? throw new ArgumentNullException(nameof(response));                
            }

            public HttpStatusCode GetStatusCode()
                => _response.StatusCode;

            public async Task<T> ReadContentAs<T>()
            {                
                var jsonData = await _response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            public async Task ThrowIfError()
            {
                if (_response.StatusCode != HttpStatusCode.OK)
                    throw new HttpServerError(_response.StatusCode, await _response.Content.ReadAsStringAsync());
            }
        }

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }

    public class HttpServerError : Exception
    {
        public HttpStatusCode StatusCode { get; }        

        public HttpServerError(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}