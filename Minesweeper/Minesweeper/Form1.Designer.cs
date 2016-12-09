namespace Minesweeper
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnPlayerState = new System.Windows.Forms.Button();
      this.numUpDownCols = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.numUpDownRows = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.numUpDownMines = new System.Windows.Forms.NumericUpDown();
      this.mineButtonPanel = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownCols)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownRows)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownMines)).BeginInit();
      this.SuspendLayout();
      // 
      // btnPlayerState
      // 
      this.btnPlayerState.Location = new System.Drawing.Point(12, 12);
      this.btnPlayerState.Name = "btnPlayerState";
      this.btnPlayerState.Size = new System.Drawing.Size(59, 22);
      this.btnPlayerState.TabIndex = 0;
      this.btnPlayerState.Text = "button1";
      this.btnPlayerState.UseVisualStyleBackColor = true;
      this.btnPlayerState.Click += new System.EventHandler(this.btnPlayerState_Click);
      // 
      // numUpDownCols
      // 
      this.numUpDownCols.Location = new System.Drawing.Point(196, 12);
      this.numUpDownCols.Maximum = new decimal(new int[] {
            26,
            0,
            0,
            0});
      this.numUpDownCols.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numUpDownCols.Name = "numUpDownCols";
      this.numUpDownCols.Size = new System.Drawing.Size(41, 20);
      this.numUpDownCols.TabIndex = 1;
      this.numUpDownCols.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numUpDownCols.ValueChanged += new System.EventHandler(this.numUpDownCols_ValueChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(89, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(101, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Number of columns:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(243, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(84, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Number of rows:";
      // 
      // numUpDownRows
      // 
      this.numUpDownRows.Location = new System.Drawing.Point(333, 12);
      this.numUpDownRows.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
      this.numUpDownRows.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numUpDownRows.Name = "numUpDownRows";
      this.numUpDownRows.Size = new System.Drawing.Size(41, 20);
      this.numUpDownRows.TabIndex = 3;
      this.numUpDownRows.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numUpDownRows.ValueChanged += new System.EventHandler(this.numUpDownRows_ValueChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(390, 15);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(89, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Number of mines:";
      // 
      // numUpDownMines
      // 
      this.numUpDownMines.Location = new System.Drawing.Point(485, 12);
      this.numUpDownMines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numUpDownMines.Name = "numUpDownMines";
      this.numUpDownMines.Size = new System.Drawing.Size(41, 20);
      this.numUpDownMines.TabIndex = 6;
      this.numUpDownMines.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.numUpDownMines.ValueChanged += new System.EventHandler(this.numUpDownMines_ValueChanged);
      // 
      // mineButtonPanel
      // 
      this.mineButtonPanel.Location = new System.Drawing.Point(12, 40);
      this.mineButtonPanel.Name = "mineButtonPanel";
      this.mineButtonPanel.Size = new System.Drawing.Size(200, 100);
      this.mineButtonPanel.TabIndex = 7;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(543, 269);
      this.Controls.Add(this.mineButtonPanel);
      this.Controls.Add(this.numUpDownMines);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.numUpDownRows);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.numUpDownCols);
      this.Controls.Add(this.btnPlayerState);
      this.Name = "Form1";
      this.Text = "Dillon Sweeper";
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownCols)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownRows)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numUpDownMines)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnPlayerState;
    private System.Windows.Forms.NumericUpDown numUpDownCols;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown numUpDownRows;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown numUpDownMines;
    private System.Windows.Forms.Panel mineButtonPanel;
  }
}

