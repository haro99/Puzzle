
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Threading;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    public UIManager UIManager;
    public GameManager GameManager;
    public GameObject RankList, RankScore;
    public InputField GetField;
    public List<string> scoresDatas;
    public bool add;

    // Start is called before the first frame update
    void Start()
    {
        //GetData();
    }

    public void AddScore()
    {
        if (!add)
        {
            // Get the root reference location of the database.
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

            //まずRankingのノードにレコードを登録(Push)して、生成されたキーを取得（これを１件のノード名に使う）
            string key = reference.Child("Ranking").Push().Key;

            int score = GameManager.Score;
            string name = GetField.text;
            if (name == "")
                name = "Guest";

            //登録する１件データをDictionaryで定義(nameとtime)
            Dictionary<string, object> itemMap = new Dictionary<string, object>();
            itemMap.Add("name", name);
            itemMap.Add("Score", score);

            //１件データをさらにDictionaryに入れる。キーはノード(bossNo/さっき生成したキー)
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("Ranking" + "/" + key, itemMap);
            //データ更新！
            reference.UpdateChildrenAsync(map);
            Debug.Log("Scoreを送りました");
            add = true;
    }

    GetData();
    }
    public async void GetData()
    {
        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //var snap = await reference.Child("Ranking").GetValueAsync();
        //IEnumerator<DataSnapshot> en = snap.Children.GetEnumerator();
        //while (en.MoveNext())
        //{
        //    DataSnapshot data = en.Current;
        //    Debug.Log("Name is " + data.Child("Score").GetValue(true));
        //}

        // Get the root reference location of the database.

        scoresDatas.Clear();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //ランキングデータ取得
        await reference.Child("Ranking").OrderByChild("Score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            { //取得失敗
                Debug.Log("取得失敗");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result; //結果取得

                Debug.Log(snapshot.ChildrenCount);
                //string json = snapshot.GetRawJsonValue();
                //Debug.Log(json);

                IEnumerator<DataSnapshot> en = snapshot.Children.GetEnumerator();//結果リストをenumeratorで処理

                while (en.MoveNext())
                {
                    DataSnapshot data = en.Current; //データ取る
                    //表示
                    Debug.Log(data.Child("name").Value + " " +  data.Child("Score").Value);
                    scoresDatas.Add(data.Child("name").Value + " " + data.Child("Score").Value);

                    //オブジェクト型ではConvert.ToInt32で変換しないと実行エラーになる
                    //System.Convert.ToInt32(data.Child("Score").Value);

                    //string name = data.Child("name").Value.ToString();
                    //int score = System.Convert.ToInt32(data.Child("Score").Value);
                    //scoresDatas.Add(name, score);
                }
            }
        });

        //Scores.Sort((a, b) => b - a);
        UIManager.SetRank(scoresDatas);
        Debug.Log($"[3] {Thread.CurrentThread.ManagedThreadId}");
    }
}
