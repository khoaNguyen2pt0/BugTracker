using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BugTracker.Helpers
{
    public class FileStamp
    {
        public static string MakeUnique(string fileName)
        {
            // "my Image.png"
            var extension = Path.GetExtension(fileName); // .png (including the dot) - could be .jpg .gif ... 
            fileName = Path.GetFileNameWithoutExtension(fileName); // my Image
            fileName = SlugMaker.MakeSlug(fileName); // my-image
            fileName = $"{fileName}{DateTime.Now.Ticks}{extension}"; // my-image-231241231243225413266779.png (unique name everytime)
            return fileName;

           
        }
    }
}