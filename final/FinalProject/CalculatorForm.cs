
using System.Drawing;


public class CalculatorForm : Form
{
    private TextBox display;
    private Calculator calculator;
    private Button[] numberButtons;
    private Button[] operationButtons;
    private Button equalsButton;
    private Button clearButton;
    private MenuStrip menuStrip;
    private ToolStripMenuItem colorMenu;

    public CalculatorForm()
    {
        calculator = new Calculator();
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Calculator";
        this.Size = new Size(300, 400);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Menu for color customization
        menuStrip = new MenuStrip();
        colorMenu = new ToolStripMenuItem("Colors");
        colorMenu.DropDownItems.Add("Default", null, (s, e) => SetColors(Color.White, Color.Black));
        colorMenu.DropDownItems.Add("Dark", null, (s, e) => SetColors(Color.DarkGray, Color.White));
        colorMenu.DropDownItems.Add("Blue", null, (s, e) => SetColors(Color.LightBlue, Color.DarkBlue));
        menuStrip.Items.Add(colorMenu);
        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);

        // Display
        display = new TextBox();
        display.Location = new Point(10, 30);
        display.Size = new Size(260, 40);
        display.Font = new Font("Arial", 16);
        display.TextAlign = HorizontalAlignment.Right;
        display.ReadOnly = true;
        this.Controls.Add(display);

        // Number buttons
        numberButtons = new Button[10];
        for (int i = 0; i < 10; i++)
        {
            numberButtons[i] = new Button();
            numberButtons[i].Text = i.ToString();
            numberButtons[i].Size = new Size(50, 50);
            numberButtons[i].Click += NumberButton_Click;
        }

        // Position number buttons
        int x = 10, y = 80;
        for (int i = 1; i <= 9; i++)
        {
            numberButtons[i].Location = new Point(x, y);
            this.Controls.Add(numberButtons[i]);
            x += 60;
            if (i % 3 == 0) { x = 10; y += 60; }
        }
        numberButtons[0].Location = new Point(10, y);
        this.Controls.Add(numberButtons[0]);

        // Operation buttons
        operationButtons = new Button[4];
        string[] ops = { "+", "-", "*", "/" };
        Operation[] operations = { new AddOperation(), new SubtractOperation(), new MultiplyOperation(), new DivideOperation() };

        for (int i = 0; i < 4; i++)
        {
            operationButtons[i] = new Button();
            operationButtons[i].Text = ops[i];
            operationButtons[i].Size = new Size(50, 50);
            operationButtons[i].Location = new Point(190, 80 + i * 60);
            operationButtons[i].Tag = operations[i]; // Store operation
            operationButtons[i].Click += OperationButton_Click;
            this.Controls.Add(operationButtons[i]);
        }

        // Equals button
        equalsButton = new Button();
        equalsButton.Text = "=";
        equalsButton.Size = new Size(50, 50);
        equalsButton.Location = new Point(130, y);
        equalsButton.Click += EqualsButton_Click;
        this.Controls.Add(equalsButton);

        // Clear button
        clearButton = new Button();
        clearButton.Text = "C";
        clearButton.Size = new Size(50, 50);
        clearButton.Location = new Point(70, y);
        clearButton.Click += ClearButton_Click;
        this.Controls.Add(clearButton);

        SetColors(Color.White, Color.Black); // Default colors
    }

    private void SetColors(Color backColor, Color foreColor)
    {
        this.BackColor = backColor;
        foreach (Control c in this.Controls)
        {
            c.BackColor = backColor;
            c.ForeColor = foreColor;
        }
        display.BackColor = Color.White; // Keep display white for readability
        display.ForeColor = Color.Black;
    }

    private void NumberButton_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        display.Text += btn.Text;
    }

    private void OperationButton_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (double.TryParse(display.Text, out double num))
        {
            calculator.EnterNumber(num);
            bool calculated = calculator.SetOperation((Operation)btn.Tag);
            if (calculated)
            {
                display.Text = calculator.GetResult().ToString();
            }
            else
            {
                display.Text = "";
            }
        }
    }

    private void EqualsButton_Click(object sender, EventArgs e)
    {
        if (double.TryParse(display.Text, out double num))
        {
            calculator.EnterNumber(num);
            double result = calculator.Calculate();
            display.Text = result.ToString();
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        calculator.Reset();
        display.Text = "";
    }
}