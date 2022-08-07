using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciciosModulo14EX02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Criando um dicionario que recebe um chave int (opcao de voto)
            // e valor string (nome linguagem).
            Dictionary<int, string> votos = new Dictionary<int, string>();

            // Criando lista que vai receber os votos.
            List<int> votacao = new List<int>();

            // Adicionando as opções de votos no dicionário.
            votos.Add(1, "C#");
            votos.Add(2, "Java");
            votos.Add(3, "C");
            votos.Add(4, "C++");
            votos.Add(5, "Python");

            // Realizando um loop enquanto não seleciona a opcao '0'.
            // Se a opção for diferente de 0, armazena o voto na lista 'votacao'.
            int opcao;
            do
            {
                opcao = Votar(votos);
                if (opcao != 0)
                {
                    votacao.Add(opcao);
                }

            } while (opcao != 0);

            // Iniciando variaveis que armazenarão os mais votados
            // maxVoto armazena maior quantidade
            // maxLg armazena a linguagem mais votada
            int maxVoto = -1;
            string maxLg = null;

            // Imprimindo cabeçalho da tabela.
            Header();

            // Loop que passará por cada grupo de chave e valor do dicionario votos.
            // Pra cada umas das opcoes possiveis de voto, ele vai contar quantas vezes a chave (voto) aparece na lista de votacao.
            foreach (KeyValuePair<int, string> valor in votos)
            {
                // Armazenando a chave atual (opcao) e o valor atual (linguagem) em variaveis.
                int c = valor.Key;
                string s = valor.Value;

                // Chama o método 'ContarVotos'.
                // Esse método ele pega a chave atual (opcao de voto) e percorre toda a lista de votacao (chaves votadas)
                // armazenando o total de votos que aquela opcao (chave) foi votada.
                int numVotos = ContarVotos(c, votacao);

                // Se o numero de votos da chave atual for maior que o que está armazenado
                // as variveis de maxima receberao a linguagem atual (s) e o numero de votos (numVotos).
                if (numVotos > maxVoto)
                {
                    maxVoto = numVotos;
                    maxLg = s;
                }

                double porcentagem = 0;

                // Calculará o percetual de voto da linguagem/chave atual
                // dividindo pelo total de votos realizados (votacao.Count).
                if (votacao.Count > 0)
                {
                    porcentagem = (double)numVotos / votacao.Count;
                }
                else
                {
                    // Se o total de votos for 0, considera 0% para evitar divisão por 0.
                    porcentagem = 0;
                }

                // Imprimindo na tabela a linguagem atual 's'
                // O número de votos que ela recebeu
                // a a porcetagem dos votos dela sob o total dos votos.
                Console.WriteLine($"{s, -10} {numVotos,5} {porcentagem,7:P1}");

            }

            // Depois de percorrer todos os itens do dicionário,
            // vamos imprimir o total de votos realizados
            // ajustando o layout na tabela.
            Console.WriteLine(string.Concat(Enumerable.Repeat('=', 25)));
            Console.WriteLine($"{"Total",-11}{votacao.Count,5}\n");

            // De acordo com as variaveis de maximas durante o loop,
            // vamos imprimir a linguagem mais votada e quantos votos teve.
            Console.WriteLine($"A linguagem mais votada foi {maxLg} com {maxVoto} votos!\n");

        }

        // Métodos que retorna um inteiro, sendo a opcao digitada pelo usuario,
        // ele deve receber um dicionario que contem as opcaos de votos possiveis.
        // O método imprime na tela todas as opcoes possiveis consultado o dicionario informado no parametro,
        // utilizando a chave (.key) e o valor (.value) do mesmo + a opcao '0' para encerrar a votação.
        // Colocado um TRY para avisar se a opção digitada não for um INT,
        // depois é chamado o método ValidaVoto, que verifica se a opcao digitada está dentro das permitidas.
        // Enquanto ValidaVoto for FALSE, ele continua pedindo um voto valido,
        // se a opcao digitada for diferente de zero, imprime layout que o voto foi computado (VotoComputado)
        // se for '0', apenas retorna a opção.
        
        /// <summary>
        /// Inicializar um processo de votação
        /// </summary>
        /// <param name="op">Dicionário com as opções de voto</param>
        /// <returns>Retorna a opção de voto selecionada</returns>
        public static int Votar(Dictionary<int, string> op)
        {
            bool val = false;
            int opcao = -1;
            while (!val)
            {
                Console.WriteLine("Vote na sua linguagem favorita:\n");

                // Mostrando as opções de voto de acordo com o dicionario + a opcao de encerrar (0).
                foreach (KeyValuePair<int, string> k in op)
                {
                    Console.WriteLine($"{k.Key} - {k.Value}");
                }
                Console.WriteLine($"0 - Encerrar");
                Console.Write("\nDigite sua opção: ");
                
                // Try para validar se a opcao digitada foi um INT.
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {

                    Console.WriteLine("\nOpção digitada é inválida!\n");                    
                    continue;
                }
                
                // Chamando metodo para validar o voto dentro das opções possiveis.
                val = ValidaVoto(op, opcao);
            }
            // Se opção diferente de 0, imprime mensagem e retorna a opcao digitada.
            if (opcao != 0)
            {
                VotoComputado();
                return opcao;
            }
            else
            {
                // Se a opcao for zero, não imprime mensagem pois não é um voto.
                return opcao;
            }            
            
        }

        // Metodo que é chamado para validar a opcao informada pelo usuario
        // É criado Coleção de chaves com todas as chaves possiveis de voto no dicionario informado no parametro,
        // se a opcao digitada, recebida atraves do parametro 'int voto' está dentro das opções possiveis (chaves) ou a opcao digitada for 0,
        // retorna true, se não estiver dentro das opcoes possiveis, imprime na tela e retorna falso.
        // Com esse falso, o loop while no método Votar() continuará até ser digitado uma opcao aceita.
        
        /// <summary>
        /// Valida a opção informada pelo usuário
        /// </summary>
        /// <param name="opcoes">Dicionário com as opções de voto</param>
        /// <param name="voto">Opção escolhida pelo usuário</param>
        /// <returns>Retorna se o voto selecionado é válido</returns>
        public static bool ValidaVoto(Dictionary<int, string> opcoes, int voto)
        {
            Dictionary<int, string>.KeyCollection chaves = opcoes.Keys;
            if (chaves.Contains(voto) || voto == 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("\nOpção inválida!\n");
                return false;
            }

        }

        /// <summary>
        /// Imprime na tela que o voto foi computado
        /// </summary>
        public static void VotoComputado()
        {
            Console.Clear();
            Console.WriteLine(String.Concat(Enumerable.Repeat('-', 17)));
            Console.WriteLine("Voto computado!");
            Console.WriteLine(String.Concat(Enumerable.Repeat('-', 17)));
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        //Metodo que percorre uma lista, contando quantas vezes o número aparece nessa lista
        //O paramentro int 'v' é o item que vai ser contado
        //O parametro lista 'votos' é a lista onde vamos contar quantas vezes o 'v' aparece

        /// <summary>
        /// Realiza a contagem dos votos
        /// </summary>
        /// <param name="v">Opção do voto a ser contada</param>
        /// <param name="votos">Lista ocom todos os votos realizados</param>
        /// <returns></returns>
        public static int ContarVotos(int v, List<int> votos)
        {
            int cont = 0;
            foreach (int n in votos)
            {
                if (n == v)
                {
                    cont++;
                }
            }
            return cont;
        }

        /// <summary>
        /// Imprime um cabeçalho para a tabela.
        /// </summary>
        public static void Header()
        {
            Console.WriteLine($"\n{"Linguagem", -11}{"Votos", 7}{'%', 6}");
            Console.WriteLine(string.Concat(Enumerable.Repeat('=',25)));
        }
    }
}
