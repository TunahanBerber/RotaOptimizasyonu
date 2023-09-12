using Google.OrTools.LinearSolver;
using System;

namespace RotaOptimizasyonu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rota Maliyet Optimizasyonu Sonuçları:");
            Console.WriteLine("--------------------------------------");
            RotaMaliyetOptimizasyonu();

            Console.WriteLine("\nRota Optimizasyonu Sonuçları:");
            Console.WriteLine("-------------------------------");
            RotaOptimizasyonu();
        }

        static void RotaOptimizasyonu()
        {
            Solver solver = Solver.CreateSolver("GLOP");

            // Decision variables for route alternatives
            Variable x1 = solver.MakeNumVar(0.0, 1.0, "x1");
            Variable x2 = solver.MakeNumVar(0.0, 1.0, "x2");
            Variable x3 = solver.MakeNumVar(0.0, 1.0, "x3");

            // Costs for each route 
            double cost1 = 40 * 1.5;
            double cost2 = 60 * 1.0;
            double cost3 = 50 * 1.2;

            // Define the objective function
            Objective objective = solver.Objective();
            objective.SetCoefficient(x1, cost1);
            objective.SetCoefficient(x2, cost2);
            objective.SetCoefficient(x3, cost3);
            objective.SetMinimization();

            // Constraint for selecting only one route
            Constraint c1 = solver.MakeConstraint(1, 1);
            c1.SetCoefficient(x1, 1);
            c1.SetCoefficient(x2, 1);
            c1.SetCoefficient(x3, 1);

            solver.Solve();

            Console.WriteLine("x1: " + x1.SolutionValue());
            Console.WriteLine("x2: " + x2.SolutionValue());
            Console.WriteLine("x3: " + x3.SolutionValue());

            Variable[] decisionVariables = new Variable[] { x1, x2, x3 };
            string[] routeNames = new string[] { "Güzergah 1", "Güzergah 2", "Güzergah 3" };

            for (int i = 0; i < decisionVariables.Length; i++)
            {
                if (decisionVariables[i].SolutionValue() == 1)
                {
                    Console.WriteLine($"En uygun rota: {routeNames[i]}'dır.");
                    break;
                }
            }
        }

        static void RotaMaliyetOptimizasyonu()
        {
            Solver solver = Solver.CreateSolver("GLOP");

            double yakitFiyati = 36.83; // TL/lt

            // Decision variables
            Variable rota1 = solver.MakeNumVar(0.0, 1.0, "Ankara-İstanbul");
            Variable rota2 = solver.MakeNumVar(0.0, 1.0, "Ankara-İzmir");
            Variable rota3 = solver.MakeNumVar(0.0, 1.0, "Ankara-Antalya");
            
            // Cost calculations
            double maliyet1 = 450 * 6 * yakitFiyati / 100;
            double maliyet2 = 580 * 7 * yakitFiyati / 100;
            double maliyet3 = 485 * 5.5 * yakitFiyati / 100;

            // Define the objective function
            Objective objective = solver.Objective();
            objective.SetCoefficient(rota1, maliyet1);
            objective.SetCoefficient(rota2, maliyet2);
            objective.SetCoefficient(rota3, maliyet3);
            objective.SetMinimization();

            // Constraint for selecting only one route
            Constraint constraint = solver.MakeConstraint(1, 1);
            constraint.SetCoefficient(rota1, 1);
            constraint.SetCoefficient(rota2, 1);
            constraint.SetCoefficient(rota3, 1);

            solver.Solve();

            Variable[] decisionVariables = new Variable[] { rota1, rota2, rota3 };
            string[] routeNames = new string[] { "Ankara-İstanbul", "Ankara-İzmir", "Ankara-Antalya" };

            for (int i = 0; i < decisionVariables.Length; i++)
            {
                if (decisionVariables[i].SolutionValue() == 1)
                {
                    Console.WriteLine($"En düşük maliyetli rota: {routeNames[i]}'dır. Maliyet: {(new double[] { maliyet1, maliyet2, maliyet3 })[i]} TL");
                    break;
                }
            }
        }
    }
}
