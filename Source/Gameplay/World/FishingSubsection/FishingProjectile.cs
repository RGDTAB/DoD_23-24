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
using System.Diagnostics;
#endregion

namespace DoD_23_24
{
    public class FishingProjectile : Entity
    {
        TransformComponent transform;
        Vector2 velocity;
        int damage;
        TransformComponent playerTransform;
        FishComponent fish;

        public FishingProjectile(string name, string PATH, Vector2 POS, float ROT, Vector2 DIMS, int DAM, Vector2 VEL): base(name, Layer.NPC)
        {
            transform = (TransformComponent)AddComponent(new TransformComponent(this, POS, ROT, DIMS));
            AddComponent(new RenderComponent(this, PATH));
            AddComponent(new CollisionComponent(this, false, false));
            damage = DAM;
            velocity = VEL;

            fish = (FishComponent)AddComponent(new FishComponent(this));
        }

        ~FishingProjectile()
        {
            fish.Remove();
        }

        public override void Update(GameTime gameTime)
        {
            if (fish.IsHooked())
            {
                // TODO: Replace with code to swing the fish in a circle
                velocity.Y = Math.Abs(velocity.Y) * -1;
            }
            else
            {
                // I used this to prove that the fish was released
                //velocity.Y = Math.Abs(velocity.Y);
            }
            transform.pos += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void OnCollision(Entity otherEntity)
        {
            if (otherEntity.layer == Layer.Player || otherEntity.layer == Layer.NPC)
            {
                if (otherEntity.name == "hookZone")
                {
                    if (!fish.IsHooked())
                    {
                        Entity boat = otherEntity.GetParent();
                        boat.GetComponent<HookComponent>().EnterRange(this);
                    }
                }
                else
                {
                    // TODO: Damage Player or NPC
                    //this.SetActive(false);
                    //fish.Remove();
                }
            }
        }
    }
}