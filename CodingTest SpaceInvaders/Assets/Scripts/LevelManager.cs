using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextAsset jsonText;
    public static LevelManager lManager;
    [System.Serializable]
    public class LevelList
    {
        public Level[] level;
    }

    public LevelList levelList = new LevelList();
    private void Awake()
    {
        lManager = this;
    }

    private void Start()
    {
        levelList = JsonUtility.FromJson<LevelList>(jsonText.text);   
    }
}
