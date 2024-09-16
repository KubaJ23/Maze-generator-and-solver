
namespace Maze_Generator_and_solver
{
    public partial class MazesMenuForm : Form
    {
        Graphics g;
        public MazesMenuForm()
        {
            InitializeComponent();
        }

        private void maze2D_btn_Click(object sender, EventArgs e)
        {
            LoadRectangularMazesForm();
        }

        private void LoadRectangularMazesForm()
        {
            this.Hide();
            Mazes2DForm maze2DForm = new Mazes2DForm();
            maze2DForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MazeForm_FormClosing);
            maze2DForm.Show();
        }
        private void MazeForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            this.Show();
        }
        private void mazeCircular_btn_Click(object sender, EventArgs e)
        {
            LoadCircularMazesForm();
        }
        private void LoadCircularMazesForm()
        {
            this.Hide();
            MazesCircularForm mazeCircularForm = new MazesCircularForm();
            mazeCircularForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MazeForm_FormClosing);
            mazeCircularForm.Show();
        }
        private void maze3Dsurface_btn_Click(object sender, EventArgs e)
        {
            Load3DMazesForm();
        }
        private void Load3DMazesForm()
        {
            this.Hide();
            Maze3DForm maze3dForm = new Maze3DForm();
            maze3dForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MazeForm_FormClosing);
            maze3dForm.Show();
        }

        private void help_btn_Click(object sender, EventArgs e)
        {
            string helpMessage =
                "-> Hello, welcome to my maze Generator and solver\n" +
                "-> There are 3 Different mazes available, they are a 2D rectangular and circular maze and a 3D surface maze\n" +
                "-> In the 2D rectangular maze you have the most control over the maze and you have many options to change the way that the maze is generated and solved\n" +
                "-> All of the maze choices have buttons that allow you to generate a new maze, solve the current maze and to save the maze to the maze file\n" +
                "-> The saved mazes can be found at: Maze generator & solver > bin > Debug > net6.0-windows > mazeimage\n" +
                "-> You can solve the 2D Rectangular maze and the 3D maze by using WASD\n" +
                "-> When solving the 3D maze you can only see the face of the 3D cube that your player is currently on\n" +
                "-> You can view the solutions to the mazes by pressing the solve maze button (after pressing this button you can no longer complete the maze as you have seen the answer!)\n" + 
                "-> When solving the 2D rectangular maze, you can press CAPSLOCK to toggle whether to hide or unhide the maze";
            MessageBox.Show(helpMessage);
        }
    }
}