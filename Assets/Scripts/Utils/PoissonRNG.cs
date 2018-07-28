using System;

public class PoissonRNG {

    private Random UniformRNG;

    public PoissonRNG()
    {
        UniformRNG = new Random();
    }

    /**Generates a random Poisson-distributed integer k.
     * The distribution of k will be d(k) = lambda^k / k! * exp(-lambda)
     * The expected value of k is lambda.
     * The variance of k is lambda.
     * k is distributed in [0;inf].
     * This function only works well for lambda < 30.
     **/
    public int Next(double lambda)
    {
        double p = 1.0;
        double L = Math.Exp(-lambda);
        int k = 0;
        do
        {
            k++;
            p *= UniformRNG.NextDouble();
        }
        while (p > L);
        return k - 1;
    }
}
