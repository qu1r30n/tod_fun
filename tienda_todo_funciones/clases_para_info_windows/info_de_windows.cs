using System;
using System.Runtime.InteropServices;

namespace tienda_todo_funciones.clases_para_info_windows
{
    class info_de_windows
    {
        const int SPI_GETSCREENSAVERRUNNING = 114;

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(int uAction, int uParam, ref bool lpvParam, int fuWinIni);

        public bool PantallaEstaApagadaPorInactividad()
        {
            bool la_pantalla_tiene_protector_de_pantalla = false;
            if (SystemParametersInfo(SPI_GETSCREENSAVERRUNNING, 0, ref la_pantalla_tiene_protector_de_pantalla, 0))
            {
                return la_pantalla_tiene_protector_de_pantalla;
            }
            else
            {
                return false; // Si hay un error al obtener el estado, asumimos que no está apagada por inactividad
            }
        }
    }
}
