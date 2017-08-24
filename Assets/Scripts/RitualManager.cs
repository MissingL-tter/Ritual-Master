using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualManager : MonoBehaviour {

    GameManager gameManager;
    RitualGenerator ritualGenerator;
    public Resource[] resources;
    public int[] guess;
    public int[] solution;
    int visited;

    void Start () {
        gameManager = GameManager.instance;
        visited = gameManager.resourceTypes.Length;
        ritualGenerator = GetComponent<RitualGenerator>();
    }
    
    // check guess against solution,
    // return number of full and partial matches
    public int[] EvaluateGuess () {

        resources = transform.parent.GetComponentsInChildren<Resource>();
        solution = ritualGenerator.solution.Clone() as int[];
        guess = new int[solution.Length];
        for (int i = 0; i < guess.Length; i++) {
            guess[i] = resources[i].id;
        }

        int fullMatches = 0;
        int partialMatches = 0;

        for (int i = 0; i < solution.Length; i++) {
            if (guess[i] == solution[i]) {
                fullMatches += 1;
                solution[i] = visited;
                guess[i] = visited + 1;
            }
        }

        for (int i = 0; i < solution.Length; i++) {
            for (int j = 0; j < solution.Length; j++) {
                if (guess[i] == solution[j]) {
                    partialMatches += 1;
                    solution[j] = visited;
                    guess[i] = visited + 1;
                }
            }
        }

        return new int[] {fullMatches, partialMatches};
    }

}
