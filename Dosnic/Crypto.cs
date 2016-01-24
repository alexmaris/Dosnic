﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dosnic
{
    public class Crypto : IDisposable
    {
        public string Key { get; protected internal set; }
        public string IV { get; protected internal set; }
        public SymmetricAlgorithm SymetricAlgo { get; protected internal set; }

        public string Encrypt(string originalObject)
        {
            if (Key == null || IV == null) throw new NullReferenceException("Key and IV must be non-null");

            byte[] originalStrAsBytes = Encoding.Default.GetBytes(originalObject);

            using (MemoryStream memStream = new MemoryStream(originalStrAsBytes.Length))
            using (ICryptoTransform rdTranasform = SymetricAlgo.CreateEncryptor(Convert.FromBase64String(Key), Convert.FromBase64String(IV)))
            using (CryptoStream cryptoStream = new CryptoStream(memStream, rdTranasform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(originalStrAsBytes, 0, originalStrAsBytes.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memStream.ToArray());
            }
        }

        public string Decrypy(string encryptedObject)
        {
            if (Key == null || IV == null) throw new NullReferenceException("Key and IV must be non-null");

            byte[] encryptedObjectAsBytes = Convert.FromBase64String(encryptedObject);

            using (MemoryStream memStream = new MemoryStream(encryptedObjectAsBytes))
            using (ICryptoTransform rdTranasform = SymetricAlgo.CreateDecryptor(Convert.FromBase64String(Key), Convert.FromBase64String(IV)))
            using (CryptoStream cryptoStream = new CryptoStream(memStream, rdTranasform, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cryptoStream, true))
                return sr.ReadToEnd();
        }

        public void Dispose()
        {
            if (SymetricAlgo != null) SymetricAlgo.Clear();
        }
    }
}