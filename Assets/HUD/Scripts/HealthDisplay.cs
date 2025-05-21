using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {

    [SerializeField] GameObject heart;

    void Update() {
        if(PlayerManager.isReady()) {
            createHearts();
        }
        RefreshHealth();
    }

    void createHearts() {
        int curAmount = this.transform.childCount;
        int maxAmount = Mathf.RoundToInt(PlayerManager.getStatHandler().getStat(Stats.MAX_HEALTH));
        
        if(curAmount > maxAmount) {
            Destroy(this.transform.GetChild(this.transform.childCount-1).gameObject);
        }

        for(int i = curAmount; i < maxAmount; i++) {
            GameObject obj = Instantiate(heart, this.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * obj.GetComponent<RectTransform>().sizeDelta.x, 0, 0);
        }
    }

    void RefreshHealth() {
        int curAmount = PlayerManager.getHealthManager().getHealth();
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).Find("Inner").gameObject.SetActive(i < curAmount);
        }
    }
}
