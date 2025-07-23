using System;
using System.IO;
using cfEngine.IO;
using cfGodotEngine.Util;

namespace cfGodotTemplate.Util;

public static class InfoUtil 
{
    public static IStorage CreateStorage(Type infoType) 
    {
        var storagePath = $"{Application.exportDataPath}/Info/{infoType.Name}";
        
        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }
        
        return new LocalFileStorage(storagePath);
    }
}