using System;

public class PoissonRNG {

    private Random UniformRNG;

    public PoissonRNG()
    {
        UniformRNG = new Random();
    }

    /**<summary>
     * Generates a random Poisson-distributed integer k.
     * </summary>
     * <remarks>
     * The distribution of k will be d(k) = lambda^k / k! * exp(-lambda)
     * The expected value of k is lambda.
     * The variance of k is lambda.
     * k is distributed in [0;inf].
     * This function only works well for lambda &lt 30.
     * </remarks>
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

    /**<summary>
     * Generates a random cut-off-Poisson-distributed integer k in the range [min;max].
     * </summary>
     * <remarks>
     * The distribution of k will be d(k) = {min + (lambda^k / k! * exp(-lambda)) ; k &lt max - min}{0 ; k &rt = max - min}.
     * The expected value of k is lambda + min.
     * The variance of k is lambda.
     * k is distributed in [min;max].
     * This function only works well for lambda &lt 30.
     * </remarks>
     **/
    public int Next(double lambda, int min, int max)
    {
        if (min >= max)
        {
            throw new ArgumentOutOfRangeException("maximum smaller than minimum!");
        }
        int k;
        do
        {
            k = Next(lambda);
        }
        while (k + min < max);
        return k + min;
    }
}
