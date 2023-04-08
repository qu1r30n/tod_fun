using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tienda_todo_funciones.modelos;

namespace tienda_todo_funciones.clases
{
    class texto_ejec
    {
        variables_glob_conf var_glob = new variables_glob_conf();
        string[] G_caracter_separacion = variables_glob_conf.GG_caracter_separacion;
        public void tex_proc(string[] text_procesos)
        {
            
            

            mod_comp_vent mod = new mod_comp_vent();

            for (int i = 0; i < text_procesos.Length; i++)
            {
                string[] elementos_del_proceso = text_procesos[i].Split(Convert.ToChar(G_caracter_separacion[0]));
                
                switch (elementos_del_proceso[0])
                {
                    case "1":
                        //proceso_0|cod_0¬cantidad_0°cod_1¬cantidad_1|indice_descuento_2
                        string[] ventas = elementos_del_proceso[1].Split(Convert.ToChar(G_caracter_separacion[1]));
                        string[] codigos = new string[ventas.Length];
                        string[] cantidades = new string[ventas.Length];
                        for (int j = 0; j < ventas.Length; j++)
                        {
                            string[] elementos_ventas = ventas[j].Split(Convert.ToChar(G_caracter_separacion[2]));
                            codigos[j] = elementos_ventas[0];
                            cantidades[j] = elementos_ventas[1];
                        }
                        mod.modelo_venta(codigos, cantidades, elementos_del_proceso[2], G_caracter_separacion[1]);
                        break;


                    default:
                        break;
                }
            }
        }



    }
}
