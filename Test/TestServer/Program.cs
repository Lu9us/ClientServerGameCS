using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Lidgren.Network;
using Input;
using Entites;
namespace TestServer
{
    //test class remove after testing 
    class TestObj : GameObj
    {
        int t = 0;
        public TestObj()
        {
            tran = new Transform();
        
        }

        public override void onUpdate(float dt)
        {
            t++;
            if (t > 1000)
            {
                Random r = new Random();
                this.tran.xy[0] = r.Next(500);
                this.tran.xy[1] = r.Next(500);
                t = 0;
            }

          
        }

        public override void onSpawn()
        {
         
        }

        public override void onDeath()
        {
          
        }
    }
    class Pobj : GameObj
    {
        ConClient client;

        public Pobj(ConClient c)
        {
            tran = new Transform();
            client = c;
        
        }

        public override void onUpdate(float dt)
        {
            try
            {
                this.tran.xy[0] = client.LastInput.pos[0];
                this.tran.xy[1] = client.LastInput.pos[1];
            }
            catch (NullReferenceException)
            { 
            
            }
        }

        public override void onSpawn()
        {
            throw new NotImplementedException();
        }

        public override void onDeath()
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {
        static List<GameObj> ObjList;
        static List<ConClient> ClientList;
        static int clientidc = 1;
        static NetPeerConfiguration config;
        static NetServer server;
        static NetBuffer buff;
        static void startup()
        {
            ObjList = new List<GameObj>();
            Random r=new Random();
            for(int i=0;i<10;i++)
            {   
                TestObj to = new TestObj();
                to.id = i;
                to.name = "testObj";
                to.tran.xy[0]=r.Next(500);
                to.tran.xy[1] = r.Next(500);
                to.Sprite = "test";
                ObjList.Add(to);
            }
           
            ClientList = new List<ConClient>();
            config = new NetPeerConfiguration("Litrpg");
            config.MaximumConnections = 32;
            config.Port = 5410;
            server = new NetServer(config);
            config.SetMessageTypeEnabled(NetIncomingMessageType.ConnectionApproval, true);

            server.Start();
        }


        static void Main(string[] args)
        {
            startup();
            NetIncomingMessage msg;
            while (true)
            {

                foreach (GameObj obj in ObjList)
                {
                    obj.onUpdate(0.03333f);

                }


                while ((msg = server.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {



                        case NetIncomingMessageType.Data:
                            Console.WriteLine(msg.ToString() + msg.SenderConnection.ToString());
                            InputPacket dat = new InputPacket();
                            XmlSerializer bin = new XmlSerializer(dat.GetType());
                            var daat = (string)msg.ReadString();

                            StringReader read = new StringReader(daat);


                            dat = (InputPacket)bin.Deserialize(read);
                            foreach (ConClient cl in ClientList)
                            {
                                if (msg.SenderConnection == cl.con)
                                {
                                    cl.LastInput = dat;
                                    break;
                                }
                            
                            }
                            
                            Console.WriteLine(daat);
                            //str.Write(, 0, msg.Data.Length);
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            msg.SenderConnection.Approve();
                            //  NetOutgoingMessage msgs=server.CreateMessage();
                            ConClient c = new ConClient();
                            c.id = clientidc;
                            c.con = msg.SenderConnection;
                            ClientList.Add(c);
                            Pobj po = new Pobj(c);
                            po.Sprite = "test";
                            ObjList.Add(po);
                            //msgs.Write(clientidc);

                            //server.SendMessage(msgs, msg.SenderConnection, NetDeliveryMethod.Unreliable);
                            clientidc++;

                            break;
                        default:
                            break;
                    }

                    //(string) bin.Deserialize(str);

                }

                DataPacket p= new DataPacket();
                foreach (GameObj va in ObjList)
                {
                    p.addGameObj(va);
                
                }
                Console.WriteLine("Dub the fuck out this shit");
                foreach (ConClient va in ClientList)
                {
                    
                    NetOutgoingMessage msgs = server.CreateMessage();
                    XmlSerializer s = new XmlSerializer(p.GetType());
                      using (var stream = new StringWriter())
            {
                s.Serialize(stream, p);
                var str = stream.ToString();
                
                msgs.Write(str);
                Console.WriteLine("sending: " + str+"size in bits: "+msgs.LengthBits);
                server.SendMessage(msgs, va.con, NetDeliveryMethod.UnreliableSequenced);

                   
                    
                      }
                
                
                }

            }


        }
    }
}
