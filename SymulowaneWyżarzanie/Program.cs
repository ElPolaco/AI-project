//Symulowane wyżarzanie

using SymulowaneWyżarzanie;

bool czyMożnaWybraćPierwszy(List<Zadanie> lista,List<int[]> założenia, int i,double total_p1,double total_p2)
{
    int j= 0;
    double t = total_p2;
    int precede=1;
    foreach (int[] założenie in założenia)
    {
        if ((założenie[0] == i + 1 || założenie[założenie.Length-1]==i+1))//Jeśli pierwszy lub ostatni w parzystej iteracji
        {
            return j % 2 == 0;
        }
        if (założenie.Contains(i + 1))
        {
            foreach (int z in założenie)
            {
                if (z == (i + 1)) break;
                precede = z;
               
            }
            foreach(Zadanie z in lista)
            {
                if (z.Fx == 2)
                {
                    t -= z.Czas_p2;
                }
            }
            if (lista[precede - 1].Fx == 1 && lista[precede - 1].Czas_p1 > t) return true;
            break;
        }
        j++;
    }
    return false;
}
void symuluj(List<Zadanie> lista,List<int[]> założenia)
{
    List<int> p1 = new List<int>();
    List<int> p2 = new List<int>();
    double n = 0.91;
    double T = 1;
    double p;
    double total_p1 = 0;
    double total_p2 = 0;
    int i = 0;
    foreach (Zadanie zadanie in lista)
    {
        if (zadanie.Czas_p1 == 0)
        {
            total_p2 += zadanie.Czas_p2;
            i++;
            zadanie.Fx = 2;
            p2.Add(i);
            T = n * T;
            continue;
        }
        else if (zadanie.Czas_p2 == 0)
        {
            total_p1 += zadanie.Czas_p1;
            i++;
            zadanie.Fx = 1;
            p1.Add(i);
            T = n * T;
            continue;
        }

        if (czyMożnaWybraćPierwszy(lista, założenia, i, total_p1, total_p2))
        {
            total_p1 += zadanie.Czas_p1;
            zadanie.Fx = 1;
            p1.Add(i + 1);
        }
        else
        {
            p = Math.Exp((zadanie.Czas_p1 - zadanie.Czas_p2) / T);
            if (new Random().NextDouble() < p)
            {
                total_p2 += zadanie.Czas_p2;
                zadanie.Fx = 2;

                p2.Add(i + 1);

            }
            else
            {
                total_p1 += zadanie.Czas_p1;
                zadanie.Fx = 1;

                p1.Add(i + 1);

            }
        }
        i++;
        T = n * T;
    }
    Console.WriteLine(total_p1 + " " + total_p2);
    Console.WriteLine(Math.Max(total_p1, total_p2));
    foreach (int j in p1)
    {
        Console.WriteLine(j);
    }
}

List<Zadanie> lista= new List<Zadanie>();
lista.Add(new Zadanie(2, 5));
lista.Add(new Zadanie(5, 1));
lista.Add(new Zadanie(7, 3));
lista.Add(new Zadanie(6, 4));
lista.Add(new Zadanie(7, 8));
lista.Add(new Zadanie(0,7));
lista.Add(new Zadanie(6,3));

List<int[]> założenia = new List<int[]>();


założenia.Add(new int[] { 1,3,5});
założenia.Add(new int[] {2,4});
List<Zadanie> lista2 = new List<Zadanie>();
lista2.Add(new Zadanie(1, 2));
lista2.Add(new Zadanie(3, 7));
lista2.Add(new Zadanie(0, 5));
lista2.Add(new Zadanie(6, 0));
lista2.Add(new Zadanie(7, 8));
lista2.Add(new Zadanie(9, 10));
List<int[]> założenia2 = new List<int[]>();
założenia2.Add(new int[] { 1,3});
założenia2.Add(new int[]{2,4});

symuluj(lista, założenia);
symuluj(lista2, założenia2);
