using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private float maxHP;
    private Slider playerSlider2D;
    public Slider playerSlider3D;
    public int health;
    public GameObject hpLine;    
    
    void Start()
    {
        playerSlider2D = GetComponent<Slider>();
        playerSlider2D.maxValue = health;
        playerSlider3D.maxValue = health;
        maxHP = health;//ó�� ü���� maxü����
        GetHP();
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider2D.value = health;
        playerSlider3D.value = playerSlider2D.value;
    }
    
    public void GetHP()
    {
        health += 100;
        float scaleX = (maxHP / 100) / (health / 100);
        hpLine.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach(Transform child in hpLine.transform)//�ڽ� ������Ʈ�� X�������� �����ؼ� ü�¹ٸ� ������.
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        hpLine.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
 