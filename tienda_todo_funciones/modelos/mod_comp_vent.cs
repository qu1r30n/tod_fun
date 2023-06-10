using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tienda_todo_funciones.procesos;

namespace tienda_todo_funciones.modelos
{
    class mod_comp_vent
    {
        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;

        procesamientos pr = new procesamientos();

        //-------------------------------------------------------------------------------

        public void modelo_unico(string operacion, string[] descripcion_arreglo_opcional = null, string[][] arreglos_de_entrada = null, string[] informacion_de_variables = null, string direccion_rapido = null, string texto_rapido=null)
        {
            //funciones a hacer
            //--------------------------------------------------------------------------------------------------------------
            //"crear_archivos_inicio"
            //----------------------------------------------------------------------------------------------------------------
            //"modelo_venta"
            //arreglos_de_entrada[0]=string[] codigo,
            //arreglos_de_entrada[1]=string[] cantidad,
            //informacion_de_variables[0]=string indices_descuento = "",
            //informacion_de_variables[1]=string caracter_separacion_indices = "|"
            //------------------------------------------------------------------------------------------------------
            //"modelo_compra"
            //arreglos_de_entrada[0] = string[] codigo
            //arreglos_de_entrada[1] = string[] cantidad
            //arreglos_de_entrada[2] = string[] precio
            //arreglos_de_entrada[3] = string[] impuesto_porcentage
            //informacion_de_variables[0] = bool aplicar_impuesto_a_la_compra = false
            //informacion_de_variables[1] = string descuento = "0"
            //informacion_de_variables[2] = double minimo_porcentaje_ganancia = 15
            //informacion_de_variables[3] =  double porcentaje_elevar = 20
            //-----------------------------------------------------------------------------------------------------------
            //"pasar_arreglo_a_archivo"
            //direccion_rapido
            //informacion_de_variables//este es arreglo pero se pasa todo el arreglo
            //------------------------------------------------------------------------------------------------------------
            //agregar_string_al_archivo
            //direccion_rapido = string direccion
            //texto_rapido = string texto
            //-----------------------------------------------------------------------------------------------------------
            //agregar_string_al_inventario
            //texto_rapido = string texto

            if (operacion == "crear_archivos_inicio")
            {
                pr.crear_archivos_inicio_programa();
            }
            else if (operacion == "modelo_venta")
            {
                if (arreglos_de_entrada.Length >= 2 && informacion_de_variables.Length >= 2)
                {
                    modelo_venta(arreglos_de_entrada[0], arreglos_de_entrada[1], informacion_de_variables[0], informacion_de_variables[1]);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "modelo_venta"
                }
            }
            else if (operacion == "modelo_compra")
            {
                if (arreglos_de_entrada.Length >= 4)
                {
                    modelo_compra(arreglos_de_entrada[0], arreglos_de_entrada[1], arreglos_de_entrada[2], arreglos_de_entrada[3]);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "modelo_compra"
                }
            }
            else if (operacion == "pasar_arreglo_a_archivo")
            {
                if (direccion_rapido != null && informacion_de_variables != null)
                {
                    pr.cambiar_archivo_por_arreglo(direccion_rapido, informacion_de_variables);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "pasar_arreglo_a_archivo"
                }
            }
            else if (operacion == "agregar_string_al_archivo")
            {
                if (direccion_rapido != null && texto_rapido != null)
                {
                    pr.agregar_string_archivo(direccion_rapido, texto_rapido);
                }
                else
                {
                    // Manejar el caso cuando faltan parámetros requeridos para la operación "agregar_string_al_archivo"
                }
            }
            else if (operacion == "agregar_string_al_inventario")
            {
                if (texto_rapido != null)
                {
                    pr.agregar_string_archivo(variables_glob_conf.GG_direccion_base[0], texto_rapido);
                }
                else
                {
                    // Manejar el caso cuando falta el parámetro requerido para la operación "agregar_string_al_inventario"
                }
            }
            else
            {
                // Manejar el caso cuando se proporciona una operación no válida
            }



        }







        private void modelo_venta(string[] codigo, string[] cantidad, string indices_descuento = "", string caracter_separacion_indices = "|")
        {
            string[] cantidad_a_observar = cantidad;

            string[] indices = indices_descuento.Split(Convert.ToChar(caracter_separacion_indices));

            procesamientos prc = new procesamientos();

            double acum_precio_promo = 0;
            double acum_precio_normal = 0;

            for (int i = 0; i < indices.Length; i++)
            {
                string[] promociones = prc.promociones_ventas(codigo, cantidad_a_observar);
                //nombre_promocion|codigo_barras_1¬cantidad_del_producto¬nombre_producto_1°codigo_barras_2¬cantidad_del_producto¬nombre_productp_2|precio_anterior|precio_pagar
                string promo_seleccionada =promociones[Convert.ToInt32(indices[i])];
                string[] info_promo = promo_seleccionada.Split(Convert.ToChar(G_caracter_separacion[0]));

                
                if (info_promo[3]=="1")
                {
                    acum_precio_promo = acum_precio_promo + Convert.ToDouble(info_promo[4]);
                    acum_precio_normal= acum_precio_normal + Convert.ToDouble(info_promo[3]);

                    string[] productos_de_promo = info_promo[1].Split(Convert.ToChar(G_caracter_separacion[1]));
                    for (int j = 0; j < productos_de_promo.Length; j++)
                    {
                        string[] elementos_producto_prom=productos_de_promo[j].Split(Convert.ToChar(G_caracter_separacion[1]));
                        for (int k = 0; k < codigo.Length; k++)
                        {
                            if (codigo[k]==elementos_producto_prom[0])
                            {
                                cantidad_a_observar[k] = "" + (Convert.ToDouble(cantidad_a_observar[k]) - Convert.ToDouble(elementos_producto_prom[1]));
                            }
                        }
                    }
                }
            }


            prc.proceso_venta(codigo, cantidad, acum_precio_normal - acum_precio_promo);



        }

        private void modelo_compra(string[] codigo, string[] cantidad, string[] precio, string[] impuesto_porcentage, bool aplicar_impuesto_a_la_compra = false, string descuento = "0", double minimo_porcentaje_ganancia = 15, double porcentaje_elevar = 20)
        {
            procesamientos prc = new procesamientos();

            prc.procesar_compra(codigo, cantidad, precio, impuesto_porcentage, aplicar_impuesto_a_la_compra = false, descuento = "0", minimo_porcentaje_ganancia = 15, porcentaje_elevar = 20);
        }



        //-------------------------------------------------------------------------------



    }
}
