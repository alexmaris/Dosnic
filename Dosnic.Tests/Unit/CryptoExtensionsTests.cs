﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;

namespace Dosnic.Tests.Unit
{
    [TestClass]
    public class CryptoExtensionsTests
    {
        [TestMethod]
        public void Should_default_to_RijndaelManaged()
        {
            var crypto = CryptoUtils.Initialize<string>();

            crypto.SymetricAlgo.Should().BeOfType<RijndaelManaged>();
        }

        [TestMethod]
        public void Should_assign_key()
        {
            var key = "my secret key";
            var crypto = CryptoUtils.Initialize<string>().WithKey(key);

            crypto.Key.Should().BeEquivalentTo(key);
        }

        [TestMethod]
        public void Should_assign_iv()
        {
            var iv = "123 my iv";
            var crypto = CryptoUtils.Initialize<string>().WithIV(iv);

            crypto.IV.Should().BeEquivalentTo(iv);
        }

        [TestMethod]
        public void Should_generate_key()
        {
            var crypto = CryptoUtils.Initialize<string>().GenerateSecretKey();

            crypto.Key.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Should_generate_iv()
        {
            var crypto = CryptoUtils.Initialize<string>().GenerateSecretIV();

            crypto.IV.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Should_generate_key_and_iv()
        {
            var crypto = CryptoUtils.Initialize<string>().WithAutoGeneratedKeyAndIV();
            crypto.IV.Should().NotBeNullOrEmpty();
            crypto.Key.Should().NotBeNullOrEmpty();
        }
    }
}
