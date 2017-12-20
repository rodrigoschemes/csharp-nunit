using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.Leilao
{
    [TestFixture]
    public class CalculoMatematicoTest
    {
        [Test]
        public void DeveMultiplicarNumerosMaioresQue30()
        {
            CalculoMatematico matematica = new CalculoMatematico();
            Assert.AreEqual(50 * 4, matematica.ContaMatematica(50));
        }

        [Test]
        public void DeveMultiplicarNumerosMaioresQue10EMenoresQue30()
        {
            CalculoMatematico matematica = new CalculoMatematico();
            Assert.AreEqual(20 * 3, matematica.ContaMatematica(20));
        }

        [Test]
        public void DeveMultiplicarNumerosMenoresQue10()
        {
            CalculoMatematico matematica = new CalculoMatematico();
            Assert.AreEqual(5 * 2, matematica.ContaMatematica(5));
        }
    }
}
