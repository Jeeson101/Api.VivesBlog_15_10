using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vives.Presentation.Authentication;
using VivesBlog.Dtos.Requests;
using VivesBlog.Sdk;
using VivesBlog.Sdk.Extensions;
using VivesBlog.Ui.ConsoleApp.Settings;
using VivesBlog.Ui.ConsoleApp.Stores;

var configurationBuilder = new ConfigurationBuilder();
var services = new ServiceCollection();

configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = configurationBuilder.Build();

var apiSettings = new ApiSettings();
configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
services.AddApi(apiSettings.BaseUrl);
services.AddScoped<IBearerTokenStore, BearerTokenStore>();

var serviceProvider = services.BuildServiceProvider();

Console.WriteLine("Do you wish to \"login\" or \"register\"? Type the command:");
Console.Write("Command> ");
var command = Console.ReadLine();

var identitySdk = serviceProvider.GetRequiredService<IdentitySdk>();
var tokenStore = serviceProvider.GetRequiredService<IBearerTokenStore>();

var jwtToken = string.Empty;


switch (command)
{

	case "login":
		Console.WriteLine("Username: ");
		var username = Console.ReadLine();
		Console.WriteLine("Password: ");
		var password = Console.ReadLine();

		var request = new SignInRequest { Username = username, Password = password };
		var result = await identitySdk.SignIn(request);
		jwtToken = result.Token;
		tokenStore.SetToken(jwtToken);
		break;

	case "register":
		Console.WriteLine("Username: ");
		var registerUsername = Console.ReadLine();
		Console.WriteLine("Password: ");
		var registerPassword = Console.ReadLine();

		var registerRequest = new RegisterRequest() { Username = registerUsername, Password = registerPassword };
		var registerResult = await identitySdk.Register(registerRequest);
		jwtToken = registerResult.Token;
		tokenStore.SetToken(jwtToken);
		break;
	default:
		Console.WriteLine("Wrong command");
		break;
}

//Show People list
var personSdk = serviceProvider.GetRequiredService<PersonSdk>();
var people = await personSdk.Find();
foreach (var person in people)
{
	Console.WriteLine($"{person.FirstName} {person.LastName} ({person.NumberOfArticles})");
}





Console.WriteLine("Press any key to exit.");
Console.ReadLine();