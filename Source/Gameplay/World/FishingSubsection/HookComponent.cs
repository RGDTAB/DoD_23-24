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
using System.Reflection.Metadata.Ecma335;
using System.ComponentModel;
#endregion
namespace DoD_23_24
{
    public class HookComponent : Component
    {
        bool isFishing = false;
        List<Entity> inRange = new List<Entity>();
        Entity hookedEntity;
        Texture2D lineModel;

        public HookComponent(Entity entity) : base(entity)
        {
            hookedEntity = null;
            lineModel = new Texture2D(Globals.graphics, 1, 1);
            lineModel.SetData(new Color[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            if (inRange.Count != 0 && !isFishing && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isFishing = true;

                Vector2 parentPos = (entity.GetComponent<TransformComponent>()).pos;
                float minDistance = 1000000; // Arbitrarily large value
                TransformComponent fishTransform;
                Vector2 fishPos;

                foreach (Entity fish in inRange)
                {
                    fishTransform = fish.GetComponent<TransformComponent>();
                    fishPos = new Vector2(fishTransform.pos.X + fishTransform.dims.X / 2,
                        fishTransform.pos.Y + fishTransform.dims.Y);
                    float dist = Vector2.Distance(parentPos, fishPos);
                    if (dist < minDistance)
                    {
                        hookedEntity = fish;
                        minDistance = dist;
                    }
                }
                hookedEntity.GetComponent<FishComponent>().Hook(this);
            }
            else if (isFishing && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                if (hookedEntity != null)
                {
                    hookedEntity.GetComponent<FishComponent>().BeReleased();
                }
                this.Release();
            }
            inRange.Clear();
        }

        public override void Draw()
        {
            if (isFishing)
            {
                TransformComponent fishTransform = hookedEntity.GetComponent<TransformComponent>();
                Vector2 fishPos = new Vector2(fishTransform.pos.X + fishTransform.dims.X / 2,
                    fishTransform.pos.Y + fishTransform.dims.Y / 2);
                Vector2 parentPos = entity.GetComponent<TransformComponent>().pos;
                float dist = Vector2.Distance(fishPos, parentPos);

                Globals.spriteBatch.Draw(lineModel,
                    new Rectangle((int)parentPos.X, (int)parentPos.Y, 1, (int)dist),
                    null, Color.White,
                    (float)Math.Atan2(fishPos.Y - parentPos.Y, fishPos.X - parentPos.X) - (float)(Math.PI / 2f),
                    Vector2.Zero, SpriteEffects.None, 0);
            }
        }

        public void EnterRange(Entity entity)
        {
            if (!isFishing)
            {
                inRange.Add(entity);
            }
        }

        public void Release()
        {
            hookedEntity = null;
            isFishing = false;
        }

        public Vector2 GetParentPos()
        {
            // Should we replace with center of parent instead?
            return entity.GetComponent<TransformComponent>().pos;
        }

        public bool IsFishing()
        {
            return isFishing;
        }
    }
}