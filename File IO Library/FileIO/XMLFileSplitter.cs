using System;
using System.Linq;
using System.Xml.Linq;
namespace FileIOLib
{
    internal class XMLFileSplitter
    {
        public static void SplitXmlFile(string sourceFile
                       , string rootElement
                       , string descendantElement
                       , int takeElements
                       , string destFilePrefix
                       , string destPath)
        {
            XElement xml = XElement.Load(sourceFile);
            // Child elements from source file to split by.
            var childNodes = xml.Descendants(descendantElement);

            // This is the total number of elements to be sliced up into 
            // separate files.
            int cnt = childNodes.Count();

            var skip = 0;
            var take = takeElements;
            var fileno = 0;

            // Split elements into chunks and save to disk.
            while (skip < cnt)
            {
                // Extract portion of the xml elements.
                var c1 = childNodes
                            .Skip(skip)
                            .Take(take);

                // Setup number of elements to skip on next iteration.
                skip += take;
                // File sequence no for split file.
                fileno += 1;
                // Filename for split file.
                var filename = String.Format(destFilePrefix + "_{0}.xml", fileno);
                // Create a partial xml document.
                XElement frag = new XElement(rootElement, c1);
                // Save to disk.
                frag.Save(destPath + filename);
            }
        }
    }
}
