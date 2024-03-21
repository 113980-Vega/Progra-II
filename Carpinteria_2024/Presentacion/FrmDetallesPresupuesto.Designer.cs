namespace Carpinteria_2024.Presentacion
{
    partial class FrmDetallesPresupuesto
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
            this.TxtFecha = new System.Windows.Forms.TextBox();
            this.TxtCliente = new System.Windows.Forms.TextBox();
            this.TxtTotal = new System.Windows.Forms.TextBox();
            this.TxtDescuento = new System.Windows.Forms.TextBox();
            this.LblFecha = new System.Windows.Forms.Label();
            this.LblCliente = new System.Windows.Forms.Label();
            this.LblTotal = new System.Windows.Forms.Label();
            this.LblDescuento2 = new System.Windows.Forms.Label();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.BtnCerrar = new System.Windows.Forms.Button();
            this.TxtFechaBaja = new System.Windows.Forms.TextBox();
            this.LblFechaBaja = new System.Windows.Forms.Label();
            this.ColProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtFecha
            // 
            this.TxtFecha.Location = new System.Drawing.Point(245, 51);
            this.TxtFecha.Name = "TxtFecha";
            this.TxtFecha.Size = new System.Drawing.Size(100, 20);
            this.TxtFecha.TabIndex = 0;
            // 
            // TxtCliente
            // 
            this.TxtCliente.Location = new System.Drawing.Point(245, 101);
            this.TxtCliente.Name = "TxtCliente";
            this.TxtCliente.Size = new System.Drawing.Size(340, 20);
            this.TxtCliente.TabIndex = 1;
            // 
            // TxtTotal
            // 
            this.TxtTotal.Location = new System.Drawing.Point(245, 157);
            this.TxtTotal.Name = "TxtTotal";
            this.TxtTotal.Size = new System.Drawing.Size(100, 20);
            this.TxtTotal.TabIndex = 2;
            // 
            // TxtDescuento
            // 
            this.TxtDescuento.Location = new System.Drawing.Point(485, 157);
            this.TxtDescuento.Name = "TxtDescuento";
            this.TxtDescuento.Size = new System.Drawing.Size(100, 20);
            this.TxtDescuento.TabIndex = 3;
            // 
            // LblFecha
            // 
            this.LblFecha.AutoSize = true;
            this.LblFecha.Location = new System.Drawing.Point(157, 54);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(40, 13);
            this.LblFecha.TabIndex = 4;
            this.LblFecha.Text = "Fecha:";
            // 
            // LblCliente
            // 
            this.LblCliente.AutoSize = true;
            this.LblCliente.Location = new System.Drawing.Point(157, 101);
            this.LblCliente.Name = "LblCliente";
            this.LblCliente.Size = new System.Drawing.Size(42, 13);
            this.LblCliente.TabIndex = 5;
            this.LblCliente.Text = "Cliente:";
            // 
            // LblTotal
            // 
            this.LblTotal.AutoSize = true;
            this.LblTotal.Location = new System.Drawing.Point(157, 157);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(34, 13);
            this.LblTotal.TabIndex = 6;
            this.LblTotal.Text = "Total:";
            // 
            // LblDescuento2
            // 
            this.LblDescuento2.AutoSize = true;
            this.LblDescuento2.Location = new System.Drawing.Point(395, 157);
            this.LblDescuento2.Name = "LblDescuento2";
            this.LblDescuento2.Size = new System.Drawing.Size(76, 13);
            this.LblDescuento2.TabIndex = 7;
            this.LblDescuento2.Text = "Descuento % :";
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColProd,
            this.ColPrecio,
            this.ColCant});
            this.dgvDetalles.Location = new System.Drawing.Point(82, 212);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.Size = new System.Drawing.Size(623, 150);
            this.dgvDetalles.TabIndex = 8;
            // 
            // BtnCerrar
            // 
            this.BtnCerrar.Location = new System.Drawing.Point(348, 396);
            this.BtnCerrar.Name = "BtnCerrar";
            this.BtnCerrar.Size = new System.Drawing.Size(75, 23);
            this.BtnCerrar.TabIndex = 9;
            this.BtnCerrar.Text = "Cerrar";
            this.BtnCerrar.UseVisualStyleBackColor = true;
            // 
            // TxtFechaBaja
            // 
            this.TxtFechaBaja.Location = new System.Drawing.Point(485, 51);
            this.TxtFechaBaja.Name = "TxtFechaBaja";
            this.TxtFechaBaja.Size = new System.Drawing.Size(100, 20);
            this.TxtFechaBaja.TabIndex = 10;
            // 
            // LblFechaBaja
            // 
            this.LblFechaBaja.AutoSize = true;
            this.LblFechaBaja.Location = new System.Drawing.Point(404, 51);
            this.LblFechaBaja.Name = "LblFechaBaja";
            this.LblFechaBaja.Size = new System.Drawing.Size(67, 13);
            this.LblFechaBaja.TabIndex = 11;
            this.LblFechaBaja.Text = "Fecha Baja: ";
            // 
            // ColProd
            // 
            this.ColProd.HeaderText = "Producto";
            this.ColProd.Name = "ColProd";
            // 
            // ColPrecio
            // 
            this.ColPrecio.HeaderText = "Precio";
            this.ColPrecio.Name = "ColPrecio";
            // 
            // ColCant
            // 
            this.ColCant.HeaderText = "Cantidad";
            this.ColCant.Name = "ColCant";
            // 
            // FrmDetallesPresupuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LblFechaBaja);
            this.Controls.Add(this.TxtFechaBaja);
            this.Controls.Add(this.BtnCerrar);
            this.Controls.Add(this.dgvDetalles);
            this.Controls.Add(this.LblDescuento2);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.LblCliente);
            this.Controls.Add(this.LblFecha);
            this.Controls.Add(this.TxtDescuento);
            this.Controls.Add(this.TxtTotal);
            this.Controls.Add(this.TxtCliente);
            this.Controls.Add(this.TxtFecha);
            this.Name = "FrmDetallesPresupuesto";
            this.Text = "Detalles Presupuesto";
            this.Load += new System.EventHandler(this.FrmDetallesPresupuesto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtFecha;
        private System.Windows.Forms.TextBox TxtCliente;
        private System.Windows.Forms.TextBox TxtTotal;
        private System.Windows.Forms.TextBox TxtDescuento;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.Label LblCliente;
        private System.Windows.Forms.Label LblTotal;
        private System.Windows.Forms.Label LblDescuento2;
        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.Button BtnCerrar;
        private System.Windows.Forms.TextBox TxtFechaBaja;
        private System.Windows.Forms.Label LblFechaBaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCant;
    }
}