using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Display Reel values
                string[] reels = spinReel();
                assignImagesToControls(reels);
                ViewState.Add("PlayersWallet", 100);
            }
        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            decimal playersWallet = 100.00M;
            decimal playersBet = 0.00M;

            if (!decimal.TryParse(betTextBox.Text, out playersBet))
                return;

            string[] threeImagesOnReel = spinReel();

            // Determine the value of the pull
            evaluateReel(threeImagesOnReel, playersBet, playersWallet);
        }

        private string[] spinReel()
        {
            // Populate an array with the twelve image names
            string[] imageNameArray = new string[12];
            populateImageNameArray(out imageNameArray);

            Random randomNumber = new Random();

            // Array to store the three random image names to be displayed
            string[] imageDisplayArray = new string[3];

            // Get three random images
            getThreeImages(imageNameArray, imageDisplayArray);

            // Assign image Urls to server controls
            assignImagesToControls(imageDisplayArray);

            return imageDisplayArray;
        }

        private void populateImageNameArray(out string[] imageNameArray)
        {
            imageNameArray = new string[] {"Strawberry", "Bar", "Lemon",
                "Bell", "Clover", "Cherry", "Diamond", "Orange",
                "Seven", "HorseShoe", "Plum", "Watermelon"};
        }

        private void getThreeImages(string[] imageNameArray, string[] imageDisplayArray)
        {
            Random randomNumber = new Random();

            // Select three random image names and store them in imageDisplayArray[]
            for (int i = 0; i < imageDisplayArray.Length; i++)
            {
                imageDisplayArray[i] = imageNameArray[randomNumber.Next(11)]; // 0 to 11 images
            }
        }

        private void evaluateReel(string[] threeImagesOnReel, decimal playersBet, decimal playersWallet)
        {
            // Multiplier changes depending on reel evaluation
            int multiplier = 1;

            if (isBarPresent(threeImagesOnReel))
                updateLabels(multiplier, playersBet);

            // Check for "Cherries"
            else if (evaluateForCherries(threeImagesOnReel, playersBet, playersWallet, multiplier))
                return;

            // Check for three "Sevens"
            else if (evaluateForSevens(threeImagesOnReel, playersBet, playersWallet, multiplier))
                return;

            // If no combos are present, leave multiplier = 1 and call updateLabels()
            else updateLabels(multiplier, playersBet);

            return;
        } 

        private bool evaluateForCherries(string[] threeImagesOnReel, decimal playersBet, decimal playersWallet, int multiplier)
        {
            switch (areCherriesPresent(threeImagesOnReel))
            {
                // Evaluate cases based on number of "Cherries" present
                case 1:
                    {
                        multiplier = 2;
                        updateLabels(multiplier, playersBet);
                        return true;
                    }
                case 2:
                    {
                        multiplier = 3;
                        updateLabels(multiplier, playersBet);
                        return true;
                    }
                case 3:
                    {
                        multiplier = 4;
                        updateLabels(multiplier, playersBet);
                        return true;
                    }
                default:
                    return false;
            }
        }

        private bool evaluateForSevens(string[] threeImagesOnReel, decimal playersBet, decimal playersWallet, int multiplier)
        {
            if (areThreeSevensPresent(threeImagesOnReel))
            {
                multiplier = 100;
                updateLabels(multiplier, playersBet);

                return true;
            }

            return false;
        }

        private bool isBarPresent(string[] threeImagesOnReel)
        {
            bool isBarPresent = false;

            // Iterate through the three images checking for "Bars"
            for (int i = 0; i < threeImagesOnReel.Length; i++)
            {
                if (threeImagesOnReel[i] == "Bar")
                {
                    isBarPresent = true;
                    return isBarPresent;
                }
            }

            return isBarPresent;
        }

        private int areCherriesPresent(string[] threeImagesOnReel)
        {
            // Counter to track number of "Cherries"
            int cherryCount = 0;

            for (int i = 0; i < threeImagesOnReel.Length; i++)
            {
                if (threeImagesOnReel[i] == "Cherry") cherryCount++;
            }

            return cherryCount;
        }

        private bool areThreeSevensPresent(string[] threeImagesOnReel)
        {
            // Counter to track number of "Sevens"
            int sevensCount = 0;

            for (int i = 0; i < threeImagesOnReel.Length; i++)
            {
                if (threeImagesOnReel[i] == "Seven") sevensCount++;
            }

            if (sevensCount == 3) return true;
            else return false;
        }

        private void assignImagesToControls(string[] imageDisplayArray)
        {
            // Concatenate a directory path as a string for the three random 
            // images names and assign it as the imageUrl to server controls
            firstImage.ImageUrl = "/Images/" + imageDisplayArray[0] + ".png";
            secondImage.ImageUrl = "/Images/" + imageDisplayArray[1] + ".png";
            thirdImage.ImageUrl = "/Images/" + imageDisplayArray[2] + ".png";

            return;
        }

        private void updateLabels(int multiplier, decimal playersBet)
        {

            decimal playersWallet = decimal.Parse(ViewState["PlayersWallet"].ToString());

            if (multiplier == 1)
            { 
                resultLabel.Text = $"Sorry, you lost {playersBet:C}. " +
                    "Better luck next time.";

                playersWallet -= playersBet;
                walletLabel.Text = $"{playersWallet:C}";
            }
            else if (multiplier > 1)
            { 
                resultLabel.Text = $"You bet {playersBet:C} and " +
                    $"won {playersBet *= multiplier:C}!";

                playersWallet += playersBet;
                walletLabel.Text = $"{playersWallet:C}";
            } 

            ViewState["PlayersWallet"] = playersWallet;

            return;
        }        
    }
}