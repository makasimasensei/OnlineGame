public class PlayerManager : BaseManager
{
    UserData userData;
    public PlayerManager(GameFacade gameFacade) : base(gameFacade)
    {

    }

    public UserData UserData { get => userData; set => userData = value; }
}
