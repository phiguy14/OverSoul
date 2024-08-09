
public class MapSegment{
    public int originX, originY;
    //public World.Type[,] tileTypes;
    public int[,] tileValues;
    public MapSegment(int oX,int oY,int x,int y){
        //tileTypes = new World.Type[x,y];
        tileValues = new int[x,y];
        originX = oX;
        originY= oY;
    }
    public float GetY(){return originX;}
    public float GetX(){return originY;}
    /*public int GetValue(int x, int y){
        return tileValues[x,y];
    }
    public World.Type GetType(int x, int y){
        return tileTypes[x,y];
    }
    public int[,] GetAllValues(){
        return tileValues;
    }
    public World.Type[,] GetAllTypes(){
        return tileTypes;
    }*/
}
