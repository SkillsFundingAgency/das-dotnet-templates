﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using SFA.DAS.WebTemplateSourceName.Messages.Events;

namespace SFA.DAS.WebTemplateSourceName.Functions.TestHarness
{
    public class TestHarness
    {
        private readonly IMessageSession _publisher;

        public TestHarness(IMessageSession publisher)
        {
            _publisher = publisher;
        }

        public async Task Run()
        {
            long accountId = 1001;
            ConsoleKey key = ConsoleKey.Escape;

            while (key != ConsoleKey.X)
            {
                Console.Clear();
                Console.WriteLine("Test Options");
                Console.WriteLine("------------");
                Console.WriteLine("A - CreateAccountEvent");               
                Console.WriteLine("X - Exit");
                Console.WriteLine("Press [Key] for Test Option");
                key = Console.ReadKey().Key;

                try
                {
                    switch (key)
                    {
                        case ConsoleKey.A:
                            await _publisher.Publish(new CreatedAccountEvent { AccountId = accountId, Created = DateTime.Now, HashedId = "HPRIV", PublicHashedId = "PUBH", Name = "My Test", UserName = "Tester", UserRef = Guid.NewGuid() });
                            Console.WriteLine();
                            Console.WriteLine($"Published CreatedAccountEvent");
                            break;                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }

                if (key == ConsoleKey.X) break;

                Console.WriteLine();
                Console.WriteLine("Press any key to return to menu");
                Console.ReadKey();
            }
        }
    }
}
