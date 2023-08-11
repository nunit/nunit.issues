using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebApp;

namespace WebAppIntegrationTesting
{
    public  class WhateverTests
    {
        [Test]
        public void Test1()
        {
            var whatever = new Whatever();
            Assert.That(whatever.Add(1, 2), Is.EqualTo(3));
        }

        [Test]
        public void Test2()
        {
            var whatever = new Whatever();
            Assert.That(whatever.Add(1, 2), Is.EqualTo(3));
        }
    }
}
