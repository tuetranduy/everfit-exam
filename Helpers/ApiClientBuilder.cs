using RestSharp;
using RestSharp.Authenticators;

namespace EverfitExam.Helpers;

public class ApiClientBuilder
{
    private RestClient _client;
    private RestRequest _request;
    private string _host;

    public ApiClientBuilder()
    {
        _client = new RestClient();
    }

    public ApiClientBuilder WithHost(string host)
    {
        _host = host;
        _client = new RestClient(host);

        return this;
    }

    public ApiClientBuilder WithBasicAuth(string username, string password)
    {
        var options = new RestClientOptions(_host);
        options.Authenticator = new HttpBasicAuthenticator(username, password);

        _client = new RestClient(options);

        return this;
    }
    
    public ApiClientBuilder WithBearerAuth(string accessToken)
    {
        var options = new RestClientOptions(_host);
        options.Authenticator = new JwtAuthenticator(accessToken);

        _client = new RestClient(options);

        return this;
    }

    public ApiClientBuilder WithDefaultHeader(string key, string value)
    {
        _client.AddDefaultHeader(key, value);

        return this;
    }

    public ApiClientBuilder WithRequestHeader(string key, string value)
    {
        _request.AddOrUpdateHeader(key, value);

        return this;
    }

    public ApiClientBuilder WithRequestBody(object body)
    {
        _request?.AddBody(body);

        return this;
    }

    public ApiClientBuilder WithRequestParameter(string key, string value)
    {
        _request?.AddParameter(key, value);

        return this;
    }

    public ApiClientBuilder Post(string endpoint)
    {
        _request = new RestRequest(endpoint, Method.Post);
        _request.AddHeader("Content-Type", "application/json");

        return this;
    }

    public ApiClientBuilder Get(string endpoint)
    {
        _request = new RestRequest(endpoint, Method.Get);

        return this;
    }
    
    public ApiClientBuilder Delete(string endpoint)
    {
        _request = new RestRequest(endpoint, Method.Delete);

        return this;
    }

    public ApiClientBuilder Build()
    {
        return this;
    }

    public RestResponse Execute()
    {
        return _client.Execute(_request);
    }

    public RestResponse<T> Execute<T>()
    {
        return _client.Execute<T>(_request);
    }
}