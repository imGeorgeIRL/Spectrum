INCLUDE globals.ink
#speaker:Austin
{fridge_Interactions > 0: -> AlreadyDone | ->Main}
=== Main ===
what should I eat for breakfast?
    * Cereal
        which cereal?
            ** Fruity loops
                ~ dailyTasks(1)
                 So tasty!
                ~ fridge_Interactions++
                -> DONE
            ** Corn Flakes
                ~ dailyTasks(1)
                 okay I suppose...
                ~ fridge_Interactions++
                -> DONE
    * Toast
        ~ dailyTasks(1)
         <dangle a=0.5> yummy toast!</dangle>
        ~ fridge_Interactions++
        -> DONE
    * Nothing
         Not feeling that hungry this morning...
        ~ fridge_Interactions++
        -> DONE

        
=== AlreadyDone ===
hmm, i've already eaten...
-> DONE