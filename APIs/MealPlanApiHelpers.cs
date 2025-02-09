using System;
using EverfitExam.Drivers;
using EverfitExam.Helpers;
using EverfitExam.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EverfitExam.APIs;

public class MealPlanApiHelpers
{
    private readonly IConfiguration _configuration;

    public MealPlanApiHelpers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApiResponse DeleteMealPlan(string id)
    {
        try
        {
            var response = _getApiClient().Delete($"meal-plans/{id}")
                .Execute();

            return JsonConvert.DeserializeObject<ApiResponse>(response?.Content);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
    
    public CreateMealPlanResponse.Root CreateMealPlan(MealPlanInfo mealPlan)
    {
        try
        {
            var response = _getApiClient().Post($"meal-plans").WithRequestBody(JsonConvert.SerializeObject(mealPlan))
                .Execute();

            return JsonConvert.DeserializeObject<CreateMealPlanResponse.Root>(response?.Content);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    private string _getAccessTokenFromLocalStorage()
    {
        var accessToken = DriverUtils.ExecuteScript<string>("return localStorage.getItem('access-token')");

        return accessToken;
    }

    private ApiClientBuilder _getApiClient()
    {
        return new ApiClientBuilder().WithHost(_configuration["Api.Url"]).WithDefaultHeader("x-access-token", _getAccessTokenFromLocalStorage()).Build();
    }
}