using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreInMemory
{
    internal class TestService : BackgroundService
    {
        private readonly TestController controller;

        public TestService( TestController controller )
        {
            this.controller = controller;
        }

        protected override async Task ExecuteAsync( CancellationToken stoppingToken )
        {
            try
            {
                int internalCounter = 1;

                Console.WriteLine( "TestService started." );

                while ( !stoppingToken.IsCancellationRequested )
                {
                    await this.controller.AddPersonAsync( $"Cookie-{internalCounter}", Random.Shared.Next( 13, 37 ));

                    var list = await this.controller.GetPeopleAsync();

                    Console.WriteLine( "[Pass {0}]", internalCounter );

                    foreach ( var person in list )
                        Console.WriteLine( "Id: {0} - Name: {1} - Age: {2}", person.Id, person.Name, person.Age );

                    internalCounter++;

                    await Task.Delay( 1_000 );
                }
            } 
            catch ( OperationCanceledException )
            {
                // Cleanup

                Console.WriteLine( "TestService cancel." );
            }
        }
    }
}
