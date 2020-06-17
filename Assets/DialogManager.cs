using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject test;

    private string _savedDialog = null;

    private string[] _honestSentencesNpc1 = { "<color=red>the shape of a man</color>", "<color=red>a gray haired person, or maybe purple</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _honestSentencesNpc2 = { "<color=red>the shape of a woman</color>", "<color=red>a pink haired person, or maybe red</color>" };
    private string[] _honestSentencesNpc3 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>", "<color=red>someone wearing a cape</color>", "<color=red>someone wearing a crown</color>" };
    private string[] _honestSentencesNpc4 = { "<color=red>the shape of a woman</color>", "<color=red>a blue haired person, or maybe green</color>", "<color=red>someone wearing a cape</color>" };
    private string[] _honestSentencesNpc5 = { "<color=red>the shape of a woman</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _honestSentencesNpc6 = { "<color=red>the shape of a man</color>", "<color=red>a blue haired person, or maybe green</color>" };
    private string[] _honestSentencesNpc7 = { "<color=red>the shape of a woman</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _honestSentencesNpc8 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _honestSentencesNpc9 = { "<color=red>the shape of a woman</color>", "<color=red>a gray haired person, or maybe purple</color>" };
    private string[] _honestSentencesNpc10 = { "<color=red>the shape of a man</color>", "<color=red>a blue haired person, or maybe green</color>", "<color=red>someone wearing a cape</color>" };
    private string[] _honestSentencesNpc11 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>" };
    private string[] _honestSentencesNpc12 = { "<color=red>the shape of a man</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a cape</color>" };
    private string[] _honestSentencesNpc13 = { "<color=red>the shape of a woman</color>", "<color=red>a gray haired person, or maybe purple</color>" };
    private string[] _honestSentencesNpc14 = { "<color=red>the shape of a woman</color>", "<color=red>a pink haired person, or maybe red</color>", "<color=red>someone wearing a crown</color>" };
    private string[] _honestSentencesNpc15 = { "<color=red>the shape of a woman</color>", "<color=red>a blond haired person</color>" };

    private string[] _lieSentencesNpc1 = { "<color=red>the shape of a woman</color>", "<color=red>a pink haired person, or maybe red</color>" };
    private string[] _lieSentencesNpc2 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>", "<color=red>someone wearing a cape</color>", "<color=red>someone wearing a crown</color>" };
    private string[] _lieSentencesNpc3 = { "<color=red>the shape of a woman</color>", "<color=red>a blue haired person, or maybe green</color>" };
    private string[] _lieSentencesNpc4 = { "<color=red>the shape of a man</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _lieSentencesNpc5 = { "<color=red>the shape of a man</color>", "<color=red>a blue haired person, or maybe green</color>", "<color=red>someone wearing a cape</color>" };
    private string[] _lieSentencesNpc6 = { "<color=red>the shape of a woman</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _lieSentencesNpc7 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>" };
    private string[] _lieSentencesNpc8 = { "<color=red>the shape of a woman</color>", "<color=red>a gray haired person, or maybe purple</color>" };
    private string[] _lieSentencesNpc9 = { "<color=red>the shape of a man</color>", "<color=red>a blue haired person, or maybe green</color>", "<color=red>someone wearing a cape</color>", "<color=red>someone wearing a red ribbon</color>" };
    private string[] _lieSentencesNpc10 = { "<color=red>the shape of a woman</color>", "<color=red>a blond haired person</color>" };
    private string[] _lieSentencesNpc11 = { "<color=red>the shape of a woman</color>", "<color=red>a dark haired person, or maybe brown</color>", "<color=red>someone wearing a a cape</color>" };
    private string[] _lieSentencesNpc12 = { "<color=red>the shape of a woman</color>", "<color=red>a gray haired person, or maybe purple</color>" };
    private string[] _lieSentencesNpc13 = { "<color=red>the shape of a man</color>", "<color=red>a pink haired person, or maybe red</color>", "<color=red>someone wearing a crown</color>" };
    private string[] _lieSentencesNpc14 = { "<color=red>the shape of a man</color>", "<color=red>a blond haired person</color>" };
    private string[] _lieSentencesNpc15 = { "<color=red>the shape of a man</color>", "<color=red>a gray haired person, or maybe purple</color>", "<color=red>someone wearing a red ribbon</color>" };

    private string[] _irrelevantSentences = { "I'm scared, please protect me !", "What are we gonna do ? This is terrible...", "Have you found the murderer yet ?", "\"Why don't we hide inside our houses ?\" Well apparently the person that built the village forgot to put the doors.", "I have a lot of ambitions and dreams, this can't end up like this ! Right ?", "I will find that criminal and make him pay !", "Have you seen my dog ? I can't flee without him... His name is Osef, he's very kind.", "Why do I feel like I experienced this before ?", "We should group up to fight back together but I can't trust anyone.. Can I even trust you ?", "Don't approach me !", "Don't come near me, I'm not joking !", "Hmm ? I have no time for your questions.", "I am sorry, I saw nothing...", "I did not saw anything, but if I do I will warn you, stay safe !", "Those are dark times...", "It happened so suddenly...", "Who are you ?", "No, I saw nothing, leave me alone !", "If I saw or heard anything suspicious ? Nothing that I can think of, I am sorry.", "Do you like jazz ?", "Everything was so peaceful...", "I wish it was just a game...", "Hey, back off ! I saw your weapon !", "Please ! Please ! Find the murderer, I can't take it anymore !", "I should buy new shoes...", "I hope your eyes are wide opened. We are counting on you.", "If I had a weapon I could take care of that murderer myself !", "It's not me, I swear !", "Don't look at me like that, do I look like a murderer to you ?", "If you are looking for the murderer, this is the wrong place.", "How can anyone do this ?.." };

    private string buildSentence(string [] array)
    {
        //return ("ok");
        int _rnd1 = UnityEngine.Random.Range(0, array.Length);
        int _rnd2 = UnityEngine.Random.Range(1, 4);

        if (_rnd2 == 1)
            return ("I definitively saw " + array[_rnd1] + " in the distance.");
        else if (_rnd2 == 2)
            return ("I think I saw " + array[_rnd1] + " near the crime scene.");
        else
            return ("I saw " + array[_rnd1] + " when it happened ! Please do something about it !");
    }

    private string firstTime()
    {
        int _rnd = UnityEngine.Random.Range(1, 3);

        if (_rnd == 1)
            return ("If I have any information about the recent murder ? Yes, ");
        else if (_rnd == 2)
            return ("If I saw or heard something suspicious ? Yeah, ");
        else
            return ("If I saw anything ? Yeah ! ");
    }

    private string honestTalk()
    {
        if (_savedDialog == null)
        {
            if (GameObject.Find("Npc1(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc1);
            else if (GameObject.Find("Npc2(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc2);
            else if (GameObject.Find("Npc3(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc3);
            else if (GameObject.Find("Npc4(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc4);
            else if (GameObject.Find("Npc5(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc5);
            else if (GameObject.Find("Npc6(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc6);
            else if (GameObject.Find("Npc7(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc7);
            else if (GameObject.Find("Npc8(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc8);
            else if (GameObject.Find("Npc9(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc9);
            else if (GameObject.Find("Npc10(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc10);
            else if (GameObject.Find("Npc11(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc11);
            else if (GameObject.Find("Npc12(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc12);
            else if (GameObject.Find("Npc13(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc13);
            else if (GameObject.Find("Npc14(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_honestSentencesNpc14);
            else
                _savedDialog = buildSentence(_honestSentencesNpc15);
            return (firstTime() + _savedDialog);
        }
        return ("As I said, " + _savedDialog);
    }

    private string lieTalk()
    {
        if (_savedDialog == null)
        {
            if (GameObject.Find("Npc1(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc1);
            else if (GameObject.Find("Npc2(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc2);
            else if (GameObject.Find("Npc3(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc3);
            else if (GameObject.Find("Npc4(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc4);
            else if (GameObject.Find("Npc5(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc5);
            else if (GameObject.Find("Npc6(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc6);
            else if (GameObject.Find("Npc7(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc7);
            else if (GameObject.Find("Npc8(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc8);
            else if (GameObject.Find("Npc9(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc9);
            else if (GameObject.Find("Npc10(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc10);
            else if (GameObject.Find("Npc11(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc11);
            else if (GameObject.Find("Npc12(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc12);
            else if (GameObject.Find("Npc13(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc13);
            else if (GameObject.Find("Npc14(Clone)").GetComponent<NpcManager>()._isMurderer == true)
                _savedDialog = buildSentence(_lieSentencesNpc14);
            else
                _savedDialog = buildSentence(_lieSentencesNpc15);
            return (firstTime() + _savedDialog);
        }
        return ("As I said, " + _savedDialog);
    }

    public void Talk()
    {
        //GameObject.Find("Text").GetComponent<Text>().text = "test";
        UIManager.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;

        //GameObject.Find("Canvas").SetActive(true);

        //        UIManager.instance.gameObject.SetActive(true);
        if (this.gameObject.GetComponent<NpcManager>()._isDialogHonest == true)
            UIManager.instance.NewText.text = honestTalk();
        else if (this.gameObject.GetComponent<NpcManager>()._isDialogLie == true)
            UIManager.instance.NewText.text = lieTalk();
        else
            UIManager.instance.NewText.text = _irrelevantSentences[UnityEngine.Random.Range(0, _irrelevantSentences.Length)];
    }
}
