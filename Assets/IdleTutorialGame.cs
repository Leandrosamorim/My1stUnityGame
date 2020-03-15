using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class IdleTutorialGame : MonoBehaviour
{
    public GameObject noFunds;
    public GameObject newGame;
    public GameObject gameUi;
    public GameObject stopUi;

    public Text coinsText;
    public Text coinsPerSecText;
    public Text costPerMinText;
    public Text clickText;
    public Text productionUpgradeText;
    public Text clickUpgradeText;
    public Text costUpgradeText;
    public Text powerText;
    public Text powerUpgradeText;

    public double coins;
    public double clickValue;

    public double coinsPerSec;
    public double costPerMin;
    public double clickUpgradeCost;
    public int clickUpgradeLevel;

    public double productionUpgradeCost;
    public int productionUpgradeLevel;

    public double costUpgradeCost;
    public int costUpgradeLevel;

    public double powerUpgradeCost;
    public int powerUpgradeLevel;

    public static bool loaded;

    public void Start()
    {
        gameUi.SetActive(false);
        newGame.SetActive(true);
        noFunds.SetActive(false);
        stopUi.SetActive(false);
    }

    public void Save()
    {
        PlayerPrefs.SetString("coins", coins.ToString());
        PlayerPrefs.SetString("clickUpgradeCost", clickUpgradeCost.ToString());
        PlayerPrefs.SetString("productionUpgradeCost", productionUpgradeCost.ToString());
        PlayerPrefs.SetString("costUpgradeCost", costUpgradeCost.ToString());
        PlayerPrefs.SetString("powerUpgradeCost", powerUpgradeCost.ToString());
        PlayerPrefs.SetString("clickValue", clickValue.ToString());
        PlayerPrefs.SetString("costPerMin", costPerMin.ToString());

        PlayerPrefs.SetInt("powerUpgradeLevel", powerUpgradeLevel);
        PlayerPrefs.SetInt("costUpgradeLevel", costUpgradeLevel);
        PlayerPrefs.SetInt("productionUpgradeLevel", productionUpgradeLevel);
        PlayerPrefs.SetInt("clickUpgradeLevel", clickUpgradeLevel);
    }

    public void Load()
    {
        coins =  float.Parse(PlayerPrefs.GetString("coins", "0"));
        clickUpgradeCost = float.Parse(PlayerPrefs.GetString("clickUpgradeCost", "10"));
        productionUpgradeCost = float.Parse(PlayerPrefs.GetString("productionUpgradeCost", "25"));
        costUpgradeCost = float.Parse(PlayerPrefs.GetString("costUpgradeCost", "100"));
        powerUpgradeCost = float.Parse(PlayerPrefs.GetString("powerUpgradeCost", "1000"));
        clickValue = float.Parse(PlayerPrefs.GetString("clickValue", "1"));
        costPerMin = float.Parse(PlayerPrefs.GetString("costPerMin", "100"));
        powerUpgradeLevel = PlayerPrefs.GetInt("powerUpgradeLevel", 0);
        costUpgradeLevel = PlayerPrefs.GetInt("costUpgradeLevel", 0);
        productionUpgradeLevel = PlayerPrefs.GetInt("productionUpgradeLevel", 0);
        clickUpgradeLevel = PlayerPrefs.GetInt("clickUpgradeLevel", 0);

        gameUi.SetActive(true);
        newGame.SetActive(false);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(3);
        noFunds.SetActive(false);
    }

    public void Update()
    {
        if (!loaded)
        {
            Load();
            loaded = true;
        }

        gameUi.SetActive(true);
        coinsPerSec = productionUpgradeLevel;

        coinsText.text = "Coins: " + coins.ToString("0,0", CultureInfo.InvariantCulture);
        coinsPerSecText.text = "Coins per sec: " + coinsPerSec.ToString("0,0", CultureInfo.InvariantCulture);
        costPerMinText.text = "Cost per min: " + costPerMin.ToString("0,0", CultureInfo.InvariantCulture);
        powerText.text = "Investors: " + (powerUpgradeLevel).ToString("0,0", CultureInfo.InvariantCulture);

        clickUpgradeText.text = "Upgrade \n cost: " + clickUpgradeCost + "\n Level: " + clickUpgradeLevel + "\n effect: +1 coin per click, increases cost per min";
        productionUpgradeText.text = "Upgrade \n cost: " + productionUpgradeCost + "\n Level: " + productionUpgradeLevel + "\n effect: +1 coin per sec, increases cost per min";
        costUpgradeText.text = "Upgrade \n cost: " + costUpgradeCost + "\n Level " + costUpgradeLevel + " \n effect: reduces cost per min";
        powerUpgradeText.text = "Upgrade \n cost: " + powerUpgradeCost + "\n Level " + powerUpgradeLevel + "\n effect: more investors multiply your coins";

        coins += (coinsPerSec * Time.deltaTime) * powerUpgradeLevel;
        coins -= (costPerMin * Time.deltaTime / 60);

        Save();

    }

    public void Click()
    {
        coins += clickValue;
    }

    public void BuyClickUpgrade()
    {
        if (coins < clickUpgradeCost)
        {
            noFunds.SetActive(true);
            StartCoroutine(wait());
            
            return;
        }

        coins -= clickUpgradeCost;
        clickUpgradeCost *= 2;
        costPerMin += costPerMin * 0.1 * clickUpgradeLevel;
        clickUpgradeLevel++;
        

        clickValue++;
        clickText.text = "Click \n +" + clickValue + "coins";
    }

    public void BuyProductionUpgrade()
    {
        if (coins < productionUpgradeCost)
        {
            noFunds.SetActive(true);
            StartCoroutine(wait());

            return;
        }

        coins -= productionUpgradeCost;
        productionUpgradeCost *= 2;
        costPerMin += costPerMin * 0.1 * clickUpgradeLevel;
        productionUpgradeLevel++;

        coinsPerSec++;
    }

    public void BuyCostUpgrade()
    {
        if (coins < costUpgradeCost)
        {
            noFunds.SetActive(true);
            StartCoroutine(wait());

            return;
        }

        coins -= costUpgradeCost;
        costUpgradeCost *= 2;
        costUpgradeLevel++;

        costPerMin = costPerMin / 1.07;
    }

    public void BuyPowerUpgrade()
    {
        if (coins < powerUpgradeCost)
        {
            noFunds.SetActive(true);
            StartCoroutine(wait());

            return;
        }

        coins -= powerUpgradeCost;
        powerUpgradeCost *= 3;
        powerUpgradeLevel++;

        costPerMin += costPerMin * 0.1 * clickUpgradeLevel;
    }

}
