﻿#region Includes
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

using System.Reflection.Metadata;

#endregion

namespace DoD_23_24
{
    public class World
    {
        public List<Entity> entities = new List<Entity>();
        
        public World()
        {
            Globals.collisionSystem = new CollisionSystem();

            /*Entity playerInstance = new Player("Player", "2D/Sprites/Item", new Vector2(100, 100), 0.0f, new Vector2(16, 16));
            Entity randomThing = new Entity("RandomThing", Layer.NPC);
            randomThing.AddComponent(new TransformComponent(randomThing,
                new Vector2(-50, -50), 0.0f, new Vector2(16, 16)));
            randomThing.AddComponent(new RenderComponent(randomThing, "2D/Sprites/Item"));
            randomThing.AddComponent(new CollisionComponent(randomThing, true, true));
            Entity camera = new Entity("Camera", Layer.Camera);
            camera.AddComponent(new CameraComponent(camera, playerInstance));


            NPC book = new NPC("Book", "2D/Sprites/Special1", new Vector2(80, 64), 0.0f, new Vector2(16, 16), "Content/NPCText/TestNPC.txt");

            TileMapGenerator tileMapGenerator = new TileMapGenerator("Content/map.tmx", "Tiny Adventure Pack\\");
            entities.AddRange(tileMapGenerator.GetTiles());
            entities.Add(playerInstance);
            entities.Add(randomThing);
            entities.Add(camera);
            entities.Add(book);
            entities.Add(book.GetOverlapZone());*/

            // Fisherman Subsection
            PirateShip pirateShip = new PirateShip("Pirate", "2D/Sprites/PirateShip", new Vector2(100, 0), 0, new Vector2(50, 50));
            BoatPlayer boatPlayerInstance = new("Player", "2D/Sprites/Item", new Vector2(100, 100), 0.0f, new Vector2(16, 16));
            TileMapGenerator tileMapGenerator = new TileMapGenerator("Content/FishermanBattle.tmx", "2D\\");
            entities.AddRange(tileMapGenerator.GetTiles());
            entities.Add(boatPlayerInstance);
            entities.Add(boatPlayerInstance.GetHookZone());
            entities.Add(pirateShip);


            //Entity center = new Entity("center", Layer.Tiles);
            // Not actually at the center. Tilemap is 23 tiles wide, but TileMapGenerator reports 30
            //center.AddComponent(new TransformComponent(center, new Vector2(184, 112), 0, Vector2.Zero));
            Entity camera = new Entity("Camera", Layer.Camera);
            camera.AddComponent(new CameraComponent(camera, boatPlayerInstance));
            entities.Add(camera);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity entity in entities)
            {
                entity.Update(gameTime);
            }

            Globals.collisionSystem.Update(gameTime);
        }

        public void Draw()
        {
            foreach (Entity entity in entities)
            {
                entity.Draw();
            }
        }

        public Entity GetCamera()
        {
            foreach (Entity entity in entities)
            {
                if (entity.name == "Camera")
                {
                    return entity;
                }
            }
            return null;
        }
    }
}