namespace женя;

public class Graph
{
    public List<List<int>> Matrix;
    public List<Edge> Edges = new();
    public List<Vertex> Vertexs=new();

    public void AddVertex(int number)
    {
        Vertexs.Add(new Vertex(number));
    }


    public void AddEdge(Vertex v1, Vertex v2, int weight)
    {
        Edges.Add(new Edge(weight,v1,v2));
    }
    

    public string WriteMatrix()
    {
        var matrix = "";
        for (int i = 0; i < Matrix.Count; i++)
        {
            for (int j = 0; j < Matrix[i].Count; j++)
            {
                matrix += (Matrix[i][j]+" ");
            }
            matrix += "\n";
        }
        return matrix;
    }


    public void AddAdjVerts(Edge e)
    {
        e.V1.AddAdjVert(e.V2);
        e.V2.AddAdjVert(e.V1);
    }

    public void CreateGraph()
    {
        for (int i = 0; i < Matrix.Count; i++)
        {
            AddVertex(i+1);
        }
        for (int i = 0; i < Matrix.Count; i++)
        {
            for (int j = i+1; j < Matrix.Count; j++)
            {
                if (Matrix[i][j]!=0)
                {
                    AddEdge(Vertexs[i],Vertexs[j],Matrix[i][j]);
                }
            }
        }
        foreach (var edge in Edges)
        {
            AddAdjVerts(edge);
        }
    }
}