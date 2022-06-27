//Algorytm Genetyczny

using GAF;
using GAF.Operators;
using SymulowaneWyżarzanie;

List<Zadanie> lista = new List<Zadanie>();
lista.Add(new Zadanie(2, 5));
lista.Add(new Zadanie(5, 1));
lista.Add(new Zadanie(7, 3));
lista.Add(new Zadanie(6, 4));
lista.Add(new Zadanie(7, 8));
lista.Add(new Zadanie(0, 7));
lista.Add(new Zadanie(6, 3));

List<int[]> założenia = new List<int[]>();


założenia.Add(new int[] { 1, 3, 5 });
założenia.Add(new int[] { 2, 4 });
//W genomie
//0 - procesor 1
//1 - procesor 2
double CalculateFitness(Chromosome chromosome)
{
    double fitness = 1;
    if (checkConditions(chromosome))
        fitness-=(double)f(chromosome)/100;
    return fitness;
}

bool checkConditions(Chromosome chromosome)
{
    int[] t=new int[2];
    t[0] = 0;
    t[1] = 0;
    int j = 0;
    String bity = chromosome.ToBinaryString();
    foreach(int[] założenie in założenia)
    {
        for(int i = 0; i < założenie.Length; i++)
        {
            t[j] +=Convert.ToInt32(bity[założenie[i]-1])-48;
        }
        j++;
    }
    if ( t[0]==założenia[0].Length && t[1]==0 || t[0]==0 && t[1]==założenia[1].Length || t[0] == 0 && t[1] == 0 || t[0] == założenia[0].Length && t[1] == założenia[1].Length) return true; //Same 0 lub 1 
   

    return false;
}

bool TerminateFunction(Population population,
                                int currentGeneration,
                                long currentEvaluation)
{
    return currentGeneration >= 400;
}
int N = 20;
Population population = new Population(N,7);
var elite = new Elite(5);


var mutation = new SwapMutate(0.01);
var crossover = new Crossover(0.85, true)
{
    CrossoverType = CrossoverType.SinglePoint,
    ReplacementMethod = ReplacementMethod.GenerationalReplacement
};

var ga=new GeneticAlgorithm(population,CalculateFitness);
ga.Operators.Add(elite);
ga.Operators.Add(crossover);
ga.Operators.Add(mutation);
ga.OnGenerationComplete += ga_OnGenerationComplete;
ga.OnRunComplete += Ga_OnRunComplete;



ga.Run(TerminateFunction);

 void ga_OnGenerationComplete(object sender, GaEventArgs e)
{

    var chromosome = e.Population.GetTop(1)[0];
}
void Ga_OnRunComplete(object sender, GaEventArgs e)
{
    var chromosome = e.Population.GetTop(1)[0];

    Console.WriteLine("{0} f(x)={1} ", chromosome.ToBinaryString(), f(chromosome));
}
int f(Chromosome chromosome)
{
    int sum1 = 0;
    int sum2 = 0;
    String s=chromosome.ToBinaryString();
    Zadanie z;
    int j = 0;
    int b;
    int czas;
    foreach(Gene g in chromosome.Genes)
    {
        z = lista[j];
        b = Convert.ToInt32(s[j])-48;
        if (z.Czas_wg_bitu(b) == 0)
        {
            czas = z.Drugi_czas_wg_bitu(b);
            if (b == 0) b = 1;
            else b = 0;
        }
        else czas = z.Czas_wg_bitu(b);

        if(b==0)sum1 +=czas;
        else sum2 += czas;
        
        j++;
    }
    return System.Math.Max(sum1,sum2);
}