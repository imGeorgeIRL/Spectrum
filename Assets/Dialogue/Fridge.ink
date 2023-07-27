INCLUDE globals.ink 

{Day_Of_Week:
- 0: {isNight: ->NightTime | ->Monday}
- 1: -> Tuesday
- 2: -> Wednesday
- 3: -> Thursday
- 4: -> Friday
}


=== Monday ===
{fridge_Interactions == 1: -> AlreadyDone | ->MondayNotEaten}
=MondayNotEaten
what should I eat for breakfast?#speaker:Austin
    * Cereal
        which cereal?
            ** Fruity loops
                 So tasty!
                ~ fridge_Interactions = 1
                -> DONE
            ** Corn Flakes
                 okay I suppose...
                ~ fridge_Interactions = 1
                -> DONE
    * Toast
         <dangle a=0.5> yummy toast!</dangle>
        ~ fridge_Interactions = 1
        -> DONE
    * Nothing
         Not feeling that hungry this morning...
        ~ fridge_Interactions = 1
        -> DONE

        
=== AlreadyDone ===
hmm, i've already eaten...#speaker:Austin
-> DONE


//*************************************************


==NightTime===
{hasEatenDinner: ->isTrue | ->notTrue }

    =isTrue
    I've already had dinner#speaker:Austin
    Maybe I could watch some tv now?
    ->DONE


    =notTrue
        What should I eat for dinner?#speaker:Austin
            *[Cereal]
                I know its not that great for me
                but I'm super tired.
                ~hasEatenDinner = true
            
            *[Mac and Cheese]
                I'm almost out,
                I should go shopping sometime this week.
                ~hasEatenDinner = true
            
            *[Toast]
                I love toast
                I could eat it for every meal!
                ~hasEatenDinner = true
            
        -I didn't even realise I was that hungry
        ->DONE
->DONE


===Tuesday===
{ fridge_Interactions == 1 : ->Tuesday2 }
bleh
->DONE


=Tuesday2
->DONE

===Wednesday===
->DONE

===Thursday===
->DONE

===Friday===
->DONE