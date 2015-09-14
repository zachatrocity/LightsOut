using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GRID_OFFSET = 25; // Distance from upper-left side of window
        private int GRID_LENGTH = 200; // Size in pixels of grid
        private int NUM_CELLS; // Number of cells in grid
        private int CELL_LENGTH;
        private bool[,] grid; // Stores on/off state of cells in grid
        private Random rand; // Used to generate random numbers
        public MainForm()
        {
            InitializeComponent();

            NUM_CELLS = 3;
            CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

            rand = new Random(); // Initializes random number generator
            grid = new bool[NUM_CELLS, NUM_CELLS];

            
            
            // Turn entire grid on
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate();

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    // Get proper pen and brush for on/off
                    // grid section
                    Brush brush;
                    Pen pen;
                    if (grid[r, c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White; // On
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black; // Off
                    }
                    // Determine (x,y) coord of row and col to draw rectangle
                    int x = c * CELL_LENGTH + GRID_OFFSET;
                    int y = r * CELL_LENGTH + GRID_OFFSET;
                    // Draw outline and inner rectangle
                    g.DrawRectangle(pen, x, y, CELL_LENGTH, CELL_LENGTH);
                    g.FillRectangle(brush, x + 1, y + 1, CELL_LENGTH - 1, CELL_LENGTH - 1);
                }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Make sure click was inside the grid
            if (e.X < GRID_OFFSET || e.X > CELL_LENGTH * NUM_CELLS + GRID_OFFSET ||
            e.Y < GRID_OFFSET || e.Y > CELL_LENGTH * NUM_CELLS + GRID_OFFSET)
                return;
            // Find row, col of mouse press
            int r = (e.Y - GRID_OFFSET) / CELL_LENGTH;
            int c = (e.X - GRID_OFFSET) / CELL_LENGTH;
            // Invert selected box and all surrounding boxes
            for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < NUM_CELLS && j >= 0 && j < NUM_CELLS)
                        grid[i, j] = !grid[i, j];
            // Redraw grid
            this.Invalidate();
            // Check to see if puzzle has been solved
            if (PlayerWon())
            {
                // Display winner dialog box
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            bool result = true;
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    if(grid[r, c] == true)
                    {
                        return false;
                    }

            return result;
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in sizeToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            x3ToolStripMenuItem.Checked = true;
            NUM_CELLS = 3;
            CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

            grid = new bool[NUM_CELLS, NUM_CELLS];
            // Turn entire grid on
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate();
        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in sizeToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            x4ToolStripMenuItem.Checked = true;
            NUM_CELLS = 4;
            CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

            grid = new bool[NUM_CELLS, NUM_CELLS];
            // Turn entire grid on
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate();
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in sizeToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            x5ToolStripMenuItem.Checked = true;
            NUM_CELLS = 5;
            CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

            grid = new bool[NUM_CELLS, NUM_CELLS];
            // Turn entire grid on
            // Fill grid with either white or black
            for (int r = 0; r < NUM_CELLS; r++)
                for (int c = 0; c < NUM_CELLS; c++)
                    grid[r, c] = rand.Next(2) == 1;
            // Redraw grid
            this.Invalidate();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            var form = sender as Form;

            GRID_LENGTH = Math.Min(form.Size.Height, form.Size.Width) - 100;
            redrawBoard();
        }

        private void redrawBoard()
        {
            CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

            
            // Redraw grid
            this.Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }
    }
}
