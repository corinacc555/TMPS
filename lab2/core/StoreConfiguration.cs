namespace FlorarieSOLID;

public class StoreConfiguration
{
    private static StoreConfiguration _instance;
    public string StoreName { get; private set; }

    private StoreConfiguration()
    {
        StoreName = "Florăria SOLID & Patterns";
    }

    public static StoreConfiguration Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StoreConfiguration();
            }
            return _instance;
        }
    }
}