﻿using System;
using System.Collections.Generic;

namespace ElectionGuard.Encryption.Bench
{
    public class Program
    {
        private static readonly List<Fixture> Fixtures =
        new List<Fixture>{
            new BenchEncrypt(),
            new BenchManifest()
        };

        static void Main()
        {
            Console.WriteLine("------ benchmarking ------");

            foreach(var fixture in Fixtures)
            {
                fixture.Run();
            }
        }
    }
}
