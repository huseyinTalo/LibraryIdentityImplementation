using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add OpenAPI support
builder.Services.AddOpenApi();

// Configure database
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseInMemoryDatabase("IdentityDemo"));

// Configure Identity
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Map Identity endpoints
app.MapIdentityApi<IdentityUser>();
app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager,
    [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.Unauthorized();
})
.WithOpenApi()
.RequireAuthorization();
// Custom endpoint to list all available Identity APIs
app.MapGet("/api/endpoints", () =>
{
    var endpointsList = new List<object>
    {
        new {
            Path = "/register",
            Method = "POST",
            Description = "Register a new user",
            RequestBody = new {
                email = "string",
                password = "string"
            }
        },
        new {
            Path = "/login",
            Method = "POST",
            Description = "Login with email and password",
            RequestBody = new {
                email = "string",
                password = "string"
            },
            Response = new {
                tokenType = "string",
                accessToken = "string",
                expiresIn = "int (seconds)",
                refreshToken = "string"
            }
        },
        new {
            Path = "/refresh",
            Method = "POST",
            Description = "Refresh the access token",
            RequestBody = new {
                refreshToken = "string"
            }
        },
        new {
            Path = "/confirmEmail",
            Method = "GET",
            Description = "Confirm email address",
            QueryParams = new {
                userId = "string",
                code = "string"
            }
        },
        new {
            Path = "/resendConfirmationEmail",
            Method = "POST",
            Description = "Resend email confirmation",
            RequestBody = new {
                email = "string"
            }
        },
        new {
            Path = "/forgotPassword",
            Method = "POST",
            Description = "Request password reset",
            RequestBody = new {
                email = "string"
            }
        },
        new {
            Path = "/resetPassword",
            Method = "POST",
            Description = "Reset password with token",
            RequestBody = new {
                email = "string",
                resetToken = "string",
                newPassword = "string"
            }
        },
        new {
            Path = "/manage/2fa",
            Method = "POST",
            Description = "Enable/disable two-factor authentication",
            Authentication = "Bearer token required"
        },
        new {
            Path = "/manage/info",
            Method = "GET",
            Description = "Get current user information",
            Authentication = "Bearer token required"
        },
        new {
            Path = "/manage/info",
            Method = "POST",
            Description = "Update user information",
            Authentication = "Bearer token required"
        }
    };

    return TypedResults.Ok(new
    {
        BaseUrl = "/",
        Note = "All endpoints are prefixed with application base URL",
        AuthenticationType = "Bearer Token",
        Endpoints = endpointsList,
        CommonResponses = new
        {
            Success = new { status = 200, message = "Operation successful" },
            Unauthorized = new { status = 401, message = "Authentication required or token invalid" },
            BadRequest = new { status = 400, message = "Invalid request data" },
            NotFound = new { status = 404, message = "Resource not found" }
        }
    });
})
.WithName("GetEndpoints")
.WithOpenApi()
.AllowAnonymous();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();