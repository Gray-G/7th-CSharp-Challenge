using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void leverButton_Click(object sender, EventArgs e)
        {
            // Has funds to pull the lever?
            if (!hasMoneyToBet())
            {
                resultLabel.Text = "You don't have enough money " +
                    "to make that bet.";
            }

            // Pull the lever, display images using random
            double winnings = 0.0;
            pullLever(out winnings);

            // Evaluate winnings and update money variable
            evaluateWinnings();
        }

        private bool hasMoneyToBet()
        {
            //Make sure money is not 0

            //Check available money against user's bet
        }

        private void pullLever(out double winnings)
        {
            //Roll new images to display
        }

        private void evaluateWinnings()
        {
            //Conditionals to evaluate slot images

            //Update user's total money
        }
    }
}