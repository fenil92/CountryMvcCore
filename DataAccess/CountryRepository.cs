using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAuth.Data;
using MVCAuth.Models;
public class CountryRepository
{
    private  ApplicationDbContext context;
    public CountryRepository(ApplicationDbContext context)
    {
        this.context=context;
    }

    // public   void AddCountry(Favorites fc)
    // {
    //     using (var context = new ApplicationDbContext(
    //             serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
    //         {
    //         };
    // }
    
}