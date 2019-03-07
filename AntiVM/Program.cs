namespace AntiVM
{
    using System;

    internal static class Program
    {
        private static void Main()
        {
            Console.Title = "Developer by Antlion";
            if (!AntiVM.GetCheckVMBot())
            {        
                Console.WriteLine("Программа запущена вне виртуальных машин!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Внимание - Обнаружена виртуальная машина.");
                Console.ReadKey();
            }
        }
    }
}