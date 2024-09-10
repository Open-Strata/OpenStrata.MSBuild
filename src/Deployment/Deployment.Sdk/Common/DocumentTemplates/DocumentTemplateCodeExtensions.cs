using Microsoft.Xrm.Tooling.PackageDeployment.CrmPackageExtentionBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;

namespace OpenStrata.Deployment.Sdk.Common.DocumentTemplates
{
    public static class DocumentTemplateCodeExtensions
    {

        public static void ReplaceText (this PackagePart part, string origText, string newText, TraceLogger logger)
        {
            using (Stream itemStream = part.GetStream(FileMode.Open, FileAccess.ReadWrite))
            {
                itemStream.ReplaceText(origText, newText, logger);
            }
        }

        public static void ReplaceText (this Stream stream, string origText, string newText, TraceLogger logger)
        {
            var tempDir = new DirectoryInfo(Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString()));



            try
            {
                tempDir.Create();
                var workFile = new FileInfo(Path.Combine(tempDir.FullName, "replacing.txt"));

                using (FileStream outputFileStream = new FileStream(workFile.FullName, FileMode.Create))
                {
                    stream.CopyTo(outputFileStream);
                }

                File.WriteAllText(workFile.FullName, File.ReadAllText(workFile.FullName)
                                                         .Replace(origText, newText));

                using (FileStream inputStream = new FileStream(workFile.FullName,FileMode.Open))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    inputStream.CopyTo(stream);
                }

            }
            finally
            {
                if (tempDir.Exists) tempDir.Delete(true);
            }


        }


    }
}
