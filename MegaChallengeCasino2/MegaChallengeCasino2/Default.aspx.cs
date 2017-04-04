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
            // When button is clicked, display a random image in firstImage.
            string[] imageNameArray = new string[] 
            {"Strawberry", "Bar", "Lemon",
                "Bell", "Clover", "Cherry", "Diamond", "Orange",
                "Seven", "HorseShoe", "Plum", "Watermelon"};

            Random randomNumber = new Random();

            string[] imageDisplayArray = new string[3];

            for (int i = 0; i < imageDisplayArray.Length; i++)
            {
                imageDisplayArray[i] = imageNameArray[randomNumber.Next(11)]; // 0 to 11 images
            }   

            firstImage.ImageUrl = "/Images/" + imageDisplayArray[0] + ".png";
            secondImage.ImageUrl = "/Images/" + imageDisplayArray[1] + ".png";
            thirdImage.ImageUrl = "/Images/" + imageDisplayArray[2] + ".png";
        }
    }
}