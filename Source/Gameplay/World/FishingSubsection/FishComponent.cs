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
    public class FishComponent : Component
    {
        HookComponent hook;

        public FishComponent(Entity entity) : base(entity)
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public bool IsHooked()
        {
            return (hook != null);
        }

        public HookComponent GetHook()
        {
            return hook;
        }

        public void Hook(HookComponent h)
        {
            hook = h;
        }

        public void BeReleased()
        {
            hook = null;
        }

        public void Remove()
        {
            if (hook != null)
            {
                hook.Release();
                hook = null;
            }
        }
    }
}