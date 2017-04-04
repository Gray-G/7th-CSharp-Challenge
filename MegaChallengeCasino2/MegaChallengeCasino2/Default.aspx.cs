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
            spinReel();
        }

        private void spinReel()
        {
            // Populate an array with the image names
            string[] imageNameArray = new string[12];
            populateImageNameArray(out imageNameArray);

            Random randomNumber = new Random();

            // Array to store the three random image names to be displayed
            string[] imageDisplayArray = new string[3];

            // Get three random images
            getThreeImages(imageNameArray, imageDisplayArray);

            // Assign image Urls to server controls
            assignImages(imageDisplayArray);
        }

        private void populateImageNameArray(out string[] imageNameArray)
        {
            imageNameArray = new string[] {"Strawberry", "Bar", "Lemon",
                "Bell", "Clover", "Cherry", "Diamond", "Orange",
                "Seven", "HorseShoe", "Plum", "Watermelon"};
        }

        private void assignImages(string[] imageDisplayArray)
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
    }
}