
namespace CSharpEIF.AppEnvironment
{
    class My3DExperience
    {
        // e.g failover:(ssl://pod171-33-98-34.3dexperience.3ds.com:80,ssl://pod171-33-98-34-1.3dexperience.3ds.com:80)?randomize=false 
        public string JmsUrl => "failover:(ssl://eu1-msgbus.3dexperience.3ds.com:61616,ssl://eu1-msgbus-1.3dexperience.3ds.com:61616)?randomize=false";

        // e.g 1fff6195-5c3f-4774-b4ca-5691c585d5f2
        public string AgentID => "7d738b39-b683-4103-9589-7ca7d2e12ce8";

        // The password provided by PFI...
        public string AgentPassword => "&,y+sp2vbk95<X-C*2W>Lqx#";

        // e.g R1132100982379
        public string Tenant => "R1132101096635";
    }
}
