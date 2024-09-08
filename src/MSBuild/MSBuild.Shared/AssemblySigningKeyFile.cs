using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OpenStrata.MSBuild
{
    public static class AssemblySigningKeyFile
    {
        public static void Create(string keyFilePath)
        {
            using (RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(2048))
            {
                byte[] bytes = cryptoServiceProvider.ExportCspBlob(true);
                bytes[4] = (byte)0;
                bytes[5] = (byte)36;
                bytes[6] = (byte)0;
                bytes[7] = (byte)0;
                File.WriteAllBytes(keyFilePath, bytes);
            }
        }

    }
}
