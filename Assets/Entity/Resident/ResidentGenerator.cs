using System.Linq;
using UnityEngine;
using static Utils;

public class ResidentGenerator : MonoBehaviour
{
    public  GameObject[] hairPossibilities;
    public  GameObject[] bodyPossibilities;
    public  GameObject[] legPossibilities;
    public  GameObject[] feetPossibilities;
    public  GameObject[] headPossibilities;
    public  GameObject[] accessoryPossibilities;


    public GameObject hair;
    public GameObject body;
    public GameObject leg;
    public GameObject feet;
    public GameObject head;
    public GameObject accessory;

    public bool isMale;

    public void Start()
    {
        GenerateResident();
    }

    public void GenerateResident()
    {
        isMale = Random.Range(0, 2) == 0;

        GetComponent<EntityInfo>().entity.male = isMale;

        string[] lines = GetCSVLine("Assets/Entity/Resident/ResidentName.csv").ToArray();

        string[] filteredLines = lines.Where(line => line.Split(',')[1] == (isMale ? "M" : "F")).ToArray();

        string selectedLine = filteredLines[Random.Range(0, filteredLines.Length)];
        string selectedName = selectedLine.Split(',')[0];

        GetComponent<EntityInfo>().entity.entityName = selectedName;

        //GenerateBodyPart();
    }

    private void GenerateBodyPart()
    {
        GameObject hair = Instantiate(hairPossibilities[Random.Range(0, hairPossibilities.Length)]);
        hair.transform.parent = transform;

        GameObject body = Instantiate(bodyPossibilities[Random.Range(0, bodyPossibilities.Length)]);
        body.transform.parent = transform;

        GameObject leg = Instantiate(legPossibilities[Random.Range(0, legPossibilities.Length)]);
        leg.transform.parent = transform;

        GameObject feet = Instantiate(feetPossibilities[Random.Range(0, feetPossibilities.Length)]);
        feet.transform.parent = transform;

        GameObject head = Instantiate(headPossibilities[Random.Range(0, headPossibilities.Length)]);
        head.transform.parent = transform;

        GameObject accessory = Instantiate(accessoryPossibilities[Random.Range(0, accessoryPossibilities.Length)]);
        accessory.transform.parent = transform;
    }

}
