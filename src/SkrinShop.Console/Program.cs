﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkrinShop.Console;
using SkrinShop.Console.Orders;
using SkrinShop.Persistence;
using System.CommandLine;

var fileOption = new Option<FileInfo>(
    name: "--file",
    description: "Path to xml file")
{
    IsRequired = true
};
var connectionStringOption = new Option<string>(
    name: "--connection",
    description: "Connection string")
{
    IsRequired = true
};
var ensureCreatedOption = new Option<bool>(
    name: "--ensure-created",
    description: "Enable esurance that database exits");

var rootCommand = new RootCommand("Order import util");
rootCommand.AddOption(fileOption);
rootCommand.AddOption(connectionStringOption);

rootCommand.SetHandler(async (file, connectionString, isEnsureEnabled) =>
    {
        var services = new ServiceCollection();
        services.AddPersistence(connectionString);
        services.AddApplication();
        services.AddLogging(builder => builder.AddConsole());

        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        OrderImportSerivce importSerivce = serviceProvider.GetRequiredService<OrderImportSerivce>();
        OrderReader parsingService = serviceProvider.GetRequiredService<OrderReader>();
        ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        if (isEnsureEnabled && serviceProvider.EnusereDbCreated())
        {
            logger.LogInformation("Database was created");
        }
        using FileStream fileStream = file.OpenRead();
        IEnumerable<OrderDto> orders = parsingService.ReadOrders(fileStream);
        await importSerivce.ImportOrderAsync(orders);
        logger.LogInformation("Import completed");
    },
    fileOption, connectionStringOption, ensureCreatedOption);

return await rootCommand.InvokeAsync(args);