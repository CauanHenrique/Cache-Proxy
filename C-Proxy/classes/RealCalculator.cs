namespace C_Proxy;

public class RealCalculator : interfaces.Calculator
{
    public float sum(float first, float second)
    {
        return first+second;
    }
}