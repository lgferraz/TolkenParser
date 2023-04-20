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
            //tokenizer
            string entrada = "funcao pegar data variavel lista {aa} aaa";
            //separadores
            string[] separadores = new string[] { "=", "{", "}", "(", ")", ";", ",", "." };
            //instancia o tokenizer
            Tolkenizer obj = new Tolkenizer();
            //tolkeniza
            obj.Tokenizar(entrada, separadores);
            //coloca tolkens numa lista e mostra o resultado
            string[] saida = obj.Tolkens();

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
            tolken[] tolkens_parser = parser.Parsing();

            foreach(tolken tk in tolkens_parser)
            {
                Console.WriteLine(tk.identificador + " | " + tk.tipo);
            }

            Console.ReadKey();

        }
    }
}
