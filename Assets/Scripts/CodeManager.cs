using UnityEngine;
using System.Collections;

public class CodeManager {
	
	// Manages the secret code and checks guesses against it,
	// returning the number of full and partial matches.
	
	private int numSlots = 5;
	private int numTypes = 3;
	public int numGuesses = 0;
	
	private int[] secretCode;
	
	// make a new random code that will be stored as a member variable
	public void GenerateCode (int slots, int types) {
		numSlots = slots;
		numTypes = types;
		secretCode = new int[numSlots];
		for (int i = 0; i<numSlots; i++){
			secretCode[i] = Random.Range(0,numTypes);
		}
	}
	
	// check guess against secret code,
	// return number of full and partial matches	
	public int[] EvaluateGuess (int[] guess) {
		numGuesses++;
		int fullMatches = 0;
		int partialMatches = 0;
		int[] secretCodeClone = secretCode.Clone() as int[];
		int[] guessClone = guess.Clone() as int[];

		for(int i = 0; i < numSlots; i++){
			if(guessClone[i] == secretCodeClone[i]){
				fullMatches += 1;
				secretCodeClone[i] = 8;
				guessClone[i] = 9;
			}
		}

		for(int i = 0; i < numSlots; i++){
			for(int j = 0; j < secretCodeClone.Length; j++){
				if(guessClone[i] == secretCodeClone[j]){
					partialMatches += 1;
					secretCodeClone[j] = 8;
					guessClone[i] = 9;
				}
			}
		}
		return new int[] {fullMatches, partialMatches};
	}

}
