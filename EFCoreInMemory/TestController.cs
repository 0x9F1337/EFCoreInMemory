using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreInMemory
{
    internal class TestController
    {
        private readonly IDbContextFactory<TestContext> context;

        public TestController( IDbContextFactory<TestContext> context )
        {
            this.context = context;
        }

        public async Task AddPersonAsync( string name, int age )
        {
            using var ctx = await context.CreateDbContextAsync();

            await ctx.Database.EnsureCreatedAsync();

            await ctx.People.AddAsync( new Person()
            {
                Id = await ctx.People.MaxAsync( p => p.Id ) + 1,
                Name = name,
                Age = age
            } );

            await ctx.SaveChangesAsync();
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            using var ctx = await context.CreateDbContextAsync();
            return await ctx.People.ToListAsync();
        }
    }
}
