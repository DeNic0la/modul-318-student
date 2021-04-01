namespace SwissTransport
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SwissTransport.Core;
    using System;

    /// <summary>
    /// The Swiss Transport API tests.
    /// </summary>
    [TestClass]
    public class TransportTest
    {
        private ITransport testee;

        [TestMethod]
        public void Locations()
        {
            testee = new Transport();
            var stations = this.testee.GetStations("Sursee,");

            Assert.AreEqual(10, stations.StationList.Count);
        }

        [TestMethod]
        public void StationBoard()
        {
            testee = new Transport();
            var stationBoard = this.testee.GetStationBoard("Sursee", "8502007");

            Assert.IsNotNull(stationBoard);
        }

        [TestMethod]
        public void Connections()
        {
            testee = new Transport();
            var connections = this.testee.GetConnections("Sursee", "Luzern");

            Assert.IsNotNull(connections);
        }


        [TestMethod]
        public void ConnectionsWithTime()
        {
            testee = new Transport();
            var connections = this.testee.GetConnections("Sursee", "Luzern", DateTime.Now, "12:00");

            Assert.IsNotNull(connections);
        }
    }
}
