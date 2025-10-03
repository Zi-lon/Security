using System;
using System.Numerics;
class Program
{
    static void Main(string[] args)
    {
        BigInteger m = BigInteger.Parse(Console.ReadLine());
        BigInteger p = BigInteger.Parse(Console.ReadLine());
        BigInteger q = BigInteger.Parse(Console.ReadLine());
        BigInteger a = BigInteger.Parse(Console.ReadLine());

        (BigInteger p1, BigInteger p2) = blumInteger(a, p);
        (BigInteger q1, BigInteger q2) = blumInteger(a, q);

        List<BigInteger> wortels = new List<BigInteger>()
        {
            ChineseRemainderTheorem(p1, p, q1, q, m),
            ChineseRemainderTheorem(p1, p, q2, q, m),
            ChineseRemainderTheorem(p2, p, q1, q, m),
            ChineseRemainderTheorem(p2, p, q2, q, m)
        };

        wortels.Sort();

        foreach (BigInteger wortel in wortels)
        {
            Console.WriteLine(wortel);
        }
        ;
    }

    // pseudocode van : https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
    internal static (BigInteger x, BigInteger y) extendedGcd(BigInteger a, BigInteger b)
    {
        (BigInteger x, BigInteger oudeX) = (0, 1);
        (BigInteger y, BigInteger oudeY) = (1, 0);
        while (b != 0)
        {
            BigInteger quotient = a / b;
            (a, b) = (b, a % b);
            (x, oudeX) = (oudeX - quotient * x, x);
            (y, oudeY) = (oudeY - quotient * y, y);
        }
        return (oudeX, oudeY);
    }
    internal static (BigInteger, BigInteger) blumInteger(BigInteger b, BigInteger p)
    {
        BigInteger r = BigInteger.ModPow(b, (p + 1) / 4, p);
        return (r, p - r);
    }
    internal static BigInteger ChineseRemainderTheorem(BigInteger pInv, BigInteger p, BigInteger qInv, BigInteger q, BigInteger modulo)
    {
        (BigInteger x, BigInteger y) = extendedGcd(p, q);

        BigInteger res = (pInv * y * q + qInv * x * p) % modulo;

        if (res < 0)
        {
            res = res + modulo;
        }

        return res;
    }
}
