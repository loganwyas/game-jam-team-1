using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LootLocker.Requests;
using System.Threading.Tasks;

public class Leaderboard : MonoBehaviour
{
    public int ID = 5262;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    private List<string> names;
    private List<string> scores;

    private void Start() {
        names = new List<string>();
        scores = new List<string>();
        ResetText();
        StartCoroutine(SetupRoutine());
    }

    private void Update() {
        string tempNames = "Names\n";
        string tempScores = "Scores\n";
        for (int i = 0; i < 10; i++) {
            tempNames += (i + 1).ToString() + ". " + names[i] + "\n";
            tempScores += scores[i] + "\n";
        }
        playerNames.text = tempNames;
        playerScores.text = tempScores;
    }

    IEnumerator SetupRoutine() {
        yield return LoginRoutine();
        yield return new WaitForSeconds(3f);
        yield return FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine() {
        Debug.Log("Logging in");
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) => {
            if (response.success) {
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
            }
            Debug.Log("Done logging in");
            done = true;
        });
        yield return new WaitWhile(() => done == false);
    }

    private void ResetText() {
        names.Clear();
        scores.Clear();
        for (int i = 0; i < 10; i++) {
            names.Add("");
            scores.Add("");
        }
    }

    string GetPlayerNameById(string id, int rank) {
        string printed = "";
        LootLockerSDKManager.LookupPlayerNamesByPlayerIds(new ulong[] { ulong.Parse(id) }, response =>
        {
            string name = "";
            if (response.success && response.players.Length != 0) name = response.players[0].name;
            printed = (name != "") ? name : id;
            names[rank-1] = printed;
        });
        return printed;
    }
    public IEnumerator FetchTopHighscoresRoutine() {
        ResetText();
        bool done = false;
        Debug.Log("Getting score list");
        LootLockerSDKManager.GetScoreList(ID, 10, 0, (response) => {
            if (response.success) {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                foreach (LootLockerLeaderboardMember member in members) {
                    tempPlayerNames += member.rank + ". ";
                    string id = member.member_id;
                    name = GetPlayerNameById(id, member.rank);
                    tempPlayerNames += name + "\n";
                    tempPlayerScores += member.score + "\n";
                    scores[member.rank - 1] = member.score.ToString();
                }
                done = true;
                // playerNames.text = tempPlayerNames;
                // playerScores.text = tempPlayerScores;
            }
            else {
                Debug.Log(response.text);
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void SubmitScore(int score) {
        string playerId = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerId, score, ID, (response) => {});
    }
}