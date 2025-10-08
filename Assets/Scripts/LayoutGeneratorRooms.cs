using UnityEngine;

public class LayoutGeneratorRooms : MonoBehaviour
{
    [SerializeField] private int m_width = 64;
    [SerializeField] private int m_length = 64;
    [SerializeField] private int m_minRoomWidth = 3;
    [SerializeField] private int m_maxRoomWidth = 10;
    [SerializeField] private int m_minRoomLength = 3;
    [SerializeField] private int m_maxRoomLength = 10;
    [SerializeField] private GameObject m_levelLayoutDisplay;

    System.Random m_random;

    [ContextMenu("Generate Level Layout")]
    public void GenerateLevel()
    {
        m_random = new System.Random();
        var roomRect = GetStartRoomRect();
        Debug.Log(roomRect);
        DrawLayout(roomRect);
    }

    private RectInt GetStartRoomRect()
    {
        int roomWidth = m_random.Next(m_minRoomWidth, m_maxRoomWidth);
        int availableWidthX = m_width / 2 - roomWidth;
        int randomX = m_random.Next(0, availableWidthX);
        int roomX = randomX + (m_width / 4);

        int roomLength = m_random.Next(m_minRoomLength, m_maxRoomLength);
        int availableLengthY = m_length / 2 - roomLength;
        int randomY = m_random.Next(0, availableLengthY);
        int roomY = randomY + (m_length / 4);

        return new RectInt(roomX, roomY, roomWidth, roomLength);
    }

    private void DrawLayout(RectInt roomCandidateRect = new RectInt())
    {
        var renderer = m_levelLayoutDisplay.GetComponent<Renderer>();
        var layoutTexture = (Texture2D)renderer.sharedMaterial.mainTexture;

        layoutTexture.Reinitialize(m_width, m_length);
        m_levelLayoutDisplay.transform.localScale = new Vector3(m_width, m_length, 1);
        layoutTexture.FillWithColor(Color.black);
        layoutTexture.DrawRectangle(roomCandidateRect, Color.white);
        layoutTexture.SaveAsset();
    }
}
