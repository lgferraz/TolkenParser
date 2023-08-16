using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TolkenParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //entrada teste
            string entrada = "funcao {aa} a (a((pegardata aaa)) aaaa) variavel lista  aaa";

            //tokenizer
            //separadores
            string[] separadores = new string[] { "=", "{", "}", "(", ")", ";", ",", "." };
            //instancia o tokenizer
            Tolkenizer tolkenizer = new Tolkenizer(separadores);
            //tolkeniza
            tolkenizer.Tokenizar(entrada);
            //coloca tolkens numa lista e mostra o resultado
            string[] saida = tolkenizer.Tolkens();
            Console.Write("Tolkens: ");
            foreach (string s in saida)
            {
                Console.Write(s+" ");
            }
            //parser
            //define algumas listas de identificadores
            string[] funcoes = new string[] { "funcao", "pegardata" };
            string[] palavras_chave = new string[] { "variavel", "lista" };
            string[] blocos = new string[] { "{", "}", "(", ")" };
            //cria os tolkens com seus tipos
            TipoTolken tolkens_funcoes = new TipoTolken();
            tolkens_funcoes.identificadores = funcoes;
            tolkens_funcoes.tipo = "funcao";
            tolkens_funcoes.conjunto = false;
            TipoTolken tolkens_palavras_chave = new TipoTolken();
            tolkens_palavras_chave.identificadores = palavras_chave;
            tolkens_palavras_chave.tipo = "palavra chave";
            tolkens_palavras_chave.conjunto = false;
            TipoTolken tolkens_blocos = new TipoTolken();
            tolkens_blocos.identificadores = blocos;
            tolkens_blocos.tipo = "bloco";
            tolkens_blocos.conjunto = true;
            //cria array de tolkens
            TipoTolken[] tolkens = new TipoTolken[] { tolkens_funcoes, tolkens_palavras_chave, tolkens_blocos };
            //instancia parser
            Parser parser = new Parser(saida, tolkens);
            //faz parsing
            List<object> tolkens_parser = parser.Parsing();
            Console.WriteLine("\n///Pos parsing///");
            foreach(object tk in tolkens_parser)
            {
                if (tk is List<tolken>)
                {
                    Console.Write("bloco: ");
                    foreach (object t in (List <tolken>)tk)
                    {
                        tolken t1 = (tolken)t;
                        Console.Write(" "+t1.identificador);
                    }
                    Console.Write("\n");
                }
                else
                {
                    tolken t1 = (tolken)tk;
                    Console.WriteLine("tolken: "+t1.identificador);
                }
                    
            }

            Console.ReadKey();

        }
    }
}
