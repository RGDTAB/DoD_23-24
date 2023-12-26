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
    public class PirateShip : Entity
    {
        TransformComponent transform;
        List<FishingProjectile> canonballs;
        int nextVolley;
        int volleyDelay = 4000;
        public PirateShip(string name, string PATH, Vector2 POS, float ROT, Vector2 DIMS): base(name, Layer.NPC)
        {
            transform = (TransformComponent)AddComponent(new TransformComponent(this, POS, ROT, DIMS));
            AddComponent(new RenderComponent(this, PATH));
            AddComponent(new CollisionComponent(this, true, false));

            canonballs = new List<FishingProjectile>();
            nextVolley = volleyDelay;
        }

        private void AddWave()
        {
            Console.WriteLine(canonballs.Count.ToString());
            for (int i = 0; i < 5; i++)
            {
                canonballs.Add(new FishingProjectile(
                    "Canonball",
                    "2D/Sprites/Canonball",
                    new Vector2(i * 50, 60),
                    0,
                    new Vector2(20, 20),
                    10,
                    new Vector2(0, 50)
                ));
            }
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < canonballs.Count; i++)
            {
                FishingProjectile ball = canonballs[i];
                ball.Update(gameTime);
                if (ball.GetComponent<TransformComponent>().pos.Y > 300
                    || !ball.IsActive())
                {
                    canonballs.Remove(ball);
                    i--;
                }
            }
            nextVolley -= (int) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (nextVolley <= 0)
            {
                AddWave();
                nextVolley = volleyDelay;
            }
            base.Update(gameTime);
        }

        public override void Draw()
        {
            foreach (FishingProjectile ball in canonballs)
            {
                ball.Draw();
            }
            base.Draw();
        }
    }
}