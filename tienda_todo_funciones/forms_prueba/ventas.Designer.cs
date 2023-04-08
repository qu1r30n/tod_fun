
namespace tienda_todo_funciones.forms_prueba
{
    partial class ventas
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
            this.lstb_descripcion_promo = new System.Windows.Forms.ListBox();
            this.txt_movimiento = new System.Windows.Forms.TextBox();
            this.lstb_promociones = new System.Windows.Forms.ListBox();
            this.btn_restaurar = new System.Windows.Forms.Button();
            this.lbl_modo_inventario = new System.Windows.Forms.Label();
            this.btn_guardar_venta = new System.Windows.Forms.Button();
            this.lbl_ventas_compras_resultado = new System.Windows.Forms.Label();
            this.chb_ventas_compras = new System.Windows.Forms.CheckBox();
            this.Lbl_nom_product_list = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lbl_costo_product_list = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Lbl_cuenta = new System.Windows.Forms.Label();
            this.pb_product = new System.Windows.Forms.PictureBox();
            this.Btn_elim_ultimo = new System.Windows.Forms.Button();
            this.Btn_procesar_venta = new System.Windows.Forms.Button();
            this.Btn_eliminar_todo = new System.Windows.Forms.Button();
            this.Btn_eliminar_seleccionado = new System.Windows.Forms.Button();
            this.Lst_ventas = new System.Windows.Forms.ListBox();
            this.Txt_nom_producto = new System.Windows.Forms.TextBox();
            this.Txt_buscar_producto = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajustesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provedorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestamosdeproddinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comprasabajoarribaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pb_product)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstb_descripcion_promo
            // 
            this.lstb_descripcion_promo.FormattingEnabled = true;
            this.lstb_descripcion_promo.Location = new System.Drawing.Point(642, 54);
            this.lstb_descripcion_promo.Name = "lstb_descripcion_promo";
            this.lstb_descripcion_promo.Size = new System.Drawing.Size(155, 342);
            this.lstb_descripcion_promo.TabIndex = 68;
            // 
            // txt_movimiento
            // 
            this.txt_movimiento.Location = new System.Drawing.Point(12, 417);
            this.txt_movimiento.Name = "txt_movimiento";
            this.txt_movimiento.Size = new System.Drawing.Size(526, 20);
            this.txt_movimiento.TabIndex = 67;
            // 
            // lstb_promociones
            // 
            this.lstb_promociones.FormattingEnabled = true;
            this.lstb_promociones.Location = new System.Drawing.Point(462, 54);
            this.lstb_promociones.Name = "lstb_promociones";
            this.lstb_promociones.Size = new System.Drawing.Size(174, 342);
            this.lstb_promociones.TabIndex = 66;
            // 
            // btn_restaurar
            // 
            this.btn_restaurar.Location = new System.Drawing.Point(391, 139);
            this.btn_restaurar.Name = "btn_restaurar";
            this.btn_restaurar.Size = new System.Drawing.Size(65, 23);
            this.btn_restaurar.TabIndex = 52;
            this.btn_restaurar.Text = "restaurar";
            this.btn_restaurar.UseVisualStyleBackColor = true;
            // 
            // lbl_modo_inventario
            // 
            this.lbl_modo_inventario.AutoSize = true;
            this.lbl_modo_inventario.Location = new System.Drawing.Point(14, 60);
            this.lbl_modo_inventario.Name = "lbl_modo_inventario";
            this.lbl_modo_inventario.Size = new System.Drawing.Size(10, 13);
            this.lbl_modo_inventario.TabIndex = 65;
            this.lbl_modo_inventario.Text = ".";
            // 
            // btn_guardar_venta
            // 
            this.btn_guardar_venta.Location = new System.Drawing.Point(329, 139);
            this.btn_guardar_venta.Name = "btn_guardar_venta";
            this.btn_guardar_venta.Size = new System.Drawing.Size(56, 23);
            this.btn_guardar_venta.TabIndex = 51;
            this.btn_guardar_venta.Text = "guardar";
            this.btn_guardar_venta.UseVisualStyleBackColor = true;
            // 
            // lbl_ventas_compras_resultado
            // 
            this.lbl_ventas_compras_resultado.AutoSize = true;
            this.lbl_ventas_compras_resultado.Location = new System.Drawing.Point(33, 41);
            this.lbl_ventas_compras_resultado.Name = "lbl_ventas_compras_resultado";
            this.lbl_ventas_compras_resultado.Size = new System.Drawing.Size(81, 13);
            this.lbl_ventas_compras_resultado.TabIndex = 64;
            this.lbl_ventas_compras_resultado.Text = "ventas|compras";
            this.lbl_ventas_compras_resultado.Visible = false;
            // 
            // chb_ventas_compras
            // 
            this.chb_ventas_compras.AutoSize = true;
            this.chb_ventas_compras.Checked = true;
            this.chb_ventas_compras.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_ventas_compras.Location = new System.Drawing.Point(12, 40);
            this.chb_ventas_compras.Name = "chb_ventas_compras";
            this.chb_ventas_compras.Size = new System.Drawing.Size(15, 14);
            this.chb_ventas_compras.TabIndex = 63;
            this.chb_ventas_compras.UseVisualStyleBackColor = true;
            // 
            // Lbl_nom_product_list
            // 
            this.Lbl_nom_product_list.AutoSize = true;
            this.Lbl_nom_product_list.Location = new System.Drawing.Point(9, 317);
            this.Lbl_nom_product_list.Name = "Lbl_nom_product_list";
            this.Lbl_nom_product_list.Size = new System.Drawing.Size(104, 13);
            this.Lbl_nom_product_list.TabIndex = 62;
            this.Lbl_nom_product_list.Text = "nombre del producto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 361);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "TOTAL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "codigo";
            // 
            // Lbl_costo_product_list
            // 
            this.Lbl_costo_product_list.AutoSize = true;
            this.Lbl_costo_product_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_costo_product_list.Location = new System.Drawing.Point(12, 330);
            this.Lbl_costo_product_list.Name = "Lbl_costo_product_list";
            this.Lbl_costo_product_list.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Lbl_costo_product_list.Size = new System.Drawing.Size(24, 25);
            this.Lbl_costo_product_list.TabIndex = 55;
            this.Lbl_costo_product_list.Text = "$";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 388);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "nombre";
            // 
            // Lbl_cuenta
            // 
            this.Lbl_cuenta.AutoSize = true;
            this.Lbl_cuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_cuenta.Location = new System.Drawing.Point(207, 379);
            this.Lbl_cuenta.Name = "Lbl_cuenta";
            this.Lbl_cuenta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Lbl_cuenta.Size = new System.Drawing.Size(24, 25);
            this.Lbl_cuenta.TabIndex = 56;
            this.Lbl_cuenta.Text = "$";
            // 
            // pb_product
            // 
            this.pb_product.Location = new System.Drawing.Point(329, 45);
            this.pb_product.Name = "pb_product";
            this.pb_product.Size = new System.Drawing.Size(103, 94);
            this.pb_product.TabIndex = 57;
            this.pb_product.TabStop = false;
            // 
            // Btn_elim_ultimo
            // 
            this.Btn_elim_ultimo.Location = new System.Drawing.Point(329, 249);
            this.Btn_elim_ultimo.Name = "Btn_elim_ultimo";
            this.Btn_elim_ultimo.Size = new System.Drawing.Size(75, 40);
            this.Btn_elim_ultimo.TabIndex = 48;
            this.Btn_elim_ultimo.Text = "eliminar ultimo";
            this.Btn_elim_ultimo.UseVisualStyleBackColor = true;
            // 
            // Btn_procesar_venta
            // 
            this.Btn_procesar_venta.Location = new System.Drawing.Point(329, 295);
            this.Btn_procesar_venta.Name = "Btn_procesar_venta";
            this.Btn_procesar_venta.Size = new System.Drawing.Size(75, 23);
            this.Btn_procesar_venta.TabIndex = 47;
            this.Btn_procesar_venta.Text = "procesar venta";
            this.Btn_procesar_venta.UseVisualStyleBackColor = true;
            // 
            // Btn_eliminar_todo
            // 
            this.Btn_eliminar_todo.Location = new System.Drawing.Point(329, 216);
            this.Btn_eliminar_todo.Name = "Btn_eliminar_todo";
            this.Btn_eliminar_todo.Size = new System.Drawing.Size(75, 23);
            this.Btn_eliminar_todo.TabIndex = 49;
            this.Btn_eliminar_todo.Text = "eliminar todo";
            this.Btn_eliminar_todo.UseVisualStyleBackColor = true;
            // 
            // Btn_eliminar_seleccionado
            // 
            this.Btn_eliminar_seleccionado.Location = new System.Drawing.Point(329, 168);
            this.Btn_eliminar_seleccionado.Name = "Btn_eliminar_seleccionado";
            this.Btn_eliminar_seleccionado.Size = new System.Drawing.Size(86, 42);
            this.Btn_eliminar_seleccionado.TabIndex = 50;
            this.Btn_eliminar_seleccionado.Text = "eliminar seleccionado";
            this.Btn_eliminar_seleccionado.UseVisualStyleBackColor = true;
            // 
            // Lst_ventas
            // 
            this.Lst_ventas.FormattingEnabled = true;
            this.Lst_ventas.Location = new System.Drawing.Point(12, 76);
            this.Lst_ventas.Name = "Lst_ventas";
            this.Lst_ventas.Size = new System.Drawing.Size(311, 238);
            this.Lst_ventas.TabIndex = 58;
            // 
            // Txt_nom_producto
            // 
            this.Txt_nom_producto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Txt_nom_producto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Txt_nom_producto.Location = new System.Drawing.Point(54, 384);
            this.Txt_nom_producto.Name = "Txt_nom_producto";
            this.Txt_nom_producto.Size = new System.Drawing.Size(150, 20);
            this.Txt_nom_producto.TabIndex = 53;
            // 
            // Txt_buscar_producto
            // 
            this.Txt_buscar_producto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Txt_buscar_producto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Txt_buscar_producto.Location = new System.Drawing.Point(54, 358);
            this.Txt_buscar_producto.Name = "Txt_buscar_producto";
            this.Txt_buscar_producto.Size = new System.Drawing.Size(150, 20);
            this.Txt_buscar_producto.TabIndex = 46;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem,
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 54;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajustesToolStripMenuItem});
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configuracionToolStripMenuItem.Text = "configuracion";
            // 
            // ajustesToolStripMenuItem
            // 
            this.ajustesToolStripMenuItem.Name = "ajustesToolStripMenuItem";
            this.ajustesToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.ajustesToolStripMenuItem.Text = "ajustes";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.provedorToolStripMenuItem,
            this.prestamosdeproddinToolStripMenuItem,
            this.comprasabajoarribaToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.opcionesToolStripMenuItem.Text = "opciones";
            // 
            // provedorToolStripMenuItem
            // 
            this.provedorToolStripMenuItem.Name = "provedorToolStripMenuItem";
            this.provedorToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.provedorToolStripMenuItem.Text = "provedor";
            // 
            // prestamosdeproddinToolStripMenuItem
            // 
            this.prestamosdeproddinToolStripMenuItem.Name = "prestamosdeproddinToolStripMenuItem";
            this.prestamosdeproddinToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.prestamosdeproddinToolStripMenuItem.Text = "prestamos_de_prod_din";
            // 
            // comprasabajoarribaToolStripMenuItem
            // 
            this.comprasabajoarribaToolStripMenuItem.Name = "comprasabajoarribaToolStripMenuItem";
            this.comprasabajoarribaToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.comprasabajoarribaToolStripMenuItem.Text = "compras_abajo_arriba";
            // 
            // ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstb_descripcion_promo);
            this.Controls.Add(this.txt_movimiento);
            this.Controls.Add(this.lstb_promociones);
            this.Controls.Add(this.btn_restaurar);
            this.Controls.Add(this.lbl_modo_inventario);
            this.Controls.Add(this.btn_guardar_venta);
            this.Controls.Add(this.lbl_ventas_compras_resultado);
            this.Controls.Add(this.chb_ventas_compras);
            this.Controls.Add(this.Lbl_nom_product_list);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Lbl_costo_product_list);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lbl_cuenta);
            this.Controls.Add(this.pb_product);
            this.Controls.Add(this.Btn_elim_ultimo);
            this.Controls.Add(this.Btn_procesar_venta);
            this.Controls.Add(this.Btn_eliminar_todo);
            this.Controls.Add(this.Btn_eliminar_seleccionado);
            this.Controls.Add(this.Lst_ventas);
            this.Controls.Add(this.Txt_nom_producto);
            this.Controls.Add(this.Txt_buscar_producto);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ventas";
            this.Text = "ventas";
            ((System.ComponentModel.ISupportInitialize)(this.pb_product)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstb_descripcion_promo;
        private System.Windows.Forms.TextBox txt_movimiento;
        private System.Windows.Forms.ListBox lstb_promociones;
        private System.Windows.Forms.Button btn_restaurar;
        private System.Windows.Forms.Label lbl_modo_inventario;
        private System.Windows.Forms.Button btn_guardar_venta;
        private System.Windows.Forms.Label lbl_ventas_compras_resultado;
        private System.Windows.Forms.CheckBox chb_ventas_compras;
        private System.Windows.Forms.Label Lbl_nom_product_list;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Lbl_costo_product_list;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Lbl_cuenta;
        private System.Windows.Forms.PictureBox pb_product;
        private System.Windows.Forms.Button Btn_elim_ultimo;
        private System.Windows.Forms.Button Btn_procesar_venta;
        private System.Windows.Forms.Button Btn_eliminar_todo;
        private System.Windows.Forms.Button Btn_eliminar_seleccionado;
        private System.Windows.Forms.ListBox Lst_ventas;
        private System.Windows.Forms.TextBox Txt_nom_producto;
        private System.Windows.Forms.TextBox Txt_buscar_producto;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajustesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provedorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prestamosdeproddinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comprasabajoarribaToolStripMenuItem;
    }
}