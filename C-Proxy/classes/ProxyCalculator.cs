using C_Proxy.interfaces;

namespace C_Proxy;

public class ProxyCalculator : interfaces.Calculator
{
    private Calculator _realCalculator;
    public ProxyCalculator()
    {
        _realCalculator = new RealCalculator();
    }
    public float sum(float first, float second)
    {
        string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"cache.txt");
        Console.WriteLine("file: "+file);
        Cache cache = new(file);
        CacheValue? valueInCache = cache.getInCache(first, second);
        if (valueInCache != null)
        {
            Console.WriteLine("Got in cache");
            return valueInCache.result;
        }
        Console.WriteLine("Added to cache");
        CacheValue result = new CacheValue()
        {
            result = _realCalculator.sum(first, second),
            key = new Key(){ val1 = first, val2 = second},
            expirationDate = DateTime.Now.AddMinutes(2)
        };
        cache.AddToCache(result);
        return result.result;
    }
}