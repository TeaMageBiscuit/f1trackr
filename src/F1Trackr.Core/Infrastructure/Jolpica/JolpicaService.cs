using System.Net.Http.Json;
using System.Text.Json;
using F1Trackr.Core.Infrastructure.Jolpica.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace F1Trackr.Core.Infrastructure.Jolpica;

public class JolpicaService
{
    private static readonly DistributedCacheEntryOptions CacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
    };

    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;

    public JolpicaService(HttpClient httpClient, IDistributedCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<ConstructorsResponse?> GetConstructorsAsync(
        int season,
        int? offset,
        int? limit,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"constructors-{season}-{offset}-{limit}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<ConstructorsResponse>(cachedResponse);
        }

        var response = await GetResponse<ConstructorsResponse>(
            $"/{season}/constructors",
            offset,
            limit,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<ConstructorStandingsResponse?> GetConstructorStandingsAsync(
        int season,
        string round,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"constructor-standings-{season}-{round}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<ConstructorStandingsResponse>(cachedResponse);
        }

        var response = await GetResponse<ConstructorStandingsResponse>(
            $"/{season}/{round}/constructorstandings",
            null,
            50,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<DriversResponse?> GetDriversAsync(
        int season,
        int? offset,
        int? limit,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"drivers-{season}-{offset}-{limit}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<DriversResponse>(cachedResponse);
        }

        var response = await GetResponse<DriversResponse>(
            $"/{season}/drivers",
            offset,
            limit,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<DriverStandingsResponse?> GetDriverStandingsAsync(
        int season,
        string round,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"driver-standings-{season}-{round}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<DriverStandingsResponse>(cachedResponse);
        }

        var response = await GetResponse<DriverStandingsResponse>(
            $"/{season}/{round}/driverstandings",
            null,
            50,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<RacesResponse?> GetRacesAsync(
        int season,
        int? offset,
        int? limit,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"races-{season}-{offset}-{limit}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<RacesResponse>(cachedResponse);
        }

        var response = await GetResponse<RacesResponse>(
            $"/{season}/races",
            offset,
            limit,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<ResultsResponse?> GetRaceResultAsync(
        int season,
        int round,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"race-result-{season}-{round}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<ResultsResponse>(cachedResponse);
        }

        var response = await GetResponse<ResultsResponse>(
            $"/{season}/{round}/results",
            null,
            null,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<QualifyingResponse?> GetQualifyingResultAsync(
        int season,
        int round,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"race-qualifying-{season}-{round}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<QualifyingResponse>(cachedResponse);
        }

        var response = await GetResponse<QualifyingResponse>(
            $"/{season}/{round}/qualifying",
            null,
            null,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    public async Task<SprintsResponse?> GetSprintResultAsync(
        int season,
        int round,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"sprint-result-{season}-{round}";
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (cachedResponse is not null)
        {
            return JsonSerializer.Deserialize<SprintsResponse>(cachedResponse);
        }

        var response = await GetResponse<SprintsResponse>(
            $"/{season}/{round}/sprint",
            null,
            null,
            cancellationToken);

        if (response is not null)
        {
            await _cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(response),
                CacheEntryOptions,
                cancellationToken);
        }

        return response;
    }

    private Task<TResponse?> GetResponse<TResponse>(
        string requestPath,
        int? offset,
        int? limit,
        CancellationToken cancellationToken)
    {
        var query = new QueryBuilder();

        if (offset.HasValue)
        {
            query.Add("offset", offset.Value.ToString());
        }

        if (limit.HasValue)
        {
            query.Add("limit", limit.Value.ToString());
        }

        return _httpClient.GetFromJsonAsync<TResponse>(new Uri($"/ergast/f1{requestPath}{query.ToQueryString()}", UriKind.Relative), cancellationToken);
    }
}
