namespace BSI.Integra.PruebasUnitarias
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var resultado = 2 + 3;
            Assert.AreEqual(5, resultado);
        }
    }
}
