using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState.Add("PlayersMoney", 100);
                moneyLabel.Text = ViewState["PlayersMoney"].ToString();
                ViewState.Add("ImageNames", initializeImageNames());
            }
        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            decimal playersBet = 0;

            if (!decimal.TryParse(betTextBox.Text, out playersBet))
            {
                resultLabel.Text = "Error parsing bet.";
                return;
            }

            // Retrieve image array from ViewState
            string[] imageNames = (string[])ViewState["ImageNames"];

            // Generate three random images for display
            string[] threeSlotsImages = pullLever(imageNames);

            // Display three random images to server controls
            displayImages(threeSlotsImages);

            // Evaluate reels and store return value as multiplier of player's bet
            int multiplier = evaluateReels(threeSlotsImages);

            // Update money total and labels using multiplier
            updateResults(multiplier);
        }

        private string[] initializeImageNames()
        {
            string[] imageNames = new string[] { "~/Images/Bar.png", "~/Images/Bell.png", "~/Images/Cherry.png",
                "~/Images/Clover.png", "~/Images/Diamond.png", "~/Images/HorseShoe.png", "~/Images/Lemon.png",
                "~/Images/Orange.png", "~/Images/Plum.png", "~/Images/Seven.png", "~/Images/Strawberry.png",
                "~/Images/Watermelon.png" };

            return imageNames;
        }

        private string spinReel(string[] imageNames, Random random)
        {
            return (imageNames[random.Next(imageNames.Length)]);
        }

        private string[] pullLever(string[] imageNames)
        {
            Random random = new Random();
            string[] threeSlotsImages = new string[] { spinReel(imageNames, random), spinReel(imageNames, random), spinReel(imageNames, random) };

            return threeSlotsImages;
        }

        private void displayImages(string[] threeSlotsImages)
        {
            firstImage.ImageUrl = threeSlotsImages[0];
            secondImage.ImageUrl = threeSlotsImages[1];
            thirdImage.ImageUrl = threeSlotsImages[2];

            return;
        }

        private int evaluateReels(string[] threeSlotsImages)
        {
            // Check for Bars
            if (isBar(threeSlotsImages))
            {
                return 1;
            }

            // Check for Cherries
            int cherryCount = 0;
            countCherries(threeSlotsImages, out cherryCount);

            if (cherryCount == 1)
                return 2;
            else if (cherryCount == 2)
                return 3;
            else if (cherryCount == 3)
                return 4;

            // Check for Sevens
            if (isThreeSevens(threeSlotsImages))
                return 100;

            else return -1; // Sentinel value which could be used in error reporting of function evaluateReels()
        }

        private bool isBar(string[] threeSlotsImages)
        {
            bool isBarPresent = false;
            for (int i = 0; i < threeSlotsImages.Length; i++)
            {
                if (threeSlotsImages[i] == "~/Images/Bar.png")
                {
                    isBarPresent = true;
                    return isBarPresent;
                }
            }

            return isBarPresent;
        }

        private void countCherries(string[] threeSlotsImages, out int cherryCount)
        {
            cherryCount = 0;

            for (int i = 0; i < threeSlotsImages.Length; i++)
            {
                if (threeSlotsImages[i] == "~/Images/Cherry.png")
                    cherryCount++;
            }

            return;
        }

        private bool isThreeSevens(string[] threeSlotsImages)
        {
            int sevenCount = 0;

            for (int i = 0; i < threeSlotsImages.Length; i++)
            {
                if (threeSlotsImages[i] == "~/Images/Seven.png")
                    sevenCount++;
            }

            if (sevenCount == 3)
                return true;
            else return false;
        }

        private void updateResults(int multiplier)
        {
            // Interpret winnings
            if (multiplier == 1)
            {
                resultLabel.Text = $"Sorry, you lost {money:C}. Better luck next time.";
            }
            if (multiplier == 2 || multiplier == 3 || multiplier == 4 )
            {

            }

            // Change playersMoney
        }
    }
}