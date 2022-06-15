using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayersTop : MonoBehaviour
{
    [SerializeField] private Text[] _nickName;
    [SerializeField] private Text[] _score;


    public void SetText(List<Player> players)
    {
        Player[] top = players
                       .OrderByDescending(p => p.Score)
                       .ToArray();

        for(int i = 0; i < 5; i++)
        {
            if (i >= top.Length || top.Length == 0) return;
            _nickName[i].text = "";
            _score[i].text = "";

            _nickName[i].text = i + 1 + ". " + top[i].photonView.Owner.NickName;
            _score[i].text = top[i].Score.ToString();
        }
    }

    public void ResetText()
    {
        for (int i = 0; i < 5; i++)
        {
            _nickName[i].text = "";
            _score[i].text = "";
        }
    }
}
