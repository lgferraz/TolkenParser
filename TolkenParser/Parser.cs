using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TolkenParser
{
    internal class Parser
    {
        private string[] entrada;
        private TipoTolken[] tiposTolken;
        private List<tolken> tolkens = new List<tolken>();
        private tolken tk;
        private bool conjuntoTk;
        private int index;
        public Parser(string[] entrada, TipoTolken[] tiposTolken)
        {
            this.entrada = entrada;
            this.tiposTolken = tiposTolken;
            this.conjuntoTk = false;
            this.index = 0;
        }
        private bool conjunto(string tolken)
        {
            if (identificarTipo(tolken)[1] == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string[] identificarTipo(string tolken)
        {
            string[] tipoR = new string[2];
            tipoR[0] = "null";
            tipoR[1] = "null";
            foreach (TipoTolken tipo in this.tiposTolken)
            {
                if (tipo.identificadores.Contains(tolken))
                {
                    tipoR[0] = tipo.tipo;
                    tipoR[1] = tipo.conjunto.ToString();
                    return tipoR;
                }
            }
            return tipoR;
        }
        private void finalizar()
        {
            this.tolkens.Add(this.tk);
            this.index++;
        }
        private void identificar()
        {
            string[] tipoR = identificarTipo(this.entrada[this.index]);
            if (tipoR[1] == "False")
            {
                this.tk.identificador = this.entrada[this.index];
                this.tk.tipo = tipoR[0];
                finalizar();
            }
            else if (tipoR[1] == "True" && conjuntoTk == false)
            {
                this.tk.identificador = this.entrada[this.index];
                this.conjuntoTk = true;
                this.index++;
            }
            else if (tipoR[1] == "True" && conjuntoTk == true)
            {
                this.tk.identificador += this.entrada[this.index];
                this.tk.tipo = tipoR[0];
                this.conjuntoTk = false;
                finalizar();
            }
            else if (conjuntoTk)
            {
                this.tk.identificador += this.entrada[this.index];
                this.tk.tipo = tipoR[0];
                this.index++;
            }
            else
            {
                this.tk.identificador = this.entrada[this.index];
                this.tk.tipo = tipoR[0];
                finalizar();
            }
        }
        public tolken[] Parsing()
        {
            foreach(string tk in entrada)
            {
                identificar();
            }
            tolken[] tolkensF = this.tolkens.ToArray();
            return tolkensF;
        }
    }
}
