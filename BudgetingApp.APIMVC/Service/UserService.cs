using BudgetingApp.APIMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BudgetingApp.APIMVC.Service
{
	public class UserService : IUserService
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;
		private readonly ILogger<UserService> _logger;

		public UserService(HttpClient client, IConfiguration configuration, ILogger<UserService> logger)
		{
			_client = client;
			_configuration = configuration;
			_logger = logger;
		}

		private string GetBaseUrl()
		{
			return _configuration["BaseUrl"] + "/api/Users";
		}

		public async Task<UserWithToken> Login(UserLogin loginDTO)
		{
			var json = JsonSerializer.Serialize(loginDTO);
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var httpResponse = await _client.PostAsync(GetBaseUrl() + "/login", data);

			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception($"Username / Password not match");
			}

			var content = await httpResponse.Content.ReadAsStringAsync();
			var userWithToken = JsonSerializer.Deserialize<UserWithToken>(content);
			if (userWithToken == null)
			{
				throw new ArgumentException("failed retrieve token");
			}
			return userWithToken;
		}

		public async Task<IEnumerable<UserDTO>> GetRoles()
		{
			var httpResponse = await _client.GetAsync(GetBaseUrl() + "/roles");
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Failed to retrieve users with roles");
			}
			var content = await httpResponse.Content.ReadAsStringAsync();
			var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(content);
			return users;
		}

		public async Task<IEnumerable<UserDTO>> GetUserWithRoles()
		{
			var httpResponse = await _client.GetAsync(GetBaseUrl());
			if (!httpResponse.IsSuccessStatusCode)
			{
				throw new Exception("Failed to retrieve users with roles");
			}

			var content = await httpResponse.Content.ReadAsStringAsync();
			var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(content);
			return users;
		}

		public async Task UpdateRole(int userId, string role, string token)
		{
			try
			{
				_client.DefaultRequestHeaders.Authorization =
			   new AuthenticationHeaderValue("Bearer", token);
				var content = new StringContent("", Encoding.UTF8, "application/json");
				var response = await _client.PutAsync($"{GetBaseUrl()}/{userId}?role={role}", content);
				response.EnsureSuccessStatusCode(); // Throws exception if response status code is not success
			}
			catch (HttpRequestException ex)
			{
				throw new Exception($"Failed to update user role: {ex.Message}");
			}
		}



	}
}
