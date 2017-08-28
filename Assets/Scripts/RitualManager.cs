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

    void Update() {

        // Get resources that have been added to the ritual
        resources = transform.GetComponentsInChildren<Resource>();

    }
    
    // check guess against solution,
    // return number of full and partial matches
    public int[] EvaluateGuess () {

        int fullMatches = 0;
        int partialMatches = 0;

        try {
            guess = new int[solution.Length];
            for (int i = 0; i < guess.Length; i++) {
                guess[i] = resources[i].id;
            }

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
            
        } catch (System.IndexOutOfRangeException) {
            Debug.Log("Cannot Perform an Incomplete Ritual");
        }

        Debug.Log("Full Matches: " + fullMatches);
        Debug.Log("Partial Matches: " + partialMatches);

        return new int[] {fullMatches, partialMatches};
    }

}
