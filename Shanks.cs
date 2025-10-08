using System;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        // even de int naar biginteger verzetten
        // vervolgens de ceiling en sqrt een eigen functie maken
        BigInteger p = BigInteger.Parse(Console.ReadLine()); // modulus
        BigInteger g = BigInteger.Parse(Console.ReadLine()); // grondtal
        int q = int.Parse(Console.ReadLine());              // orde
        BigInteger k = BigInteger.Parse(Console.ReadLine()); // k-getallen x_1 t/m x_k

        for (int i = 0; i < k; i++)
        {
            Console.WriteLine("\n");
            BigInteger x = BigInteger.Parse(Console.ReadLine());
            Dictionary<BigInteger, BigInteger> giant = giantStep(p, g, q);
            if (babyStep(p, x, g, q, giant, out BigInteger log))
                {
                    Console.WriteLine(log);
                }
            else
                {
                    Console.WriteLine("geen macht");
                }
        }
    }
    static Dictionary<BigInteger, BigInteger> giantStep(BigInteger modulus, BigInteger grondgetal, int orde)
    {
        Dictionary<BigInteger, BigInteger> table = new Dictionary<BigInteger, BigInteger>();
        int steps = (int)Math.Ceiling(Math.Sqrt(orde)); // deze nog aan te passen 
        for (int j = 0; j < steps; j++)
        {
            BigInteger exp = steps * j;
            BigInteger res = BigInteger.ModPow(grondgetal, exp, modulus);
            table.Add(res, j);
        }
        return table;
    }

    static bool babyStep(BigInteger modulus, BigInteger x, BigInteger grondgetal, int orde, Dictionary<BigInteger, BigInteger> giantdic, out BigInteger answer)
    {
        Dictionary<BigInteger, BigInteger> table = new Dictionary<BigInteger, BigInteger>();
        int steps = (int)Math.Ceiling(Math.Sqrt(orde));
        answer = -1;
        for (int j = 0; j < steps; j++)
        {
            BigInteger val = BigInteger.ModPow(grondgetal, j, modulus) * x % modulus;
            table.Add(j, val);

            if (giantdic.TryGetValue(val, out BigInteger l))
            {
                answer = l * steps - j;
                return true;
            }
        }
        return false;
    }
}
