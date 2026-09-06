using System.Collections.Generic;
using UnityEngine;

public enum EOreType
{
    Copper,
    Iron,
    Gold,
    Diamond
}

public class Ore :  MonoBehaviour,IInteract
{
    public EOreType eOreType;
    [SerializeField] private ItemStack _itemsToDrop;
    [SerializeField] [Range(1, 10)] private int _oreMaxAmountDroppable = 3;
    [SerializeField] private int _cooldownOreRecolt = 2;
    [SerializeField] private Material _oreColor;
    [SerializeField] private List<MeshRenderer> _meshRenderers;
    private List<MeshRenderer> _usedMeshRenderers =  new List<MeshRenderer>();
    private int _oreAmount;
    private int _oreToGive = 0;



    private void Awake()
    {
        GenerateOre();
    }

    private void GenerateOre()
    {
        _meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
        _oreAmount = Random.Range(1, _oreMaxAmountDroppable + 1);
        _oreToGive = _oreAmount;
        print(_oreAmount);
        int i = 0;
        while (i < _oreAmount)
        {
            int meshRendererIndex = Random.Range(0, _meshRenderers.Count);
            _meshRenderers[meshRendererIndex].enabled = true;
            _meshRenderers[meshRendererIndex].material = _oreColor;
            _usedMeshRenderers.Add(_meshRenderers[meshRendererIndex]);
            _meshRenderers.RemoveAt(meshRendererIndex);
            i++;
        }
    }

    public void Interact(ref float coolDown)
    {
        _oreAmount--;
        if (_oreAmount <= 0)
        {
            DropOre();
            Destroy(gameObject);
        }
        else
        {
            int randomedOre = Random.Range(0, _usedMeshRenderers.Count);
            _usedMeshRenderers[randomedOre].enabled = false;
        }
        coolDown = _cooldownOreRecolt;
    }

    public void AddInInventory(ItemStack[] soItemToAdd)
    {
        IInteract.AddInInventory(soItemToAdd);
    }

    private void DropOre()
    {
        AddInInventory(new [] { new ItemStack(_itemsToDrop.item, _oreToGive) });
    }
}