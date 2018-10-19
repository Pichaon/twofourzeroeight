using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace twozerofoureight
{
    public partial class TwoZeroFourEightView : Form, View
    {
        Model model;
        Controller controller;

        public TwoZeroFourEightView()
        {
            InitializeComponent();
            model = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        public void Notify(Model m)
        {
            UpdateBoard(((TwoZeroFourEightModel)m).GetBoard());
            UpdateScore(((TwoZeroFourEightModel)m).Getscore()); 
            UpdateGameOver(((TwoZeroFourEightModel)m).CheckGameOver());
        }

        private void UpdateTile(Label l, int i)
        {
            if (i != 0)
            {
                l.Text = Convert.ToString(i);
            }
            else
            {
                l.Text = "";
            }
            switch (i)
            {
                case 0:
                    l.BackColor = Color.Gray;
                    break;
                case 2:
                    l.BackColor = Color.DarkGray;
                    break;
                case 4:
                    l.BackColor = Color.Orange;
                    break;
                case 8:
                    l.BackColor = Color.Red;
                    break;
                default:
                    l.BackColor = Color.Green;
                    break;
            }
        }
        private void UpdateBoard(int[,] board)
        {
            UpdateTile(lbl00, board[0, 0]);
            UpdateTile(lbl01, board[0, 1]);
            UpdateTile(lbl02, board[0, 2]);
            UpdateTile(lbl03, board[0, 3]);
            UpdateTile(lbl10, board[1, 0]);
            UpdateTile(lbl11, board[1, 1]);
            UpdateTile(lbl12, board[1, 2]);
            UpdateTile(lbl13, board[1, 3]);
            UpdateTile(lbl20, board[2, 0]);
            UpdateTile(lbl21, board[2, 1]);
            UpdateTile(lbl22, board[2, 2]);
            UpdateTile(lbl23, board[2, 3]);
            UpdateTile(lbl30, board[3, 0]);
            UpdateTile(lbl31, board[3, 1]);
            UpdateTile(lbl32, board[3, 2]);
            UpdateTile(lbl33, board[3, 3]);
        }
        //make key W-A-S-D can control the game movement action 
        private void TwoZeroFourEightView_KeyDown(object sender, KeyEventArgs e)
        {          
            switch (e.KeyData)
            {
                case Keys.W: //in case press buttom W to control UP action
                case Keys.Up:
                    controller.ActionPerformed(TwoZeroFourEightController.UP);
                    break;
                case Keys.S: //in case press buttom S to control DOWN action
                case Keys.Down:
                    controller.ActionPerformed(TwoZeroFourEightController.DOWN);
                    break;
                case Keys.A: //in case press buttom A to control LEFT action
                case Keys.Left:
                    controller.ActionPerformed(TwoZeroFourEightController.LEFT);
                    break;
                case Keys.D: //in case press buttom D to control RIGHT action
                case Keys.Right:
                    controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
                    break;

            }
        }
        private void UpdateScore(int score) //display the score
        { 
            lblScore.Text = Convert.ToString(score); //make the label display the score
        }
        private void UpdateGameOver(int gg) 
        { 
            if (gg==1)//if win
            {
                GameOver.Text = "YOU WIN!!!";//display the word YOU WIN!!!
                //make the button disable
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                btnLeft.Enabled = false;
                btnRight.Enabled = false;
            }
            if (gg==2)//lose
            {
                GameOver.Text = "YOU LOSE!!!"; //display the word YOU LOSE!!!
                //make the button disable
                btnUp.Enabled =  false;
                btnDown.Enabled = false;
                btnLeft.Enabled = false;
                btnRight.Enabled = false;
            }

        }
        private void btnLeft_Click(object sender, EventArgs e) //make buttom left arrow can control the action
        {
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        private void btnRight_Click(object sender, EventArgs e)//make buttom right arrow can control the action
        {
            controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
        }

        private void btnUp_Click(object sender, EventArgs e)//make buttom up arrow can control the action
        {
            controller.ActionPerformed(TwoZeroFourEightController.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)//make buttom down arrow can control the action
        {
            controller.ActionPerformed(TwoZeroFourEightController.DOWN);
        }

        private void TwoZeroFourEightView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {   //recieve the input action from the player  to make it display that action
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }
        //function to make buttom left can control the tile to go letf
        private void btnLeft_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TwoZeroFourEightView_PreviewKeyDown(sender, e);
        }
        //function to make buttom up can control the tile to go up
        private void btnUp_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TwoZeroFourEightView_PreviewKeyDown(sender, e);  
        }
        //function to make buttom right can control the tile to go right
        private void btnRight_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TwoZeroFourEightView_PreviewKeyDown(sender, e);
        }
        //function to make buttom  down can control the tile to go down
        private void btnDown_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TwoZeroFourEightView_PreviewKeyDown(sender, e);
        }
    }
}
