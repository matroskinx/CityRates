using System;
using CityRates.Infrastructure.Repositories;

namespace CityRates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var belagroprombunkRepo = new BelagroprombankRepository();
            var belarusbunkRepo = new BelarusbankRepository();
            //belagroprombunkRepo.GetDepartmentsWithRates();
            var aa = belarusbunkRepo.GetDepartmentsWithRates();
            //foreach (var a in aa)
            //{
            //    Console.WriteLine(a.Lat);
            //}
           
        }
    }
}
