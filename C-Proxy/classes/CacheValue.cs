namespace C_Proxy.interfaces;

public class Key
{
    public float val1 { get; set; }
    public float val2 { get; set; }
}

public class CacheValue
{
    public Key key { get; set; }
    public float result { get; set; }
    public DateTime expirationDate { get; set; }
}