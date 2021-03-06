﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont GameFont;
        private  List<GameObject> gameObjects,gameObjectsToAdd,gameObjectsToRemove;
        public Map Map { get; private set; }
        public bool MapChanged { get; set; }
        /// <summary>
        /// gets and sets the gameobject list
        /// </summary>
        public  List<GameObject> GameObjects
        { get{ return gameObjects; } set { gameObjects = value; } }
        /// <summary>
        /// gets and sets the gameobjects to add list
        /// </summary>
        public  List<GameObject> GameObjectsToAdd
        { get { return gameObjectsToAdd; } set { gameObjectsToAdd = value; } }
        /// <summary>
        /// gets and sets the gameobjects to remove
        /// </summary>
        public  List<GameObject> GameObjectsToRemove
        { get { return gameObjectsToRemove; } set { gameObjectsToRemove = value; } }
        EnemyPool enemyPool = new EnemyPool();
        public TowerPool towerPool = new TowerPool();
        public EnemyPool EnemyPool { get { return enemyPool; } }
        private List<Collider> colliders;
        /// <summary>
        /// gets the list of colliders
        /// </summary>
        public List<Collider> Colliders { get { return colliders; } }
        

        private Effect noEffect;
        private bool drawing;
        public Random rnd { get; private set; }
        private double fps;
        private static GameWorld instance = null;
        /// <summary>
        /// gets the deltatime
        /// </summary>
        public float deltaTime;
        /// <summary>
        /// gets the gameworld instance
        /// </summary>
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public bool Running { get; set; }


        private GameWorld()
        {

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
            gameObjects = new List<GameObject>();
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            colliders = new List<Collider>();
            rnd = new Random();
            Map = new Map(25,16);
            this.IsMouseVisible = true;
            Running = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //the game director and builders

            //add all the gameobjects
            //add player

            //add one enemy
            
            //gameObjects.Add(towerPool.Create(new Vector2(320, 0), 1, 5, 1));
            //gameObjects.Add(towerPool.Create(new Vector2(320,32),1,5,1 ));
            //gameObjects.Add(towerPool.Create(new Vector2(352, 96), 1, 5, 1));
            //gameObjects.Add(towerPool.Create(new Vector2(384, 32), 1, 5, 1));
            //gameObjects.Add(towerPool.Create(new Vector2(384, 64), 1, 5, 1));
            //loads the map
            Map.LoadContent(Content);
            //loads all the gameobjects
            foreach (var gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }
            GameFont = Content.Load<SpriteFont>("font");
            AI.Instance.Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public GameTime upGameTime { get; private set; }
        bool toggle = false;
        private float spawnTimer;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            upGameTime = gameTime;
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            spawnTimer += deltaTime;
            if(spawnTimer > 10f) { 
            for (int i = 0; i < 2; i++)
            {
                gameObjectsToAdd.Add(enemyPool.Create(new Vector2(0, rnd.Next(0, Map.sizeY) * 32), 0.5f, 5, 1));
            }
                spawnTimer = 0;
            }
            KeyboardState keyState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) { 
                Running = false;
                Exit();
            }
            // TODO: Add your update logic here

            //test start
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            if (Map.Tiles != null)
            {
                for (int x = 0; x < Map.Tiles.GetLength(0); x++)
                {
                    for (int y = 0; y < Map.Tiles.GetLength(1); y++)
                    {
                        if (Map.Tiles[x, y].Pos.X <= mouseX
                            && Map.Tiles[x, y].Pos.X + 32 > mouseX &&
                            Map.Tiles[x, y].Pos.Y <= mouseY
                            && Map.Tiles[x, y].Pos.Y + 32 > mouseY)
                        {
                            if(mouseState.LeftButton == ButtonState.Pressed)
                            if (!Map.Tiles[x, y].HasTower)
                            {
                                gameObjectsToAdd.Add(towerPool.Create(new Vector2(32*x, 32*y), 1, 5, 1));
                                Map.Tiles[x, y].HasTower = true;
                                
                            }
                        }
                    }
                }
            }
            if (keyState.IsKeyDown(Keys.K) && !toggle)
            {
                gameObjectsToAdd.Add(enemyPool.Create(new Vector2(0,GraphicsDevice.PresentationParameters.Bounds.Height / 2), 0, 5,1));
                toggle = true;
            }
            else if(toggle && !keyState.IsKeyDown(Keys.K))
            {
                toggle = false;
            }

            //test end


            if (gameObjectsToAdd.Count > 0)
            {
                foreach (var gameObject in gameObjectsToAdd)
                {
                    gameObject.LoadContent(Content);
                }
            }
         Map.Update(gameTime);
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update();
            }
            
            if (!drawing)
            {
                if (gameObjectsToRemove.Count > 0)
                {
                    for (int i = 0; i < gameObjectsToRemove.Count; i++)
                    {
                        if (gameObjectsToRemove[i].GetComponent("Enemy") != null)
                        {
                            enemyPool.ReleaseObject(gameObjectsToRemove[i]);
                        }
                        gameObjects.Remove(gameObjectsToRemove[i]);
                    }
                }
                if (gameObjectsToAdd.Count > 0)
                {
                    for (int i = 0; i < gameObjectsToAdd.Count; i++)
                    {
                        gameObjects.Add(gameObjectsToAdd[i]);
                    }
                }
                gameObjectsToAdd.Clear();
                gameObjectsToRemove.Clear();
            }
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            drawing = true;
            Window.Title = fps.ToString();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);
            //draws the map
            Map.Draw(spriteBatch);

            //draws all objects
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
            spriteBatch.DrawString(GameFont,"",Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
            drawing = false;
        }
    }
}
