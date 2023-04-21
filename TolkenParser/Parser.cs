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
        private List<string> pilha = new List<string>();
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
        private bool conjunto()
        {
            Console.WriteLine("Count pilha: "+this.pilha.Count.ToString());
            if (this.pilha.Count > 0)
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
            tipoR[1] = "False";
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
        private string tolkenProximo(string tolken)
        {
            foreach (TipoTolken tipo in this.tiposTolken)
            {
                if (Array.IndexOf(tipo.identificadores, tolken)+2 <= tipo.identificadores.Length && tipo.identificadores.Contains(tolken))
                {
                    Console.WriteLine("iden: " + tipo.identificadores[Array.IndexOf(tipo.identificadores, tolken)]);
                    return tipo.identificadores[Array.IndexOf(tipo.identificadores, tolken)+1];
                }
            }
            return "null";
        }
        private void finalizar()
        {
            this.tolkens.Add(this.tk);
            this.tk = new tolken();
            this.index++;
        }
        private void identificar()
        {
            string[] tipoR = identificarTipo(this.entrada[this.index]);
            this.conjuntoTk = conjunto();
            if (tipoR[1] == "False" && this.conjuntoTk == false)
            {
                this.tk.identificador = this.entrada[this.index];
                this.tk.tipo = tipoR[0];
                finalizar();
            }
            else if (tipoR[1] == "True" && this.conjuntoTk == false)
            {

                this.tk.identificador = this.entrada[this.index];
                this.pilha.Add(this.entrada[this.index]);
                this.index++;
            }
            else if (tipoR[1] == "True" && this.entrada[this.index] == tolkenProximo(this.pilha.Last()) && this.pilha.Count == 1)
            {
                this.tk.identificador += this.entrada[this.index];
                this.pilha.RemoveAt(this.pilha.Count - 1);
                this.tk.tipo = tipoR[0];
                Console.WriteLine("fechou " + tk.identificador);
                finalizar();

            }
            else if (tipoR[1] == "True" && this.entrada[this.index] == tolkenProximo(this.pilha.Last()) && this.pilha.Count > 1)
            {

                this.tk.identificador += this.entrada[this.index];
                this.pilha.RemoveAt(this.pilha.Count - 1);
                this.index++;

            }
            else if (tipoR[1] == "True")
            {
                this.tk.identificador += this.entrada[this.index];
                this.pilha.Add(this.entrada[this.index]);
                this.index++;
            }
            else
            {
                this.tk.identificador += this.entrada[this.index];
                this.index++;
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
