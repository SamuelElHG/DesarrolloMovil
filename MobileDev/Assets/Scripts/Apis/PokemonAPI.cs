using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

#region ObjetosAPI
[System.Serializable]
public class Pokemon
{
    public int id;
    public List<Ability> abilities;
    public List<Type> types;
    public Species species;
}

[System.Serializable]
public class Ability
{
    public AbilityDetails ability;
}

[System.Serializable]
public class AbilityDetails
{
    public string name;
}

[System.Serializable]
public class Type
{
    public TypeDetails type;
}

[System.Serializable]
public class TypeDetails
{
    public string name;
}

[System.Serializable]
public class Species
{
    public string url;
}

[System.Serializable]
public class Description
{
    public string flavor_text;
}
[System.Serializable]
public class FlavorText
{
    public string flavor_text;
    public Language language;
}
[System.Serializable]
public class FlavorTextEntries
{
    public List<FlavorText> flavor_text_entries;
}

[System.Serializable]
public class Language
{
    public string name;
}
#endregion
public class PokemonAPI : MonoBehaviour
{
    public string pokemonName = "pikachu";  // Puedes cambiar el nombre aquí o hacerlo dinámico

    private string api_Sprites = "https://github.com/PokeAPI/sprites/blob/master/sprites/pokemon/other/official-artwork/"; //+id.png para imagen del pokemon
    private string api_Pokemon_description = "https://pokeapi.co/api/v2/pokemon-species/"; //ApiParaLaDescripcion


    [SerializeField] private TMP_InputField textInput;

    [SerializeField] private TMP_Text displayText;

    [SerializeField] private SpriteRenderer pokemon_Art;

    [SerializeField] private TMP_Text descriptionText;

    private int pokemonID;

    public void BuscarPokemon()
    {
        StartCoroutine(GetPokemonData(textInput.text, displayText));
    }

    IEnumerator GetPokemonData(string pokemonName, TMP_Text resultText)
    {
        // URL de la PokeAPI para obtener los datos de un Pokémon específico
        string url = "https://pokeapi.co/api/v2/pokemon/" + pokemonName.ToLower();

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            resultText.text = "";
            string json = request.downloadHandler.text;

            // Deserializar los datos del Pokémon
            Pokemon pokemon = JsonUtility.FromJson<Pokemon>(json);

            // Mostrar el ID del Pokémon
            pokemonID = pokemon.id;

            // Obtener las habilidades del Pokémon
            resultText.text += "Skills: \n";
            foreach (var ability in pokemon.abilities)
            {
                resultText.text += "- " + ability.ability.name + "\n";
            }

            // Obtener los tipos del Pokémon
            resultText.text += "Types: \n";
            foreach (var type in pokemon.types)
            {
                resultText.text += "- " + type.type.name + "\n";
            }

            // Obtener la descripción del Pokémon (requiere otra solicitud a la API para obtener los detalles de la especie)
            StartCoroutine(GetPokemonDescription(pokemon.species.url, descriptionText));
            StartCoroutine(DownloadImage(api_Sprites, pokemonID, pokemon_Art));
        }
        else
        {
            Debug.LogError("Error al obtener datos del Pokémon: " + request.error);
        }
    }

    IEnumerator GetPokemonDescription(string speciesUrl, TMP_Text resultText)
    {
        UnityWebRequest request = UnityWebRequest.Get(speciesUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Species species = JsonUtility.FromJson<Species>(json);

            // Obtener la descripción desde la URL de la especie
            UnityWebRequest descriptionRequest = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon-species/" + pokemonID);
            yield return descriptionRequest.SendWebRequest();

            if (descriptionRequest.result == UnityWebRequest.Result.Success)
            {
                string descriptionText = "";
                FlavorTextEntries description = JsonUtility.FromJson<FlavorTextEntries>(descriptionRequest.downloadHandler.text);
                foreach (FlavorText flavorText in description.flavor_text_entries)
                {
                    if (flavorText.language.name == "en")
                    {
                        descriptionText = flavorText.flavor_text;
                        break;
                    }
                }
                resultText.text = descriptionText;
            }
        }
        else
        {
            Debug.LogError("Error al obtener los detalles de la especie: " + request.error);
        }
    }

    IEnumerator DownloadImage(string url, int id, SpriteRenderer pokemonPhoto)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url + id + ".png?raw=true"); //descargamos la imagen del pokemon
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError) Debug.Log(request.error);
        else
        {
            Texture2D art = ((DownloadHandlerTexture)request.downloadHandler).texture; //Creamos la textura
            Rect rect = new Rect(0, 0, art.width, art.height);
            pokemonPhoto.sprite = Sprite.Create(art, rect, new Vector2(0.5f, 0.5f)); //Convertimos la textura en un sprite
        }
    }
}
