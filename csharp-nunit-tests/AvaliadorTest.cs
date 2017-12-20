using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.Leilao
{
    [TestFixture]
    public class AvaliadorTest
    {
        private Avaliador leiloeiro;
        private Usuario joao;
        private Usuario jose;
        private Usuario maria;

        [OneTimeSetUp]
        public void TestandoBeforeClass()
        {
            Console.WriteLine("Exceuta uma vez antes do teste");
        }

        [SetUp]
        public void SetUp()
        {
            this.leiloeiro = new Avaliador();

            this.joao = new Usuario("João");
            this.jose = new Usuario("José");
            this.maria = new Usuario("Maria");
        }

        [Test]
        public void DeveEntenderLancesEmOrdemCrescente()
        {
            Leilao leilao = new Leilao("Playstation 3 Novo");
            leilao.Propoe(new Lance(maria, 250.0));
            leilao.Propoe(new Lance(joao, 300.0));
            leilao.Propoe(new Lance(jose, 400.0));

            // executando a acao 
            leiloeiro.Avalia(leilao);

            // comparando a saida com o esperado 
            double maiorEsperado = 400;
            double menorEsperado = 250;
            Assert.AreEqual(maiorEsperado, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(menorEsperado, leiloeiro.MenorLance, 0.0001);
        }

        [Test]
        public void DeveEntenderLeilaoComApenasUmLance()
        {
            Leilao leilao = new Leilao("Playstation 3 Novo");

            leilao.Propoe(new Lance(joao, 1000.0));

            leiloeiro.Avalia(leilao);

            Assert.AreEqual(1000, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(1000, leiloeiro.MenorLance, 0.0001);
        }

        [Test]
        public void DeveEncontrarOsTresMaioresLances()
        {
            Leilao leilao = new CriadorDeLeilao().Para("Playstation 3 Novo")
            .Lance(joao, 100.0)
            .Lance(maria, 200.0)
            .Lance(joao, 300.0)
            .Lance(maria, 400.0)
            .Constroi();

            leiloeiro.Avalia(leilao);

            IList<Lance> maiores = leiloeiro.TresMaiores;

            Assert.AreEqual(3, maiores.Count);
        }

        [Test]
        public void DeveEntenderLeilaoComLancesEmOrdemRandomica()
        {
            Leilao leilao = new Leilao("Playstation 3 Novo");

            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 450.0));
            leilao.Propoe(new Lance(joao, 120.0));
            leilao.Propoe(new Lance(maria, 700.0));
            leilao.Propoe(new Lance(joao, 630.0));
            leilao.Propoe(new Lance(maria, 230.0));

            leiloeiro.Avalia(leilao);

            Assert.AreEqual(700.0, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(120.0, leiloeiro.MenorLance, 0.0001);
        }

        [Test]
        public void DeveEntenderLeilaoComLancesEmOrdemDecrescente()
        {
            Leilao leilao = new Leilao("Playstation 3 Novo");

            leilao.Propoe(new Lance(joao, 400.0));
            leilao.Propoe(new Lance(maria, 300.0));
            leilao.Propoe(new Lance(joao, 200.0));
            leilao.Propoe(new Lance(maria, 100.0));

            leiloeiro.Avalia(leilao);

            Assert.AreEqual(400.0, leiloeiro.MaiorLance, 0.0001);
            Assert.AreEqual(100.0, leiloeiro.MenorLance, 0.0001);
        }

        [Test]
        public void NaoDeveAvaliarLeiloesSemNenhumLanceDado()
        {
            Leilao leilao = new CriadorDeLeilao()
                .Para("Playstation 3 Novo")
                .Constroi();
            
            Assert.That(() => leiloeiro.Avalia(leilao),
                Throws.TypeOf<Exception>());
        }

        [TearDown]
        public void Finaliza()
        {
            Console.WriteLine("fim");
        }

        [OneTimeTearDown]
        public void TestandoAfterClass()
        {
            Console.WriteLine("Executa uma vez após o fim dos testes");
        }
    }
}
