using FindingNationalityGame.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindingNationalityGame
{
    public partial class MainApplicationForm : Form
    {
        #region Constructor
        public MainApplicationForm()
        {
            InitializeComponent();

            this.pictureBoxInput.BringToFront();
            this.btnStart.BringToFront();
            this.AllowDrop = true;
            this.pictureBoxInput.Hide();
            this.pictureBoxInput.BackColor = Color.LightBlue;

            imagesDropInitialPoint = new Point(337, 0);
            imageMovePoints = imagesDropInitialPoint;

            this.timerInputImageDrop.Interval = Int32.Parse((imageDropinMiliseconds / this.Height).ToString());
            LoadImagesFromDirectory();
        }
        #endregion Constructor

        #region Local variables

        //Configurations
        private string[] imagesInputPaths;
        private int imageDropinMiliseconds = 3000;  // Miliseconds of Droping Image and disappear
        private int imageSelectedIndex = 0;
        private const int totalImagesCount = 10;
        private const int imageMoveMinPixel = 20; //20px if moved then belongs to any box

        private Point mouseClickDownLocation; // to calculate the object moving position

        private Point imagesDropInitialPoint;
        private Point imageMovePoints;

        private Point japanBoxPoint = new Point(0, 2);
        private Point chineseBoxPoint = new Point(665, 2);
        private Point koreanBoxPoint = new Point(0, 469);
        private Point thaiBoxPoint = new Point(665, 469);

        //Flags
        private bool isPictureInHand = false;
        private string selectedByImageDropBox = "";

        private float imageOpacity = 1;
        private int animationMoveIndex = 0;
        private Point[] animationMovePoints;
        #endregion

        #region Control Events
        private void pictureBoxDropInput_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var pictureBoxInputCenterPoint = Calculations.GetCenterPointOfObject(this.pictureBoxInput);
                mouseClickDownLocation = e.Location;
                isPictureInHand = true;
            }
        }

        private void pictureBoxDropInput_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBoxDropInput_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBoxInput.Left = e.X + pictureBoxInput.Left - mouseClickDownLocation.X;
                pictureBoxInput.Top = e.Y + pictureBoxInput.Top - mouseClickDownLocation.Y;

                FindNearestNationalityBox_ChangeBackground();
            }

        }

        private void timerInputImageDrop_Tick(object sender, EventArgs e)
        {
            if (imageMovePoints.Y > 400) // Image moved to the bottom of screen
            {
                if ((imageSelectedIndex + 1) >= totalImagesCount)
                {
                    GameEnd();
                }
                else
                {
                    AppearNextImage();
                }
            }
            else
            {
                imageMovePoints.Y += 2; //Image Move Steps Increments

                if (!isPictureInHand)
                {
                    this.pictureBoxInput.Location = imageMovePoints;
                }
            }
        }

        private void timerImageAnimate_Tick(object sender, EventArgs e)
        {
            if (animationMoveIndex >= animationMovePoints.Length)
            {
                timerImageAnimate.Stop();

                if (imageSelectedIndex < totalImagesCount)
                {
                    AppearNextImage();
                    timerInputImageDrop.Start();
                }
                else
                {
                    GameEnd();
                }
            }
            else
            {
                this.pictureBoxInput.Location = animationMovePoints[animationMoveIndex++];

                pictureBoxInput.Image = ImageTransformation.ChangeOpacity(Image.FromFile(imagesInputPaths[imageSelectedIndex]), imageOpacity);

                imageOpacity = imageOpacity - .05f; // Changing Opacity
            }
        }

        private void pictureBoxInput_MouseUp(object sender, MouseEventArgs e)
        {
            isPictureInHand = false;
            if (selectedByImageDropBox != "") // If imageIsInSomeBox then move picture to that box
            {
                timerInputImageDrop.Stop();

                if (Path.GetFileName(imagesInputPaths[imageSelectedIndex]).Split("_")[0] == selectedByImageDropBox) // If user selection is right
                    Calculations.TotalScore += 20; //If the user guessed correctly, it will be a positive 20 points
                else
                    Calculations.TotalScore -= 5; //If it was wrong it will be -5 points.

                SetTotalScoreToLabel();

                var pictureCenterPoint = Calculations.GetCenterPointOfObject(this.pictureBoxInput);

                switch (selectedByImageDropBox)
                {
                    case "Japan":
                        animationMovePoints = Calculations.GetPoints(this.pictureBoxInput.Location, japanBoxPoint, 30);
                        break;
                    case "Chinese":
                        animationMovePoints = Calculations.GetPoints(this.pictureBoxInput.Location, chineseBoxPoint, 30);
                        break;
                    case "Korean":
                        animationMovePoints = Calculations.GetPoints(this.pictureBoxInput.Location, koreanBoxPoint, 30);
                        break;
                    case "Thai":
                        animationMovePoints = Calculations.GetPoints(this.pictureBoxInput.Location, thaiBoxPoint, 30);
                        break;
                    default:
                        break;
                }

                animationMoveIndex = 0;

                imageOpacity = 1;
                timerImageAnimate.Start();

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            imageSelectedIndex = 0;

            Random rnd = new Random();
            imagesInputPaths = imagesInputPaths.OrderBy(x => rnd.Next()).ToArray(); // Randomize Images in Array so that it comes randomly for user

            this.pictureBoxInput.Image = new Bitmap(imagesInputPaths[imageSelectedIndex]);
            this.pictureBoxInput.Location = imagesDropInitialPoint;
            imageMovePoints = imagesDropInitialPoint;
            this.pictureBoxInput.Show();
            this.btnStart.Hide();
            this.labelDisclaimer.Hide();
            Calculations.TotalScore = 0; //Initial Score
            SetTotalScoreToLabel();
            timerInputImageDrop.Start();
        }
        #endregion

        #region Other Functions

        private void SetTotalScoreToLabel()
        {
            this.labelTotalScore.Text = "Total Score : " + Calculations.TotalScore;
        }

        private void FindNearestNationalityBox_ChangeBackground()
        {
            ResetBackground_NationalityPanels();

            var pictureBoxInputCenterPoint = Calculations.GetCenterPointOfObject(this.pictureBoxInput);

            int intialPointDifference = imagesDropInitialPoint.X - this.pictureBoxInput.Location.X;
            if (Math.Abs(intialPointDifference) > imageMoveMinPixel) //20px 
            {
                if (intialPointDifference < 0) // If Negative then Picture moved to Left side
                {
                    var distanceBetweenChineseBox = Calculations.GetDistanceBetweenPoints(chineseBoxPoint, pictureBoxInputCenterPoint);
                    var distanceBetweenThaiBox = Calculations.GetDistanceBetweenPoints(pictureBoxInputCenterPoint, thaiBoxPoint);

                    if (distanceBetweenChineseBox < distanceBetweenThaiBox)
                    {
                        this.panelChinese.BackColor = Color.LightBlue;
                        selectedByImageDropBox = "Chinese";
                    }
                    else if (distanceBetweenThaiBox < distanceBetweenChineseBox)
                    {
                        this.panelThai.BackColor = Color.LightBlue;
                        selectedByImageDropBox = "Thai";
                    }
                }
                else if (intialPointDifference >= 0) // If Negative then Picture moved to Right side
                {
                    var distanceBetweenJapanBox = Calculations.GetDistanceBetweenPoints(japanBoxPoint, pictureBoxInputCenterPoint);
                    var distanceBetweenKoreanBox = Calculations.GetDistanceBetweenPoints(koreanBoxPoint, pictureBoxInputCenterPoint);

                    if (distanceBetweenJapanBox < distanceBetweenKoreanBox)
                    {
                        this.panelJapan.BackColor = Color.LightBlue;
                        selectedByImageDropBox = "Japan";
                    }
                    else if (distanceBetweenKoreanBox < distanceBetweenJapanBox)
                    {
                        this.panelKorean.BackColor = Color.LightBlue;
                        selectedByImageDropBox = "Korean";
                    }
                }
            }

        }

        private void ResetBackground_NationalityPanels()
        {
            this.panelChinese.BackColor = Color.Transparent;
            this.panelJapan.BackColor = Color.Transparent;
            this.panelKorean.BackColor = Color.Transparent;
            this.panelThai.BackColor = Color.Transparent;

            selectedByImageDropBox = "";
        }

        private void AppearNextImage()
        {
            this.pictureBoxInput.Image = new Bitmap(imagesInputPaths[++imageSelectedIndex]);

            imageDropinMiliseconds = 3000;

            this.pictureBoxInput.Location = imagesDropInitialPoint;

            ResetBackground_NationalityPanels();

            imageMovePoints = imagesDropInitialPoint;
        }

        private void GameEnd()
        {
            ResetBackground_NationalityPanels();

            imageSelectedIndex = 0;
            this.pictureBoxInput.Hide();
            this.timerInputImageDrop.Stop();
            this.btnStart.Show();
            this.btnStart.Text = "Start Again";

            labelDisclaimer.Show();

            if (Calculations.TotalScore > 0)
                MessageBox.Show("Congragulations. You won!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("You loose. \nTry Again next time!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void LoadImagesFromDirectory()
        {
            imagesInputPaths = Directory.GetFiles(Application.StartupPath + "\\Images");

            if (imagesInputPaths.Length < totalImagesCount)
            {
                throw new Exception("Input images must be atleast 10");
            }
        }
        #endregion
    }
}
