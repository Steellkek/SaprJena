namespace женя;

public class Vertex
{
    public int Number;

    private List<Vertex> AdjVert = new List<Vertex>();

    public int d;
    public Vertex(int number)
    {
        Number = number;
    }

    public Vertex(Vertex vertex)
    {
        Number = vertex.Number;
        AdjVert = vertex.AdjVert;
        d = vertex.d;
    }
    public void AddAdjVert(Vertex v)
    {
        AdjVert.Add(v);
    }

    public List<Vertex> GetAdjVerts()
    {
        return AdjVert;
    }
}