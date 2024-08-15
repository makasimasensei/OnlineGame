public class BaseManager
{
    protected GameFacade facade;

    public BaseManager(GameFacade facade) 
    {
        this.facade = facade;
    }

    public virtual void Update() { }

    public virtual void OnInit() { }
    public virtual void OnDestroy() { }
}
