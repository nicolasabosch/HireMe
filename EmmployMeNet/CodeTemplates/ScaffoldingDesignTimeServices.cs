using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

public class ScaffoldingDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
        var options = ReverseEngineerOptions.DbContextAndEntities;
        
        services.AddHandlebarsScaffolding(options);

        services.AddHandlebarsScaffolding(options =>
        {
            // Exclude some tables
            options.ExcludedTables = new List<string> { "dbo.Audit", "dbo.AuditDetail","dbo.DataTranslation" };
            
            
        });
        var notDataTranslation = (helperName: "not-DataTranslation", helperFunction: (Action<TextWriter, HelperOptions, Dictionary<string, object>, object[]>)NotDataTranslation);
        services.AddHandlebarsBlockHelpers(notDataTranslation);

        var implements = (helperName: "implements", helperFunction: (Action<TextWriter, Dictionary<string, object>, object[]>)Implements);
        var concurrencyCheck = (helperName: "concurrencyCheck", helperFunction: (Action<TextWriter, Dictionary<string, object>, object[]>)ConcurrencyCheck);

        services.AddHandlebarsHelpers(implements, concurrencyCheck);

    }

    void NotDataTranslation(TextWriter writer, HelperOptions options, Dictionary<string, object> context, object[] args)
    {

        if (context.ContainsKey("class") && context["class"].ToString() != "DataTranslation")
        {
            options.Template(writer, context);
        }

    }

    void Implements(TextWriter writer, Dictionary<string, object> context, object[] parameters)
    {
        string[] excludeTables = { "Audit", "AuditDetail", "sysdiagrams", "DataTranslation" };

        if (context.ContainsKey("class"))
        {
            var tableName = context["class"].ToString();
            if (!tableName.StartsWith("v") && !tableName.StartsWith("z") && !tableName.StartsWith("Z") && !excludeTables.Contains(tableName))
            {
                writer.Write(" : IEntityRecord");
            }

        }
    }


    void ConcurrencyCheck(TextWriter writer, Dictionary<string, object> context, object[] parameters)
    {
        if (context.ContainsKey("property-name") && context["property-name"].ToString() == "LastModifiedOn")
        {
            writer.WriteLine("[ConcurrencyCheck]");
        }
    }
}
