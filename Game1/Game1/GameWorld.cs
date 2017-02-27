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
        private static List<GameObject> gameObjects,gameObjectsToAdd,gameObjectsToRemove;
        /// <summary>
        /// gets and sets the gameobject list
        /// </summary>
        public static List<GameObject> GameObjects
        { get{ return gameObjects; } set { gameObjects = value; } }
        /// <summary>
        /// gets and sets the gameobjects to add list
        /// </summary>
        public static List<GameObject> GameObjectsToAdd
        { get { return gameObjectsToAdd; } set { gameObjectsToAdd = value; } }
        /// <summary>
        /// gets and sets the gameobjects to remove
        /// </summary>
        public static List<GameObject> GameObjectsToRemove
        { get { return gameObjectsToRemove; } set { gameObjectsToRemove = value; } }
        EnemyPool enemyPool = new EnemyPool();
        private List<Collider> colliders;
        /// <summary>
        /// gets the list of colliders
        /// </summary>
        public List<Collider> Colliders { get { return colliders; } }


        private Effect noEffect;
        private bool drawing;
        private Random rnd;
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
            GameObjectDirector gdr;
            PlayerBuilder playerBuilder = new PlayerBuilder();
            playerBuilder.BuildGameObject(Vector2.Zero, graphics.GraphicsDevice, 0.5f, 5);
            
            //add all the gameobjects
            //add player
            gdr = new GameObjectDirector(playerBuilder);
            gameObjects.Add(gdr.Construct());

            //add one enemy


            gameObjects.Add(enemyPool.Create(new Vector2(100,100),graphics.GraphicsDevice,0,5 ));

            //GameObject player = new GameObject(new Vector2(0,0),graphics.GraphicsDevice );
            //player.AddComponent(new SpriteRenderer(player, "HeroSheet", 0));
            //player.AddComponent(new Animator(player,5));
            //player.AddComponent(new Player(player, 10));
            //gameObjects.Add(player);
            //loads all the gameobjects

            foreach (var gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            GameFont = Content.Load<SpriteFont>("GameFont");


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private float removeenemy;
        bool toggle = false;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            removeenemy += deltaTime;
            KeyboardState keyState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here

            //test start
            if (removeenemy > 10)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].GetComponent("Enemy") != null)
                    {
                        enemyPool.ReleaseObject(gameObjects[i]);
                        gameObjectsToRemove.Add(gameObjects[i]);
                        removeenemy = -20;
                    }
                }
            }
            
            if (keyState.IsKeyDown(Keys.K) && !toggle)
            {
                gameObjectsToAdd.Add(enemyPool.Create(new Vector2(100, 100), graphics.GraphicsDevice, 0, 5));
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend);
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