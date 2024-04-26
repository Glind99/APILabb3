using APILabb3.Models;
using APILabb3.Data;
using Microsoft.EntityFrameworkCore;
using System;
using APILabb3.NewFolder;
using System.Text.Json.Serialization;
using System.Text.Json;
using APILabb3.DTO;

namespace APILabb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("DefaultConnection")));
            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();

            //Get Current people and their interest.
            app.MapGet("/persons" + " List of members and their interest", async (ApplicationDbContext context) =>
            {
                var persons = await context.Persons
                    .Include(p => p.interests)
                        .ThenInclude(i => i.Links)
                    .ToListAsync();

                if (persons == null || !persons.Any())
                {
                    return Results.NotFound("Hittade inga personer");
                }

                var personsWithInterests = persons.Select(p => new
                {
                    p.PersonId,
                    p.FirstName,
                    p.LastName,
                    p.PhoneNumber,
                    Interests = p.interests != null ? p.interests.Select(i => new
                    {
                        i.InterestId,
                        i.Title,
                        i.Summary,
                        Links = i.Links != null ? i.Links.Select(l => new
                        {
                            l.LinkId,
                            l.Url
                        }).ToList<object>() : new List<object>()
                    }).ToList<object>() : new List<object>()
                }).ToList();

                return Results.Ok(personsWithInterests);
            });

            //Create a new person. Use Dto to make it more eazy.
            app.MapPost("/persons" + " Add person to database", async (CreatePersonDto personDto, ApplicationDbContext context) =>
            {
                var newPerson = new Person
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    PhoneNumber = personDto.PhoneNumber
                };

                context.Persons.Add(newPerson);
                await context.SaveChangesAsync();
                return Results.Created($"/persons/{newPerson.PersonId}", newPerson);
            });


            //Add interest
            app.MapPost("/persons/{personId} Add interests to current member", async (int personId, InterestDto interestDto, ApplicationDbContext context) =>
            {
                var person = await context.Persons.Include(p => p.interests).FirstOrDefaultAsync(p => p.PersonId == personId);

                if (person == null)
                {
                    return Results.NotFound($"Person med ID {personId} hittades inte");
                }

                var interest = new Interest
                {
                    Title = interestDto.Title,
                    Summary = interestDto.Summary,
                    FK_PersonId = personId,
                    Links = new List<Link>() // Create a new list
                };

                if (interestDto.Links != null)
                {
                    foreach (var linkDto in interestDto.Links)
                    {
                        interest.Links.Add(new Link { Url = linkDto.Url });
                    }
                }

                person.interests.Add(interest);

                context.Interests.Add(interest);
                await context.SaveChangesAsync();

                return Results.Created($"/persons/{personId}/interests/{interest.InterestId}", interest);
            });
            //Get specific interest
            app.MapGet("/persons/{personId}/Get specific interest by Id's/{interestId}", async (int personId, int interestId, ApplicationDbContext context) =>
            {
                var interest = await context.Interests
                    .FirstOrDefaultAsync(i => i.FK_PersonId == personId && i.InterestId == interestId);

                if (interest == null)
                {
                    return Results.NotFound($"Intresse med ID {interestId} hittades inte för person med ID {personId}");
                }

                return Results.Ok(interest);
            });

            // Update specific interest
            app.MapPut("/persons/{personId}/interests/{interestId}", async (int personId, int interestId, InterestUpdateDto updatedInterestDto, ApplicationDbContext context) =>
            {
                var existingInterest = await context.Interests
                    .FirstOrDefaultAsync(i => i.FK_PersonId == personId && i.InterestId == interestId);

                if (existingInterest == null)
                {
                    return Results.NotFound($"Intresse med ID {interestId} hittades inte för person med ID {personId}");
                }

                // Update properties
                existingInterest.Title = updatedInterestDto.Title;
                existingInterest.Summary = updatedInterestDto.Summary;

                // Ensure Links list is instantiated
                existingInterest.Links ??= new List<Link>();

                // Clear existing links
                existingInterest.Links.Clear();

                // Add new links
                if (updatedInterestDto.Links != null)
                {
                    foreach (var linkDto in updatedInterestDto.Links)
                    {
                        existingInterest.Links.Add(new Link { Url = linkDto.Url });
                    }
                }

                // Save changes
                await context.SaveChangesAsync();

                return Results.Ok(existingInterest);
            });


            app.Run();
        }
    }
}
