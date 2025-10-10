using System.Numerics;

class Program
{
    static void Main()
    {
        BigInteger p = BigInteger.Parse(Console.ReadLine()); // modulus
        BigInteger g = BigInteger.Parse(Console.ReadLine()); // grondtal
        BigInteger q = BigInteger.Parse(Console.ReadLine()); // orde
        BigInteger k = BigInteger.Parse(Console.ReadLine()); // k-getallen x_1 t/m x_k

        List<BigInteger> getallen = new List<BigInteger>();

        for (int i = 0; i < k; i++)
        {
            BigInteger x = BigInteger.Parse(Console.ReadLine());
            getallen.Add(x);
        }

        Console.WriteLine("\nAntwoorden:");

        foreach (BigInteger getal in getallen)
        {
            Dictionary<BigInteger, BigInteger> giant = giantStep(p, g, q);
            if (babyStep(p, getal, g, q, giant, out BigInteger antwoord))
            {
                Console.WriteLine(antwoord);
            }
            else
            {
                Console.WriteLine("geen macht");
            }
        }
    }
    static Dictionary<BigInteger, BigInteger> giantStep(BigInteger modulus, BigInteger grondgetal, BigInteger orde)
    {
        Dictionary<BigInteger, BigInteger> table = new Dictionary<BigInteger, BigInteger>();

        BigInteger steps = perfecteWortel(orde) ? Sqrt(orde) : Sqrt(orde) + 1;

        for (BigInteger j = 0; j < steps; j++)
        {
            BigInteger exp = steps * j;
            BigInteger res = BigInteger.ModPow(grondgetal, exp, modulus);
            table.Add(res, j);
        }
        return table;
    }

    static bool babyStep(BigInteger modulus, BigInteger x, BigInteger grondgetal, BigInteger orde, Dictionary<BigInteger, BigInteger> giantdic, out BigInteger antwoord)
    {
        Dictionary<BigInteger, BigInteger> table = new Dictionary<BigInteger, BigInteger>();

        BigInteger steps = perfecteWortel(orde) ? Sqrt(orde) : Sqrt(orde) + 1;

        antwoord = BigInteger.MinusOne;
        for (BigInteger j = 0; j < steps; j++)
        {
            BigInteger val = BigInteger.ModPow(grondgetal, j, modulus) * x % modulus;
            table.Add(j, val);

            if (giantdic.TryGetValue(val, out BigInteger l))
            {
                antwoord = l * steps - j;
                return true;
            }
        }
        return false;
    }

    static bool perfecteWortel(BigInteger value)
    {
        BigInteger a = Sqrt(value);
        return a * a == value;
    }

    static BigInteger Sqrt(BigInteger value)
    {
        if (value < 0)
            throw new ArgumentException("min kan niet");
        
        if (value == 0)
            return 0;
        
        if (value < 4)
            return 1;
        
        BigInteger x = value;
        BigInteger y = (value + 1) / 2;
        
        while (y < x)
        {
            x = y;
            y = (x + value / x) / 2;
        }
        return x;
    }
}
