namespace Common.Models;

public sealed record GitHubCredentials(string Token, string? UserName = null);
