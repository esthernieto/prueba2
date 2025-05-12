using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestController : MonoBehaviour
{
    [SerializeField] private ItemListSO itemList;
    [SerializeField] private RarityListSO rarityList;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chest: ");
        Debug.Log(other.name);

        var items = Loot();
        foreach (var item in items)
        {
            // Posicionar el loot justo encima del cofre
            Vector3 spawnPosition = transform.position + Vector3.up * 1f;

            // Instanciar el objeto
            GameObject lootObject = Instantiate(item.rarity.prefab, spawnPosition, Quaternion.identity);

            // Escalar el objeto para evitar que salga gigante
            lootObject.transform.localScale = Vector3.one * 0.3f;
        }

        // Eliminar el cofre tras recoger el loot
        Destroy(gameObject);
    }

    private List<ItemSO> Loot()
    {
        // Ordenar rarezas por probabilidad
        rarityList.rarities.Sort(
            (RaritySO r, RaritySO s) => r.baseProbability.CompareTo(s.baseProbability));

        float rnd = Random.Range(0f, 100.0f);
        RaritySO lootRarity = rarityList.rarities.Last();

        foreach (var rarity in rarityList.rarities)
        {
            if (rnd < rarity.baseProbability)
            {
                lootRarity = rarity;
                break;
            }
            rnd -= rarity.baseProbability;
        }

        // Filtrar los items que coincidan con la rareza seleccionada
        var items = itemList.items
            .Where((ItemSO so) => so.rarity.name == lootRarity.name)
            .ToList();

        Debug.Log("Loot: ");
        Debug.Log(lootRarity.name);
        Debug.Log(items.First().name);

        return items;
    }
}
