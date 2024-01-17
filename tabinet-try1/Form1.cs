using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using tabinet_try1.Classes;

namespace tabinet_try1
{
	public partial class Tabla : Form
	{
		
		TabinetGame Game = new TabinetGame();
		List<PictureBox> TableCardsVisual = new List<PictureBox>();
		List<PictureBox> P1CardsVisual = new List<PictureBox>();
		List<PictureBox> P2CardsVisual = new List<PictureBox>();


		Board board = new Board();


		public string ImageString(Card card)
		{
			return card.Rank + "_of_" + card.Suit + ".png";
		}

		private void UpdateInterface()
		{
			UpdateHandP1();
			UpdateHandP2();
			UpdateBoard();
			if(Game.GameIsOver == true )
			{
				DoForGameOver();
			}
		}

		public void UpdateHandP1()
		{
			foreach (PictureBox pic in P1CardsVisual)
			{
				pic.BackgroundImage = null;
				pic.BackColor = Color.Transparent;
			}
			int i = 0;
			foreach (Card card in Game.player1.Hand)
			{
				P1CardsVisual[i++].BackgroundImage = Image.FromFile("../../Images/" + ImageString(card));
			}

		}
		public void UpdateHandP2()
		{
			foreach (PictureBox pic in P2CardsVisual)
			{
				pic.BackgroundImage = null;
				pic.BackColor = Color.Transparent;
			}
			int i = 0;
			foreach (Card card in Game.player2.Hand)
			{
				P2CardsVisual[i++].BackgroundImage = Image.FromFile("../../Images/" + ImageString(card));
			}
		}

		public void UpdateBoard()
		{
			foreach (PictureBox pic in TableCardsVisual)
			{
				pic.BackgroundImage = null;
				pic.BackColor = Color.Transparent;
			}
			int i = 0;
			foreach (Card card in Game.board.Tabla)
			{
				TableCardsVisual[i++].BackgroundImage = Image.FromFile("../../Images/" + ImageString(card));
			}

		}
		public Tabla()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			TableCardsVisual.Add(tabla_card1);
			TableCardsVisual.Add(tabla_card2);
			TableCardsVisual.Add(tabla_card3);
			TableCardsVisual.Add(tabla_card4);
			TableCardsVisual.Add(tabla_card5);
			TableCardsVisual.Add(tabla_card6);
			TableCardsVisual.Add(tabla_card7);
			TableCardsVisual.Add(tabla_card8);

			P1CardsVisual.Add(p1_card1);
			P1CardsVisual.Add(p1_card2);
			P1CardsVisual.Add(p1_card3);
			P1CardsVisual.Add(p1_card4);
			P1CardsVisual.Add(p1_card5);
			P1CardsVisual.Add(p1_card6);

			P2CardsVisual.Add(p2_card1);
			P2CardsVisual.Add(p2_card2);
			P2CardsVisual.Add(p2_card3);
			P2CardsVisual.Add(p2_card4);
			P2CardsVisual.Add(p2_card5);
			P2CardsVisual.Add(p2_card6);

		}



		private void button1_Click(object sender, EventArgs e)
		{
			Game.Initialization();
			UpdateHandP1();
			UpdateHandP2();
			UpdateBoard();
			btnStart.Hide();
			picturedeck.Show();
			PutCardDown.Show();
			Acepoint.Show();


		}
		private int ct = 0;
		private void picturedeck_Click(object sender, EventArgs e)
		{

			if (ct % 3 == 0)
			{
				foreach (PictureBox picture in P1CardsVisual)
				{
					picture.Show();
				}
				ct++;
			}

			else if (ct % 3 == 1)
			{
				foreach (PictureBox picture in P2CardsVisual)
				{
					picture.Show();
				}
				ct++;
			}

			else if (ct % 3 == 2)
			{
				foreach (PictureBox picture in TableCardsVisual)
				{
					picture.Show();
				}
				ct++;
			}
		}



		private void TableCardSelected(object sender, EventArgs e)
		{
			PictureBox pic = sender as PictureBox;
			SwitchColor(sender);
			if (pic.BackgroundImage != null)
				for (int i = 0; i < TableCardsVisual.Count; i++)
				{
					if (TableCardsVisual[i] == sender as PictureBox)
					{
						Game.BoardCardSelected(i);
                        if (Game.board.Tabla[i].Rank == "ace")
                            Acepoint.Enabled = true;
                        else Acepoint.Enabled = false;
                        if (Game.VerifCardEqual())
						{
							UpdateInterface();
						}

					}
				}
		}

		private void SwitchColor(object obj)
		{
			PictureBox picture = obj as PictureBox;
			if (picture.BackColor == Color.Transparent)
			{
				picture.BackColor = Color.Purple;
			}
			else
			{
				picture.BackColor = Color.Transparent;
			}
		}

		private void PlayerCardSelect(object sender, EventArgs e)
		{
			PictureBox pic = sender as PictureBox;
			if (pic.BackgroundImage != null && Game.currentPlayer == Game.player1)
				for (int i = 0; i < P1CardsVisual.Count; i++)
				{
					P1CardsVisual[i].BackColor = Color.Transparent;
					if (P1CardsVisual[i] == sender as PictureBox)
					{
						Game.selectedCard = i;
						SwitchColor(sender);
					}
				}
			else if (pic.BackgroundImage != null)
				for (int i = 0; i < P2CardsVisual.Count; i++)
				{
					P2CardsVisual[i].BackColor = Color.Transparent;
					if (P2CardsVisual[i] == sender as PictureBox)
					{
						Game.selectedCard = i;
                        SwitchColor(sender);
					}
				}


		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			if (Game.board.Tabla.Count < 9)
			{
				Game.PutCardDown();
				UpdateInterface();
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
			//Game.currentPlayer.Hand[Game.selectedCard].AceVal = !Game.currentPlayer.Hand[Game.selectedCard].AceVal;
			Game.BoardSelectedCards.Last().AceVal = !Game.BoardSelectedCards.Last().AceVal;
			Acepoint.Text = Game.BoardSelectedCards.Last().AceVal ? "1" : "11";

        }


		private void DoForGameOver()
		{
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)control;
                    pictureBox.Visible = false;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    button.Visible = false;
                }
            }

			if (Game.player1.Points > Game.player2.Points)
			{
				GameOverText.Text = "Player 1 câștigă cu " + Game.player1.Points + " Puncte.";
			}
			else GameOverText.Text = "Player 2 câștigă cu " + Game.player2.Points + " Puncte.";
            GameOverText.Visible = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			
         
				

            
        }
    }
}
