using System;
using System.Collections.Generic;
using CityRates.Domain;
using CityRates.Domain.Bank.Belagroprombank.Repositories;
using CityRates.Domain.Bank.Belarusbank.Repositories;

namespace CityRates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new BelagroprombankRepository();
            var belRepo = new BelarusbankRepository();
            var deparments = new List<GlobalDepartmentDomain>();

            var belagroDeps = repo.GetDepartmentsWithRates();
            var belarusDeps = belRepo.GetDepartmentsWithRates();

            deparments.AddRange(belagroDeps);
            deparments.AddRange(belarusDeps);

            Console.WriteLine("Total departments: " + deparments.Count);
        }
    }
}
