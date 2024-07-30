
namespace CSharpEIF.AppEnvironment
{
    class My3DExperience
    {
        // e.g failover:(ssl://pod171-33-98-34.3dexperience.3ds.com:80,ssl://pod171-33-98-34-1.3dexperience.3ds.com:80)?randomize=false 
        public string JmsUrl => "message bus url from agent manager";

        // e.g 1fff6195-5c3f-4774-b4ca-5691c585d5f2
        public string AgentID => "your agent id";

        // The password provided by PFI...
        public string AgentPassword => "your agent password";

        // e.g R1132100982379
        public string Tenant => "RXXXXXX your tenant";
    }
}
