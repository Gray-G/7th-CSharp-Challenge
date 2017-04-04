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

        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            decimal playersBet = 0.00M;
            decimal playersWallet = 100.00M;


            string[] threeImagesOnReel = spinReel();

            // Determine the value of the pull
            evaluateReel(threeImagesOnReel, playersBet, playersWallet);

            // Set the player's new money total, persist it to ViewState
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

        private void assignImagesToControls(string[] imageDisplayArray)
        {
            // Concatenate a directory path as a string for the three random 
            // images names and assign it as the imageUrl to server controls
            firstImage.ImageUrl = "/Images/" + imageDisplayArray[0] + ".png";
            secondImage.ImageUrl = "/Images/" + imageDisplayArray[1] + ".png";
            thirdImage.ImageUrl = "/Images/" + imageDisplayArray[2] + ".png";

            return;
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
            // Check the three images for any "Bars"
            if (isBarPresent(threeImagesOnReel))
            {
                updateLabels(0, playersBet, playersWallet);
      
                return;
            }

            // Check for "Cherries"
            switch (areCherriesPresent(threeImagesOnReel))
            {
                // Evaluate cases based on number of "Cherries" present
                case 1:
                {
                    updateLabels(1, playersBet, playersWallet);
                    return;
                }
            case 2:
                {
                    updateLabels(2, playersBet, playersWallet);
                    return;
                }
            case 3:
                {
                    updateLabels(3, playersBet, playersWallet);
                    return;
                }
            }


            // Check for three "Sevens"
            if (areThreeSevensPresent(threeImagesOnReel))
            {
                updateLabels(4, playersBet, playersWallet);

                return;
            }

            // If no combos are present, default to scenario 0 (no winnings)
            else updateLabels(0, playersBet, playersWallet);

            return;
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

        private void updateLabels(int scenario, decimal playersBet, decimal playersWallet)
        {
            switch (scenario)
            {
                case 0:
                    {
                        resultLabel.Text = $"Sorry, you lost {playersBet:C}. " +
                            "Better luck next time.";

                        playersWallet -= playersBet;
                        walletLabel.Text = $"{playersWallet:C}";

                        return;
                    }
                case 1:
                    {
                        playersBet *= 2;
                        resultLabel.Text = $"You bet {playersBet /= 2:C} and " +
                            $"won {playersBet:C}!";

                        playersWallet += playersBet;
                        walletLabel.Text = $"{playersWallet:C}";

                        return;
                    }
                case 2:
                    {
                        playersBet *= 3;
                        resultLabel.Text = $"You bet {playersBet /= 3:C} and " +
                            $"won {playersBet:C}!";

                        playersWallet += playersBet;
                        walletLabel.Text = $"{playersWallet:C}";

                        return;
                    }
                case 3:
                    {
                        playersBet *= 4;
                        resultLabel.Text = $"You bet {playersBet /= 4:C} and " +
                            $"won {playersBet:C}!";

                        playersWallet += playersBet;
                        walletLabel.Text = $"{playersWallet:C}";

                        return;
                    }
                case 4:
                    {
                        playersBet *= 100;
                        resultLabel.Text = $"You bet {playersBet /= 100:C} and " +
                            $"won {playersBet:C}!";

                        playersWallet += playersBet;
                        walletLabel.Text = $"{playersWallet:C}";

                        return;
                    }
            }
        }
    }
}