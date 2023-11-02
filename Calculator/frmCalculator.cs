using Guna.UI2.WinForms;

namespace Calculator
{
    public enum Operations
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }

    public partial class frmCalculator : Form
    {
        private Stack<Operations> OperationsStack;
        private double Buffer { get; set; } = double.NaN;
        public frmCalculator()
        {
            InitializeComponent();
            OperationsStack = new Stack<Operations>();
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            if (txtDisplay.Text == "0")
                txtDisplay.Text = "";

            var button = sender as Guna2Button;
            if (button != null)
                txtDisplay.Text += button.Text;

        }

        private void btnDecimalPoint_Click(object sender, EventArgs e)
        {
            var str = txtDisplay.Text;
            if (!str.Contains("."))
                txtDisplay.Text += ".";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisplay.Text))
                return;

            var str = txtDisplay.Text;
            str = str.Remove(str.Length - 1);
            txtDisplay.Text = str;
        }

        private void btnNegate_Click(object sender, EventArgs e)
        {
            var number = double.TryParse(txtDisplay.Text, out var result) ? result * (-1) : 0;
            txtDisplay.Text = number.ToString();
        }

        private void txtBuffer_TextChanged(object sender, EventArgs e)
        {
            if (txtBuffer.Text == "0")
                txtBuffer.Text = "";
        }

        private void PerformOperation()
        {
            if (double.IsNaN(Buffer))
            {
                Buffer = double.Parse(txtDisplay.Text);
                txtBuffer.Text = Buffer.ToString();
            }

            else if (OperationsStack.Count > 0)
            {
                var operation = OperationsStack.Pop();
                EvaluateOperation(operation);
            }
        }

        private void EvaluateOperation(Operations operation)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                switch (operation)
                {
                    case Operations.Addition:
                        Buffer += double.Parse(txtDisplay.Text);
                        break;
                    case Operations.Subtraction:
                        Buffer -= double.Parse(txtDisplay.Text);
                        break;
                    case Operations.Multiplication:
                        Buffer *= double.Parse(txtDisplay.Text);
                        break;
                    case Operations.Division:
                        Buffer /= double.Parse(txtDisplay.Text);
                        break;
                }
            }

            txtDisplay.Text = Buffer.ToString();
            txtBuffer.Text = Buffer.ToString();
        }

        private void ExecuteOperation(Operations operation)
        {
            PerformOperation();

            OperationsStack.Push(operation);
            txtDisplay.Clear();

            switch (operation)
            {
                case Operations.Addition:
                    txtBuffer.Text += " + ";
                    break;
                case Operations.Subtraction:
                    txtBuffer.Text += " - ";
                    break;
                case Operations.Multiplication:
                    txtBuffer.Text += " * ";
                    break;
                case Operations.Division:
                    txtBuffer.Text += " / ";
                    break;
                default:
                    txtBuffer.Text = ""; // Ako nema odabrane operacije
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ExecuteOperation(Operations.Addition);
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            ExecuteOperation(Operations.Subtraction);
        }

        private void btnMultiplication_Click(object sender, EventArgs e)
        {
            ExecuteOperation(Operations.Multiplication);
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            ExecuteOperation(Operations.Division);
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                if (OperationsStack.Count != 0)
                {
                    var operation = OperationsStack.Pop();
                    EvaluateOperation(operation);
                }
            }

            txtDisplay.Text = Buffer.ToString();
            txtBuffer.Text = Buffer.ToString();
            OperationsStack.Clear();
        }

        private void Clear()
        {
            txtDisplay.Text = "0";
            txtBuffer.Text = "";
            Buffer = double.NaN;
            OperationsStack.Clear();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}