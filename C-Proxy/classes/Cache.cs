using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using C_Proxy.interfaces;

namespace C_Proxy;

public class Cache
{
    private string cacheFile;

    public Cache(string file)
    {
        cacheFile = file;
    }

    public CacheValue? getInCache(float val1, float val2)
    {
        List<CacheValue>? cacheItems = ListCache();
        if (cacheItems == null)
        {
            string emptyList = JsonSerializer.Serialize<List<CacheValue>>(new List<CacheValue>());
            File.WriteAllText(cacheFile, emptyList);

            return null;
        }

        CacheValue? foundCache = new();

        foundCache = cacheItems.Find((cache) => { return cache.key.val1 == val1 && cache.key.val2 == val2; });
        if (foundCache != null && foundCache.expirationDate <= DateTime.Now)
        {
            RemoveFromCache(foundCache);
            return null;
        }

        return foundCache;
    }

    public bool RemoveFromCache(CacheValue cacheValue)
    {
        List<CacheValue>? cacheItems = ListCache();
        if (cacheItems == null)
        {
            return false;
        }

        int removed = cacheItems.RemoveAll(cache => (cache.key.val1 == cacheValue.key.val1) &&
                                                     (cache.key.val2 == cacheValue.key.val2));
        if (removed <= 0)
        {
            return false;
        }

        string strCacheItems = JsonSerializer.Serialize<List<CacheValue>>(cacheItems);
        File.WriteAllText(cacheFile, strCacheItems);
        return true;
    }

    public List<CacheValue>? ListCache()
    {
        if (!File.Exists(cacheFile))
        {
            return null;
        }

        CacheValue? foundCache = new();
        var fileContent = File.ReadAllText(cacheFile);
        return JsonSerializer.Deserialize<List<CacheValue>>(fileContent);
    }

    public bool AddToCache(CacheValue value)
    {
        List<CacheValue>? cacheItems = ListCache();
        if (cacheItems == null)
        {
            using (FileStream fs = File.OpenWrite(cacheFile))
            {
                List<CacheValue> newCacheList = new List<CacheValue>() { value };
                string strCacheItems = JsonSerializer.Serialize<List<CacheValue>>(newCacheList);
                Byte[] info =
                    new UTF8Encoding(true).GetBytes(strCacheItems);
                fs.Write(info, 0, info.Length);
            }
        }

        cacheItems.Add(value);
        string addedToFile = JsonSerializer.Serialize<List<CacheValue>>(cacheItems);
        File.WriteAllText(cacheFile, addedToFile);
        return true;
    }
}