using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Simple game engine that I created with the majority of the code from other programs. 
/// This game was completed on April 10, 2016.
/// Focuses on player that has movement, and the ability to fire at the programed enemy.
/// The goal of the enemy was to create a bot that could dodge bullets incoming from the player
/// and also fire towards the player.
/// </summary>

namespace GameEngine
{
    public partial class GameScreen : UserControl
    {

        //Global Variables
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, qArrowDown, wArrowDown, spaceArrowDown;
        int direction = 0;
        int counter = 12;
        int scoreCounter = 30;
        int currentScore = 0;
        int collisionCheck;
        Image[] playerImage = new Image[] { Properties.Resources.HeroPlayer, Properties.Resources.HeroPlayerLeft };
        Image[] enemyImage = new Image[] { Properties.Resources.rsz_enemysforengine };
        Player Pl;
        Enemy En;
        List<Enemy> enemys = new List<Enemy>();
        List<Bullets> bullets = new List<Bullets>();
        Pen bulletsPen = new Pen(Color.Black);
        SolidBrush bulletsBrush = new SolidBrush(Color.DarkGoldenrod);

        //Random generator to create multiple bots over time
        Random botGenerator = new Random();

        public GameScreen()
        {

            InitializeComponent();

            

            //Player attributes 
            Pl = new Player(50, 50, 60, 5, playerImage);
            En = new Enemy(725, 425, 50, 2, enemyImage);
            
            //Starting the game timer
            gameTimer.Enabled = true;
            gameTimer.Start();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Q:
                    qArrowDown = true;
                    break;
                case Keys.W:
                    wArrowDown = true;
                    break;
                case Keys.Space:
                    spaceArrowDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Q:
                    qArrowDown = false;
                    break;
                case Keys.W:
                    wArrowDown = false;
                    break;
                case Keys.Space:
                    spaceArrowDown = false;
                    break;
                default:
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            this.Focus();
            // Start counter countdown to enable a bullet timer between each shot
            counter--;
            scoreCounter--;


            // Movement for player, also changes the direction of the image
            if (leftArrowDown)
            {
                Pl.move(Pl, "Left");
                direction = 1;
            }
            else if (rightArrowDown)
            {
                Pl.move(Pl, "Right");
                direction = 0;
            }
            if (upArrowDown)
            {
                Pl.move(Pl, "Up");
            }
            else if (downArrowDown)
            {
                Pl.move(Pl, "Down");
            }

            //Enemy movement towards player
            if (Pl.x > En.x)
            {
                En.move(En, "Right");
                collisionCheck = 1;
                if (Pl.y > En.y)
                {
                    En.move(En, "Down");
                    collisionCheck = 2;
                }
                if (Pl.y < En.y)
                {
                    En.move(En, "Up");
                    collisionCheck = 3;
                }

            }
            
            if (Pl.x < En.x)
            {
                En.move(En, "Left");
                collisionCheck = 4;

                if (Pl.y < En.y)
                {
                    En.move(En, "Up");
                    collisionCheck = 3;
                } 
                if (Pl.y > En.y)
                {
                    En.move(En, "Down");
                    collisionCheck = 2;
                }
            }

            // Bullet fire for the player
            if (spaceArrowDown &&  counter < 0)

                //Check if player is moving left
                if (direction == 0)
                {
          
                        Bullets Bs = new Bullets(Pl.x + 32, Pl.y + 35, 2, 7, "Right");
                        bullets.Add(Bs);
                        counter = 12;

                }
                //Check if player is moving right
                else if (direction == 1)
                {
                        Bullets Bs = new Bullets(Pl.x, Pl.y + 35, 2, 7, "Left");
                        bullets.Add(Bs);
                        counter = 12;
                }
                       
            //foreach bullet  b  -  b.move(b);
            foreach (Bullets Bs in bullets)
            {
                Bs.move(Bs);
            }

            //Adds score over time
            if (scoreCounter < 0)
            {
                currentScore++;
                
                scoreNumber.Text = "1" + currentScore;
                scoreCounter = 30;
                
            }

            //Check if the enemy hits the player
           foreach (Enemy En in enemys)
            {
                if (Pl.collision(Pl, En) == true)
                {
                    
                    Application.Exit();
                }
            }
            
            
              


                //if ( )
                //{
                //    Application.Exit();
                //}
                Refresh();
        }
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Drawing images of monster, player, and bullets
            e.Graphics.DrawImage(playerImage[direction], Pl.x, Pl.y, Pl.size, Pl.size);
            e.Graphics.DrawImage(enemyImage[0], En.x, En.y, En.size, En.size);

            foreach (Bullets Bs in bullets)
            {
                e.Graphics.DrawRectangle(bulletsPen, Bs.x, Bs.y, Bs.size + 13, Bs.size);
                e.Graphics.FillRectangle(bulletsBrush, Bs.x, Bs.y, Bs.size + 13, Bs.size);
            }
        }
    }
}
