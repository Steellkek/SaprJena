namespace женя;

public class Alg
{
    private Vertex[] massive;
    private Vertex[] bestmassive;
    private int N;
    
    public void Go()
    {
        LocalFile lf = new();
        Graph graph = new Graph();
        graph.Matrix = lf.ReadGraph();
        N = lf.ReadRadius();
        graph.CreateGraph();
        massive = new Vertex[N];
        bestmassive = new Vertex[N];
        int ran = new Random().Next(0,graph.Vertexs.Count);
        massive[0] = graph.Vertexs[ran];
        graph.Vertexs.Remove(graph.Vertexs[ran]);
        int j = 1;
        try
        {
            while (graph.Vertexs.Count!=0)
            {
                int x = 0;
                while (massive.Last()==null)
                {
                    foreach (var vert in massive[x].GetAdjVerts())
                    {
                        if (graph.Vertexs.Contains(vert))
                        {
                            massive[j] = vert;
                            graph.Vertexs.Remove(vert);
                            j += 1;
                            break;
                        }
                    }

            
                    if (!massive[x].GetAdjVerts().Intersect(graph.Vertexs).Any())
                    {
                        x += 1;
                    }
                }
      
                for (int i = 0; i < massive.Length; i++)
                {
                    var sum = 0;
                    var b = DijkstraAlgo(graph.Matrix, massive[i].Number - 1, graph.Matrix.Count);
                    for (int k = 0; k < massive.Length; k++)
                    {
                        if (i!=k)
                        {
                            sum+= b[massive[k].Number-1];
                            massive[i].d = sum;
                        }
                    }
                }
      
                massive = massive.OrderBy(vertx => vertx.d).ToArray();
                if ((bestmassive.Last() != null))
                {
                    if (bestmassive.Min(v=>v.d)>massive.Min(v=>v.d))
                    {
                        for (int i = 0; i < massive.Length; i++)
                        {
                            bestmassive[i] = new Vertex(massive[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < massive.Length; i++)
                    {
                        bestmassive[i] = new Vertex(massive[i]);
                    }
                }
                massive[massive.Length - 1] = new Vertex(int.MaxValue);
                massive.OrderBy(v => v.Number).ToArray();
                massive[massive.Length - 1] = null;
                j = massive.Length-1;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Алгоритм зашел в тупик, для более точного ответа запустите заново.");
        }
        lf.WriteResult(N,bestmassive,graph);
    }
    private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue;
        int minIndex = 0;
 
        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }
 
        return minIndex;
    }
    public static int[] DijkstraAlgo(List<List<int>> graph, int source, int verticesCount)
    {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];
 
        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }
 
        distance[source] = 0;
 
        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;
 
            for (int v = 0; v < verticesCount; ++v)
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u][v]) && distance[u] != int.MaxValue && distance[u] + graph[u][v] < distance[v])
                    distance[v] = distance[u] + graph[u][v];
        }

        return distance;
    }

}