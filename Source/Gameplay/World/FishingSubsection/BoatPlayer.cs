#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion
namespace DoD_23_24
{
	public class BoatPlayer : Entity
	{
        const float speed = 50f;
        TransformComponent transform;
        bool isFrozen = false;

        Entity hookZone;
        HookComponent hook;

        public BoatPlayer(string name, string PATH, Vector2 POS, float ROT, Vector2 DIMS) : base(name, Layer.Player)
		{
            transform = (TransformComponent)AddComponent(new TransformComponent(this, POS, ROT, DIMS));
            AddComponent(new RenderComponent(this, PATH));
            AddComponent(new CollisionComponent(this, true, true));

            hookZone = new Entity("hookZone", Layer.Player);
            hookZone.AddComponent(new TransformComponent(hookZone, new Vector2((int)POS.X - 64, (int)POS.Y - 96), 0.0f, new Vector2(128, 96)));
            hookZone.AddComponent(new CollisionComponent(hookZone, false, false));
            hookZone.SetParent(this);
            hook = (HookComponent)AddComponent(new HookComponent(this));
        }

        public override void Update(GameTime gameTime)
        {
            // Update location for hook region
            TransformComponent hookTransform = hookZone.GetComponent<TransformComponent>();
            hookTransform.pos = new Vector2((int)transform.pos.X - 64 + transform.dims.X / 2, (int)transform.pos.Y - 96);

            base.Update(gameTime);
            Movement(gameTime);

        }

        public void Movement(GameTime gameTime)
        {
            if(isFrozen || hook.IsFishing())
            {
                return;
            }

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Left))
            {
                transform.pos.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                transform.pos.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Up))
            {
                transform.pos.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                transform.pos.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void OnCollision(Entity otherEntity)
        {
            Console.WriteLine("I'm Colliding!");
        }
        
        public Entity GetHookZone()
        {
            return hookZone;
        }
    }
}