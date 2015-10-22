using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Input;
using Entites;
using Lidgren.Network;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
namespace Test
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        NetPeerConfiguration conf;
        NetClient client;
        Dictionary<string, Texture2D> texmap;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<ProxyObj> objlist;
        int clientID=0;
        public Game1()
        {

            objlist = new List<ProxyObj>();
            conf = new NetPeerConfiguration("Litrpg");
            client = new NetClient(conf);
            client.Start();
            client.Connect("127.0.0.1", 5410);
            texmap = new Dictionary<string, Texture2D>();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
         
              
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        Vector2 FontPos;
        SpriteFont font;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            texmap.Add("test", Content.Load<Texture2D>("testex"));
            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                                  graphics.GraphicsDevice.Viewport.Height / 2);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        InputPacket pk;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
             pk = new InputPacket();
            // TODO: Add your update logic here
             XmlSerializer bin = new XmlSerializer(pk.GetType()); 
            
            base.Update(gameTime);
            NetOutgoingMessage msg=client.CreateMessage();
            using (var stream = new StringWriter())
            {
                
                bin.Serialize(stream,pk);
                var aarr = stream.ToString();
            msg.Write( stream.ToString());
            var arrarr = msg.ReadString();
            client.SendMessage(msg, NetDeliveryMethod.UnreliableSequenced);
            
            }

            NetIncomingMessage cmsg;

            while ((cmsg = client.ReadMessage()) != null)
            {
               
                if (cmsg.MessageType == NetIncomingMessageType.Data)
                {

                    XmlSerializer dbin = new XmlSerializer(typeof(DataPacket)); //desserilizer
                    var daat = (string)cmsg.ReadString();

                    StringReader read = new StringReader(daat);
                    try {
                        DataPacket a;
                        if ((a=(DataPacket)dbin.Deserialize(read)) != null)
                        {
                             
                            objlist = a.ObjList;
                        }
                    }
                    catch(System.InvalidOperationException)
                    {
                        break;
                    }
                
                }
                  


                            
            




            }
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            Vector2 i= new Vector2();

            Vector2 FontOrigin = font.MeasureString(pk.pos[0].ToString()) / 2;
            spriteBatch.DrawString(font, pk.pos[0].ToString(), new Vector2(30, 10), Color.LightGreen,
                    0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            FontOrigin = font.MeasureString(pk.pos[1].ToString()) / 2;
            spriteBatch.DrawString(font, pk.pos[1].ToString(), new Vector2(30, 30), Color.LightGreen,
                    0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            i.X += 10;
            spriteBatch.Draw(texmap["test"], new Vector2(pk.pos[0], pk.pos[1]), null);

            foreach (Keys var in pk.keys)
            { 
               FontOrigin = font.MeasureString(var.ToString()) / 2;
                spriteBatch.DrawString(font, var.ToString(), FontPos+i, Color.LightGreen,
                    0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                i.X+=10;
                i.X += FontOrigin.X;
            }
            foreach (ProxyObj obj in objlist)
            {
                if (obj.spritetag != null  )
                {
                    spriteBatch.Draw(texmap[obj.spritetag], new Vector2(obj.tran.xy[0], obj.tran.xy[1]), null);
                }
            
            
            }


            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
